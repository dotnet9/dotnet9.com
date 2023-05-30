import { Button, Card, Form, Popover, Space, Table } from '@douyinfe/semi-ui';
import { IconMore } from '@douyinfe/semi-icons';
import  { Component } from 'react'
import { ResourceDto } from '../../../models/blogger';
import { ResourceService } from '../../../services/resourceService';

import './index.css'
import Add from './add';

export default class ResourceListManage extends Component {

  state = {
    input: {
      keyword: '',
      page: 1,
      pageSize: 20
    },
    visible:false,
    data: {
      total: 0,
      result: [] as ResourceDto[]
    }
  }

  onDelete(id: number) {
    console.log(id)
  }

  onEdit(id: number) {
    console.log(id)
  }

  handleSubmit(values: any) {
    console.log(values)
  }

  load() {
    const { input } = this.state;
    ResourceService.GetList(input.keyword, input.page, input.pageSize)
      .then(res => {
        this.setState({
          data: res
        })
      })
  }

  componentDidMount(): void {
    this.load();
  }

  render() {

    const columns = [
      {
        title: '标题',
        dataIndex: 'title',
      },
      {
        title: '描述',
        dataIndex: 'description',
      },
      {
        title: '下载次数',
        dataIndex: 'downloadCount',
      },
      {
        title: '推荐',
        dataIndex: 'referee',
      },
      {
        title: '分享作者',
        dataIndex: 'userName',
      },
      {
        title: '作者外链',
        dataIndex: 'href',
      },
      {
        title: '',
        dataIndex: '操作',
        render: (v: any, data: any) => {
          return <Popover
            style={{
              backgroundColor: 'rgba(var(--semi-blue-4),1)',
              borderColor: 'rgba(var(--semi-blue-4),1)',
              color: 'var(--semi-color-white)',
              borderWidth: 1,
              borderStyle: 'solid',
            }}
            showArrow content={<div>
              <Button type='primary' theme='borderless' onClick={() => this.onEdit(data.id)}>编辑</Button>
              <Button type='danger' theme='borderless' onClick={() => this.onDelete(data.id)}>删除</Button>
            </div>}>
            <IconMore />
          </Popover>;
        },
      },
    ];
    const scroll = {
      y: 600
    };

    const { input, data,visible } = this.state;

    let pagination = {
      currentPage: input.page,
      pageSize: input.pageSize,
      total: data.total,
    }

    return (
      <>
        <Card className='search-layout'>
          <Form onSubmit={values => this.handleSubmit(values)} labelPosition='left' layout='horizontal' onValueChange={values => console.log(values)}>
            {({ formState, values, formApi }) => (
              <>
                <Space>
                  <Form.Input initValue={input.keyword} onChange={(e) => this.setState({
                    input: {
                      ...input,
                      keyword: e
                    }
                  })} field='keyword' label='关键字' style={{ width: 150 }} />
                  <Button type="primary" htmlType="submit" className="btn-margin-right">搜索</Button>
                  <Button type="primary" className="btn-margin-right" onClick={()=>this.setState({
                    visible:true
                  })}>新增</Button>
                </Space>
              </>
            )}
          </Form>
        </Card>
        <Card className='list-layout'>
          <Table scroll={scroll} columns={columns} dataSource={data.result} pagination={pagination} onChange={(info) => {
            this.setState({
              input: {
                ...input,
                page: info.pagination?.currentPage,
                pageSize: info.pagination?.pageSize
              }
            }, () => this.load());
          }} />
        </Card>
        <Add visible={visible} change={()=>this.setState({
          visible:false
        })}/>
      </>
    )
  }
}
