import { Keyboard } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '键代码信息-Keycode info',
  path: '/keycode-info',
  description: '查找任何按下的键的javascript keycode, code, location and modifiers。',
  keywords: [
    'keycode',
    'info',
    'code',
    'javascript',
    'event',
    'keycodes',
    'which',
    'keyboard',
    'press',
    'modifier',
    'alt',
    'ctrl',
    'meta',
    'shift',
  ],
  component: () => import('./keycode-info.vue'),
  icon: Keyboard,
});
