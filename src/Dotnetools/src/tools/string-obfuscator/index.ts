import { EyeOff } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '字符串混淆器',
  path: '/string-obfuscator',
  description: '对字符串（如机密、IBAN 或令牌）进行模糊处理，使其可共享和可识别，而不会泄露其内容。-Obfuscate a string (like a secret, an IBAN, or a token) to make it shareable and identifiable without revealing its content.',
  keywords: ['string', 'obfuscator', 'secret', 'token', 'hide', 'obscure', 'mask', 'masking'],
  component: () => import('./string-obfuscator.vue'),
  icon: EyeOff,
  createdAt: new Date('2023-08-16'),
});
