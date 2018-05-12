<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="Nacion.WebUI.Info" MasterPageFile="~/Default.Master" Title="Nacion | Info General"%>

<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div style="border:solid 1px #003399; margin:10px; padding:5px;" >
        <span style="font-family:Verdana;font-size:medium;font-weight:bold; color:#003366">Información General</span>
        <br />
        <br />
        <span>Nro de Cliente: </span><asp:Label runat="server" ID="lblNroCliente"></asp:Label><br />
        <span>Nro de Caja de Ahorro: </span><asp:Label runat="server" ID="lblNroCajaAhorro"></asp:Label><br />
        <span>CBU: </span><asp:Label runat="server" ID="lblCBU"></asp:Label><br />
        <span>Nro de préstamo: </span><asp:Label runat="server" ID="lblNroPrestamo"></asp:Label><br />
        <span>Tasa TEM: </span><asp:Label runat="server" ID="lblTasaTEM"></asp:Label><br />
        <span>Tasa TNAV: </span><asp:Label runat="server" ID="lblTasaTNAV"></asp:Label><br />
        <span>Fecha del primer vencimiento: </span><asp:Label runat="server" ID="lblFechaPrimerVencimiento"></asp:Label><br />
        <span>Fecha del último vencimiento: </span><asp:Label runat="server" ID="lblFechaUltimoVencimiento"></asp:Label><br />
        <span>Capital: </span><asp:Label runat="server" ID="lblCapital"></asp:Label><br />
    </div>
</asp:Content>