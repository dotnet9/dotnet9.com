import { removeUser, user } from '@/services/ant-design-pro/api';
import { PlusOutlined } from '@ant-design/icons';
import type { ActionType, ProColumns } from '@ant-design/pro-components';
import {
  FooterToolbar,
  PageContainer,
  ProTable,
} from '@ant-design/pro-components';
import { Button, message, Modal } from 'antd';
import React, { useRef, useState } from 'react';
import moment from 'moment';

const handleRemove = async (data: string[]) => {
  const hide = message.loading('正在删除');
  if (!data) return true;
  try {
    await removeUser(data);
    hide();
    message.success('删除成功，即将刷新');
    return true;
  } catch (error) {
    hide();
    message.error('删除失败，请重试');
    return false;
  }
};

const UserTableList: React.FC = () => {
  const actionRef = useRef<ActionType>();
  const [selectedRowsState, setSelectedRows] = useState<API.UserListItem[]>([]);

  const handleRemoveSubmit = async (ids: string[], isBatch: boolean) => {
    Modal.confirm({
      title: '删除用户',
      content: '确定删除该用户吗？',
      okText: '确认',
      cancelText: '取消',
      onOk: async () => {
        const success = await handleRemove(ids);
        if (success) {
          if (isBatch) {
            setSelectedRows([]);
            actionRef.current?.reloadAndRest?.();
          } else {
            if (actionRef.current) {
              actionRef.current.reload();
            }
          }
        }
      },
    });
  };

  const columns: ProColumns<API.UserListItem>[] = [
    {
      dataIndex: 'id',
      tip: '用户Id是唯一的 key',
      hideInForm: true,
      hideInSearch: true,
      hideInTable: true,
    },
    {
      title: '用户名',
      dataIndex: 'userName',
    },
    {
      title: '角色',
      hideInSearch: true,
      dataIndex: 'roleNames',
    },
    {
      title: '手机号码',
      dataIndex: 'phoneNumber',
    },
    {
      title: '创建时间',
      dataIndex: 'creationTime',
      valueType: 'dateTimeRange',
      sorter: true,
      initialValue: [
        moment().add(-1, 'year').format('YYYY-MM-DD HH:mm:ss'),
        moment().format('YYYY-MM-DD HH:mm:ss'),
      ],
      render: (_, record) => record.creationTime,
      search: {
        transform: (value: any) => ({ startCreationTime: value[0], endCreationTime: value[1] }),
      },
    },
    {
      title: '操作',
      dataIndex: 'option',
      valueType: 'option',
      render: (_, record) => [
        <a
          key="edit"
          onClick={() => {
            // TODO 跳转编辑用户页面
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
      <ProTable<API.UserListItem, API.PageParams>
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
              // 跳转新增用户页面
            }}
          >
            <PlusOutlined /> 新建
          </Button>,
        ]}
        request={user}
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
    </PageContainer>
  );
};

export default UserTableList;
