<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmInmuebles.aspx.cs" Inherits="MaestraNet.GC.SVTA.Mantenedor.frmInmuebles" %>

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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
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
        function isNumberKey(evt)
		{
         var charCode = (evt.which) ? evt.which : evt.keyCode;
         if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;    
         return true;
		 }
        function isNumberMasMenos(evt,texto)
            {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var n = texto.split("-").length-1;
                
            if (n > 0) {
                if (charCode == 45)
                    return false;
            }
            else if ((texto.length > 0) && (charCode==45))
                return false;


                if ((charCode > 47 && charCode < 58) || (charCode==45))
                    return true;    
                return false;
            }

    </script>
    <title>Mantenedor Inmueble</title>
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
                            <asp:Label runat="server" Text=" Buscar Inmueble" CssClass="principal"></asp:Label>
                        </legend>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="Proyecto">
                                        Proyecto: &nbsp;
                                         <asp:DropDownList ID="ddlProyecto" runat="server" CssClass="BordeRadio10" DataSourceID="sdsProyecto" DataTextField="Proyecto" DataValueField="IdProyecto" AutoPostBack="True" OnSelectedIndexChanged="ddlProyecto_SelectedIndexChanged"></asp:DropDownList>
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
                                <div class="col-md-4" style="text-align:right">
                                    <label for="Buscar">
                                        <asp:LinkButton ID="lnkBuscar" runat="server" CssClass="botoMaestra btn" OnClick="lnkBuscar_Click" OnClientClick="ValidaProyecto();">
                                            Buscar <span class="oi oi-magnifying-glass"></span>
                                        </asp:LinkButton>
                                    </label>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <!-- FIN Filtros -->
					<br />
                </td>
            </tr>
        </table>
        <br />
        <table class="principal">
            <tr>
                <td>
                    <div id="GrillaInmueble">

                        <fieldset class="principal">
                            <legend>
                                <asp:Label ID="lblLista" runat="server" Text=" Seleccione Inmueble" CssClass="principal"></asp:Label>
                            </legend>
                            <table width="100%">
                            <tr>
                                <td>
                                    <div>
                                        <span style="font-weight:bold;">
                                            <ul>
                                                <li>
                                                    Planilla excel debe tener las siguientes columnas: IDINMUEBLE, PISO, NDEPTO, EDIFICIO,	MODELO,	ORIENTACION, DEPTOUTIL,	BALCON,	LOGIA, PRECIOLISTA,	OBSERVACION, ESTADOINMUEBLE, IDPACK
                                                </li>
                                                <li>
                                                    Estados Inmuebles: 0 Disponible, 1 No Disponible, 2 Reservado, 3 Promesado, 4 Escriturado, 5 Entregado a Cliente
                                                </li>
                                                <li> 
                                                    Modelo Inmueble:  1: 1 DORM, 2: 2 DORM, 3: 3 DORM, 4: ESTACIONAMIENTO, 5: BODEGA,  8: TERRAZA, 9: 2 DORM 2B,  10: 3 DORM 2B, 11: BODEGA TIPO B, 12: 2 DORM 2 C
                                                </li>
                                            </ul>
                                        </span>
                                    </div>
                                </td>
                                <td align="right">
                                    <asp:LinkButton ID="btnNuevoInmueble" runat="server" CssClass  ="botoMaestra btn"  ToolTip="Crear Inmueble" OnClick="btnNuevoInmueble_Click">
                                        Nuevo Inmueble <span class="oi oi-home"></span>
                                    </asp:LinkButton>
                                </td>
                                <td width="30%" align="right">
                                     <asp:FileUpload ID="FileUpload1" runat="server" />
                                </td>
                                <td align="right">
                                        <asp:LinkButton ID="btnCargaInmuebles" runat="server" CssClass  ="botoMaestra btn"  ToolTip="Carga Inmueble" OnClick="btnCargaInmuebles_Click">
                                        Carga Inmuebles <span class="oi oi-home"></span>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <br />
                            <asp:GridView ID="gvInmuebles" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                    OnSorting="gvInmuebles_Sorting" OnDataBound="gvInmuebles_DataBound" OnRowDataBound="gvInmuebles_RowDataBound"
                                    AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvInmuebles_PageIndexChanging" CssClass="grid_data" PageSize="10">
                                <AlternatingRowStyle CssClass="grid_linea_alterna" />
                                <Columns>
                                    <asp:BoundField DataField="IdInmueble" HeaderText="Id Inmueble" ReadOnly="True" SortExpression="IdInmueble" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ReadOnly="True" SortExpression="Descripcion" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Edificio" HeaderText="Edificio" ReadOnly="True" SortExpression="Edificio" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="modeloInmueble" HeaderText="Modelo" ReadOnly="True" SortExpression="modeloInmueble" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="M2Terreno" HeaderText="Terraza" ReadOnly="True" SortExpression="M2Terreno" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="M2" HeaderText="M2 Util" ReadOnly="True" SortExpression="M2" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Numero" HeaderText="Numero" ReadOnly="True" SortExpression="Numero" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Orientacion" HeaderText="Orientacion" ReadOnly="True" SortExpression="Orientacion" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Piso" HeaderText="Piso" ReadOnly="True" SortExpression="Piso" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="PrecioLista" HeaderText="Precio Lista" ReadOnly="True" SortExpression="PrecioLista" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="EstadoInmueble" HeaderText="Estado" ReadOnly="True" SortExpression="EstadoInmueble" HeaderStyle-Font-Underline="true" />
                                    <asp:TemplateField HeaderText="Inmueble Pack">
													<ItemTemplate>
														<asp:TextBox ID="txtInmueblePack" CssClass="BordeRadio10" runat="server"  Width="90px" Height="25px" Text='<%# Bind("IdInmueble_Pack") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
													</ItemTemplate>
												</asp:TemplateField>
                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAsociar" CssClass="btn grid_boton" runat="server" Text="Editar" Onclick="btnAsociar_Click" ForeColor="#ffffff" ToolTip="Asociar Inmueble"> <span class="iconify" data-icon="mdi:arrow-collapse" data-inline="false"></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnEditarInmueble" CssClass="btn grid_boton" runat="server" Text="Editar" OnClick="btnEditarInmueble_Click" ForeColor="#ffffff" ToolTip="Modificar Inmueble"> <i class="oi oi-pencil"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnEliminarInmueble" CssClass="btn grid_boton" runat="server" Text="Eliminar" OnClientClick="return window.confirm('Seguro de eliminar el Inmueble?')" OnClick="btnEliminarInmueble_Click" ForeColor="#ffffff" ToolTip="Eliminar Inmueble"> <i class="oi oi-trash"></i>
                                            </asp:LinkButton>
                                            <asp:HiddenField ID="HiddenFieldDifferentUsers" Value='<%# Eval("IdInmueble") %>' runat="server" />
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
                            <br /><br />
                        </fieldset>

                    </div>
                    <br />
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
    </form>
</body>
</html>
