<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Promociones.aspx.cs" Inherits="Dashboard_Promociones" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
        .back_btn {
            height: 43px;
            position: absolute;
            left: 50px;
            top: 20px;
            color: #919191;
            font-size: 12px;
            text-transform: uppercase;
            cursor: pointer;
            background-image: url(../content/img/icons/arrow_left.png);
            background-position: 1px center;
            background-repeat: no-repeat;
            background-color: #FFF;
            line-height: 43px;
            padding-left: 15px;
            padding-right: 20px;
        }

            .back_btn:hover {
                color: #69c0af;
                background-image: url(../content/img/icons/arrow_left_hover.png);
            }
    </style>
    <div id="campaigns" class="disable_selection semi_bold">
        <!-- Top Bar -->
        <div id="list_name_bar">
            <div class="back_btn semi_bold">Regresar</div>
        </div>
        <div id="mainWrapper">
            <asp:Repeater ID="rptPromociones" runat="server">
                <HeaderTemplate>
                    <div id="box_row_titles">
                        <!-- Titles of the rows -->
                        <div id="row_titles2">
                            <div id="row_picture">#</div>
                            <div id="row_color">Titulo</div>
                            <div id="row_name" class="semi_bold">Descripcion</div>
                            <div id="row_date" class="semi_bold">Vigencia Inicial </div>
                            <div id="row_descuento" class="semi_bold">Vigencia Final </div>
                            <div id="row_tarjeta" class="semi_bold">Membresia </div>
                        </div>
                    </div>
                    <ul id="edit-urls-images" class="regular hidden" style="display: block;">
                </HeaderTemplate>
                <ItemTemplate>
                    <li data-id="<%# Eval("Promocionid") %>" data-url="<%= ResolveClientUrl("~/Dashboard/promocion") %>" data-token="IzE0MzYwOTEyMzAzMzA=" data-value="Blanco">
                        <div class="row_picture">
                            <label>
                                <input type="checkbox"><a></a>
                                <asp:HiddenField runat="server" ID="PromocionId" Value='<%# Eval("Promocionid") %>' />
                            </label>
                        </div>
                        <div class="row_titulo">
                            <%# Eval("Titulo") %>
                        </div>
                        <div class="row_descripcion"><%# Eval("Descripcion") %></div>
                        <div class="row_fecha"><%# Eval("Vigenciainicial","{0:d}") %></div>
                        <div class="row_fecha"><%# Eval("Vigenciafinal","{0:d}") %></div>
                        <div class="row_membresia"><%# Eval("Tipomembresia") %></div>
                        <div class="actions semi_bold">
                            <a href="../send/index.php?campaign_id=168761">
                                <div class="send" name="168761">Ver</div>
                            </a>
                            <%--<asp:LinkButton CssClass="delete" runat="server" ID="lnkEliminar" OnClick="lnkEliminar_Click" CommandArgument='<%#Eval("Promocionid")%>'>Eliminar</asp:LinkButton>--%>
                            <%--<div class="export" name="168761"></div>--%>
                            <div class="duplicate" name="168761"></div>
                            <div class="edit" name="168761"></div>
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            //variables
            sent = $('#edit-urls-images li').size();
            campaign_id = '143609';

            //change count of sent campaigns in list name bar
            $('#list_name_bar h2 span b').text(sent);

            // Back button
            $(document).on('click', '.back_btn', function () {

                $(location).attr('href', '../index.php');

            })

            //change multiple links
            $(document).on('click', '.change_selected_urls', function () {

                headline = 'Change multiple URL\'s';
                paragraph = 'Type your new URL and all the selected URL\'s will be updated automatically.';

                btnTrue = 'Change Multiple URL\'s';
                btnTrueId = 'change-multiple-urls';

                btnFalse = 'Nevermind';

                inputField = 'New URL here';
                inputFieldId = 'change-url-value';

                openPopup();

                setTimeout(function () {

                    $('#popup [type="text"]').removeAttr('maxlength');

                }, 500)

            });

            //change url
            $(document).on('click', '#change-multiple-urls', function () {

                //variables
                var url = $('#change-url-value').val();
                var tokensArray = [];

                if (url == '') {

                    notificationContent = 'That URL looks empty';
                    notificationColor = "#ea5a5b";

                    notification();

                }

                else {

                    //for each checked 
                    $('label .checked').each(function () {

                        //variables
                        token = $(this).closest('li').attr('data-token');

                        tokensArray.push(token);

                    });


                    $.ajax({
                        type: "POST",
                        dataType: "html",
                        url: "../../scripts/calls.php?func=change_campaign_url",
                        data: { url: url, tokensArray: tokensArray, campaign_id: campaign_id }
                    }).done(function (data) {

                        if (data == 'success') {

                            //if url does not contain http://, add it automatically
                            if (url.indexOf("http://") !== 0) { url = 'http://' + url; }

                            //for each token, update the link in DM
                            for (i = 0; i < tokensArray.length; i++) {

                                $('[data-token="' + tokensArray[i] + '"]').find('.url').val(url);

                            }

                            closePopup();

                            setTimeout(function () {

                                notificationContent = 'The URL\'s has been changed';
                                notificationColor = "#69c0af";

                                notification();

                            }, 500)

                        }

                        else {

                            notificationContent = 'Error. Please try again';
                            notificationColor = "#ea5a5b";

                            notification();

                        }

                    });

                }

            });

            //click edit to change url or image
            $(document).on('click', '.edit-url-image', function () {

                //variables
                var value = $(this).closest('li').find('h4').text();

                console.log(".... --->");
                console.log(value);

                var url = $(this).closest('li').attr('data-url');
                token = $(this).closest('li').attr('data-token');

                //if value is image, change the popup box
                if (value == 'Image') {

                    paragraph = 'Would you like the change the URL attached to the image? Changes will be reflected immediately.';

                }

                else {

                    paragraph = 'Enter a new URL in order to change the link attached to <span class="semi_bold" style="color: #020202;">' + value + '</span>. Changes will be reflected immediately.';

                }

                headline = 'Change the URL';

                btnTrue = 'Change URL';
                btnTrueId = 'change-url';

                btnFalse = 'Nevermind';

                inputField = url;
                inputFieldId = 'change-url-value';

                openPopup();

                setTimeout(function () {

                    $('#popup [type="text"]').removeAttr('maxlength');

                }, 500)

            });

            //view link
            $(document).on('click', '.view', function () {

                //variables
                var url = $(this).closest('li').attr('data-url');

                window.open(url, '_blank');

            })

            //open image in new tab
            $(document).on('click', '.change-sent-campaign-img', function () {

                //variables
                var img = $(this).closest('li').attr('data-value');

                window.open(img, '_blank');

            });

            //change url
            $(document).on('click', '#change-url', function () {

                //variables
                var url = $('#change-url-value').val();
                var campaign_id = '143609';
                tokensArray = [];
                tokensArray.push(token);

                if (url == '') {

                    notificationContent = 'That URL looks empty';
                    notificationColor = "#ea5a5b";

                    notification();

                }

                else {

                    $.ajax({
                        type: "POST",
                        dataType: "html",
                        url: "../../scripts/calls.php?func=change_campaign_url",
                        data: { url: url, tokensArray: tokensArray, campaign_id: campaign_id }
                    }).done(function (data) {

                        if (data == 'success') {

                            if (url.indexOf("http://") !== 0) { url = 'http://' + url; }

                            $('[data-token="' + token + '"]').find('.url').val(url);

                            closePopup();

                            setTimeout(function () {

                                notificationContent = 'The URL has been changed';
                                notificationColor = "#69c0af";

                                notification();

                            }, 500)

                        }

                        else {

                            notificationContent = 'Error. Please try again';
                            notificationColor = "#ea5a5b";

                            notification();

                        }

                    });

                }


            });

            //convert the value to either text of an image icon
            convertValueOfUrls();

            //check for empty campaigns
            emptyCampaignCheck();

        });

        //function to convert the value of the urls to either text or an image
        function convertValueOfUrls() {

            //for reach list item
            $('#edit-urls-images li').each(function () {
                console.log("demo");
                //varibales
                var url = $(this).attr('data-url');
                var token = $(this).attr('data-token');
                var value = $(this).attr('data-value');

                if (value.indexOf(".jpg") >= 0 || value.indexOf(".gif") >= 0 || value.indexOf(".png") >= 0 || value.indexOf(".jpeg") >= 0) {

                    $(this).find('h4').html('<div class="change-sent-campaign-img semi_bold">Image</div>');

                }

                else {

                    $(this).find('h4').text(value.substring(0, 50));

                }

            })

        }

        //function to check for empty campaigns
        function emptyCampaignCheck() {
            $('#edit-urls-images, #row_titles2, .select_all_checkboxes').show();
            //variables
            li_count = $('#edit-urls-images li').size();
            if (li_count < 1) {

                $('#edit-urls-images, #row_titles2, .select_all_checkboxes').hide();
                $('#mainWrapper').append('<div class="empty_campaigns regular" style="font-size: 34px; color: #4a4a4a"><a href="#" class="semi_bold" style="padding: 18px 34px; font-size: 13px; margin: auto;">No existen promociones</a></div>');

            }
            else {
                $('#sent, #row_titles2, .select_all_checkboxes').show();
            }

        }

    </script>
</asp:Content>


