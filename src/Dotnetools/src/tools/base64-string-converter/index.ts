import { FileDigit } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'Base64字符串编码/解码',
  path: '/base64-string-converter',
  description: '简单将字符串编码和解码为base64表示。',
  keywords: ['base64', 'converter', 'conversion', 'web', 'data', 'format', 'atob', 'btoa'],
  component: () => import('./base64-string-converter.vue'),
  icon: FileDigit,
  redirectFrom: ['/file-to-base64', '/base64-converter'],
});
