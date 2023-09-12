
import { defineStore } from 'pinia'

const useVSetting = defineStore('v-setting', {
    persist: {
        storage: window.localStorage,
        key: 'v-setting'
    },
    state: () => {
        return {
            menuOpen: false,
            _token: '',
            showViewBg: true
        }
    },
    actions: {
        toggleMenu() {
            this.menuOpen = !this.menuOpen;
        },
        setToken(token: string) {
            this._token = token
        }
    }
})

export {
    useVSetting
}