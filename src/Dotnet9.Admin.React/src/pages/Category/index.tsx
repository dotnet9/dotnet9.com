import {
  addCategory,
  removeCategory,
  category,
  updateCategory,
  changeCategoryVisible,
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
import AddOrUpdateCategory from './components/AddOrUpdateCategory';

const handleAdd = async (fields: API.CategoryListItem) => {
  const hide = message.loading('正在添加');
  try {
    await addCategory({ ...fields });
    hide();
    message.success('添加成功');
    return true;
  } catch (error) {
    hide();
    message.error('添加失败，请重试！');
    return false;
  }
};

const handleUpdate = async (fields: API.CategoryListItem) => {
  const hide = message.loading('正在更新');
  try {
    await updateCategory({ ...fields });
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
    await changeCategoryVisible({ id, visible: categoryVisible });
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
    await removeCategory(data);
    hide();
    message.success('删除成功，即将刷新');
    return true;
  } catch (error) {
    hide();
    message.error('删除失败，请重试');
    return false;
  }
};

const CategoryTableList: React.FC = () => {
  const [done, setDone] = useState<boolean>(false);
  const [visible, setVisible] = useState<boolean>(false);
  const [current, setCurrent] = useState<API.CategoryListItem | undefined>(undefined);
  const [showDetail, setShowDetail] = useState<boolean>(false);
  const actionRef = useRef<ActionType>();
  const [selectedRowsState, setSelectedRows] = useState<API.CategoryListItem[]>([]);

  const handleDone = () => {
    setDone(false);
    setVisible(false);
    setCurrent(undefined);
  };

  const handleAddOrUpdateSubmit = async (values: API.CategoryListItem) => {
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
      title: '删除分类',
      content: '确定删除该分类吗？删除分类不会删除分类下的文章',
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

  const handleChangeVisibleCommit = (categoryVisible: boolean, record: API.CategoryListItem) => {
    handleChangeVisible(record.id, categoryVisible);
  };

  const columns: ProColumns<API.CategoryListItem>[] = [
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
      title: '分类名称',
      dataIndex: 'name',
      hideInSearch: true,
    },
    {
      title: '显示序号',
      hideInSearch: true,
      dataIndex: 'sequenceNumber',
    },
    {
      title: '父级分类',
      dataIndex: 'parentId',
      hideInSearch: true,
      hideInTable: true,
      hideInDescriptions: true,
    },
    {
      title: '父级分类',
      dataIndex: 'parentName',
      hideInSearch: true,
    },
    {
      title: '别名',
      hideInSearch: true,
      dataIndex: 'slug',
    },
    {
      title: '描述',
      dataIndex: 'description',
      valueType: 'textarea',
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
      <ProTable<API.CategoryListItem, API.PageParams>
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
        request={category}
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

      <AddOrUpdateCategory
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
        {current?.name && (
          <ProDescriptions<API.CategoryListItem>
            column={1}
            title={current?.name}
            request={async () => ({
              data: current || {},
            })}
            params={{
              id: current?.name,
            }}
            columns={columns as ProDescriptionsItemProps<API.CategoryListItem>[]}
          />
        )}
      </Drawer>
    </PageContainer>
  );
};

export default CategoryTableList;
