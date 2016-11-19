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

$('#captureform-to-resultsform').click(function () {
    var captureformdiv = document.getElementById('captureformdiv');
    var resultsformdiv = document.getElementById('resultsformdiv');

    captureformdiv.setAttribute('class', 'hidden');
    resultsformdiv.setAttribute('class', 'visible');

    var results1div = document.getElementById('results1div');
    var results2div = document.getElementById('results2div');
    var slider = document.getElementById('slidervalue');

    results1div.setAttribute('class', 'hidden');
    results2div.setAttribute('class', 'hidden');
    results3div.setAttribute('class', 'hidden');
    results4div.setAttribute('class', 'hidden');

    if (slider.value <= 50)
    {
        results1div.setAttribute('class', 'visible');
    }
    else if ( (slider.value > 50) && (slider.value <= 70))
    {
        results2div.setAttribute('class', 'visible');
    }
    else if ((slider.value > 70) && (slider.value <= 90))
    {
        results3div.setAttribute('class', 'visible');
    }
    else
    {
        results4div.setAttribute('class', 'visible');
    }


});

$('#resultsform-to-captureform').click(function () {
    var captureformdiv = document.getElementById('captureformdiv');
    var resultsformdiv = document.getElementById('resultsformdiv');

    captureformdiv.setAttribute('class', 'visible');
    resultsformdiv.setAttribute('class', 'hidden');
});

$('#captureform-to-loginform').click(function () {
    $('#loginformdiv').removeClass('hidden');
    $("#captureform").slideUp();
    $("#loginform").fadeIn();
});

$('#captureform-to-registerform').click(function () {
    $('#registerformdiv').removeClass('hidden');
    $("#captureform").slideUp();
    $("#registerform").fadeIn();
});

$('#resultsform-to-loginform').click(function () {
    var loginformdiv = document.getElementById('loginformdiv');
    var resultsformdiv = document.getElementById('resultsformdiv');

    loginformdiv.setAttribute('class', 'visible');
    resultsformdiv.setAttribute('class', 'hidden');
});

/* Sliders */
// With JQuery
$('#ex1').slider({
    formatter: function (value) {
        return 'Current value: ' + value;
    }
});

$("#range_rating").ionRangeSlider({
    type: "single",
    grid: true,
    min: 10,
    max: 100,
    prefix: "Rating ",
    postfix: "%",
    values: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100],
     onChange: function (data) {
         //console.log(data.from);
         var realslidervalue = (data.from + 1) * 10;
         $('#slidervalue').val(realslidervalue);
    }
    //onFinish: function (data) {
    //    console.log("onFinish");
    //},
    //onUpdate: function (data) {
    //    console.log("onUpdate");
    //}
});
