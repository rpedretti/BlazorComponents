window.rpedrettiBlazorComponents = window.rpedrettiBlazorComponents || {};
window.rpedrettiBlazorComponents.bingMaps = Object.assign(window.rpedrettiBlazorComponents.bingMaps || {}, (function () {
    const _maps = new Map();
    const notInit = [];
    return {
        initScript: function (apiKey, mapLanguage) {
            var mapScriptTag = document.createElement('script');
            mapScriptTag.type = 'text/javascript';
            document.head.appendChild(mapScriptTag);
            const language = mapLanguage || window.navigator.userLanguage || window.navigator.language;
            mapScriptTag.src = `https://www.bing.com/api/maps/mapcontrol?callback=getBingMaps&key=${apiKey}&setLang=${language}`;
        },
        initMaps: function () {
            notInit.forEach(({ mapId, mapRef, config }) => {
                const map = Microsoft.Maps.Map(`#${mapId}`, config);
                _maps.set(mapId, { mapRef, map });
                mapRef.invokeMethodAsync('NotifyMapLoaded');
            });
        },
        getMap: function (mapRef, mapId, config) {
            rpedrettiBlazorComponents.helpers.removeEmpty(config);
            try {
                const map = Microsoft.Maps.Map(`#${mapId}`, config);
                _maps.set(mapId, { mapRef, map });
                mapRef.invokeMethodAsync('NotifyMapLoaded');
            } catch (e) {
                notInit.push({ mapId, mapRef, config });
            }
        },
        unloadMap: function (mapID) {
            _maps.get(mapID).map.entities.clear();
            _maps.delete(mapID);
        },
        updateView: function (mapId, config) {
            rpedrettiBlazorComponents.helpers.removeEmpty(config);
            _maps.get(mapId).map.setView(config);
        },
        loadModule(mapId, moduleId, funcName, funcParams) {
            const map = _maps.get(mapId).map;
            if (funcName !== null) {
                const func = rpedrettiBlazorComponents.helpers.genericFunction(funcName);
                Microsoft.Maps.loadModule(moduleId, func.bind(Object.assign({}, funcParams, { map })));
            } else {
                Microsoft.Maps.loadModule(moduleId, () => { });
            }
        },

        pushpin: (function () {
            const _pushpins = new Map();
            const normalizePushpin = function (pushpin) {
                rpedrettiBlazorComponents.helpers.removeEmpty(pushpin);
                const options = pushpin.options;
                if (options.color) {
                    const c = options.color;
                    if (typeof c === 'string') {
                        options.color = Microsoft.Maps.Color.fromHex(c);
                    } else {
                        options.color = new Microsoft.Maps.Color(c.a, c.r, c.g, c.b);
                    }
                }
            };

            return {
                add: function (mapId, pushpin) {
                    normalizePushpin(pushpin);
                    const map = _maps.get(mapId).map;
                    const pushpinInstance = new Microsoft.Maps.Pushpin(pushpin.center, pushpin.options);

                    if (!_pushpins.has(mapId)) {
                        _pushpins.set(mapId, new Map());
                    }

                    _pushpins.get(mapId).set(pushpin.id, pushpinInstance);

                    map.entities.push(pushpinInstance);
                },
                updateOptions: function (mapId, pushpinId, pushpinOptions) {
                    normalizePushpin({ options: pushpinOptions });
                    const instance = _pushpins.get(mapId).get(pushpinId).setOption(pushpinOptions);
                },
                updateLocation: function (mapId, pushpinId, location) {
                    _pushpins.get(mapId).get(pushpinId).setLocation(location);
                },
                remove: function (mapId, pushpinId) {
                    const instance = _pushpins.get(mapId).get(pushpinId);
                    _maps.get(mapId).map.entities.remove(instance);
                }
            };
        })()
    };
})());

function getBingMaps() {
    rpedrettiBlazorComponents.bingMaps.initMaps();
}