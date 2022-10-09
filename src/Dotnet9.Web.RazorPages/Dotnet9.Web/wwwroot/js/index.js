var TOKEN_KEY = "AUTH_TOKEN";
var USER_KEY = "USER_INFO";
var SALT = "dotnet9"; // 加盐

$(function () {
  // 登录
  loginLogic();
  // 归档
  pagination();
  // toc
  tocInit();
  // 编辑和删除
  initEditLogic();
  // 顶部选中
  headerActive();
  // 归档排序
  initPigeSort();
  // 搜索
  initSearch();
});

function setAjaxToken(xhr) {
  xhr.setRequestHeader("Authorization", localStorage.getItem("AUTH_TOKEN"));
}

function headerActive() {
  var nav = $('a[href="' + location.pathname + '"]');
  if (nav.length == 0) return;
  nav.addClass("active");
}
function initEditLogic() {
  var edit = $(".detail-edit");
  if (localStorage.getItem(TOKEN_KEY) && edit.length > 0) {
    edit.show();
    var delEle = $(".detail-delete");
    // 绑定删除事件
    delEle.click(function () {
      deleteDetail(delEle.attr("pid"));
    });
  }
}
// 登录部分逻辑
function loginLogic() {
  if (localStorage.getItem(TOKEN_KEY)) {
    $(".login-action").hide();
    $(".login-end").show();
    var userInfo = JSON.parse(localStorage.getItem(USER_KEY)) || {};
    $(".login-username").text(userInfo.name);
  }
  // 登录
  $(".login-submint").click(function () {
    var tipEle = $(".login-tip");
    var name = $(".login-name").val();
    var passwd = $(".login-passwd").val();
    if (!name) return tipEle.show().text("请输入用户名");
    if (!passwd) return tipEle.show().text("请输入密码");

    // md5加密
    var MD5Passwd = new Hashes.MD5().hex(passwd + SALT);
    $.ajax({
      url: "http://127.0.0.1:5141/api/login/account",
      data: JSON.stringify({ username: name, password: passwd, type: "account", autologin: false }),
      contentType: "application/json",
      type: "POST",
      success: function (res) {
          if (res.success !== true) {
            return tipEle.show().text(res.errorMessage);
          }
          localStorage.setItem(TOKEN_KEY, res.token);
          localStorage.setItem(USER_KEY, JSON.stringify(res.user));
          location.href = "/";
      },
      error: function (err) {
        console.log("err", err);
        tipEle.show().text("登录错误，请重试");
      },
    });
  });
  // 退出登录
  $(".login-out").click(function () {
    localStorage.removeItem(USER_KEY);
    localStorage.removeItem(TOKEN_KEY);
    $(".login-action").show();
    $(".login-end").hide();
  });
}

// 翻页逻辑
function pagination() {
  var query = new URLSearchParams(location.search);
  var page = query.get("page") || 1;
  $(".pagination-next").click(function () {
    page++;
    location.search = "?page=" + page;
  });
  $(".pagination-prev").click(function () {
    page--;
    if (page == 1) return (location.search = "");
    location.search = "?page=" + page;
  });
  // $(".pagination-btn").click(function (event) {
  //   var val = $(event.target).attr("value");
  //   if (val == 1) return (location.href = "/");
  //   location.search = "?page=" + val;
  // });
}
function deleteDetail(id) {
  var r = confirm("是否确认删除？");
  if (!r) return;
  $.ajax({
    url: "/api/v1/post/" + id,
    type: "DELETE",
    contentType: "application/json",
    success: function (res) {
      if (res.code != 200) alert(res.error);
      location.href = "/";
    },
    beforeSend: setAjaxToken,
  });
}
function tocInit() {
  var tocBox = $("#toc-box");
  if (tocBox.length == 0) return;
  imageZoom();
  var mdTocList = $(".markdown-toc-list");
  // 如果有TOC
  if (mdTocList.length > 0 && mdTocList.children().length > 0) {
    tocBox.append(mdTocList);
    tocScrollTo(tocBox);
  } else {
    $(".detail-left").css("width", "100%");
    $(".detail-right").hide();
  }
}
function imageZoom() {
  var zoom = $(".zoom-prev");
  $(".detail-content").on("click", "img", function (event) {
    $(".zoom-container").css(
      "background-image",
      "url(" + $(event.target).attr("src") + ")"
    );
    zoom.show();
  });
  zoom.click(function () {
    zoom.hide();
  });
}
function tocScrollTo(tocBox) {
  // 组织默认事件
  var all = document.querySelectorAll("#toc-box a");
  for (var i = 0, len = all.length; i < len; i++) {
    all[i].href = "javascript:void(0)";
  }
  var prvEle = null;
  tocBox.on("click", "a", function (event) {
    event.stopPropagation();
    ele = $(event.target);
    ele.addClass("active");
    if (prvEle) prvEle.removeClass("active");
    prvEle = ele;
    var _href = $(event.target).text();
    var top = $("a[name='" + _href + "']").offset().top;
    window.scrollTo(0, top - 80);
  });
}

function initPigeSort() {
  var box = $(".pige-content");
  if (box.length == 0) return;
  var children = box.children();
  // 翻转排序
  for (var i = children.length; i >= 0; i--) {
    box.append(children[i]);
  }
}

function initSearch() {
  var timer = null;
  var searchList = [];
  var drop = $(".search-drop");
  var input = $("#search-input");
  input.on("input", function (event) {
    clearTimeout(timer);
    timer = setTimeout(function () {
      searchHandler(event.target.value);
    }, 300);
  });
  input.on("blur", function () {
    if (searchList.length === 0) drop.hide();
  });
  function searchHandler(val) {
    if (!val) return (searchList = []);
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
  }
}
