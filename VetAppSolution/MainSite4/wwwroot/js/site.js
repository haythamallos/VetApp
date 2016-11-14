// Write your Javascript code.

// Register and signin
$('#loginform-to-recover').click(function () {
    $('#recoverformdiv').removeClass('hidden');
    $("#loginform").slideUp();
    $("#recoverform").fadeIn();
});

$('#loginform-to-signup').click(function () {
    $('#registerformdiv').removeClass('hidden');
    $("#loginform").slideUp();
    $("#registerform").fadeIn();
});

$('#recoverform-to-loginform').click(function () {
    $('#loginformdiv').removeClass('hidden');
    $("#recoverform").slideUp();
    $("#loginform").fadeIn();
});

$('#recoverform-to-registerform').click(function () {
    $('#registerformdiv').removeClass('hidden');
    $("#recoverform").slideUp();
    $("#registerform").fadeIn();
});

$('#registerform-to-loginform').click(function () {
    $('#loginformdiv').removeClass('hidden');
    $("#registerform").slideUp();
    $("#loginform").fadeIn();
});