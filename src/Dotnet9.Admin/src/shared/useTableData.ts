import {ref} from "vue";

export function useTableData() {
    const loading = ref(false)
    const pageLayout = "total,prev,pager,next"

    return {
        loading,
        pageLayout
    }
}
