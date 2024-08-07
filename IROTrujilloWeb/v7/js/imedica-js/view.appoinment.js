
$('form.app-form .error').hide();
$('#appointmentSuccess').hide();
$('#appointmentError').hide();

$('form.app-form').on('submit', function(event) {

	var a = b = c = true;
	var fname = $('form.app-form #apt-first-name').val();
	var phone = $('form.app-form #apt-phno').val();
	var email = $('form.app-form #apt-email').val();

	if(phone == '' || !$.isNumeric( phone )) {
		$('form.app-form .apt-phno-error').show();
		a = false;
	} else {
		$('form.app-form .apt-phno-error').hide();
	}

	if(fname == '') {
		$('form.app-form .apt-first-name-error').show();
		b = false;
	} else {
		$('form.app-form .apt-first-name-error').hide();
	}

	var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
	var emailTest = regex.test(email);
	if(email == '' || emailTest == 'false') {
		$('form.app-form .apt-email-error').show();
		c = false;
	} else {
		$('form.app-form .apt-email-error').hide();
	}
	
	//
	if( a && b && c ) {
	    var data = $(this).serialize();
	    // console.log( $(this).serialize());
	    $.ajax({
	            url: 'php/appoinment-form.php',
	            type: 'POST',
	            data: data
	        })
	        .done(function(data) {
	        	$('#appointmentSuccess').show();
	        	$('#appointmentError').hide();
	            console.log(data);
	        })
	        .fail(function() {
			    $('#appointmentSuccess').hide();
			    $('#appointmentError').show();
	            console.log("error");
	        });

	    event.preventDefault();
	    // Act on the event 
		return true;
	} else {
		return false;
	}

});
