var rpedrettiBlazorComponents = window.rpedrettiBlazorComponents || {};
rpedrettiBlazorComponents.helpers = {
    focusById: function (id) {
        $(`#${id}`).focus();
        return 1;
    },

    senseClickOutside: function ($evtTarget, $container) {
        if (($evtTarget).closest($container).length === 0) {
            return true;
        }
    }
};