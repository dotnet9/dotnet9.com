import { defineStore } from "pinia";
import { reactive, computed } from "vue";
import AppApi from "@/api/AppApi";
import type {
  ArticleReportOutput,
  BlogSetting,
  BloggerInfo,
} from "@/api/models";
import { randomNumber } from "@/utils";
import ArticleApi from "@/api/ArticleApi";

export const useApp = defineStore("app", () => {
  const app = reactive({
    covers: {
      home: [] as string[],
      about: [] as string[],
      archives: [] as string[],
      category: [] as string[],
      tag: [] as string[],
      cover: [] as string[],
      talk: [] as string[],
      message: [] as string[],
      user: [] as string[],
      link: [] as string[],
      tagList: [] as string[],
      categories: [] as string[],
    },
    info: {} as BloggerInfo,
    isInit: false,
    blogSetting: {} as BlogSetting,
    report: {
      articleCount: 0,
      tagCount: 0,
      categoryCount: 0,
    } as ArticleReportOutput,
  });

  /**
   * 初始化博客基本信息
   */
  const init = async () => {
    if (app.isInit) {
      return;
    }
    app.isInit = true;
    const { data } = await AppApi.info();
    const covers = data!.covers!;
    app.info = data!.info ?? {
      nikeName: "Dotnet9",
      motto: "时间如流水，只能流去不流回",
      qq: "632871194",
      avatar: "/logo.png",
    };
    app.blogSetting = data!.site ?? {
      siteName: "Dotnet9",
      motto: "时间如流水，只能流去不流回",
      isAllowComments: true,
      isAllowMessage: true,
      runTime: new Date("2019/11/12"),
      copyright: "©2023 By 沙漠尽头的狼",
      description: "一个专注互联网分享精神的博客网站",
      filing: "蜀ICP备19038256号-1",
      favicon: "favicon.ico",
      keyword: "Dotnet9",
      visitorNumbers: 0,
    };
    app.covers.home = covers.home ?? ["/cover/logo.png"];
    app.covers.about = covers.about ?? ["/cover/about.jpg"];
    app.covers.archives = covers.archives ?? ["/cover/archives.jpg"];
    app.covers.category = covers.category ?? ["/cover/category.jpg"];
    app.covers.tag = covers.tag ?? ["/cover/tag.png"];
    app.covers.cover = covers.cover ?? ["/cover/cover.jpg"];
    app.covers.talk = covers.talk ?? ["/cover/talk.jpg"];
    app.covers.message = covers.message ?? ["/cover/message.png"];
    app.covers.user = covers.user ?? ["/cover/user.jpg"];
    app.covers.link = covers.link ?? ["/cover/logo.png"];
    app.covers.tagList = covers.tagList ?? ["/cover/logo.png"];
    app.covers.categories = covers.categories ?? ["/cover/logo.png"];
  };
  /**
   * 首页cover
   */
  const homeCover = () => {
    const arr = app.covers.home;
    return arr[randomNumber(0, arr.length - 1)];
  };
  /**
   * 关于
   */
  const aboutCover = () => {
    const arr = app.covers.about;
    return arr[randomNumber(0, arr.length - 1)];
  };
  /**
   * 归档
   */
  const archivesCover = () => {
    const arr = app.covers.archives;
    return arr[randomNumber(0, arr.length - 1)];
  };
  /**
   * 分类
   */
  const categoryCover = () => {
    const arr = app.covers.category;
    return arr[randomNumber(0, arr.length - 1)];
  };
  /**
   * 标签
   */
  const tagCover = () => {
    const arr = app.covers.tag;
    return arr[randomNumber(0, arr.length - 1)];
  };
  /**
   * 模块封面
   */
  const coverCover = () => {
    const arr = app.covers.cover;
    return arr[randomNumber(0, arr.length - 1)];
  };
  /**
   * 说说
   * @returns
   */
  const talkCover = () => {
    const arr = app.covers.talk;
    return arr[randomNumber(0, arr.length - 1)];
  };
  /**
   * 留言
   */
  const messageCover = () => {
    const arr = app.covers.message;
    return arr[randomNumber(0, arr.length - 1)];
  };
  /**
   * 个人中心
   */
  const userCover = () => {
    const arr = app.covers.user;
    return arr[randomNumber(0, arr.length - 1)];
  };
  /**
   * 友链
   */
  const linkCover = () => {
    const arr = app.covers.link;
    return arr[randomNumber(0, arr.length - 1)];
  };
  /**
   * 标签列表封面
   * @returns
   */
  const tagListCover = () => {
    const arr = app.covers.tagList;
    return arr[randomNumber(0, arr.length - 1)];
  };
  /**
   * 分类列表封面
   */
  const categoriesCover = () => {
    const arr = app.covers.categories;
    return arr[randomNumber(0, arr.length - 1)];
  };

  /**
   * 是否已初始化
   */
  const isInit = computed(() => {
    return app.isInit;
  });

  const info = computed(() => {
    return app.info;
  });

  const blogSetting = computed(() => {
    return app.blogSetting;
  });

  const report = computed(() => {
    return app.report;
  });

  const getSiteReport = async () => {
    const { data } = await ArticleApi.report();
    if (data) {
      app.report = data;
    }
  };

  return {
    init,
    homeCover,
    aboutCover,
    archivesCover,
    categoryCover,
    tagCover,
    coverCover,
    talkCover,
    messageCover,
    userCover,
    linkCover,
    tagListCover,
    categoriesCover,
    getSiteReport,
    isInit,
    info,
    blogSetting,
    report,
  };
});
