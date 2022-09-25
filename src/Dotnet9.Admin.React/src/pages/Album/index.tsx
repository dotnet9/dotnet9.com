import {
  addAlbum,
  removeAlbum,
  album,
  updateAlbum,
  changeAlbumVisible,
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
import AddOrUpdateAlbum from './components/AddOrUpdateAlbum';

const handleAdd = async (fields: API.AlbumListItem) => {
  const hide = message.loading('正在添加');
  try {
    await addAlbum({ ...fields });
    hide();
    message.success('添加成功');
    return true;
  } catch (error) {
    hide();
    message.error('添加失败，请重试！');
    return false;
  }
};

const handleUpdate = async (fields: API.AlbumListItem) => {
  const hide = message.loading('正在更新');
  try {
    await updateAlbum({ ...fields });
    hide();
    message.success('更新成功');
    return true;
  } catch (error) {
    hide();
    message.error('更新失败，请重试！');
    return false;
  }
};

const handleChangeVisible = async (id?: string, albumVisible?: boolean) => {
  const hide = message.loading('正在更新');
  try {
    await changeAlbumVisible({ id, visible: albumVisible });
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
    await removeAlbum(data);
    hide();
    message.success('删除成功，即将刷新');
    return true;
  } catch (error) {
    hide();
    message.error('删除失败，请重试');
    return false;
  }
};

const AlbumTableList: React.FC = () => {
  const [done, setDone] = useState<boolean>(false);
  const [visible, setVisible] = useState<boolean>(false);
  const [current, setCurrent] = useState<API.AlbumListItem | undefined>(undefined);
  const [showDetail, setShowDetail] = useState<boolean>(false);
  const actionRef = useRef<ActionType>();
  const [selectedRowsState, setSelectedRows] = useState<API.AlbumListItem[]>([]);

  const handleDone = () => {
    setDone(false);
    setVisible(false);
    setCurrent(undefined);
  };

  const handleAddOrUpdateSubmit = async (values: API.AlbumListItem) => {
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
      title: '删除专辑',
      content: '确定删除该专辑吗？删除专辑不会删除专辑下的文章',
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

  const handleChangeVisibleCommit = (albumVisible: boolean, record: API.AlbumListItem) => {
    handleChangeVisible(record.id, albumVisible);
  };

  const columns: ProColumns<API.AlbumListItem>[] = [
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
      title: '封面',
      dataIndex: 'cover',
      width: 150,
      valueType: 'image',
      hideInSearch: true,
    },
    {
      title: '专辑名称',
      dataIndex: 'name',
      hideInSearch: true,
    },
    {
      title: '显示序号',
      hideInSearch: true,
      dataIndex: 'sequenceNumber',
    },
    {
      title: '所属分类',
      dataIndex: 'categoryNames',
      hideInSearch: true,
      render: (_, record) => <span>{record.categoryNames?.join(',')}</span>,
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
      <ProTable<API.AlbumListItem, API.PageParams>
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
        request={album}
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

      <AddOrUpdateAlbum
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
          <ProDescriptions<API.AlbumListItem>
            column={1}
            title={current?.name}
            request={async () => ({
              data: current || {},
            })}
            params={{
              id: current?.name,
            }}
            columns={columns as ProDescriptionsItemProps<API.AlbumListItem>[]}
          />
        )}
      </Drawer>
    </PageContainer>
  );
};

export default AlbumTableList;
