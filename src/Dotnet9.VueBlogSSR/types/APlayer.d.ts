
type audio = {
    name?: string
    artist?: string
    url?: string
    cover?: string
    lrc?: string
    theme?: string
    type?: 'auto' | 'hls' | 'normal' = "auto"
}
class APlayer {
    constructor(option: APlayerOptions) {

    }
    static version: string
    mode: string
    play()

    pause()

    seek(time: number)

    toggle()

    on(event: string, handler: function)

    on(event: string, handler: function)
    theme(color: string, index: number)
    setMode(mode: 'mini' | 'normal')
    notice(text: string, time: number, opacity: number)
    skipBack()
    skipForward()
    destroy()
    lrc: {
        show()
        hide()
        toggle()
    }
    list: {
        show()
        hide()
        toggle()
        add(a: audio[])
        remove(index: number)
        switch(index: number)
        clear()
    }
    audio: HTMLAudioElement
}
class APlayerOptions {
    /**
     * 播放器容器元素
     */
    container: HTMLElement
    /**
     * 开启吸底模式
     */
    fixed?: boolean = false

    mini?: boolean = false
    autoplay?: boolean = false
    theme?: string = '#b7daff'
    loop?: 'all' | 'one' | 'none' = 'all'
    order?: 'list' | 'random' = 'list'
    preload?: 'none' | 'metadata' | 'auto' = 'auto'
    volume?: number = 0.7
    audio: audio[]
    customAudioType?: (container: HTMLElement, audio: audio, player: APlayer) => void
    mutex?: boolean = true
    lrcType?: number = 0
    listFolded?: boolean = false
    listMaxHeight?: string
    storageName?: string = 'aplayer-setting'
}

declare module "APlayer" {
    export = APlayer;
}
declare var APlayer: APlayer;
