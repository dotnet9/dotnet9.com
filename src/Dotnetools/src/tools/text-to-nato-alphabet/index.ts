import { Speakerphone } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '北约拼音转换器',
  path: '/text-to-nato-alphabet',
  description: '将文本转换为北约语音字母表进行口头传输。（Transform text into NATO phonetic alphabet for oral transmission.）',
  keywords: ['string', 'nato', 'alphabet', 'phonetic', 'oral', 'transmission'],
  component: () => import('./text-to-nato-alphabet.vue'),
  icon: Speakerphone,
});
