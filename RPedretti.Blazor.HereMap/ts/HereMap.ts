import * as loadScript from 'load-external-scripts';
import { DotNetMapOptions } from './types/DotNetMapOptions';

export class HereMap {
    private static readonly baseScriptUrl = 'https://js.api.here.com/v3/3.0/mapsjs-';
    private static readonly coreModuleScript = 'core';
    private static readonly serviceModuleScript = 'service';
    private static readonly _maps = new Map<string, { instance: H.Map, element: Element }>();
    private static readonly _mapDefaultLayer = new Map<H.Map, H.service.DefaultLayers>();
    private static platform: H.service.Platform;

    initService = async (appId: string, appCode: string, useCIT = true): Promise<number> => {
        if (HereMap.platform === undefined) {
            await loadScript({ src: `${HereMap.baseScriptUrl}${HereMap.coreModuleScript}.js`, id: HereMap.coreModuleScript });
            await loadScript({ src: `${HereMap.baseScriptUrl}${HereMap.serviceModuleScript}.js`, id: HereMap.serviceModuleScript });
            HereMap.platform = new H.service.Platform({
                app_code: appCode,
                app_id: appId,
                useCIT: useCIT,
                useHTTPS: true
            });
        }

        return 1;
    }

    loadModules = async (modules: string[]) => {
        await Promise.all(modules.map(m => loadScript({ src: `${HereMap.baseScriptUrl}${m}.js`, id: m })));
        if (modules.indexOf("ui") >= 0) {
            const link = document.createElement('link');
            link.rel = 'stylesheet';
            link.type = 'text/css';
            link.href = 'https://js.api.here.com/v3/3.0/mapsjs-ui.css?dp-version=1533195059';
            document.head.appendChild(link);
        }
        return 1;
    }

    initMap = (map: Element, options: DotNetMapOptions): number => {
        var pixelRatio = window.devicePixelRatio || 1;
        var defaultLayers = HereMap.platform.createDefaultLayers({
            tileSize: pixelRatio === 1 ? 256 : 512,
            ppi: pixelRatio === 1 ? undefined : 320
        });
        var mapInstance = new H.Map(
            map,
            defaultLayers.normal.map,
            DotNetMapOptions.toHereMapOptions(options)
        );

        HereMap._maps.set(map.id, { instance: mapInstance, element: map });
        HereMap._mapDefaultLayer.set(mapInstance, defaultLayers);
        return 1;
    }

    addDefaultBehaviour = (mapId) => {
        if (!HereMap._maps.has(mapId)) {
            throw new Error(`${mapId} does not exists`);
        }

        new H.mapevents.Behavior(new H.mapevents.MapEvents(HereMap._maps.get(mapId)!!.instance));

        return 1;
    }

    addDefaultUi = (mapId) => {
        if (!HereMap._maps.has(mapId)) {
            throw new Error(`${mapId} does not exists`);
        }
        var map = HereMap._maps.get(mapId)!!;
        var layer = HereMap._mapDefaultLayer.get(map.instance)!!;
        H.ui.UI.createDefault(map.instance, layer);

        return 1;
    }
}