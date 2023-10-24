import { Speakerphone } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '北约字母转换器',
  path: '/text-to-nato-alphabet',
  description: '将文本转换为北约音标以进行口头传播。（Transform text into NATO phonetic alphabet for oral transmission.）',
  keywords: ['string', 'nato', 'alphabet', 'phonetic', 'oral', 'transmission'],
  component: () => import('./text-to-nato-alphabet.vue'),
  icon: Speakerphone,
});
