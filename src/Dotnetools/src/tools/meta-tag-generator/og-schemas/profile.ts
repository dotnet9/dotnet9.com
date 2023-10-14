import type { OGSchemaType } from '../OGSchemaType.type';

export const profile: OGSchemaType = {
  name: '简介',
  elements: [
    {
      type: 'input',
      label: '名字',
      placeholder: '输入人员的名字...',
      key: 'profile:first_name',
    },
    {
      type: 'input',
      label: '姓',
      placeholder: '输入人员的姓氏...',
      key: 'profile:last_name',
    },
    { type: 'input', label: '用户名', placeholder: '输入人员的用户名...', key: 'profile:username' },
    { type: 'input', label: '性别', placeholder: '输入人员的性别...', key: 'profile:gender' },
  ],
};
