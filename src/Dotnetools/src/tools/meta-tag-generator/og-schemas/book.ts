import type { OGSchemaType } from '../OGSchemaType.type';

export const book: OGSchemaType = {
  name: '书籍',
  elements: [
    { type: 'input', label: '作者', key: 'book:author', placeholder: '谁写了这本书...' },
    { type: 'input', label: 'ISBN', key: 'book:isbn', placeholder: '国际标准书号...' },
    {
      type: 'input',
      label: '发行时间',
      key: 'book:release_date',
      placeholder: '这本书的出版发行日期...',
    },
    { type: 'input', label: '标签', key: 'book:tag', placeholder: '与本书相关的字词...' },
  ],
};
