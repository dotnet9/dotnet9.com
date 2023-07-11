jQuery(document).ready(function($){
  
	 var obj=null;
    var As=document.getElementById('starlist').getElementsByTagName('a');
    obj = As[0];
    for(i=1;i<As.length;i++){if(window.location.href.indexOf(As[i].href)>=0)
    obj=As[i];}
    obj.id='selected';		

	  //nav
    $("#mnavh").on('click', function (event) {
        $("#starlist").toggle();
	    $("#mnavh").toggleClass("open");
	    $("body").toggleClass("ovhi");
	});
	
	
	 $(window).scroll(function() {
        var h = $("body").height() - window.getHeight();
        //console.log(h);
        if ($(window).scrollTop() > 80 && h > 120) {
            $(".topnav").addClass("is-fixed").find("").fadeOut(400);			
		

        } else if ($(window).scrollTop() < 80) {
            $(".topnav").removeClass("is-fixed").find("").fadeIn(400);
		

        }
    });
	   //scroll to top
        var offset = 300,
        offset_opacity = 1200,
        scroll_top_duration = 700,
        $back_to_top = $('.icon-top');
		
    $(window).scroll(function () {
        ($(this).scrollTop() > offset) ? $back_to_top.addClass('cd-is-visible') : $back_to_top.removeClass('cd-is-visible cd-fade-out');
        if ($(this).scrollTop() > offset_opacity) {
            $back_to_top.addClass('cd-fade-out');
        }
    });
	 $back_to_top.on('click', function (event) {
        event.preventDefault();
        $('body,html').animate({
                scrollTop: 0,
            }, scroll_top_duration
        );
    });

	
});


   window.getHeight = function() {
    if (window.innerHeight != undefined) {
        return window.innerHeight;
    } else {
        var B = document.body
          , D = document.documentElement;
        return Math.min(D.clientHeight, B.clientHeight);
    }
}

window.windowInit = function () {
    $("#mnavh").on('click', function (event) {
        $("#starlist").toggle();
        $("#mnavh").toggleClass("open");
        $("body").toggleClass("ovhi");
    });
}

function handlePseudoLinks() {
    var pseudoLinks = document.querySelectorAll('a[href^="#"]');
    pseudoLinks.forEach(function (link) {
        var href = link.getAttribute('href');
        var absoluteUri = window.location.origin + window.location.pathname + href;
        link.setAttribute('href', absoluteUri);
    });
}

window.scrollToTop = function () {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
}

window.pictureZoom = function () {
    // 只在文章内的img标签添加点击放大事件
    const div = document.getElementById('details-content');

    // 获取所有的img标签
    const imgs = div.querySelectorAll('img');

    // 遍历所有的img标签
    for (let i = 0; i < imgs.length; i++) {
        // 给每个img标签添加点击事件
        imgs[i].addEventListener('click', function () {
            // 创建一个新的div容器
            const container = document.createElement('div');
            container.style.position = 'fixed';
            container.style.top = '0';
            container.style.left = '0';
            container.style.width = '100%';
            container.style.height = '100%';
            container.style.backgroundColor = 'rgba(0, 0, 0, 0.8)';
            container.style.zIndex = '9999';
            container.style.cursor = 'zoom-out';

            // 创建一个新的img标签
            const newImg = document.createElement('img');
            newImg.src = this.src;
            newImg.style.position = 'absolute';
            newImg.style.top = '50%';
            newImg.style.left = '50%';
            newImg.style.transform = 'translate(-50%, -50%)';
            newImg.style.maxWidth = '90%';
            newImg.style.maxHeight = '90%';
            newImg.style.objectFit = 'contain';

            // 将新img标签和按钮添加到容器中
            container.appendChild(newImg);

            // 将容器添加到body中
            document.body.appendChild(container);

            let scale = 1; // 初始化缩放比例
            container.addEventListener('wheel', function (event) {
                event.preventDefault();
                const delta = event.deltaY > 0 ? 0.9 : 1.1;
                scale *= delta; // 累积缩放比例
                newImg.style.transform = `scale(${scale})`;
            });

            // 点击容器时移除容器
            container.addEventListener('click', function () {
                document.body.removeChild(container);
            });
        });
    }
}