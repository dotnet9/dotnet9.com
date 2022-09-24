import { ModalForm, ProFormSelect, ProFormText } from '@ant-design/pro-components';
import { Button, Result } from 'antd';
import React from 'react';

export type AddOrUpdateUserProps = {
  done: boolean;
  open: boolean;
  current: Partial<API.UserListItem> | undefined;
  onDone: () => void;
  onSubmit: (values: API.UserListItem) => Promise<void>;
};

const AddOrUpdateUser: React.FC<AddOrUpdateUserProps> = (props) => {
  const { done, open, current, onDone, onSubmit, children } = props;
  if (!open) {
    return null;
  }
  return (
    <ModalForm<API.UserListItem>
      open={open}
      title={done ? null : `用户${current ? '编辑' : `添加`}`}
      width="400px"
      onFinish={async (values) => {
        onSubmit(values);
      }}
      initialValues={current}
      submitter={{
        render: (_, dom) => (done ? null : dom),
      }}
      trigger={<>{children}</>}
      modalProps={{
        onCancel: () => onDone(),
        destroyOnClose: true,
        bodyStyle: done ? { padding: '72px 0' } : {},
      }}
    >
      {!done ? (
        <>
          <ProFormText name="id" label="id" hidden />
          <ProFormText
            name="userName"
            label="用户名" 
            readonly={current?.id ? true : false}
            placeholder="请输入2-32个字符"
            width="md"
            rules={[
              {
                required: true,
                message: '请输入名称，长度为2-32个字符',
                min: 2,
                max: 32,
              },
            ]}
          />
          <ProFormSelect
            name="roleNames"
            width="md"
            label="角色"
            request={async () => [
              { label: '普通用户', value: 'User' },
              { label: '管理员', value: 'Admin' },
            ]}
            rules={[
              {
                required: true,
                message: '请选择角色！',
              },
            ]}
          />
          <ProFormText
            name="phoneNumber"
            label="手机号码"
            width="md"
            rules={[
              {
                required: true,
                message: '请输入手机号码',
              },
            ]}
          />
        </>
      ) : (
        <Result
          status="success"
          title="操作成功"
          subTitle="感觉没用的提示"
          extra={
            <Button type="primary" onClick={onDone}>
              知道了
            </Button>
          }
        />
      )}
    </ModalForm>
  );
};

export default AddOrUpdateUser;
