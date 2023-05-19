import { onMounted, onUnmounted, nextTick } from 'vue'

const hasScrolled = (element: HTMLElement) => {
    if (element.scrollHeight > element.clientHeight) {
        return { direction: 'v', value: Math.abs(element.scrollHeight - element.clientHeight) }
    }
    if (element.scrollWidth > element.clientWidth) {
        return {
            direction: 'h', value: Math.abs(element.scrollWidth - element.clientWidth)
        }
    }
    return null;
}

/**
 * 检查是否出现横向滚动条
 * @param element 
 * @returns 大于0，就是滚动条的宽度
 */
const checkVScrollBarIsShow = (element: HTMLElement) => {
    return Math.abs(element.scrollHeight - element.clientHeight)
}


/**
 * 窗口大小变化 不包含滚动条
 * @param func 
 */
const useWindowClientResize = (func: (w: number, h: number) => void) => {
    const onResize = () => {
        func(document.body.clientWidth, document.body.clientHeight)
    }
    onMounted(() => {
        window.addEventListener('resize', onResize)
        nextTick(() => {
            //挂载成功后调用一次
            onResize();
        })
    })


    onUnmounted(() => {
        window.removeEventListener('resize', onResize)
    })
}

export {
    checkVScrollBarIsShow,
    useWindowClientResize
}