interface AlbumItem {
    id?: number
    name?: string
    slug?: string
    cover?: string
    description?: string
    parentId?: number
    index?: number
    isShow: boolean
}

export {
    AlbumItem
}