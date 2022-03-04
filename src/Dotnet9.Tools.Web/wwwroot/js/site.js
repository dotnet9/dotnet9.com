window.setCookie = function(name, value) {
    document.cookie = `${name}=${escape(value.toString())};path=/;}`;
};

window.getCookie = function(name) {
    const reg = new RegExp(`(^| )${name}=([^;]*)(;|$)`);
    const arr = document.cookie.match(reg);
    if (arr) {
        return unescape(arr[2]);
    }
    return null;
};

async function downloadFileFromStream(fileName, contentStreamReference) {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);

    const url = URL.createObjectURL(blob);

    triggerFileDownload(fileName, url);

    URL.revokeObjectURL(url);
}

function triggerFileDownload(fileName, url) {
    const anchorElement = document.createElement("a");
    anchorElement.href = url;

    if (fileName) {
        anchorElement.download = fileName;
    }

    anchorElement.click();
    anchorElement.remove();
}

function loadJs(sourceUrl) {
    if (sourceUrl.Length == 0) {
        console.error("Invalid source URL");
        return;
    }

    var tag = document.createElement("script");
    tag.src = sourceUrl;
    tag.type = "text/javascript";

    tag.onload = function() {
        console.log("Script loaded successfully");
    };

    tag.onerror = function() {
        console.error("Failed to load script");
    };

    document.body.appendChild(tag);
}

function write(content) { document.write(content); }