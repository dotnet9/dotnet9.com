import { PasswordRound } from '@vicons/material';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '基本身份验证生成器',
  path: '/basic-auth-generator',
  description: '根据用户名和密码生成base64请求头',
  keywords: [
    'basic',
    'auth',
    'generator',
    'username',
    'password',
    'base64',
    'authentication',
    'header',
    'authorization',
  ],
  component: () => import('./basic-auth-generator.vue'),
  icon: PasswordRound,
});
