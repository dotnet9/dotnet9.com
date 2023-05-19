import { ref } from 'vue'

const isCollapse = ref(false)

const expandHandler = () => {
    if (isCollapse.value) {
        isCollapse.value = false
    } else {
        isCollapse.value = true;
    }
    console.log(isCollapse.value)
}

const closeSlideMenu = () => {
    isCollapse.value = false
}


export {
    expandHandler,
    isCollapse,
    closeSlideMenu
}