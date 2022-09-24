// @ts-ignore
/* eslint-disable */

declare namespace API {
  type CurrentUser = {
    name?: string;
    avatar?: string;
    userid?: string;
    email?: string;
    signature?: string;
    title?: string;
    group?: string;
    tags?: { key?: string; label?: string }[];
    notifyCount?: number;
    unreadCount?: number;
    country?: string;
    access?: string;
    geographic?: {
      province?: { label?: string; key?: string };
      city?: { label?: string; key?: string };
    };
    address?: string;
    phone?: string;
  };

  type LoginResult = {
    status?: string;
    type?: string;
    currentAuthority?: string;
    token?: string;
  };

  type ChangePasswordParams = {
    oldPassword?: string;
    newPassword?: string;
  };

  type UserListItem = {
    id?: string;
    userName?: string;
    roleNames?: string;
    phoneNumber?: string;
    creationTime?: Date;
  };

  type AddOrResetUserResponse = {
    success?: boolean;
    data: {
      userName?: string;
      password?: string;
    };
  };

  type UserList = {
    data?: UserListItem[];
    total?: number;
    success?: boolean;
  }

  type PageParams = {
    current?: number;
    pageSize?: number;
  };

  type LinkListItem = {
    id?: string;
    sequencenumber?: number;
    name?: string;
    url?: string;
    description?: string;
    kind?: number;
  };

  type LinkList = {
    data?: LinkListItem[];
    total?: number;
    success?: boolean;
  };

  type ActionLogListItem = {
    id?: string;
    uid?: string;
    os?: string;
    browser?: string;
    ip?: string;
    referer?: string;
    accessName?: string;
    original?: string;
    url?: string;
    controller?: string;
    action?: string;
    method?: string;
    arguments?: string;
    duration?: string;
    creationTime?: Date;
  };

  type ActionLogList = {
    data?: ActionLogListItem[];
    /** 列表的内容总数 */
    total?: number;
    success?: boolean;
  };

  type AboutItem = {
    content: string;
  }

  type DonationItem = {
    content: string;
  }

  type PrivacyItem = {
    content: string;
  }

  type FakeCaptcha = {
    code?: number;
    status?: string;
  };

  type LoginParams = {
    username?: string;
    password?: string;
    autoLogin?: boolean;
    type?: string;
  };

  type ErrorResponse = {
    /** 业务约定的错误码 */
    errorCode: string;
    /** 业务上的错误信息 */
    errorMessage?: string;
    /** 业务上的请求是否成功 */
    success?: boolean;
  };

  type NoticeIconList = {
    data?: NoticeIconItem[];
    /** 列表的内容总数 */
    total?: number;
    success?: boolean;
  };

  type NoticeIconItemType = 'notification' | 'message' | 'event';

  type NoticeIconItem = {
    id?: string;
    extra?: string;
    key?: string;
    read?: boolean;
    avatar?: string;
    title?: string;
    status?: string;
    datetime?: string;
    description?: string;
    type?: NoticeIconItemType;
  };
}
