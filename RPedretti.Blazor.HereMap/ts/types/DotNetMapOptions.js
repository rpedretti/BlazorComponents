"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const Helpers_1 = require("../Helpers");
class DotNetMapOptions {
    static toHereMapOptions(instance) {
        const clone = Object.assign({}, instance);
        Helpers_1.Helper.removeEmpty(clone);
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
exports.DotNetMapOptions = DotNetMapOptions;
//# sourceMappingURL=DotNetMapOptions.js.map