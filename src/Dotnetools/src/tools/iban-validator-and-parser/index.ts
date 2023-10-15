import { defineTool } from '../tool';
import Bank from '~icons/mdi/bank';

export const tool = defineTool({
  name: 'IBAN 验证和解析',
  path: '/iban-validator-and-parser',
  description: '验证和解析 IBAN 号码。检查 IBAN 是否有效并获取国家/地区 BBAN，如果它是 QR-IBAN 和 IBAN 友好格式。-Validate and parse IBAN numbers. Check if IBAN is valid and get the country, BBAN, if it is a QR-IBAN and the IBAN friendly format.',
  keywords: ['iban', 'validator', 'and', 'parser', 'bic', 'bank'],
  component: () => import('./iban-validator-and-parser.vue'),
  icon: Bank,
  createdAt: new Date('2023-08-26'),
});
