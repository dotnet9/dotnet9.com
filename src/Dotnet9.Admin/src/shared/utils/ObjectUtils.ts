
function deepCopy<T extends Array<T> | any>(sourceData: T): T {
    if (Array.isArray(sourceData)) {
        return sourceData.map(item => deepCopy(item)) as T
    }
    const obj: T = {} as T
    for (let key in sourceData) {
        if ((typeof sourceData[key] === 'object') && sourceData[key] !== null) {
            obj[key] = deepCopy(sourceData[key])
        } else {
            obj[key] = sourceData[key]
        }
    }
    return obj
}

export {
    deepCopy
}