# 前台 Razor Pages工程

## 文章搜索JS代码需要调整

```js
$.ajax({
        url: "http://localhost:5005/api/search?keywords=" + val,// TODO 生产环境需要将/api反向代理到5005端口，开发环境暂时未做代理，调试时接口需要写成 http://localhost:5005/api/search?keywords=
      contentType: "application/json",
      success: function (res) {
        if (res.success !== true) return alert(res.errorMessage);
        var data = res.data || [];
        searchList = [];
        if (data.length === 0) return drop.html("");
        for (var i = 0, len = data.length; i < len; i++) {
          var item = data[i];
          searchList.push(
            "<a href='/" + item.year + "/" + item.month + "/" + item.slug + "'>" + item.title + "<a/>"
          );
          drop.show().html(searchList.join(""));
        }
      },
    });
```

后面调研代码中反向代理/api到5005端口，或许dapr等框架可行？