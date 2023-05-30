import React, { Component } from 'react'
import axios from 'axios';
import 'react-markdown-editor-lite/lib/index.css';
import MdEditor from 'react-markdown-editor-lite';
import { Prism as SyntaxHighlighter } from 'react-syntax-highlighter'
import { dark } from 'react-syntax-highlighter/dist/esm/styles/prism'
import remarkMath from 'remark-math'
import rehypeKatex from 'rehype-katex'
import remarkGfm from 'remark-gfm'
import ReactMarkdown from 'react-markdown'
import { Button, Card, Divider, Form, Notification, Select } from '@douyinfe/semi-ui';
import { FileService } from '../../../services/fileService';
import input from '@douyinfe/semi-ui/lib/es/input';
import tabs from '@douyinfe/semi-ui/lib/es/tabs';
import { TabDto, CategoryDto } from '../../../models/blogger';
import { CategoryService } from '../../../services/categoryService';
import { TabService } from '../../../services/tabService';
import { ArticleService } from '../../../services/articleService';

export default class PushBlog extends Component {

    state = {
        text: '',
        input: {
            keyword: '',
            categoryId: null,
            tabIds: ''
        },
        tabs: [] as TabDto[],
        categorys: [] as CategoryDto[],
        editorRef: React.createRef<any>()
    }
    async onUploadImg(file: File) {
        return new Promise((rev, rej) => {
            const form = new FormData();
            form.append('file', file);
            FileService.UploadImage(form)
                .then(res => { rev(res) })
                .catch(error => { rej(error) })
        });
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

    componentDidMount() {
        this.loadTabs()
        this.loadCategorys()
    }

    handleSubmit(values: any): void {
        const { text } = this.state;
        console.log(text, values);
        ArticleService.Create({
            title: values.title,
            content: text,
            categoryId: values.categoryId,
            tabs: JSON.stringify(values.tabIds),
        }).then(res => {
            Notification.success({
                title: '成功',
                content: '创建成功'
            })
        })
    }

    render() {
        const { text, tabs, categorys, input } = this.state
        return (
            <div>

                <Card className='search-layout'>
                    <Form onSubmit={values => this.handleSubmit(values)} labelPosition='left' layout='horizontal' onValueChange={values => console.log(values)}>
                        <Form.Input initValue={input.keyword} onChange={(e) => this.setState({
                            input: {
                                ...input,
                                keyword: e
                            }
                        })} field='title' label='标题' style={{ width: 150 }} />
                        <Form.Select field="categoryId" label={{ text: '博客分类' }} style={{ width: 176 }} >
                            {categorys.map(x => {
                                return (<Select.Option value={x.id}>{x.name}</Select.Option>)
                            })}
                            <Select.Option value={''}>全部</Select.Option>
                        </Form.Select>
                        <Form.Select multiple field='tabIds' label={{ text: '标签' }} style={{ width: '320px' }} >
                            {tabs.map(x => {
                                return (<Select.Option value={x.name}>{x.name}</Select.Option>)
                            })}
                            <Select.Option value={''}>全部</Select.Option>
                        </Form.Select>
                        <Button type="primary" htmlType="submit" className="btn-margin-right">新增</Button>
                    </Form>
                </Card>
                <Divider ></Divider>
                <Card>
                    <MdEditor
                        style={{ height: '500px' }}
                        value={text}
                        onChange={(e) => this.setState({ text: e.text })}
                        onImageUpload={(file: any) => this.onUploadImg(file)}
                        renderHTML={(value) => <ReactMarkdown
                            children={value}
                            remarkPlugins={[remarkMath]}
                            rehypePlugins={[rehypeKatex, remarkGfm]}
                            className='blog-markdown'
                            components={{
                                code({ node, inline, className, children, ...props }) {
                                    const match = /language-(\w+)/.exec(className || '')
                                    return !inline && match ? (
                                        <div style={{ position: "relative" }}>
                                            <SyntaxHighlighter
                                                {...props}
                                                children={String(children)}
                                                className="hljs"
                                                style={dark}
                                                language={!inline && match ? match[1] : 'text'}
                                                PreTag="div"
                                            />
                                        </div>
                                    ) : (
                                        <code {...props} className={className}>
                                            {children}
                                        </code>
                                    )
                                }
                            }}
                        />
                        } />
                </Card>
            </div>
        )
    }
}
