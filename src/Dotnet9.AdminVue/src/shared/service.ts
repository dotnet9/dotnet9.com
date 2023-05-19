/** Generate by swagger-axios-codegen */
// @ts-nocheck
/* eslint-disable */

/** Generate by swagger-axios-codegen */
/* eslint-disable */
// @ts-nocheck
import axiosStatic, { AxiosInstance, AxiosRequestConfig } from 'axios';

export interface IRequestOptions extends AxiosRequestConfig {
  /** only in axios interceptor config*/
  loading: boolean;
}

export interface IRequestConfig {
  method?: any;
  headers?: any;
  url?: any;
  data?: any;
  params?: any;
}

// Add options interface
export interface ServiceOptions {
  axios?: AxiosInstance;
  /** only in axios interceptor config*/
  loading: boolean;
  showError: boolean;
}

// Add default options
export const serviceOptions: ServiceOptions = {};

// Instance selector
export function axios(configs: IRequestConfig, resolve: (p: any) => void, reject: (p: any) => void): Promise<any> {
  if (serviceOptions.axios) {
    return serviceOptions.axios
      .request(configs)
      .then(res => {
        resolve(res.data);
      })
      .catch(err => {
        reject(err);
      });
  } else {
    throw new Error('please inject yourself instance like axios  ');
  }
}

export function getConfigs(method: string, contentType: string, url: string, options: any): IRequestConfig {
  const configs: IRequestConfig = {
    loading: serviceOptions.loading,
    showError: serviceOptions.loading,
    ...options,
    method,
    url
  };
  configs.headers = {
    ...options.headers,
    'Content-Type': contentType
  };
  return configs;
}

export const basePath = '';

export interface IList<T> extends Array<T> {}
export interface List<T> extends Array<T> {}
export interface IDictionary<TValue> {
  [key: string]: TValue;
}
export interface Dictionary<TValue> extends IDictionary<TValue> {}

export interface IListResult<T> {
  items?: T[];
}

export class ListResultDto<T> implements IListResult<T> {
  items?: T[];
}

export interface IPagedResult<T> extends IListResult<T> {
  totalCount?: number;
  items?: T[];
}

export class PagedResultDto<T = any> implements IPagedResult<T> {
  totalCount?: number;
  items?: T[];
}

// customer definition
// empty

export class AccountService {
  /**
   * 初始化系统账号
   */
  static initAccount(options: IRequestOptions = {}): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Account/InitAccount';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 测试event
   */
  static test(options: IRequestOptions = {}): Promise<string> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Account/Test';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 账户列表
   */
  static getList(
    params: {
      /** 页码 */
      index?: number;
      /** 页大小 */
      pageSize?: number;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<AccountItemModelPageDto> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Account/GetList';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);
      configs.params = { Index: params['index'], PageSize: params['pageSize'] };

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 禁止登录
   */
  static accountForbidLogin(
    params: {
      /**  */
      accountId?: string;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Account/AccountForbidLogin';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);
      configs.params = { accountId: params['accountId'] };

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 创建用户
   */
  static createUser(
    params: {
      /** requestBody */
      body?: CreateUserModel;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Account/CreateUser';

      const configs: IRequestConfig = getConfigs('post', 'application/json', url, options);

      let data = params.body;

      configs.data = data;

      axios(configs, resolve, reject);
    });
  }
  /**
   * 修改密码
   */
  static updatePwd(
    params: {
      /** requestBody */
      body?: UpdatePwdModel;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Account/UpdatePwd';

      const configs: IRequestConfig = getConfigs('post', 'application/json', url, options);

      let data = params.body;

      configs.data = data;

      axios(configs, resolve, reject);
    });
  }
}

export class AuthService {
  /**
   * 登录
   */
  static login(
    params: {
      /** requestBody */
      body?: LoginModel;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Auth/Login';

      const configs: IRequestConfig = getConfigs('post', 'application/json', url, options);

      let data = params.body;

      configs.data = data;

      axios(configs, resolve, reject);
    });
  }
  /**
   * 退出
   */
  static loginOut(options: IRequestOptions = {}): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Auth/LoginOut';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 修改密码
   */
  static changeCurrPwd(
    params: {
      /** requestBody */
      body?: ChangeCurrPwd;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Auth/ChangeCurrPwd';

      const configs: IRequestConfig = getConfigs('post', 'application/json', url, options);

      let data = params.body;

      configs.data = data;

      axios(configs, resolve, reject);
    });
  }
}

export class CateService {
  /**
   * 分类列表
   */
  static getList(
    params: {
      /** 页码 */
      index?: number;
      /** 页大小 */
      pageSize?: number;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<CateDtoModelPageDto> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Cate/GetList';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);
      configs.params = { Index: params['index'], PageSize: params['pageSize'] };

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 添加和编辑分类
   */
  static edit(
    params: {
      /** requestBody */
      body?: CateRequest;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Cate/Edit';

      const configs: IRequestConfig = getConfigs('post', 'application/json', url, options);

      let data = params.body;

      configs.data = data;

      axios(configs, resolve, reject);
    });
  }
  /**
   * 删除分类
   */
  static delete(
    params: {
      /**  */
      id?: number;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Cate/Delete';

      const configs: IRequestConfig = getConfigs('delete', 'application/json', url, options);
      configs.params = { Id: params['id'] };

      let data = null;

      configs.data = data;

      axios(configs, resolve, reject);
    });
  }
}

export class CommonService {
  /**
   * 上传
   */
  static upload(options: IRequestOptions = {}): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Common/Upload';

      const configs: IRequestConfig = getConfigs('post', 'application/json', url, options);

      let data = null;

      configs.data = data;

      axios(configs, resolve, reject);
    });
  }
}

export class ConfigService {
  /**
   *
   */
  static test(options: IRequestOptions = {}): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Config/Test';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 保存配置
   */
  static setSite(
    params: {
      /** requestBody */
      body?: SiteConfig;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Config/SetSite';

      const configs: IRequestConfig = getConfigs('post', 'application/json', url, options);

      let data = params.body;

      configs.data = data;

      axios(configs, resolve, reject);
    });
  }
  /**
   * 获取站点配置
   */
  static getSiteConfig(options: IRequestOptions = {}): Promise<SiteConfig> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Config/GetSiteConfig';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
}

export class FriendLinkService {
  /**
   * 友情链接列表
   */
  static getList(
    params: {
      /** 页码 */
      index?: number;
      /** 页大小 */
      pageSize?: number;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<FriendLinkModelPageDto> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/FriendLink/GetList';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);
      configs.params = { Index: params['index'], PageSize: params['pageSize'] };

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 编辑
   */
  static edit(
    params: {
      /** requestBody */
      body?: FriendLinkModel;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/FriendLink/Edit';

      const configs: IRequestConfig = getConfigs('post', 'application/json', url, options);

      let data = params.body;

      configs.data = data;

      axios(configs, resolve, reject);
    });
  }
}

export class PostService {
  /**
   *
   */
  static get(
    params: {
      /**  */
      id?: number;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<PostEditRequest> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Post/Get';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);
      configs.params = { Id: params['id'] };

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 置顶
   */
  static top(
    params: {
      /**  */
      id?: number;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Post/Top';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);
      configs.params = { Id: params['id'] };

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 文章列表
   */
  static list(
    params: {
      /**  */
      cateId?: number;
      /**  */
      tagId?: number;
      /** 页码 */
      index?: number;
      /** 页大小 */
      pageSize?: number;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<PostItemModelPageDto> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Post/List';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);
      configs.params = {
        CateId: params['cateId'],
        TagId: params['tagId'],
        Index: params['index'],
        PageSize: params['pageSize']
      };

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 新增和编辑文章
   */
  static edit(
    params: {
      /** requestBody */
      body?: PostEditRequest;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Post/Edit';

      const configs: IRequestConfig = getConfigs('post', 'application/json', url, options);

      let data = params.body;

      configs.data = data;

      axios(configs, resolve, reject);
    });
  }
}

export class PostTagService {
  /**
   * 获取标签列表
   */
  static getList(
    params: {
      /** 页码 */
      index?: number;
      /** 页大小 */
      pageSize?: number;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<TagDtoModelPageDto> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/PostTag/GetList';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);
      configs.params = { Index: params['index'], PageSize: params['pageSize'] };

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
}

export class ResourceService {
  /**
   * 获取资源文件
   */
  static get(
    params: {
      /** 后缀名 */
      suffix?: string;
      /** 页码 */
      index?: number;
      /** 页大小 */
      pageSize?: number;
    } = {} as any,
    options: IRequestOptions = {}
  ): Promise<ResourceModelsPageDto> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin/Resource/Get';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);
      configs.params = { Suffix: params['suffix'], Index: params['index'], PageSize: params['pageSize'] };

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
}

export class SpaService {
  /**
   *
   */
  static efDb(options: IRequestOptions = {}): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/ef-db';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
  /**
   * 后台Spa页面
   */
  static admin(options: IRequestOptions = {}): Promise<any> {
    return new Promise((resolve, reject) => {
      let url = basePath + '/admin';

      const configs: IRequestConfig = getConfigs('get', 'application/json', url, options);

      /** 适配ios13，get请求不允许带body */

      axios(configs, resolve, reject);
    });
  }
}

export interface AccountItemModel {
  /**  */
  id?: string;

  /**  */
  userName?: string;

  /**  */
  email?: string;

  /**  */
  createTime?: Date;

  /**  */
  lastUpdateTime?: Date;

  /**  */
  lastLoginTime?: Date;

  /**  */
  roleName?: string[];

  /**  */
  lockedTime?: Date;

  /**  */
  isLocked?: boolean;
}

export interface AccountItemModelPageDto {
  /**  */
  total?: number;

  /**  */
  data?: AccountItemModel[];
}

export interface CateDtoModel {
  /**  */
  id?: number;

  /**  */
  cateName?: string;

  /**  */
  postCount?: number;

  /**  */
  createTime?: Date;

  /**  */
  lastUpdateTime?: Date;
}

export interface CateDtoModelPageDto {
  /**  */
  total?: number;

  /**  */
  data?: CateDtoModel[];
}

export interface CateItem {
  /**  */
  id?: number;

  /**  */
  cateName?: string;
}

export interface CateRequest {
  /**  */
  id?: number;

  /**  */
  cateName?: string;
}

export interface ChangeCurrPwd {
  /**  */
  oldPwd?: string;

  /**  */
  newPwd?: string;
}

export interface CreateUserModel {
  /** 用户名 */
  userName?: string;

  /** 密码 */
  password?: string;

  /** 邮箱 */
  email?: string;
}

export interface FriendLinkModel {
  /**  */
  name?: string;

  /**  */
  url?: string;

  /**  */
  order?: number;

  /**  */
  id?: number;

  /**  */
  isPublish?: boolean;
}

export interface FriendLinkModelPageDto {
  /**  */
  total?: number;

  /**  */
  data?: FriendLinkModel[];
}

export interface LoginModel {
  /** 账号 */
  userName: string;

  /** 密码 */
  pwd: string;

  /** 验证码 */
  validCode?: string;
}

export interface PostEditRequest {
  /**  */
  id?: number;

  /** 标题 */
  title: string;

  /** 缩略图 */
  thumb?: string;

  /** 内容 */
  content: string;

  /** 摘要 */
  snippet: string;

  /** 标签 */
  tagsStr?: string;

  /** 标签 */
  tags?: string[];

  /** 分类 */
  cates?: number[];

  /** 是否发布 */
  isPublish?: boolean;
}

export interface PostItemModel {
  /**  */
  id?: number;

  /**  */
  title?: string;

  /**  */
  thumb?: string;

  /**  */
  content?: string;

  /**  */
  snippet?: string;

  /**  */
  lastUpdateTime?: Date;

  /**  */
  readCount?: number;

  /**  */
  commentCount?: number;

  /**  */
  tagItems?: TagItem[];

  /**  */
  cateItems?: CateItem[];

  /**  */
  isTop?: boolean;

  /**  */
  isPublish?: boolean;
}

export interface PostItemModelPageDto {
  /**  */
  total?: number;

  /**  */
  data?: PostItemModel[];
}

export interface ResourceModels {
  /**  */
  url?: string;

  /**  */
  suffix?: string;

  /**  */
  domain?: string;
}

export interface ResourceModelsPageDto {
  /**  */
  total?: number;

  /**  */
  data?: ResourceModels[];
}

export interface SiteConfig {
  /**  */
  siteName: string;

  /**  */
  icp: string;

  /**  */
  age?: number;
}

export interface TagDtoModel {
  /**  */
  id?: number;

  /**  */
  tagName?: string;

  /**  */
  count?: number;
}

export interface TagDtoModelPageDto {
  /**  */
  total?: number;

  /**  */
  data?: TagDtoModel[];
}

export interface TagItem {
  /**  */
  id?: number;

  /**  */
  tagName?: string;
}

export interface UpdatePwdModel {
  /**  */
  id?: string;

  /**  */
  newPwd?: string;
}
