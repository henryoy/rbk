function pageLoad(sender, args) {
    $(document).ready(function () {
        var mapaRuta, moneda, campoFecha, formDotacion;
        moneda = $('body').find('.moneda').length;
        if (moneda != 0) {
            $('.moneda').autoNumeric('init');            
        }
        mapaRuta = $('.formulario-ruta').find('.mapa-google').length;
        if (mapaRuta != 0) {
            rutasMap();
            validarRuta();
        }
        campoFecha = $('body').find('.fecha').length;
        if (campoFecha != 0) {
            $('.fecha').mostrarFecha({
                btnId : '.show-calendar'
            });
        }
        formDotacion = $('body').find('.formulario-dotacion').length;
        if (formDotacion != 0) {
            validarDotacion();
        }
        formCaja = $('body').find('.formulario-caja').length;
        if (formCaja != 0) {
            validarCaja();
        }
    });
    window.VerModal = function (modalId) {
        modalId.modal({
            show: true,
            backdrop: 'static'
        });
    }

    window.CerrarModal = function (modalId) {
        $(modalId).modal('hide');
        $('body').removeClass('modal-open').removeAttr('style').removeAttr("class");
    }
    window.RadioCheck = function (rb) {
        var gv = $(rb).closest('.table').attr('id');
        var idTabla = $("#" + gv);
        var rbs = idTabla.find("input");
        for (var i = 0; i < rbs.length; i++) {
            if (rbs[i].type == "radio") {
                if (rbs[i].checked && rbs[i] != rb) {
                    rbs[i].checked = false;
                    break;
                }
            }
        }
    }
    window.buttonClick = function (btn) {
        document.getElementById(btn).click();
        //console.log("Click boton");
    }

    window.intKey = function (evt) {
        if (evt.charCode >= 48 && evt.charCode <= 57)
            return true;
        else
            return false;
    }

    window.isNumberKey = function (evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        //console.log(charCode);
        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }

    var tablaMovimientos = $('#MainContent_grvDetalleMovimientos').length;
    if (tablaMovimientos != 0) {
        keyCode();
        cancelarEdicion();
    }
    var fieldFecha = $('body').find('.formulario-standar').find('.fecha').length;
    if (fieldFecha != 0) {
        mostrarCalendario();
        fechaEspecifica();
        CostoTotal();
    }
    var fechasPoliticas = $('.formulario-politicas').find('.fecha').length;
    if (fechasPoliticas != 0) {
        mostrarCalendarioPoliticas();
    }
    var horaSucursal = $('.formulario-sucursal').find('.hora').length;
    if (horaSucursal != 0) {
        mostrarHora();
    }
    var fechasSuscripcion = $('.formulario-suscripcion').find('.fecha').length;
    if (fechasSuscripcion != 0) {
        mostrarCalendarioSuscripciones();
    }
    var fechasConvenios = $('.formulario-convenios').find('.fecha').length;
    if (fechasConvenios != 0) {
        mostrarCalendarioConvenios();
    }
    var fechaTableros = $('.contenedor-tabla-head').find('.fecha').length;
    if (fechaTableros != 0) {
        mostrarCalendarioTableros();
    }
    var fechaClientes = $('.formulario-clientes').find('.fecha').length;
    if (fechaClientes != 0) {
        mostrarCalendarioClientes();
    }
    var fechaRutas = $('.formulario-ruta').find('.fecha').length;
    if (fechaRutas != 0) {
        mostrarCalendarioRutas();
    }
    var cuadroElement = $('.cuadro-altura').length;
    if (cuadroElement != 0) {
        resizeElement($('.cuadro-altura'), $(window));
        $(window).on('load resize', function () {
            resizeElement($('.cuadro-altura'), $(window));
        });
    }
    var tabs = $('.tab-inner').find('.nav-tabs').length;
    if (tabs != 0) {
        activeTab();
    }
	
	// function initMap(lat, lng, txtBuscar, txtLat, txtLng, mapa) {
        // var configuracion;
        // if (lat != '' || lng != '') {
            // configuracion = {
                // txtBuscarId: '#' + txtBuscar + '',
                // txtLatId: '#' + txtLat + '',
                // txtLngId: '#' + txtLng + '',
                // Lat: lat,
                // Lng: lng,
                // zoom: 14
            // }
        // }
        // else {
            // configuracion = {
                // txtBuscarId: '#' + txtBuscar + '',
                // txtLatId: '#' + txtLat + '',
                // txtLngId: '#' + txtLng + '',
            // }
        // }
        // $('#' + mapa + '').mapaMultimple(configuracion);
    // }

    //function initMap() {
    //    var map = null;
    //    var infoWindow = null;

    //    function openInfoWindow(marker) {
    //        var markerLatLng = marker.getPosition(),
    //        geocoder = new google.maps.Geocoder();
    //        var latlng = { lat: markerLatLng.lat(), lng: markerLatLng.lng() };
    //        geocoder.geocode({
    //            "location": latlng
    //        }, function (results, status) {
    //            if (status === google.maps.GeocoderStatus.OK) {
    //                if (results[0]) {
    //                    console.log("Resultados: " + results[0].formatted_address);
    //                    infoWindow.setContent('<b>Direcci&oacute;n: </b>' + results[0].formatted_address + '<br/>' + '<b>Latitud:</b> ' + markerLatLng.lat() + '<br/>' + '<b>Longitud:</b> ' + markerLatLng.lng());
    //                }
    //                else {
    //                    console.log('No se encontraron resultados');
    //                }
    //            }
    //            else {
    //                console.log('Geocoder failed due to: ' + status);
    //            }
    //        });
    //        infoWindow.open(map, marker);
    //    }

    //    var lat = 20.967079665556163,
    //        lng = -89.62377979084317,
    //        latlng = new google.maps.LatLng(lat, lng);

    //    var mapOptions = {
    //        center: latlng,
    //        zoom: 11,
    //        mapTypeId: google.maps.MapTypeId.ROADMAP
    //    },
    //    map = new google.maps.Map($('#mapaUbicacion').get(0), mapOptions),
    //    infoWindow = new google.maps.InfoWindow(),
    //    marker = new google.maps.Marker({
    //        icon: '../Images/maps/icon-marker.png',
    //        draggable: true,
    //        position: latlng,
    //        map: map,
    //        title: "Ejemplo marcador arrastrable"
    //    }),
    //    input = $('#MainContent_txtBuscarMaps').get(0);
    //    var autocomplete = new google.maps.places.Autocomplete(input);

    //    google.maps.event.addListener(autocomplete, 'place_changed', function (event) {
    //        //openInfoWindow(marker);
    //        var place = autocomplete.getPlace();
    //        if (place.geometry.viewport) {
    //            map.fitBounds(place.geometry.viewport);
    //        } else {
    //            map.setCenter(place.geometry.location);
    //            map.setZoom(17);
    //        }
    //        marker.setPosition(place.geometry.location);
    //        $('#MainContent_txtLatitud').val(place.geometry.location.lat());
    //        $('#MainContent_txtLongitud').val(place.geometry.location.lng());
    //    });

    //    google.maps.event.addListener(marker, 'dragend', function (event) {
    //        //openInfoWindow(marker);
    //        $('#MainContent_txtLatitud').val(event.latLng.lat());
    //        $('#MainContent_txtLongitud').val(event.latLng.lng());
    //        var geocoder = new google.maps.Geocoder();
    //        geocoder.geocode({
    //            "latLng": event.latLng
    //        }, function (results, status) {
    //            if (status == google.maps.GeocoderStatus.OK) {
    //                var lat = results[0].geometry.location.lat(),
    //                    lng = results[0].geometry.location.lng(),
    //                    placeName = results[0].address_components[0].long_name,
    //                    latlng = new google.maps.LatLng(lat, lng);
    //                $("#MainContent_txtBuscarMaps").val(results[0].formatted_address);
    //            }
    //        });
    //    });
    //    google.maps.event.addListener(marker, 'click', function () {
    //        //openInfoWindow(marker);
    //    });
    //}

    //function iniciarMap() {
    //    var lat = 20.967079665556163,
    //    lng = -89.62377979084317,
    //    latlng = new google.maps.LatLng(lat, lng);

    //    //zoomControl: true,
    //    //zoomControlOptions: google.maps.ZoomControlStyle.LARGE,

    //    var mapOptions = {
    //        center: new google.maps.LatLng(lat, lng),
    //        zoom: 11,
    //        mapTypeId: google.maps.MapTypeId.ROADMAP,
    //        panControl: true,
    //        panControlOptions: {
    //            position: google.maps.ControlPosition.TOP_RIGHT
    //        },
    //        zoomControl: true,
    //        zoomControlOptions: {
    //            style: google.maps.ZoomControlStyle.LARGE,
    //            position: google.maps.ControlPosition.TOP_left
    //        }
    //    },
    //    map = new google.maps.Map(document.getElementById('MainContent_ucDireccionEntrega_mapaUbicacionModal'), mapOptions),
    //    marker = new google.maps.Marker({
    //        icon: '../Images/maps/icon-marker.png',
    //        draggable: true,
    //        position: latlng,
    //        map: map,
    //    });

    //    var input = document.getElementById('MainContent_ucDireccionEntrega_txtBuscarMaps');
    //    var autocomplete = new google.maps.places.Autocomplete(input, {
    //        types: ["geocode"]
    //    });

    //    autocomplete.bindTo('bounds', map);
    //    var infowindow = new google.maps.InfoWindow();

    //    google.maps.event.addListener(autocomplete, 'place_changed', function (event) {
    //        infowindow.close();
    //        var place = autocomplete.getPlace();
    //        if (place.geometry.viewport) {
    //            map.fitBounds(place.geometry.viewport);
    //        } else {
    //            map.setCenter(place.geometry.location);
    //            map.setZoom(17);
    //        }

    //        moveMarker(place.name, place.geometry.location);
    //        $('#MainContent_ucDireccionEntrega_txtLatitud').val(place.geometry.location.lat());
    //        $('#MainContent_ucDireccionEntrega_txtLongitud').val(place.geometry.location.lng());
    //    });
    //    google.maps.event.addListener(marker, 'dragend', function (event) {
    //        $('#MainContent_ucDireccionEntrega_txtLatitud').val(event.latLng.lat());
    //        $('#MainContent_ucDireccionEntrega_txtLongitud').val(event.latLng.lng());
    //        //document.getElementById("lat").value = event.latLng.lat();
    //        //document.getElementById("long").value = event.latLng.lng();
    //        infowindow.close();
    //        var geocoder = new google.maps.Geocoder();
    //        geocoder.geocode({
    //            "latLng": event.latLng
    //        }, function (results, status) {
    //            console.log(results, status);
    //            if (status == google.maps.GeocoderStatus.OK) {
    //                console.log(results);
    //                var lat = results[0].geometry.location.lat(),
    //                    lng = results[0].geometry.location.lng(),
    //                    placeName = results[0].address_components[0].long_name,
    //                    latlng = new google.maps.LatLng(lat, lng);

    //                moveMarker(placeName, latlng);
    //                $("#MainContent_ucDireccionEntrega_txtBuscarMaps").val(results[0].formatted_address);
    //            }
    //        });
    //    });

    //    function moveMarker(placeName, latlng) {
    //        marker.setPosition(latlng);
    //        infowindow.setContent(placeName);
    //        //infowindow.open(map, marker);
    //    }
    //}

    // function setMarkers(map) {
    //     var image = {
    //         url: '../Images/maps/icon-marker.png',
    //         size: new google.maps.Size(50, 50),
    //         origin: new google.maps.Point(0, 0),
    //         anchor: new google.maps.Point(17, 34),
    //         scaledSize: new google.maps.Size(40, 40)
    //     };
    //     var shape = {
    //         coords: [1, 1, 1, 20, 18, 20, 18, 1],
    //         type: 'poly'
    //     };
    //     for (var i = 0; i < beaches.length; i++) {
    //         var beach = beaches[i];
    //         var marker = new google.maps.Marker({
    //             position: { lat: beach[1], lng: beach[2] },
    //             map: map,
    //             icon: image,
    //             shape: shape,
    //             title: beach[0],
    //             zIndex: beach[3]
    //         });
    //     }
    // }
    var mapa = $('.tab-inner').find('.mapa-google').length;
    if (mapa != 0) {
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            e.target // newly activated tab
            e.relatedTarget // previous active tab
            initMap();
            iniciarMap();
        })
    }
    var formularioClientes = $('#MainContent_formularioClientes').length;
    if (formularioClientes != 0) {
        $('#MainContent_ddlDelegacion').SelectEditable({
            selectId: '#MainContent_ddlDelegacion',
            inputId: '.control-delegacion',
            parentId: '.row',
            hiddenId: '#MainContent_txtDelegacion'
        });
        $('#MainContent_ddlColonia').SelectEditable({
            selectId: '#MainContent_ddlColonia',
            inputId: '.control-colonia',
            parentId: '.row',
            hiddenId: '#MainContent_txtColonia'
        });
        $('#MainContent_ddlCodigosPostales').SelectEditable({
            selectId: '#MainContent_ddlCodigosPostales',
            inputId: '.control-codigosp',
            parentId: '.row',
            hiddenId: '#MainContent_txtCodigosPostales'
        });
        //Controles Modal
        $('#MainContent_ucDireccionEntrega_ddlDelegacionDireccionEntrega').SelectEditable({
            selectId: '#MainContent_ucDireccionEntrega_ddlDelegacionDireccionEntrega',
            inputId: '.control-delegacion-direccion-entrega',
            parentId: '.row',
            hiddenId: '#MainContent_ucDireccionEntrega_txtDelegacionDireccionEntrega'
        });
        $('#MainContent_ucDireccionEntrega_ddlColoniaDireccionEntrega').SelectEditable({
            selectId: '#MainContent_ucDireccionEntrega_ddlColoniaDireccionEntrega',
            inputId: '.control-colonia-direccion-entrega',
            parentId: '.row',
            hiddenId: '#MainContent_ucDireccionEntrega_txtColoniaDireccionEntrega'
        });
        $('#MainContent_ucDireccionEntrega_ddlCodigosPostalesDireccionEntrega').SelectEditable({
            selectId: '#MainContent_ucDireccionEntrega_ddlCodigosPostalesDireccionEntrega',
            inputId: '.control-codigosp-direccion-entrega',
            parentId: '.row',
            hiddenId: '#MainContent_ucDireccionEntrega_txtCodigosPostalesDireccionEntrega'
        });
    }

    //$(function () {
    //    removeEmptyDiv();
    //    $(window).load(function () {
    //        openMenu();
    //        validarEdicionFecha();
    //        VerTab();
    //        // Delay show just to simulate a real page with tons of content to load
    //        window.setInterval(showPage, 10);
    //        function showPage() {
    //            $('a.imFoto').removeAttr("href");
    //            $('body').show();
    //        }
    //    });
    //});

    window.validarEdicionFecha = function () {
        var editar = $(".btnEditar").attr("id");
        if (editar) {
            $(".fecha").addClass("deshabilitado");
            $(".MostrarCalendario").addClass("deshabilitado");
        }
        else {
            $(".fecha").removeClass("deshabilitado");
            $(".MostrarCalendario").removeClass("deshabilitado");
        }
    }

    window.removeEmptyDiv = function () {
        $("ul.nav.nav-tabs li").each(function (index, item) {
            if ($.trim($(item).text()) == "") {
                $(item).remove();
            }
        });

        $("table tbody tr").find("ul li").each(function (index, item) {
            if ($(item).html().trim() == "") {
                $(item).remove();
            }
        });

        $('.hidden-xs.hidden-sm').each(function (i, item) {
            $(item).attr("title", $(item).text());
            $(item).css("cursor", "default");
        });
    }

    window.openMenu = function () {
        var activeNode = $.cookie("IdMenu");
        var idLiga = $.cookie("idLiga");
        var numeroElementosPadre = $.cookie("elementosParent");
        $(".mtree-node").each(function (i, item) {
            var IdMenu = $(item).attr("id");
            if (activeNode != 'undefined') {
                if (IdMenu == activeNode) {
                    if ($(item).hasClass('mtree-closed')) {
                        $('.mtree-active').not($(item)).removeClass('mtree-active');
                        //$(item).toggleClass('mtree-closed mtree-open');
                        $(item).addClass('mtree-active');
                        //$(item).children('ul').first().css({ 'height': 'auto', 'display': 'block' });
                    }
                    var submenuItem = $(item).find('.mtree-submenu-node');
                    //console.log($(item).find('.mtree-submenu-node').length);
                    submenuItem.each(function () {
                        var idSeccion = $(this).attr('id');
                        if (idSeccion == idLiga) {
                            var ulParents = $('li#' + idSeccion + ".mtree-submenu-node").parents('ul:not(.mtree)');
                            //var contador = 0;
                            ulParents.each(function () {
                                $(this).css({ 'height': 'auto', 'display': 'block' });
                                $(this).parent(item).toggleClass('mtree-closed mtree-open');
                                //contador++;
                            });
                            $('li#' + idSeccion + ".mtree-submenu-node").addClass('mtree-submenu-node-active');
                        }
                    });
                }
            }
        });

        var abierto = $.cookie("open-menu-sidebar");
        if (abierto == 'true') {
            $('.main-page').removeClass('efecto-transition');
            $('.menu-sidebar').removeClass('efecto-transition');
            $('.header-sistema-airtemp').removeClass('efecto-transition');
            $('.main-page').removeClass('main-page-close');
            $('.menu-sidebar').removeClass('menu-sidebar-close');
            $('.main-page').addClass('main-page-open');
            $('.menu-sidebar').addClass('menu-sidebar-open');
            $('.header-sistema-airtemp').addClass('open');
            var TamanioVentana = $(window).width();
            if ($("body").find('.menu-sidebar-overlay').length == 0 && TamanioVentana < 768) {
                var HtmlOverlayMenuSidebar = '<div class="menu-sidebar-overlay overlay-hide"></div>';
                $(HtmlOverlayMenuSidebar).insertAfter(menuSidebar);
                var OverlayMenuSidebar = $('.menu-sidebar-overlay');
                OverlayMenuSidebar.fadeIn();
            }
            //$(".container-fluid .menu-logo").css("display", "none");
        }
        else {
            /*$('.main-page').addClass('efecto-transition');
            $('.menu-sidebar').addClass('efecto-transition');
            $('.header-sistema-airtemp').addClass('efecto-transition');
            $('.main-page').removeClass('main-page-open');
            $('.menu-sidebar').removeClass('menu-sidebar-open');
            $('.main-page').addClass('main-page-close');
            $('.menu-sidebar').addClass('menu-sidebar-close');
            $('.header-sistema-airtemp').removeClass('open');*/
            //$(".container-fluid .menu-logo").css("display", "block");
        }

        if ($.removeCookie('IdMenu') == true && $.removeCookie('idLiga') == true) {
            $(".mtree-node").each(function (i, item) {
                $(item).removeClass('mtree-active')
                $(item).removeClass('mtree-open');
                $(item).addClass('mtree-closed');
            });
            $('.mtree ul').each(function (i, item) {
                $(item).css({ 'height': '0', 'display': 'none' });
            });
        }
    }


    window.VerModal = function (modal, size) {
        if (size)
            modal.find('div.modal-dialog').addClass('modal-lg');

        modal.modal({
            show: true,
            backdrop: 'static'
        });

        //var dialog = $(modal).find('.modal-dialog');
        //dialog.css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 4));
        modal.css("overflow-y", "auto");
    }

    window.VerModalC = function (modal) {
        modal.modal({
            show: true,
            backdrop: 'static'
        });

        //var dialog = $(modal).find('.modal-dialog');
        //dialog.css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 4));
        modal.css("overflow-y", "auto");
    }

    window.CerrarModal = function (modal) {
        $(modal).modal('hide');
        //$('.modal-backdrop').remove();
        $('body').removeClass('modal-open');
        $('body').removeAttr('style');
    }

    window.VerTab = function () {
        //if (window.location.href.indexOf("/Reportes/rpt") > -1) {
        //    VerModal($('#filtros'));
        //    $('body').removeAttr("style");
        //}
        //else
        if (window.location.href.indexOf("Configuraciones.aspx") == -1) return;

        $("ul.nav-tabs li").each(function (index, item) {
            $(item).removeClass("active");
            console.log(item);
        });

        $(".tab-pane").each(function (index, item) {
            $(item).removeClass("in");
            $(item).removeClass("active");
            if ($(item).html().trim() == "") {
                $(item).remove();
            }
        });

        $("ul.nav-tabs li").first().addClass("active");
        $(".tab-pane").first().addClass("in active");
    }

    window.changeCta = function (cuenta) {
        cuenta = cuenta.substring(0, 8);
        if (cuenta.indexOf("___-___-") > -1 || cuenta.length == 0) cuenta = "000-000-";

        $('.table tbody tr').each(function (i, item) {
            $(item).find("td:eq(1)").find("div.input-group").find("a.input-group-addon").text(cuenta);
        });
    }

    String.prototype.replaceAll = function (find, replace) {
        var str = this;
        return str.replace(new RegExp(find.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&'), 'g'), replace);
    };

    $(document).ready(function () {
        //$(window).on("load resize", function () {
        //    var alturaVentana = $(this).height(),
        //        cuadro = $('.tabla-principal.tabla-reportes'),
        //        alturaCuadro = $('.contenedor-tabla').height(),
        //        alturaHeader = $('.header-sistema-airtemp').height(),
        //        alturaTabs = $('.contenedor-tabla-head').height(),
        //        alturaGeneral = alturaVentana - (alturaHeader + alturaTabs);
        //    //alturaCuadro = alturaVentana - alturaHeader;
        //    //alturaCuadro = alturaVentana - alturaHeader;
        //    //$('.cuadro-altura').height(alturaCuadro);
        //    if (alturaGeneral > alturaCuadro) {
        //        console.log('Altura general: ' + alturaGeneral);
        //        cuadro.height(alturaGeneral);
        //    }
        //    else {
        //        console.log('Es menor la altura');
        //        cuadro.height(alturaCuadro);
        //    }
        //});

        $('.moneda').autoNumeric('init', {
            aForm: false,
            aSep: ',',
            aDec: '.',
            vMin: '0.00',
            vMax: '999999999.99',
            aSign: '$ '
        });

        $('.decimal').autoNumeric('init', {
            aForm: false,
            aDec: '.',
            vMin: '0.00',
            vMax: '999999999.99'
        });

        $('.entero').autoNumeric('init', {
            aForm: false,
            aSep: '',
            vMin: '0',
            vMax: '999999999'
        });

        $(document).on("keyup", ".mayuscula", function (e) {
            var pos = this.selectionStart;
            this.value = this.value.toUpperCase();
            this.focus();
            this.setSelectionRange(pos, pos);
            e.preventDefault();
        });

        $(document).on("keyup", ".minuscula", function (e) {
            var pos = this.selectionStart;
            this.value = this.value.toLowerCase();
            this.focus();
            this.setSelectionRange(pos, pos);
            e.preventDefault();
        });

        $(document).on("focus", ".moneda", function (e) {
            var pos = this.value.indexOf("$") + 1;
            this.focus();
            this.setSelectionRange(pos, pos);
            e.preventDefault();
        });

        $(document).on("blur", ".cuenta", function (e) {
            var cta = $(this).val();
            changeCta(cta);
        });

        $(document).on("change", ".cbxEdit", function (e) {
            var input = $(e.target).parent().find("input:text");
            input.val(this.value);
            input.focus();
        });

        $(".cuenta").mask("999-999-000");

        $(".sub-cuenta").mask("999");

        $(".clave-ext").mask("9999");

        $(".folio").mask("9999999999");

        $(".fecha").mask("99/99/9999");

        $(".num-inter").mask("999-99");

        $(".art").mask("99999aa99");

        $(".folio-imp").mask("a999999999");

        $(document).on("blur", ".folio", function (e) {
            var cta = this.value.replaceAll('_', '');
            if (cta.length < 10) {
                var sfcta = "0000000000";
                sfcta = sfcta.substring(0, (10 - cta.length));
                cta = sfcta + cta;
                $(this).val(cta);
            }
        });

        $('.fecha').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd/mm/yy",
            //maxDate: 0,
            yearRange: "-90:+10"
        });

        $('.fechaIni').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd/mm/yy",
            maxDate: 0,
            yearRange: "-90:+10",
            onClose: function (dateText, inst) {
                if ($('.fechaFin').val() != '') {
                    var testStartDate = $('.fechaIni').datepicker('getDate');
                    var testEndDate = $('.fechaFin').datepicker('getDate');
                    if (testStartDate > testEndDate)
                        $('.fechaFin').datepicker('setDate', testStartDate);
                }
                else {
                    $('.fechaFin').val(dateText);
                }
            },
            onSelect: function (selectedDateTime) {
                $('.fechaFin').datepicker('option', 'minDate', $('.fechaIni').datepicker('getDate'));
            }
        });

        $('.fechaFin').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd/mm/yy",
            maxDate: 0,
            yearRange: "-100:+0",
            onClose: function (dateText, inst) {
                if ($('.fechaIni').val() != '') {
                    var testStartDate = $('.fechaIni').datepicker('getDate');
                    var testEndDate = $('.fechaFin').datepicker('getDate');
                    if (testStartDate > testEndDate)
                        $('.fechaIni').datepicker('setDate', testEndDate);
                }
                else {
                    $('.fechaIni').val(dateText);
                }
            },
            onSelect: function (selectedDateTime) {
                $('.fechaIni').datepicker('option', 'maxDate', $('.fechaFin').datepicker('getDate'));
            }
        });

        $('.MostrarCalendario').click(function () {
            var cajaTexto = $(this).closest('.input-group').find('input[type=text]').attr('id');
            $('#' + cajaTexto).datepicker('show', {
                dateFormat: "dd/mm/yy"
            });
        });

        $(document).on("keydown", ".inputs", function (e) {
            if (e.which === 13) {
                var index = $('.inputs').index(this) + 1;
                $('.inputs').eq(index).focus();
                e.stopPropagation();
                e.preventDefault();
            }
            //else if (e.which === 37) {
            //    var index = $('.inputs').index(this) - 1;
            //    $('.inputs').eq(index).focus();
            //}

            //e.stopPropagation();
            //if (e.which === 13) e.preventDefault();
        });

        $('#uploader_div').ajaxupload({
            url: '../upload.aspx',
            maxFileSize: '1M',
            maxFiles: 1,
            resizeImage: {
                maxWidth: 60,
                maxHeight: 42,
                quality: 0.5,
                scaleMethod: undefined,
                format: undefined,
                removeExif: false
            },
            allowExt: ['jpg', 'jpeg', 'bmp', 'png'],
            removeOnSuccess: true,
            error: function (txt, obj) {
                alert(txt);
            },
            onSelect: function (files) {

            },
            finish: function (file) {
                GuardarCambios();
            },
            success: function (file_name) {
                $(".imFoto").attr("src", "../uploads/" + file_name);
                $("#txtUrlImg").val(file_name);
                GuardarCambios();
            }
        });

        $(document).on("click", ".imFoto", function (e) {
            $(".ax-browse").trigger("click");
            e.preventDefault();
            e.stopPropagation();
            e.stopImmediatePropagation();
        });

        $(document).on("click", ".btn-save", function (e) {
            var Archivos = $('.ax-file-list li');
            if (Archivos.length == 0) GuardarCambios();
            else $(".ax-upload").click();
            e.preventDefault();
            e.stopPropagation();
            e.stopImmediatePropagation();
        });

        //$(document).on("click", ".ax-remove", function (e) {
        //    var txt = "";
        //    $('.ax-file-list li').each(function (i, item) {
        //        var archivo = $(item).find('.ax-details').find(".ax-file-name");
        //        txt += archivo.text() + ",";
        //    });
        //    if (txt.length > 0) txt = txt.substr(0, txt.length - 1);
        //    $(".lsArch").val(txt);
        //    e.preventDefault();
        //    e.stopPropagation();
        //    e.stopImmediatePropagation();
        //});

        var currCell = $('td').first();
        // User navigates table using keyboard
        $('table').keydown(function (e) {
            var c = "";
            //if (e.which == 39) {
            //    // Right Arrow
            //    c = currCell.next();
            //} else if (e.which == 37) {
            //    // Left Arrow
            //    c = currCell.prev();
            //} else
            if (e.which == 38) {
                // Up Arrow
                c = currCell.closest('tr').prev().find('td:eq(' +
                  currCell.index() + ')');
            } else if (e.which == 40) {
                // Down Arrow
                c = currCell.closest('tr').next().find('td:eq(' +
                  currCell.index() + ')');
            }
            //else if (!editing && (e.which == 13 || e.which == 32)) {
            //    // Enter or Spacebar - edit cell
            //    e.preventDefault();
            //} else if (!editing && (e.which == 9 && !e.shiftKey)) {
            //    // Tab
            //    e.preventDefault();
            //    c = currCell.next();
            //} else if (!editing && (e.which == 9 && e.shiftKey)) {
            //    // Shift + Tab
            //    e.preventDefault();
            //    c = currCell.prev();
            //}

            //// If we didn't hit a boundary, update the current cell
            //if (c.length > 0) {
            //    currCell = c;
            //    currCell.focus();
            //}
        });

        function GuardarCambios() {
            //var files = JSON.stringify(Archivos);
            //console.log(Archivos);
            javascript: __doPostBack('ctl00$MainContent$btnGuardar', '');
            //javascript: __doPostBack('ctl00$MainContent$btnGuardar', '')
        }

        $('#uploader_div2').ajaxupload({
            url: '../upload.aspx',
            maxFileSize: '3M',
            maxFiles: 10,
            resizeImage: {
                maxWidth: 60,
                maxHeight: 42,
                quality: 0.5,
                scaleMethod: undefined,
                format: undefined,
                removeExif: false
            },
            allowExt: ['jpg', 'jpeg', 'bmp', 'png', 'zip', 'pdf', 'rar', 'xml', 'xls', 'xlsx'],
            removeOnSuccess: true,
            error: function (txt, obj) {
                alert(txt);
            },
            onSelect: function (files) {

            },
            onProgress: function (bytes) {
                //console.log(bytes);
            },
            finish: function (file) {
                //alert("finish");
                //console.log(file);
                GuardarCambios();
            },
            success: function (file_name) {
                //console.log(file_name);
                var ant = $(".lsArch").val() == "" ? "" : $(".lsArch").val();
                if (ant.indexOf(file_name) == -1) {
                    ant += file_name + ",";
                }
                $(".lsArch").val(ant);
            }
        });
    });
    /*$(document).on('show.bs.modal', '.modal', function (event) {
                var zIndex = 1040 + (10 * $('.modal:visible').length);
                $(this).css('z-index', zIndex);
                setTimeout(function () {
                    $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
                }, 0);
            });*/

    /*function VerModal (modalId) {
        $(modalId).modal({
            show: true,
            backdrop: 'static'
        });
    }*/

    /*$(document).ready(function(){
        VerModal('#modalSituacionMovimiento');
        console.log("Estas llegando");
    });*/
}