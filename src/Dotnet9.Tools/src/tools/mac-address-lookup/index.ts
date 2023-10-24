import { Devices } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'MAC 地址查找',
  path: '/mac-address-lookup',
  description: '通过 MAC 地址查找设备的供应商和制造商。',
  keywords: ['mac', 'address', 'lookup', 'vendor', 'parser', 'manufacturer'],
  component: () => import('./mac-address-lookup.vue'),
  icon: Devices,
  createdAt: new Date('2023-04-06'),
});
