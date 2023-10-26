import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import CoversApi from "~/api/CoversApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const { data } = await CoversApi.list({
    pageNo: Number(pageContext.urlParsed.search.page ?? 1),
    pageSize: 6,
  });
  return {
    pageContext: {
      pageProps: {
        covers: data?.rows ?? [],
        total: data?.total ?? 0,
        pages: data?.pages ?? 0,
        pageNo: data?.pageNo ?? 1,
      },
      meta: {
        title: "模块",
      },
    },
  };
};
export { onBeforeRender };
