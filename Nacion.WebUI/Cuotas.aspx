<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cuotas.aspx.cs" Inherits="Nacion.WebUI.Cuotas" MasterPageFile="~/Default.Master" Title="Nacion | Cuotas"%>

<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div style="text-align:center">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" DataSourceID="ObjectDataSource" 
            ForeColor="#333333" GridLines="None" PageSize="20" Width="806px">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="Nro" HeaderText="Nro de Cuota" />
                <asp:BoundField DataField="Vencimiento" DataFormatString="{0:d}" 
                    HeaderText="Vencimiento" />
                <asp:BoundField DataField="Capital" DataFormatString="{0:c}" 
                    HeaderText="Capital" />
                <asp:BoundField DataField="Interes" DataFormatString="{0:c}" 
                    HeaderText="Interes" />
                <asp:BoundField DataField="Cargos" DataFormatString="{0:c}" 
                    HeaderText="Cargos" />
                <asp:BoundField DataField="Impuestos" DataFormatString="{0:c}" 
                    HeaderText="Impuestos" />
                <asp:BoundField DataField="Total" DataFormatString="{0:c}" HeaderText="Total" />
                <asp:BoundField DataField="StatusString" HeaderText="Status" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
<asp:ObjectDataSource ID="ObjectDataSource" runat="server" 
    SelectMethod="GetCuotas" TypeName="Nacion.WebUI.NacionService.Service">
</asp:ObjectDataSource>
</asp:Content>