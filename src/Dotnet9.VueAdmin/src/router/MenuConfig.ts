const memuList = [
    {
        title: "仪表盘",
        icon: "",
        routeName: "仪表盘"
    },
    {
        title: "博客",
        children: [
            {
                title: "文章"
            },
            {
                title: "分类",
                routeName: '文章分类'
            },
            {
                title: "标签"
            },
            {
                title: "友情链接"
            }
        ]
    },
    {
        title: '系统',
        icon: 'setting',
        children: [
            {
                title: '设置',
                routeName: '系统设置'
            }
        ]
    }
]


export {
    memuList
}