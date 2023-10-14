import type { OGSchemaType } from '../OGSchemaType.type';

export const twitter: OGSchemaType = {
  name: '推特',
  elements: [
    {
      type: 'select',
      options: [
        { label: '总结', value: 'summary' },
        { label: '大图像摘要', value: 'summary_large_image' },
        { label: '应用', value: 'app' },
        { label: 'Player', value: 'player' },
      ],
      label: 'Card类型',
      placeholder: 'The Twitter card type...',
      key: 'twitter:card',
    },
    {
      type: 'input',
      label: '站点账户',
      placeholder: '该网站的推特账户的名称 (例如: @dotnetoolscom)...',
      key: 'twitter:site',
    },
    {
      type: 'input',
      label: '创作者账户',
      placeholder: '创作者的推特账号名称 (例如: @dotnetools)...',
      key: 'twitter:creator',
    },
  ],
};
