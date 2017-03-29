<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPaginacion.ascx.cs" Inherits="Controles_ucPaginacion" %>
<div id="container">
    <div class="pagination">
        <asp:Repeater ID="rptPaginas" runat="server">
            <ItemTemplate>
                <asp:LinkButton class='page <%# Convert.ToBoolean(Eval("Enabled")) ? "" : PaginaClass %>' ID="btnPagina" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                    OnClick="btnPagina_Click" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>