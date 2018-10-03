import { Helper } from "../Helpers";

export class DotNetMapOptions {

    center?: { latitude: number, longitude: number, altitude: number, altitueContext: H.geo.AltitudeContext };
    zoom?: number;
    bounds?: { top: number, left: number, bottom: number, right: number };
    //layers?: H.map.layer.Layer[];
    engineType?: H.Map.EngineType;
    pixelRatio?: number;
    imprint?: H.map.Imprint.Options;
    renderBaseBackground?: H.Map.BackgroundRange;
    autoColor?: boolean;
    margin?: number;
    padding?: H.map.ViewPort.Padding;
    fixedCenter?: boolean;

    public static toHereMapOptions(instance: DotNetMapOptions): H.Map.Options {
        const clone = Object.assign({}, instance);
        Helper.removeEmpty(clone);
        return {
            autoColor: clone.autoColor,
            zoom: clone.zoom,
            bounds: clone.bounds ?
                new H.geo.Rect(clone.bounds.top, clone.bounds.left, clone.bounds.bottom, clone.bounds.right) :
                clone.bounds,
            center: clone.center ?
                { alt: clone.center.altitude, lat: clone.center.latitude, lng: clone.center.longitude, ctx: clone.center.altitueContext } :
                undefined,
            engineType: clone.engineType,
            fixedCenter: clone.fixedCenter,
            imprint: clone.imprint,
            renderBaseBackground: clone.renderBaseBackground,
            margin: clone.margin,
            padding: clone.padding
        };
    }
}