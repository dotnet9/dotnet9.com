<template>
  <div class="article-container">
    <span v-if="article.isTop" class="article-tag">
      <b>
        <svg-icon icon-class="pin" />
        {{ t('settings.pinned') }}
      </b>
    </span>
    <span v-else-if="article.isFeatured" class="article-tag">
      <b>
        <svg-icon icon-class="hot" />
        {{ t('settings.featured') }}
      </b>
    </span>
    <div class="article">
      <div class="article-thumbnail">
        <img v-if="article.cover" v-lazy="article.cover" alt="" />
        <img v-else src="@/assets/default-cover.jpg" />
        <span class="thumbnail-screen" :style="gradientBackground" />
      </div>
      <div class="article-content">
        <span>
          <b v-if="article.categoryNames">
            {{ article.categoryNames }}
          </b>
          <ob-skeleton v-else tag="b" height="20px" width="35px" />
          <ul v-if="article.tagNames && article.tagNames.length > 0">
            <li v-for="tag in article.tagNames" :key="tag">
              <em># {{ tag }}</em>
            </li>
          </ul>
          <ul v-else-if="article.tagNames && article.tagNames.length <= 0">
            <li>
              <em># {{ t('settings.default-tag') }}</em>
            </li>
          </ul>
          <ul v-else>
            <ob-skeleton v-if="!article.tagNames" :count="2" tag="li" height="16px" width="35px" />
          </ul>
        </span>
        <h1 class="article-title" v-if="article.title" @click="toArticle" data-dia="article-link">
          <a>
            <span> {{ article.title }}</span>
            <svg-icon v-if="article.status == 2" icon-class="lock" class="lock-svg" />
          </a>
        </h1>
        <ob-skeleton v-else tag="h1" height="3rem" />
        <p v-if="article.description">{{ article.description }}</p>
        <ob-skeleton v-else tag="p" :count="5" height="16px" />
        <div class="article-footer" v-if="article.original && article.creationTime">
          <div class="flex flex-row items-center">
            <img
              class="hover:opacity-50 cursor-pointer"
              :src="article.originalAvatar || ''"
              alt="author avatar"
              @click="handleAuthorClick(article.originalLink)" />
            <span class="text-ob-dim">
              <strong
                class="text-ob-normal pr-1.5 hover:text-ob hover:opacity-50 cursor-pointer"
                @click="handleAuthorClick(article.originalLink)">
                {{ article.original }}
              </strong>
              {{ t('settings.shared-on') }} {{ t(`settings.months[${new Date(article.creationTime).getMonth()}]`) }}
              {{ new Date(article.creationTime).getDate() }}, {{ new Date(article.creationTime).getFullYear() }}
            </span>
          </div>
        </div>
        <div class="article-footer" v-else>
          <div class="flex flex-row items-center mt-6">
            <ob-skeleton class="mr-2" height="28px" width="28px" :circle="true" />
            <span class="text-ob-dim mt-1">
              <ob-skeleton height="20px" width="150px" />
            </span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, toRefs, getCurrentInstance } from 'vue'
import { useAppStore } from '@/stores/app'
import { useUserStore } from '@/stores/user'
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import emitter from '@/utils/mitt'

export default defineComponent({
  name: 'ArticleCard',
  props: ['data'],
  setup(props) {
    const proxy: any = getCurrentInstance()?.appContext.config.globalProperties
    const appStore = useAppStore()
    const userStore = useUserStore()
    const router = useRouter()
    const { t } = useI18n()
    const handleAuthorClick = (link: string) => {
      if (link === '') link = window.location.href
      window.open(link)
    }
    const toArticle = () => {
      let isAccess = false
      userStore.accessArticles.forEach((item: any) => {
        if (item == props.data.id) {
          isAccess = true
        }
      })
      if (props.data.status === 2 && isAccess == false) {
        if (userStore.userInfo === '') {
          proxy.$notify({
            title: 'Warning',
            message: '该文章受密码保护,请登录后访问',
            type: 'warning'
          })
        } else {
          emitter.emit('changeArticlePasswordDialogVisible', props.data.id)
        }
      } else {
        router.push({ path: '/' + props.data.year + '/' + props.data.month + '/' + props.data.slug })
      }
    }
    return {
      gradientBackground: computed(() => {
        return { background: appStore.themeConfig.header_gradient_css }
      }),
      article: toRefs(props).data,
      handleAuthorClick,
      toArticle,
      t
    }
  }
})
</script>

<style lang="scss" scoped>
.article-title:hover {
  cursor: default;
}
</style>
