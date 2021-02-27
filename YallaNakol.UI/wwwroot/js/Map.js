
var Mymap = document.querySelector('#map')
function loadMap() {
    console.log("not working");
    //check browser support
    if (navigator.geolocation) {
        //get position

        navigator.geolocation.getCurrentPosition(success, error)
    }
    else {
        map.innerText = "update your browser :P"
    }

}
    loadMap();

function success(e) {
    console.log(e)
    // map.innerText = "position is "+e.coords.latitude + " , " +e.coords.longitude
    var myMap = new google.maps.Map(Mymap, {
        center: {
            lat: e.coords.latitude,
            lng: e.coords.longitude
        },
        zoom: 16
    })

}

function error(e) {
    console.log(e)
    var message = "";
    switch (e.code) {
        case 1: {
            message = "please allow me to know your location"
            break
        }
        case 2: {
            message = "position is not available check your configuration"
            break
        }
        case 3: {
            message = "try again later"
            break
        }
        default: {
            message = "unknown error"
        }

    }
    map.innerText = message
}