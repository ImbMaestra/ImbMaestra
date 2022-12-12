<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmTabMantenedor.aspx.cs" Inherits="MaestraNet.GC.SVTA.Mantenedor.frmTabMantenedor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <link href="../../../open-iconic/font/css/open-iconic-bootstrap.css" rel="stylesheet" />
<link rel="stylesheet" href="../../../style-wide.css" />
 <script src="../../../Scripts/jquery-3.0.0.min.js"></script>
 <script src="../../../Scripts/popper.min.js"></script>
 <script src="../../../Scripts/bootstrap.min.js"></script>
 <script src="../../../js/jquery-slim.min.js"></script>
 <script src="../../../js/jquery.bxslider.min.js"></script>
<link href="../sistema_venta.css" rel="stylesheet" />
<title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <div class="btn-group" role="group" aria-label="Basic example">
                <asp:LinkButton ID="lnkProyectos" runat="server" class="botoMaestraActivo btn" OnClick="lnkProyectos_Click">Proyectos</asp:LinkButton>
                <asp:LinkButton ID="lnkInmueble" runat="server" class="botoMaestra btn" OnClick="lnkInmueble_Click">Inmuebles</asp:LinkButton>
                <asp:LinkButton ID="lnkClientes" runat="server" class="botoMaestra btn" OnClick="lnkClientes_Click">Clientes</asp:LinkButton>
                <asp:LinkButton ID="lnkPlantilla" runat="server" class="botoMaestra btn" OnClick="lnkPlantilla_Click">Plantillas Promesa</asp:LinkButton>
                <asp:LinkButton ID="lnkPerfil" runat="server" class="botoMaestra btn" OnClick="lnkPerfil_Click">Perfil Paginas</asp:LinkButton>
                <asp:LinkButton ID="lnkTipoInmueble" runat="server" class="botoMaestra btn" OnClick="lnkTipoInmueble_Click">Tipo Inmueble</asp:LinkButton>
                <%--<asp:LinkButton ID="lnkMantenedorGeneral" runat="server" class="botoMaestra btn" OnClick="lnkMantenedorGeneral_Click">Mantenedor General</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkInmuebleNew" runat="server" class="botoMaestra btn" OnClick="lnkInmuebleNew_Click">Inmuebles New</asp:LinkButton>
                <asp:LinkButton ID="lnkCargaMasiva" runat="server" class="botoMaestra btn" OnClick="lnkCargaMasiva_Click" >Carga Masiva</asp:LinkButton>
            </div>
        </div>

        <br />

        <embed id="myFrame" runat="server" src="#" height="800px" width="100%" />

    </form>
</body>
</html>
