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
            notInit.forEach(({ mapId, config }) => {
                const map = Microsoft.Maps.Map(`#${mapId}`, config);
                _maps.set(mapId, map);
            });
        },
        getMap: function (mapId, config) {
            
            try {
                const map = Microsoft.Maps.Map(`#${mapId}`, config);
                _maps.set(mapId, map);
            } catch {
                notInit.push({ mapId: mapId, config: config });
            }
        },
        updateView: function (mapId, config) {
            _maps.get(mapId).setView(config);
        }
    };
})();

function getBingMaps() {
    rpedrettiBlazorComponents.bingMaps.initMaps();
}