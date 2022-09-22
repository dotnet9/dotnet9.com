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
import moment from 'moment';

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

const ActionLogTableList: React.FC = () => {
  const [current, setCurrent] = useState<API.ActionLogListItem | undefined>(undefined);
  const [showDetail, setShowDetail] = useState<boolean>(false);
  const actionRef = useRef<ActionType>();
  const [selectedRowsState, setSelectedRows] = useState<API.ActionLogListItem[]>([]);

  const handleRemoveSubmit = async (ids: string[], isBatch: boolean) => {
    Modal.confirm({
      title: '删除操作日志',
      content: '确定删除该操作日志吗？',
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
      title: 'id',
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
      sorter: true,
    },
    {
      title: '操作系统',
      dataIndex: 'os',
      sorter: true,
    },
    {
      title: '浏览器',
      dataIndex: 'browser',
      sorter: true,
    },
    {
      title: '主机地址',
      dataIndex: 'ip',
      sorter: true,
    },
    {
      title: '访问来源',
      dataIndex: 'referer',
      sorter: true,
    },
    {
      title: 'AccessName',
      dataIndex: 'accessName',
      hideInSearch: true,
      sorter: true,
    },
    {
      title: 'Original',
      dataIndex: 'original',
      sorter: true,
    },
    {
      title: '请求Url',
      dataIndex: 'url',
      ellipsis: true,
      sorter: true,
    },
    {
      title: '控制器',
      dataIndex: 'controller',
      ellipsis: true,
      sorter: true,
    },
    {
      title: '接口',
      dataIndex: 'action',
      ellipsis: true,
      sorter: true,
    },
    {
      title: '请求方式',
      dataIndex: 'method',
      sorter: true,
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
      sorter: true,
    },
    {
      title: '周期(ms)',
      dataIndex: 'duration',
      hideInSearch: true,
      sorter: true,
    },
    {
      title: '创建时间',
      dataIndex: 'creationTime',
      valueType: 'dateTimeRange',
      sorter: true,
      initialValue: [
        moment().add(-1, 'day').format('YYYY-MM-DD HH:mm:ss'),
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
          key="showDetail"
          onClick={() => {
            setCurrent(record);
            setShowDetail(true);
          }}
        >
          查看
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
      <ProTable<API.ActionLogListItem, API.PageParams>
        headerTitle="查询表格"
        actionRef={actionRef}
        rowKey="id"
        search={{
          labelWidth: 120,
        }}
        request={async (params, sort) => {
          const res: any = await actionLog(params, sort);
          return res;
        }}
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
        {current?.id && (
          <ProDescriptions<API.ActionLogListItem>
            column={1}
            title={current?.ip}
            request={async () => ({
              data: current || {},
            })}
            params={{
              id: current?.id,
            }}
            columns={columns as ProDescriptionsItemProps<API.ActionLogListItem>[]}
          />
        )}
      </Drawer>
    </PageContainer>
  );
};

export default ActionLogTableList;
