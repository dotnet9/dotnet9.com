import { LetterX } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '罗马数字转换器',
  path: '/roman-numeral-converter',
  description: '将罗马数字转换为阿拉伯数字，或将阿拉伯数字转换为罗马数字。',
  keywords: ['roman', 'arabic', 'converter', 'X', 'I', 'V', 'L', 'C', 'D', 'M'],
  component: () => import('./roman-numeral-converter.vue'),
  icon: LetterX,
});
