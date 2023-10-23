import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import ArticleApi from "~/api/ArticleApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const [c, r] = await Promise.all([
    ArticleApi.categories(),
    ArticleApi.report(),
  ]);
  return {
    pageContext: {
      pageProps: {
        categories: c.data ?? [],
        total: r.data?.categoryCount ?? 0,
      },
      meta: {
        title: "分类",
      },
    },
  };
};

export { onBeforeRender };
