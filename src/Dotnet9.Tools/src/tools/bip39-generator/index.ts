import { AlignJustified } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'BIP39助记词生成器',
  path: '/bip39-generator',
  description: '从现有或随机种子生成BIP39密码短语，或从密码短语中获取种子。',
  keywords: ['BIP39', 'passphrase', 'generator', 'mnemonic', 'entropy'],
  component: () => import('./bip39-generator.vue'),
  icon: AlignJustified,
});
