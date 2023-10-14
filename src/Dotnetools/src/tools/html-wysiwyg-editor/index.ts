import { Edit } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '网页所见即所得编辑器',
  path: '/html-wysiwyg-editor',
  description: '具有功能丰富的所见即所得编辑器的在线HTML编辑器，可立即获取内容的源代码。',
  keywords: ['html', 'wysiwyg', 'editor', 'p', 'ul', 'ol', 'converter', 'live'],
  component: () => import('./html-wysiwyg-editor.vue'),
  icon: Edit,
});
