import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import CommentApi from "~/api/CommentApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const { data } = await CommentApi.list({
    pageNo: 1,
    pageSize: 1000,
  });
  return {
    pageContext: {
      pageProps: {
        items: data?.rows ?? [],
        pageNo: data?.pageNo ?? 1,
        pages: data?.pages ?? 0,
      },
      meta: {
        title: "留言",
      },
    },
  };
};
export { onBeforeRender };
