Blazor.registerFunction('focusById', (id) => {
    $(`#${id}`).focus();
    return 1;
});

function senseClickOutside($evtTarget, $container) {
    if (($evtTarget).closest($container).length === 0) {
        return true;
    }
};