import { Binary } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'IPv4 地址转换器',
  path: '/ipv4-address-converter',
  description: '将 IP 地址转换为十进制、二进制、十六进制或ipv6中的事件-Convert an ip address into decimal, binary, hexadecimal or event in ipv6',
  keywords: ['ipv4', 'address', 'converter', 'decimal', 'hexadecimal', 'binary', 'ipv6'],
  component: () => import('./ipv4-address-converter.vue'),
  icon: Binary,
  createdAt: new Date('2023-04-08'),
});
