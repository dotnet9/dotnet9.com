import { PageContextBuiltIn } from "vite-plugin-ssr/types";
import ArticleApi from "~/api/ArticleApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const { data } = await ArticleApi.list({
    pageNo: Number(pageContext.urlParsed.search.page ?? 1),
    pageSize: 10,
  });
  return {
    pageContext: {
      pageProps: {
        articles: data?.rows ?? [],
        pages: data?.pages ?? 1,
        total: data?.total ?? 0,
        pageNo: data?.pageNo ?? 1,
      },
      meta: {
        title: "归档",
      },
    },
  };
};

export { onBeforeRender };
