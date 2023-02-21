import { defineStore } from 'pinia'

export const useMetaStore = defineStore('metaStore', {
  state: () => {
    return {
      title: 'Dotnet9-一个专注互联网分享精神的博客网站'
    }
  }
})
