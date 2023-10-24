import { Hourglass } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '预计完成时间计算器-ETA calculator',
  path: '/eta-calculator',
  description:
    'ETA（预计到达时间）计算器，用于了解任务的大致结束时间，例如下载结束的时刻。（An ETA (Estimated Time of Arrival) calculator to know the approximate end time of a task, for example the moment of ending of a download.）',
  keywords: ['eta', 'calculator', 'estimated', 'time', 'arrival', 'average'],
  component: () => import('./eta-calculator.vue'),
  icon: Hourglass,
});
