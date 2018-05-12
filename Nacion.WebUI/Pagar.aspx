<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pagar.aspx.cs" Inherits="Nacion.WebUI.Pagar" MasterPageFile="~/Default.Master" Title="Nacion | Pagar"%>

<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div style="border:solid 1px #003399; margin:10px; padding:5px;" >
        <span style="font-family:Verdana;font-size:medium;font-weight:bold; color:#003366">Pagar Cuotas</span>
        <br />
        <br />
        <span>El siguiente nro de cuota a pagar es la: </span><asp:Label runat="server" ID="lblNroSiguienteCuota"></asp:Label><br />
        <span>Monto total a pagar: </span><asp:Label runat="server" ID="lblMontoSiguienteCuota"></asp:Label><br />
        <span>Vencimiento: </span><asp:Label runat="server" ID="lblVencimientoSiguienteCuota"></asp:Label><br />
        <br />
        <asp:LinkButton runat="server" ID="lnkPagar" Text="Pagar" 
            onclick="lnkPagar_Click"></asp:LinkButton><br />
        <span><b>Pagar</b> y adelantar: </span><asp:TextBox runat="server" 
            ID="txtPagarAdelantar" Width="47px"></asp:TextBox><span> cuotas&nbsp; </span>
        <asp:Button runat="server" ID="btnPagarAdelantar" Text="Pagar y adelantar" 
            Width="117px" onclick="btnPagarAdelantar_Click"></asp:Button><br />
    </div>
    <div style="border:solid 1px #003399; margin:10px; padding:5px;" >
        <span style="font-family:Verdana;font-size:medium;font-weight:bold; color:#003366">Adelantar Cuotas</span>
        <br />
        <br />
        <span>Ingrese la cantidad de cuotas a adelantar: </span>
        <br />
        <br />
        <span>Adelantar: </span><asp:TextBox runat="server" ID="txtAdelantar" Width="200px"></asp:TextBox>
        <asp:Button runat="server" ID="btnAdelantar" Text="Adelantar" Width="76px" 
            onclick="btnAdelantar_Click"></asp:Button><br />
    </div>
    <div style="border:solid 1px #003399; margin:10px; padding:5px;" >
        <span style="font-family:Verdana;font-size:medium;font-weight:bold; color:#003366">Resetear Cuotas</span>
        <br />
        <br />
        <span>Ingrese la cantidad de cuotas a resetear a partir de la actual: </span>
        <br />
        <br />
        <span>Resetear: </span><asp:TextBox runat="server" ID="txtResetear" Width="200px"></asp:TextBox>
        <asp:Button runat="server" ID="btnResetear" Text="Resetear" Width="76px" 
            onclick="btnResetear_Click"></asp:Button><br />
    </div>
</asp:Content>