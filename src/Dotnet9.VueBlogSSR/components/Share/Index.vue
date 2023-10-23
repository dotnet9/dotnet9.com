<template>
  <div class="share-component social-share">
    <a
      v-for="item in urls"
      :key="item"
      :class="'social-share-icon icon-' + item"
      :target="item !== ShareType.wechat ? '_blank' : ''"
      :tabindex="item === ShareType.wechat ? -1 : 0"
      :href="formatUrl(item.toString())"
    >
      <div class="wechat-qrcode" v-if="item === ShareType.wechat">
        <h4>{{ wechatQrcodeTitle }}</h4>
        <canvas class="qrcode" id="qrcode"></canvas>
        <div class="help" v-html="wechatQrcodeHelper"></div>
      </div>
    </a>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed, nextTick, watch } from "vue";
import QrCode from "qrcode";
import "~/assets/css/share.scss";
import { ShareType } from "./ShareType";
interface ShareOption {
  url?: string;
  origin?: string;
  source?: string;
  title?: string;
  description?: string;
  image?: string;
  imageSelector?: string;
  weiboKey?: string;
  wechatQrcodeTitle?: string;
  wechatQrcodeHelper?: string;
  wechatQrcodeSize?: number;
  sites?: ShareType[];
  mobileSites?: ShareType[];
  disabled?: ShareType[];
}

const props = withDefaults(defineProps<ShareOption>(), {
  url: location.href,
  origin: location.origin,
  image: (document.images[0] || 0).src || "",
  weiboKey: "",
  wechatQrcodeTitle: "微信扫一扫：分享",
  wechatQrcodeHelper:
    "<p>微信里点“发现”，扫一下</p><p>二维码便可将本文分享至朋友圈。</p>",
  wechatQrcodeSize: 100,
  sites: () => [
    ShareType.weibo,
    ShareType.qq,
    ShareType.wechat,
    ShareType.douban,
    ShareType.qzone,
    ShareType.linkedin,
    ShareType.facebook,
    ShareType.twitter,
    ShareType.google,
  ],
  mobileSites: () => [ShareType.qq, ShareType.wechat],
});
const isMobile = ref<boolean>(false);
const urls = computed(() => {
  if (props.disabled?.length ?? 0 > 0) {
    return isMobile.value
      ? props.mobileSites.filter((item) => !props.disabled!.includes(item))
      : props.sites.filter((item) => !props.disabled!.includes(item));
  }
  return isMobile.value ? props.mobileSites : props.sites;
});

onMounted(() => {
  isMobile.value = document.body.clientWidth <= 768;
  //监听页面宽度变化
  window.onresize = () => {
    return (() => {
      isMobile.value = document.body.clientWidth <= 768;
    })();
  };
  nextTick(() => {
    const el = document.getElementById("qrcode");
    QrCode.toCanvas(el, props.url, {
      width: 100,
    });
  });
});

//监听页面宽度变化
watch(isMobile, (n) => {});

function getMetaContentByName(name: string): string {
  const h = document.getElementsByName(name)[0];
  if (h) {
    return (h as HTMLMetaElement).content;
  }
  return "";
}

/**
 *格式化URL
 * @param name 属性名
 */
function formatUrl(name: string): string {
  let templates: { [key: string]: string } = {
    qzone:
      "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?url={{URL}}&title={{TITLE}}&desc={{DESCRIPTION}}&summary={{SUMMARY}}&site={{SOURCE}}&pics={{IMAGE}}",
    qq: 'http://connect.qq.com/widget/shareqq/index.html?url={{URL}}&title={{TITLE}}&source={{SOURCE}}&desc={{DESCRIPTION}}&pics={{IMAGE}}&summary="{{SUMMARY}}"',
    weibo:
      "https://service.weibo.com/share/share.php?url={{URL}}&title={{TITLE}}&pic={{IMAGE}}&appkey={{WEIBOKEY}}",
    wechat: "javascript:",
    douban:
      "http://shuo.douban.com/!service/share?href={{URL}}&name={{TITLE}}&text={{DESCRIPTION}}&image={{IMAGE}}&starid=0&aid=0&style=11",
    linkedin:
      "http://www.linkedin.com/shareArticle?mini=true&ro=true&title={{TITLE}}&url={{URL}}&summary={{SUMMARY}}&source={{SOURCE}}&armin=armin",
    facebook: "https://www.facebook.com/sharer/sharer.php?u={{URL}}",
    twitter:
      "https://twitter.com/intent/tweet?text={{TITLE}}&url={{URL}}&via={{ORIGIN}}",
    google: "https://plus.google.com/share?url={{URL}}",
  };
  let data = { ...props } as { [key: string]: any };
  data.title =
    getMetaContentByName("title") ||
    getMetaContentByName("Title") ||
    document.title;
  data.description =
    getMetaContentByName("description") ||
    getMetaContentByName("Description") ||
    "";
  if (data.imageSelector) {
    data.image = Array.from(document.querySelectorAll(data.imageSelector))
      .map(function (item) {
        return item.src;
      })
      .join("||");
  } else {
    data.image =
      (
        Array.from(document.body.querySelectorAll("img")).filter(
          (item) => (item as HTMLImageElement).src
        )[0] || 0
      ).src || "";
  }
  return templates[name].replace(/\{\{(\w)(\w*)\}\}/g, function (m, fix, key) {
    var nameKey = name + fix + key.toLowerCase();
    key = (fix + key).toLowerCase();
    return encodeURIComponent(
      (data[nameKey] === undefined ? data[key] : data[nameKey]) || ""
    );
  });
}
</script>

<!-- <style lang="sass" src="../../assets/css/share.scss" scoped></style> -->
