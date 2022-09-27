import {
  addBlogPost,
  removeBlogPost,
  blogpost,
  updateBlogPost,
  changeBlogPostVisible,
} from '@/services/ant-design-pro/api';
import { PlusOutlined } from '@ant-design/icons';
import type { ActionType, ProColumns, ProDescriptionsItemProps } from '@ant-design/pro-components';
import {
  FooterToolbar,
  PageContainer,
  ProDescriptions,
  ProTable,
} from '@ant-design/pro-components';
import { Button, Drawer, message, Modal, Switch } from 'antd';
import React, { useRef, useState } from 'react';
import AddOrUpdateBlogPost from './components/AddOrUpdateBlogPost';

const handleAdd = async (fields: API.BlogPostListItem) => {
  const hide = message.loading('正在添加');
  try {
    await addBlogPost({ ...fields });
    hide();
    message.success('添加成功');
    return true;
  } catch (error) {
    hide();
    message.error('添加失败，请重试！');
    return false;
  }
};

const handleUpdate = async (fields: API.BlogPostListItem) => {
  const hide = message.loading('正在更新');
  try {
    await updateBlogPost({ ...fields });
    hide();
    message.success('更新成功');
    return true;
  } catch (error) {
    hide();
    message.error('更新失败，请重试！');
    return false;
  }
};

const handleChangeVisible = async (id?: string, categoryVisible?: boolean) => {
  const hide = message.loading('正在更新');
  try {
    await changeBlogPostVisible({ id, visible: categoryVisible });
    hide();
    message.success('更新成功');
    return true;
  } catch (error) {
    hide();
    message.error('更新失败，请重试！');
    return false;
  }
};

const handleRemove = async (data: string[]) => {
  const hide = message.loading('正在删除');
  if (!data) return true;
  try {
    await removeBlogPost(data);
    hide();
    message.success('删除成功，即将刷新');
    return true;
  } catch (error) {
    hide();
    message.error('删除失败，请重试');
    return false;
  }
};

const BlogPostTableList: React.FC = () => {
  const [done, setDone] = useState<boolean>(false);
  const [visible, setVisible] = useState<boolean>(false);
  const [current, setCurrent] = useState<API.BlogPostListItem | undefined>(undefined);
  const [showDetail, setShowDetail] = useState<boolean>(false);
  const actionRef = useRef<ActionType>();
  const [selectedRowsState, setSelectedRows] = useState<API.BlogPostListItem[]>([]);

  const handleDone = () => {
    setDone(false);
    setVisible(false);
    setCurrent(undefined);
  };

  const handleAddOrUpdateSubmit = async (values: API.BlogPostListItem) => {
    const success = values.id ? await handleUpdate(values) : await handleAdd(values);
    if (success) {
      handleDone();
      if (actionRef.current) {
        actionRef.current.reload();
      }
    }
  };

  const handleRemoveSubmit = async (ids: string[], isBatch: boolean) => {
    Modal.confirm({
      title: '删除文章',
      content: '确定删除该文章吗？',
      okText: '确认',
      cancelText: '取消',
      onOk: async () => {
        const success = await handleRemove(ids);
        if (success) {
          if (isBatch) {
            setSelectedRows([]);
            actionRef.current?.reloadAndRest?.();
          } else {
            handleDone();
            if (actionRef.current) {
              actionRef.current.reload();
            }
          }
        }
      },
    });
  };

  const handleChangeVisibleCommit = (categoryVisible: boolean, record: API.BlogPostListItem) => {
    handleChangeVisible(record.id, categoryVisible);
  };

  const columns: ProColumns<API.BlogPostListItem>[] = [
    {
      title: 'ID',
      dataIndex: 'id',
      tip: 'Id是唯一的 key',
      hideInForm: true,
      hideInSearch: true,
      hideInTable: true,
      hideInDescriptions: true,
    },
    {
      title: '关键字',
      dataIndex: 'keywords',
      hideInTable: true,
      hideInForm: true,
      hideInDescriptions: true,
    },
    {
      title: '封面',
      dataIndex: 'cover',
      valueType: 'image',
      hideInSearch: true,
    },
    {
      title: '标题',
      dataIndex: 'title',
      hideInSearch: true,
    },
    {
      title: '版权',
      hideInSearch: true,
      dataIndex: 'copyRightType',
    },
    {
      title: '描述',
      hideInSearch: true,
      dataIndex: 'description',
    },
    {
      title: '别名',
      dataIndex: 'slug',
      hideInSearch: true,
    },
    {
      title: '专辑名',
      dataIndex: 'albumNames',
      hideInSearch: true,
    },
    {
      title: '分类名',
      dataIndex: 'categoryNames',
      hideInSearch: true,
    },
    {
      title: '标签',
      dataIndex: 'tagNames',
      hideInSearch: true,
    },
    {
      title: '来源',
      dataIndex: 'originalAvatar',
      valueType: 'image',
      hideInSearch: true,
    },    
    {
      title: '来源',
      dataIndex: 'original',
      hideInSearch: true,
    },
    {
      title: '原文标题',
      dataIndex: 'originalTitle',
      hideInSearch: true,
    },
    {
      title: '原文链接',
      dataIndex: 'originalLink',
      hideInSearch: true,
    },
    {
      title: '可见',
      dataIndex: 'visible',
      hideInSearch: true,
      render: (val, record) => (
        <Switch
          defaultChecked={val}
          onChange={(checked) => handleChangeVisibleCommit(checked, record)}
        />
      ),
    },
    {
      title: '操作',
      dataIndex: 'option',
      valueType: 'option',
      hideInDescriptions: true,
      render: (_, record) => [
        <a
          key="showDetail"
          onClick={() => {
            setCurrent(record);
            setShowDetail(true);
          }}
        >
          查看
        </a>,
        <a
          key="edit"
          onClick={() => {
            setVisible(true);
            setCurrent(record);
          }}
        >
          编辑
        </a>,
        <a
          key="delete"
          onClick={() => {
            handleRemoveSubmit([record.id!], false);
          }}
        >
          删除
        </a>,
      ],
    },
  ];

  return (
    <PageContainer>
      <ProTable<API.BlogPostListItem, API.PageParams>
        headerTitle="查询表格"
        actionRef={actionRef}
        rowKey="id"
        search={{
          labelWidth: 120,
        }}
        toolBarRender={() => [
          <Button
            type="primary"
            key="primary"
            onClick={() => {
              setVisible(true);
              setCurrent(undefined);
            }}
          >
            <PlusOutlined /> 新建
          </Button>,
        ]}
        request={blogpost}
        columns={columns}
        rowSelection={{
          onChange: (_, selectedRows) => {
            setSelectedRows(selectedRows);
          },
        }}
      />
      {selectedRowsState?.length > 0 && (
        <FooterToolbar
          extra={
            <div>
              已选择
              <a style={{ fontWeight: 600 }}>{selectedRowsState.length}</a> 项
            </div>
          }
        >
          <Button
            onClick={async () => {
              handleRemoveSubmit(
                selectedRowsState.map((row) => row.id!),
                true,
              );
            }}
          >
            批量删除
          </Button>
        </FooterToolbar>
      )}

      <AddOrUpdateBlogPost
        done={done}
        open={visible}
        current={current}
        onDone={handleDone}
        onSubmit={handleAddOrUpdateSubmit}
      />

      <Drawer
        width={600}
        open={showDetail}
        onClose={() => {
          setCurrent(undefined);
          setShowDetail(false);
        }}
        closable={false}
      >
        {current?.title && (
          <ProDescriptions<API.BlogPostListItem>
            column={1}
            title={current?.title}
            request={async () => ({
              data: current || {},
            })}
            params={{
              id: current?.title,
            }}
            columns={columns as ProDescriptionsItemProps<API.BlogPostListItem>[]}
          />
        )}
      </Drawer>
    </PageContainer>
  );
};

export default BlogPostTableList;
