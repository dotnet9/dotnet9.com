export interface MenuItem {
    index: string
    label: string
    icon?: string
}

export interface SubMenu {
    index: string
    label: string
    icon?: string
    hideGroup?: boolean
    children?: MenuItem[]
}

const PostMenu: SubMenu[] = [
    {
        index: '文章',
        label: '文章',
        hideGroup: false,
        children: [
            {
                index: '/admin/post/write',
                label: '写一篇'
            },
            {
                index: '/admin/post',
                label: '文章管理',
            }
        ]
    },{
        label:'分类',
        index:'分类',
        hideGroup:false,
        children:[
            {
                index:'/admin/editcate',
                label:'添加分类'
            },
            {
                index: '/admin/categories',
                label: '分类管理'
            }
        ]
    }
]

const settingMenu: SubMenu[] = [
    {
        index: '/admin/setting',
        label: '设置',
        children: []
    }
]

const tagMenu: SubMenu[] = [
    {
        label: '标签',
        index: '标签',
        hideGroup: true,
        children: [
            {
                index: '/admin/tag', label: '标签管理'
            },
            {
                index: '/admin/tag/edit', label: '标签添加'
            }
        ]
    }
]

export {
    PostMenu,
    settingMenu,
    tagMenu
}