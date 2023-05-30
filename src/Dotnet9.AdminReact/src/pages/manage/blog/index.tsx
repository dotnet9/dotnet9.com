import { Button, Card, Form, Notification, Select, Table, Tooltip } from '@douyinfe/semi-ui';
import { IconHelpCircle } from '@douyinfe/semi-icons';
import React, { Component } from 'react'
import './index.css'
import { TabService } from '../../../services/tabService';
import { CategoryDto, TabDto } from '../../../models/blogger';
import { CategoryService } from '../../../services/categoryService';
import { ArticleService } from '../../../services/articleService';

export default class ManageBlog extends Component {

    state = {
        tabs: [] as TabDto[],
        categorys: [] as CategoryDto[],
        input: {
            keyword: '',
            categoryId: null,
            tabIds: '',
            page: 1,
            pageSize: 10
        },
        data: {
            total: 0,
            result: []
        }
    }

    loadTabs() {
        TabService.getTabs()
            .then(res => {
                this.setState({
                    tabs: res
                })
            })
    }

    loadCategorys() {
        CategoryService
            .getList()
            .then(res => {
                this.setState({
                    categorys: res
                })
            })
    }

    loadBlogs() {
        var { input } = this.state;
        ArticleService.getList(input.keyword, input.categoryId, input.tabIds, input.page, input.pageSize)
            .then(res => {
                this.setState({
                    data: res
                })
            })
    }

    componentDidMount(): void {
        this.loadTabs();
        this.loadCategorys();
        this.loadBlogs();
    }

    handleSubmit(values: any) {
        this.setState({
            input: {
                ...this.state.input,
                ...values
            }
        }, () => this.loadBlogs())
    }

    onDelete(value: string) {
        ArticleService.delete(value)
            .then(res => {
                Notification.success({
                    title: '删除成功',
                    content: '删除成功'
                });
                this.loadBlogs();
            })
    }

    render() {
        var { tabs, categorys, data, input } = this.state;

        let pagination = {
            currentPage: input.page,
            pageSize: input.pageSize,
            total: data.total,
        }

        const columns = [
            {
                title: '标题',
                dataIndex: 'title',
            },
            {
                title: '发布时间',
                dataIndex: 'publishTime',
            },
            {
                title: '所有者',
                dataIndex: 'UserName',
            },
            {
                title: '阅读量',
                dataIndex: 'readCount',
            },
            {
                title: '点赞量',
                dataIndex: 'like',
            },
            {
                title: '分类',
                dataIndex: 'categoryName',
            },
            {
                title: '',
                dataIndex: '操作',
                render: (v: any, data: any) => {
                    return <div>
                        <Button type='danger' onClick={() => this.onDelete(data.id)}>删除</Button>
                    </div>;
                },
            },
        ];

        const scroll = {
            y: 600
        };

        return (
            <>
                <Card className='search-layout'>
                    <Form onSubmit={values => this.handleSubmit(values)} labelPosition='left' layout='horizontal' onValueChange={values => console.log(values)}>
                        {({ formState, values, formApi }) => (
                            <>
                                <Form.Input initValue={input.keyword} onChange={(e) => this.setState({
                                    input: {
                                        ...input,
                                        keyword: e
                                    }
                                })} field='keyword' label='关键字' style={{ width: 150 }} />
                                <Form.Select field="categoryId" label={{ text: '博客分类', optional: true }} style={{ width: 176 }} >
                                    {categorys.map(x => {
                                        return (<Select.Option value={x.id}>{x.name}</Select.Option>)
                                    })}
                                    <Select.Option value={''}>全部</Select.Option>
                                </Form.Select>
                                <Form.Select field='tabIds'  label={{ text: '标签', optional: true }} style={{ width: '320px' }} >
                                    {tabs.map(x => {
                                        return (<Select.Option value={x.name}>{x.name}</Select.Option>)
                                    })}
                                    <Select.Option value={''}>全部</Select.Option>
                                </Form.Select>
                                <Button type="primary" htmlType="submit" className="btn-margin-right">搜索</Button>
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
                        }, () => this.loadBlogs());
                    }} />
                </Card>
            </>
        )
    }
}
