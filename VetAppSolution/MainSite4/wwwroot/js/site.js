// Write your Javascript code.



$(document).ready(function () {
    showform('captureformdiv');
});


var themes = {
    //"default": "//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css",
    "default": "~/lib/bootstrap/dist/css/bootstrap.min.css",
    "cerulean": "//bootswatch.com/cerulean/bootstrap.min.css",
    "cosmo": "//bootswatch.com/cosmo/bootstrap.min.css",
    "cyborg": "//bootswatch.com/cyborg/bootstrap.min.css",
    "flatly": "//bootswatch.com/flatly/bootstrap.min.css",
    "journal": "//bootswatch.com/journal/bootstrap.min.css",
    "readable": "//bootswatch.com/readable/bootstrap.min.css",
    "simplex": "//bootswatch.com/simplex/bootstrap.min.css",
    "slate": "//bootswatch.com/slate/bootstrap.min.css",
    "spacelab": "//bootswatch.com/spacelab/bootstrap.min.css",
    "united": "//bootswatch.com/united/bootstrap.min.css"
    //etc... add your stylesheet from http://bootswatch.com/
    //example:
    // "ADDNAME": "//bootswatch.com/ADDNAME/bootstrap.min.css"
}

//switches
$(function () {
    var themesheet = $('<link href="' + themes['default'] + '" rel="stylesheet" />');
    themesheet.appendTo('head');
    $('.theme-link').click(function () {
        var themeurl = themes[$(this).attr('data-theme')];
        themesheet.attr('href', themeurl);
    });
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


function login(usernameval, passwordval) {

    //jQuery AJAX Post request
    $.post("/Account/Authenticate", { username: usernameval, password: passwordval },
        function (data) {
            if (!data) {
                $('#smallmodalinvalidlogin').modal('show');
            }
            else {
                $.ajax({
                    type: "GET",
                    url: "/Account/Login",
                    data: { username: usernameval, password: passwordval },
                    datatype: "JSON",
                    contentType: "application/json; charset=utf-8",
                    success: function (returndata) {
                        if (returndata.ok)
                            window.location = returndata.newurl;
                        else
                            $('#smallmodalinvalidlogin').modal('show');
                    }
                });
            }
        });
}

function register(usernameval, passwordval) {
    //jQuery AJAX Post request
    $.get("/Account/Register", { username: usernameval, password: passwordval },
        function (data) {
            if (!data.ok) {
                $('#smallmodaluserexist').modal('show');
            } else {
                //$('#smallmodalerror').modal('show');
                window.location = data.newurl;
            }
        });
}

$("#btnRegisterEvaluation").click(function () {

    var oForm = document.forms["resultsform"];

    var usernameval = oForm.elements["Register.Username"].value;
    var passwordval = oForm.elements["Register.Password"].value;
    //var isfirsttimefilingval = $('input[name=isfirsttimefiling]:checked').val();
    //var hasclaimwithvaval = $('input[name=hasclaimwithva]:checked').val();
    //var hasactiveappealval = $('input[name=hasactiveappeal]:checked').val();
    //var hasratingval = $('input[name=hasrating]:checked').val();
    //var slidervalueval = document.forms["captureform"].elements["slidervalue"].value;;

  
    register(usernameval, passwordval);

    //jQuery AJAX Post request
    //$.post("/Account/RegisterEvaluation", { username: usernameval, password: passwordval, isfirsttimefiling: isfirsttimefilingval, hasclaimwithva: hasclaimwithvaval, hasactiveappeal: hasactiveappealval, hasratingval: hasratingval, slidervalue: slidervalueval },
    //    function (data) {
    //        if (data) {
    //            status.html(name + " is available!");
    //        } else {
    //            status.html("It seems you have an account with us already!");
    //        }
    //    });
});

$("#btnRegister").click(function () {
    var oForm = document.forms["registerform"];
    var usernameval = oForm.elements["Register.Username"].value;
    var passwordval = oForm.elements["Register.Password"].value;
    register(usernameval, passwordval);
});

$("#btnLogin").click(function () {
    var oForm = document.forms["loginform"];
    var usernameval = oForm.elements["Login.Username"].value;
    var passwordval = oForm.elements["Login.Password"].value;
    login(usernameval, passwordval);
});
//$.post("/Account/Login", { username: usernameval, password: passwordval },
//     function (data) {
//         console.log("url is:  " + returndata.newurl);
//         if (data.ok) {
//             window.location = data.newurl;
//         }
//         else
//         {
//             $('#smallmodalinvalidlogin').modal('show');
//         }
//     }
//     );



$("#btnTestSmallModal").click(function () {
    $('#smallmodaluserexist').modal('show');
});

$("#btnContactFormModal").click(function () {
    $('#contactformmodal').modal('show');
});

$("#btnPhoneFormModal").click(function () {
    $('#smallmodalphone').modal('show');
});


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