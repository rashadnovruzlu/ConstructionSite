
    let map, infoWindow;

      function initMap() {
        map = new google.maps.Map(document.getElementById("map"), {
            center: {
                lat: this.getlat(),
                lng: this.getlng()
            },
            zoom: 6
        });
        infoWindow = new google.maps.InfoWindow(); // Try HTML5 geolocation.

        if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            position => {
                const pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };
                infoWindow.setPosition(pos);
                infoWindow.setContent("Location found.");
                infoWindow.open(map);
                map.setCenter(pos);
            },
            () => {
                handleLocationError(true, infoWindow, map.getCenter());
            }
        );
        } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
        }
      }

      function handleLocationError(browserHasGeolocation, infoWindow, pos) {
        infoWindow.setPosition(pos);
        infoWindow.setContent(
          browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
        );
        infoWindow.open(map);
}

function getlat() {

  let lat;
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {

             lat = position.coords.latitude;
           
           

        });
    }
    return lat;

}    
function getlng() {

    let lng;
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {

           
             lng = position.coords.longitude;


        });
    }
    return lng;

}    
   