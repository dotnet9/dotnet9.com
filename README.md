<p align="center">
  <a href="https://dotnet9.com">
    <img src="https://img1.dotnet9.com/site/logo.png" width="128" height="128" alt="Dotnet9">
  </a>
</p>

<h1 align="center">Dotnet9</h1>

<div align="center">

One use `ASP NET Core MVC 7.0` developed the `blog system`, which integrates online free `tools`, and is currently under development

 ![dotnet-version](https://img.shields.io/badge/.NET%207.0-blue)  ![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio%20-2022-blueviolet)  [![Github](https://img.shields.io/badge/%20-github-%2324292e)](https://github.com/dotnet9/Dotnet9) [![Github stars](https://img.shields.io/github/stars/dotnet9/Dotnet9)](https://github.com/dotnet9/Dotnet9/stargazers)

 </div>

English | [简体中文](README-zh_CN.md)

## ✨ Features

1. Use `ASP Net core MVC 7.0` development
2. With blog browsing feature
3. Use with tools

## 🌈 Online Examples

Dotnet9：[https://dotnet9.com](https://dotnet9.com)

## 🖥 Environment Support

- .NET 7.0
- Visual Studio 2022
- PostgreSQL

### 3.1 Project configuration

Please configure the project before running correctly. Please see the following instructions.

1. Configuration database connection string

Add connection string of MySQL to the `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=[host];Username=[username];Database=[database];port=[5432];password=[password];"
}
```

2. Configuration the datas of blog

Add seed data of blog to the `appsettings.json`:

```json
  "AssetsLocalPath": "F:\\github_gitee\\Assets.Dotnet9",
  "AssetsRemotePath": "https://img1.dotnet9.com"
```

- AssetsLocalPath: There are Blog post categories, albums, posts and other are stored in this directory, these needs to be cloned from repository: https://github.com/dotnet9/Assets.Dotnet9
- AssetsRemotePath：This is cdn url and the image resources are storage in this repository.

### 3.2 Data migration

Open the package console and select the project `Dotnet9.EntityFrameworkCore`, then execute the following command:

```shell
Add-Migration InitDB
Update-Database
```

### 3.2 Generate data seed

After the above two steps are completed, run the project and visit the link 'localhost:5000 /seed' to generate seed data. This method is written in below:

```C#
[Route("seed")]
public async Task<bool> Seed()
{
  // Seed execution method body
}
```

## 💕 Donation

<div align="center">
<img src="https://img1.dotnet9.com/pays/WeChatPay.jpg" width="256" alt="Wechat"><img src="https://img1.dotnet9.com/pays/AliPay.jpg" style="margin-left: 5px; margin-right: 5px;" width="256" alt="Alipay"><img src="https://img1.dotnet9.com/pays/QQPay.jpg" width="256" alt="QQpay">
</div>

## ☀️ License

MIT

## A few last screenshots of the website

Introduction to website articles：[分享我做Dotnet9博客网站时积累的一些资料](https://dotnet9.com/2022/03/Share-some-learning-materials-I-accumulated-when-I-was-a-blog-website)

![](./assets/01_front_home.gif)

### Front

**Album**

![](./assets/02_front_album.gif)

**Category**

![](./assets/03_front_cat.gif)

**Details of blog post**

![](./assets/04_front_blogpost.gif)

### 后台

![](./assets/08_backend_all.gif)

**Register page**

![](./assets/05_backend_register_admin.png)

**Login page**

![](./assets/06_backend_login.png)

**Dashboard**

![](./assets/07_backend_home.png)