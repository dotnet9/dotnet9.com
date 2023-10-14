import type { OGSchemaType } from '../OGSchemaType.type';

const typeOptions = [
  { label: '网站', value: 'website' },
  { label: '文章', value: 'article' },
  { label: '书籍', value: 'book' },
  { label: '简介', value: 'profile' },
  {
    type: 'group',
    label: '音乐',
    key: 'Music',
    children: [
      { label: 'Song', value: 'music.song' },
      { label: 'Music album', value: 'music.album' },
      { label: 'Playlist', value: 'music.playlist' },
      { label: 'Radio station', value: 'music.radio_station' },
    ],
  },
  {
    type: 'group',
    label: '视频',
    key: 'Video',
    children: [
      { label: 'Movie', value: 'video.movie' },
      { label: 'Episode', value: 'video.episode' },
      { label: 'TV show', value: 'video.tv_show' },
      { label: 'Other video', value: 'video.other' },
    ],
  },
];

export const website: OGSchemaType = {
  name: '基本信息',
  elements: [
    {
      type: 'select',
      label: '页面类型',
      placeholder: '选择网站类型...',
      key: 'type',
      options: typeOptions,
    },
    { type: 'input', label: '标题', placeholder: '输入您网站的标题...', key: 'title' },
    {
      type: 'input',
      label: '描述',
      placeholder: '输入您网站的描述...',
      key: 'description',
    },
    {
      type: 'input',
      label: '页面网址',
      placeholder: '输入您网站的网址...',
      key: 'url',
    },
  ],
};
