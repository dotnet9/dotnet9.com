module.exports = (ctx) => ({
    parser: ctx.parser ? "sugarss" : false, // 解析器
    map: ctx.env === "development" ? ctx.map : false, // map文件
    // 配置插件
    plugins: {
      // 允许你使用未来的CSS特性
      "postcss-preset-env": {
        stage: 2,
        browsers: "last 2 versions",
      },
      // 允许使用 import
      "postcss-import": {},
      // css 嵌套
      "postcss-nested": {},
      // 一个模块化的 CSS 压缩器
      cssnano: ctx.env === "production" ? {} : false,
    },
  });
  