import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import AppApi from "~/api/AppApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const { data } = await AppApi.links();
  return {
    pageContext: {
      pageProps: {
        links: data ?? [],
      },
      meta: {
        title: "友情链接",
      },
    },
  };
};
export { onBeforeRender };
