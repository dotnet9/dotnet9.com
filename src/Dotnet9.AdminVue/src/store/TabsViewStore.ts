import { defineStore } from "pinia";

const useTabsViewStore = defineStore('useTabsViewStore', {
    // persist: {
    //     key: 'useTabsViewStore',
    //     storage: sessionStorage
    // },
    state: () => {
        return ({
            tableList: [{
                fullPath: '/admin/dashboard',
                isFix: true,
                title: '控制台'
            }] as TabRoute[],
            activePath: '/admin/dashboard',
            /**
             * 排除的
             */
            exCludeList: [] as string[]
        })
    },
    getters: {
        /**
         * 获取最后一个页面
         * @param state 
         * @returns 
         */
        getLastUrl: (state) => state.tableList[state.tableList.length - 1].fullPath,
        /**
         *  获取需要keepAlive的组件名称列表
         * @param state 
         * @returns 
         */
        getKeepAliveComponentNames: (state) => state.tableList.filter(a => a.componentName).map(a => a.componentName!),
    },
    actions: {
        close(url: string) {
            console.log('关闭:' + url)
            let index = this.tableList.findIndex(a => a.fullPath == url);
            var item = this.tableList[index]
            if (item.componentName) {
                if (this.exCludeList.findIndex(a => a == item.componentName) == -1) {
                    this.exCludeList.push(item.componentName);
                }
            }
            // 获取下一个
            if ((index + 1) == this.tableList.length) {
                url = this.tableList[index - 1].fullPath;
            }
            else {
                url = this.tableList[index + 1].fullPath;
            }
            this.tableList.splice(index, 1);
            console.log('跳转:' + url, this.tableList)


        },
        add(item: TabRoute) {
            if (item.fullPath.startsWith("/admin") == false) return;
            if (this.tableList.findIndex(a => a.fullPath == item.fullPath) == -1) {
                this.tableList.push(item)

            }
            this.exCludeList = this.exCludeList.filter(a => a != item.componentName);
        },

        clear() {
            this.tableList = [];
        }

    }
})

export {
    useTabsViewStore
}

export interface TabRoute {
    fullPath: string,
    isFix: boolean,
    title: string
    componentName?: string
}

