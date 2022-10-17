<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="frmTab.aspx.cs" Inherits="MaestraNet.GC.SVTA.frmTab" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script src="../../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../../Scripts/popper.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../js/jquery-slim.min.js"></script>
    <script src="../../js/jquery.bxslider.min.js"></script>
    <link href="sistema_venta.css" rel="stylesheet" />
    <script language=Javascript>
        function date_time(id)
        {
                date = new Date;
                year = date.getFullYear();
                month = date.getMonth();
                months = new Array('Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Deciembre');
                d = date.getDate();
                day = date.getDay();
                days = new Array('Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sábado');
                h = date.getHours();
                if(h<10) { h = "0"+h; }
                m = date.getMinutes();
                if(m<10)
                {
                        m = "0"+m;
                }
                s = date.getSeconds();
                if(s<10)
                {
                        s = "0"+s;
                }
                result = ''+days[day]+', '+months[month]+' '+d+' '+year+'  &nbsp;   '+h+':'+m+':'+s;
                document.getElementById(id).innerHTML = result;
                setTimeout('date_time("'+id+'");','1000');
                return true;
        }

        function activaReserva() {

            document.getElementById("<%=lnkReserva.ClientID %>").className = "botoMaestraActivo btn";
            document.getElementById("<%=lnkCotizacion.ClientID %>").className = "botoMaestra btn";

        }

    </script>
    <style>
        .MyEmbede {
            padding-left:10px;
        }
	    .activa {
            background: #E0DEDD;
        }
    </style>

    <nav id="nav">
        <h4><table width="100%"><tr><td>Modulo Ventas</td><td align="right" style=padding-bottom:3px;"><span style="font-size:14px; vertical-align:middle;">Valor UF: <asp:Label ID="lblValorUF" runat="server" Text=""></asp:Label></span>&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;<span style="font-size:14px; vertical-align:middle;" id="date_time"></span><script type="text/javascript">window.onload = date_time('date_time');</script></td></tr></table></h4>
    </nav>
    <article>
        <div class="btn-group" role="group" aria-label="Basic example">
            <asp:LinkButton ID="lnkCotizacion" runat="server" class="botoMaestra btn" OnClick="lnkCotizacion_Click">Cotización</asp:LinkButton>
            <asp:LinkButton ID="lnkReserva" runat="server" class="botoMaestra btn" OnClick="lnkReserva_Click" >Contratos</asp:LinkButton>
            <asp:LinkButton ID="lnkPromesa" runat="server" class="botoMaestra btn" OnClick="lnkPromesa_Click" >Promesa</asp:LinkButton>
            <asp:LinkButton ID="lnkContabilidad" runat="server" class="botoMaestra btn" OnClick="lnkContabilidad_Click" >Contabilidad</asp:LinkButton>
            <asp:LinkButton ID="lnkCobranza" runat="server" class="botoMaestra btn" OnClick="lnkCobranza_Click" >Cobranza</asp:LinkButton>
            <asp:LinkButton ID="lnkEscrituracion" runat="server" class="botoMaestra btn" OnClick="lnkEscrituracion_Click" >Escrituración</asp:LinkButton>
            <asp:LinkButton ID="lnkRecuperacion" runat="server" class="botoMaestra btn" OnClick="lnkRecuperacion_Click" >Recuperación</asp:LinkButton>
            <asp:LinkButton ID="lnkEventos" runat="server" class="botoMaestra btn" OnClick="lnkEventos_Click" >Registro de Eventos</asp:LinkButton>
            <asp:LinkButton ID="lnkReportes" runat="server" class="botoMaestra btn" OnClick="lnkReportes_Click" >Reportes</asp:LinkButton>
            <asp:LinkButton ID="lnkMantenedores" runat="server" class="botoMaestra btn" OnClick="lnkMantenedores_Click" >Mantenedores</asp:LinkButton>
        </div>   

        <iframe  style="width:100%; height:800px;" frameborder="0" runat="server" id="Iframe1"></iframe>
    </article>
</asp:Content>
