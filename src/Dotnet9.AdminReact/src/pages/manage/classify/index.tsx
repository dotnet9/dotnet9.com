import { Button, Card, Col, Form, Notification, Row, Select, Space } from '@douyinfe/semi-ui'
import input from '@douyinfe/semi-ui/lib/es/input'
import tabs from '@douyinfe/semi-ui/lib/es/tabs'
import Tag, { TagColor } from '@douyinfe/semi-ui/lib/es/tag'
import React, { Component } from 'react'
import { TabService } from '../../../services/tabService'
import { CategoryDto, TabDto } from '../../../models/blogger'
import { CategoryService } from '../../../services/categoryService'

export default class Classify extends Component {

    state = {
        tabs: [] as TabDto[],
        categorys: [] as CategoryDto[],
        colors: ['amber', 'blue', 'cyan', 'green', 'grey', 'indigo',
            'light-blue', 'light-green', 'lime', 'orange', 'pink',
            'purple', 'red', 'teal', 'violet', 'yellow', 'white'
        ] as TagColor[],
        input: {
            name: '',
            description: '',
            tabName: ''
        }
    }

    createCategory() {
        const { input } = this.state;
        if(input.name){
            CategoryService.create(input.name, input.description)
                .then(res => {
                    Notification.success({
                        title: '创建成功'
                    })
                    this.loadCategorys()
                })    
        }

    }

    createTab() {
        const { input } = this.state;
        if(input.tabName){
            TabService.create(input.tabName)
                .then(res => {
                    Notification.success({
                        title: '创建成功'
                    })
                    this.loadTabs()
                })
        }
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

    loadTabs() {
        TabService.getTabs()
            .then(res => {
                this.setState({
                    tabs: res
                })
            })
    }

    constructor(props: any) {
        super(props)
        this.loadTabs()
        this.loadCategorys()
    }

    render() {
        const { tabs, colors, categorys, input } = this.state;
        return (
            <div style={{
                height: '100%'
            }}>
                <Card>
                    <Form labelPosition='left' layout='horizontal' onValueChange={values => console.log(values)}>
                        <Form.Input initValue={input.name} onChange={(e) => this.setState({
                            input: {
                                ...input,
                                name: e
                            }
                        })} field='name' label='名称' style={{ width: 150 }} />
                        <Form.Input initValue={input.description} onChange={(e) => this.setState({
                            input: {
                                ...input,
                                description: e
                            }
                        })} field='description' label='描述' style={{ width: 150 }} />
                        <Button style={{marginRight:'5px'}} type="primary" htmlType="submit" onClick={() => this.createCategory()} className="btn-margin-right">新增分类</Button>
                        <Form.Input  initValue={input.tabName} onChange={(e) => this.setState({
                            input: {
                                ...input,
                                tabName: e
                            }
                        })} field='name' label='标签名称' style={{ width: 150 }} />
                        <Button type="primary" htmlType="submit" onClick={() => this.createTab()} className="btn-margin-right">新增标签</Button>
                    </Form>
                </Card>
                <Card style={{
                    height: '95%'
                }}>
                    <Row style={{
                        height: '100%'
                    }}>
                        <Col span={12}>
                            <Space wrap>
                                {categorys.map((x: any) => {
                                    return (<Tag color={colors[Math.floor(Math.random() * colors.length)]} key={x.id}>{x.name} </Tag>)
                                })}
                            </Space>
                        </Col>
                        <Col span={12}>
                            <Space wrap>
                                {tabs.map((x: any) => {
                                    return (<Tag color={colors[Math.floor(Math.random() * colors.length)]} key={x.id}>{x.name} </Tag>)
                                })}
                            </Space>
                        </Col>
                    </Row>
                </Card>
            </div>
        )
    }
}
