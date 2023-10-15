## 配置

设置全局配置

```shell
git config --global user.name "[name]"
git config --global user.email "[email]"
```

## 开始使用

创建 git 存储库

```shell
git init
```

克隆现有 git 存储库

```shell
git clone [url]
```

## 提交（Commit）

提交所有修订-Commit all tracked changes

```shell
git commit -am "[commit message]"
```

向上次提交添加新修改-Add new modifications to the last commit

```shell
git commit --amend --no-edit
```

## 我犯了一个错误-I’ve made a mistake

更改上次提交消息-Change last commit message

```shell
git commit --amend
```

撤消最近的提交并保留更改-Undo most recent commit and keep changes

```shell
git reset HEAD~1
```

撤消最近的提交并保留更改N-Undo the `N` most recent commit and keep changes

```shell
git reset HEAD~N
```

撤消最近的提交并删除更改-Undo most recent commit and get rid of changes

```shell
git reset HEAD~1 --hard
```

将分支重置为远程状态-Reset branch to remote state

```shell
git fetch origin
git reset --hard origin/[branch-name]
```

## 杂项-Miscellaneous

将本地主分支重命名为主分支-Renaming the local master branch to main

```shell
git branch -m master main
```
