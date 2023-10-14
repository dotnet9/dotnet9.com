import type { OGSchemaType } from '../OGSchemaType.type';

export const article: OGSchemaType = {
  name: '文章',
  elements: [
    {
      type: 'input',
      label: '出版日期',
      key: 'article:published_time',
      placeholder: '当文章首次发表时...',
    },
    {
      type: 'input',
      label: '修改日期',
      key: 'article:modified_time',
      placeholder: '文章上次更改时...',
    },
    {
      type: 'input',
      label: '有效期',
      key: 'article:expiration_time',
      placeholder: '当文章过期时...',
    },
    { type: 'input', label: '作者', key: 'article:author', placeholder: '文章的作者...' },
    {
      type: 'input',
      label: '章节',
      key: 'article:section',
      placeholder: '重点章节名称，例如科技..',
    },
    { type: 'input', label: '标签', key: 'article:tag', placeholder: '与文章相关的字词...' },
  ],
};
