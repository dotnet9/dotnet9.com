import { TimerOutlined } from '@vicons/material';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '计时器-Chronometer',
  path: '/chronometer',
  description: '监控事物的持续时间。基本上是一个具有简单天文台功能的天文台表。-Monitor the duration of a thing. Basically a chronometer with simple chronometer features.',
  keywords: ['chronometer', 'time', 'lap', 'duration', 'measure', 'pause', 'resume', 'stopwatch'],
  component: () => import('./chronometer.vue'),
  icon: TimerOutlined,
});
