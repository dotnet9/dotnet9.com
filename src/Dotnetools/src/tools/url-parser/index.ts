import { Unlink } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'Url分析器',
  path: '/url-parser',
  description:
    '解析url字符串以获得所有不同的部分（协议、来源、参数、端口、用户名密码…）',
  keywords: ['url', 'parser', 'protocol', 'origin', 'params', 'port', 'username', 'password', 'href'],
  component: () => import('./url-parser.vue'),
  icon: Unlink,
});
