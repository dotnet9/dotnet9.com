import {
  ModalForm,
  ProFormText,
  ProFormTextArea,
  ProFormDatePicker,
} from '@ant-design/pro-components';
import { Button, Result } from 'antd';
import React from 'react';

export type AddOrUpdateTimelineProps = {
  done: boolean;
  open: boolean;
  current: Partial<API.TimelineListItem> | undefined;
  onDone: () => void;
  onSubmit: (values: API.TimelineListItem) => Promise<void>;
};

const AddOrUpdateTimeline: React.FC<AddOrUpdateTimelineProps> = (props) => {
  const { done, open, current, onDone, onSubmit, children } = props;
  if (!open) {
    return null;
  }
  return (
    <ModalForm<API.TimelineListItem>
      open={open}
      title={done ? null : `时间线${current ? '编辑' : `添加`}`}
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
          <ProFormDatePicker
            name="time"
            label="时间"
            placeholder="请选择时间"
            width="md"
            rules={[
              {
                required: true,
                message: '请输入选择时间',
              },
            ]}
          />
          <ProFormText
            name="title"
            label="标题"
            placeholder="请输入5-64个字符"
            width="md"
            rules={[
              {
                required: true,
                message: '请输入链接，长度为5-64个字符',
                min: 5,
                max: 64,
              },
            ]}
          />
          <ProFormTextArea
            name="content"
            width="md"
            label="描述"
            placeholder="请输入不多于256个字符"
            rules={[
              {
                required: true,
                message: '请输入不多于256个字符的描述！',
                min: 5,
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

export default AddOrUpdateTimeline;
