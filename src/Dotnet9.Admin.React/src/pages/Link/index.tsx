import { addLink, removeLink, link, updateLink } from '@/services/ant-design-pro/api';
import { PlusOutlined } from '@ant-design/icons';
import type { ActionType, ProColumns, ProDescriptionsItemProps } from '@ant-design/pro-components';
import {
  FooterToolbar,
  PageContainer,
  ProDescriptions,
  ProTable,
} from '@ant-design/pro-components';
import { Button, Drawer, message, Modal } from 'antd';
import React, { useRef, useState } from 'react';
import AddOrUpdateLink from './components/AddOrUpdateLink';

const handleAdd = async (fields: API.LinkListItem) => {
  const hide = message.loading('正在添加');
  try {
    await addLink({ ...fields });
    hide();
    message.success('添加成功');
    return true;
  } catch (error) {
    hide();
    message.error('添加失败，请重试！');
    return false;
  }
};

const handleUpdate = async (fields: API.LinkListItem) => {
  const hide = message.loading('正在更新');
  try {
    await updateLink({ ...fields });
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
    await removeLink(data);
    hide();
    message.success('删除成功，即将刷新');
    return true;
  } catch (error) {
    hide();
    message.error('删除失败，请重试');
    return false;
  }
};

const LinkTableList: React.FC = () => {
  const [done, setDone] = useState<boolean>(false);
  const [visible, setVisible] = useState<boolean>(false);
  const [current, setCurrent] = useState<API.LinkListItem | undefined>(undefined);
  const [showDetail, setShowDetail] = useState<boolean>(false);
  const actionRef = useRef<ActionType>();
  const [selectedRowsState, setSelectedRows] = useState<API.LinkListItem[]>([]);

  const handleDone = () => {
    setDone(false);
    setVisible(false);
    setCurrent(undefined);
  };

  const handleAddOrUpdateSubmit = async (values: API.LinkListItem) => {
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
      title: '删除链接',
      content: '确定删除该链接吗？',
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

  const columns: ProColumns<API.LinkListItem>[] = [
    {
      title: 'ID',
      dataIndex: 'id',
      tip: '链接Id是唯一的 key',
      hideInForm: true,
      hideInSearch: true,
      hideInTable: true,
    },
    {
      title: '链接名称',
      dataIndex: 'name',
    },
    {
      title: '显示序号',
      hideInSearch: true,
      dataIndex: 'sequenceNumber',
    },
    {
      title: '链接',
      dataIndex: 'url',
    },
    {
      title: '描述',
      dataIndex: 'description',
      valueType: 'textarea',
    },
    {
      title: '类型',
      dataIndex: 'kind',
      hideInForm: true,
      valueEnum: {
        Private: {
          text: '私密',
          status: 'Default',
        },
        Owner: {
          text: '网站相关',
          status: 'Success',
        },
        Friend: {
          text: '友情链接',
          status: 'Processing',
        },
        Course: {
          text: '课程链接',
          status: 'Error',
        },
      },
    },
    {
      title: '操作',
      dataIndex: 'option',
      valueType: 'option',
      render: (_, record) => [<a
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
      <ProTable<API.LinkListItem, API.PageParams>
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
        request={link}
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

      <AddOrUpdateLink
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
          <ProDescriptions<API.LinkListItem>
            column={1}
            title={current?.name}
            request={async () => ({
              data: current || {},
            })}
            params={{
              id: current?.name,
            }}
            columns={columns as ProDescriptionsItemProps<API.LinkListItem>[]}
          />
        )}
      </Drawer>
    </PageContainer>
  );
};

export default LinkTableList;
