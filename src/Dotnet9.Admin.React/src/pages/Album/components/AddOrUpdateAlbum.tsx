import { categoryTree } from '@/services/ant-design-pro/api';
import {
  ModalForm,
  ProFormText,
  ProFormDigit,
  ProFormTextArea,
  ProFormRadio,
  ProFormTreeSelect,
} from '@ant-design/pro-components';
import { Button, Result, message, TreeSelect } from 'antd';
import React, { useEffect, useState } from 'react';

export type AddOrUpdateAlbumProps = {
  done: boolean;
  open: boolean;
  current: Partial<API.AlbumListItem> | undefined;
  onDone: () => void;
  onSubmit: (values: API.AlbumListItem) => Promise<void>;
};

const AddOrUpdateAlbum: React.FC<AddOrUpdateAlbumProps> = (props) => {
  const { done, open, current, onDone, onSubmit, children } = props;
  const [categoryTreeItems, setCategoryTreeItems] = useState<API.CategoryTreeItem[]>([]);

  const handleRead = async () => {
    const hide = message.loading('正在读取');
    try {
      const data = await categoryTree();
      setCategoryTreeItems(data.data);
      hide();
      message.success('读取成功');
      return true;
    } catch (e) {
      hide();
      message.error('读取失败');
      return false;
    }
  };

  useEffect(() => {
    handleRead();
  }, []);
  if (!open) {
    return null;
  }

  return (
    <ModalForm<API.AlbumListItem>
      open={open}
      title={done ? null : `专辑${current ? '编辑' : `添加`}`}
      width={600}
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
            name="cover"
            label="封面"
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
            fieldProps={{
              style: {
                width: '100%',
              },
            }}
          />
          <ProFormRadio.Group
            name="visible"
            label="是否可见"
            required
            initialValue={true}
            options={[
              {
                value: true,
                label: '显示',
              },
              {
                value: false,
                label: '隐藏',
              },
            ]}
            fieldProps={{
              style: {
                width: '100%',
              },
            }}
          />
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
            fieldProps={{
              style: {
                width: '100%',
              },
            }}
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
            fieldProps={{
              style: {
                width: '100%',
              },
            }}
          />
          <ProFormText
            name="slug"
            label="别名"
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
            fieldProps={{
              style: {
                width: '100%',
              },
            }}
          />
          <ProFormTreeSelect
            name="categoryIds"
            label="所属分类"
            request={async () => categoryTreeItems}
            fieldProps={{
              fieldNames: {
                label: 'title',
              },
              treeCheckable: true,
              showCheckedStrategy: TreeSelect.SHOW_PARENT,
              placeholder: '请选择',
            }}
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
            fieldProps={{
              style: {
                width: '100%',
              },
            }}
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

export default AddOrUpdateAlbum;
