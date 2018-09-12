import { Helpers } from "../Helpers";

export class Infobox {
    private _infoboxes = new Map<string, Microsoft.Maps.Infobox>();
    private normalizeOptions = (options: Microsoft.Maps.IInfoboxOptions) => {
        Helpers.removeEmpty(options);
        if (options.location) {
            const l = options.location;
            options.location = new Microsoft.Maps.Location(l.latitude, l.longitude);
        }
    };

    public register = (id: string, center: Microsoft.Maps.Location, options: Microsoft.Maps.IInfoboxOptions) => {
        this.normalizeOptions(options);
        const _infobox = new Microsoft.Maps.Infobox(center, options);
        this._infoboxes.set(id, _infobox);

        return 1;
    };

    public get = (id: string, propName: string) => {
        return this._infoboxes.get(id)!!['get' + propName]();
    };

    public setHtmlContent = (id: string, content: string) => {
        this._infoboxes.get(id)!!.setHtmlContent(content);

        return 1;
    };

    public setLocation = (id: string, location: { latitude: number, longitude: number }) => {
        const msLocation = new Microsoft.Maps.Location(location.latitude, location.longitude);
        this._infoboxes.get(id)!!.setLocation(msLocation);

        return 1;
    };

    public setMap = (id: string, mapInstance: Microsoft.Maps.Map) => {
        this._infoboxes.get(id)!!.setMap(mapInstance);

        return 1;
    };

    public setOptions = (id: string, options: Microsoft.Maps.IInfoboxOptions, ignoreDelay = false) => {
        this.normalizeOptions(options);
        this._infoboxes.get(id)!!.setOptions(options, ignoreDelay);

        return 1;
    };
}