import axios from 'axios'
import { Component } from 'react'
import ReactMarkdown from 'react-markdown'
import SyntaxHighlighter from 'react-syntax-highlighter'
import { dark } from 'react-syntax-highlighter/dist/esm/styles/hljs'
import './index.css'

export default class About extends Component {

    state = {
        content: ''
    }

    loadAbout() {
        axios.get('/about.md').then(x => {
            this.setState({
                content: x.data
            })
        })
    }

    componentDidMount(): void {
        this.loadAbout()
    }

    render() {
        var { content } = this.state;
        return (
            <div>
                <ReactMarkdown
                    children={content}
                    className='about-md'
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
            </div>
        )
    }
}
