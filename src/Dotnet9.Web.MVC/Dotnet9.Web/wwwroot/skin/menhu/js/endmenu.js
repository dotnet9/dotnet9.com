jQuery(document).ready(function($){
	
  $(".endmenu li a").each(function(){
       $this = $(this);
       if($this[0].href==String(window.location)){
        $this.parent().addClass("act");
      }    
  });
	
	
	
});

  


