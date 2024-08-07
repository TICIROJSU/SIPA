/*
var canvasP = document.createElement("canvas");
 
      var ctxP = canvasP.getContext("2d");
         if (ctxP) {
             // creamos el patrón.
            ctxP.canvas.width = 20;
            ctxP.canvas.height = 20;   
            ctxP.rect(0, 0, 10, 10);
            ctxP.rect(10, 10, 20, 20);
            ctxP.fillStyle = "#d9d9d9";
            ctxP.fillRect(0, 0, 10, 10);
            ctxP.fillRect(10, 10, 20, 20);
            }
			
      var canvas = document.getElementsByClassName("lienzo"); 
      var ctx;
      for( var i = 0; i < canvas.length; i++){
      if (canvas[i] && canvas[i].getContext) {
      ctx = canvas[i].getContext("2d");
         if (ctx) {console.log("Habemus context")
            ctx.restore();
            // aplicamos el patrón
            ctx.fillStyle = ctx.createPattern(canvasP,"repeat");
            ctx.fillRect(0,0, canvas[i].width, canvas[i].height);
            ctx.save();   
         }
      }
   }
  */ 




   
/*    
var canvas = document.querySelector("#canvas");
var X,Y,W,H,r;              
canvas.height = 250; 
function inicializarCanvas(){ 
  if (canvas && canvas.getContext) {
    var ctx = canvas.getContext("2d");
        if (ctx) {
			 var s = getComputedStyle(canvas);
			 var w = s.width;
			 var h = s.height;
					
			 W = canvas.width = w.split("px")[0];
			 H = canvas.height = h.split("px")[0];
			 
			 X = Math.floor(W/2);
			 Y = Math.floor(H/2);
			 r = Math.floor(W/3);
			   
			 dibujarEnElCanvas(ctx);
			 }
		}
	}
	  	   
function dibujarEnElCanvas(ctx){
  ctx.strokeStyle = "#006400";
  ctx.fillStyle = "#6ab155";
  ctx.lineWidth = 5;
  ctx.arc(X,Y,r,0,2*Math.PI);
  ctx.fill();
  ctx.stroke();
}
        

setTimeout(function() {
  inicializarCanvas();
  addEventListener("resize", inicializarCanvas);
  }, 15);
 */