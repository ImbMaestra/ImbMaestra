<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCargaMasiva.aspx.cs" Inherits="MaestraNet.GC.SVTA.Mantenedor.frmCargaMasiva" %>

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

    </script>

    <title>Carga Masiva</title>
</head>
<body>
    <form id="form1" runat="server">
        <br /><br />
            <table class="principal">
            <tr>
                <td>
                    <!-- Filtros -->
                    <fieldset class="principal">
                        <legend>
                            <asp:Label runat="server" Text="Descarga de Datos" CssClass="principal"></asp:Label>
                        </legend>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="Proyecto">
                                        Proyecto: &nbsp;
                                            <asp:DropDownList ID="ddlProyecto" runat="server" CssClass="BordeRadio10" DataSourceID="sdsProyecto" DataTextField="Proyecto" DataValueField="IdProyecto" AutoPostBack="True" OnSelectedIndexChanged="ddlProyecto_SelectedIndexChanged" ></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsProyecto" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                            SelectCommand="sp_VTA_Combo_Proyecto" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </label>
                                </div>
                                <div class="col-md-4">
                                    <label for="Torre">
                                                Torre: &nbsp;
                                        <asp:DropDownList ID="ddlTorre" runat="server" CssClass="BordeRadio10" ></asp:DropDownList>
                                            </label>
                                </div>
                                <div class="col-md-4">
                                    <label for="Tipo Inmueble">
                                        Tipo Inmueble: &nbsp;
                                        <asp:DropDownList ID="ddlTipoInmueble" runat="server" CssClass="BordeRadio10" DataSourceID="sdsTipoInmueble" DataTextField="Nombre" DataValueField="IdTipoInmueble"></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsTipoInmueble" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                            SelectCommand="sp_VTA_TipoInmueble" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </label>
                                </div>
                               
                            </div>
                            <div class="row">
                                 <div class="col-md-4">
                                    <label for="Modelo Inmueble">
                                        Modelo Inmueble: &nbsp;
                                        <asp:DropDownList ID="ddlModeloInmueble" runat="server" CssClass="BordeRadio10" DataSourceID="sdsModeloInmueble" DataTextField="Nombre" DataValueField="IdModeloInmueble"></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsModeloInmueble" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                            SelectCommand="sp_VTA_ModeloInmueble" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        
                                    </label>
                                </div>
                                <div class="col-md-4">
                                            <label for="Depto">
                                                Depto:
                                                <asp:TextBox ID="txtDepto" runat="server" Width="50px" MaxLength="5" CssClass="BordeRadio10"></asp:TextBox>
                                            </label>
                                </div>
                                 <div class="col-md-4">
                                            <label for="Piso">
                                                Piso:
                                                <asp:TextBox ID="txtPiso" runat="server" Width="50px" MaxLength="5" CssClass="BordeRadio10" onkeypress="return isNumberMasMenos(event,this.value)"></asp:TextBox>
                                            </label>
                                </div>

                                <div class="col-md-4">
                                            <label for="Orientacion">
                                                Orientacion: &nbsp;
                                                <asp:DropDownList ID="ddlOrientacion" runat="server" CssClass="BordeRadio10" DataSourceID="sdsOrientacion" DataTextField="Orientacion" DataValueField="IdOrientacion"></asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsOrientacion" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                                    SelectCommand="sp_VTA_Orientacion" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                            </label>
                                </div>
                                <div class="col-md-4">
                                            <label for="Estado">
                                                Estado: &nbsp;
                                                <asp:DropDownList ID="ddlEstadoInmueble" runat="server" CssClass="BordeRadio10" DataSourceID="sdsEstadoInmueble" DataTextField="Estado" DataValueField="IdEstado"></asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsEstadoInmueble" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                                    SelectCommand="sp_VTA_Estados_Inmuebles" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                            </label>
                                </div>

                                <div class="col-md-2" style="text-align:right">
                                    <asp:LinkButton ID="btnDescargaInmuebles" runat="server" CssClass="botoMaestra btn"  ToolTip="Descarga Inmueble" OnClick="btnDescargaInmuebles_Click">
                                        Descarga de Inmuebles <span class="oi oi-cloud-download"></span>
                                    </asp:LinkButton>
                                </div>

                                <div class="col-md-2" style="text-align:right">
                                    <asp:LinkButton ID="btnDescargaFormatoArchivo" runat="server" CssClass="botoMaestra btn"  ToolTip="Descarga Inmueble" OnClick="btnDescargaFormatoArchivo_Click">
                                        Descarga formato archivo <span class="oi oi-data-transfer-download"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <br />

                        </div>
                    </fieldset>
                    <!-- FIN Filtros -->   
                </td>
            </tr>
            <tr>
                <td>
                    <br /><br />
                    <fieldset class="principal">
                        <legend>
                            <asp:Label runat="server" Text="Carga de Datos" CssClass="principal"></asp:Label>
                        </legend>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-6">
                                    <span style="font-weight:bold;">
                                            <ul>
                                                <li>
                                                    Debe seleccionar un Proyecto de la lista y luego seleccionar un archivo para poder cargar datos.
                                                </li>
                                            </ul>
                                        </span>
                                </div>
                                <div class="col-md-6" style="text-align:right">
                                    <asp:FileUpload ID="FileUpload1" runat="server" accept="application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" /> 
                                    
                                    <asp:LinkButton ID="btnCargaInmuebles" runat="server" CssClass="botoMaestra btn"  ToolTip="Carga Inmueble" OnClick="btnCargaInmuebles_Click">
                                        Visualizar datos <span class="oi oi-camera-slr"></span>
                                    </asp:LinkButton>
                                </div>
  
                            </div>
                            <br />
                        </div>  
                        
                         <div id="GrillaInmueble">

                        <br />
                            <asp:GridView ID="gvInmuebles" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                    DataKeyNames="IdInmueble" OnSorting="gvInmuebles_Sorting" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvDatosActuales_PageIndexChanging" CssClass="grid_data" PageSize="20">
                                <AlternatingRowStyle CssClass="grid_linea_alterna" />
                                <Columns>
                                    <asp:BoundField DataField="IDINMUEBLE" HeaderText="Id Inmueble" ReadOnly="True" SortExpression="IDINMUEBLE" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="PISO" HeaderText="Piso" ReadOnly="True" SortExpression="PISO" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="NDEPTO" HeaderText="Número" ReadOnly="True" SortExpression="NDEPTO" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="EDIFICIO" HeaderText="Edificio" ReadOnly="True" SortExpression="EDIFICIO" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="MODELO" HeaderText="Modelo" ReadOnly="True" SortExpression="MODELO" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="ORIENTACION" HeaderText="Orientación" ReadOnly="True" SortExpression="ORIENTACION" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" ReadOnly="True" SortExpression="ESTADO" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="DEPTOUTIL" HeaderText="M2" ReadOnly="True" SortExpression="DEPTOUTIL" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="BALCON" HeaderText="Terraza" ReadOnly="True" SortExpression="BALCON" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="LOGIA" HeaderText="Logia" ReadOnly="True" SortExpression="LOGIA" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="PRECIOLISTA" HeaderText="Precio Lista" ReadOnly="True" SortExpression="PRECIOLISTA" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="OBSERVACION" HeaderText="Observación" ReadOnly="True" SortExpression="OBSERVACION" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" ReadOnly="True" SortExpression="DESCRIPCION" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="IDPACK" HeaderText="IdPack" ReadOnly="True" SortExpression="IDPACK" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="USOGOCE" HeaderText="UsoGoce" ReadOnly="True" SortExpression="USOGOCE" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="USUARIOEDICION" HeaderText="Usuario" ReadOnly="True" SortExpression="USUARIOEDICION" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="ALICUOTA" HeaderText="Alicuota" ReadOnly="True" SortExpression="ALICUOTA" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="NUMEROROL" HeaderText="N° Rol" ReadOnly="True" SortExpression="NUMEROROL" HeaderStyle-Font-Underline="true" />
                                
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
                       <%-- </fieldset>--%>

                    </div>


                    </fieldset>
                </td>
            </tr>
        </table>
        <br />

                  
        <table class="principal" border="0">
            <tr>
                <td>
                    <div class="container-fluid">
                       <div class="row" >
                            <div class="col-md-12" style="text-align:right">
                                <asp:LinkButton ID="btnSubirDatosArchivo" runat="server" CssClass="botoMaestra btn"  ToolTip="Carga Inmueble" OnClick="btnSubirDatosArchivo_Click">
                                        Subir datos <span class="oi oi-cloud-upload"></span>
                                </asp:LinkButton>
                            </div>
                           <%--<div class="col-md-1" >
                            </div>--%>
                        </div>
                    </div>
                </td>
                <td>
                    &nbsp;
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
