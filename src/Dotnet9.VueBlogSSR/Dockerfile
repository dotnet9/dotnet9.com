# 使用Node.js 基础镜像
FROM node:lts-alpine

WORKDIR /app

RUN yarn config set registry https://registry.npmmirror.com/ #设置镜像源

COPY . .

RUN yarn install&&yarn run build

ENV NODE_ENV production

ENV PORT 5200

EXPOSE 5200

ENTRYPOINT ["yarn","run","server"]
