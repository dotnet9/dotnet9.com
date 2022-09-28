import { albumTree, categoryTree, tagTree } from '@/services/ant-design-pro/api';
import {
  ProFormText,
  ProFormDigit,
  ProFormTextArea,
  ProFormRadio,
  ProForm,
  ProFormTreeSelect,
} from '@ant-design/pro-components';

import { PageContainer } from '@ant-design/pro-layout';
import { Card, Col, Row, message, TreeSelect } from 'antd';
import React, { useEffect, useState } from 'react';
import MarkdownIt from 'markdown-it';
import MdEditor from 'react-markdown-editor-lite';
import 'react-markdown-editor-lite/lib/index.css';

const mdParser = new MarkdownIt({
  html: true,
  linkify: false,
  typographer: true,
});

const AddOrUpdateBlogPost: React.FC<Record<string, any>> = () => {
  const [albumTreeItems, setAlbumTreeItems] = useState<API.AlbumTreeItem[]>([]);
  const [categoryTreeItems, setCategoryTreeItems] = useState<API.CategoryTreeItem[]>([]);
  const [tagTreeItems, setTagTreeItems] = useState<API.TagTreeItem[]>([]);
  const [markdownValue, setMarkdownValue] = useState<string>('');

  function handleEditorChange({ text }: { html: string; text: string }) {
    setMarkdownValue(text);
  }

  const handleRead = async () => {
    const hide = message.loading('正在读取');
    try {
      const albumTreeResult = await albumTree();
      setAlbumTreeItems(albumTreeResult.data);

      const categoryTreeResult = await categoryTree();
      setCategoryTreeItems(categoryTreeResult.data);

      const tagTreeResult = await tagTree();
      setTagTreeItems(tagTreeResult.data);

      hide();
      message.success('读取成功');
      return true;
    } catch (e) {
      hide();
      message.error('读取失败');
      return false;
    }
  };

  const onFinish = async (values: Record<string, any>) => {
    try {
      console.log(values);
      message.success('提交成功');
    } catch {
      // console.log
    }
  };

  const onFinishFailed = () => {};

  useEffect(() => {
    handleRead();
  }, []);
  if (!open) {
    return null;
  }

  return (
    <ProForm layout="vertical" requiredMark onFinish={onFinish} onFinishFailed={onFinishFailed}>
      <PageContainer content="文章编辑">
        <Card bordered={false}>
          <ProFormText name="id" label="id" hidden />
          <Row gutter={16}>
            <Col lg={24} md={24} sm={24}>
              <ProFormText
                name="title"
                label="标题"
                placeholder="请输入2-256个字符"
                rules={[
                  { required: true, message: '请输入链接，长度为2-256个字符', min: 2, max: 256 },
                ]}
                fieldProps={{ style: { width: '100%' } }}
              />
            </Col>
          </Row>
          <Row gutter={16}>
            <Col lg={24} md={24} sm={24}>
              <ProForm.Item
                label="内容"
                name="content"
                shouldUpdate
                rules={[
                  {
                    required: true,
                    message: '请输入文章内容，长度为10-2560个字符',
                    min: 10,
                    max: 2560,
                  },
                ]}
              >
                <MdEditor
                  value={markdownValue}
                  style={{ height: '800px' }}
                  renderHTML={(text) => mdParser.render(text)}
                  onChange={handleEditorChange}
                />
              </ProForm.Item>
            </Col>
          </Row>
          <Row gutter={16}>
            <Col lg={24} md={24} sm={24}>
              <ProFormText
                name="slug"
                label="别名"
                placeholder="请输入2-256个字符"
                rules={[
                  { required: true, message: '请输入别名，长度为2-256个字符', min: 2, max: 256 },
                ]}
                fieldProps={{ style: { width: '100%' } }}
              />
            </Col>
          </Row>
          <Row gutter={16}>
            <Col lg={24} md={24} sm={24}>
              <ProFormText
                name="cover"
                label="封面"
                placeholder="请输入2-256个字符"
                rules={[
                  { required: true, message: '请输入别名，长度为2-256个字符', min: 2, max: 256 },
                ]}
                fieldProps={{ style: { width: '100%' } }}
              />
            </Col>
          </Row>
          <Row gutter={16}>
            <Col lg={24} md={24} sm={24}>
              <ProFormTextArea
                name="description"
                label="简述"
                placeholder="请输入2-256个字符"
                rules={[
                  { required: true, message: '请输入别名，长度为2-256个字符', min: 2, max: 256 },
                ]}
                fieldProps={{ style: { width: '100%' } }}
              />
            </Col>
          </Row>        
          <ProFormTreeSelect
            name="albumIds"
            key={'albumIds'}
            label="所属专辑"
            request={async () => albumTreeItems}
            fieldProps={{
              fieldNames: {
                label: 'title',
                key: 'key',
                value: 'value',
              },
              treeCheckable: true,
              showCheckedStrategy: TreeSelect.SHOW_PARENT,
              placeholder: '请选择',
            }}
          />
          <ProFormTreeSelect
            name="categoryIds"
            key={'categoryIds'}
            label="所属分类"
            request={async () => categoryTreeItems}
            fieldProps={{
              fieldNames: {
                label: 'title',
                key: 'key',
                value: 'value',
              },
              treeCheckable: true,
              showCheckedStrategy: TreeSelect.SHOW_PARENT,
              placeholder: '请选择',
            }}
          />
          <ProFormTreeSelect
            name="tagIds"
            key={'tagIds'}
            label="所属标签"
            request={async () => tagTreeItems}
            fieldProps={{
              fieldNames: {
                label: 'title',
                key: 'key',
                value: 'value',
              },
              treeCheckable: true,
              showCheckedStrategy: TreeSelect.SHOW_PARENT,
              placeholder: '请选择',
            }}
          />
          <Row gutter={16}>
            <Col lg={6} md={12} sm={24}>
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
                  style: { width: '100%' },
                }}
              />
            </Col>
            <Col xl={{ span: 6, offset: 2 }} lg={{ span: 8 }} md={{ span: 12 }} sm={24}>
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
              />
            </Col>
            <Col xl={{ span: 8, offset: 2 }} lg={{ span: 10 }} md={{ span: 24 }} sm={24}>
              <ProFormRadio.Group
                name="copyRightType"
                label="版权"
                required
                options={[
                  {
                    value: 'default',
                    label: '原创',
                  },
                  {
                    value: 'reprint',
                    label: '转载',
                  },
                  {
                    value: 'reprint',
                    label: '投稿',
                  },
                ]}
              />
            </Col>
          </Row>
          <Row gutter={16}>
            <Col lg={6} md={12} sm={24}>
              <ProFormText
                name="original"
                label="来源"
                placeholder="请输入2-256个字符"
                rules={[
                  { required: true, message: '请输入来源，长度为2-256个字符', min: 2, max: 256 },
                ]}
                fieldProps={{ style: { width: '100%' } }}
              />
            </Col>
            <Col xl={{ span: 6, offset: 2 }} lg={{ span: 8 }} md={{ span: 12 }} sm={24}>
              <ProFormText
                name="originalAvatar"
                label="来源头像"
                placeholder="请输入2-256个字符"
                rules={[
                  { required: true, message: '请输入别名，长度为2-256个字符', min: 2, max: 256 },
                ]}
                fieldProps={{ style: { width: '100%' } }}
              />
            </Col>
            <Col xl={{ span: 8, offset: 2 }} lg={{ span: 10 }} md={{ span: 24 }} sm={24}>
              <ProFormText
                name="originalTitle"
                label="来源标题"
                placeholder="请输入2-256个字符"
                rules={[
                  { required: true, message: '请输入别名，长度为2-256个字符', min: 2, max: 256 },
                ]}
                fieldProps={{ style: { width: '100%' } }}
              />
            </Col>
          </Row>
          <ProFormText
            name="originalLink"
            label="来源链接"
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
        </Card>
      </PageContainer>
    </ProForm>
  );
};

export default AddOrUpdateBlogPost;
