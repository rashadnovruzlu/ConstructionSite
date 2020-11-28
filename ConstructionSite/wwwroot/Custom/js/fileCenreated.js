function cenerateInput(e) {

    $('<input>').attr({
        type: 'hidden',
        value: e,
        name: 'ImageID'
    }).appendTo('form');
}
