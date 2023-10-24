import { LetterCaseToggle } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '变量命名转换器',
  path: '/case-converter',
  description: '更改字符串的大小写并在不同格式之间进行选择',
  keywords: [
    'case',
    'converter',
    'camelCase',
    'capitalCase',
    'constantCase',
    'dotCase',
    'headerCase',
    'noCase',
    'paramCase',
    'pascalCase',
    'pathCase',
    'sentenceCase',
    'snakeCase',
  ],
  component: () => import('./case-converter.vue'),
  icon: LetterCaseToggle,
});
