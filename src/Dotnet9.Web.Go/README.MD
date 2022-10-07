# Dotnet9网站前台

## 暂时不维护

后面的功能不打算继续写了，继续折腾`ASP.NET Core Razor Pages`！

当然不排除哪天心血来潮，又维护这个项目了...

### 登录接口地址

目前是写死在`public/resource/js/index.js`，后面需要读配置文件

```js
http://127.0.0.1:5141/api/login/account
```

## 配置文件

创建 `/appsettings.toml` 文件，配置内容如下

```toml
[site]
    DefaultAuthor = "沙漠尽头的狼"
[db]
    ConnectionString = "host=127.0.0.1 port=5432 user=postgres password=fcs3x5rX7f6ZFhEs dbname=dotnet9 sslmode=disable TimeZone=Asia/Shanghai"
[admin]
    Api = "http://127.0.0.1:5164"
[viewer]
    Title = "Dotnet9"
    Description = "一个专注于软件开发技术分享的网站"
    Logo = "https://img1.dotnet9.com/site/logo.png"
    Navigation = ["首页", "/", "Dotnet", "/c/dotnet", "Go语言", "/c/more-language-go", "归档", "/pigeonhole", "关于", "/about"]
    Github = "https://github.com/dotnet9/Dotnet9"
    Bilibili = "https://space.bilibili.com/470546606"
    Zhihu = "https://www.zhihu.com/people/dotnet9"
    Avatar = "https://img1.dotnet9.com/site/cat.png"
    UserName = "Dotnet9"
    UserDesc = "一个热衷于互联网分享精神的程序员"
[system]
    CdnURL = ""
	QiniuAccessKey = ""
	QiniuSecretKey = ""
	Valine = true
	ValineAppId = "j8ELbjeA487uingrummes-gzGzoHsz"
	ValineAppKey = "OT6bYtVpY1456lggrTaMUuW"
	ValineServerURL = "https://j8hllbjea.lcrhcn-n1-dfared.com"
```

## 致谢

- [《【码神之路】原生Go语言博客实战教程，练手级项目实战教程，未使用任何框架，通俗易懂，十年大厂程序员讲解》](https://www.bilibili.com/video/BV1VS4y1F7NM)
- [Go博客练手级实战项目教程](https://mszlu.com/goblog/01.html)