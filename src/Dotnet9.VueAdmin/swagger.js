import { codegen } from 'swagger-axios-codegen';
//工具仓库地址 https://github.com/Manweill/swagger-axios-codegen
codegen({
    methodNameMode: 'path',
    remoteUrl: 'http://localhost:51775/swagger/v1/swagger.json',
    outputDir: './src/shared',
    useStaticMethod: true,
    fileName: 'service.ts'
});
