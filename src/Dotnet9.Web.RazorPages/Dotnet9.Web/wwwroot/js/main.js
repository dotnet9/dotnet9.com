jQuery(document).ready(function($){
     
	//nav 
	$("#mnavh").click(function() {
        $("#starlist").toggle();
        $("#mnavh").toggleClass("open");
        $(".sub").hide();
        //$(".sub").first().show();
    });
			
    var obj=null;
    var As=document.getElementById('starlist').getElementsByTagName('a');
    obj = As[0];
    for(i=1;i<As.length;i++){if(window.location.href.indexOf(As[i].href)>=0)
    obj=As[i];}
    obj.id='selected';
	
	 $(window).scroll(function() {
        var h = $("body").height() - window.getHeight();
        //console.log(h);
        if ($(window).scrollTop() > 28 && h > 120) {
            $(".topnav").addClass("is-fixed").find("").fadeOut(400);			
		

        } else if ($(window).scrollTop() < 28) {
            $(".topnav").removeClass("is-fixed").find("").fadeIn(400);
		

        }
    });
 
	


  //nav menu
  
  $(".menu").click(function(event) {	
  $(this).children('.sub').slideToggle();
  $(this).siblings('.menu').children('.sub').slideUp('');	
   event.stopPropagation()
   });
   $(".menu a").click(function(event) {
   event.stopPropagation(); 
   });
   $(".sub li").click(function(event) {
   event.stopPropagation(); 
   });
	
	//toolbar	
	$(".toolbar-open").click(function(){
	$(".toolbar-open").addClass("openviewd");
	$(".toolbar").addClass("closed");
	});
	
    $("#closed").click(function(){
	$(".toolbar-open").removeClass("openviewd");
	$(".toolbar").removeClass("closed");
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
	
	
//search
	$(".is-search").click(function() {
        $(".search-page").toggle();
    });	
	$(".go-left").click(function() {
        $(".search-page").toggle();
    });
	


 $(".endmenu li a").each(function(){
       $this = $(this);
       if($this[0].href==String(window.location)){
        $this.parent().addClass("act");
      }    
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




