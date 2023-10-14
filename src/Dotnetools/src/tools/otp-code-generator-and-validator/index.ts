import { DeviceMobile } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '一次性密码生成器',
  path: '/otp-generator',
  description: '生成并验证用于多重身份验证的基于时间的 OTP（一次性密码）。',
  keywords: [
    'otp',
    'code',
    'generator',
    'validator',
    'one',
    'time',
    'password',
    'authentication',
    'MFA',
    'mobile',
    'device',
    'security',
    'TOTP',
    'Time',
    'HMAC',
  ],
  component: () => import('./otp-code-generator-and-validator.vue'),
  icon: DeviceMobile,
});
