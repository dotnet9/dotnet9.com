import { Database } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'SQL 美化和格式化',
  path: '/sql-prettify',
  description: '在线格式化和美化您的 SQL 查询语句（它支持各种 SQL 变种）。',
  keywords: [
    'sql',
    'prettify',
    'beautify',
    'GCP BigQuery',
    'IBM DB2',
    'Apache Hive',
    'MariaDB',
    'MySQL',
    'Couchbase N1QL',
    'Oracle PL/SQL',
    'PostgreSQL',
    'Amazon Redshift',
    'Spark',
    'SQL Server Transact-SQL',
  ],
  component: () => import('./sql-prettify.vue'),
  icon: Database,
});
