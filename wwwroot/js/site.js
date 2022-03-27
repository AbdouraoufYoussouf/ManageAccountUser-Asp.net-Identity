// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function confirmDeleteUser(userId, isDelectClicked)
{
    var deleteSpan = 'deleteSpan_'+userId;
    var confirmDeleteSpan = 'confirmDeleteSpan_'+userId;

    if(isDelectClicked)
    {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    }
        else
    {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}
