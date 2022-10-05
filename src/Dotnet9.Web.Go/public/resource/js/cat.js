// ==UserScript==
// @name         云养宠物
// @version      2.0.1
// @description  在网页的右下角挂一个你喜欢的宠物咯
// @match        *
// @include      *
// @exclude      https://juejin.cn/editor/drafts/*
// @run-at       document-end
// @icon         https://img2.baidu.com/it/u=4226010475,2406859093&fm=26&fmt=auto
// @require      https://code.jquery.com/jquery-3.1.1.min.js
// @namespace    http://www.touch-fish.com/bin/1
// @grant        GM.setValue
// @grant        GM.getValue
// ==/UserScript==
{
  let defaultHTML = `<html>
  <head>
      <meta charset="utf-8">
      <title>CSS睡猫</title>
      <style>
          @keyframes tail{6.6666666667%{transform:rotate(0)}10%{transform:rotate(10deg)}16.6666666667%{transform:rotate(-5deg)}20%{transform:rotate(30deg)}26.6666666667%{transform:rotate(-2deg)}46.6666666667%{transform:rotate(10deg)}53.3333333333%{transform:rotate(-5deg)}56.6666666667%{transform:rotate(10deg)}}
          @keyframes body{6.6666666667%{transform:scaleY(1)}10%{transform:scaleY(1.15)}16.6666666667%{transform:scaleY(1)}20%{transform:scaleY(1.25)}26.6666666667%{transform:scaleY(1)}46.6666666667%{transform:scaleY(1.15)}53.3333333333%{transform:scaleY(1)}56.6666666667%{transform:scaleY(1.15)}}
          @keyframes left-whisker{6.6666666667%{transform:rotate(0)}10%{transform:rotate(0deg)}16.6666666667%{transform:rotate(-5deg)}20%{transform:rotate(0deg)}26.6666666667%{transform:rotate(0deg)}46.6666666667%{transform:rotate(10deg)}53.3333333333%{transform:rotate(-5deg)}56.6666666667%{transform:rotate(10deg)}}
          @keyframes right-whisker{6.6666666667%{transform:rotate(180deg)}10%{transform:rotate(190deg)}16.6666666667%{transform:rotate(180deg)}20%{transform:rotate(175deg)}26.6666666667%{transform:rotate(190deg)}46.6666666667%{transform:rotate(180deg)}53.3333333333%{transform:rotate(185deg)}56.6666666667%{transform:rotate(175deg)}}
          @keyframes left-ear{0%{transform:rotate(-20deg)}6.6666666667%{transform:rotate(-6deg)}13.3333333333%{transform:rotate(-15deg)}26.6666666667%{transform:rotate(-15deg)}33.3333333333%{transform:rotate(-30deg)}40%{transform:rotate(-30deg)}46.6666666667%{transform:rotate(0deg)}53.3333333333%{transform:rotate(0deg)}60%{transform:rotate(-15deg)}80%{transform:rotate(-15deg)}93.3333333333%{transform:rotate(-6deg)}100%{transform:rotateZ(-6deg)}}
          @keyframes right-ear{0%{transform:rotateZ(-16deg)}6.6666666667%{transform:rotateZ(-16deg)}13.3333333333%{transform:rotateZ(-19deg)}26.6666666667%{transform:rotateZ(-19deg)}33.3333333333%{transform:rotateZ(-30deg)}36.6666666667%{transform:rotateZ(-19deg)}37.3333333333%{transform:rotateZ(-30deg)}38%{transform:rotateZ(-19deg)}40%{transform:rotateZ(-19deg)}40.6666666667%{transform:rotateZ(-30deg)}41.3333333333%{transform:rotateZ(-19deg)}46.6666666667%{transform:rotateZ(-9deg)}53.3333333333%{transform:rotateZ(-9deg)}60%{transform:rotateZ(-19deg)}60.6666666667%{transform:rotateZ(-30deg)}61.3333333333%{transform:rotateZ(-19deg)}62.6666666667%{transform:rotateZ(-19deg)}63.3333333333%{transform:rotateZ(-30deg)}64%{transform:rotateZ(-19deg)}80%{transform:rotateZ(-19deg)}93.3333333333%{transform:rotateZ(-16deg)}100%{transform:rotateZ(-16deg)}}
          body{display:inline-flex;justify-content:center;align-items:center}.main{height:400px;width:400px;position:relative;top:-80px;left:-80px}.main .stand{position:absolute;top:50%;left:50%;transform:translate(-50%);height:20px;width:200px;border-radius:20px;background-color:#fd6e72;z-index:2}.main .stand::after{content:"";position:absolute;bottom:-10px;left:50%;transform:translate(-50%);height:10px;width:50px;border-radius:20px;background-color:#fdf9de;box-shadow:0 10px 0 #fdf9de,0 20px 0 #fdf9de,0 30px 0 #fdf9de,0 40px 0 #fdf9de,0 50px 0 #fdf9de,0 60px 0 #fdf9de,0 70px 0 #fdf9de,0 80px 0 #fdf9de,0 90px 0 #fdf9de,0 100px 0 #fdf9de,0 110px 0 #fdf9de,0 120px 0 #fdf9de,0 130px 0 #fdf9de,0 140px 0 #fdf9de,0 150px 0 #fdf9de,0 160px 0 #fdf9de,0 170px 0 #fdf9de}.main .cat{width:110px;height:50px;position:absolute;top:calc(50% - 50px);right:130px;border-top-left-radius:100px;border-top-right-radius:100px}.main .cat .body{width:110px;height:50px;background-color:#745260;position:absolute;border-top-left-radius:100px;border-top-right-radius:100px;animation:body 12s none infinite}.main .cat .head{content:"";width:70px;height:35px;background-color:#745260;position:absolute;top:calc(50% - 10px);left:-40px;border-top-left-radius:80px;border-top-right-radius:80px}.tail-container{position:absolute;right:0;bottom:-13px;z-index:3}.tail{position:absolute;height:30px;width:14px;bottom:-10px;right:0;border-bottom-right-radius:5px;background:#745260;z-index:0}.tail>.tail{animation:tail 12s none infinite;height:100%;width:14px;transform-origin:left;border-bottom-left-radius:20px 20px;border-bottom-right-radius:20px 20px;border-top-right-radius:40px}.ear{position:absolute;left:4px;top:-4px;width:0;height:0;border-left:12px solid transparent;border-right:12px solid transparent;border-bottom:20px solid #745260;transform:rotate(-30deg);animation:left-ear 12s both infinite}.ear+.ear{animation:right-ear 12s both infinite;top:-12px;left:30px}.nose{position:absolute;bottom:10px;left:-10px;background-color:#fd6e72;height:5px;width:5px;border-radius:50%}.whisker-container{position:absolute;bottom:5px;left:-36px;width:20px;height:10px;transform-origin:right;animation:left-whisker 12s both infinite}.whisker-container:nth-child(2){left:-20px;bottom:12px;transform-origin:right;transform:rotate(180deg);animation:right-whisker 12s both infinite}.whisker{position:absolute;top:0;width:100%;border:1px solid #fdf9de;transform-origin:100% 0;transform:rotate(10deg)}.whisker:last-child{top:0;transform:rotate(-20deg)}
      </style>
  </head>
  <body><div style="height: 250px;width: 250px;overflow: hidden;display: inline-block;"><div class="main"><span class="stand"></span><div class="cat"><div class="body"></div><div class="head"><div class="ear"></div><div class="ear"></div></div><div class="face"><div class="nose"></div><div class="whisker-container"><div class="whisker"></div><div class="whisker"></div></div><div class="whisker-container"><div class="whisker"></div><div class="whisker"></div></div></div><div class="tail-container"><div class="tail"><div class="tail"><div class="tail"><div class="tail"><div class="tail"><div class="tail"><div class="tail"></div></div></div></div></div></div></div></div></div></div></div></body>
</html>
`;
  let html = `<div class="touch-fish" style="-webkit-user-select: none; width: 250px;height: 250px;z-index: 9998;position: fixed;right: 0;bottom: 0;">
      <iframe class="showHtml" style="width: 250px;height: 250px;position: absolute;border: none;">
      </iframe>
      <div class="touch-fish-toolbar" style="position: absolute;top: 0;left: 0;display:grid;z-index: 9999;">
         <span style="padding:10px;cursor:pointer" class="list" title="管理列表"><svg class="icon" style="width: 20px;height: 20px;vertical-align: middle;fill: currentColor;overflow: hidden;" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="496"><path d="M187.392 70.656q28.672 0 48.64 19.456t19.968 48.128l0 52.224q0 28.672-19.968 48.64t-48.64 19.968l-54.272 0q-27.648 0-47.616-19.968t-19.968-48.64l0-52.224q0-28.672 19.968-48.128t47.616-19.456l54.272 0zM889.856 70.656q27.648 0 47.616 19.456t19.968 48.128l0 52.224q0 28.672-19.968 48.64t-47.616 19.968l-437.248 0q-28.672 0-48.64-19.968t-19.968-48.64l0-52.224q0-28.672 19.968-48.128t48.64-19.456l437.248 0zM187.392 389.12q28.672 0 48.64 19.968t19.968 48.64l0 52.224q0 27.648-19.968 47.616t-48.64 19.968l-54.272 0q-27.648 0-47.616-19.968t-19.968-47.616l0-52.224q0-28.672 19.968-48.64t47.616-19.968l54.272 0zM889.856 389.12q27.648 0 47.616 19.968t19.968 48.64l0 52.224q0 27.648-19.968 47.616t-47.616 19.968l-437.248 0q-28.672 0-48.64-19.968t-19.968-47.616l0-52.224q0-28.672 19.968-48.64t48.64-19.968l437.248 0zM187.392 708.608q28.672 0 48.64 19.968t19.968 47.616l0 52.224q0 28.672-19.968 48.64t-48.64 19.968l-54.272 0q-27.648 0-47.616-19.968t-19.968-48.64l0-52.224q0-27.648 19.968-47.616t47.616-19.968l54.272 0zM889.856 708.608q27.648 0 47.616 19.968t19.968 47.616l0 52.224q0 28.672-19.968 48.64t-47.616 19.968l-437.248 0q-28.672 0-48.64-19.968t-19.968-48.64l0-52.224q0-27.648 19.968-47.616t48.64-19.968l437.248 0z" p-id="497"></path></svg></span>
          <span style="padding:10px;cursor:pointer" class="change" title="下一个"><svg class="icon" style="width: 20px;height: 20px;vertical-align: middle;fill: currentColor;overflow: hidden;" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="946"><path d="M810.752 205.397333a213.333333 213.333333 0 0 1 213.333333 213.333334v128h-85.333333v-128a128 128 0 0 0-128-128h-768a42.666667 42.666667 0 0 1-24.746667-77.397334L316.586667 0l49.493333 69.461333-190.293333 135.936h634.88z m-597.333333 597.333334a213.333333 213.333333 0 0 1-213.333334-213.333334v-128h85.333334v128a128 128 0 0 0 128 128h768a42.666667 42.666667 0 0 1 24.746666 77.397334l-298.666666 213.333333-49.493334-69.461333 190.293334-135.936H213.333333z" p-id="947"></path></svg></span>
      </div>
  </div>`;
  console.log('test')
  let config = function() {
      let cache = JSON.parse(localStorage.touchFishDatas || null);
      if (cache == null) {
          cache = {};
          cache['currUse'] = 'sleepCat';
          cache.resource = {};
          cache.resource['sleepCat'] = defaultHTML;
          updateLocalStorage();
      }

      function updateLocalStorage() {
          localStorage.touchFishDatas = JSON.stringify(cache);
      }
      return {
          add(key, html) {
              cache.resource[key] = html;
              updateLocalStorage();
          },
          remove(key) {
              delete cache.resource[key];
              updateLocalStorage();
          },
          change(key) {
              cache['currUse'] = key;
              changeHTML(cache.resource[key]);
              updateLocalStorage();
          },
          curr() {
              return cache.resource[cache['currUse']];
          },
          next() {
              let keys = Object.keys(cache.resource);
              let i = keys.indexOf(cache['currUse']);
              return cache.resource[keys[(i + 1) % keys.length]];
          },
          update(old, newK, html) {
              delete cache.resource[old];
              cache.resource[newK] = html;
              updateLocalStorage();
          },
          resource() {
              return cache.resource;
          },
          currKey(){
              return cache['currUse'];
          }
      }
  }()
  $('body').prepend(html)
  $('head').append(`<style>
  .touch-fish,
  .touch-fish iframe {
      width: 250px;
      height: 250px;
      z-index: 9998;
      position: absolute;
      right: 0;
      bottom: 0;
      border: none;
  }
  .touch-fish-toolbar>span {
      padding: 10px;
      cursor: pointer;
  }
  .tf-list>* {
      padding: 5px 10px;
      margin: 5px;
      border-radius: 4px;
      background: #eee;
      cursor: pointer;
      border: none;
  }
  .tf-btn-add{
      padding: 10px 5px;
      text-align: center;
      background: #00a9;
      cursor: pointer;
      color: #fff;
      font-weight: bold;
      border-radius: 20px;
      margin: 10px;
  }
  .tf-btn-use{
      background: #0a0a;
      color: #fff;
  }
</style>`)
  let delayFunc = function() {
      let time;
      return {
          delay(func, t = 1200) {
              window.clearTimeout(time);
              time = setTimeout(() => func(), t);
          }
      }
  }();
  $('.list').click(function() {
      let rols = `<div class="tf-list" style="">
          <input old="oldK" name="key" placeholder="描述" style="width: 100px;">
          <input name="html" placeholder="网页内容">
          <input name="useThis" type="button" value="启用">
          <input name="del" type="button" value="删除">
      </div>`;
      let html =
          `<div class="tf-m-page" style="background:#fff;position: fixed;z-index: 99999;top: calc(50vh - 200px);left: calc(50vw - 260px); width: 520px;height: 400px;overflow: auto;border: 1px solid orangered;padding: 10px;">
          <div class="tf-btn-add" style="">ADD</div>
          </div>`;
      $('body').prepend(html);

      {
          let rtime; // 定时器交互
          $('.tf-m-page').mouseleave(function() {
              rtime = setTimeout(() => $('.tf-m-page').remove(), 600)
          }).mouseenter(function() {
              window.clearTimeout(rtime)
          });
      }
      $('.tf-m-page').on('input propertychange', 'input', function() {
          console.log('inpu')
          let $t = $(this).parent().find('input');
          let old = $($t[0]).attr('old'),
              newK = $($t[0]).val(),
              h = $($t[1]).val();
          delayFunc.delay(() => {
              config.update(old, newK, h);
          })
      }).on('click', 'input[name="del"]', function() {
          $(this).parent().remove();
          config.remove($(this).prev('[name="key"]').val())
      }).on('click', 'input[name="useThis"]', function() {
          if ($(this).hasClass('tf-btn-use')) {
              return false;
          }
          $('.tf-list input[name="useThis"]').removeClass('tf-btn-use');
          $(this).addClass('tf-btn-use');
          config.change($(this).prev().prev().val())
      });
      $('.tf-btn-add').click(function() {
          $(this).parent().append(rols);
      })
      // 初始化表单数据
      Object.keys(config.resource()).forEach(k => {
          $('.tf-m-page').append(rols);
          let $input = $('.tf-m-page>.tf-list:last>input');
          $($input[0]).attr('old', k).val(k);
          $($input[1]).val(config.resource()[k]);
      })
      // 并选中当前的
      $(`input[old="${config.currKey()}"]`).next().next().addClass('tf-btn-use')
  })
  $('.change').click(function() {
      console.log('change');
      changeHTML(config.next());
  })
  changeHTML(config.curr());
  $('.touch-fish-toolbar').hide()
  $('.touch-fish').mouseover(function() {
      $('.touch-fish-toolbar').show();
  }).mouseout(function() {
      $('.touch-fish-toolbar').hide()
  });

  function changeHTML(html) {
      $('iframe[class="showHtml"]').contents().find('html').empty().append(html);
  }
}