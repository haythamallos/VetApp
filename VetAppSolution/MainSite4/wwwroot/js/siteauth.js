﻿$('#exampleBasic').wizard({
    onFinish: function () {
        alert('finish');
    }
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