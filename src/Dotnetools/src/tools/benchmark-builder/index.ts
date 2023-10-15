import { SpeedFilled } from '@vicons/material';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '基准生成器-Benchmark builder',
  path: '/benchmark-builder',
  description: '使用这个非常简单的在线基准测试构建器轻松比较任务的执行时间。-Easily compare execution time of tasks with this very simple online benchmark builder.',
  keywords: ['benchmark', 'builder', 'execution', 'duration', 'mean', 'variance'],
  component: () => import('./benchmark-builder.vue'),
  icon: SpeedFilled,
  createdAt: new Date('2023-04-05'),
});
