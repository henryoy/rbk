<%@ Page Language="C#" MasterPageFile="~/Dashboard/Template/Editor.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Dashboard_Template_index" %>

<asp:Content ID="Css" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="canvas">
        <!-- Editor Holder -->
        <div id="holder" style="margin-left: 352.5px; width: 700px;">
            <div id="titles_holders">
            </div>
            <div id="meta_holder">
                <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;">
            </div>
            <div id="styles_holder">
                <style type="text/css">
                    /* Custom Font */
                    @font-face {
                        font-family: "Proxima N W15 Thin Reg";
                        src: url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/e7c1fd50-6611-4b2b-86eb-03f6159100c3.eot?#iefix");
                        src: url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/e7c1fd50-6611-4b2b-86eb-03f6159100c3.eot?#iefix") format("eot"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/cb1061dc-f26a-43a0-8dd8-bb0541873c3d.woff") format("woff"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/a2e9a37c-6342-4985-8053-a9b44d5d3524.ttf") format("truetype"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/602df5ad-7d3a-48e7-8f6a-867f5d482c77.svg#602df5ad-7d3a-48e7-8f6a-867f5d482c77") format("svg");
                    }

                    @font-face {
                        font-family: "Proxima N W15 Light";
                        src: url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/53f72e41-ffd4-47d4-b8bf-b1ab3cada2e5.eot?#iefix");
                        src: url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/53f72e41-ffd4-47d4-b8bf-b1ab3cada2e5.eot?#iefix") format("eot"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/fb5639f2-f57b-487d-9610-3dc50820ab27.woff") format("woff"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/2eafe9b7-5a21-49c0-84ca-54c54f899019.ttf") format("truetype"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/0a2fe21c-cfdd-4f40-9dca-782e95c1fa90.svg#0a2fe21c-cfdd-4f40-9dca-782e95c1fa90") format("svg");
                    }

                    @font-face {
                        font-family: "Proxima N W15 Reg";
                        src: url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/ccd538c8-85a6-4215-9f3f-643c415bbb19.eot?#iefix");
                        src: url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/ccd538c8-85a6-4215-9f3f-643c415bbb19.eot?#iefix") format("eot"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/e8e438df-9715-40ed-b1ae-58760b01a3c0.woff") format("woff"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/baf65064-a8a8-459d-96ad-d315581d5181.ttf") format("truetype"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/76bd19c9-c46a-4c27-b80e-f8bd0ecd6057.svg#76bd19c9-c46a-4c27-b80e-f8bd0ecd6057") format("svg");
                    }

                    @font-face {
                        font-family: "Proxima N W15 Smbd";
                        src: url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/f0900b9e-436e-4bb2-ba92-174617a6b4bc.eot?#iefix");
                        src: url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/f0900b9e-436e-4bb2-ba92-174617a6b4bc.eot?#iefix") format("eot"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/91b14d48-ff2a-4a42-87df-b04c76cfb67f.woff") format("woff"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/65e3a762-7125-4d24-9247-fc73d4786cd0.ttf") format("truetype"),url("http://www.stampready.net/themeforest/dashboard/templates/nova/font/4b8633b5-6a28-45ea-afc0-1784363b823a.svg#4b8633b5-6a28-45ea-afc0-1784363b823a") format("svg");
                    }

                    /* Reset */
                    * {
                        margin-top: 0px;
                        margin-bottom: 0px;
                        padding: 0px;
                        border: none;
                        line-height: normal;
                        outline: none;
                        list-style: none;
                        -webkit-text-size-adjust: none;
                        -ms-text-size-adjust: none;
                    }

                    table {
                        border-collapse: collapse !important;
                        padding: 0px !important;
                        border: none !important;
                        border-bottom-width: 0px !important;
                        mso-table-lspace: 0pt;
                        mso-table-rspace: 0pt;
                    }

                        table td {
                            border-collapse: collapse;
                            text-decoration: none;
                        }

                    body {
                        margin: 0px;
                        padding: 0px;
                        background-color: #FFFFFF;
                    }

                    .ExternalClass * {
                        line-height: 100%;
                    }

                    /* Responsive */
                    @media only screen and (max-width:600px) {

                        /* Tables
	parameters: width, alignment, padding */

                        table[class=scale] {
                            width: 100% !important;
                        }

                        /* Td */
                        td[class=scale-left] {
                            width: 100% !important;
                            text-align: left !important;
                        }

                        td[class=scale-left-bottom] {
                            width: 100% !important;
                            text-align: left !important;
                            padding-bottom: 24px !important;
                        }

                        td[class=scale-left-top] {
                            width: 100% !important;
                            text-align: left !important;
                            padding-top: 24px !important;
                        }

                        td[class=scale-left-all] {
                            width: 100% !important;
                            text-align: left !important;
                            padding-top: 24px !important;
                            padding-bottom: 24px !important;
                        }

                        td[class=scale-center] {
                            width: 100% !important;
                            text-align: center !important;
                        }

                        td[class=scale-center-both] {
                            width: 100% !important;
                            text-align: center !important;
                            padding-left: 20px !important;
                            padding-right: 20px !important;
                        }

                        td[class=scale-center-bottom] {
                            width: 100% !important;
                            text-align: center !important;
                            padding-bottom: 24px !important;
                        }

                        td[class=scale-center-top] {
                            width: 100% !important;
                            text-align: center !important;
                            padding-top: 24px !important;
                        }

                        td[class=scale-center-all] {
                            width: 100% !important;
                            text-align: center !important;
                            padding-top: 24px !important;
                            padding-bottom: 24px !important;
                            padding-left: 20px !important;
                            padding-right: 20px !important;
                        }

                        td[class=scale-right] {
                            width: 100% !important;
                            text-align: right !important;
                        }

                        td[class=scale-right-bottom] {
                            width: 100% !important;
                            text-align: right !important;
                            padding-bottom: 24px !important;
                        }

                        td[class=scale-right-top] {
                            width: 100% !important;
                            text-align: right !important;
                            padding-top: 24px !important;
                        }

                        td[class=scale-right-all] {
                            width: 100% !important;
                            text-align: right !important;
                            padding-top: 24px !important;
                            padding-bottom: 24px !important;
                        }

                        td[class=scale-center-bottom-both] {
                            width: 100% !important;
                            text-align: center !important;
                            padding-bottom: 24px !important;
                            padding-left: 20px !important;
                            padding-right: 20px !important;
                        }

                        td[class=scale-center-top-both] {
                            width: 100% !important;
                            text-align: center !important;
                            padding-top: 24px !important;
                            padding-left: 20px !important;
                            padding-right: 20px !important;
                        }

                        td[class=reset] {
                            height: 0px !important;
                        }

                        td[class=scale-center-topextra] {
                            width: 100% !important;
                            text-align: center !important;
                            padding-top: 84px !important;
                        }

                        img[class="reset"] {
                            display: inline !important;
                        }

                        a[class=pad-top] {
                            padding-top: 50px;
                            display: block;
                        }

                        span[class=mobile-hidden] {
                            display: none;
                        }
                    }
                </style>
            </div>
            <div id="modules_holder" style="opacity: 0; display: none;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" name="threecolumns" bgcolor="#FFFFFF" data-module="intro" data-thumb="http://www.stampready.net/dashboard/templates/epitome/thumbnails/intro.jpg" class="">
                    <tbody>
                        <tr>
                            <td style="border-bottom: 1px solid #e0e0e0;" data-padding="">

                                <table width="600" border="0" cellspacing="0" cellpadding="0" align="center" class="scale" bgcolor="#FFFFFF">
                                    <tbody>
                                        <tr>
                                            <td height="40"></td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <table width="171" border="0" cellspacing="0" cellpadding="0" align="left" style="font-family: 'Proxima N W15 Thin Reg', Helvetica, Arial, sans-serif; font-size: 15px; color: #9b9b9b;" class="scale" bgcolor="#FFFFFF">
                                                    <tbody>
                                                        <tr>
                                                            <td class="scale-center" align="center" height="100">

                                                                <img runat="server" src="~/Content/editor/img/client-1.png" border="0" style="display: block; max-width: 171px" data-crop="false" class="reset">
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>

                                                <table width="386" border="0" cellspacing="0" cellpadding="0" align="right" class="scale" bgcolor="#FFFFFF">
                                                    <tbody>
                                                        <tr>
                                                            <td>

                                                                <table width="171" border="0" cellspacing="0" cellpadding="0" align="left" style="font-family: 'Proxima N W15 Thin Reg', Helvetica, Arial, sans-serif; font-size: 15px; color: #9b9b9b;" class="scale">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="scale-center-top" height="100" align="center">

                                                                                <img runat="server" src="~/Content/editor/img/client-2.png" border="0" style="display: block; max-width: 171px" data-crop="false" class="reset">
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                                <table width="171" border="0" cellspacing="0" cellpadding="0" align="right" style="font-family: 'Proxima N W15 Thin Reg', Helvetica, Arial, sans-serif; font-size: 15px; color: #9b9b9b;" class="scale">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="scale-center-top" align="center" height="100">

                                                                                <img runat="server" src="~/Content/editor/img/client-3.png" border="0" style="display: block; max-width: 171px" data-crop="false" class="reset">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="40"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table width="600" border="0" cellspacing="0" cellpadding="0" align="center" style="font-family: 'Proxima N W15 Thin Reg', Helvetica, Arial, sans-serif; font-size: 16px; color: #9b9b9b;" class="scale">

                                    <tbody>
                                        <tr>
                                            <td style="padding: 0px 0px 36px 0px; line-height: 34px;" class="scale-center-both" data-color="Paragraphs" data-size="Paragraphs" data-min="12" data-max="20">Tu quoque, Brute, fili mi, nihil timor populi, nihil! Vivamus sagittis lacus vel augue laoreet rutrum faucibus. <a href="http://www.stampready.net/dashboard/editor/index.php?campaign_id=180087#" style="color: #ce5f40; text-decoration: none;">Salutantibus</a> vitae elit libero, a pharetra augue. Curabitur est gravida et <a href="http://www.stampready.net/dashboard/editor/index.php?campaign_id=180087#" style="color: #ce5f40; text-decoration: none;">libero</a> vitae dictum.
						
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="30"></td>
                                        </tr>
                                    </tbody>
                                </table>

                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <!-- Editor Frame -->
            <div id="frame" class="ui-sortable empty" style="min-height: 250px;">
                <div id="edit_link" class="hidden" style="display: none;">
                    <div class="close_link"></div>
                    <input type="text" id="edit_link_value" class="createlink" placeholder="Your URL">
                    <div id="change_image_wrapper">
                        <div id="change_image">
                            <p id="change_image_button">Change &nbsp; <span class="pixel_result"></span></p>
                        </div>
                        <input type="button" value="" id="change_image_link">
                        <input type="button" value="" id="remove_image">
                    </div>                    
                    <div id="tip"></div>
                </div>
            </div>
        </div>
        <span class="highlighter-container hidden" style="display: none; position: absolute;">
            <div id="template_actions">

                <li style="padding-left: 12px!important;">
                    <input type="button" value="" id="cmdBold"></li>
                <li style="width: 25px!important;">
                    <input type="button" value="" id="cmdItalic" style="background-position: 6px center!important;"></li>
                <li>
                    <input type="button" value="" id="cmdLeftAlign"></li>
                <li>
                    <input type="button" value="" id="cmdCenterAlign"></li>
                <li>
                    <input type="button" value="" id="cmdRightAlign"></li>
                <li style="width: 33px!important;">
                    <input type="button" value="" id="cmdLink" onclick="createLink()"></li>

            </div>

            <!-- Link Value Wrapper -->
            <div id="link" style="display: none;">

                <!-- Close -->
                <div class="close_link"></div>

                <!-- Link Value -->
                <input type="text" id="link_value" class="createlink" placeholder="Your URL">
            </div>

            <!-- Tool Tip Arrow -->
            <div id="tip"></div>
        </span>

    </div>
</asp:Content>
<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
</asp:Content>

