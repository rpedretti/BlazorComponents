import { DotNetEntity, DotNetPushpin } from "./DotNetEntityTypes";
import { Dictionary } from "./../Collections";
import { Helpers } from "./../Helpers";
import * as _ from 'lodash';

export class Pushpin {
    private _pushpins = new Map<string, Microsoft.Maps.Pushpin>();
    private _handlers = new Map<string, Dictionary<Microsoft.Maps.IHandlerId>>();
    private _initHandler = new Map<string, { pushpinId: string, eventName: string, pushpinRef, refEventName: string }[]>();

    private normalizePushpin = (pushpin: DotNetPushpin) => {
        Helpers.removeEmpty(pushpin);
        const options = pushpin.options;
        if (options && options.color) {
            options.color = Helpers.parseColor(options.color);
        }
    };

    private updateOptions = (pushpin: DotNetPushpin) => {
        const pushpinInstance = this._pushpins.get(pushpin.id);
        if (pushpinInstance) {
            pushpinInstance.setOptions(pushpin.options);
        }
    };

    private updateLocation = (pushpin) => {
        const pushpinInstance = this._pushpins.get(pushpin.id);
        if (pushpinInstance) {
            pushpinInstance.setLocation(pushpin.center);
        }
    };

    private clearTarget = (target: Microsoft.Maps.Pushpin, id: string) => {
        const clear = {
            center: target.getLocation(),
            id: id
        };

        return clear;
    };

    private clearEvents = (pushpinId: string) => {
        const handlers = this._handlers.get(pushpinId) || [];
        for (const handler in handlers) {
            Microsoft.Maps.Events.removeHandler(handlers[handler]);
        }
    };

    private addEventHandler = (pushpinInstance, pushpinId, eventName, pushpinRef, refEventName) => {
        const handler = Microsoft.Maps.Events.addHandler(pushpinInstance, eventName, (e) => {
            var evt = Object.assign({}, e) as any;
            const event = e as Microsoft.Maps.IMouseEventArgs;
            evt.getX = event.getX();
            evt.getY = event.getY();
            delete evt.primitive;
            delete evt.layer;
            evt.target = this.clearTarget(evt.target, pushpinId);
            pushpinRef.invokeMethodAsync(refEventName, evt);
        });
        if (!this._handlers.has(pushpinId)) {
            this._handlers.set(pushpinId, {});
        }
        this._handlers.get(pushpinId)!![eventName] = handler;
    };

    public buildInstance = (pushpin: DotNetPushpin): Microsoft.Maps.Pushpin => {
        this.normalizePushpin(pushpin);
        const pushpinInstance = new Microsoft.Maps.Pushpin(pushpin.center, pushpin.options);

        for (var { pushpinId, eventName, pushpinRef, refEventName } of this._initHandler.get(pushpin.id) || []) {
            this.addEventHandler(pushpinInstance, pushpinId, eventName, pushpinRef, refEventName);
        }

        this._initHandler.delete(pushpin.id);
        this._pushpins.set(pushpin.id, pushpinInstance);

        return pushpinInstance;
    }

    public update = (pushpin: DotNetPushpin) => {
        this.normalizePushpin(pushpin);
        const originalPushpin = this._pushpins.get(pushpin.id);
        if (originalPushpin && !_.isEqual(pushpin.center, JSON.parse(JSON.stringify(originalPushpin.getLocation())))) {
            this.updateLocation(pushpin);
        }

        if (pushpin.options) {
            this.updateOptions(pushpin);
        }

        return 1;
    }

    public getPropertie = (id: string, propName: string) => {
        return this._pushpins.get(id)!!['get' + propName]();
    };

    public getInstance = (pushpinId) => {
        const instance = this._pushpins.get(pushpinId);
        return instance;
    };
    public remove = (pushpinId: string) => {
        const instance = this._pushpins.get(pushpinId);
        this.clearEvents(pushpinId);
        this._pushpins.delete(pushpinId);
        return instance;
    };

    public attachEvent = (pushpinId, eventName, pushpinRef, refEventName) => {
        const pushpinInstance = this._pushpins.get(pushpinId);
        if (pushpinInstance) {
            this.addEventHandler(pushpinInstance, pushpinId, eventName, pushpinRef, refEventName);
        } else {
            if (!this._initHandler.has(pushpinId)) {
                this._initHandler.set(pushpinId, []);
            }
            this._initHandler.get(pushpinId)!!.push({ pushpinId, eventName, pushpinRef, refEventName });
        }

        return 1;
    };

    public detachEvent = (pushpinId: string, eventName: string) => {
        const handler = this._handlers.get(pushpinId)!![eventName];
        Microsoft.Maps.Events.removeHandler(handler);

        return 1;
    }
}