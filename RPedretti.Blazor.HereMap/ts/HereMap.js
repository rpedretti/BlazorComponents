"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const load_external_scripts_1 = require("load-external-scripts");
const DotNetMapOptions_1 = require("./types/DotNetMapOptions");
class HereMap {
    constructor() {
        this.initService = (appId, appCode, useCIT = true) => __awaiter(this, void 0, void 0, function* () {
            if (HereMap.platform === undefined) {
                yield load_external_scripts_1.default({ src: `${HereMap.baseScriptUrl}${HereMap.coreModuleScript}.js`, id: HereMap.coreModuleScript });
                yield load_external_scripts_1.default({ src: `${HereMap.baseScriptUrl}${HereMap.serviceModuleScript}.js`, id: HereMap.serviceModuleScript });
                HereMap.platform = new H.service.Platform({
                    app_code: appCode,
                    app_id: appId,
                    useCIT: useCIT,
                    useHTTPS: true
                });
            }
            return 1;
        });
        this.loadModules = (modules) => __awaiter(this, void 0, void 0, function* () {
            yield Promise.all(modules.map(m => load_external_scripts_1.default({ src: `${HereMap.baseScriptUrl}${m}.js`, id: m })));
            if (modules.indexOf("ui") >= 0) {
                const link = document.createElement('link');
                link.rel = 'stylesheet';
                link.type = 'text/css';
                link.href = 'https://js.api.here.com/v3/3.0/mapsjs-ui.css?dp-version=1533195059';
                document.head.appendChild(link);
            }
            return 1;
        });
        this.initMap = (map, options) => {
            var pixelRatio = window.devicePixelRatio || 1;
            var defaultLayers = HereMap.platform.createDefaultLayers({
                tileSize: pixelRatio === 1 ? 256 : 512,
                ppi: pixelRatio === 1 ? undefined : 320
            });
            var mapInstance = new H.Map(map, defaultLayers.normal.map, DotNetMapOptions_1.DotNetMapOptions.toHereMapOptions(options));
            HereMap._maps.set(map.id, { instance: mapInstance, element: map });
            HereMap._mapDefaultLayer.set(mapInstance, defaultLayers);
            return 1;
        };
        this.addDefaultBehaviour = (mapId) => {
            if (!HereMap._maps.has(mapId)) {
                throw new Error(`${mapId} does not exists`);
            }
            new H.mapevents.Behavior(new H.mapevents.MapEvents(HereMap._maps.get(mapId).instance));
            return 1;
        };
        this.addDefaultUi = (mapId) => {
            if (!HereMap._maps.has(mapId)) {
                throw new Error(`${mapId} does not exists`);
            }
            var map = HereMap._maps.get(mapId);
            var layer = HereMap._mapDefaultLayer.get(map.instance);
            H.ui.UI.createDefault(map.instance, layer);
            return 1;
        };
    }
}
HereMap.baseScriptUrl = 'https://js.api.here.com/v3/3.0/mapsjs-';
HereMap.coreModuleScript = 'core';
HereMap.serviceModuleScript = 'service';
HereMap._maps = new Map();
HereMap._mapDefaultLayer = new Map();
exports.HereMap = HereMap;
//# sourceMappingURL=HereMap.js.map