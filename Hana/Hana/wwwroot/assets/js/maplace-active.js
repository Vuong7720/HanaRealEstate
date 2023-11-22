function LoadMarker() {
    $.ajax({
        url: '/RealEstate/GetAllActiveLocation',
        type: 'GET',
        success: function (response) {
            //create a blank array
            var markers = [];

            //loop the list of addresses returned from Ajax request
            $.each(response.data, function (index, item) {
                //create a blank array of address
                var marker = {};

                //fill data
                marker["id"] = item.id;
                marker["title"] = item.address;
                marker["lat"] = item.latitude;
                marker["lng"] = item.longtitude;
                marker["description"] = item.address;

                //push the current marker details in markers array
                markers.push(marker);
            });

            //call Map function with all markers passed as list of arrays
            initializeMap(markers);
        }
    });
}

function initializeMap(markers) {
    //you can check your marker data in console
    console.log(markers);
    //Create Google map options
    var GoogleMapOptions = {
        center: new google.maps.LatLng(21.07207761606375, 105.77385452319807),
        zoom: 14,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    //create a variable of InfoWindow type to show data on clicking map icon
    var infoWindow = new google.maps.InfoWindow();
    var map = new google.maps.Map(document.getElementById("map"), GoogleMapOptions);
    if (markers !== null) {
        //loop through each marker data
        for (i = 0; i < markers.length; i++) {
            var data = markers[i];
            //set lat long of current marker
            var myLatlng = new google.maps.LatLng(data.lat, data.lng);

            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: data.title,
                url: "/thong-tin-chi-tiet?id=" + data.id
            });

            (function (marker, data) {
                // add a on marker click event
                google.maps.event.addListener(marker, "click", function (e) {
                    //show description
                    infoWindow.setContent(data.description);
                    infoWindow.open(map, marker);
                    window.location.href = marker.url;
                });
            })(marker, data);
        }
    }
}
