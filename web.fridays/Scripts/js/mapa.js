(function ($) {
    "use strict";
    $.fn.mapaMultimple = function (options) {
        var defaults = {
            txtBuscarId: '#txtBuscarId',
            txtLatId: '#txtLatId',
            txtLngId: '#txtLngId',
            Lat: 20.967079665556163,
            Lng: -89.62377979084317,
            zoom: 11,
            activeGeocode: false
        },
        settings = $.extend(defaults, options);

        return this.each(function () {
            var map = null, infoWindow = null, latlng, mapaConfiguracion, mapa, marker, inputBuscar, autocomplete;

            function openInfoWindow(marker) {
                var markerLatLng, geocoder, latlng;
                markerLatLng = marker.getPosition(),
                geocoder = new google.maps.Geocoder(),
                latlng = {
                    lat: markerLatLng.lat(),
                    lng: markerLatLng.lng()
                };
                geocoder.geocode({
                    "location": latlng
                }, function (results, status) {
                    if (status === google.maps.GeocoderStatus.OK) {
                        if (results[0]) {
                            infoWindow.setContent('<b>Direcci&oacute;n: </b>' + results[0].formatted_address + '<br/>' + '<b>Latitud:</b> ' + markerLatLng.lat() + '<br/>' + '<b>Longitud:</b> ' + markerLatLng.lng());
                        }
                        else {
                            console.log('NO EXISTE INFORMACIÓN PARA MOSTRAR');
                        }
                    }
                    else {
                        console.log('Error: ' + status);
                    }
                });
                infoWindow.open(map, marker);
            }

            function geocodePosition(LatLng, activeGeocode) {
                if (activeGeocode != false) {
                    var geocoder = new google.maps.Geocoder();
                    geocoder.geocode({
                        "location": LatLng
                    }, function (results, status) {
                        if (status === google.maps.GeocoderStatus.OK) {
                            if (results[0]) {
                                $(settings.txtBuscarId).val(results[0].formatted_address).trigger('change');;
                            }
                            else {
                                console.log('NO EXISTE INFORMACIÓN PARA MOSTRAR');
                            }
                        }
                        else {
                            console.log('Error: ' + status);
                        }
                    });
                }
            }

            latlng = new google.maps.LatLng(settings.Lat, settings.Lng),
            mapaConfiguracion = {
                center: latlng,
                zoom: settings.zoom,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            },
            map = new google.maps.Map($(this).get(0), mapaConfiguracion),
            infoWindow = new google.maps.InfoWindow(),
            marker = new google.maps.Marker({
                icon: '../Images/maps/icon-marker.png',
                draggable: true,
                position: latlng,
                map: map,
                title: "Arrastre para indicar la ubicación"
            }),
            inputBuscar = $(settings.txtBuscarId).get(0),
            autocomplete = new google.maps.places.Autocomplete(inputBuscar);

            geocodePosition(latlng, settings.activeGeocode);

            google.maps.event.addListener(autocomplete, 'place_changed', function (event) {
                //openInfoWindow(marker);
                var place = autocomplete.getPlace();
                if (place.geometry.viewport) {
                    map.fitBounds(place.geometry.viewport);
                } else {
                    map.setCenter(place.geometry.location);
                    map.setZoom(17);
                }
                marker.setPosition(place.geometry.location);

                var _lat = place.geometry.location.lat().toString();
                var _lng = place.geometry.location.lng().toString();

                if (_lat.length > 6) {
                    _lat = _lat.substr(0, 10);
                }
                if (_lng.length > 6) {
                    _lng = _lng.substr(0, 10);
                }
                $(settings.txtLatId).val(_lat);
                $(settings.txtLngId).val(_lng);
                // $(settings.txtLatId).val(place.geometry.location.lat());
                // $(settings.txtLngId).val(place.geometry.location.lng());
            });

            google.maps.event.addListener(marker, 'dragend', function (event) {
                //openInfoWindow(marker);

                var _lat = event.latLng.lat().toString();
                var _lng = event.latLng.lng().toString();

                if (_lat.length > 6) {
                    _lat = _lat.substr(0, 10);
                }
                if (_lng.length > 6) {
                    _lng = _lng.substr(0, 10);
                }
                $(settings.txtLatId).val(_lat);
                $(settings.txtLngId).val(_lng);

                // $(settings.txtLatId).val(event.latLng.lat());
                // $(settings.txtLngId).val(event.latLng.lng());
                var geocoder = new google.maps.Geocoder();
                geocoder.geocode({
                    "latLng": event.latLng
                }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        var lat = results[0].geometry.location.lat(),
                            lng = results[0].geometry.location.lng(),
                            placeName = results[0].address_components[0].long_name,
                            latlng = new google.maps.LatLng(lat, lng);
                        $(settings.txtBuscarId).val(results[0].formatted_address).trigger('change');;
                    }
                });
            });

            google.maps.event.addListener(marker, 'click', function () {
                //openInfoWindow(marker);
            });
        });
    }
})(jQuery);