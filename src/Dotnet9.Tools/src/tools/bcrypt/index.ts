import { LockSquare } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'Bcrypt',
  path: '/bcrypt',
  description:
    'bcrypt算法是一种密码哈希算法，它是基于Blowfish加密算法改进的，能够生成安全性很高的哈希值，并且可以通过调整计算时间来提高安全性。',
  keywords: ['bcrypt', 'hash', 'compare', 'password', 'salt', 'round', 'storage', 'crypto'],
  component: () => import('./bcrypt.vue'),
  icon: LockSquare,
});
