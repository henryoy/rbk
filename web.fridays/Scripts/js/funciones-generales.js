$(document).ready(function ($) {
    ListadoVacio();
    function ListadoVacio() {
        $('#edit-urls-images, #row_titles2, .select_all_checkboxes').show();
        li_count = $('#edit-urls-images li').size();

        if (li_count < 1) {
            $('#edit-urls-images, #row_titles2, .select_all_checkboxes').hide();
            $('#mainWrapper').append('<div class="empty_campaigns regular" style="font-size: 34px; color: #4a4a4a"><a href="#" class="semi_bold nuevo" style="padding: 18px 34px; font-size: 13px; margin: auto;">NO EXISTE INFORMACIÓN PARA MOSTRAR</a></div>');
        }
        else {
            $('#sent, #row_titles2, .select_all_checkboxes').show();
        }
    }

    $(document).on('click', '.back_btn', function () {
        $(location).attr('href', '../default.aspx');
    });

    $(document).on("click", ".btnFalse", function (e) {
        $("#popupOverlay").hide();
        ListadoVacio();
    });

    $('.decimal').autoNumeric('init', {
        aForm: false,
        aDec: '.',
        vMin: '-999999999.99',
        vMax: '999999999.99'
    });

    $(".position").autoNumeric('init', {
        aForm: false,
        aDec: '.',
        vMin: '-999999999.9999999',
        vMax: '999999999.9999999'
    });

    $(document).on("click", ".nuevo", function (e) {
        eval($(".agregar").attr('href'));
        e.preventDefault();
        e.stopPropagation();
    });

    $(document).on("click", ".view.active", function (e) {
        eval($(".info").attr('href'));
        e.preventDefault();
        e.stopPropagation();
    });

    $(document).on("change paste keyup", ".input-map", function (e) {
        $(".input-direccion").val(this.value);
    });

    window.VerPopUp = function () {
        //openPopup();
        $("#popupOverlay").show();
    }
});