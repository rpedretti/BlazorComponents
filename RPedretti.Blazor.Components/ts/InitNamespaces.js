"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const SuggestBox_1 = require("./SuggestBox");
var rpedrettiBlazorComponents;
(function (rpedrettiBlazorComponents) {
    rpedrettiBlazorComponents.suggestbox = new SuggestBox_1.Suggestbox();
})(rpedrettiBlazorComponents || (rpedrettiBlazorComponents = {}));
window.rpedrettiBlazorComponents = rpedrettiBlazorComponents;
$(document).on('click', rpedrettiBlazorComponents.suggestbox.clearSelection);
//# sourceMappingURL=InitNamespaces.js.map