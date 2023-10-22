import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import AlbumsApi from "~/api/AlbumsApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const { data } = await AlbumsApi.list({
    pageNo: Number(pageContext.urlParsed.search.page ?? 1),
    pageSize: 6,
  });
  return {
    pageContext: {
      pageProps: {
        albums: data?.rows ?? [],
        total: data?.total ?? 0,
        pages: data?.pages ?? 0,
        pageNo: data?.pageNo ?? 1,
      },
      meta: {
        title: "相册",
      },
    },
  };
};
export { onBeforeRender };
