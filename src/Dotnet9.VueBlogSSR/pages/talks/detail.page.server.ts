import type { PageContextBuiltIn } from "vite-plugin-ssr/types";
import TalksApi from "~/api/TalksApi";

const onBeforeRender = async (pageContext: PageContextBuiltIn) => {
  const id = Number(pageContext.routeParams.id);
  const { data } = await TalksApi.talkDetail(id);
  return {
    pageContext: {
      pageProps: {
        talk: data,
        id,
      },
      meta: {
        title: "说说-详情",
      },
    },
  };
};
export { onBeforeRender };
