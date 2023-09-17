jQuery(document).ready(function($){
     		
	//tab-num01
   $('.tab-num01 #tab li').click(function(){
   $(this).addClass('tab-current').siblings().removeClass('tab-current');
   $('.tab-num01 #tab-content>section:eq('+$(this).index()+')').show().siblings().hide();
    });
	

 	//tab-num02 pic
   $('.tab-num02 #tab li').click(function(){
   $(this).addClass('tab-current').siblings().removeClass('tab-current');
   $('.tab-num02 #tab-content>section:eq('+$(this).index()+')').show().siblings().hide();
    }); 
	
	
	//tab-num03 news
   $('.tab-num03 #tab li').click(function(){
   $(this).addClass('tab-current').siblings().removeClass('tab-current');
   $('.tab-num03 #tab-content>section:eq('+$(this).index()+')').show().siblings().hide();
    }); 
	
	
	 //tab-num04 videos
   $('.tab-num04 #tab li').click(function(){
   $(this).addClass('tab-current').siblings().removeClass('tab-current');
   $('.tab-num04 #tab-content>section:eq('+$(this).index()+')').show().siblings().hide();
    });	
	
	
	//tab-num05 wenzi
   $('.tab-num05 #tab li').click(function(){
   $(this).addClass('tab-current').siblings().removeClass('tab-current');
   $('.tab-num05 #tab-content>section:eq('+$(this).index()+')').show().siblings().hide();
    });	



	//tab-num06 ziyuan
   $('.tab-num06 #tab li').click(function(){
   $(this).addClass('tab-current').siblings().removeClass('tab-current');
   $('.tab-num06 #tab-content>section:eq('+$(this).index()+')').show().siblings().hide();
    });			
	
});



