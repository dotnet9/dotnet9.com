
import { memuList } from "@/router/MenuConfig";
import { onMounted, reactive } from "vue";
import { useRouter } from "vue-router"

/**
 * 侧边菜单生成
 * @returns 
 */
export function useSlideMenu() {
    const router = useRouter();
    const memuTree = reactive<MenuItem[]>([])

    onMounted(() => {
        initMenuList();
    })

    const getUrl = (item) => {

        if (!item.routeName) {
            if (item.title) {
                item.routeName = item.title
            }

        }
        let url = router.getRoutes().filter(a => a.name == item.routeName).map(a => a.path)[0]
        
        return url;
    }

    const initMenuList = () => {

        memuList.forEach(item => {
            let url = getUrl(item)
            var children: MenuItem[] = []
            item.children?.forEach(childItem => {
                children.push({
                    title: childItem.title,
                    url: getUrl(childItem)
                })
            });
            memuTree.push({
                title: item.title,
                url: url,
                children: children
            })
        })
        // console.log('memuTree',memuTree)
    };

    return {
        memuTree
    }
}

interface MenuItem {
    title: string
    icon?: string
    url: string
    children?: MenuItem[]
}