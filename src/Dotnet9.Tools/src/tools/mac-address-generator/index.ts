import { Devices } from '@vicons/tabler';
import { defineTool } from '../tool';
export const tool = defineTool({
  name: 'MAC地址生成器',
  path: '/mac-address-generator',
  description: '输入数量和前缀，并以您选择的大小写（大写或小写）生成MAC地址-Enter the quantity and prefix. MAC addresses will be generated in your chosen case (uppercase or lowercase)',
  keywords: ['mac', 'address', 'generator', 'random', 'prefix'],
  component: () => import('./mac-address-generator.vue'),
  icon: Devices,
  createdAt: new Date('2023-11-31'),
});
