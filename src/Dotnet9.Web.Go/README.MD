# Dotnet9网站前台

## 配置文件

配置数据库连接字符串,创建 `/appsettings.toml` 文件，配置内容如下

```toml
[db]
    ConnectionString = "host=127.0.0.1 port=5432 user=postgres password=【密码】 dbname=dotnet9 sslmode=disable TimeZone=Asia/Shanghai"
```

## 致谢

- [《【码神之路】原生Go语言博客实战教程，练手级项目实战教程，未使用任何框架，通俗易懂，十年大厂程序员讲解》](https://www.bilibili.com/video/BV1VS4y1F7NM)
- [Go博客练手级实战项目教程](https://mszlu.com/goblog/01.html)