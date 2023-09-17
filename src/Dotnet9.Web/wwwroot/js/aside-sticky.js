jQuery(document).ready(function($){
     
//aside
    var Sticky = new hcSticky('aside', {
      stickTo: 'article',
      innerTop: 200,
      followScroll: false,
      queries: {
        480: {
          disable: true,
          stickTo: 'body'
        }
      }
    });
		
});

   