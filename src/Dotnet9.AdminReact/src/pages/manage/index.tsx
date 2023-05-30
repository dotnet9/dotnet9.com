import { Layout, Nav, Button,Skeleton, Avatar } from '@douyinfe/semi-ui';
import { IconBell, IconHelpCircle, IconHome, IconHash, IconArticle, IconSend,IconArrowUpLeft,IconCustomize } from '@douyinfe/semi-icons';
import { Outlet, useNavigate } from 'react-router-dom';


export default function Manage() {

    localStorage.getItem('token') || (window.location.href = '/login');

    const { Header, Footer, Sider, Content } = Layout;
    const navigate = useNavigate()

    return (

        <Layout style={{ height: '100%', border: '1px solid var(--semi-color-border)' }}>
            <Sider style={{ backgroundColor: 'var(--semi-color-bg-1)' }}>
                <Nav
                    defaultSelectedKeys={['Home']}
                    style={{ maxWidth: 220, height: '100%' }}
                    items={[
                        { itemKey: '/manage', text: '首页', icon: <IconHome size="large" /> },
                        { itemKey: '/manage/blog', text: '博客管理', icon: <IconArticle size="large" /> },
                        { itemKey: '/manage/push-blog', text: '发布博客', icon: <IconSend size="large" /> },
                        { itemKey: '/manage/resource-list', text: '资源列表', icon: <IconCustomize  size="large" /> },
                        { itemKey: '/manage/classify', text: '分类管理', icon: <IconHash size="large" /> },
                        { itemKey: '/', text: '返回博客', icon: <IconArrowUpLeft size="large" /> }
                    ]}
                    onClick={(key) => {
                        navigate(key.itemKey as string);
                    }}
                    header={{
                        text: '博客后台',
                    }}
                    footer={{
                        collapseButton: true,
                    }}
                />
            </Sider>
            <Layout>
                <Header style={{ backgroundColor: 'var(--semi-color-bg-1)' }}>
                    <Nav
                        mode="horizontal"
                        footer={
                            <>
                                <Button
                                    theme="borderless"
                                    icon={<IconBell size="large" />}
                                    style={{
                                        color: 'var(--semi-color-text-2)',
                                        marginRight: '12px',
                                    }}
                                />
                                <Button
                                    theme="borderless"
                                    icon={<IconHelpCircle size="large" />}
                                    style={{
                                        color: 'var(--semi-color-text-2)',
                                        marginRight: '12px',
                                    }}
                                />
                                <Avatar color="orange" size="small">
                                    YJ
                                </Avatar>
                            </>
                        }
                    ></Nav>
                </Header>
                <Content
                    style={{
                        padding: '24px',
                        height: '100%',
                        backgroundColor: 'var(--semi-color-bg-0)',
                    }}
                >
                    <div
                        style={{
                            borderRadius: '10px',
                            border: '1px solid var(--semi-color-border)',
                            height: '90%',
                            padding: '32px',
                        }}
                    >
                        <Skeleton placeholder={<Skeleton.Paragraph rows={2} />} loading={false}>
                            <Outlet />
                        </Skeleton>
                    </div>
                </Content>
                <Footer
                    style={{
                        display: 'flex',
                        justifyContent: 'space-between',
                        padding: '20px',
                        color: 'var(--semi-color-text-2)',
                        backgroundColor: 'rgba(var(--semi-grey-0), 1)',
                    }}
                >
                    <span
                        style={{
                            display: 'flex',
                            alignItems: 'center',
                        }}
                    >
                        <span>Powered by .NET 7.0 on Kubernetes</span>
                    </span>
                </Footer>
            </Layout>
        </Layout>
    )
}
