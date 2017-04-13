<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-3.1.1.min.js"></script>
    <script src="Scripts/jquery.signalR-2.2.1.min.js"></script>
    <script src="/signalR/hubs"></script>
    <script type="text/javascript">

        $(function () {

            // Proxy created on the fly
            var job = $.connection.myHub;

            // Declare a function on the job hub so the server can invoke it
            job.client.displayStatus = function () {
                getData();
            };

            // Start the connection
            $.connection.hub.start();
            getData();
        });

        function getData() {
            var $tbl = $('#tbl');
            $.ajax({
                url: 'index.aspx/GetData',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "POST",
                success: function (data) {
                    if (data.d.length > 0) {
                        var newdata = data.d;
                        $tbl.empty();
                        $tbl.append(' <tr><th>Usuario</th><th>Mensaje</th></tr>');
                        var rows = [];
                        for (var i = 0; i < newdata.length; i++) {
                            rows.push(' <tr><td>' + newdata[i].UsuarioID + '</td><td>' + newdata[i].Mensaje + '</td></tr>');
                        }
                        $tbl.append(rows.join(''));
                    }
                }
            });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="tbl"></table>
        </div>
    </form>
</body>
</html>
