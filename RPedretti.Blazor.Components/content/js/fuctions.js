Blazor.registerFunction('focus', (id) => {
    $(`#${id}`).focus();
    return 1;
});

function initSuggestBox(id) {
    const element = $(`#${id}`);
    console.debug(`init suggestbox #${id}`, element);

    element.keydown($event => {
        var parent = $($event.target.parentNode.parentNode)[0];
        if (parent.classList.contains('-open') && ($event.key === 'ArrowDown' || $event.key === 'ArrowUp')) {
            $event.preventDefault();
        }

        if ($event.key !== 'ArrowDown' &&
            $event.key !== 'ArrowUp' &&
            $event.key !== 'Enter' &&
            $event.key !== 'Esc' &&
            $event.key !== 'Tabp') {
            event.stopPropagation();
        }
    });

    return 1;
}

Blazor.registerFunction('initSuggestBox', initSuggestBox)