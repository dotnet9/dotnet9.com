import { addOrUpdateAbout, about } from '@/services/ant-design-pro/api';
import { useEffect, useState } from 'react';
import { Button, message } from 'antd';
import MarkdownIt from 'markdown-it';
import MdEditor from 'react-markdown-editor-lite';
import 'react-markdown-editor-lite/lib/index.css';

const mdParser = new MarkdownIt({
  html: true,
  linkify: false,
  typographer: true,
});

const About: React.FC = () => {
  const [value, setValue] = useState<string>('');

  function handleEditorChange({ text }: { html: string; text: string }) {
    setValue(text);
  }

  const handleRead = async () => {
    const hide = message.loading('正在读取');
    try {
      const data = await about();
      setValue(data.content);
      hide();
      message.success('读取成功');
      return true;
    } catch (e) {
      hide();
      message.error('读取失败');
      return false;
    }
  };

  const handleSave = async () => {
    const hide = message.loading('正在保存');
    try {
      await addOrUpdateAbout({ content: value });
      hide();
      message.success('保存成功');
      return true;
    } catch (e) {
      hide();
      message.error('保存失败');
      return false;
    }
  };

  useEffect(() => {
    handleRead();
  }, []);

  return (
    <div>
      <h2>关于管理</h2>
      <Button type="primary" key="primary" onClick={handleSave}>
        保存
      </Button>
      <MdEditor
        value={value}
        style={{ height: '800px' }}
        renderHTML={(text) => mdParser.render(text)}
        onChange={handleEditorChange}
      />

      <Button type="primary" key="primary" onClick={handleSave}>
        保存
      </Button>
    </div>
  );
};

export default About;
