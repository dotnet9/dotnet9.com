import { AlignJustified } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '段落生成器-Lorem ipsum generator',
  path: '/lorem-ipsum-generator',
  description:
    'Lorem ipsum 是一种占位符文本，通常用于演示文档或字体的视觉形式，而不依赖于有意义的内容-Lorem ipsum is a placeholder text commonly used to demonstrate the visual form of a document or a typeface without relying on meaningful content',
  keywords: ['lorem', 'ipsum', 'dolor', 'sit', 'amet', 'placeholder', 'text', 'filler', 'random', 'generator'],
  component: () => import('./lorem-ipsum-generator.vue'),
  icon: AlignJustified,
});
