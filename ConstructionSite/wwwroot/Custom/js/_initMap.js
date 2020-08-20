 let map;

      function initMap() {
        map = new google.maps.Map(document.getElementById("map"), {
          center: {
                lat: 40.409264,
                lng: 49.867092
          },
          zoom: 5
        });
      }