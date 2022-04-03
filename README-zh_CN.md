<p align="center">
  <a href="https://dotnet9.com">
    <img src="https://img1.dotnet9.com/site/logo.png" width="128" height="128" alt="Dotnet9">
  </a>
</p>

<h1 align="center">Dotnet9</h1>

<div align="center">

一个使用`ASP.NET Core MVC 7.0`开发的`博客`系统，集成了在线免费`工具`，目前正在开发中...

 ![dotnet-version](https://img.shields.io/badge/.NET%207.0-blue)  ![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio%20-2022-blueviolet)  <a target="_blank" href="https://qm.qq.com/cgi-bin/qm/qr?k=iL6egdGSGCMPezcUyzMPEcs9qsllgwr-&jump_from=webapi"><img border="0" src="https://pub.idqqimg.com/wpa/images/group.png" alt="Dotnet9软件技术交流" title="Dotnet9软件技术交流"></a> [![码云](https://img.shields.io/badge/Gitee-%E7%A0%81%E4%BA%91-orange)](https://gitee.com/dotnet9/Dotnet9)   [![Github](https://img.shields.io/badge/%20-github-%2324292e)](https://github.com/dotnet9/Dotnet9) [![Github stars](https://img.shields.io/github/stars/dotnet9/Dotnet9)](https://github.com/dotnet9/Dotnet9)

 </div>

[English](./README.md) | 简体中文

## ✨ 1. 特性

1. 使用`ASP.NET Core MVC 7.0`开发
2. 带博客浏览功能
3. 带工具使用

## 🌈 2. 在线示例

Dotnet9：[https://dotnet9.com](https://dotnet9.com)

## 🖥 3. 支持环境

- .NET 7.0
- Visual Studio 2022
- MySQL

### 3.1 项目配置

正确运行前，请先对项目进行配置，请看下面说明。

1. 配置数据库连接字符串

在`appsettings.json`中添加节点，配置MySQL连接字符串：

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;user=[username];database=[databasename];port=[port];password=[password];SslMode=None"
}
```

2. 配置博客数据

种子数据来源，在`appsettings.json`中添加节点

```json
  "AssetsLocalPath": "F:\\github_gitee\\Assets.Dotnet9",
  "AssetsRemotePath": "https://img1.dotnet9.com"
```

- AssetsLocalPath: 博客分类信息、专辑信息、文章信息等存放在这个目录下，需要将仓库克隆到本地：https://github.com/dotnet9/Assets.Dotnet9
- AssetsRemotePath：CDN链接，图片资源存放路径

### 3.2 数据迁移

打开程序包控制台，选择项目：`Dotnet9.EntityFrameworkCore`，执行以下命令：

```shell
Add-Migration InitDB
Update-Database
```

### 3.2 生成数据种子

以上2个步骤完成后，运行项目，访问链接`localhost:5000/seed`执行种子数据生成，此方法写在`HomeController`中

```C#
[Route("seed")]
public async Task<bool> Seed()
{
  // 种子执行方法体
}
```

## 💕 支持本项目

<div align="center">
<img src="https://img1.dotnet9.com/pays/WeChatPay.jpg" width="256" alt="Wechat"><img src="https://img1.Dotnet9.com/pays/AliPay.jpg" style="margin-left: 5px; margin-right: 5px;" width="256" alt="Alipay"><img src="https://img1.dotnet9.com/pays/QQPay.jpg" width="256" alt="QQpay">
</div>

## ☀️ License

MIT