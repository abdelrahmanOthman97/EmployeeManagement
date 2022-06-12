// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//----when submit form , disabled this submit button
$('input[type="submit"]').click(function () {
    let btn = $(this);
    $(btn).prop('disabled', true);
    let form = $(btn).parents().find('form');
    $(form).submit();
    setTimeout(() => {
        $(btn).prop('disabled', false);
    }, 3000)
});