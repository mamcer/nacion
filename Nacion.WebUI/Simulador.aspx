<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Simulador.aspx.cs" Inherits="Nacion.WebUI.Simulador"  MasterPageFile="~/Default.Master" Title="Nacion | Simulador"%>

<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div style="border:solid 1px #003399; margin:10px; padding:5px;" >
        <span style="font-family:Verdana;font-size:medium;font-weight:bold; color:#003366">Simulador</span>
        <br />
        <br />
        <span>Dinero: </span><asp:TextBox runat="server" ID="txtDinero" Width="213px"></asp:TextBox>&nbsp;<asp:Button 
            runat="server" ID="btnSimular" Text="Simular" Font-Names="Verdana" 
            Font-Size="Small" Width="57px" onclick="btnSimular_Click"></asp:Button><br />
        <span>Monto Siguiente Cuota: </span><asp:Label runat="server" ID="lblMontoSiguienteCuota"></asp:Label><br />
        <span>Próximo Vencimiento: </span><asp:Label runat="server" ID="lblVencimientoSiguienteCuota"></asp:Label><br />
    </div>
    <asp:panel runat="server" ID="pnlResultado" Visible="false" style="border:solid 1px #003399; margin:10px; padding:5px;" >
        <span>Cantidad de cuotas adelantadas: </span><asp:Label runat="server" ID="lblCuotasAdelantadas"></asp:Label><br />
        <span>Capital adelantado: </span><asp:Label runat="server" ID="lblCapitalAdelantado"></asp:Label><br />
        <span>Intereses ahorrados: </span><asp:Label runat="server" ID="lblInteresesAhorrados"></asp:Label><br />
        <span>Nuevo vencimiento del crédito: </span><asp:Label runat="server" ID="lblNuevoVencimiento"></asp:Label><br />
        <span>Siguiente nro de cuota: </span><asp:Label runat="server" ID="lblSiguienteNroCuota"></asp:Label><br />
        <span>Dinero restante: </span><asp:Label runat="server" ID="lblDineroRestante"></asp:Label><br />
    </asp:panel>
</asp:Content>
