import 'bingmaps';

export class TrafficModule {
    private _managers = new Map<string, Microsoft.Maps.Traffic.TrafficManager>();

    public init = (map, options) => {
        Microsoft.Maps.loadModule('Microsoft.Maps.Traffic', () => {
            const trafficManager = new Microsoft.Maps.Traffic.TrafficManager(map);
            if (options.showTraffic) {
                trafficManager.show();
            }
            this._managers.set(options.mapId, trafficManager);
        });

        return 1;
    };

    public updateTraffic = (mapId: string, options: Microsoft.Maps.Traffic.ITrafficOptions) => {
        const manager = this._managers.get(mapId);
        if (!!manager) {
            manager.setOptions(options);
        }

        return 1;
    }
}