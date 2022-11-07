<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmInmuebleDetalleNew.aspx.cs" Inherits="MaestraNet.GC.SVTA.Mantenedor.frmInmuebleDetalleNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../../open-iconic/font/css/open-iconic-bootstrap.css" rel="stylesheet" />
    <script src="../../../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../../../Scripts/bootstrap.min.js"></script>
    <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../sistema_venta.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <script src="https://code.iconify.design/1/1.0.3/iconify.min.js"></script>
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
    <title>Administrador de Inmueble</title>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <table class="principal">
            <tr>
                <td>
                    <asp:Label ID="lblIdInmueble" runat="server" Visible="false"></asp:Label>

                    <fieldset class="principal">
                        <legend>
                            <asp:Label ID="lblTituloInmueble" runat="server" Text=" Datos del Inmueble" CssClass="principal"></asp:Label>
                        </legend>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:HiddenField ID="hddTipoInmueble" Value='' runat="server" />
                                    <label for="ModeloInmueble">
                                        Modelo: &nbsp;
                                        <asp:DropDownList ID="ddlModeloInmueble" runat="server" DataSourceID="sdsModeloInmueble" CssClass="BordeRadio10" BackColor="Yellow" DataValueField="IdModeloInmueble" DataTextField="Nombre">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsModeloInmueble" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                            SelectCommand="sp_VTA_ModeloInmueble" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </label>
                                </div>
								
                                <div class="col-md-2">
                                    <label for="Orientacion">
                                        Orientacion: &nbsp;
                                       <%-- <asp:TextBox ID="txtOrientacion" runat="server" Width="80px" ReadOnly="true" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="50"></asp:TextBox>--%>
                                    
                                        <asp:DropDownList ID="ddlOrientacion" runat="server" Width="110px" CssClass="BordeRadio10" BackColor="Yellow" DataSourceID="sdsOrientacion" DataTextField="Orientacion" DataValueField="IdOrientacion"></asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsOrientacion" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                                    SelectCommand="sp_VTA_Orientacion" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </label>
                                </div>	

                                <div class="col-md-2">
                                    <label for="Edificio">
                                        Edificio: &nbsp;
                                        <asp:TextBox ID="txtEdificio" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="15"></asp:TextBox>
                                    </label>
                                </div>								
								
                                <div class="col-md-2">
                                    <label for="Piso">
                                        Piso: &nbsp;
                                        <asp:TextBox ID="txtPiso" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="15"></asp:TextBox>
                                    </label>
                                </div>
								
                                <div class="col-md-2">
                                    <label for="NumDepto">
                                        Num. Depto: &nbsp;
                                        <asp:TextBox ID="txtNDepto" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="5"></asp:TextBox>
                                    </label>
                                </div>
                            </div>
                             <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="Estado">
                                        Estado: &nbsp;
                                        <asp:DropDownList ID="ddlEstadoInmueble" runat="server" CssClass="BordeRadio10" DataSourceID="sdsEstadoInmueble" DataTextField="Estado" DataValueField="IdEstado" BackColor="Yellow" Width="200"></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsEstadoInmueble" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                            SelectCommand="sp_VTA_Estados_Inmuebles" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="Terraza">
                                        Terraza: &nbsp;
                                        <asp:TextBox ID="txtTerraza" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="5"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="M2Util">
                                        M2 Util: &nbsp;
                                        <asp:TextBox ID="txtM2Util" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="6"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="Logia">
                                        M2 Logia: &nbsp;
                                        <asp:TextBox ID="txtLogia" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                    </label>
                                </div>

                                <div class="col-md-2">
                                    <label for="PrecioLista">
                                        Precio de Lista: &nbsp;
                                        <asp:TextBox ID="txtPrecioLista" runat="server" Width="80px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="4" Enabled="False"></asp:TextBox>
                                    </label>
                                </div>

                            </div>
                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label for="Observacion">
                                        Observacion: &nbsp;
                                        <asp:TextBox ID="txtObservacion" runat="server" Width="410px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="50"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="Alicuota">
                                        Alicuota: &nbsp;
                                        <asp:TextBox ID="txtAlicuota" runat="server" Width="80px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="50"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="NumeroRol">
                                        Número Rol: &nbsp;
                                        <asp:TextBox ID="txtNumeroRol" runat="server" Width="80px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="10"></asp:TextBox>
                                    </label>
                                </div>
							</div>



<%-- 



                            <div class="row">
                                <div class="col-md-2">
                                    <label for="ModeloInmueble">
                                        Modelo Inmueble:
                                        <br />
                                        <asp:DropDownList ID="ddlModeloInmueble" runat="server" DataSourceID="sdsModeloInmueble" CssClass="BordeRadio10" BackColor="Yellow" DataValueField="IdModeloInmueble" DataTextField="Nombre">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsModeloInmueble" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                            SelectCommand="sp_VTA_ModeloInmueble" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="Piso">
                                        Piso:
                                        <br />
                                        <asp:TextBox ID="txtPiso" runat="server" Width="100px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="15"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-1">

                                    <label for="Edificio">
                                        Edificio:
                                        <br />
                                        <asp:TextBox ID="txtEdificio" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="15"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="Observacion">
                                        Observacion:
                                        <br />
                                        <asp:TextBox ID="txtObservacion" runat="server" Width="200px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="50"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="NumDepto">
                                        Num. Depto:
                                        <br />
                                        <asp:TextBox ID="txtNDepto" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="5"></asp:TextBox>
                                    </label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="Terraza">
                                        Terraza:
                                        <br />
                                        <asp:TextBox ID="txtTerraza" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="5"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <label for="M2Util">
                                        M2 Util:
                                        <br />
                                        <asp:TextBox ID="txtM2Util" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="6"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="Logia">
                                        M2 Logia:
                                        <br />
                                        <asp:TextBox ID="txtLogia" runat="server" Width="50px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                    </label>

                                </div>
                                <div class="col-md-2">
                                    <label for="Orientacion">
                                        Orientacion:
                                        <br />
                                        <asp:TextBox ID="txtOrientacion" runat="server" Width="80px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="50"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-1">
                                    <label for="PrecioLista">
                                        Precio de Lista:
                                        <br />
                                        <asp:TextBox ID="txtPrecioLista" runat="server" Width="80px" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="4"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="Estado Inmueble">
                                        Estado Inmueble:
                                        <br />
                                        <asp:DropDownList ID="ddlEstadoInmueble" runat="server" CssClass="BordeRadio10" DataSourceID="sdsEstadoInmueble" DataTextField="Estado" DataValueField="IdEstado" BackColor="Yellow" Width="200"></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsEstadoInmueble" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                            SelectCommand="sp_VTA_Estados_Inmuebles" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </label>
                                </div>
                            </div>
--%>

                        </div>
                    </fieldset>
                    <br />
                    <br />
                    <fieldset class="principal">
                        <legend>
                            <asp:Label ID="lblTituloAsociacion" runat="server" Text=" Asociación vía N° Rol" CssClass="principal"></asp:Label>
                        </legend>
                       <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="Tipo Inmueble">
                                        Tipo Inmueble: &nbsp;
                                        <asp:DropDownList ID="ddlTipoInmueble" runat="server" CssClass="BordeRadio10" DataSourceID="sdsTipoInmueble" DataTextField="Nombre" DataValueField="IdTipoInmueble"></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsTipoInmueble" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                            SelectCommand="sp_VTA_TipoInmueble" SelectCommandType="StoredProcedure"></asp:SqlDataSource>       
                                    </label>
                                </div>
                                <div class="col-md-2" style="text-align:right">
                                    <label for="Buscar">
                                        <asp:LinkButton ID="lnkBuscar" runat="server" CssClass="botoMaestra btn" OnClick="lnkBuscar_Click" OnClientClick="ValidaProyecto();">
                                            Buscar <span class="oi oi-magnifying-glass"></span>
                                        </asp:LinkButton>
                                    </label>
                                </div>
                            </div> 

                           <div class="row">
                               <br />
                               <div class="col-md-6">
                                    <asp:GridView ID="gvInmuebles" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                            DataKeyNames="IdInmueble"
                                            OnDataBound="gvInmuebles_DataBound" OnRowDataBound="gvInmuebles_RowDataBound"
                                            AllowPaging="True" AllowSorting="false" OnPageIndexChanging="gvInmuebles_PageIndexChanging" CssClass="grid_data" PageSize="5">
                                        <AlternatingRowStyle CssClass="grid_linea_alterna" />
                                        <Columns>
                                            <asp:BoundField DataField="IdInmueble" HeaderText="Id Inmueble" ReadOnly="True" SortExpression="IdInmueble" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ReadOnly="True" SortExpression="Descripcion" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="Edificio" HeaderText="Edificio" ReadOnly="True" SortExpression="Edificio" HeaderStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="modeloInmueble" HeaderText="Modelo" ReadOnly="True" SortExpression="modeloInmueble" HeaderStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="M2Terreno" HeaderText="Terraza" ReadOnly="True" SortExpression="M2Terreno" HeaderStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="M2" HeaderText="M2 Util" ReadOnly="True" SortExpression="M2" HeaderStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="Numero" HeaderText="Numero" ReadOnly="True" SortExpression="Numero" HeaderStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" SortExpression="Estado" HeaderStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="NumeroRol" HeaderText="Nro. Rol" ReadOnly="True" SortExpression="NumeroRol" HeaderStyle-Font-Underline="true" />
                                            <asp:TemplateField HeaderText="Asociar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAsociar" CssClass="btn grid_boton" runat="server" Text="Editar" Onclick="btnAsociar_Click" ForeColor="#ffffff" ToolTip="Asociar Inmueble"> <span class="iconify" data-icon="mdi:arrow-collapse" data-inline="false"></span>
                                                    </asp:LinkButton>
                                                    <asp:HiddenField ID="HiddenFieldDifferentUsers" Value='<%# Eval("IdInmueble") + "|"+ Eval("NumeroRol") %>' runat="server" />
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
                                    <br />
                                    <br />
                                </div>



                               <div class="col-md-6">
                                    <asp:GridView ID="gvAsociadoRol" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                            DataKeyNames="IdInmuebleA"
                                            OnDataBound="gvAsociadoRol_DataBound" OnRowDataBound="gvAsociadoRol_RowDataBound"
                                            AllowPaging="True" AllowSorting="false" OnPageIndexChanging="gvAsociadoRol_PageIndexChanging" CssClass="grid_data" PageSize="5">
                                        <AlternatingRowStyle CssClass="grid_linea_alterna" />
                                        <Columns>
                                            <asp:BoundField DataField="NumeroRolA" HeaderText="Rol" ReadOnly="True" SortExpression="NumeroRolA" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ReadOnly="True" SortExpression="Descripcion" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="Edificio" HeaderText="Edificio" ReadOnly="True" SortExpression="Edificio" HeaderStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="modeloInmueble" HeaderText="Modelo" ReadOnly="True" SortExpression="modeloInmueble" HeaderStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="M2Terreno" HeaderText="Terraza" ReadOnly="True" SortExpression="M2Terreno" HeaderStyle-Font-Underline="true" />
                                            <%--<asp:BoundField DataField="M2" HeaderText="M2 Util" ReadOnly="True" SortExpression="M2" HeaderStyle-Font-Underline="true" />--%>
                                            <%--<asp:BoundField DataField="Numero" HeaderText="Numero" ReadOnly="True" SortExpression="Numero" HeaderStyle-Font-Underline="true" />--%>
                                            <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" SortExpression="Estado" HeaderStyle-Font-Underline="true" />
                                            <asp:BoundField DataField="TipoInmueble" HeaderText="Tipo" ReadOnly="True" SortExpression="TipoInmueble" HeaderStyle-Font-Underline="true" />
                                            <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEliminarAsociacion" CssClass="btn grid_boton" runat="server" Text="Eliminar" OnClick="btnEliminarInmueble_Click" ForeColor="#ffffff" ToolTip="Eliminar Inmueble"> <i class="oi oi-trash"></i>
                                                    </asp:LinkButton>
                                                    <asp:HiddenField ID="HiddenFieldDifferentUsers" Value='<%# Eval("IdInmuebleA") + "|"+ Eval("IdInmuebleB") + "|"+ Eval("NumeroRolA") + "|"+Eval("NumeroRolB") %>' runat="server" />
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
                                    <br />
                                    <br />
                                </div>
                            </div>
                           <br />
                        <br />
                        </div>
               
                    </fieldset>
                </td>
            </tr>
        </table>
        <br />
        <br />

        <table class="principal">
            <tr>
                <td align="left">
                    <asp:LinkButton ID="lnkVolver" runat="server" CssClass="botoMaestra btn" OnClick="lnkVolver_Click">Volver</asp:LinkButton>
                </td>
                <td align="right">
                    <asp:LinkButton ID="lnkConfirmModificar" runat="server" CssClass="botoMaestra btn" Width="150" ToolTip="Modificar Inmueble" OnClick="lnkConfirmModificar_Click">Modificar Inmueble</asp:LinkButton>
                </td>
            </tr>
        </table>

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
        <!-- FIN - Alerta Información-->
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
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </h6>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="botoMaestra btn" data-dismiss="modal">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- FIN - Alerta Alert-->
    </form>
</body>
</html>