function initSuggestBox(id) {
    const element = $(`#${id}`);


    $(document).on('click', function (e) {
        if (senseClickOutside($(e.target), element)) {
            Blazor.invokeDotNetMethod({
                type: {
                    assembly: 'RPedretti.Blazor.Components',
                    name: 'RPedretti.Blazor.Components.SuggestBox.SuggestBoxList'
                },
                method: {
                    name: 'ClearSelection'
                }
            }, id);
            return;
        }
    });


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
}

Blazor.registerFunction('initSuggestBox', initSuggestBox);