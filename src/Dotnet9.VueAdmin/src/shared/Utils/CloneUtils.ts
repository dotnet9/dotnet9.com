export class CloneUtils {
    /**
     * 判断对象是否为数组
     * @param obj
     * @returns
     */
    private static IsArray(obj: any) {
        return obj && typeof obj == "object" && obj instanceof Array;
    }

    /**
     * 对象深拷贝
     * @param tSource
     * @returns
     */
    public static DeepClone<T>(tSource: T, tTarget?: Record<string, any> | T): T {
        if (this.IsArray(tSource)) {
            tTarget = tTarget || [];
        } else {
            tTarget = tTarget || {};
        }
        for (const key in tSource) {
            if (Object.prototype.hasOwnProperty.call(tSource, key)) {
                if (typeof tSource[key] === "object" && typeof tSource[key] !== null) {
                    tTarget[key] = this.IsArray(tSource[key]) ? [] : {};
                    this.DeepClone(tSource[key], tTarget[key]);
                } else {
                    tTarget[key] = tSource[key];
                }
            }
        }
        return tTarget as T;
    }

    /**
     * 对象浅拷贝
     * @param tSource
     * @returns
     */
    public static SimpleClone<T>(tSource: T, tTarget?: Record<string, any> | T): T {
        if (this.IsArray(tSource)) {
            tTarget = tTarget || [];
        } else {
            tTarget = tTarget || {};
        }
        for (const key in tSource) {
            if (Object.prototype.hasOwnProperty.call(tSource, key)) {
                tTarget[key] = tSource[key];
            }
        }
        return tTarget as T;
    }

}