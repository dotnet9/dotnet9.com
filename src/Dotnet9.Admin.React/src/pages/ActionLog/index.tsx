import { removeActionLog, actionLog } from '@/services/ant-design-pro/api';
import type { ActionType, ProColumns, ProDescriptionsItemProps } from '@ant-design/pro-components';
import {
  FooterToolbar,
  PageContainer,
  ProDescriptions,
  ProTable,
} from '@ant-design/pro-components';
import { Button, Drawer, message, Modal } from 'antd';
import React, { useRef, useState } from 'react';

const handleRemove = async (data: string[]) => {
  const hide = message.loading('正在删除');
  if (!data) return true;
  try {
    await removeActionLog(data);
    hide();
    message.success('删除成功，即将刷新');
    return true;
  } catch (error) {
    hide();
    message.error('删除失败，请重试');
    return false;
  }
};

const TableList: React.FC = () => {
  const [current, setCurrent] = useState<API.ActionLogListItem | undefined>(undefined);
  const [showDetail, setShowDetail] = useState<boolean>(false);
  const actionRef = useRef<ActionType>();
  const [selectedRowsState, setSelectedRows] = useState<API.ActionLogListItem[]>([]);

  const handleRemoveSubmit = async (ids: string[], isBatch: boolean) => {
    Modal.confirm({
      title: '删除任务',
      content: '确定删除该任务吗？',
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

  const columns: ProColumns<API.ActionLogListItem>[] = [
    {
      dataIndex: 'id',
      tip: '操作日志Id是唯一的 key',
      hideInForm: true,
      hideInSearch: true,
      hideInTable: true,
      render: (dom, entity) => {
        return (
          <a
            onClick={() => {
              setCurrent(entity);
              setShowDetail(true);
            }}
          >
            {dom}
          </a>
        );
      },
    },
    {
      title: '代理',
      dataIndex: 'ua',
      ellipsis: true,
    },
    {
      title: '操作系统',
      dataIndex: 'os',
    },
    {
      title: '浏览器',
      dataIndex: 'browser',
    },
    {
      title: '主机地址',
      dataIndex: 'ip',
    },
    {
      title: '访问来源',
      dataIndex: 'referer',
    },
    {
      title: 'AccessName',
      dataIndex: 'accessName',
      hideInSearch: true,
    },
    {
      title: 'Original',
      dataIndex: 'original',
    },
    {
      title: '请求Url',
      dataIndex: 'url',
      ellipsis: true,
    },
    {
      title: '控制器',
      dataIndex: 'controller',
      ellipsis: true,
    },
    {
      title: '接口',
      dataIndex: 'action',
      ellipsis: true,
    },
    {
      title: '请求方式',
      dataIndex: 'method',
      valueEnum: {
        GET: {
          text: 'GET',
          status: 'Success',
        },
        POST: {
          text: 'POST',
          status: 'Processing',
        },
        PUT: {
          text: 'PUT',
          status: 'Processing',
        },
        DELETE: {
          text: 'DELETE',
          status: 'Error',
        },
      },
    },
    {
      title: '参数',
      dataIndex: 'arguments',
      ellipsis: true,
    },
    {
      title: '周期(ms)',
      dataIndex: 'duration',
      hideInSearch: true,
    },
    {
      title: '创建时间',
      key: 'showTime',
      dataIndex: 'creationTime',
      valueType: 'date',
      hideInSearch: true,
    },
    {
      title: '创建时间',
      key: 'showTime',
      dataIndex: 'creationTime',
      valueType: 'dateTimeRange',
      hideInTable: true,
      search: {
        transform: (value) => {
          return {
            startTime: value[0],
            endTime: value[1],
          };
        },
      },
    },
    {
      title: '操作',
      dataIndex: 'option',
      valueType: 'option',
      render: (_, record) => [
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
      <ProTable<API.ActionLogListItem, API.PageParams>
        headerTitle="查询表格"
        actionRef={actionRef}
        rowKey="id"
        search={{
          labelWidth: 120,
        }}
        request={actionLog}
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

      <Drawer
        width={600}
        open={showDetail}
        onClose={() => {
          setCurrent(undefined);
          setShowDetail(false);
        }}
        closable={false}
      >
        {current?.uid && (
          <ProDescriptions<API.ActionLogListItem>
            column={2}
            title={current?.uid}
            request={async () => ({
              data: current || {},
            })}
            params={{
              id: current?.uid,
            }}
            columns={columns as ProDescriptionsItemProps<API.ActionLogListItem>[]}
          />
        )}
      </Drawer>
    </PageContainer>
  );
};

export default TableList;
