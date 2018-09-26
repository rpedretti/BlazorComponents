import { Helpers } from "./helpers";

export class Suggestbox {
    private readonly registeredSuggestboxes = new Map<string, any>();

    public unregisterSuggestBox = (id) => {
        this.registeredSuggestboxes.delete(id);
    }

    public initSuggestBox = (dotnetRef, inputId) => {
        this.registeredSuggestboxes.set(inputId, dotnetRef);
        const element = $(`#${inputId}`);
        element.keydown($event => {
            var parent = $($event!!.target!!.parentNode!!.parentNode!!);
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
    }

    public clearSelection = (e) => {
        for (var [id, ref] of this.registeredSuggestboxes) {
            const element = $(`#${id}`);
            if (Helpers.senseClickOutside($(e.target), element)) {
                ref.invokeMethodAsync('ClearSelection');
                return;
            }
        }
    }
}

