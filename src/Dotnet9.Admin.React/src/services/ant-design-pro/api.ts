// @ts-ignore
/* eslint-disable */
import { request } from '@umijs/max';

/** 获取当前的用户 GET /api/currentUser */
export async function currentUser(options?: { [key: string]: any }) {
  return request<{
    data: API.CurrentUser;
  }>('/api/login/currentUser', {
    method: 'GET',
    ...(options || {}),
  });
}

/** 退出登录接口 POST /api/login/outLogin */
export async function outLogin(options?: { [key: string]: any }) {
  return request<Record<string, any>>('/api/login/outLogin', {
    method: 'POST',
    ...(options || {}),
  });
}

/** 登录接口 POST /api/login/account */
export async function login(body: API.LoginParams, options?: { [key: string]: any }) {
  return request<API.LoginResult>('/api/login/account', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

/** 修改密码接口 POST /api/login/changepassword */
export async function changepassword(body: API.ChangePasswordParams, options?: { [key: string]: any }) {
  return request<API.LoginResult>('/api/login/changepassword', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

/** 此处后端没有提供注释 GET /api/notices */
export async function getNotices(options?: { [key: string]: any }) {
  return request<API.NoticeIconList>('/api/notices', {
    method: 'GET',
    ...(options || {}),
  });
}

/** 获取所有用户列表 GET /api/user */
export async function user(  
  params: {
    current?: number;
    pageSize?: number;
  },
  options?: { [key: string]: any },
) {
  return request<API.UserList>('/api/user', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 更新用户 PUT /api/user */
export async function updateUser(body: API.UserListItem, options?: { [key: string]: any }) {
  return request<API.UserListItem>(`/api/user/${body.id}`, {
    method: 'PUT',
    data: body,
    ...(options || {}),
  });
}

/** 新建用户 POST /api/user */
export async function addUser(body: API.UserListItem, options?: { [key: string]: any }) {
  return request<API.AddOrResetUserResponse>('/api/user', {
    method: 'POST',
    data: body,
    ...(options || {}),
  });
}

/** 重置用户密码 Post /api/user/id */
export async function resetUserPassword(data: string, options?: { [key: string]: any }) {
  return request<API.AddOrResetUserResponse>(`/api/user/${data}`, {
    method: 'POST',
    ...(options || {}),
  });
}

/** 删除用户 DELETE /api/user */
export async function removeUser(data: string[], options?: { [key: string]: any }) {
  return request<Record<string, any>>('/api/user', {
    method: 'DELETE',
    data: { ids: data },
    ...(options || {}),
  });
}


/** 获取时间线列表 GET /api/timeline */
export async function timeline(
  params: {
    current?: number;
    pageSize?: number;
  },
  options?: { [key: string]: any },
) {
  return request<API.TimelineList>('/api/timeline', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 更新时间线 PUT /api/timeline */
export async function updateTimeline(body: API.LinkListItem, options?: { [key: string]: any }) {
  return request<API.TimelineListItem>(`/api/timeline/${body.id}`, {
    method: 'PUT',
    data: body,
    ...(options || {}),
  });
}

/** 新建时间线 POST /api/timeline */
export async function addTimeline(body: API.TimelineListItem, options?: { [key: string]: any }) {
  return request<API.TimelineListItem>('/api/timeline', {
    method: 'POST',
    data: body,
    ...(options || {}),
  });
}

/** 删除时间线 DELETE /api/timeline */
export async function removeTimeline(data: string[], options?: { [key: string]: any }) {
  return request<Record<string, any>>('/api/timeline', {
    method: 'DELETE',
    data: { ids: data },
    ...(options || {}),
  });
}

/** 获取分类名列表 GET /api/category/names */
export async function categoryNames(
  options?: { [key: string]: any },
) {
  return request<API.CategoryNames>('/api/category/names', {
    method: 'GET',
    ...(options || {}),
  });
}

/** 获取分类列表 GET /api/category */
export async function category(
  params: {
    current?: number;
    pageSize?: number;
  },
  options?: { [key: string]: any },
) {
  return request<API.CategoryList>('/api/category', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 更新分类 PUT /api/category */
export async function updateCategory(body: API.CategoryListItem, options?: { [key: string]: any }) {
  return request<API.CategoryListItem>(`/api/category/${body.id}`, {
    method: 'PUT',
    data: body,
    ...(options || {}),
  });
}

/** 更新分类可见性 PUT /api/category/{id}/changeVisible */
export async function changeCategoryVisible(body: API.ChangeCategoryVisible, options?: { [key: string]: any }) {
  return request<API.CategoryListItem>(`/api/category/${body.id}/changeVisible`, {
    method: 'PUT',
    data: body,
    ...(options || {}),
  });
}

/** 新建分类 POST /api/category */
export async function addCategory(body: API.CategoryListItem, options?: { [key: string]: any }) {
  return request<API.CategoryListItem>('/api/category', {
    method: 'POST',
    data: body,
    ...(options || {}),
  });
}

/** 删除分类 DELETE /api/category */
export async function removeCategory(data: string[], options?: { [key: string]: any }) {
  return request<Record<string, any>>('/api/category', {
    method: 'DELETE',
    data: { ids: data },
    ...(options || {}),
  });
}

/** 获取专辑列表 GET /api/album */
export async function album(
  params: {
    current?: number;
    pageSize?: number;
  },
  options?: { [key: string]: any },
) {
  return request<API.AlbumList>('/api/album', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 更新专辑 PUT /api/album */
export async function updateAlbum(body: API.AlbumListItem, options?: { [key: string]: any }) {
  return request<API.AlbumListItem>(`/api/album/${body.id}`, {
    method: 'PUT',
    data: body,
    ...(options || {}),
  });
}

/** 更新专辑可见性 PUT /api/album/{id}/changeVisible */
export async function changeAlbumVisible(body: API.ChangeAlbumVisible, options?: { [key: string]: any }) {
  return request<API.AlbumListItem>(`/api/album/${body.id}/changeVisible`, {
    method: 'PUT',
    data: body,
    ...(options || {}),
  });
}

/** 新建专辑 POST /api/album */
export async function addAlbum(body: API.AlbumListItem, options?: { [key: string]: any }) {
  return request<API.LinkListItem>('/api/album', {
    method: 'POST',
    data: body,
    ...(options || {}),
  });
}

/** 删除专辑 DELETE /api/album */
export async function removeAlbum(data: string[], options?: { [key: string]: any }) {
  return request<Record<string, any>>('/api/album', {
    method: 'DELETE',
    data: { ids: data },
    ...(options || {}),
  });
}

/** 获取链接列表 GET /api/link */
export async function link(
  params: {
    current?: number;
    pageSize?: number;
  },
  options?: { [key: string]: any },
) {
  return request<API.LinkList>('/api/link', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 更新链接 PUT /api/link */
export async function updateLink(body: API.LinkListItem, options?: { [key: string]: any }) {
  return request<API.LinkListItem>(`/api/link/${body.id}`, {
    method: 'PUT',
    data: body,
    ...(options || {}),
  });
}

/** 新建链接 POST /api/link */
export async function addLink(body: API.LinkListItem, options?: { [key: string]: any }) {
  return request<API.LinkListItem>('/api/link', {
    method: 'POST',
    data: body,
    ...(options || {}),
  });
}

/** 删除链接 DELETE /api/link */
export async function removeLink(data: string[], options?: { [key: string]: any }) {
  return request<Record<string, any>>('/api/link', {
    method: 'DELETE',
    data: { ids: data },
    ...(options || {}),
  });
}

/** 获取操作日志列表 GET /api/actionlog */
export async function actionLog(
  params: {
    current?: number;
    pageSize?: number;
  },
  sort: { [key: string]: any },
  options?: { [key: string]: any },
) {
  return request<API.ActionLogList>('/api/actionlog', {
    method: 'GET',
    params: {
      ...params,
      sort,
    },
    ...(options || {}),
  });
}

/** 删除操作日志 DELETE /api/actionlog */
export async function removeActionLog(data: string[], options?: { [key: string]: any }) {
  return request<Record<string, any>>('/api/actionlog', {
    method: 'DELETE',
    data: { ids: data },
    ...(options || {}),
  });
}

/** 获取赞助 GET /api/donation */
export async function donation(
  options?: { [key: string]: any },
) {
  return request<API.DonationItem>('/api/donation', {
    method: 'GET',
    ...(options || {}),
  });
}

/** 添加或更新赞助 POST /api/donation */
export async function addOrUpdateDonation(body: { [key: string]: any }, options?: { [key: string]: any }) {
  return request<string>('/api/donation', {
    method: 'POST',
    data: body,
    ...(options || {}),
  });
}

/** 获取关于 GET /api/about */
export async function about(
  options?: { [key: string]: any },
) {
  return request<API.AboutItem>('/api/about', {
    method: 'GET',
    ...(options || {}),
  });
}

/** 添加或更新关于 POST /api/about */
export async function addOrUpdateAbout(body: { [key: string]: any }, options?: { [key: string]: any }) {
  return request<string>('/api/about', {
    method: 'POST',
    data: body,
    ...(options || {}),
  });
}

/** 获取隐私声明 GET /api/privacy */
export async function privacy(
  options?: { [key: string]: any },
) {
  return request<API.PrivacyItem>('/api/privacy', {
    method: 'GET',
    ...(options || {}),
  });
}

/** 添加或更新隐私声明 POST /api/privacy */
export async function addOrUpdatePrivacy(body: { [key: string]: any }, options?: { [key: string]: any }) {
  return request<string>('/api/privacy', {
    method: 'POST',
    data: body,
    ...(options || {}),
  });
}