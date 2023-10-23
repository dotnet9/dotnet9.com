import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import ArticleApi from "~/api/ArticleApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const [first, last] = await Promise.all([
    ArticleApi.info(Number(pageContext.routeParams.id)),
    ArticleApi.latest(),
  ]);
  const info = first.data;
  return {
    pageContext: {
      pageProps: {
        info: info ?? {},
        latest: last.data ?? [],
      },
      meta: {
        title: info?.title,
        description: info?.summary,
        keywords: [
          info?.title,
          ...(info?.tags?.map((t) => t.name) ?? []),
          info?.author,
          info?.categoryName,
        ].join(","),
      },
    },
  };
};

export { onBeforeRender };
