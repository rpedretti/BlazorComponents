import { HereMap } from "./HereMap";

namespace rpedrettiBlazorComponents.hereMaps {
    export const map = new HereMap();
}

declare global {
    interface Window {
        rpedrettiBlazorComponents: typeof rpedrettiBlazorComponents;
    }
}

window.rpedrettiBlazorComponents = rpedrettiBlazorComponents;