import { defineStore } from "pinia";
import type { Component } from "vue";
import * as pkg from "vue-toastification";
export const useToast = defineStore("toast", () => {
  const { useToast, POSITION } = pkg;
  /**
   * 提示成功
   * @param message 消息内容
   * @param position 提示框位置
   * @param timeout 显示时长
   */
  const success = (
    message: string | Component,
    position = POSITION.TOP_CENTER,
    timeout: number = 3000
  ) => {
    useToast().success(message, {
      position: position,
      timeout: timeout,
      hideProgressBar: true,
      closeButton: false,
    });
  };

  /**
   * 提示错误
   * @param message 消息内容
   * @param position 提示框位置
   * @param timeout 显示时长
   */
  const error = (
    message: string | Component,
    position = POSITION.TOP_CENTER,
    timeout: number = 3000
  ) => {
    useToast().error(message, {
      position: position,
      timeout: timeout,
      hideProgressBar: true,
      closeButton: false,
    });
  };

  /**
   * 提示警告
   * @param message 消息内容
   * @param position 提示框位置
   * @param timeout 显示时长
   */
  const warning = (
    message: string | Component,
    position = POSITION.TOP_CENTER,
    timeout: number = 3000
  ) => {
    useToast().warning(message, {
      position: position,
      timeout: timeout,
      hideProgressBar: true,
      closeButton: false,
    });
  };

  /**
   * 提示
   * @param message 消息内容
   * @param position 提示框位置
   * @param timeout 显示时长
   */
  const info = (
    message: string | Component,
    position = POSITION.TOP_CENTER,
    timeout: number = 3000
  ) => {
    useToast().info(message, {
      position: position,
      timeout: timeout,
      hideProgressBar: true,
      closeButton: false,
    });
  };
  return { success, error, warning, info };
});
