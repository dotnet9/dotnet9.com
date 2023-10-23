import { PageContextBuiltIn } from "vite-plugin-ssr/types";
import ArticleApi from "~/api/ArticleApi";
import TalksApi from "~/api/TalksApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const { page } = pageContext.urlParsed.search;
  const [talks, articles] = await Promise.all([
    TalksApi.list({ pageNo: 1, pageSize: 10 }),
    ArticleApi.list({
      pageNo: Number(page ?? 1),
      pageSize: 10,
    }),
  ]);
  return {
    pageContext: {
      pageProps: {
        talks: talks.data?.rows ?? [],
        articles: articles.data?.rows ?? [],
        pages: articles.data?.pages ?? 0,
        pageNo: articles.data?.pageNo ?? 1,
      },
      meta: {
        title: "首页",
      },
    },
  };
};
export { onBeforeRender };
