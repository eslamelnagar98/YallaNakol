﻿
var mapHTML = document.querySelector('#map')
var myLat = 30;
var myLng = 31;
var address = { zone: 0, area: 0, city: 0 }
var myMap;

getPosition();

function getPosition()
{
    navigator.geolocation.getCurrentPosition(function (e) {
        
        myLat = e.coords.latitude;
        myLng = e.coords.longitude;
        //---------------
        myMap = new google.maps.Map(mapHTML, {
            center: {
                lat: myLat,
                lng: myLng
            },
            zoom: 16
        })
        geocode();
        AllowDrag();
    }, function ()
    {
            console.log("Error getting location");
            AllowDrag();
    })
}

function geocode()
{
    var geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(myLat, myLng);
    geocoder.geocode({ location: latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK)
        {
            //Loop to get the result with only four components
            for (var i = 0; i < results.length; i++)
            {
                console.log(results[i].types[0])
                if (results[i].types[0] == "administrative_area_level_3")
                {
                    var addressComponents = results[i].address_components;
                    address.zone = addressComponents[0].short_name;
                    address.area = addressComponents[1].short_name;
                    address.city = addressComponents[2].short_name.split(' ')[0];
                    break;
                }
            }
        };
    });
}
function AllowDrag() {
    google.maps.event.addListener(myMap, 'center_changed',
        function () {
            myLat = myMap.getCenter().lat();
            myLng = myMap.getCenter().lng();
            geocode();
        });
}




