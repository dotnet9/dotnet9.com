import React, { Component } from 'react'
import { Form, Col, Row, Button, SideSheet,Notification } from '@douyinfe/semi-ui';
import { IconUpload } from '@douyinfe/semi-icons';
import { FileService } from '../../../services/fileService';
import { ResourceService } from '../../../services/resourceService';

export default class Add extends Component<any> {

    onSubmit(values: any) {
        var file = (values.files as any[]).find(x=>true);
        console.log('file',file.fileInstance);
        var form = new FormData();
        form.append('file', file.fileInstance);
        FileService.UploadFile(form)
            .then(res => {
                if(res){
                    Notification.success({
                        title: '上传成功',
                        content: '上传文件成功,正在保存资源信息'
                    })
                    ResourceService.Create({
                        title: values.title,
                        description: values.description,
                        href: values.href,
                        userName: values.userName,
                        url: (res as string),
                    }).then(res => {
                        Notification.success({
                            title: '新增成功',
                            content: '新增资源成功'
                        });
                        this.props.change();
                    }).catch(err => {
                        console.log(err);
                        Notification.error({
                            title: '新增失败',
                            content: '新增资源失败'
                        })
                    });
                }
            }).catch(err => {
                console.log(err);
                Notification.error({
                    title: '上传失败',
                    content: '上传文件失败'
                })
            });
    }

    render() {
        const { visible, change } = this.props as any;
        const style = { width: '90%' };
        const { Section, TextArea } = Form;

        return (
            <SideSheet title="新增资源列表" visible={visible} onCancel={change} width={600}>
                <Form
                    style={{ padding: 10, width: '100%' }}
                    onSubmit={(v) => {
                        this.onSubmit(v)
                    }}
                >
                    <Section text={'基本信息'}>
                        <Row>
                            <Col span={24}>
                                <Form.Input field='title' label='标题' />
                                <Form.Input field='userName' label='分享作者' />
                                <Form.Input field='href' label='作者外链' />
                            </Col>
                            <Col span={24}>
                                <Form.Upload
                                    field='files'
                                    label='资源文件'
                                    multiple={false}
                                    uploadTrigger="custom"
                                    action=''
                                >
                                    <Button icon={<IconUpload />} theme="light">
                                        点击上传
                                    </Button>
                                </Form.Upload>
                            </Col>
                            <Col span={12}>
                                <TextArea
                                    style={{ ...style, height: 120 }}
                                    field='description'
                                    label='资源描述'
                                    placeholder='请填写资源描述'
                                />
                            </Col>
                        </Row>
                    </Section>
                    <Button type="primary" block htmlType="submit" className="btn-margin-right">提交(submit)</Button>
                </Form>
            </SideSheet>
        )
    }
}
