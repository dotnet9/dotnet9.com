import { RouterOutlined } from '@vicons/material';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'IPv4 子网计算器',
  path: '/ipv4-subnet-calculator',
  description: '解析您的 IPv4 CIDR 块并获取您需要的有关子网的所有信息。',
  keywords: ['ipv4', 'subnet', 'calculator', 'mask', 'network', 'cidr', 'netmask', 'bitmask', 'broadcast', 'address'],
  component: () => import('./ipv4-subnet-calculator.vue'),
  icon: RouterOutlined,
});
