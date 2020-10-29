
function cenerateInput(number) {
    $(document).ready(function () {
        $('<input>').attr({
            type: 'hidden',
            value: number,
            name: 'ImageID'
        }).appendTo('form');
    });
}