import { DeviceDesktop } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '设备信息',
  path: '/device-information',
  description: '获取有关当前设备的信息（屏幕大小、像素比例、用户代理等）',
  keywords: [
    'device',
    'information',
    'screen',
    'pixel',
    'ratio',
    'status',
    'data',
    'computer',
    'size',
    'user',
    'agent',
  ],
  component: () => import('./device-information.vue'),
  icon: DeviceDesktop,
});
