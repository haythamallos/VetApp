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


var themes = {
    "default": "~/Scripts/bootstrap.js",
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

$('.accordion').find('.accordion-toggle').click(function () {
    $(this).next().slideToggle('600');
    $(".accordion-content").not($(this).next()).slideUp('600');
});
$('.accordion-toggle').on('click', function () {
    $(this).toggleClass('active').siblings().removeClass('active');
});


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