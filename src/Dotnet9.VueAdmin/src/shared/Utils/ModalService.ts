import { h, render } from 'vue'

import { ElDialog } from 'element-plus'


interface ModalOption {
    /**
     * 支持组件和字符串
     */
    title: any
    draggable?: true
}

interface ModalSlot {
    header: any,
    props: any
}

class ModalService {

    /**
     * 创建一个弹出框
     * @param component 组件
     * @param opt 配置
     */
    static Create(component: any, props: any, opt?: ModalOption,) {
        return new Promise((resolve, reject) => {

            const close = () => {
                render(null, container)
                document.body.removeChild(container);
                resolve(true)
            }

            const modelValue = true

            const dialogProps = {
                modelValue: modelValue,
                title: opt?.title,
                draggable: opt?.draggable,
                destroyOnClose: true,
                onClosed: close,
            }

            const closeHandler = () => {
                if (vNode.component?.props.modelValue) {
                    vNode.component!.props.modelValue = false
                }

            }

            const container = document.createElement('div')
            document.body.appendChild(container)
            var vNode = h(ElDialog, dialogProps, {
                default: () => {
                    let type = typeof component;
                    if (type == 'string' || type == 'number') {
                        return h('div', component)
                    } else {
                        return h(component, {
                            ...props,
                            onClose: closeHandler
                        })
                    }
                },
                header: () => {
                    if (dialogProps) {
                        let type = typeof dialogProps.title
                        if (type == 'string' || type == 'number') {
                            return h('span', dialogProps.title)
                        }
                        return h(dialogProps.title, null)
                    }
                }
            });

            render(vNode, container)
            return vNode.component;
        })


    }
}

export {
    ModalService
}