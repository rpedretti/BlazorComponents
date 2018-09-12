import { Polygon } from './Entities/Polygon';
import { Polyline } from './Entities/Polyline';
import { Pushpin } from './Entities/Pushpin';
import { DotNetEntity, DotNetPushpin, DotNetPolyline, DotNetPolygon } from './Entities/DotNetEntityTypes';
import { TrafficModule } from './Modules/Traffic.module';
import { DirectionsModule } from './Modules/Directions.module';
import { Helpers } from './Helpers';
import { Infobox } from './Entities/Infobox';

export class BingMaps {
    private readonly _maps = new Map<string, { map: Microsoft.Maps.Map, mapRef: any }>();
    private readonly _layers = new Map<string, { layerInstance: Microsoft.Maps.Layer }>();
    private readonly notInit: { mapId: string, mapRef: any, config: any }[] = [];

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

    public loadModule = (mapId: string , moduleId: string, funcName: string, funcParams: any): number => {
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

    public removeLayer = (mapId, id) => {
        const mapObj = this._maps.get(mapId);
        const layerObj = this._layers.get(id);
        if (!!mapObj && !!layerObj) {
            mapObj.map.layers.remove(layerObj.layerInstance);
        }
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


