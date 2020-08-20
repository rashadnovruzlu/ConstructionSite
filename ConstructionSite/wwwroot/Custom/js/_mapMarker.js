function initMap() {
    const myLatLng = {
        lat: 40.409264,
        lng: 49.867092
    };
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 4,
        center: myLatLng
    });
    new google.maps.Marker({
        position: myLatLng,
        map,
        title: "Hello World!"
    });
}