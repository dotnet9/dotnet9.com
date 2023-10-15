import { BrandGit } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'Git备忘录',
  path: '/git-memo',
  description:
    'Git 是一个去中心化的版本管理软件。使用此备忘单，您将可以快速访问最常见的 git 命令。',
  keywords: ['git', 'push', 'force', 'pull', 'commit', 'amend', 'rebase', 'merge', 'reset', 'soft', 'hard', 'lease'],
  component: () => import('./git-memo.vue'),
  icon: BrandGit,
});
