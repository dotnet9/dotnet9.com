import { defineStore } from "pinia";
import type { Component } from "vue";
import { useToast as toast, POSITION } from "vue-toastification";
export const useToast = defineStore("toast", () => {
  /**
   * 提示成功
   * @param message 消息内容
   * @param position 提示框位置
   * @param timeout 显示时长
   */
  const success = (
    message: string | Component,
    position: POSITION = POSITION.TOP_CENTER,
    timeout: number = 3000
  ) => {
    toast().success(message, {
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
    position: POSITION = POSITION.TOP_CENTER,
    timeout: number = 3000
  ) => {
    toast().error(message, {
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
    position: POSITION = POSITION.TOP_CENTER,
    timeout: number = 3000
  ) => {
    toast().warning(message, {
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
    position: POSITION = POSITION.TOP_CENTER,
    timeout: number = 3000
  ) => {
    toast().info(message, {
      position: position,
      timeout: timeout,
      hideProgressBar: true,
      closeButton: false,
    });
  };
  return { success, error, warning, info };
});
