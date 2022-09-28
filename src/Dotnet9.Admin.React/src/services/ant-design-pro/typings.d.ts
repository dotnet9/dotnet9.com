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

  type ChangeBlogPostVisible = {
    id?: string;
    visible?: boolean;
  };

  type AddOrUpdateBlogPostRequest = {
    id?: string;
    title?: string;
    slug?: string;
    description?: string;
    cover?: string;
    content?: string;
    copyRightType?: string;
    original?: string;
    originalAvatar?: string;
    originalTitle?: string;
    originalLink?: string;
    albumNames?: string[];
    categoryNames?: string[];
    tagNames?: string[];
    visible?: boolean;
  };

  type BlogPostListItem = {
    id?: string;
    title?: string;
    slug?: string;
    description?: string;
    cover?: string;
    copyRightType?: string;
    original?: string;
    originalAvatar?: string;
    originalTitle?: string;
    originalLink?: string;
    albumNames?: string;
    albumIds?: string[];
    categoryNames?: string;
    categoryIds?: string[];
    tagNames?: string;
    tagIds?: string[];
    visible?: boolean | undefined;
  };

  type BlogPostList = {
    data?: CategoryListItem[];
    total?: number;
    success?: boolean;
  };

  type ChangeCategoryVisible = {
    id?: string;
    visible?: boolean;
  };

  type CategoryTreeItem = {
    title?: string;
    value?: string;
    key?: string;
    children?: CategoryTreeItem[];
  }

  type CategoryTree = {
    success: boolean;
    errorCode: string,
    errorMessage: string,
    data: CategoryTreeItem[],
  };

  type CategoryListItem = {
    id?: string;
    parentId?: string;
    parentName?: string;
    sequencenumber?: number;
    name?: string;
    slug?: string;
    cover?: string;
    description?: string;
    visible?: boolean | undefined;
  };

  type CategoryList = {
    data?: CategoryListItem[];
    total?: number;
    success?: boolean;
  };

  type AlbumTreeItem = {
    title?: string;
    value?: string;
    key?: string;
  }

  type AlbumTree = {
    success: boolean;
    errorCode: string,
    errorMessage: string,
    data: AlbumTreeItem[],
  };

  type ChangeAlbumVisible = {
    id?: string;
    visible?: boolean;
  };

  type AlbumListItem = {
    id?: string;
    categoryNames?: string;
    categoryIds?: string;
    sequencenumber?: number;
    name?: string;
    slug?: string;
    cover?: string;
    description?: string;
    visible?: boolean | undefined;
  };

  type AlbumList = {
    data?: AlbumListItem[];
    total?: number;
    success?: boolean;
  };

  type TagTreeItem = {
    title?: string;
    value?: string;
    key?: string;
  }

  type TagTree = {
    success: boolean;
    errorCode: string,
    errorMessage: string,
    data: TagTreeItem[],
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

  type TimelineListItem = {
    id?: string;
    time?: Date;
    title?: string;
    content?: string;
  };

  type TimelineList = {
    data?: TimelineListItem[];
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
