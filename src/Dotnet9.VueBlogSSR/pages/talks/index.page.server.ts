import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import TalksApi from "~/api/TalksApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const { data } = await TalksApi.list({
    pageNo: Number(pageContext.urlParsed.search.page ?? 1),
    pageSize: 10,
  });
  return {
    pageContext: {
      pageProps: {
        talks: data?.rows ?? [],
        pages: data?.pages ?? 0,
        pageNo: data?.pageNo ?? 1,
      },
      meta: {
        title: "说说",
      },
    },
  };
};
export { onBeforeRender };
