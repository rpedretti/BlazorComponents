export class Helpers {
    static focusById(id) {
        $(`#${id}`).focus();
        return 1;
    }

    static senseClickOutside($evtTarget, $container): boolean {
        if ($evtTarget.closest($container).length === 0) {
            return true;
        }

        return false;
    }

    static removeEmpty(obj) {
        Object.keys(obj).forEach(k =>
            obj[k] && typeof obj[k] === 'object' && Helpers.removeEmpty(obj[k]) ||
            !obj[k] && (obj[k] === undefined || obj[k] === null) && delete obj[k]
        );
        return obj;
    }

    static genericFunction(path): Function | undefined {
        var func = [window].concat(path.split('.')).reduce(function (prev, curr) {
            return (prev as any)[curr as any];
        });

        return func as any;
    }
}