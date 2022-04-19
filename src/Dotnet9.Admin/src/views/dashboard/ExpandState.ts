import {ref} from 'vue'

const expandState = ref(true)

const showExpandButton = ref(true)

const handlerExpand = (state: boolean) => {
    expandState.value = state
}

export {
    expandState,
    handlerExpand,
    showExpandButton
}