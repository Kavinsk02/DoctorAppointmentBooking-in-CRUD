window.setTimeout(function()
{
    $(".alert").fadeTo(500, 0).slideup(500, function ()
    {
        $(this).remove();

    });

}, 4000);