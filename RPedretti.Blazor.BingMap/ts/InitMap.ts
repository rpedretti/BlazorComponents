import { BingMaps } from "./BingMap";

namespace rpedrettiBlazorComponents {
    export const bingMaps = new BingMaps()
}

declare global {
    interface Window {
        rpedrettiBlazorComponents: typeof rpedrettiBlazorComponents;
        getBingMaps: Function
    }
}

window.rpedrettiBlazorComponents = rpedrettiBlazorComponents;

window.getBingMaps = function() {
    rpedrettiBlazorComponents.bingMaps.initMaps();
}