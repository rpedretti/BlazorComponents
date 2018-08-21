var rpedrettiBlazorComponents = window.rpedrettiBlazorComponents || {};
rpedrettiBlazorComponents.helpers = (function () {
    return {
        focusById: (id) => {
            $(`#${id}`).focus();
            return 1;
        },

        senseClickOutside: ($evtTarget, $container) => {
            if ($evtTarget.closest($container).length === 0) {
                return true;
            }
        },

        removeEmpty: (obj) => {
            Object.keys(obj).forEach(k =>
                obj[k] && typeof obj[k] === 'object' && rpedrettiBlazorComponents.helpers.removeEmpty(obj[k]) ||
                !obj[k] && (obj[k] === undefined || obj[k] === null) && delete obj[k]
            );
            return obj;
        },
        genericFunction: (path) => {
            return [window].concat(path.split('.')).reduce(function (prev, curr) {
                return prev[curr];
            });
        }
    };
})();