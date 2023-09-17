import { defineStore } from "pinia";

const useRouteStore = defineStore('router-store', {
    persist: {
        key: 'is-dynamic-route-store',
        storage: sessionStorage
    },
    state: () => {
        return {
            isStore: false
        }
    },
    actions: {
        addStore() {
            this.isStore = true
        },
        isStoreDynamic() {
            return this.isStore;
        }
    }
})

export {
    useRouteStore
}