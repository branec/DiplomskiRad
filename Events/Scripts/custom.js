$("#slideshow > div:gt(0)").hide();

setInterval(function() { 
  $('#slideshow > div:first')
    .fadeOut(1000)
    .next()
    .fadeIn(1000)
    .end()
    .appendTo('#slideshow');
}, 3000);



function increaseInteresting(id) {

    $.ajax({
        type: "POST",
        url: "http://localhost:50966/Event/Interesting?eventId=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var broj = parseInt($('#interesting-' + id).html());
            $('#interesting-' + id).html(broj + 1);
            $('#interestring-button-' + id).removeAttr('onclick').off('click');
            
        },
        error: function () { alert("Ajax Error"); }
    });

}

function addComent(idEvent) {

    var stars = $("#stars-select").val();
    var comment = $('#comment-input').val();


    var reviewEvent = { Id: null, Stars: parseInt(stars), Comment: comment, IdEvent: parseInt(idEvent) };

    $.ajax({
        type: "POST",
        data: JSON.stringify(reviewEvent),
        url: "http://localhost:50966/Event/AddComment",
        contentType: "application/json"
    });

    var template = "<tr><td><div>Ocjena: {{Stars}}</div><p class=\"text-muted\">{{Comment}}</p></td></tr>";
    var html = Mustache.to_html(template, reviewEvent);
    $('#comment-table').append(html);

}


$('form').card({
    // a selector or DOM element for the container
    // where you want the card to appear
    form: 'form',
    container: '.card-wrapper' // *required*

    // all of the other options from above
});
