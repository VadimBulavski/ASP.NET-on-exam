$(function () {
    $("#detailsButton").click(function () {
        if ($("#details").is(':visible'))
        {
            $("#details").hide(1000);
        }
        else if ($("#details").is(':hidden'))
        {
            $("#details").show(1000);
        }
        
       
    })
});