import { GithubOutlined } from '@ant-design/icons';
import { DefaultFooter } from '@ant-design/pro-components';
import { useIntl } from '@umijs/max';

const Footer: React.FC = () => {
  const intl = useIntl();
  const defaultMessage = intl.formatMessage({
    id: 'app.copyright.produced',
    defaultMessage: '沙漠尽头的狼',
  });

  const currentYear = new Date().getFullYear();

  return (
    <DefaultFooter
      style={{
        background: 'none',
      }}
      copyright={`${currentYear} ${defaultMessage}`}
      links={[
        {
          key: 'Dotnet9',
          title: 'Dotnet9',
          href: 'https://dotnet9.com',
          blankTarget: true,
        },
        {
          key: 'github',
          title: <GithubOutlined />,
          href: 'https://github.com/dotnet9/Dotnet9',
          blankTarget: true,
        },
        {
          key: 'The Wolf at the End of the Desert',
          title: '沙漠尽头的狼',
          href: 'https://github.com/dotnet9',
          blankTarget: true,
        },
      ]}
    />
  );
};

export default Footer;
