import { CloneUtils } from "./Utils/CloneUtils";


const table = {
    border: true
}

const pagination = {
    small: true,
    layout: "total, prev, pager, next,sizes",
    'hide-on-single-page': true,
    background: true,
    'page-sizes': [10, 20, 50, 100],

}

const DeepClonePagination = () => {
    return CloneUtils.DeepClone(pagination);
}


class QcConfig {
    getTableConfig() {
        return {
            border: true
        }
    }
    getPageConfig() {
        return {
            small: true,
            layout: "total, prev, pager, next,sizes",
            'hide-on-single-page': true,
            background: true,
            'page-sizes': [10, 20, 50, 100],

        }
    }
}

export interface BasePageModel{
    index: number
    pageSize?: number
    total: number,
    data:any[] | undefined
}

// export interface PageModel<T> extends BasePageModel {
//     data?: T[]
// }

export {
    table,
    pagination,
    DeepClonePagination,
    QcConfig
}