import type { ArticleListQueryInput } from "./../../api/models/article-list-query-input";
import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import ArticleApi from "~/api/ArticleApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const { page } = pageContext.urlParsed.search;
  const { data, extras } = await ArticleApi.list({
    pageNo: Number(page ?? 1),
    categoryId: Number(pageContext.routeParams.id),
  } as ArticleListQueryInput);
  return {
    pageContext: {
      pageProps: {
        articles: data?.rows ?? [],
        pages: data?.pages ?? 0,
        pageNo: data?.pageNo ?? 1,
        name: extras.name,
        cover: extras.cover,
      },
      meta: {
        title: `分类.${extras.name}`,
      },
    },
  };
};

export { onBeforeRender };
