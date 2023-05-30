import { Layout } from '@douyinfe/semi-ui'
import React, { Component } from 'react'
import './index.css'
import BlogHeader from '../../componses/blog-header';
import BlogContent from '../../componses/blog-content';
import BlogFooter from '../../componses/blog-footer';

const { Header, Footer, Content } = Layout;

export default class Index extends Component {
    render() {
        return (
            <Layout style={{ height: "100%" }} >
                <Header>
                    <BlogHeader />
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
                        <BlogContent />
                    </div>
                </Content>
                <Footer >
                    <BlogFooter/>
                </Footer>
            </Layout>
        )
    }
}
