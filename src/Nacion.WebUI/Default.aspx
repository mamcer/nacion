<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Nacion.WebUI.Default1" MasterPageFile="~/Default.Master" Title="Nacion | Resumen" %>

<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div style="border:solid 1px #003399; margin:10px; padding:5px;" >
        <span style="font-family:Verdana;font-size:medium;font-weight:bold; color:#003366">Siguiente Cuota</span>
        <br />
        <br />
        <span>Nro de la siguiente cuota: </span><asp:Label runat="server" ID="lblNroSiguienteCuota"></asp:Label><br />
        <span>Monto total a pagar: </span><asp:Label runat="server" ID="lblMontoSiguienteCuota"></asp:Label><br />
        <span>Vencimiento: </span><asp:Label runat="server" ID="lblVencimientoSiguienteCuota"></asp:Label><br />
    </div>
    <div style="border:solid 1px #003399; margin:10px; padding:5px;" >
        <span style="font-family:Verdana;font-size:medium;font-weight:bold; color:#003366">Información general del crédito</span>
        <br />
        <br />
        <span>Total pagado a la fecha: </span><asp:Label runat="server" ID="lblTotalPagado"></asp:Label><br />
        <span>Cantidad de cuotas pagas: </span><asp:Label runat="server" ID="lblCuotasPagas"></asp:Label><br />
        <span>Cantidad de cuotas adelantadas: </span><asp:Label runat="server" ID="lblCuotasAdelantadas"></asp:Label><br />
        <span>Resto a pagar: </span><asp:Label runat="server" ID="lblRestoPagar"></asp:Label><br />
        <span>Vencimiento original del crédito: </span><asp:Label runat="server" ID="lblVencimientoOriginal"></asp:Label><br />
        <span>Vencimiento actual del crédito: </span><asp:Label runat="server" ID="lblVencimientoActual"></asp:Label><br />
    </div>
</asp:Content>