$('#AZ').click(function (event) {
    event.preventDefault();
    $('.AZ').removeClass('AZ')
    $('.AZ').addClass('current-menu-item');
});
$('#РУ').click(function (event) {
    event.preventDefault();
    $('.РУ').removeClass('РУ')
    $('.РУ').addClass('current-menu-item');
});
$('#EN').click(function (event) {
    event.preventDefault();
    $('.EN').removeClass('EN')
    $('.EN').addClass('current-menu-item');
});
