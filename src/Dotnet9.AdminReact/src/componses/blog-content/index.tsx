import { Component } from 'react'
import './index.css'
import { Row, Col, Card, Avatar, Divider, Collapse, Tag, Space } from '@douyinfe/semi-ui'
import { IconHome, IconPaperclip, IconAppCenter, IconMenu, IconGithubLogo, IconSetting, IconBranch } from '@douyinfe/semi-icons';
import { Outlet, Link } from "react-router-dom";
import { PathEvent } from '../events/pathEvent';
import { TabService } from '../../services/tabService';
import { CategoryDto, TabDto } from '../../models/blogger';
import { TagColor } from '@douyinfe/semi-ui/lib/es/tag';
import { CategoryService } from '../../services/categoryService';
import Ranking from '../ranking';

export default class BlogContent extends Component {

    state = {
        tab: [] as TabDto[],
        category: [
        ] as CategoryDto[]
    }

    getTabs() {
        TabService.getTabs()
            .then(res => {
                this.setState({
                    tab: res
                })
            })
    }

    getCategory() {
        CategoryService.getList()
            .then(res => {
                this.setState({
                    category: res
                })
            })
    }

    componentDidMount(): void {
        this.getTabs();
        this.getCategory();

        const boxes = document.querySelectorAll('.box');

        boxes.forEach((box, index) => {
            box.addEventListener('mouseover', () => {
                box.classList.add('active');
                for (let i = 0; i < index; i++) {
                    boxes[i].classList.add('previous');
                }
                for (let i = index + 1; i < boxes.length; i++) {
                    boxes[i].classList.remove('previous');
                }
            });

            box.addEventListener('mouseout', () => {
                box.classList.remove('active');
                for (let i = 0; i < index; i++) {
                    boxes[i].classList.remove('previous');
                }
            });
        });

    }

    wisdom() {
        const values = ["人生苦短，及时行乐。",
            "知识就是力量。",
            "时间就是金钱。",
            "天下没有免费的午餐。",
            "机会只留给有准备的人。",
            "成功的秘诀在于坚持不懈。",
            "失败是成功之母。",
            "人生没有彩排，每一天都是现场直播。",
            "生命不止，奋斗不息。",
            "人生如戏，全靠演技。",
            "天道酬勤，人道酬善。",
            "行动胜于空谈。",
            "不经历风雨，怎么见彩虹。",
            "成功需要付出代价。",
            "没有人能够让你觉得自卑，除非你自己同意。",
            "人生就像一盒巧克力，你永远不知道下一颗是什么味道。",
            "人生最大的敌人是自己。",
            "人生就是一场修行。",
            "人生没有绝对的公平，只有相对的公平。",
            "我们没有永恒的朋友，也没有永恒的敌人，只有永恒的利益。",
            "人生最重要的是做自己。"]
        // 随机从 values 中取出一个元素
        return values[Math.floor(Math.random() * values.length)];
    }



    render() {
        var pathname = window.location.pathname;
        var { category, tab } = this.state;
        let count = 0;
        category.forEach(x => {
            count += x.count;
        })
        var colors = ['amber', 'blue', 'cyan', 'green', 'grey', 'indigo',
            'light-blue', 'light-green', 'lime', 'orange', 'pink',
            'purple', 'red', 'teal', 'violet', 'yellow', 'white'
        ] as TagColor[];
        return (
            <div className='blog-content'>
                <Row style={{ height: '100%' }}>
                    <Col span={4} style={{ height: '100%' }} className='blog-info'>
                        <Card style={{ height: '100%' }}>
                            <div onClick={() => window.open('https://github.com/239573049')} style={{ height: '100px' }}>
                                <Card
                                    shadows='hover'
                                    bodyStyle={{
                                        display: 'flex',
                                        alignItems: 'center',
                                        justifyContent: 'space-between'
                                    }}>
                                    <Avatar style={{ backgroundColor: '#87d068', margin: 4 }} src='https://avatars.githubusercontent.com/u/61819790?v=4'></Avatar>
                                    <div>
                                        <div style={{ fontSize: '16px', color: '#0159f7', fontStyle: 'oblique', fontFamily: 'fantasy' }}>Token</div>
                                        <div style={{ fontSize: '10px', fontStyle: 'oblique' }}>一个热爱.NET的开发者！</div>
                                    </div>
                                </Card>
                            </div>
                            <span style={{ fontSize: '12px', color: '#98a6ad!important' }}>导航</span>
                            <div style={{ margin: '5px' }}>
                                <Link  className={"menu box " + (pathname === "/" ? "menu-select" : "")} onClick={() => {
                                    this.setState({
                                        pathname: '/'
                                    })
                                    PathEvent.emit('blog-path', {
                                        deleteid: 'true',
                                    })
                                }} to={'/'}>
                                    <IconHome style={{ margin: '3px' }} />
                                    <span>首页({count})</span>
                                </Link>
                                <Link className={"menu box " + (pathname === "/compilations" ? "menu-select" : "")} onClick={() => {
                                    this.setState({
                                        pathname: '/compilations'
                                    })
                                    PathEvent.emit('blog-path', {
                                        deleteid: 'true',
                                    })
                                }} to={'/compilations'}>
                                    <IconPaperclip style={{ margin: '3px' }} />
                                    <span>合集</span>
                                </Link>
                                <Link className={"menu box " + (pathname === "/resource-list" ? "menu-select" : "")} onClick={() => {
                                    this.setState({
                                        pathname: '/resource-list'
                                    })
                                    PathEvent.emit('blog-path', {
                                        deleteid: 'true',
                                    })
                                }} to={'/resource-list'}>
                                    <IconMenu style={{ margin: '3px' }} />
                                    <span>资源列表</span>
                                </Link>
                                <Link className={"menu box " + (pathname === "/links" ? "menu-select" : "")} onClick={() => {
                                    this.setState({
                                        pathname: '/links'
                                    })
                                }} to={'/links'}>
                                    <IconBranch style={{ margin: '3px' }} />
                                    <span>友链</span>
                                </Link >
                                <Link to='/repository' className={"menu box " + (pathname === "/repository" ? "menu-select" : "")} onClick={() => {
                                    this.setState({
                                        pathname: '/repository'
                                    })
                                }}>
                                    <IconGithubLogo style={{ margin: '3px' }} />
                                    <span>仓库</span>
                                </Link>
                            </div>
                            <Divider margin='12px' />
                            <Collapse>
                                <Collapse.Panel header={<><IconAppCenter />分类</>} itemKey="1">
                                    {category.map(x => {
                                        return (
                                            <Link to={"/?type=" + x.id} className={"menu " + (pathname === x.id ? "menu-select" : "")} onClick={() => {
                                                this.setState({
                                                    pathname: x.id
                                                })
                                                PathEvent.emit('blog-path', {
                                                    id: x.id,
                                                })
                                            }}>
                                                <span>{x.name}</span>
                                                <span style={{ float: 'right' }}>({x.count})</span>
                                            </Link>
                                        )
                                    })}
                                </Collapse.Panel>
                            </Collapse>
                            <div>
                                <Link to='/manage' className={"menu " + (pathname === "/manage" ? "menu-select" : "")} onClick={() => {
                                    this.setState({
                                        pathname: '/manage'
                                    })
                                }}>
                                    <IconSetting style={{ margin: '3px' }} />
                                    <span>设置</span>
                                </Link>
                            </div>
                        </Card>
                    </Col>
                    <Col span={16} className='blog-render'>
                        <div style={{ marginLeft: '5px', marginRight: '5px' }}>
                            <Card style={{ height: '95px', width: '100%' }}>
                                <h2>南岛鹋</h2>
                                <span>{this.wisdom()}</span>
                            </Card>
                        </div>
                        <div style={{ margin: '5px' }}>
                            <Outlet />
                        </div>
                    </Col>
                    <Col span={4} style={{ height: '100%' }} className='blog-ranking'>
                        <Card style={{ height: '100%' }}>
                            <Ranking />
                            <Divider margin='12px' />
                            <span>标签</span>
                            <Divider margin='12px' />
                            <Space wrap>
                                {
                                    tab.map(x => {
                                        return (<Tag onClick={() => {
                                            x.selected = !x.selected;
                                            if (x.selected) {
                                                PathEvent.emit('blog-path', {
                                                    tabId: x.name,
                                                })
                                            } else {
                                                PathEvent.emit('blog-path', {
                                                    deleteTabId: true,
                                                })
                                            }
                                            this.setState({
                                                tab: tab
                                            })
                                        }} color={colors[Math.floor(Math.random() * colors.length)]} key={x.id}>{(x.selected ? "*" : "") + x.name} </Tag>)
                                    })
                                }
                                <Tag onClick={() => {
                                    PathEvent.emit('blog-path', {
                                        deleteTabId: true,
                                    })
                                }} color={colors[Math.floor(Math.random() * colors.length)]} key={99999999}>清空标签</Tag>
                            </Space>
                        </Card>
                    </Col>
                </Row>
            </div>
        )
    }
}
