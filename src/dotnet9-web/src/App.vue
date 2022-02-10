<template>
  <header>
    <div class="header-wrapper header-wrapper-default">
      <div class="header-container">
        <h1 class="header-community-logo">
          <router-link to="/">
            <span style="color: #18ad91">Dotnet9</span>
          </router-link>
        </h1>

        <nav class="header-menu">
          <router-link to="/BlogPost">
            <a class="x-link-a">问答</a>
          </router-link>
          <a class="x-link-a">文章</a>
        </nav>

        <div class="header-user">
          <div class="header-user-login" v-if="userInfo == null">
            <router-link to="/Login">
              <span class="user-login-btn">登录</span>
            </router-link>

            <i class="user-login-btn-line"></i>
            <router-link to="/Register">
              <span class="user-login-btn">注册</span>
            </router-link>
          </div>
          <div class="header-user-login" v-else>
            <a-row style=padding-left:32px>
              <a-col :span="6">
                <a-avatar>
                  <template #icon>
                    <img src="./assets/608144857.jpg">
                  </template>
                </a-avatar>
              </a-col>
              <a-col :span="18" style="line-height: 64px">
                <h2>{{ userInfo.userName }}</h2>
              </a-col>
            </a-row>
          </div>
        </div>

        <div class="header-entry">
          <router-link to="/BlogPostCreate">
            <a-radio-button value="small" style="margin-right: 20px">写文章</a-radio-button>
          </router-link>
          <a-radio-button value="small">提问</a-radio-button>
        </div>

        <div class="header-search">
          <a-input-search placeholder="请输入内容" style="width: 200px" />
        </div>

      </div>
    </div>
  </header>

  <router-view />
</template>

<script lang="ts">

import { defineComponent, onMounted, ref, reactive } from 'vue';
import store from '@/store';

export default defineComponent({
  name: 'App',
  setup() {
    let userInfo = ref(null);

    onMounted(() => {
      let tempStore = store;
      if (tempStore.state.token) {
        userInfo.value = JSON.parse(tempStore.state.userInfo!);
      }
    });

    return {
      userInfo,
    };
  }
});

</script>

<style lang="scss">

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  /* text-align: center;
  color: #2c3e50;
  margin-top: 60px; */
}

nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}

header {
  position: relative;
}

header {
  display: block;
}
.header-wrapper {
  height: 64px;
  background-color: #fff;
  box-shadow: 0 2px 5px 0 rgb(0 0 0 / 12%);
}
.header-container {
  position: relative;
  width: 1200px;
  margin: 0 auto;
}
.header-community-logo {
  float: left;
  margin: 0;
  padding-top: 10px;
  line-height: 1.4;
}
.header-community-logo img {
  height: 23px;
}
.x-link-a {
  color: inherit;
}

h1 {
  display: block;
  font-size: 2em;
  margin-block-start: 0.67em;
  margin-block-end: 0.67em;
  margin-inline-start: 0px;
  margin-inline-end: 0px;
  font-weight: bold;
}

.header-menu {
  float: left;
  line-height: 16px;
  margin-left: 40px;
}
.header-menu > a {
  font-size: 16px;
  color: #00223b;
  margin: 0 30px;
}

.header-user {
  float: right;
  line-height: 64px;
  font-size: 14px;
}
.header-user-info,
.header-user-login {
  width: 130px;
  text-align: right;
}
.user-login-btn-line {
  display: inline-block;
  width: 1px;
  height: 11px;
  background-color: #d8d8d8;
  margin: 0 11px;
}
.user-login-btn {
  cursor: pointer;
}
.header-entry {
  margin-left: 26px;
  float: right;padding-top: 16px;
}
.header-search {
  float: right;
  padding-top: 16px;;
}

</style>
