// setting Speech Synthesis
let text_to_speech = new SpeechSynthesisUtterance();
let synth = window.speechSynthesis;
$(function() {
    // SpeechSynthesisUtterance Configuration
    text_to_speech.lang = 'en';
    text_to_speech.voice = synth.getVoices()[0];

    // An Event that identifies the part of text has spoken
    text_to_speech.onboundary = function(e) {
        var message = $('#display').text()
        var b_text = String(message).substring(0, e.charIndex + e.charLength)
        var marked = $('<mark>')
        marked.text(b_text)
        $('#display').html('')
        $('#display').append(marked)
        $('#display').append(message.replace(b_text, ""))
    }

    //$('#text-form').submit(function(e) {
      //  e.preventDefault();
        // Set Text to be spoken
        text_to_speech.text = $('#message').val();
        $('#display').text($('#message').val())
        $('#message').val('')
            // Start to Speak the text
        synth.speak(text_to_speech)
        $('#display').animate({ scrollTop: 0 }, 'fast')
   // })
})