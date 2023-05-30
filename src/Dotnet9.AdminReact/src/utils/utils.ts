function formatDate(value: string) {
    const date = new Date(value);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const seconds = String(date.getSeconds()).padStart(2, '0');
    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
}

function downloadFile(url: string) {
    // 创建一个a标签
    var a = document.createElement('a');
    // 设置a标签的href属性为要下载的文件的URL
    a.href = url;
    // 设置a标签的download属性为文件名
    a.download = url.substring(url.lastIndexOf('/') + 1);
    // 将a标签添加到页面中
    document.body.appendChild(a);
    // 触发a标签的点击事件，开始下载文件
    a.click();
    // 将a标签从页面中移除
    document.body.removeChild(a);
}

export {
    formatDate,
    downloadFile
}