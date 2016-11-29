// Write your Javascript code.

$(document).ready(function () {
    showform('captureformdiv');
});

function showform(target) {
    var captureformdiv = document.getElementById('captureformdiv');
    var resultsformdiv = document.getElementById('resultsformdiv');
    var registerformdiv = document.getElementById('registerformdiv');
    var loginformdiv = document.getElementById('loginformdiv');
    var recoverformdiv = document.getElementById('recoverformdiv');
    
    captureformdiv.style.display = 'none';
    resultsformdiv.style.display = 'none';
    registerformdiv.style.display = 'none';
    loginformdiv.style.display = 'none';
    recoverformdiv.style.display = 'none';

    document.getElementById(target).style.display = 'block';
}

//function showform(target) {
//    hideform('captureform');
//    document.getElementById(target).setAttribute('class', 'visible');
//}

//function hideform(target) {
//    document.getElementById(target).setAttribute('class', 'hidden');
//}

//function hideallforms() {
//    var captureformdiv = document.getElementById('captureformdiv');
//    var resultsformdiv = document.getElementById('resultsformdiv');
//    var registerformdiv = document.getElementById('registerformdiv');
//    var loginformdiv = document.getElementById('loginformdiv');
//    var recoverformdiv = document.getElementById('recoverformdiv');

//    captureformdiv.setAttribute('class', 'hidden');
//    resultsformdiv.setAttribute('class', 'hidden');
//    registerformdiv.setAttribute('class', 'hidden');
//    loginformdiv.setAttribute('class', 'hidden');
//    recoverformdiv.setAttribute('class', 'hidden');
//}

//$('#showcaptureform').click(function () {
//    hideallforms();
//    captureformdiv.setAttribute('class', 'visible');
//});

//$('#showresultsform').click(function () {
//    hideallforms();
//    resultsformdiv.setAttribute('class', 'visible');
//});

//$('#showregisterform').click(function () {
//    hideallforms();
//    registerformdiv.setAttribute('class', 'visible');
//});

//$('#showloginform').click(function () {
//    hideallforms();
//    loginformdiv.setAttribute('class', 'visible');
//});

//$('#showform').click(function () {
//    console.log("form value is:  " + ($(this).val()));
//    if ($(this).val() == "signin") {
//        $("#captureformdiv").hide();
//        $("#loginformdiv").show();
//    }
//    else if ($(this).val() == "signup") {
//        $("#captureformdiv").hide();
//        $("#registerformdiv").show();
//    }
//});

//$('#showrecoverform').click(function () {
//    hideallforms();
//    recoverformdiv.setAttribute('class', 'visible');
//});

//$('#resultsform-to-captureform').click(function () {
//    var captureformdiv = document.getElementById('captureformdiv');
//    var resultsformdiv = document.getElementById('resultsformdiv');

//    captureformdiv.setAttribute('class', 'visible');
//    resultsformdiv.setAttribute('class', 'hidden');
//});


//$('#captureform-to-registerform').click(function () {
//    var captureformdiv = document.getElementById('captureformdiv');
//    var registerformdiv = document.getElementById('registerformdiv');

//    registerformdiv.setAttribute('class', 'visible');
//    captureformdiv.setAttribute('class', 'hidden');
//});

//$('#captureform-to-loginform').click(function () {
//    var captureformdiv = document.getElementById('captureformdiv');
//    var loginformdiv = document.getElementById('loginformdiv');

//    loginformdiv.setAttribute('class', 'visible');
//    captureformdiv.setAttribute('class', 'hidden');
//});

//$('#loginform-to-registerform').click(function () {
//    var registerformdiv = document.getElementById('registerformdiv');
//    var loginformdiv = document.getElementById('loginformdiv');

//    loginformdiv.setAttribute('class', 'hidden');
//    registerformdiv.setAttribute('class', 'visible');
//});

//$('#registerform-to-loginform').click(function () {
//    var registerformdiv = document.getElementById('registerformdiv');
//    var loginformdiv = document.getElementById('loginformdiv');

//    loginformdiv.setAttribute('class', 'visible');
//    registerformdiv.setAttribute('class', 'hidden');
//});

//$('#loginform-to-recoverform').click(function () {
//    var loginformdiv = document.getElementById('loginformdiv');
//    var recoverformdiv = document.getElementById('recoverformdiv');

//    recoverformdiv.setAttribute('class', 'visible');
//    loginformdiv.setAttribute('class', 'hidden');
//});

//$('#resultsform-to-loginform').click(function () {
//    var resultsformdiv = document.getElementById('resultsformdiv');
//    var loginformdiv = document.getElementById('loginformdiv');

//    loginformdiv.setAttribute('class', 'visible');
//    resultsformdiv.setAttribute('class', 'hidden');
//});

//$('#recoverform-to-loginform').click(function () {
//    var recoverformdiv = document.getElementById('recoverformdiv');
//    var loginformdiv = document.getElementById('loginformdiv');

//    loginformdiv.setAttribute('class', 'visible');
//    recoverformdiv.setAttribute('class', 'hidden');
//});

//$('#recoverform-to-registerform').click(function () {
//    var recoverformdiv = document.getElementById('recoverformdiv');
//    var registerformdiv = document.getElementById('registerformdiv');

//    registerformdiv.setAttribute('class', 'visible');
//    recoverformdiv.setAttribute('class', 'hidden');
//});

// Register and signin
//$('#loginform-to-recover').click(function () {
//    $('#recoverformdiv').removeClass('hidden');
//    $("#loginform").slideUp();
//    $("#recoverform").fadeIn();
//});

$('#captureform-to-resultsform').click(function () {
    showform('resultsformdiv');

    var results1div = document.getElementById('results1div');
    var results2div = document.getElementById('results2div');
    var results3div = document.getElementById('results3div');
    var results4div = document.getElementById('results4div');
    var slider = document.getElementById('slidervalue');

    results1div.setAttribute('class', 'hidden');
    results2div.setAttribute('class', 'hidden');
    results3div.setAttribute('class', 'hidden');
    results4div.setAttribute('class', 'hidden');

    if (slider.value <= 50) {
        results1div.setAttribute('class', 'visible');
    }
    else if ((slider.value > 50) && (slider.value <= 70)) {
        results2div.setAttribute('class', 'visible');
    }
    else if ((slider.value > 70) && (slider.value <= 90)) {
        results3div.setAttribute('class', 'visible');
    }
    else {
        results4div.setAttribute('class', 'visible');
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

$("#btnRegisterEvaluation").click(function () {

    var oForm = document.forms["resultsform"];

    var usernameval = oForm.elements["Register.Email"].value;
    var passwordval = oForm.elements["Register.Password"].value;
    var isfirsttimefilingval = $('input[name=isfirsttimefiling]:checked').val();
    var hasclaimwithvaval = $('input[name=hasclaimwithva]:checked').val();
    var hasactiveappealval = $('input[name=hasactiveappeal]:checked').val();
    var hasratingval = $('input[name=hasrating]:checked').val();
    var slidervalueval = document.forms["captureform"].elements["slidervalue"].value;;

    var status = $("#divStatusRegisterEvaluation"); //DIV object to display the status message
    status.html("Processing ....") //While our Thread works, we will show some message to indicate the progress

    //jQuery AJAX Post request
    $.post("/Account/RegisterEvaluation", { username: usernameval, password: passwordval, isfirsttimefiling: isfirsttimefilingval, hasclaimwithva: hasclaimwithvaval, hasactiveappeal: hasactiveappealval, hasratingval: hasratingval, slidervalue: slidervalueval },
        function (data) {
            if (data == "true") {
                status.html(name + " is available!");
            } else {
                status.html("It seems you have an account with us already!");
            }
        });
});

$("#btnRegister").click(function () {
    var oForm = document.forms["registerform"];

    var usernameval = oForm.elements["Register.Email"].value;
    var passwordval = oForm.elements["Register.Password"].value;
    
    var status = $("#divStatusRegister"); //DIV object to display the status message
    status.html("Processing ....") //While our Thread works, we will show some message to indicate the progress

    //jQuery AJAX Post request
    $.post("/Account/Register", { username: usernameval, password: passwordval },
        function (data) {
            if (data == "true") {
                //status.html(name + " is available!");
            } else {
                status.html("It seems you have an account with us already!");
            }
        });
});

$("#btnLogin").click(function () {
    var oForm = document.forms["loginform"];

    var usernameval = oForm.elements["Register.Email"].value;
    var passwordval = oForm.elements["Register.Password"].value;

    var status = $("#divStatusLogin"); //DIV object to display the status message
    status.html("Processing ....") //While our Thread works, we will show some message to indicate the progress

    //jQuery AJAX Post request
    $.post("/Account/Register", { username: usernameval, password: passwordval },
        function (data) {
            if (data == "true") {
                //status.html(name + " is available!");
            } else {
                status.html("Could not sign in.  Please try again!");
            }
        });
});

$("#btnTestSmallModal").click(function () {
    //$('#myModal').modal('show');
    $('#smallmodal').modal('show');
});