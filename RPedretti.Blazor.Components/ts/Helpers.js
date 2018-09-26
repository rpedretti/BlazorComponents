"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Helpers {
    static focusById(id) {
        $(`#${id}`).focus();
        return 1;
    }
    static senseClickOutside($evtTarget, $container) {
        if ($evtTarget.closest($container).length === 0) {
            return true;
        }
        return false;
    }
    static removeEmpty(obj) {
        Object.keys(obj).forEach(k => obj[k] && typeof obj[k] === 'object' && Helpers.removeEmpty(obj[k]) ||
            !obj[k] && (obj[k] === undefined || obj[k] === null) && delete obj[k]);
        return obj;
    }
    static genericFunction(path) {
        var func = [window].concat(path.split('.')).reduce(function (prev, curr) {
            return prev[curr];
        });
        return func;
    }
}
exports.Helpers = Helpers;
//# sourceMappingURL=Helpers.js.map