window.rpedrettiBlazorComponents = window.rpedrettiBlazorComponents || {};
window.rpedrettiBlazorComponents.bingMaps = (function () {
    const _maps = new Map();
    const notInit = [];
    return {
        initScript: function (apiKey) {
            var mapScriptTag = document.createElement('script');
            mapScriptTag.type = 'text/javascript';
            document.head.appendChild(mapScriptTag);
            mapScriptTag.src = `https://www.bing.com/api/maps/mapcontrol?callback=getBingMaps`;
        },
        initMaps: function () {
            notInit.forEach(({ mapId, mapRef, config }) => {
                const map = Microsoft.Maps.Map(`#${mapId}`, config);
                _maps.set(mapId, { mapRef, map });
                mapRef.invokeMethodAsync('MapLoaded');
            });
        },
        getMap: function (mapRef, mapId, config) {
            try {
                const map = Microsoft.Maps.Map(`#${mapId}`, config);
                _maps.set(mapId, { mapRef, map });
                mapRef.invokeMethodAsync('MapLoaded');
            } catch {
                notInit.push({ mapId, mapRef, config });
            }
        },
        updateView: function (mapId, config) {
            _maps.get(mapId).map.setView(config);
        },
        loadModule(mapId, moduleId, funcName, funcParams) {
            const map = _maps.get(mapId).map;
            Microsoft.Maps.loadModule(moduleId, window[funcName].bind(Object.assign({}, funcParams, { map })));
        }
    };
})();

function getBingMaps() {
    rpedrettiBlazorComponents.bingMaps.initMaps();
}