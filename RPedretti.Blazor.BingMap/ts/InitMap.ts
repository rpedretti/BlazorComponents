import { BingMaps } from "./BingMap";
import { DevTools } from "./DevTools";

namespace rpedrettiBlazorComponents.bingMaps {
    export const map = new BingMaps()
    export const devTools = new DevTools();
}

declare global {
    interface Window {
        rpedrettiBlazorComponents: typeof rpedrettiBlazorComponents;
        getBingMaps: Function
    }
}

window.rpedrettiBlazorComponents = rpedrettiBlazorComponents;

window.getBingMaps = function() {
    rpedrettiBlazorComponents.bingMaps.map.initMaps();
}