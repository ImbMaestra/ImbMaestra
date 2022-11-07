<%@ Page Language="C#" maintainScrollPositionOnPostBack="true" AutoEventWireup="true" CodeBehind="frmInmueblesMasivo.aspx.cs" Inherits="MaestraNet.GC.SVTA.Mantenedor.frmInmueblesMasivo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../../../open-iconic/font/css/open-iconic-bootstrap.css" rel="stylesheet" />
    <script src="../../../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../../../Scripts/bootstrap.min.js"></script>
    <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../sistema_venta.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <script src="https://code.iconify.design/1/1.0.3/iconify.min.js"></script>
    <script language="Javascript">
    //const { debug } = require("node:util");

        function showAlertaInformar() {
            $('#modalAlertaInformar').modal('show');
        }

        function showAlertaError() {
            $('#modalAlertaError').modal('show');
        }

        function showAlertaAlert() {
            $('#modalAlertaAlert').modal('show');
        }

        function showAlertaConfirmar() {
            $('#modalAlertaConfirmar').modal('show');
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) )
                return false;
            return true;
        }

        function isNumberMasMenos(evt, texto)
        {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            var n = texto.split("-").length - 1;

            if (n > 0) {
                if (charCode == 45)
                    return false;
            }
            else if ((texto.length > 0) && (charCode == 45))
                return false;


            if ((charCode > 47 && charCode < 58) || (charCode == 45))
                return true;
            return false;
        }


        function filterFloat(evt, input) {
            // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
            var key = window.Event ? evt.which : evt.keyCode;
            var chark = String.fromCharCode(key);
            var tempValue = input.value + chark;
            if (key >= 48 && key <= 57) {
                if (filter(tempValue) === false) {
                    return false;
                } else {
                    return true;
                }
            } else {
                if (key == 8 || key == 13 || key == 46 || key == 0) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        function filter(__val__) {
            var preg = /^([0-9]+\.?[0-9]{0,2})$/;
            if (preg.test(__val__) === true) {
                return true;
            } else {
                return false;
            }

        }


        $(document).ready(function () {
            $("#btnAceptarCambios").click(function (e) {
                var url = "frmInmueblesMasivo.aspx/Prueba";
                //var url = "@Url.Content('~/frmInmueblesMasivo.aspx/Prueba')";
                //$.ajax({
                //    type: 'POST',
                //    //url: baseUrl + url,
                //    //data: "{}",
                //    url: url,
                //    dataType: 'json',
                //    contentType: "application/json; charset=utf-8",
                //    success: function (resp) {
                //        //alert("Va");
                //        //$("#AvisoCierreSession").modal("hide");
                //    }
                //    , error: function (ex) {
                //        alert('Error al Reactivar Sesión ' + ex);
                //    }
                //});
                //return false;



                //$.ajax({
                //    type: "POST",
                //    url: url,
                //    data: '{TempId:1}',
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (data) {
                //            alert('Data: ' + data);
                //        },
                //    error: function (result) {
                //        alert("Error" + result);
                //    }
                //});

                $.ajax({
                    type: "POST",
                    url: 'frmInmueblesMasivo.aspx/Prueba',
                    data: '{TempId: 1}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // fail-safe for older ASP.NET frameworks
                        //var data = response.hasOwnProperty("d") ? response.d : response;
                        alert(response);  //Changed here
                    },
                    failure: function (response) {

                    }
                });


            });

            $('#ConfirmModificar').click(function () {
                //alert("Levantar popup");
                showAlertaConfirmar();
            });
        });


        //$(document).ready(function () {
        //    $("#btnUno").click(function () {
        //        //$('#modalAlertaError').modal('show');
        //        showAlertaAlert();
        //        //$('#modalAlertaInformar').modal({ backdrop: true });
        //        //$('#myModal').modal({
        //        //    show: 'true'
        //        //});
        //        //alert("Holanda");
        //    });
        //});
    </script>
    <title>Edición Masiva de Inmuebles</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <table class="principal">
            <tr>
                <td>
                    <!-- Filtros -->
                    <fieldset class="principal">
                        <legend>
                            <asp:Label runat="server" Text="Aplicar Cambios Masivos" CssClass="principal"></asp:Label>
                            <%--<asp:TextBox ID="txtProyecto"  runat="server" Width="200px"  CssClass="BordeRadio10" ></asp:TextBox>--%>
                        </legend>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-2" style="text-align:right">
                                    <label for="Terraza" > <!--M2Terreno-->
                                        Cantidad a modificar:
                                    </label>
                                </div>
                                <div class="col-md-2" style="text-align:left">
                                    <asp:TextBox ID="txtCantidad" style="text-align:right" runat="server"  CssClass="BordeRadio10" enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-2" style="text-align:right">
                                    <label for="Terraza"> <!--M2Terreno-->
                                        Proyecto:
                                    </label>
                                </div>
                                <div class="col-md-2" style="text-align:left">
                                    <asp:TextBox ID="txtProyecto" style="text-align:right" runat="server"  CssClass="BordeRadio10" enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-2" style="text-align:right">
                                    <label for="M2 Terraza"> <!--M2Terreno-->
                                        M2 Terraza:
                                    </label>
                                </div>
                                <div class="col-md-2" style="text-align:left">
                                    <asp:TextBox ID="txtM2Terraza" style="text-align:right" runat="server"  MaxLength="8" CssClass="BordeRadio10" onkeypress="return filterFloat(event,this);" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            </br>
                            <div class="row">
                                <div class="col-md-2" style="text-align:right">
                                    <label for="M2 Util"> <!--M2-->
                                        M2 Util:
                                    </label>
                                </div>
                                <div class="col-md-2" style="text-align:left">
                                    <asp:TextBox ID="txtMetroUtil" style="text-align:right" runat="server"  MaxLength="8" CssClass="BordeRadio10" onkeypress="return filterFloat(event,this);" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2" style="text-align:right">
                                    <label for="Precio Lista">  <!-- -->
                                        Estado Inmueble:
                                    </label>
                                </div>
                                <div class="col-md-2" style="text-align:left">
                                    <asp:DropDownList ID="ddlEstadoInmueble" runat="server" CssClass="BordeRadio10" DataSourceID="sdsEstadoInmueble" DataTextField="Estado" DataValueField="IdEstado" AutoPostBack="True" OnSelectedIndexChanged="ddlEstadoInmueble_SelectedIndexChanged"></asp:DropDownList>
                                        &nbsp;<asp:SqlDataSource ID="sdsEstadoInmueble" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                            SelectCommand="sp_VTA_Estados_Inmuebles" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                </div>
                                <div class="col-md-2" style="text-align:right">
                                    <label for="Justificacion"> <!--Justificacion-->
                                        Justificacion:
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtJustificacion" style="text-align:left" runat="server"  MaxLength="50" CssClass="BordeRadio10" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
     
                            <div class="row">
                                <div class="col-md-5" style="text-align:center">
                                    <asp:RadioButton ID="RdoPrecioLista1" runat="server" GroupName="PrecioLista" Text="Fijar" />
                                   &nbsp;
                                    <asp:RadioButton ID="RdoPrecioLista2" runat="server" GroupName="PrecioLista" Text="Aumento %" />
                                   &nbsp;
                                    <asp:RadioButton ID="RdoPrecioLista4" runat="server" GroupName="PrecioLista" Text="Disminuye %" />
                                   &nbsp;
                                    <asp:RadioButton ID="RdoPrecioLista3" runat="server" GroupName="PrecioLista" Text="Aumento UF" />    
                                   &nbsp;
                                    <asp:RadioButton ID="RdoPrecioLista5" runat="server" GroupName="PrecioLista" Text="Disminuye UF" />
                                </div>
                                <div class="col-md-7"> 
                                </div>
                            </div>

                            <div class="row">
                                 <div class="col-md-2" style="text-align:right"> 
                                     <label for="Precio Lista">  <!--PrecioLista-->
                                        Precio Lista:
                                    </label>
                                </div>
                                <div class="col-md-2" style="text-align:left">
                                    <asp:hiddenfield id="hddTipoPrecioLista" value="0" runat="server"/>
                                    <asp:TextBox ID="txtPrecioLista" style="text-align:right" runat="server"  MaxLength="4" CssClass="BordeRadio10" onkeypress="return isNumberKey(event)" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2" style="text-align:right">
                                    <label for="Alicuota">  <!--Alicuota-->
                                        Alicuota:
                                    </label>
                                </div>
                                <div class="col-md-2" style="text-align:left">
                                    <asp:TextBox ID="txtAlicuota" style="text-align:right" runat="server" MaxLength="50" CssClass="BordeRadio10" autocomplete="off"></asp:TextBox>
                                </div>
                                 <div class="col-md-2" style="text-align:right">
                                    <label for="Numero Rol">  <!--NumeroRol-->
                                        Numero Rol:
                                    </label>
                                </div>
                                <div class="col-md-2" style="text-align:left">
                                    <asp:TextBox ID="txtNumeroRol" style="text-align:right" runat="server"  MaxLength="10" CssClass="BordeRadio10" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                               
                            <br />
                            <div class="row">
                                <div class="col-md-8" style="text-align:left">
                                    <span style="font-weight:bold;">
                                        <ul>
                                            <li>
                                                Debe ingresar al menos un valor dentro de los campos para poder grabar.
                                            </li>
                                        </ul>
                                    </span>
                                </div>   
                                <div class="col-md-4" style="text-align:right">
                                    <asp:LinkButton ID="lnkPrevisualizaCambios" runat="server" CssClass="botoMaestra btn" Width="150" ToolTip="Modificar Inmueble" OnClick="lnkPrevisualizaCambios_Click">Previsualizar Cambios</asp:LinkButton>
                                </div>     
                            </div>
                        </div>

                    </fieldset>
                    <!-- FIN Filtros -->
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <div id="GrillaDatosActuales">
                        <fieldset class="principal">
                            <legend>
                                <asp:Label ID="lblListaDatosActuales" runat="server" Text="Datos Actuales" CssClass="principal"></asp:Label>
                            </legend>
                            <asp:GridView ID="gvDatosActuales" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                    DataKeyNames="IdInmueble" 
                                    AllowPaging="True" AllowSorting="false" OnPageIndexChanging="gvDatosActuales_PageIndexChanging" CssClass="grid_data" PageSize="20">
                                <AlternatingRowStyle CssClass="grid_linea_alterna" />
                                <Columns>
                                    <asp:BoundField DataField="IdInmueble" HeaderText="Id Inmueble" ReadOnly="True" SortExpression="IdInmueble" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="TerrazaPrev" HeaderText="Terraza" ReadOnly="True" SortExpression="TerrazaPrev" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="M2UtilPrev" HeaderText="M2 Util" ReadOnly="True" SortExpression="M2UtilPrev" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Piso" HeaderText="Piso" ReadOnly="True" SortExpression="Piso" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="PrecioLista" HeaderText="Precio Lista" ReadOnly="True" SortExpression="PrecioLista" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="EstadoInmueble" HeaderText="Estado" ReadOnly="True" SortExpression="EstadoInmueble" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="NumeroRol" HeaderText="Numero Rol" ReadOnly="True" SortExpression="NumeroRol" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Alicuota" HeaderText="Alicuota" ReadOnly="True" SortExpression="Alicuota" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="JustificacionEstadoInmueble" HeaderText="Justificación" ReadOnly="True" SortExpression="JustificacionEstadoInmueble" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="NDepto" HeaderText="Numero" ReadOnly="True" SortExpression="NDepto" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="ModeloInmueble" HeaderText="Modelo Inmueble" ReadOnly="True" SortExpression="ModeloInmueble" HeaderStyle-Font-Underline="true" />


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
                            <br />
                        </fieldset>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <div id="GrillaInmueble">
                        <fieldset class="principal">
                            <legend>
                                <asp:Label ID="lblLista" runat="server" Text=" Visualización datos" CssClass="principal"></asp:Label>
                            </legend>

                            <asp:GridView ID="gvInmueblesVista" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                    DataKeyNames="IdInmueble" 
                                    AllowPaging="True" AllowSorting="false" OnPageIndexChanging="gvInmueblesVista_PageIndexChanging" CssClass="grid_data" PageSize="20">
                                <AlternatingRowStyle CssClass="grid_linea_alterna" />
                                <Columns>
                                    <asp:BoundField DataField="IdInmueble" HeaderText="Id Inmueble" ReadOnly="True" SortExpression="IdInmueble" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="TerrazaPrev" HeaderText="Terraza" ReadOnly="True" SortExpression="TerrazaPrev" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="M2UtilPrev" HeaderText="M2 Util" ReadOnly="True" SortExpression="M2UtilPrev" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Piso" HeaderText="Piso" ReadOnly="True" SortExpression="Piso" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="PrecioLista" HeaderText="Precio Lista" ReadOnly="True" SortExpression="PrecioLista" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="EstadoInmueble" HeaderText="Estado" ReadOnly="True" SortExpression="EstadoInmueble" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="NumeroRol" HeaderText="Numero Rol" ReadOnly="True" SortExpression="NumeroRol" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Alicuota" HeaderText="Alicuota" ReadOnly="True" SortExpression="Alicuota" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="JustificacionEstadoInmueble" HeaderText="Justificación" ReadOnly="True" SortExpression="JustificacionEstadoInmueble" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="NDepto" HeaderText="Numero" ReadOnly="True" SortExpression="NDepto" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="ModeloInmueble" HeaderText="Modelo Inmueble" ReadOnly="True" SortExpression="ModeloInmueble" HeaderStyle-Font-Underline="true" />

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
                            <br />
                            <br />


                        </fieldset>
                    </div>
                </td>

            </tr>


        </table>
        <br />
        <table class="principal">
            <tr>
                <td align="left">
                    <asp:LinkButton ID="lnkVolver" runat="server" CssClass="botoMaestra btn" OnClick="lnkVolver_Click">Volver</asp:LinkButton>
                </td>
                <td align="right">
                    <asp:LinkButton ID="lnkConfirmModificar" runat="server" CssClass="botoMaestra btn" Width="150" ToolTip="Modificar Inmueble" OnClientClick="return window.confirm('Seguro que desea modificar los registros?')" OnClick="lnkConfirmModificar_Click">Modificar Inmueble</asp:LinkButton>
                    <%--<asp:LinkButton ID="lnkConfirmModificar" runat="server" CssClass="botoMaestra btn" Width="150" ToolTip="Modificar Inmueble" OnClick="lnkConfirmModificar_Click">Modificar Inmueble</asp:LinkButton>
                    <button type="button" id="ConfirmModificar" class="botoMaestra btn" data-dismiss="modal">Prueba Modificar</button>--%>
                    
                </td>
            </tr>
        </table>
    </div>

        <!-- Alerta Error-->
        <div class="modal fade" id="modalAlertaError" tabindex="-1" role="dialog" aria-labelledby="exampleModalDetalle" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">    
                    <div class="modal-header">
						<asp:Label ID="lblTituloError" runat="server" CssClass="error_modal"><span runat="server" class="oi oi-circle-x"></span>&nbsp;&nbsp;Error</asp:Label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
	                <div class="modal-body">
                        <h6 style="margin-right:auto; margin-left:auto">
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

        <!-- Alerta Alert-->
        <div class="modal fade" id="modalAlertaAlert" tabindex="-1" role="dialog" aria-labelledby="exampleModalDetalle" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">    
                    <div class="modal-header">
						<asp:Label ID="Label8" runat="server" CssClass="error_modal"><span runat="server" class="oi oi-warning"></span>&nbsp;&nbsp;Advertencia</asp:Label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
	                <div class="modal-body">
                        <h6 style="margin-right:auto; margin-left:auto">
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

		<!-- Alerta Información-->
        <div class="modal fade" id="modalAlertaInformar" tabindex="-1" role="dialog" aria-labelledby="exampleModalDetalle" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">    
                    <div class="modal-header">                        
						<asp:Label ID="lblTituloInformar" runat="server" CssClass="info_modal"><span runat="server" class="oi oi-info"></span>&nbsp;&nbsp;Información</asp:Label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
	                <div class="modal-body">
                        <h6 style="margin-right:auto; margin-left:auto">
							<asp:Label ID="lblAlertaMSGInfo" runat="server" Text=""></asp:Label>
						</h6>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="botoMaestra btn" data-dismiss="modal">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>   
        <!-- FIN - Alerta Información-->

        <!-- Alerta Confirmación-->
        <div class="modal fade" id="modalAlertaConfirmar" tabindex="-1" role="dialog" aria-labelledby="exampleModalDetalle" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">    
                    <div class="modal-header">                        
						<asp:Label ID="Label1" runat="server" CssClass="info_modal"><span runat="server" class="oi oi-info"></span>&nbsp;&nbsp;Información</asp:Label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
	                <div class="modal-body">
                        <h6 style="margin-right:auto; margin-left:auto">
							<asp:Label ID="lblAlertaMSGConfirmar" runat="server" Text=""></asp:Label>
						</h6>
                    </div>
                    <div class="modal-footer">
                        <%--<button type="button" class="botoMaestra btn" data-dismiss="modal">Aceptar</button>--%>
                        <%--<asp:Button ID="btnAceptarCambios" class="botoMaestra btn" data-dismiss="modal" runat="server" Text="Aceptar"/>--%>
                        <button id="btnAceptarCambios" name="btnAceptarCambios" type="submit" class="botoMaestra btn">Aceptar</button>
                        <%--<input type="button" id="btnUno" value="Boton" />--%>
                        <%--<button id="btnUno" name="button" class="botoMaestra btn">Click me</button>--%>
                        <%--<asp:LinkButton ID="btnUno" runat="server" CssClass="botoMaestra btn">
                            Buscar <span class="oi oi-magnifying-glass"></span>
                        </asp:LinkButton>--%>
                        &nbsp;
                        <%--<asp:Button ID="btnCancelarCambios" class="botoMaestra btn" data-dismiss="modal" runat="server" Text="Cancelar"/>--%>

                    </div>
                </div>
            </div>
        </div>   
        <!-- FIN - Alerta Información-->
    </form>
</body>
</html>
