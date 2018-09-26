"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const helpers_1 = require("./helpers");
class Suggestbox {
    constructor() {
        this.registeredSuggestboxes = new Map();
        this.unregisterSuggestBox = (id) => {
            this.registeredSuggestboxes.delete(id);
        };
        this.initSuggestBox = (dotnetRef, inputId) => {
            this.registeredSuggestboxes.set(inputId, dotnetRef);
            const element = $(`#${inputId}`);
            element.keydown($event => {
                var parent = $($event.target.parentNode.parentNode);
                if (parent.hasClass('-open') && ($event.key === 'ArrowDown' || $event.key === 'ArrowUp')) {
                    $event.preventDefault();
                }
                if ($event.key !== 'ArrowDown' &&
                    $event.key !== 'ArrowUp' &&
                    $event.key !== 'Enter' &&
                    $event.key !== 'Escape' &&
                    $event.key !== 'Tab') {
                    $event.stopPropagation();
                }
            });
            return 1;
        };
        this.clearSelection = (e) => {
            for (var [id, ref] of this.registeredSuggestboxes) {
                const element = $(`#${id}`);
                if (helpers_1.Helpers.senseClickOutside($(e.target), element)) {
                    ref.invokeMethodAsync('ClearSelection');
                    return;
                }
            }
        };
    }
}
exports.Suggestbox = Suggestbox;
//# sourceMappingURL=SuggestBox.js.map