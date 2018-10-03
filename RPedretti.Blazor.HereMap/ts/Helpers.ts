export class Helper {
    static removeEmpty(obj) {
        Object.keys(obj).forEach(k =>
            obj[k] && typeof obj[k] === 'object' && this.removeEmpty(obj[k]) ||
            !obj[k] && (obj[k] === undefined || obj[k] === null) && delete obj[k]
        );
        return obj;
    }
}