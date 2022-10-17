<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmProyectoDetalle.aspx.cs" Inherits="MaestraNet.GC.SVTA.Mantenedor.frmProyectoDetalle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
   
    
    <link href="../../../open-iconic/font/css/open-iconic-bootstrap.css" rel="stylesheet" />
    <link href="../sistema_venta.css" rel="stylesheet" />
    
    <script src="//maps.googleapis.com/maps/api/js?key=AIzaSyADc5J8q_FYaQOt_tF0Sd43ZeHeDj7Q1ss"></script>
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function isEmail(email) {
            var control = email;
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(control.value)) {
                control.focus();
            }
        }
        function showAlertaInformar() {
            $('#modalAlertaInformar').modal('show');
        }
        function showAlertaError() {
            $('#modalAlertaError').modal('show');
        }
        function showAlertaAlert() {
            $('#modalAlertaAlert').modal('show');
        }
    </script>
    <script>
        // Note: This example requires that you consent to location sharing when
        // prompted by your browser. If you see the error "The Geolocation service
        // failed.", it means you probably did not give permission for the browser to
        // locate you.
        var mapcode;
        var diag;
        function initMap() {
            mapcode = new google.maps.Geocoder();
            var lnt = new google.maps.LatLng(26.45, 82.85);
            var diagChoice = {
                zoom: 14,
                center: lnt
            }
            diag = new google.maps.Map(document.getElementById('map'), diagChoice);

            // Try HTML5 geolocation.
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };
                    var marker = new google.maps.Marker({
                        position: pos,
                        map: diag
                    });

                    diag.setCenter(pos);
                }, function () {
                    handleLocationError(true, infoWindow, map.getCenter());
                });
            } else {
                // Browser doesn't support Geolocation
                handleLocationError(false, infoWindow, map.getCenter());
            }
        }
        function getmap() {
            mapcode = new google.maps.Geocoder();
            var lnt = new google.maps.LatLng(26.45, 82.85);
            var diagChoice = {
                zoom: 14,
                center: lnt
            }
            diag = new google.maps.Map(document.getElementById('map'), diagChoice);

            var completeaddress = document.getElementById('txtDireccion').value;
            mapcode.geocode({ 'address': completeaddress }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    diag.setCenter(results[0].geometry.location);
                    document.getElementById('txtLatitud').value = results[0].geometry.location.lat();
                    document.getElementById('txtLongitud').value = results[0].geometry.location.lng();
                    var hint = new google.maps.Marker({
                        map: diag,
                        position: results[0].geometry.location
                    });
                } else {
                    alert('Dirección no encontrada. ' + status);
                }
            });
        }
        function ValidaMonto() {
            var monto = document.getElementById('<%=txtMonto.ClientID%>').value
            var tipoInmueble = document.getElementById('<%=ddlTipoInmueble.ClientID%>').value
            
            if (monto == '' || monto == 0) {
                document.getElementById('<%=lblAlertaMSGAlert.ClientID%>').textContent = "Ingrese el monto o porcentaje"
                $('#modalAlertaAlert').modal('show');
                document.getElementById('<%=txtMonto.ClientID%>').focus();
                return false;
            }
            else if (tipoInmueble == 0) {
                document.getElementById('<%=lblAlertaMSGAlert.ClientID%>').textContent = "Seleccione tipo de inmueble"
                $('#modalAlertaAlert').modal('show');
                document.getElementById('<%=ddlTipoInmueble.ClientID%>').focus();
                return false;
            }
            return true;
        }

        function ValidaFiltro() {
            var Depto = document.getElementById('<%=txtDepto.ClientID%>').value
            if (isNaN(Depto)) {
                 document.getElementById('<%=lblAlertaMSGAlert.ClientID%>').textContent = "Ingrese solo numeros en depto"
                $('#modalAlertaAlert').modal('show');
                document.getElementById('<%=txtDepto.ClientID%>').focus();
                return false;
            }
        }
    </script>
   
    <style>
        label {
            display: inline-block;
            margin-bottom: 10px;
            font-weight: normal; 
        }
    
    </style>

    <script type="text/javascript">  
        $(function () {
            $('[id*=lstPisos]').multiselect({
                includeSelectAllOption: true
            });
        });
        $(function () {
            $('[id*=lstTorre]').multiselect({
                includeSelectAllOption: true
            });
        });
         $(function () {
            $('[id*=lstModeloInmueble]').multiselect({
                includeSelectAllOption: true
            });
        });
        
    </script>
    <title>Mantenedor Proyecto</title>
</head>
<body>
    <form id="frmProyecDetalle" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <br />
        <br />
        <table class="principal">
            <tr>
                <td>
                    <asp:Panel ID="pnDatosProyecto" runat="server">
                            <asp:Label ID="lblIdProyecto" runat="server" Visible="false"></asp:Label>

                            <fieldset class="principal">
                                <legend>
                                    <asp:Label ID="lblTituloPromesa" runat="server" Text=" Datos del Proyecto" CssClass="principal"></asp:Label>
                                </legend>
                                <div class="container-fluid">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label for="Proyecto">
                                                        Proyecto:
                                                <br />
                                                        <asp:TextBox ID="txtProyecto" runat="server" Width="180px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                                    </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <label for="Region">
                                                        Región:
                                                <br />
                                                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="BordeRadio10" DataSourceID="sdsRegion" DataTextField="RegNombre" DataValueField="RegCodigo" AutoPostBack="True" Width="200" BackColor="Yellow"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsRegion" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                                            SelectCommand="sp_VTA_Regiones" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                    </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <label for="Comuna">
                                                        Comuna:
                                                <br />
                                                        <asp:DropDownList ID="ddlComuna" runat="server" CssClass="BordeRadio10" DataSourceID="sdsComuna" DataTextField="CmuNombre" DataValueField="CmuCodigo" BackColor="Yellow" Width="100"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsComuna" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                                            SelectCommand="sp_VTA_Comunas" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="ddlRegion" Name="region" PropertyName="SelectedValue" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </label>

                                                </div>
                                                <div class="col-md-4">
                                                    <label for="Direccion">
                                                        Dirección:
                                        <br />
                                                        <asp:TextBox ID="txtDireccion" runat="server" Width="220px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                                        <a class="botoMaestra btn" href="javascript:getmap();" style="line-height: 0; height: 23px; padding: 3px 5px;">Buscar <span class="oi oi-map-marker"></span>
                                                        </a>
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label for="Empresa">
                                                        Empresa:
                                                <br />
                                                        <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="BordeRadio10" DataSourceID="sdsEmpresa" DataTextField="EntNomFantasia" DataValueField="EmpId" BackColor="Yellow" Width="200"  AutoPostBack="True"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsEmpresa" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                                            SelectCommand="sp_VTA_Empresa" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                    </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <label for="Division">
                                                        Division:
                                                <br />
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="BordeRadio10"  DataSourceID="sdsDivision" DataTextField="DivGlosa" DataValueField="DivCodigo" BackColor="Yellow" Width="200"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsDivision" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                                            SelectCommand="sp_VTA_DivisionEmpresa" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="ddlEmpresa" Name="idEmpresa" PropertyName="SelectedValue" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                        
                                                    </label>
                                                </div>
                                                <div class="col-md-3">
                                                    Email:
                                        <br />
                                                    <label for="Email">
                                                        <asp:TextBox ID="txtEmail" runat="server" Width="150px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                                    </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <label for="Sala Venta">
                                                        Sala Venta:
                                                <br />
                                                        <asp:DropDownList ID="ddlSalaVenta" runat="server" DataSourceID="sdsSalaVenta" CssClass="BordeRadio10" BackColor="Yellow" DataValueField="IdSalaVenta" DataTextField="Nombre">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsSalaVenta" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                                            SelectCommand="sp_VTA_SalaVenta" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                    </label>
                                                </div>
                                               
                                            </div>
                                            <div class="row">
                                                 <div class="col-md-3">
                                                    <label for="FechaInicioVenta">
                                                        Inicio de Venta:
                                        <br />
                                                        <input type="date" value="" id="txtFechaVenta" min="" runat="server" size="60" class="BordeRadio10" style="background-color: #FFFF00;" />
                                                    </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <label for="FechaRecepcion">
                                                        Recepción:
                                        <br />
                                                        <input type="date" value="" id="txtFechaRecepcion" min="" runat="server" size="60" class="BordeRadio10" style="background-color: #FFFF00;" />
                                                    </label>

                                                </div>
                                                <div class="col-md-1">
                                                    <label for="M2">
                                                        M2:
                                                        <br />
                                                        <asp:TextBox ID="txtM2" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                                    </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <label for="EstadoEntrega">
                                                        Estado Entrega:
                                        <br />
                                                        <asp:DropDownList ID="ddlEstadoEntrega" runat="server" CssClass="BordeRadio10" BackColor="Yellow" DataSourceID="sdsEstadoEntrega" DataTextField="Nombre" DataValueField="IdEstadoEntrega" Width="100"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsEstadoEntrega" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                                            SelectCommand="sp_VTA_EstadoEntrega" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                    </label>
                                                    <asp:TextBox ID="txtLatitud" runat="server" Width="50px" Style="display: none;" />
                                                    <asp:TextBox ID="txtLongitud" runat="server" Width="50px" Style="display: none;" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label for="M2">
                                                        Valor Terreno (UF):
                                                        <br />
                                                        <asp:TextBox ID="txtValorTerreno" runat="server" Width="70px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                                    </label>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </div>
                                <br />
                            </fieldset>
                            <br />
                            <table class="principal">
                                <tr>
                                    <td align="left">
                                        <asp:LinkButton ID="lnkVolver" runat="server" CssClass="botoMaestra btn" OnClick="lnkVolver_Click">Volver</asp:LinkButton>
                                    </td>
                                    <td align="right">
                                        <asp:LinkButton ID="lnkConfirmModificar" runat="server" CssClass="botoMaestra btn" Width="150" ToolTip="Modificar Proyecto" OnClick="lnkConfirmModificar_Click">Modificar Proyecto</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="btn-group" role="group" aria-label="Basic example">
                        <asp:LinkButton ID="LinkButton1" runat="server" class="botonTabActive btn" OnClick="LinkButton1_Click">Mapa</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" class="botonTab btn" OnClick="LinkButton2_Click">Cambio de Precios</asp:LinkButton>
                    </div>
                    <br />
                    <br />
                    <br />
                    <%--<cc1:GMap ID="GMap1" runat="server" Key="AIzaSyADc5J8q_FYaQOt_tF0Sd43ZeHeDj7Q1ss" 
                            Height="600px" Width="800px" ajaxUpdateProgressMessage="Cargando..." 
                        BackColor="White" BorderColor="#000099" BorderStyle="None"    />--%>
                    <asp:Panel Height="400px" ID="map" class="principal" runat="server"></asp:Panel>
                    <asp:UpdatePanel ID="pnCambiaPrecio" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <fieldset class="principal">
                                <legend>
                                    <asp:Label ID="lblTituloInmueble" runat="server" Text="Modificar Precios" CssClass="principal"></asp:Label>
                                </legend>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="TipoInmueble">
                                                Tipo Inmueble:
                                        <br />
                                                <asp:DropDownList ID="ddlTipoInmueble" runat="server" DataSourceID="sdsTipoInmueble" CssClass="BordeRadio10" DataValueField="IdTipoInmueble" DataTextField="Nombre" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoInmueble_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsTipoInmueble" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                                    SelectCommand="sp_VTA_TipoInmueble" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                            </label>
                                        </div>
                                        <div class="col-md-3">
                                            <label for="ModeloInmueble">
                                                Modelo Inmueble:<br />
                                                <asp:ListBox ID="lstModeloInmueble" runat="server" SelectionMode="Multiple" Width="100px" CssClass="BordeRadio10" DataValueField="IdModeloInmueble" DataTextField="Nombre" AutoPostBack="True" OnSelectedIndexChanged="lstModeloInmueble_SelectedIndexChanged1" style="border-radius: 10px 10px 10px 10px;"></asp:ListBox>
                                            </label>
                                        </div>

                                        <div class="col-md-2">

                                            <label for="Piso">
                                                Piso:<br />
                                                <asp:ListBox ID="lstPisos" runat="server" SelectionMode="Multiple" Width="100px" CssClass="BordeRadio10"></asp:ListBox>
                                            </label>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="Torre">
                                                Torre:<br />
                                                <asp:ListBox ID="lstTorre" runat="server" SelectionMode="Multiple" Width="100px" CssClass="BordeRadio10"></asp:ListBox>
                                            </label>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="Depto">
                                                Depto:<br />
                                                <asp:TextBox ID="txtDepto" runat="server" Width="50px" MaxLength="5" CssClass="BordeRadio10"></asp:TextBox>
                                            </label>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="Torre">
                                                Aumenta / Disminuye:
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td width="50%">
                                                                                    <asp:RadioButton ID="rbSube" GroupName="sube" runat="server" Text="&nbsp;Sube" Checked="True" />
                                                                                </td>
                                                                                <td width="50%">
                                                                                    <asp:RadioButton ID="rbBaja" GroupName="sube" runat="server" Text="&nbsp;Baja" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                            </label>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="UF">
                                                Porcentaje / UF:
                                            </label>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td width="60%">
                                                        <asp:RadioButton ID="rbPorcentaje" GroupName="Porc" runat="server" Text="&nbsp;Porcentaje" Checked="True" />
                                                    </td>
                                                    <td width="40%">
                                                        <asp:RadioButton ID="rbPesos" GroupName="Porc" runat="server" Text="&nbsp;UF" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="Monto">
                                                Monto / Porcentaje:<br />
                                                <asp:TextBox ID="txtMonto" runat="server" Width="50px" CssClass="BordeRadio10" MaxLength="50"></asp:TextBox>
                                            </label>
                                        </div>
                                        <div class="col-md-4">
                                            <label for="Orientacion">
                                                Orientación:
                                        <br />
                                                <asp:DropDownList ID="ddlOrientacion" runat="server" DataSourceID="sdsOrientacion" CssClass="BordeRadio10" DataValueField="IdOrientacion" DataTextField="Orientacion">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsOrientacion" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                                    SelectCommand="sp_VTA_Orientacion" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                            </label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton ID="LnkFiltrar" runat="server" CssClass="botoMaestra btn" Width="150" ToolTip="FiltrarInmueble" OnClick="LnkFiltrar_Click" OnClientClick="return ValidaFiltro();">Filtar Inmueble</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row" style="height:400px;">
                                        <div class="col-md-12">
                                            <div style="width: 100%; height: 400px; overflow: scroll">
                                            <asp:GridView ID="gvInmueble" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                                 AllowSorting="True" CssClass="grid_data">
                                                <AlternatingRowStyle CssClass="grid_linea_alterna"/>
                                                <Columns>
                                                    <asp:BoundField DataField="piso" HeaderText="Piso" ReadOnly="True" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                                    <asp:BoundField DataField="Edificio" HeaderText="Edificio" ReadOnly="True" HeaderStyle-Font-Underline="true" />
                                                    <asp:BoundField DataField="modeloInmueble" HeaderText="Modelo" ReadOnly="True" HeaderStyle-Font-Underline="true" />
                                                    <asp:BoundField DataField="numero" HeaderText="Numero" ReadOnly="True" HeaderStyle-Font-Underline="true" />
                                                    <asp:BoundField DataField="EstadoInmueble" HeaderText="Estado Inmueble" HeaderStyle-Font-Underline="true" />
                                                    <asp:BoundField DataField="Orientacion" HeaderText="Orientacion" ReadOnly="True" HeaderStyle-Font-Underline="true" />
                                                    <asp:BoundField DataField="PrecioLista" HeaderText="Precio Lista" ReadOnly="True" HeaderStyle-Font-Underline="true" />
                                                    <asp:TemplateField HeaderText="Nuevo Precio">
													<ItemTemplate>
														<asp:TextBox ID="txtNuevoPrecio" CssClass="BordeRadio10" runat="server"  Width="90px" Height="25px" Text='<%# Bind("NuevoPrecio") %>'></asp:TextBox>
                                                        <asp:HiddenField ID="idInmueble" Value='<%# Eval("IdInmueble") %>' runat="server" />
													</ItemTemplate>
												</asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid_footer" />
                                                <HeaderStyle CssClass="grid_header" />
                                                <PagerStyle CssClass="grid_pager" HorizontalAlign="Center" />
                                                <RowStyle CssClass="grid_row" />
                                                <SelectedRowStyle CssClass="grid_selected_row" />
                                                <SortedAscendingCellStyle CssClass="grid_selected_row_asc" />
                                                <SortedAscendingHeaderStyle CssClass="grid_selected_header_asc" />
                                                <SortedDescendingCellStyle CssClass="grid_selected_row_des" />
                                                <SortedDescendingHeaderStyle CssClass="grid_selected_header_des" />
                                            </asp:GridView>
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <br />
                             <table class="principal">
                                <tr>
                                    <td align="left">
                                        
                                    </td>
                                    <td align="right">
                                        <asp:LinkButton ID="lnkPrecios" runat="server" CssClass="botoMaestra btn" Width="150" ToolTip="Modificar Precios" OnClick="lnkPrecios_Click">Modificar Precios</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkPrecios" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <br />
        <br />

        <table class="principal">
            <tr>
                <td colspan="2" style="height: 400px;"></td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="height: 400px;">
                        <ContentTemplate>
                            <!-- Alerta Error-->
                            <div class="modal fade" id="modalAlertaError" tabindex="-1" role="dialog" aria-labelledby="exampleModalDetalle" aria-hidden="true">
                                <div class="modal-dialog modal-sm" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">

                                            <img src="../../../images/error.png" width="50" />&nbsp;&nbsp;
						                        <asp:Label ID="lblTituloError" runat="server" CssClass="error_modal">Error</asp:Label>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <h6 style="margin-right: auto; margin-left: auto">
                                                <asp:Label ID="lblAlertaMSGError" runat="server" Text=""></asp:Label>
                                            </h6>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="botoMaestra btn" data-dismiss="modal">Aceptar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- FIN - Alerta Error-->

                            <!-- Alerta Información-->
                            <div class="modal fade" id="modalAlertaInformar" tabindex="-1" role="dialog" aria-labelledby="exampleModalDetalle" aria-hidden="true">
                                <div class="modal-dialog modal-sm" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <img src="../../../images/info.png" width="50" />&nbsp;&nbsp;
						                        <asp:Label ID="lblTituloInformar" runat="server" CssClass="info_modal">Información</asp:Label>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <h6 style="margin-right: auto; margin-left: auto">
                                                <asp:Label ID="lblAlertaMSGInfo" runat="server" Text=""></asp:Label>
                                            </h6>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="botoMaestra btn" data-dismiss="modal">Aceptar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Alerta Alert-->
                            <div class="modal fade" id="modalAlertaAlert" tabindex="-1" role="dialog" aria-labelledby="exampleModalDetalle" aria-hidden="true">
                                <div class="modal-dialog modal-sm" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <asp:Label ID="Label15" runat="server" CssClass="error_modal"><span runat="server" class="oi oi-warning"></span>&nbsp;&nbsp;Advertencia</asp:Label>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <h6 style="margin-right: auto; margin-left: auto">
                                                <asp:Label ID="lblAlertaMSGAlert" runat="server" Text=""></asp:Label>
                                            </h6>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="botoMaestra btn" data-dismiss="modal">Aceptar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- FIN - Alerta Alert-->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
