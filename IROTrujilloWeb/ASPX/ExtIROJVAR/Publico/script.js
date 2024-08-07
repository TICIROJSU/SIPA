document.getElementById('hablar').addEventListener("load",()=>{
	decir(document.getElementById("texto").value);
});

function decir(texto){
	speechSynthesis.speak(new SpeechSynthesisUtterance(texto));
}