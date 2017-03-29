// Write your Javascript code.

$("#btnTestSmallModal").click(function () {
    $('#smallmodaluserexist').modal('show');
});

$("#btnContactFormModal").click(function () {
    $('#contactformmodal').modal('show');
});

$("#btnPhoneFormModal").click(function () {
    $('#smallmodalphone').modal('show');
});

$("#btnNewSignup").click(function () {
    $('#modalEmailSent').modal('show');
});

$("#btnTestimonialsModal").click(function () {
    $('#smallmodaltestimonials').modal('show');
});

$("#btnIndexTestimonialsModal").click(function () {
    $('#smallmodaltestimonials').modal('show');
});

//$("#btnEmailSent").click(function () {
//    $('#smallmodalemailsent').modal('show');
//});


// SITEAUTH.js


$('#tab a[href="#home"]').tab('show');

//$('#exampleBasic').wizard({
//    onFinish: function () {
//        alert('finish');
//    }
//});


//var themes = {
    //"default": "~/Scripts/bootstrap.js",
    //"cerulean": "//bootswatch.com/cerulean/bootstrap.min.css",
    //"cosmo": "//bootswatch.com/cosmo/bootstrap.min.css",
    //"cyborg": "//bootswatch.com/cyborg/bootstrap.min.css",
    //"flatly": "//bootswatch.com/flatly/bootstrap.min.css",
    //"journal": "//bootswatch.com/journal/bootstrap.min.css",
    //"readable": "//bootswatch.com/readable/bootstrap.min.css",
    //"simplex": "//bootswatch.com/simplex/bootstrap.min.css",
    //"slate": "//bootswatch.com/slate/bootstrap.min.css",
    //"spacelab": "//bootswatch.com/spacelab/bootstrap.min.css",
    //"united": "//bootswatch.com/united/bootstrap.min.css"
//}

//switches
//$(function () {
//    var themesheet = $('<link href="' + themes['default'] + '" rel="stylesheet" />');
//    themesheet.appendTo('head');
//    $('.theme-link').click(function () {
//        var themeurl = themes[$(this).attr('data-theme')];
//        themesheet.attr('href', themeurl);
//    });
//});

$(document).ready(function () {
    var counter = 0;

    $("#addrowrating").on("click", function () {
        var newRow = $("<tr>");
        var cols = "";

        cols += '<td>';
        cols += '<select class="selectpicker">';
        cols += '<optgroup label="Primary Disabilities">';
        cols += '<option>Neck</option>';
        cols += '<option>Back</option>';
        cols += '<option>Ankle</option>';
        cols += '<option>Wrist</option>';
        cols += '<option>Knees</option>';
        cols += '<option>Feet</option>';
        cols += '<option>Shoulder</option>';
        cols += '<option>Elbow</option>';
        cols += '<option>Hip</option>';
        cols += '</optgroup>';
        cols += '<optgroup label="Other">';
        cols += '<option>Headache</option>';
        cols += '</optgroup>';
        cols += '</select>';
        cols += '</td>';


        cols += '<td>';
        cols += '<select class="selectpicker">';
        cols += '<option>10%</option>';
        cols += '<option>20%</option>';
        cols += '<option>30%</option>';
        cols += '<option>40%</option>';
        cols += '<option>50%</option>';
        cols += '<option>60%</option>';
        cols += '<option>70%</option>';
        cols += '<option>80%</option>';
        cols += '<option>90%</option>';
        cols += '</select>';
        cols += '</td>';

        cols += '<td>';
        cols += '<select class="selectpicker">';
        cols += '<option>Left</option>';
        cols += '<option>Right</option>';
        cols += '<option>Both</option>';
        cols += '</select>';
        cols += '</td>';
        //cols += '<td><input type="text" class="form-control" name="name' + counter + '"/></td>';
        //cols += '<td><input type="text" class="form-control" name="mail' + counter + '"/></td>';
        //cols += '<td><input type="text" class="form-control" name="phone' + counter + '"/></td>';

        cols += '<td><input type="button" class="ibtnDel btn btn-md btn-danger "  value="Delete"></td>';
        newRow.append(cols);
        $("table.order-list").append(newRow);
        counter++;
    });



    $("table.order-list").on("click", ".ibtnDel", function (event) {
        $(this).closest("tr").remove();
        counter -= 1
    });


});



function calculateRow(row) {
    var price = +row.find('input[name^="price"]').val();

}

function calculateGrandTotal() {
    var grandTotal = 0;
    $("table.order-list").find('input[name^="price"]').each(function () {
        grandTotal += +$(this).val();
    });
    $("#grandtotal").text(grandTotal.toFixed(2));
}

//$('.accordion').find('.accordion-toggle').click(function () {
//    $(this).next().slideToggle('600');
//    $(".accordion-content").not($(this).next()).slideUp('600');
//});
//$('.accordion-toggle').on('click', function () {
//    $(this).toggleClass('active').siblings().removeClass('active');
//});


//function eval_ratingselect_1(ratingselectvalue, divname, ismaxval) {
//    var resultstr = "";
//    switch (ratingselectvalue) {
//        case '0':
//            resultstr = "high chance";
//            break;
//        case '10':
//            resultstr = "medium chance";
//            break;
//        case '20':
//            resultstr = "low chance";
//            break;
//        case '30':
//            resultstr = "no chance";
//            break;
//    }

//    alert("resultstr: " + resultstr + " Value: " + ratingselectvalue + " Div: " + divname + " ismaxval: " + ismaxval);
//}

function eval_ratingselect_1(ratingselect, divname, maxval) {
    var helpstr = "";
    var selectedText = ratingselect.options[ratingselect.selectedIndex].innerHTML;
    var selectedValue = ratingselect.value;
    switch (selectedValue) {
        case '0':
            helpstr = "<span class=\"label label-success label-rounded\">high chance</span>";
            break;
        case '10':
            helpstr = "<span class=\"label label-warning label-rounded\">medium chance</span>";
            break;
        case '20':
            helpstr = "<span class=\"label label-warning label-rounded\">low chance</span>";
            break;
        case '30':
            helpstr = "<span class=\"label label-danger label-rounded\">no chance</span>";
            break;
        case '40':
            helpstr = "<span class=\"label label-danger label-rounded\">no chance</span>";
            break;
        default:
            helpstr = "<span class=\"label label-danger label-rounded\">no chance</span>";
            break;

    }
    //alert("Selected Text: " + selectedText + " Value: " + selectedValue + " Div: " + divname);
    if (maxval != selectedValue) {
        resultstr = "<div class=\"text-left\"><span class=\"text-muted\"> We have a " + helpstr + " of increasing your VA for this disability to</span><h1><sup><i class=\"ti-arrow-up text-success\"></i></sup> " + maxval + "%</h1></div>";
        //resultstr = resultstr + "<div class=\"col-xs-12\"><button id=\"formbackbutton\" class=\"btn btn-info btn-lg btn-block text-uppercase waves-effect waves-light\"><b>Start Now</b></button></div>";
    }
    else {
        resultstr = "<div class=\"text-left\"><span class=\"text-muted\">You are at max with this rating.  We have " + helpstr + " of increasing your VA benefit for this disability.</span></div>";
    }

    document.getElementById(divname).innerHTML = resultstr;

}

$('.progress .progress-bar').css("width",
               function () {
                   return $(this).attr("aria-valuenow") + "%";
               }
       )

$('#formbackbutton').click(function () {
    $("#backformdiv").load('@Url.Action("FormBack","Dashboard")');
});

$('#S316').change(function () {

    if (this.checked) {

        $('#divRadiculopathySection').show();
        $('#divRadiculopathy').show();
    }
    else {
        $('#divRadiculopathySection').hide();
        $('#divRadiculopathy').hide();
    }
});

$('#SRadiculopathyConstantPainLevelAnswer').change(function () {

    if (this.checked) {

        $('#divConstant').show();
    }
    else {
        $('#divConstant').hide();
    }
});

$('#SRadiculopathyIntermittentPainLevelAnswer').change(function () {

    if (this.checked) {

        $('#divIntermittent').show();
    }
    else {
        $('#divIntermittent').hide();
    }
});

$('#SRadiculopathyDullPainLevelAnswer').change(function () {

    if (this.checked) {

        $('#divDull').show();
    }
    else {
        $('#divDull').hide();
    }
});

$('#SRadiculopathyTinglingPainLevelAnswer').change(function () {

    if (this.checked) {

        $('#divTingling').show();
    }
    else {
        $('#divTingling').hide();
    }
});

$('#SRadiculopathyNumbnessPainLevelAnswer').change(function () {

    if (this.checked) {

        $('#divNumbness').show();
    }
    else {
        $('#divNumbness').hide();
    }
});

$('#S145MuscleSpasm').change(function () {

    if (this.checked) {

        $('#divMuscleSpasm').show();
    }
    else {
        $('#divMuscleSpasm').hide();
    }
});

$('#S55').change(function () {

    if (this.checked) {

        $('#div15C').show();
    }
    else {
        $('#div15C').hide();
    }
});

$('#S414').change(function () {

    if (this.checked) {

        $('#div17A').show();
    }
    else {
        $('#div17A').hide();
    }
});

$('#S416').change(function () {

    if (this.checked) {

        $('#div17ABrace').show();
    }
    else {
        $('#div17ABrace').hide();
    }
});

$('#S428').change(function () {

    if (this.checked) {

        $('#div17ACrutches').show();
    }
    else {
        $('#div17ACrutches').hide();
    }
});

$('#S417').change(function () {

    if (this.checked) {

        $('#div17ACane').show();
    }
    else {
        $('#div17ACane').hide();
    }
});

$('#S421').change(function () {

    if (this.checked) {

        $('#div17AWalker').show();
    }
    else {
        $('#div17AWalker').hide();
    }
});

// Morris donut chart
Morris.Donut({
    element: 'morris-donut-chart',
    data: [{
        label: "Current",
        value: CurrentRating,

    }, {
        label: "Increase",
        value: IncreaseRating,
    }, {
        label: "Potential",
        value: PotentialDelta,
    }],
    resize: true,
    colors: ['#f75b36', '#00b5c2', '#4F5467']
});



//function showAlert(x) {

//    switch (x) {
//        case 0:
//            swal("Here's a message!");
//            break;
//        case 1:
//            sweetAlert("Oops...", "Something went wrong!", "error");
//            break;
//        case 2:
//            swal("Good job!", "You clicked the button!", "success");
//            break;
//        case 3:
//            swal({ title: "Are you sure?", text: "Your will not be able to recover this imaginary file!", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "Yes, delete it!", cancelButtonText: "No, cancel plx!", closeOnConfirm: false, closeOnCancel: false }, function (isConfirm) { if (isConfirm) { swal("Deleted!", "Your imaginary file has been deleted.", "success"); } else { swal("Cancelled", "Your imaginary file is safe :)", "error"); } });
//            break;
//    }
//}

//$('#exampleBasic').wizard({
//    onFinish: function () {
//        alert('finish');
//    }
//});
//$('#exampleBasic2').wizard({
//    onFinish: function () {
//        alert('finish');
//    }
//});
//$('#exampleValidator').wizard({
//    onInit: function () {
//        $('#validation').formValidation({
//            framework: 'bootstrap',
//            fields: {
//                username: {
//                    validators: {
//                        notEmpty: {
//                            message: 'The username is required'
//                        },
//                        stringLength: {
//                            min: 6,
//                            max: 30,
//                            message: 'The username must be more than 6 and less than 30 characters long'
//                        },
//                        regexp: {
//                            regexp: /^[a-zA-Z0-9_\.]+$/,
//                            message: 'The username can only consist of alphabetical, number, dot and underscore'
//                        }
//                    }
//                },
//                email: {
//                    validators: {
//                        notEmpty: {
//                            message: 'The email address is required'
//                        },
//                        emailAddress: {
//                            message: 'The input is not a valid email address'
//                        }
//                    }
//                },
//                password: {
//                    validators: {
//                        notEmpty: {
//                            message: 'The password is required'
//                        },
//                        different: {
//                            field: 'username',
//                            message: 'The password cannot be the same as username'
//                        }
//                    }
//                }
//            }
//        });
//    },
//    validator: function () {
//        var fv = $('#validation').data('formValidation');

//        var $this = $(this);

//        // Validate the container
//        fv.validateContainer($this);

//        var isValidStep = fv.isValidContainer($this);
//        if (isValidStep === false || isValidStep === null) {
//            return false;
//        }

//        return true;
//    },
//    onFinish: function () {
//        $('#validation').submit();
//        alert('finish');
//    }
//});

//$('#accordion').wizard({
//    step: '[data-toggle="collapse"]',

//    buttonsAppendTo: '.panel-collapse',

//    templates: {
//        buttons: function () {
//            var options = this.options;
//            return '<div class="panel-footer"><ul class="pager">' +
//                '<li class="previous">' +
//                    '<a href="#' + this.id + '" data-wizard="back" role="button">' + options.buttonLabels.back + '</a>' +
//                '</li>' +
//                '<li class="next">' +
//                '<a href="#' + this.id + '" data-wizard="next"  role="button">' + options.buttonLabels.next + '</a>' +
//                //'<button name="submitButton" value="PDF" class="btn btn-success">PDF</button>' +
//                //'<button name="submitButton" value="PDF" class="btn btn-success"><a href="#" name="submitButton" data-wizard="finish" role="button">' + options.buttonLabels.finish + '</a></button>' +
//                //'<a name="submitButton" type="submit" value="PDF" href="#' + this.id + '" data-wizard="finish" class="btn btn-success">' + options.buttonLabels.finish + '</a>' +
//                //'<a href="#' + this.id + '" data-wizard="finish" role="button">' + options.buttonLabels.finish + '</a>' +
//                '<a href="#' + this.id + '" data-wizard="finish" role="button">' + options.buttonLabels.finish + '</a>' +
//                '</li>' +
//            '</ul></div>';
//        }
//    },

//    onBeforeShow: function (step) {
//        step.$pane.collapse('show');
//    },

//    onBeforeHide: function (step) {
//        step.$pane.collapse('hide');
//    },

//    onFinish: function () {
//        formqback.submit();
//        //alert('finish');
        
//    }
//});



//$("#partOneNextBtn").on("click", function () {
//    $("#collapsePrimary").collapse('show');
//    $("#collapseDefault").collapse('hide');

//    var model=$('#formqback').serialize();
//    // Serialize model for Posting model 
//    $.ajax({
//        url: '@Url.ActionLink(BackFormSave,Dashboard)',
//        type:'POST',
//        data: model,  // Pass Model to Controller Using Ajax Call
//        success:function(data)
//        {
//            // Show Data
//        }
//    });
//});

//$("#partTwoNextBtn").on("click", function () {
//    $("#collapseSuccess").collapse('show');
//    $("#collapsePrimary").collapse('hide');
//});

//$("#partTwoPrevBtn").on("click", function () {
//    $("#collapseDefault").collapse('show');
//    $("#collapsePrimary").collapse('hide');
//});

//$("#partThreeNextBtn").on("click", function () {
//    $("#collapseInfo").collapse('show');
//    $("#collapseSuccess").collapse('hide');
//});

//$("#partThreePrevBtn").on("click", function () {
//    $("#collapsePrimary").collapse('show');
//    $("#collapseSuccess").collapse('hide');
//});

//$("#partFourNextBtn").on("click", function () {
//    $("#collapseWarning").collapse('show');
//    $("#collapseInfo").collapse('hide');
//});

//$("#partFourPrevBtn").on("click", function () {
//    $("#collapseSuccess").collapse('show');
//    $("#collapseInfo").collapse('hide');
//});
