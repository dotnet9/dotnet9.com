import { Avatar, Button, Card, Col, Notification, Divider, Layout, Row, Space, Tag, Icon } from '@douyinfe/semi-ui'
import { Component } from 'react'
import { Link } from 'react-router-dom';
import { ArticleDto } from '../../models/blogger';
import { ArticleService } from '../../services/articleService';
import { TagColor } from '@douyinfe/semi-ui/lib/es/tag';
import ReactMarkdown from 'react-markdown'
import { Prism as SyntaxHighlighter } from 'react-syntax-highlighter'
import { dark } from 'react-syntax-highlighter/dist/esm/styles/prism'
import { IconCopy } from "@douyinfe/semi-icons";
import './index.css'
import Ranking from '../../componses/ranking';
import remarkMath from 'remark-math'
import rehypeKatex from 'rehype-katex'
import toc from '@jsdevtools/rehype-toc';
import rehypeSlug from 'rehype-slug'
import remarkGfm from 'remark-gfm'
import { Praise } from '../../utils/iconUtil';

var id = new URLSearchParams(window.location.search).get("id");

const { Header, Footer, Content } = Layout;

export default class Blog extends Component {
    state = {
        data: {

        } as ArticleDto
    }

    load() {
        ArticleService.get(id!)
            .then(x => {
                this.setState({
                    data: x
                })
            })
    }

    componentDidMount(): void {
        this.load()
    }

    onCopy(value: string) {
        if (value) {
            // 创建一个新的Clipboard对象
            var clipboard = navigator.clipboard;

            clipboard.writeText(value);

            Notification.open({
                content: "内容已复制",
                duration: 3,
            });
        }
    }

    onLike() {
        const { data } = this.state;
        ArticleService.like(data.id)
            .then(res => {
                Notification.success({ title: '点赞成功' })
                data.like++;
                this.setState({ data: data })
            }).catch(error => {
                Notification.error({ title: error.response.data.message })
            })
    }

    render() {
        var { data } = this.state;

        var colors = ['amber', 'blue', 'cyan', 'green', 'grey', 'indigo',
            'light-blue', 'light-green', 'lime', 'orange', 'pink',
            'purple', 'red', 'teal', 'violet', 'yellow', 'white'
        ] as TagColor[];

        let tab;
        if (data.tabs) {
            var tabs = JSON.parse(data.tabs) as [];
            tab = tabs.map(x => {
                return (<Tag color={colors[Math.floor(Math.random() * colors.length)]} key={x}>{x} </Tag>)
            })

        }

        let This = this;

        return (
            <Layout style={{ height: "100%" }}>
                <Header className='blog-header'>
                    <Card>
                        <Link to={'/'} className='header-item'>首页</Link>
                        <a onClick={() => window.open('https://github.com/239573049/TokenBlog')} className='header-item'>GitHub</a>
                        <Link to={'/about'} className='header-item'>关于</Link>
                    </Card>
                </Header>
                <Content>
                    <div
                        className="content"
                        style={{
                            padding: "5px",
                            height: '100%',
                            maxHeight: 'max-content',
                            backgroundColor: "var(--semi-color-bg-0)",
                        }}
                    >
                        <div className='blog-content'>
                            <Row style={{ height: '100%' }}>

                                <Col span={4} style={{ height: '100%' }} className='blog-info'>
                                    <Card style={{ height: '100%' }}>
                                        <div style={{ height: '60px' }}>
                                            <Avatar src='https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg' style={{ backgroundColor: '#87d068', margin: 4 }} alt='Token'>T</Avatar>
                                            <span>Token</span>
                                        </div>
                                        <Divider margin='12px' />
                                        <span>
                                            <Button theme='solid' type='primary' icon={<Icon svg={Praise()} />} onClick={() => this.onLike()}>{data.like} 点赞</Button>
                                        </span>
                                    </Card>
                                </Col>
                                <Col span={16} className='blog-render'>
                                    <div style={{ marginLeft: '5px', marginRight: '5px' }}>
                                        <h1>{data.title}</h1>
                                    </div>
                                    <div style={{ margin: '10px', overflow: 'auto', maxHeight: 'calc(100vh - 150px)', width: "100%" }}>
                                        <ReactMarkdown
                                            children={data.content}

                                            remarkPlugins={[remarkMath]}
                                            transformLinkUri={(href, children, title) => {
                                                if (href === '#') {
                                                    return href + (children.find(x => true) as any).value;
                                                }
                                                return href;
                                            }}
                                            rehypePlugins={[rehypeKatex, rehypeSlug, remarkGfm, toc]}
                                            className='blog-markdown'
                                            components={{
                                                code({ node, inline, className, children, ...props }) {
                                                    const match = /language-(\w+)/.exec(className || '')
                                                    return !inline && match ? (
                                                        <div style={{ position: "relative" }}>
                                                            <Button
                                                                onClick={() => This.onCopy(children[0] as string)}
                                                                className="copy"
                                                                theme="borderless"
                                                                type="primary"
                                                                icon={<IconCopy />}
                                                            />
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
                                    </div>

                                </Col>
                                <Col span={4} style={{ height: '100%' }} className='blog-ranking'>
                                    <Card style={{ height: '100%' }}>
                                        <div style={{ margin: '5px' }}>
                                            <Ranking />
                                        </div>
                                        <Divider margin='12px' />
                                        <span>标签</span>
                                        <Divider margin='12px' />
                                        <Space wrap>
                                            {tab}
                                        </Space>
                                    </Card>
                                </Col>
                            </Row>
                        </div>
                    </div>
                </Content>
                <Footer>

                </Footer>
            </Layout>
        )
    }
}
