import { Tags } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '网页Meta生成器',
  path: '/og-meta-generator',
  description: '为您的网站生成开放图形和社交html元标签。',
  keywords: [
    'meta',
    'tag',
    'generator',
    'social',
    'title',
    'description',
    'image',
    'share',
    'online',
    'website',
    'open',
    'graph',
    'og',
  ],
  component: () => import('./meta-tag-generator.vue'),
  icon: Tags,
});
