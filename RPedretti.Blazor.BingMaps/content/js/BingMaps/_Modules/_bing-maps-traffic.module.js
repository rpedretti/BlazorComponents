window.rpedrettiBlazorComponents = window.rpedrettiBlazorComponents || {};
window.rpedrettiBlazorComponents.bingMaps = window.rpedrettiBlazorComponents.bingMaps || {};
window.rpedrettiBlazorComponents.bingMaps.modules = window.rpedrettiBlazorComponents.bingMaps.modules || {};
window.rpedrettiBlazorComponents.bingMaps.modules.traffic = (function () {
    const _managers = new Map();
    return {
        init: function () {
            Microsoft.Maps.loadModule('Microsoft.Maps.Traffic', () => {
                //Create an instance of the directions manager.
                const trafficManager = new Microsoft.Maps.Traffic.TrafficManager(this.map);
                if (this.showTraffic) {
                    trafficManager.show();
                }
                _managers.set(this.mapId, trafficManager);
            });
        },
        updateTraffic: function (mapId, options) {
            _managers.get(mapId).setOptions(options);
        }
    };
})();