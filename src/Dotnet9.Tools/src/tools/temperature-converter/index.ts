import { Temperature } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '温度转换器',
  path: '/temperature-converter',
  description:
    '开尔文、摄氏度、华氏度、兰氏度、德莱尔、牛顿温标、列氏度和罗默的温度度转换。-Temperature degrees conversions for Kelvin, Celsius, Fahrenheit, Rankine, Delisle, Newton, Réaumur and Rømer.',
  keywords: [
    'temperature',
    'converter',
    'degree',
    'Kelvin',
    'Celsius',
    'Fahrenheit',
    'Rankine',
    'Delisle',
    'Newton',
    'Réaumur',
    'Rømer',
  ],
  component: () => import('./temperature-converter.vue'),
  icon: Temperature,
});
