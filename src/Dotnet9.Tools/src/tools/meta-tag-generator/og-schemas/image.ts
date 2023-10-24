import type { OGSchemaType } from '../OGSchemaType.type';

export const image: OGSchemaType = {
  name: '图像',
  elements: [
    {
      type: 'input',
      label: '图片网址',
      placeholder: '您网站社交图片的网址...',
      key: 'image',
    },
    {
      type: 'input',
      label: '图片替换文字',
      placeholder: '您网站社交图片的替代文本...',
      key: 'image:alt',
    },
    {
      type: 'input',
      label: '宽度',
      placeholder: '您网站社交图片的像素宽度...',
      key: 'image:width',
    },
    {
      type: 'input',
      label: '高度',
      placeholder: '您网站社交图片的像素高度...',
      key: 'image:height',
    },
  ],
};
