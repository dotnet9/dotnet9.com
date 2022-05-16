interface CategoryItem{
    id?:number
    name?:string
    slug?:string
    cover?:string
    description?:string
    parentId?:number
    index?:number
    isShow:boolean
}

export {
    CategoryItem
}