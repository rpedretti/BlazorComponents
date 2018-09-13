import { Polygon } from './Entities/Polygon';
import { Polyline } from './Entities/Polyline';
import { Pushpin } from './Entities/Pushpin';
import { Infobox } from './Entities/Infobox';
import { DotNetEntity, DotNetPushpin, DotNetPolyline, DotNetPolygon } from './Entities/DotNetEntityTypes';
import { TrafficModule } from './Modules/Traffic.module';
import { DirectionsModule } from './Modules/Directions.module';
import { Helpers } from './Helpers';
import { Dictionary } from './Collections';

export class BingMaps {

    private readonly _maps = new Map<string, { map: Microsoft.Maps.Map, mapRef: any }>();
    private _handlers = new Map<string, Dictionary<Microsoft.Maps.IHandlerId>>();
    private _t_handlers = new Map<string, Dictionary<Microsoft.Maps.IHandlerId>>();

    private _initHandler = new Map<string, { eventName: string, refEventName: string }[]>();
    private _initChangeHandler = new Map<string, { eventName: string, refEventName: string }[]>();
    private _t_initHandler = new Map<string, { eventName: string, refEventName: string }[]>();

    private readonly _layers = new Map<string, { layerInstance: Microsoft.Maps.Layer }>();
    private readonly notInit: { mapId: string, mapRef: any, config: any }[] = [];

    private invokeOnDotNet = (mapId: string, e: Microsoft.Maps.IMouseEventArgs, refEventName: string): void => {
        var evt = Object.assign({}, e) as any;
        evt.getX = e.getX();
        evt.getY = e.getY();
        delete evt.primitive;
        delete evt.layer;
        delete evt.target;

        this._maps.get(mapId)!!.mapRef.invokeMethodAsync(refEventName, evt);
    }

    private invokeChangeOnDotNet = (mapId: string, eventName: string, refEventName: string): void => {
        this._maps.get(mapId)!!.mapRef.invokeMethodAsync(refEventName, eventName);
    }

    private addEventHandler = (mapId: string, eventName: string, refEventName: string) => {
        const mapInstance = this._maps.get(mapId);
        if (!!mapInstance) {
            const handler = Microsoft.Maps.Events.addHandler(mapInstance.map, eventName, (e) => {
                this.invokeOnDotNet(mapId, e as any, refEventName);
            });
            if (!this._handlers.has(mapId)) {
                this._handlers.set(mapId, {});
            }
            this._handlers.get(mapId)!![eventName] = handler;

        } else {
            throw new Error(`Map ${mapId} does not exist`);
        }
    };

    private addChangeEventHandler = (mapId: string, eventName: string, refEventName: string) => {
        const mapInstance = this._maps.get(mapId);
        if (!!mapInstance) {
            const handler = Microsoft.Maps.Events.addThrottledHandler(mapInstance.map as any, eventName, (e) => {
                this.invokeChangeOnDotNet(mapId, eventName, refEventName);
            });
            if (!this._handlers.has(mapId)) {
                this._handlers.set(mapId, {});
            }
            this._handlers.get(mapId)!![eventName] = handler;

            return 1;
        } else {
            throw new Error(`Map ${mapId} does not exist`);
        }
    };

    private addThrottledEventHandler = (mapId: string, eventName: string, refEventName: string) => {
        const mapInstance = this._maps.get(mapId);
        if (!!mapInstance) {
            const handler = Microsoft.Maps.Events.addThrottledHandler(mapInstance.map as any, eventName, (e) => {
                this.invokeChangeOnDotNet(mapId, "t_" + eventName, refEventName);
            });
            if (!this._t_handlers.has(mapId)) {
                this._t_handlers.set(mapId, {});
            }
            this._t_handlers.get(mapId)!![eventName] = handler;

            return 1;
        } else {
            throw new Error(`Map ${mapId} does not exist`);
        }
    };

    public initScript = (apiKey: string, mapLanguage: string): number => {
        var mapScriptTag = document.createElement('script');
        mapScriptTag.type = 'text/javascript';
        document.head.appendChild(mapScriptTag);
        const language = mapLanguage || window.navigator.language;
        mapScriptTag.src = `https://www.bing.com/api/maps/mapcontrol?callback=getBingMaps&key=${apiKey}&setLang=${language}`;

        return 1;
    }

    public initMaps = (): number => {
        this.notInit.forEach(({ mapId, mapRef, config }) => {
            const map = new Microsoft.Maps.Map(`#${mapId}`, config);
            this._maps.set(mapId, { mapRef, map });

            for (var { eventName, refEventName } of this._initHandler.get(mapId) || []) {
                this.attachEvent(mapId, eventName, refEventName);
            }
            this._initHandler.delete(mapId);


            for (var { eventName, refEventName } of this._initChangeHandler.get(mapId) || []) {
                this.attachChangeEvent(mapId,eventName, refEventName);
            }
            this._initChangeHandler.delete(mapId);

            for (var { eventName, refEventName } of this._t_initHandler.get(mapId) || []) {
                this.attachThrottleEvent(mapId, eventName, refEventName);
            }
            this._t_initHandler.delete(mapId);

            mapRef.invokeMethodAsync('NotifyMapLoaded');
        });

        return 1;
    }

    public getMap = (mapRef: any, mapId: string, config: any): number => {
        Helpers.removeEmpty(config);
        try {
            const map = new Microsoft.Maps.Map(`#${mapId}`, config);
            this._maps.set(mapId, { mapRef, map });
            mapRef.invokeMethodAsync('NotifyMapLoaded');

            return 1;
        } catch (e) {
            this.notInit.push({ mapId, mapRef, config });
            return 0;
        }
    }

    public unloadMap = (mapId: string): number => {
        const mapObj = this._maps.get(mapId);
        if (!!mapObj) {
            const map = mapObj.map;
            map.entities.clear();
            map.layers.clear();
            map.dispose();
            this._maps.delete(mapId);
            this._handlers.delete(mapId);
            this._t_handlers.delete(mapId);
        } else {
            throw new Error(`Map ${mapId} does not exist`);
        }

        return 1;
    }

    public updateView = (mapId: string, config: Microsoft.Maps.IViewOptions): number => {
        Helpers.removeEmpty(config);
        const mapObj = this._maps.get(mapId);
        if (!!mapObj) {
            mapObj.map.setView(config);
        } else {
            throw new Error(`Map ${mapId} does not exist`);
        }

        return 1;
    }

    public loadModule = (mapId: string, moduleId: string, funcName: string, funcParams: any): number => {
        const mapObj = this._maps.get(mapId);
        if (!!mapObj) {
            const map = mapObj.map;
            if (!!funcName) {
                const func = Helpers.genericFunction(funcName);
                if (!!func) {
                    Microsoft.Maps.loadModule(moduleId, func.call(null, map, funcParams));
                } else {
                    throw new Error(`Function ${funcName} does not exist in global scope`);
                }
            } else {
                Microsoft.Maps.loadModule(moduleId, () => { });
            }
            return 1;
        } else {
            throw new Error(`Map ${mapId} does not exist`);
        }
    }

    public addItems = (mapId: string, items: DotNetEntity[]): number => {
        let instances: Microsoft.Maps.IPrimitive[] = [];
        for (var item of items) {
            let instance;
            switch (item.type) {
                case 'bingmappushpin':
                    instance = this.pushpin.buildInstance(item as DotNetPushpin);
                    break;
                case 'bingmappolyline':
                    instance = this.polyline.buildInstance(item as DotNetPolyline);
                    break;
                case 'bingmappolygon':
                    instance = this.polygon.buildInstance(item as DotNetPolygon);
                    break;
            }
            instances.push(instance);
        }

        if (instances.length > 0) {
            const mapObj = this._maps.get(mapId);
            if (!!mapObj) {
                mapObj.map.entities.push(instances);
            } else {
                throw new Error(`Map ${mapId} does not exist`);
            }
        }

        return 1;
    }

    public addItem = (mapId: string, item: DotNetEntity) => {
        let instance;
        switch (item.type) {
            case 'bingmappushpin':
                instance = this.pushpin.buildInstance(item as DotNetPushpin);
                break;
            case 'bingmappolyline':
                instance = this.polyline.buildInstance(item as DotNetPolyline);
                break;
            case 'bingmappolygon':
                instance = this.polygon.buildInstance(item as DotNetPolygon);
                break;
        }

        const mapObj = this._maps.get(mapId);
        if (!!mapObj) {
            mapObj.map.entities.push(instance);
            return 1;
        } else {
            throw new Error(`Map ${mapId} does not exist`);
        }

    }

    public updateItem = (mapId: string, item: DotNetEntity) => {
        switch (item.type) {
            case 'bingmappushpin':
                this.pushpin.update(item as DotNetPushpin);
                break;
            case 'bingmappolyline':
                this.polyline.update(item as DotNetPolyline);
                break;
            case 'bingmappolygon':
                this.polygon.update(item as DotNetPolygon);
                break;
        }

        return 1;
    }

    public attachInfoBox = (mapId, infoboxId) => {
        const mapObj = this._maps.get(mapId);
        if (!!mapObj) {
            this.infobox.setMap(infoboxId, mapObj.map);
        } else {
            throw new Error(`Map ${mapId} does not exist`);
        }

        return 1;
    }

    public removeItem = (mapId: string, item: DotNetEntity) => {
        let instance;
        switch (item.type) {
            case 'bingmappushpin':
                instance = this.pushpin.remove(item.id);
                break;
            case 'bingmappolyline':
                instance = this.polyline.remove(item.id);
                break;
            case 'bingmappolygon':
                instance = this.polygon.remove(item.id);
                break;
        }

        const mapObj = this._maps.get(mapId);
        if (!!mapObj) {
            mapObj.map.entities.remove(instance);
            return 1;
        } else {
            throw new Error(`Map ${mapId} does not exist`);
        }
    }

    public removeItems = (mapId: string, items: DotNetEntity[]) => {
        let instances: Microsoft.Maps.IPrimitive[] = [];
        for (var item of items) {
            let instance;
            switch (item.type) {
                case 'bingmappushpin':
                    instance = this.pushpin.remove(item.id);
                    break;
                case 'bingmappolyline':
                    instance = this.polyline.remove(item.id);
                    break;
                case 'bingmappolygon':
                    instance = this.polygon.remove(item.id);
                    break;
            }
            instances.push(instance);
        }

        if (instances.length > 0) {
            const mapObj = this._maps.get(mapId);
            if (!!mapObj) {
                mapObj.map.entities.remove(instances as any);
            } else {
                throw new Error(`Map ${mapId} does not exist`);
            }
        }
    }

    public clearItems = (mapId: string) => {
        const mapObj = this._maps.get(mapId);
        if (!!mapObj) {
            mapObj.map.entities.clear();
        }

        return 1;
    }

    public addLayer = (mapId: string, id) => {
        const mapObj = this._maps.get(mapId);
        const layerObj = this._layers.get(id);
        if (!!mapObj && !!layerObj) {
            mapObj.map.layers.insert(layerObj.layerInstance);
        }
    }

    public removeLayer = (mapId: string, id: string) => {
        const mapObj = this._maps.get(mapId);
        const layerObj = this._layers.get(id);
        if (!!mapObj && !!layerObj) {
            mapObj.map.layers.remove(layerObj.layerInstance);
        }
    }

    public attachEvent = (mapId: string, eventName: string, refEventName: string) => {
        const mapObj = this._maps.get(mapId);
        if (mapObj) {
            this.addEventHandler(mapId, eventName, refEventName);
        } else {
            if (!this._initHandler.has(mapId)) {
                this._initHandler.set(mapId, []);
            }
            this._initHandler.get(mapId)!!.push({ eventName, refEventName });
        }

        return 1;
    };

    public attachChangeEvent = (mapId: string, eventName: string, refEventName: string) => {
        const mapObj = this._maps.get(mapId);
        if (mapObj) {
            this.addChangeEventHandler(mapId, eventName, refEventName);
        } else {
            if (!this._initChangeHandler.has(mapId)) {
                this._initChangeHandler.set(mapId, []);
            }
            this._initChangeHandler.get(mapId)!!.push({ eventName, refEventName });
        }

        return 1;
    };


    public attachThrottleEvent = (mapId: string, eventName: string, refEventName: string) => {
        const mapObj = this._maps.get(mapId);
        if (mapObj) {
            this.addThrottledEventHandler(mapId, eventName, refEventName);
        } else {
            if (!this._t_initHandler.has(mapId)) {
                this._t_initHandler.set(mapId, []);
            }
            this._t_initHandler.get(mapId)!!.push({ eventName, refEventName });
        }

        return 1;
    }

    public detachEvent = (mapId: string, eventName: string) => {
        const handler = this._handlers.get(mapId)!![eventName];
        Microsoft.Maps.Events.removeHandler(handler);

        return 1;
    }

    public readonly polygon = new Polygon();
    public readonly polyline = new Polyline();
    public readonly pushpin = new Pushpin();
    public readonly infobox = new Infobox();

    public readonly modules = {
        traffic: new TrafficModule(),
        directions: new DirectionsModule()
    }
}


