import { Lock } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '文本加密、解密',
  path: '/encryption',
  description: '使用AES、TripleDES、Rabbit或RC4等加密算法对文本明文进行加密和解密。',
  keywords: ['cypher', 'encipher', 'text', 'AES', 'TripleDES', 'Rabbit', 'RC4'],
  component: () => import('./encryption.vue'),
  icon: Lock,
  redirectFrom: ['/cypher'],
});
