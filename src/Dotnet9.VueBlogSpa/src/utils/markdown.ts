import MarkdownIt from "markdown-it";
import hljs from "highlight.js";
import { generateUUID } from "./index";
export default function markdownToHtml(content: string): string {
  const md = new MarkdownIt({
    html: true,
    linkify: true,
    typographer: true,
    breaks: true,
    highlight(str: string, lang: string = "C#"): string {
      // 生成唯一的id标识
      const codeIndex = generateUUID();
      // 复制功能主要使用的是 clipboard.js
      let html: string = `<button class="copy-btn iconfont iconfuzhi" type="button" data-clipboard-action="copy" data-clipboard-target="#copy${codeIndex}"></button>`;
      const linesLength: number = str.split(/\n/).length - 1;
      // 生成行号
      let linesNum: string =
        '<span aria-hidden="true" class="line-numbers-rows">';
      for (let index = 0; index < linesLength; index++) {
        linesNum = linesNum + "<span></span>";
      }
      linesNum += "</span>";
      if (lang && hljs.getLanguage(lang)) {
        // highlight.js 高亮代码
        const preCode = hljs.highlight(lang, str, true).value;
        html = html + preCode;
        if (linesLength) {
          html += '<b class="name">' + lang + "</b>";
        }
        // 将代码包裹在 textarea 中，由于防止textarea渲染出现问题，这里将 "<" 用 "<" 代替，不影响复制功能
        return `<pre class="hljs"><code>${html}</code>${linesNum}</pre><textarea style="position: absolute;top: -9999px;left: -9999px;z-index: -9999;" id="copy${codeIndex}">${str.replace(
          /<\/textarea>/g,
          "</textarea>"
        )}</textarea>`;
      }
      return content;
    },
  })
    .use(() => require("markdown-it-sub"))
    .use(require("markdown-it-sup"))
    .use(require("markdown-it-mark"))
    .use(require("markdown-it-abbr"))
    .use(require("markdown-it-container"))
    .use(require("markdown-it-deflist"))
    .use(require("markdown-it-emoji"))
    .use(require("markdown-it-footnote"))
    .use(require("markdown-it-ins"))
    .use(require("markdown-it-katex-external"))
    .use(require("markdown-it-task-lists"));
  // 将markdown替换为html标签
  let s = md.render(content);
  if (s.length > 0) {
    s.match(/<h\d>.*?<\/h\d>/g)?.forEach((item, index) => {
      s = s.replace(item, item.replace(">", ` id="h_index_${index}">`));
    });
  }
  return s;
}
