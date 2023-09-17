jQuery(document).ready(function($){
	
   $('#tab li').click(function(){
   $(this).addClass('current').siblings().removeClass('current');
   $('.newstab>div:eq('+$(this).index()+')').show().siblings().hide();
    });  
	
});

   