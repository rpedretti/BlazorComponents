import 'bingmaps'

export class DirectionsModule {
    public init = (map: Microsoft.Maps.Map, options: any) => {
        Microsoft.Maps.loadModule('Microsoft.Maps.Directions', () => {
            //Create an instance of the directions manager.
            const directionsManager = new Microsoft.Maps.Directions.DirectionsManager(map);
            if (options.moduleRef) {
                Microsoft.Maps.Events.addHandler(directionsManager, 'directionsUpdated', () => {
                    options.moduleRef.invokeMethodAsync('DirectionsUpdatedAsync');
                });
            }


            //Specify where to display the route instructions.
            const dom = document.getElementById(options.itineraryPanelId);
            directionsManager.setRenderOptions({ itineraryContainer: dom || undefined });

            //Specify the where to display the input panel
            directionsManager.showInputPanel(options.inputPanelId);

            return 1;
        });
    }
}