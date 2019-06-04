$('document').ready(function () {
    

    $('.date-picker').datepicker({});


    $('.thumb a').click(function (e) {
        e.preventDefault();
        $('.imgBox img').attr("src", $(this).attr("href"));
    });

    showWorkingDays();

});

function showWorkingDays() {
   
        if ($('#PerDayWorkingHours')[0].checked()) {
            $("#org-work-perday").removeClass('hidden');
            $('#org-work-hour').addClass('hidden');
        }
        else {
            $("#org-work-perday").addClass('hidden');
            $('#org-work-hour').removeClass('hidden');
        }
    
}

