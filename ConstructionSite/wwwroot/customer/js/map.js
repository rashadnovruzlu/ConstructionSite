// This example requires the Places library. Include the libraries=places
// parameter when you first load the API. For example:
// <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&libraries=places">
let map;
let service;
let infowindow;

function initMap() {
    const sydney = new google.maps.LatLng(-33.867, 151.195);
    infowindow = new google.maps.InfoWindow();
    map = new google.maps.Map(document.getElementById("map"), {
        center: sydney,
        zoom: 15
    });
    const request = {
        query: "Museum of Contemporary Art Australia",
        fields: ["name", "geometry"]
    };
    service = new google.maps.places.PlacesService(map);
    service.findPlaceFromQuery(request, (results, status) => {
        if (status === google.maps.places.PlacesServiceStatus.OK) {
            for (let i = 0; i < results.length; i++) {
                createMarker(results[i]);
            }
            map.setCenter(results[0].geometry.location);
        }
    });
}

function createMarker(place) {
    const marker = new google.maps.Marker({
        map,
        position: place.geometry.location
    });
    google.maps.event.addListener(marker, "click", () => {
        infowindow.setContent(place.name);
        infowindow.open(map);
    });
}
// let map, infoWindow;

//      function initMap() {
//        map = new google.maps.Map(document.getElementById("map"), {
//            center: {
//                lat: this.getlat(),
//                lng: this.getlng()
//            },
//            zoom: 6
//        });
//        infoWindow = new google.maps.InfoWindow();

//        if (navigator.geolocation) {
//        navigator.geolocation.getCurrentPosition(
//            position => {
//                const pos = {
//                    lat: position.coords.latitude,
//                    lng: position.coords.longitude
//                };
//                infoWindow.setPosition(pos);
//                infoWindow.setContent("Location found.");
//                infoWindow.open(map);
//                map.setCenter(pos);
//            },
//            () => {
//                handleLocationError(true, infoWindow, map.getCenter());
//            }
//        );
//        } else {
//        // Browser doesn't support Geolocation
//        handleLocationError(false, infoWindow, map.getCenter());
//        }
//      }

//      function handleLocationError(browserHasGeolocation, infoWindow, pos) {
//        infoWindow.setPosition(pos);
//        infoWindow.setContent(
//          browserHasGeolocation
//            ? "Error: The Geolocation service failed."
//            : "Error: Your browser doesn't support geolocation."
//        );
//        infoWindow.open(map);
//}

//function getlat() {

//  let lat;
//    if (navigator.geolocation) {
//        navigator.geolocation.getCurrentPosition(function (position) {

//             lat = position.coords.latitude;
           
           

//        });
//    }
//    return lat;

//}    
//function getlng() {

//    let lng;
//    if (navigator.geolocation) {
//        navigator.geolocation.getCurrentPosition(function (position) {

           
//             lng = position.coords.longitude;


//        });
//    }
//    return lng;

//}
//function seach() {
//    var result = new new google.maps.places.SearchBox("Baki");
//    alert(result);
//}
   