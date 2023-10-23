import { defineStore } from "pinia"
import { ref } from 'vue'
/**
 * 设置主题颜色
 */
export const useThemeSettingStore = defineStore("themeSetting", () => {
    const theme = ref("light")
    function setTheme(color: "light" | "dark"): void {
        theme.value = color
    }
    return { theme, setTheme }
})