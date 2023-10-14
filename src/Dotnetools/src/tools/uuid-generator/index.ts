import { Fingerprint } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'UUID v4 生成器',
  path: '/uuid-generator',
  description:
    'UUID是通用唯一标识符（Universally Unique Identifier）的缩写，是一个128位的标识符。UUID的目的是在分布式系统中，为了避免冲突而设计的一种标识符。UUID的生成算法使用了标准的格式和基于硬件地址、时间戳、随机数等元素。',
  keywords: ['uuid', 'v4', 'random', 'id', 'alphanumeric', 'identity', 'token', 'string', 'identifier', 'unique'],
  component: () => import('./uuid-generator.vue'),
  icon: Fingerprint,
});
