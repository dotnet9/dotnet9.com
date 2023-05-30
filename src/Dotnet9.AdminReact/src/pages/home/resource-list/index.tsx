import { Component } from 'react'
import './index.css'
import { Button, Card, Icon, Notification, Pagination, Row, Space } from '@douyinfe/semi-ui'
import { ResourceDto } from '../../../models/blogger'
import { ResourceService } from '../../../services/resourceService'
import Meta from '@douyinfe/semi-ui/lib/es/card/meta'
import { Download, Praise } from '../../../utils/iconUtil'
import { downloadFile } from '../../../utils/utils'



export default class ResourceList extends Component {

  state = {
    data: {
      result: [] as ResourceDto[],
      total: 0
    },
    input: {
      keywords: '',
      page: 1,
      pageSize: 20
    }
  }

  load() {
    const { input } = this.state
    ResourceService.GetList(input.keywords, input.page, input.pageSize)
      .then(res => {
        this.setState({ data: res })
      })
  }

  componentDidMount(): void {
    this.load()
  }

  onPraise(id: string) {
    ResourceService.Praise(id)
      .then(() => {
        Notification.success({ title: '点赞成功' })
        this.load()
      })
      .catch((error) => {
        Notification.error({ title: error.response.data.message })
      })
  }

  onDownload(id: string) {
    ResourceService.Download(id)
      .then((res) => {
        console.log('res', res);
        if (res) {
          downloadFile(res as string)
          Notification.success({ title: '开始下载' })
          this.load()
        }
      })
      .catch((error) => {
        Notification.error({ title: error.response.data.message })
      })
  }

  render() {
    const { data, input } = this.state
    return (
      <div
        className='resource-list'

      >
        <Row gutter={[16, 16]} style={{
          maxHeight: 'calc(80vh - 100px)',
          overflow: "auto"
        }}>
          {data.result.map(x => {
            return (
              <Card
                shadows='hover'
                className='target'
                bodyStyle={{
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'space-between'
                }}
                style={{ maxWidth: 385, minWidth: 385, height: '260px', margin: '5px', float: 'left' }}
                title={
                  <Meta
                    title={x.title}
                  />
                }
                headerExtraContent={<span onClick={() => {
                  if (x.href) {
                    window.open(x.href)
                  }
                }} style={{ fontSize: '10px', fontFamily: "cursive", cursor: 'pointer', }}>作者：{x.userName}</span>}
                footerStyle={{ display: 'flex', justifyContent: 'flex-end' }}
                footer={
                  <Space style={{
                    marginRight: '-5px'
                  }}>
                    <><Icon svg={Download("#000000")}></Icon><span>{x.downloadCount}</span></>
                    <><Icon svg={Praise("#000000")}></Icon><span>{x.referee}</span></>
                    <Button theme='solid' type='primary' icon={<Icon svg={Download()} />} onClick={() => this.onDownload(x.id)} >下载</Button>
                    <Button theme='solid' type='primary' icon={<Icon svg={Praise()} />} onClick={() => this.onPraise(x.id)}>推荐</Button>
                  </Space>
                }
              >
                <div style={{ minHeight: '110px' }}>
                  {x.description}
                </div>
              </Card>)
          })}
        </Row>

        <Pagination style={{ marginTop: '20px' }} total={data.total} pageSize={input.pageSize} showTotal onChange={(e) => {
          input.page = e;
          this.setState({ input }, () => {
            this.load();
          })
        }} defaultCurrentPage={input.page}></Pagination>
      </div>
    )
  }
}
