import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import AlbumsApi from "~/api/AlbumsApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const { data, extras } = await AlbumsApi.pictures({
    pageNo: Number(pageContext.urlParsed.search.pageNo ?? 1),
    pageSize: 1000,
    albumId: Number(pageContext.routeParams.id),
  });
  return {
    pageContext: {
      pageProps: {
        photos: data?.rows ?? [],
        total: data?.total ?? 0,
        pages: data?.pages ?? 0,
        pageNo: data?.pageNo ?? 1,
        cover: extras?.cover ?? "",
        name: extras?.name ?? "",
      },
      meta: {
        title: "图片",
      },
    },
  };
};
export { onBeforeRender };
