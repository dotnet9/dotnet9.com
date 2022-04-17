jQuery(document).ready(function($){
     
var obj=null;
var As=document.getElementById('pageContents').getElementsByTagName('a');
obj = As[0];
for(i=1;i<As.length;i++){if(window.location.href.indexOf(As[i].href)>=0)
obj=As[i];}
obj.id='pagecurrent'
	

	
});

  
