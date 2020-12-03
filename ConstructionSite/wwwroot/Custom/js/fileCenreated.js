function cenerateInput( id) {

    $('<input>').attr({
        type: 'hidden',
        value: id,
        name: 'ImageID'
    }).appendTo('form');


}
