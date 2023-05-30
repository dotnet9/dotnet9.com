import  { Component } from 'react'
import { ArticleService } from '../../services/articleService'
import { GetArticleListDto } from '../../models/blogger'
import './index.css'
import { Divider, Tooltip } from '@douyinfe/semi-ui'

export default class Ranking extends Component {

    state = {
        data: [] as GetArticleListDto[]
    }

    load() {
        ArticleService
            .getRanking()
            .then(res => {
                this.setState({
                    data: res
                })
            })
    }

    componentDidMount(): void {
        this.load()
    }

    render() {
        var { data } = this.state;
        return (
            <div>
                <div  style={{
                    textAlign: 'center', 
                    fontSize: '16px'
                }}><span>热榜</span></div>
                <Divider margin='12px'/>
                <div className="list">
                    {data.map((x, i) => {
                        return (
                            <Tooltip
                                position='topLeft'
                                content={x.title + '\r\n访问量:' + x.readCount + '\r\n点赞：' + x.like}>
                                <div onClick={() => {
                                    window.location.href = '/blog?id=' + x.id
                                }} className='list-data' style={{ overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}>
                                    <span className='list-serial'>{i + 1}.</span>
                                    <span className='target'>{x.title}</span>
                                </div>
                            </Tooltip>)
                    })}
                </div>

            </div>
        )
    }
}
