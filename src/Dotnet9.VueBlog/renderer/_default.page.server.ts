export { render };
// See https://vite-plugin-ssr.com/data-fetching
export const passToClient = [
  "initialStoreState",
  "pageProps",
  "routeParams",
  "meta",
  "urlOriginal",
  "render",
  // "urlPathname",
];

import { renderToString as renderToString_ } from "@vue/server-renderer";
import type { App } from "vue";
import { escapeInject, dangerouslySkipEscape } from "vite-plugin-ssr/server";
import { createApp } from "./app";
import type { PageContextServer } from "./types";
import AppApi from "~/api/AppApi";

async function render(pageContext: PageContextServer) {
  const { Page, meta } = pageContext;
  // This render() hook only supports SSR, see https://vite-plugin-ssr.com/render-modes for how to modify render() to support SPA
  if (!Page)
    throw new Error("My render() hook expects pageContext.Page to be defined");
  const { app, store } = createApp(pageContext);
  const appHtml = await renderToString(app);
  // See https://vite-plugin-ssr.com/head
  let title = meta?.title,
    keywords = meta?.keywords,
    desc = meta?.description,
    logo = "/favicon.ico";
  if (meta === null || !title || !desc || !keywords) {
    const { data } = await AppApi.info();
    title = (meta?.title ?? "oops") + "-" + data?.site?.siteName!;
    desc = meta?.description || data?.site?.description!;
    keywords = meta?.keywords || data?.site?.keyword!;
    logo = data?.site?.logo || logo;
  }

  const documentHtml = escapeInject`<!DOCTYPE html>
    <html lang="en">
      <head>
        <meta charset="UTF-8" />
        <link rel="icon" href="${logo}" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <meta name="description" content="${desc}" />
        <meta name="keywords" content="${keywords}" />
        <title>${title}</title>
      </head>
      <body>
        <div id="app">${dangerouslySkipEscape(appHtml)}</div>
      </body>
    </html>`;
  const initialStoreState = store.state.value;
  return {
    documentHtml,
    pageContext: {
      initialStoreState,
      // We can add some `pageContext` here, which is useful if we want to do page redirection https://vite-plugin-ssr.com/page-redirection
    },
  };
}

async function renderToString(app: App) {
  let err: unknown;
  // Workaround: renderToString_() swallows errors in production, see https://github.com/vuejs/core/issues/7876
  app.config.errorHandler = (err_) => {
    err = err_;
  };
  const appHtml = await renderToString_(app);
  if (err) throw err;
  return appHtml;
}
