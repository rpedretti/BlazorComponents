import { DotNetEntity, DotNetPolygon } from "./DotNetEntityTypes";
import { Dictionary } from "./../Collections";
import { Helpers } from "./../Helpers";
import * as _ from 'lodash';

export class Polygon {
    private _polygons = new Map<string, Microsoft.Maps.Polygon>();
    private _handlers = new Map<string, Dictionary<Microsoft.Maps.IHandlerId>>();
    private _initHandler = new Map<string, { polygonId: string, eventName: string, polygonRef, refEventName: string }[]>();
    private normalizePolygon = (polygon) => {
        Helpers.removeEmpty(polygon);
        const options = polygon.options;
        if (options) {
            if (options.fillColor) {
                options.fillColor = Helpers.parseColor(options.fillColor);
            }
            if (options.strokeColor) {
                options.strokeColor = Helpers.parseColor(options.strokeColor);
            }
        }
    };

    private updateOptions = (polygon: DotNetPolygon) => {
        const polygonInstance = this._polygons.get(polygon.id);
        if (polygonInstance) {
            polygonInstance.setOptions(polygon.options);
        } else {
            console.error(`no polygon with id ${polygon.id}`)
        }
    };

    private updateLocations = (polygon: DotNetPolygon) => {
        const coordinates = polygon.coordinates.map(c => new Microsoft.Maps.Location(c.latitude, c.longitude));
        const polygonInstance = this._polygons.get(polygon.id);
        if (polygonInstance) {
            polygonInstance.setLocations(coordinates);
        } else {

        }
    };

    private updateRings = (polygon: DotNetPolygon) => {
        const rings = polygon.rings.map(ring => ring.map(c => new Microsoft.Maps.Location(c.latitude, c.longitude)));
        const polygonInstance = this._polygons.get(polygon.id);
        if (polygonInstance) {
            polygonInstance.setRings(rings);
        } else {

        }
    };

    private clearEvents = (polygonId: string) => {
        const handlers = this._handlers.get(polygonId) || [];
        for (const handler in handlers) {
            Microsoft.Maps.Events.removeHandler(handlers[handler]);
        }

        return true;
    };

    private addEventHandler = (polygonInstance: Microsoft.Maps.Polygon, polygonId: string, eventName: string, polygonRef, refEventName: string) => {
        const handler = Microsoft.Maps.Events.addHandler(polygonInstance, eventName, (e) => {
            var evt = Object.assign({}, e) as any;
            const event = e as Microsoft.Maps.IMouseEventArgs;
            evt.getX = event.getX();
            evt.getY = event.getY();
            delete evt.primitive;
            delete evt.layer;
            delete evt.target;
            polygonRef.invokeMethodAsync(refEventName, evt);
        });
        if (!this._handlers.has(polygonId)) {
            this._handlers.set(polygonId, {});
        }
        this._handlers.get(polygonId)!![eventName] = handler;
    };

    public buildInstance = (polygon: DotNetPolygon) => {
        this.normalizePolygon(polygon);
        let polygonInstance;
        if (polygon.coordinates) {
            const coordinates = polygon.coordinates.map(c => new Microsoft.Maps.Location(c.latitude, c.longitude));
            polygonInstance = new Microsoft.Maps.Polygon(coordinates, polygon.options);
        } else if (polygon.rings) {
            const rings = polygon.rings.map(ring => ring.map(c => new Microsoft.Maps.Location(c.latitude, c.longitude)));
            polygonInstance = new Microsoft.Maps.Polygon(rings, polygon.options);
        }

        for (var { polygonId, eventName, polygonRef, refEventName } of this._initHandler.get(polygon.id) || []) {
            this.addEventHandler(polygonInstance, polygonId, eventName, polygonRef, refEventName);
        }

        this._initHandler.delete(polygon.id);

        this._polygons.set(polygon.id, polygonInstance);

        return polygonInstance;
    };

    update = (polygon) => {
        this.normalizePolygon(polygon);
        const originalPolygon = this._polygons.get(polygon.id)!!;
        var c = polygon.coordinates;
        if (c.length > 0) {
            c.push(c[0]);
        }
        if (!_.isEqual(c, JSON.parse(JSON.stringify(originalPolygon.getLocations())))) {
            this.updateLocations(polygon);
        } else if (!_.isEqual(polygon.rings, JSON.stringify(originalPolygon.getRings()))) {
            this.updateRings(polygon);
        } else if (polygon.options) {
            this.updateOptions(polygon);
        }

        return 1;
    };

    public attachEvent = (polygonId: string, eventName: string, polygonRef, refEventName: string) => {
        const polygonInstance = this._polygons.get(polygonId);
        if (polygonInstance) {
            this.addEventHandler(polygonInstance, polygonId, eventName, polygonRef, refEventName);
        } else {
            if (!this._initHandler.has(polygonId)) {
                this._initHandler.set(polygonId, []);
            }
            this._initHandler.get(polygonId)!!.push({ polygonId, eventName, polygonRef, refEventName });
        }

        return 1;
    };

    remove = (polygonId: string) => {
        const instance = this._polygons.get(polygonId);
        this.clearEvents(polygonId);
        this._polygons.delete(polygonId);
        return instance;
    }
}