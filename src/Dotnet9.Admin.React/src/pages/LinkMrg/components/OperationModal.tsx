import {
  ModalForm,
  ProFormSelect,
  ProFormText,
  ProFormDigit,
  ProFormTextArea,
} from '@ant-design/pro-components';
import { Button, Result } from 'antd';
import React from 'react';

export type OperationModalProps = {
  done: boolean;
  open: boolean;
  current: Partial<API.LinkListItem> | undefined;
  onDone: () => void;
  onSubmit: (values: API.LinkListItem) => void;
};

const OperationModal: React.FC<OperationModalProps> = (props) => {
  const { done, open, current, onDone, onSubmit, children } = props;
  if (!open) {
    return null;
  }
  return (
    <ModalForm<API.LinkListItem>
      open={open}
      title={done ? null : `链接${current ? '编辑' : `添加`}`}
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
          <ProFormText name="id" label="id" hidden/>
          <ProFormText
            name="name"
            label="名称"
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
          <ProFormText
            name="url"
            label="链接"
            placeholder="请输入2-256个字符"
            width="md"
            rules={[
              {
                required: true,
                message: '请输入链接，长度为2-256个字符',
                min: 2,
                max: 256,
              },
            ]}
          />
          <ProFormDigit
            name="sequenceNumber"
            label="序号"
            min={0}
            max={100}
            width="md"
            rules={[
              {
                required: true,
                message: '请输入序号！',
              },
            ]}
          />
          <ProFormSelect
            name="kind"
            width="md"
            label="链接类型"
            request={async () => [
              { label: '私密', value: 'Private' },
              { label: '网站相关', value: 'Owner' },
              { label: '友情链接', value: 'Friend' },
              { label: '课程链接', value: 'Course' },
            ]}
            rules={[
              {
                required: true,
                message: '请选择链接类型！',
              },
            ]}
          />
          <ProFormTextArea
            name="description"
            width="md"
            label="描述"
            placeholder="请输入不多于256个字符"
            rules={[
              {
                message: '请输入不多于256个字符的描述！',
                max: 256,
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

export default OperationModal;
