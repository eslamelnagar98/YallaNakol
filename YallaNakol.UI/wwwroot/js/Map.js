
var zoneHTML = document.querySelector("#zone");
var cityHTML = document.querySelector("#city");
var areaHTML = document.querySelector("#area");
var phoneHTML = document.querySelector("#phone");
var detailedHTML = document.querySelector('#detailedInfo')

var mapHTML = document.querySelector('#map')
showMap = showMap == 1 ? 1 : 0;

/*var myLat = 30;
var myLng = 31;*/
var address = { zone: 0, area: 0, city: 0 }
var myMap;

if (!showMap) {
    getPosition();
}
else {
    $("#map").show();
    AllowDrag();
    $("#addressDiv").hide();
    geocode();
    phoneHTML.removeAttribute("readonly");
    detailedHTML.removeAttribute("readonly");
    phoneHTML.value = "";
    detailedHTML.value = "";
}
latHTML.value = myLat;
lngHTML.value = myLng;

function getPosition()
{
    navigator.geolocation.getCurrentPosition(function (e) {
        
        myLat = e.coords.latitude;
        myLng = e.coords.longitude;
        //---------------
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
        zoneHTML.value = address.zone;
        areaHTML.value = address.area;
        cityHTML.value = address.city;
        //-------
        var latHTML = document.querySelector("#latHTML");
        var lngHTML = document.querySelector("#lngHTML");
        latHTML.value = myLat;
        lngHTML.value = myLng;
    });
}
function AllowDrag() {

    myMap = new google.maps.Map(mapHTML, {
        center: {
            lat: myLat,
            lng: myLng
        },
        zoom: 16
    })
    google.maps.event.addListener(myMap, 'center_changed',
        UpdateLocation);
}

 function UpdateLocation()
{
    myLat = myMap.getCenter().lat();
    myLng = myMap.getCenter().lng();
     geocode();  
}

//-----------------------------------------------
var selectList = document.querySelector("#addressList");
if (selectList) {
    selectList.addEventListener("change", function () {
        var addressInfo = this.options[this.selectedIndex].text.split(',');
        zoneHTML.value = addressInfo[0].trim();
        areaHTML.value = addressInfo[1].trim();
        cityHTML.value = addressInfo[2].trim();
        phoneHTML.value = addressInfo[3].trim();
        detailedHTML.value = addressInfo[4].trim();

        var addressID = document.querySelector("#addressID");
        addressID.value = this.value;
    })
}

var btnAdd = document.querySelector("#btnAdd");
btnAdd.addEventListener("click", function () {
    $("#map").show();
    UpdateLocation();
    //enable input
    addressID.value = 0;
    phoneHTML.removeAttribute("readonly");
    detailedHTML.removeAttribute("readonly");
    phoneHTML.value = "";
    detailedHTML.value = "";
    $("#addressList").hide();
})










