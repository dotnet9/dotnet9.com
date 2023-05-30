import { Component } from 'react'
import { PathEvent } from '../../../componses/events/pathEvent';
import './index.css'
import { IconUser } from '@douyinfe/semi-icons';
import { ArticleService } from '../../../services/articleService';
import {  GetArticleListDto } from '../../../models/blogger';
import { formatDate } from '../../../utils/utils';
import { Pagination } from '@douyinfe/semi-ui';

interface IProps {

}

interface IState {
  data: {
    total: number,
    result: GetArticleListDto[]
  },
  input: {
    keyword: string;
    categoryId: string | null;
    page: number;
    tabIds: string | null;
    pageSize: number;
  }
}

var type = new URLSearchParams(window.location.search).get("type");

export default class Home extends Component<IProps, IState> {

  state: Readonly<IState> = {
    input: {
      keyword: '',
      categoryId: type ?? '',
      tabIds: null,
      page: 1,
      pageSize: 10
    },
    data: {
      total: 0,
      result: []
    }
  }

  componentDidMount(): void {
    this.LoadList();
    // 监听路由变化
    PathEvent.addListener('blog-path', this.handleCustomEvent);
  }

  handleCustomEvent = (event: any) => {
    var { input } = this.state;
    if (event.id) {
      input.categoryId = event.id;
      this.setState({ input }, () => {
        this.LoadList();
      })
    } else if (event.tabId) {
      input.tabIds = event.tabId;
      this.setState({ input }, () => {
        this.LoadList();
      })
    } else if (event.deleteTabId) {
      input.tabIds = '';
      this.setState({ input }, () => {
        this.LoadList();
      })
    } else if (event.deleteid) {
      input.categoryId = null;
      input.tabIds = null;
      this.setState({ input }, () => {
        this.LoadList();
      })
    }
    else {
      input.keyword = event.value;
      this.setState({ input }, () => this.LoadList())
    }
  }

  LoadList() {
    var { input } = this.state;
    ArticleService
      .getList(input.keyword, input.categoryId, input.tabIds, input.page, input.pageSize)
      .then(res => {
        res.result.forEach((x: GetArticleListDto) => {
          x.background = `url(/${Math.floor(Math.random() * 6) + 1}.jpg)`
        });
        this.setState({
          data: res
        })
      })
  }

  componentWillUnmount(): void {
    PathEvent.removeListener('blog-path', this.handleCustomEvent);
  }

  render() {
    var { input, data } = this.state;
    return (<>
      <div style={{ maxHeight: 'calc(100vh - 240px)', overflow: 'auto' }}>
        <div className='article'>
          {data.result.map((x, i) => {
            return (
              <>
                <div onClick={() => {
                  window.location.href = `/blog?id=${x.id}`
                }} style={{ margin: '5px' }}>
                  <div className='blog-img' style={{ backgroundImage: x.background }}>
                  </div>
                  <div className='blog-article' onMouseOver={() => {
                    x.show = true;
                    this.setState({
                      data: {
                        total: data.total,
                        result: data.result
                      }
                    })
                  }} onMouseOut={() => {
                    x.show = false;
                    this.setState({
                      data: {
                        total: data.total,
                        result: data.result
                      }
                    })
                  }}>
                    <div className='blog-article-title'>
                      {x.title}
                    </div>
                    <div style={{ marginTop: '65px', position: 'relative' }}>
                    </div>
                    <div className={"blog-article-content " + x.show ? "blog-article-content-select" : ""}>
                      <div style={{ margin: '10px' }}>
                        <span><IconUser />Token</span> <span>发布于：{formatDate(x.creationTime)}</span> <span>阅读：{x.readCount}</span>
                      </div>
                    </div>
                  </div>
                </div>
              </>
            )
          })}

        </div>
      </div>
      <Pagination total={data.total} showTotal onChange={(e) => {
        input.page = e;
        this.setState({ input }, () => {
          this.LoadList();
        })
      }} defaultCurrentPage={input.page}></Pagination>
    </>
    )
  }
}
