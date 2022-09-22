import { changepassword } from '@/services/ant-design-pro/api';
import { ModalForm, ProFormText } from '@ant-design/pro-components';
import { message } from 'antd';
import { LockOutlined } from '@ant-design/icons';
import React from 'react';
import styles from './index.less';

export type ChangePasswordFormProps = {
  open: boolean;
  onDone: () => void;
  onSubmit: () => Promise<void>;
};

const ChangePassword: React.FC<ChangePasswordFormProps> = (props) => {
  const { open, onDone, onSubmit } = props;
  const handleSubmit = async (fields: API.ChangePasswordParams) => {
    const hide = message.loading('正在提交修改');
    try {
      await changepassword(fields);
      hide();
      message.success('修改成功，即将退出重新登录！');
      onSubmit();
      return true;
    } catch (error) {
      hide();
      message.error('修改失败，请重试！');
      return false;
    }
  };
  if (!open) {
    return null;
  }
  return (
    <ModalForm<API.ChangePasswordParams>
      open={open}
      title="修改密码"
      width="400px"
      onFinish={async (values) => {
        await handleSubmit(values);
      }}
      modalProps={{
        onCancel: () => onDone(),
        destroyOnClose: true,
        bodyStyle: { padding: '22px' },
      }}
    >
      <ProFormText.Password
        name="oldPassword"
        fieldProps={{
          size: 'large',
          prefix: <LockOutlined className={styles.prefixIcon} />,
        }}
        label="旧密码"
        placeholder="请输入旧密码"
        width="md"
        rules={[
          {
            required: true,
            message: '请输入旧密码，长度为2-15个字符',
            min: 2,
            max: 15,
          },
        ]}
      />
      <ProFormText.Password
        name="newPassword"
        fieldProps={{
          size: 'large',
          prefix: <LockOutlined className={styles.prefixIcon} />,
        }}
        label="新密码"
        placeholder="请输入新密码"
        width="md"
        rules={[
          {
            required: true,
            message: '请输入新密码，长度为2-15个字符',
            min: 2,
            max: 15,
          },
        ]}
      />
      <ProFormText.Password
        name="password2"
        fieldProps={{
          size: 'large',
          prefix: <LockOutlined className={styles.prefixIcon} />,
        }}
        label="确认密码"
        placeholder="请输入确认密码"
        width="md"
        dependencies={['newPassword']}
        rules={[
          {
            required: true,
            message: '请输入确认密码，长度为2-15个字符',
            min: 2,
            max: 15,
          },
          ({ getFieldValue }) => ({
            validator(rule, value) {
              if (!value || getFieldValue('newPassword') === value) {
                return Promise.resolve();
              }
              return Promise.reject('新密码与确认新密码不同！');
            },
          }),
        ]}
      />
    </ModalForm>
  );
};

export default ChangePassword;
