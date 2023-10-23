import { ref } from 'vue';
import { defineStore } from 'pinia';

/**
 * 设置手机菜单显示隐藏
 */
export const useDrawerSettingStore = defineStore("drawerSetting", () => {
    const drawer = ref<boolean>(false);
    function setDrawer(value: boolean): void {
        drawer.value = value;
    }
    return { drawer, setDrawer }
})