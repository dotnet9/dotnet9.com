import { defineTool } from '../tool';
import n7mIcon from './n7m-icon.svg?component';

export const tool = defineTool({
  name: '数字生成器',
  path: '/numeronym-generator',
  description: '数字是一个单词，其中一个数字用来形成缩写。例如，“i18n”是“国际化”的数字，其中18代表单词中第一个i和最后一个n之间的字母数。-A numeronym is a word where a number is used to form an abbreviation. For example, "i18n" is a numeronym of "internationalization" where 18 stands for the number of letters between the first i and the last n in the word.',
  keywords: ['numeronym', 'generator', 'abbreviation', 'i18n', 'a11y', 'l10n'],
  component: () => import('./numeronym-generator.vue'),
  icon: n7mIcon,
  createdAt: new Date('2023-11-05'),
});
