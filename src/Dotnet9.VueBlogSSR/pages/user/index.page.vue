<template>
  <div class="banner" :style="cover">
    <h1 class="banner-title">个人中心</h1>
  </div>
  <v-card class="blog-container">
    <v-row class="mb-5">
      <v-alert variant="outlined" type="warning" prominent border="start">
        申请交换友情链接系统审核通过后，将会在本站友情链接页面显示
      </v-alert>
    </v-row>
    <div class="info">
      <span class="info-title">基本信息 | 申请友链</span>
    </div>
    <v-form v-model="state.form" @submit.prevent="onSubmit">
      <v-row class="info-wrapper">
        <v-col md="3" cols="12" class="avatar">
          <button id="pick-avatar">
            <v-avatar
              size="140"
              class="author-avatar"
              :image="info?.avatar ?? img"
            />
          </button>
        </v-col>
        <v-col md="7" cols="12">
          <v-text-field
            class="mt-3"
            label="昵称"
            placeholder="请输入您的昵称"
            variant="outlined"
            color="primary"
            density="compact"
            hint="您的昵称"
            :disabled="true"
            :model-value="info?.nickName"
          />
          <v-text-field
            class="mt-3"
            label="网站名称"
            placeholder="请输入您的网站名称"
            variant="outlined"
            color="primary"
            density="compact"
            hint="您的网站名称"
            :rules="[required]"
            v-model="state.link.siteName"
          />
          <v-text-field
            class="mt-3"
            label="个人网站"
            placeholder="http[s]://您的博客网址"
            variant="outlined"
            hint="您的博客网址"
            color="primary"
            density="compact"
            :rules="[required]"
            v-model="state.link.link"
          />
          <v-text-field
            class="mt-3"
            label="网站图标"
            placeholder="http[s]://您的网站图标"
            variant="outlined"
            hint="您的博客网址"
            color="primary"
            density="compact"
            :rules="[required]"
            v-model="state.link.logo"
          />
          <v-text-field
            class="mt-3"
            label="您的友链页面地址"
            placeholder="您的博客友情链接页面的地址（系统自动检测）"
            variant="outlined"
            hint="您的博客友情链接页面的地址"
            color="primary"
            density="compact"
            :rules="[required]"
            v-model="state.link.url"
          />
          <!-- <div class="mt-3 binding">
              <v-text-field
                label="邮箱"
                placeholder="请绑定邮箱"
                variant="outlined"
                color="primary"
                density="compact"
                hint="您的邮箱"
              /> -->
          <!-- <v-btn v-if="email" text small @click="openEmailModel">
                    修改绑定
                  </v-btn>
                  <v-btn v-else text small @click="openEmailModel">
                      绑定邮箱
                  </v-btn> -->
          <!-- </div> -->
          <v-textarea
            class="mt-3"
            label="简介"
            placeholder="介绍您的博客吧"
            variant="outlined"
            hint="您的博客网站介绍"
            color="primary"
            density="compact"
            :rules="[required]"
            v-model="state.link.remark"
          />
          <v-col md="3" cols="12" style="margin: auto; text-align: center">
            <v-btn
              type="submit"
              variant="outlined"
              color="success"
              :loading="state.loading"
              >提交</v-btn
            >
          </v-col>
        </v-col>
      </v-row>
    </v-form>
  </v-card>
</template>

<script setup lang="ts">
import { reactive, onMounted } from "vue";
import { useApp } from "~/stores/app";
import { useAuth } from "~/stores/auth";
import { useToast } from "~/stores/toast";
import img from "~/assets/images/1.jpg";
import { storeToRefs } from "pinia";
import { AddLinkOutput } from "~/api/models";
import OAuthApi from "~/api/OAuthApi";
import { computed } from "vue";
const appStore = useApp();
const authStore = useAuth();
const toastStore = useToast();
const { info } = storeToRefs(authStore);
const state = reactive({
  form: false,
  loading: false,
  link: {
    siteName: info.value?.siteName ?? "",
    url: info.value?.url ?? "",
    link: info.value?.link ?? "",
    remark: info.value?.remark ?? "",
    logo: info.value?.logo ?? "",
  } as AddLinkOutput,
  success: false,
});

//封面
const cover = computed(() => {
  return `background: url(${appStore.userCover()}) center center / cover no-repeat`;
});

const required = (v: string) => {
  return !!v || "此项为必填";
};

const onSubmit = async () => {
  if (!state.form) {
    return;
  }
  state.loading = true;
  const { succeeded } = await OAuthApi.addLink(state.link);

  state.loading = false;
  state.success = succeeded;
  if (succeeded) {
    toastStore.success("提交成功，请耐心等待审核");
    await authStore.getUserInfo();
  }
};
onMounted(async () => {
  if (!info.value) {
    const { data } = await OAuthApi.get();
    location.href = data!;
  }
  if (info.value?.status === 1) {
    toastStore.info("您申请的交换友链正在审核中");
  }
});
</script>

<style scoped lang="scss">
.info-title {
  font-size: 1.25rem;
  font-weight: bold;
}
.info-wrapper {
  margin-top: 1rem;
  display: flex;
  align-items: center;
  justify-content: center;
}
#pick-avatar {
  outline: none;
}
.binding {
  display: flex;
  align-items: center;
}
@media (max-width: 759px) {
  .info {
    text-align: center;
  }
  .info-wrapper {
    .avatar {
      text-align: center;
    }
  }
}
</style>
