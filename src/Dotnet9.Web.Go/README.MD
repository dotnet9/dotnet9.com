# Dotnet9网站前台

## 待完善

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
    ConnectionString = "host=127.0.0.1 port=5432 user=postgres password=fcY5riX64opfZFhEs dbname=dotnet9 sslmode=disable TimeZone=Asia/Shanghai"
[admin]
    Api = "http://127.0.0.1:5141"
```

## 致谢

- [《【码神之路】原生Go语言博客实战教程，练手级项目实战教程，未使用任何框架，通俗易懂，十年大厂程序员讲解》](https://www.bilibili.com/video/BV1VS4y1F7NM)
- [Go博客练手级实战项目教程](https://mszlu.com/goblog/01.html)