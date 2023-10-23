import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import ArticleApi from "~/api/ArticleApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const { data } = await ArticleApi.tags();
  return {
    pageContext: {
      pageProps: {
        tags: data ?? [],
      },
      meta: {
        title: "标签",
      },
    },
  };
};

export { onBeforeRender };
