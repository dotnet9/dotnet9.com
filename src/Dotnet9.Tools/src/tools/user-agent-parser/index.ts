import { Browser } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '用户代理（User-agent）解析器',
  path: '/user-agent-parser',
  description: '从用户代理字符串中检测和解析浏览器、引擎、操作系统、CPU 和设备类型/型号。',
  keywords: ['user', 'agent', 'parser', 'browser', 'engine', 'os', 'cpu', 'device', 'user-agent', 'client'],
  component: () => import('./user-agent-parser.vue'),
  icon: Browser,
  createdAt: new Date('2023-04-06'),
});
