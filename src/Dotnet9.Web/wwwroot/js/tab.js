jQuery(document).ready(function($){
	
   $('#tab li').click(function(){
   $(this).addClass('current').siblings().removeClass('current');
   $('.content-box>section:eq('+$(this).index()+')').show().siblings().hide();
    });  
	
});

 

