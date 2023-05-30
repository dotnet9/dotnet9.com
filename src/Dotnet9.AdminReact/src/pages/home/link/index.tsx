import { Card, Col, Row } from '@douyinfe/semi-ui'
import React, { Component } from 'react'

export default class Link extends Component {

  state = {
    link: [{
      title: '沙漠的狼',
      description: 'Dotnet9，专注ASP.NET Core网站开发、MAUI跨平台应用开发、WPF客户端开发，同时以https://Dotnet9.com 网站分享—些技术类文章，欢迎交流、学习。',
      url: 'https://dotnet9.com',
    }, {
      title: 'Perry',
      description: '二比少年，你是不是想要一起玩游戏呢？',
      url: 'http://wosperry.com/',
    }, {
      title: 'Masa Stack',
      description: 'Stack是新一代数字化云原生技术底座产品，其中核心产品包括了用户的权限设置与继承、项目管理、故障排查、以及配套化的消息中心、日志中心、监控中心、配置中心、服务中心、任务中心、文件中心、数据中心、通知中心、工作流中心等。',
      url: 'https://masastack.com',
    }]
  }

  render() {
    var { link } = this.state;
    return (
      <div
        style={{
          backgroundColor: 'var(--semi-color-fill-0)',
          height: '100%',
          padding: 20
        }}
      >
        <Row gutter={[16, 16]}>
          {link.map(x => {
            return (<Col span={8}>
              <div onClick={() => window.open(x.url)}>
                <Card
                  className='target'
                  shadows='hover' title={x.title}
                  bodyStyle={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'space-between'
                  }} style={{ minHeight: '250px' }}>
                  {x.description}
                </Card>
              </div>
            </Col>)
          })}
        </Row>
      </div>
    )
  }
}
