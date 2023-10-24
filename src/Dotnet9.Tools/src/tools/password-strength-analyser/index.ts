import { defineTool } from '../tool';
import PasswordIcon from '~icons/mdi/form-textbox-password';

export const tool = defineTool({
  name: '密码强度分析器',
  path: '/password-strength-analyser',
  description: '使用此仅限客户端的密码强度分析器和破解时间估计工具来发现您的密码强度。',
  keywords: ['password', 'strength', 'analyser', 'and', 'crack', 'time', 'estimation', 'brute', 'force', 'attack', 'entropy', 'cracking', 'hash', 'hashing', 'algorithm', 'algorithms', 'md5', 'sha1', 'sha256', 'sha512', 'bcrypt', 'scrypt', 'argon2', 'argon2id', 'argon2i', 'argon2d'],
  component: () => import('./password-strength-analyser.vue'),
  icon: PasswordIcon,
  createdAt: new Date('2023-06-24'),
});
