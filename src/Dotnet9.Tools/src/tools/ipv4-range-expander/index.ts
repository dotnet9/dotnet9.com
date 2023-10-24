import { UnfoldMoreOutlined } from '@vicons/material';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'IPv4 范围扩展器',
  path: '/ipv4-range-expander',
  description:
    '给定开始和结束 IPv4 地址，此工具使用其网段（CIDR） 表示法计算有效的 IPv4 网络。',
  keywords: ['ipv4', 'range', 'expander', 'subnet', 'creator', 'cidr'],
  component: () => import('./ipv4-range-expander.vue'),
  icon: UnfoldMoreOutlined,
  createdAt: new Date('2023-04-19'),
});
