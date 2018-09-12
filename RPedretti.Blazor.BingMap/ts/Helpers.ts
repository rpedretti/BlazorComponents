export class Helpers {

    static removeEmpty(obj) {
        Object.keys(obj).forEach(k =>
            obj[k] && typeof obj[k] === 'object' && this.removeEmpty(obj[k]) ||
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

    static parseColor(c: string | { a: number, r: number, g: number, b: number}): Microsoft.Maps.Color {
        return typeof c === 'string' ? Microsoft.Maps.Color.fromHex(c) : new Microsoft.Maps.Color(c.a, c.r, c.g, c.b);
    }
}