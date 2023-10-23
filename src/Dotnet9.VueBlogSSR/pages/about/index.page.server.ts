import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import AppApi from "~/api/AppApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const { data } = await AppApi.info();
  return {
    pageContext: {
      pageProps: {
        info: data?.info ?? {},
      },
      meta: {
        title: "关于",
      },
    },
  };
};
export { onBeforeRender };
