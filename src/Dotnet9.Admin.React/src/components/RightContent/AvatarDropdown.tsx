import { outLogin } from '@/services/ant-design-pro/api';
import { LogoutOutlined, SettingOutlined, UserOutlined } from '@ant-design/icons';
import ChangePassword from '../../pages/User/Login/ChangePassword';
import { history, useModel } from '@umijs/max';
import { Avatar, Menu, Spin, Modal } from 'antd';
import type { ItemType } from 'antd/lib/menu/hooks/useItems';
import { stringify } from 'querystring';
import type { MenuInfo } from 'rc-menu/lib/interface';
import React, { useCallback, useState } from 'react';
import HeaderDropdown from '../HeaderDropdown';
import styles from './index.less';

export type GlobalHeaderRightProps = {
  menu?: boolean;
};

/**
 * 退出登录，并且将当前的 url 保存
 */
const loginOut = async () => {
  await outLogin();
  const { search, pathname } = window.location;
  const urlParams = new URL(window.location.href).searchParams;
  /** 此方法会跳转到 redirect 参数所在的位置 */
  const redirect = urlParams.get('redirect');
  // Note: There may be security issues, please note
  if (window.location.pathname !== '/user/login' && !redirect) {
    history.replace({
      pathname: '/user/login',
      search: stringify({
        redirect: pathname + search,
      }),
    });
  }
};

const AvatarDropdown: React.FC<GlobalHeaderRightProps> = ({ menu }) => {
  const { initialState, setInitialState } = useModel('@@initialState');
  const [openChangePassword, setOpenChangePassword] = useState<boolean>(false);

  const handleLogout = () => {
    setInitialState((s) => ({ ...s, currentUser: undefined }));
    loginOut();
  };

  const handleDoneChangePassword = () => {
    setOpenChangePassword(false);
  };

  const handleCommitChangePassword = async () => {
    setOpenChangePassword(false);
    Modal.confirm({
      title: '密码修改成功',
      content: '是否重新登录？',
      okText: '确认',
      cancelText: '取消',
      onOk: async () => {
        handleLogout();
      },
    });
  };

  const onMenuClick = useCallback(
    (event: MenuInfo) => {
      const { key } = event;
      if (key === 'logout') {
        handleLogout();
        return;
      }
      if (key == 'changepassword') {
        setOpenChangePassword(true);
        return;
      }
      history.push(`/account/${key}`);
    },
    [setInitialState],
  );

  const loading = (
    <span className={`${styles.action} ${styles.account}`}>
      <Spin
        size="small"
        style={{
          marginLeft: 8,
          marginRight: 8,
        }}
      />
    </span>
  );

  if (!initialState) {
    return loading;
  }

  const { currentUser } = initialState;

  if (!currentUser || !currentUser.name) {
    return loading;
  }

  const menuItems: ItemType[] = [
    ...(menu
      ? [
          {
            key: 'center',
            icon: <UserOutlined />,
            label: '个人中心',
          },
          {
            key: 'settings',
            icon: <SettingOutlined />,
            label: '个人设置',
          },
          {
            type: 'divider' as const,
          },
        ]
      : []),
    {
      key: 'changepassword',
      icon: <LogoutOutlined />,
      label: '修改密码',
    },
    {
      key: 'logout',
      icon: <LogoutOutlined />,
      label: '退出登录',
    },
  ];

  const menuHeaderDropdown = (
    <Menu className={styles.menu} selectedKeys={[]} onClick={onMenuClick} items={menuItems} />
  );

  return (
    <HeaderDropdown overlay={menuHeaderDropdown}>
      <span className={`${styles.action} ${styles.account}`}>
        <Avatar size="small" className={styles.avatar} src={currentUser.avatar} alt="avatar" />
        <span className={`${styles.name} anticon`}>{currentUser.name}</span>
        <ChangePassword
          open={openChangePassword}
          onDone={handleDoneChangePassword}
          onSubmit={handleCommitChangePassword}
        />
      </span>
    </HeaderDropdown>
  );
};

export default AvatarDropdown;
