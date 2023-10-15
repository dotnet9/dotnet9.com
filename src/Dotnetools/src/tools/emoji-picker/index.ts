import { MoodSmile } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '表情符号选择器-Emoji picker',
  path: '/emoji-picker',
  description: '轻松复制和粘贴表情符号，并获取每个表情符号的 unicode 和码位值。-Copy and paste emojis easily and get the unicode and code points value of each emoji.',
  keywords: ['emoji', 'picker', 'unicode', 'copy', 'paste'],
  component: () => import('./emoji-picker.vue'),
  icon: MoodSmile,
  createdAt: new Date('2023-08-07'),
});
