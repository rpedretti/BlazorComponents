import { DotNetEntity, DotNetPolyline } from "./DotNetEntityTypes";
import { Dictionary } from "./../Collections";
import { Helpers } from "./../Helpers";
import * as isEqual from 'lodash.isequal';

export class Polyline {
    private _polylines = new Map<string, Microsoft.Maps.Polyline>();
    private _handlers = new Map<string, Dictionary<Microsoft.Maps.IHandlerId>>();
    private _initHandler = new Map<string, { polylineId, eventName, polylineRef, refEventName }[]>();

    private normalizePolyline = (polyline: DotNetPolyline) => {
        Helpers.removeEmpty(polyline);
        const options = polyline.options as Microsoft.Maps.IPolylineOptions;
        if (options && options.strokeColor) {
            options.strokeColor = Helpers.parseColor(options.strokeColor);
        }
    };

    private updateOptions = (polyline: DotNetPolyline) => {
        const polylineInstance = this._polylines.get(polyline.id);
        if (polylineInstance) {
            polylineInstance.setOptions(polyline.options);
        } else {
            throw new Error(`Polyline with id ${polyline.id} does not exist`);
        }
    };

    private updateLocations = (polyline: DotNetPolyline) => {
        const polylineInstance = this._polylines.get(polyline.id);
        if (polylineInstance) {
            const coordinates = polyline.coordinates.map(c => new Microsoft.Maps.Location(c.latitude, c.longitude));
            polylineInstance.setLocations(coordinates);
        } else {
            throw new Error(`Polyline with id ${polyline.id} does not exist`);
        }
    };

    private clearEvents = (polylineId: string) => {
        const handlers = this._handlers.get(polylineId) || [];
        for (const handler in handlers) {
            Microsoft.Maps.Events.removeHandler(handlers[handler]);
        }

        return true;
    };

    private addEventHandler = (polylineInstance: Microsoft.Maps.Polyline, polylineId: string, eventName: string, polylineRef, refEventName: string) => {
        const handler = Microsoft.Maps.Events.addHandler(polylineInstance, eventName, (e) => {
            const evt = Object.assign({}, e) as any;
            if (!!e) {
                const event = e as Microsoft.Maps.IMouseEventArgs;
                evt.getX = event.getX();
                evt.getY = event.getY();
                delete evt.primitive;
                delete evt.layer;
                delete evt.target;
            }
            polylineRef.invokeMethodAsync(refEventName, evt);
        });
        if (!this._handlers.has(polylineId)) {
            this._handlers.set(polylineId, {});
        }
        this._handlers.get(polylineId)!![eventName] = handler;
    };

    public buildInstance = (polyline: DotNetPolyline): Microsoft.Maps.Polyline => {
        this.normalizePolyline(polyline);
        const coordinates = polyline.coordinates.map(c => new Microsoft.Maps.Location(c.latitude, c.longitude));
        const polylineInstance = new Microsoft.Maps.Polyline(coordinates, polyline.options);

        for (var { polylineId, eventName, polylineRef, refEventName } of this._initHandler.get(polyline.id) || []) {
            this.addEventHandler(polylineInstance, polylineId, eventName, polylineRef, refEventName);
        }

        this._initHandler.delete(polyline.id);

        this._polylines.set(polyline.id, polylineInstance);

        return polylineInstance;
    }

    public update = (polyline: DotNetPolyline): number => {
        this.normalizePolyline(polyline);
        const originalPolyline = this._polylines.get(polyline.id);
        if (originalPolyline && !isEqual(polyline.coordinates, JSON.parse(JSON.stringify(originalPolyline.getLocations())))) {
            this.updateLocations(polyline);
        }

        if (polyline.options) {
            this.updateOptions(polyline);
        }

        return 1;
    }

    public attachEvent = (polylineId: string, eventName: string, polylineRef, refEventName: string) => {
        const pushpinInstance = this._polylines.get(polylineId);
        if (pushpinInstance) {
            this.addEventHandler(pushpinInstance, polylineId, eventName, polylineRef, refEventName);
        } else {
            if (!this._initHandler.has(polylineId)) {
                this._initHandler.set(polylineId, []);
            }
            this._initHandler.get(polylineId)!!.push({ polylineId, eventName, polylineRef, refEventName });
        }

        return 1;
    }

    public remove = (polylineId: string) => {
        const instance = this._polylines.get(polylineId);
        this.clearEvents(polylineId);
        this._polylines.delete(polylineId);
        return instance;
    }
}