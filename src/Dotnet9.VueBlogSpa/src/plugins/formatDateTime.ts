import type { App } from 'vue';
import dayjs from 'dayjs';
import type { Dayjs } from 'dayjs';

export default {
    install(app: App) {
        //格式化日期
        app.config.globalProperties.$formatDate = (date: string | number | Date | Dayjs | null | undefined, format: string = "YYYY-MM-DD HH:mm:ss"): string => {
            return dayjs(date).format(format)
        }
        
    }
}