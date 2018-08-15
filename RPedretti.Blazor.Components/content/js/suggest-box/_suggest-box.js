window.rpedrettiBlazorComponents = window.rpedrettiBlazorComponents || {};
window.rpedrettiBlazorComponents.suggestbox =
    (function () {
        var registeredSuggestboxes = new Map();

        return {
            unregisterSuggestBox: function (id) {
                registeredSuggestboxes.delete(id);
            },

            initSuggestBox: function (dotnetRef, inputId) {
                registeredSuggestboxes.set(inputId, dotnetRef)
                const element = $(`#${inputId}`);
                element.keydown($event => {
                    var parent = $($event.target.parentNode.parentNode)[0];
                    if (parent.classList.contains('-open') && ($event.key === 'ArrowDown' || $event.key === 'ArrowUp')) {
                        $event.preventDefault();
                    }

                    if ($event.key !== 'ArrowDown' &&
                        $event.key !== 'ArrowUp' &&
                        $event.key !== 'Enter' &&
                        $event.key !== 'Escape' &&
                        $event.key !== 'Tab') {
                        event.stopPropagation();
                    }
                });

                return 1;
            },

            clearSelection: function (e) {
                for (var [id, ref] of registeredSuggestboxes) {
                    const element = $(`#${id}`);
                    if (window.rpedrettiBlazorComponents.helpers.senseClickOutside($(e.target), element)) {
                        ref.invokeMethodAsync('ClearSelection');
                        return;
                    }
                }
            }
        }
    })();

$(document).on('click', window.rpedrettiBlazorComponents.suggestbox.clearSelection);

