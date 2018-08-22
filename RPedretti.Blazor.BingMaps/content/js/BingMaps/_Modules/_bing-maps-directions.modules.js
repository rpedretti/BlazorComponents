window.rpedrettiBlazorComponents = window.rpedrettiBlazorComponents || {};
window.rpedrettiBlazorComponents.bingMaps = window.rpedrettiBlazorComponents.bingMaps || {};
window.rpedrettiBlazorComponents.bingMaps.modules = window.rpedrettiBlazorComponents.bingMaps.modules || {};
window.rpedrettiBlazorComponents.bingMaps.modules.directions = (function () {
    return {
        init: function () {
            Microsoft.Maps.loadModule('Microsoft.Maps.Directions', () => {
                //Create an instance of the directions manager.
                directionsManager = new Microsoft.Maps.Directions.DirectionsManager(this.map);
                if (this.moduleRef) {
                    Microsoft.Maps.Events.addHandler(directionsManager, 'directionsUpdated', () => {
                        this.moduleRef.invokeMethodAsync('DirectionsUpdatedAsync');
                    });
                }


                //Specify where to display the route instructions.
                directionsManager.setRenderOptions({ itineraryContainer: `#${this.itineraryPanelId}` });

                //Specify the where to display the input panel
                directionsManager.showInputPanel(this.inputPanelId);
            });
        }
    };
})();