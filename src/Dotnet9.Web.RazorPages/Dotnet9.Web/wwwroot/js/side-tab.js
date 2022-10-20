jQuery(document).ready(function($){
     		
	//side-tab
   $('.side-tab #sidetab li').click(function(){
   $(this).addClass('sidetab-current').siblings().removeClass('sidetab-current');
   $('#sidetab-content>section:eq('+$(this).index()+')').show().siblings().hide();
    });
	

	
});



