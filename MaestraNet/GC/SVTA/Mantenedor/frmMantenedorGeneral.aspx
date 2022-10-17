<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMantenedorGeneral.aspx.cs" Inherits="MaestraNet.GC.SVTA.Mantenedor.frmMantenedorGeneral" %>

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
    <title>Mantenedor General</title>
    <script language="Javascript">
        function showAlertaInformar() {
            $('#modalAlertaInformar').modal('show');
        }
        function showAlertaError() {
            $('#modalAlertaError').modal('show');
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
         <br />
        <br />
        <table class="principal">
            <tr>
                <td>

                    <!-- Buscar Cliente -->

                    <fieldset class="principal">
                        <legend>
                            <asp:Label runat="server" Text="Texto variable" CssClass="principal"></asp:Label>
                        </legend>

                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-3">
                                    <%--<label for="Region"> Región: &nbsp;</label>
                                        <asp:DropDownList ID="ddpRegion" runat="server" CssClass="BordeRadio10" AutoPostBack="True"></asp:DropDownList>--%>
                                    
                                </div>
                                <div class="col-md-3">
                                     <%--<label for="Comuna">
                                        Comuna: &nbsp;
                                        <asp:DropDownList ID="ddpComuna" runat="server" CssClass="BordeRadio10" ></asp:DropDownList>
                                    </label>--%>
                                </div>
                                <div class="col-md-4">
                                     <%--<label for="EstadoEntrega">
                                        Estado Entrega: &nbsp;
                                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="BordeRadio10" ></asp:DropDownList>
                                    </label>--%>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <%-- <label for="Proyecto">
                                        Proyecto: &nbsp;
                                        <asp:TextBox ID="txtProyecto" runat="server" CssClass="BordeRadio10" Width="150px"></asp:TextBox>
                                    </label>    --%>                                
                                </div>

                                <div class="col-md-8" style="text-align: right">
                                   <%-- <asp:LinkButton ID="lnkBuscar" runat="server" CssClass="botoMaestra btn" OnClick="lnkBuscar_Click">
                                        Buscar <span class="oi oi-magnifying-glass"></span>
                                    </asp:LinkButton>--%>
                                </div>
                            </div>
                        </div>
                        <br />
                    </fieldset>
                </td>
            </tr>
        </table>

        <br /><br />

        <table class="principal">
            <tr>
                <td>
                    <div id="GrillaClientes">

                        <fieldset class="principal">
                            <legend>
                                <asp:Label ID="lblLista" runat="server" Text="Texto variable" CssClass="principal"></asp:Label>
                            </legend>
                            <table width="100%">
                            <tr>
                                <td width="80%">
                                    
                                </td>
                                <td align="right">
                                    <%--<asp:LinkButton ID="btnNuevoProyecto" runat="server" CssClass  ="botoMaestra btn"  ToolTip="Crear Proyecto" OnClick="btnNuevoProyecto_Click">
                                        Nuevo Proyecto <span class="oi oi-home"></span>
                                    </asp:LinkButton>--%>
                                </td>
                            </tr>
                        </table>
                        <br />
                            <%--<asp:GridView ID="gvProyectos" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                    OnSorting="gvProyectos_Sorting" OnDataBound="gvProyectos_DataBound" OnRowDataBound="gvProyectos_RowDataBound"
                                    AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvProyectos_PageIndexChanging" CssClass="grid_data" PageSize="10">
                                <AlternatingRowStyle CssClass="grid_linea_alterna" />
                                <Columns>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre Proyecto" ReadOnly="True" SortExpression="nombre" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Region" HeaderText="Region" ReadOnly="True" SortExpression="Region" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Comuna" HeaderText="Comuna" ReadOnly="True" SortExpression="Comuna" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" ReadOnly="True" SortExpression="Direccion" HeaderStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="M2" HeaderText="M2" ReadOnly="True" SortExpression="M2" HeaderStyle-Font-Underline="true" />
                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditarCliente" CssClass="btn grid_boton" runat="server" Text="Editar" OnClick="btnEditarCliente_Click" ForeColor="#ffffff"> <i class="oi oi-pencil"></i>
                                            </asp:LinkButton>
                                            <asp:HiddenField ID="HiddenFieldDifferentUsers" Value='<%# Eval("IdProyecto") %>' runat="server" />
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
                            </asp:GridView>--%>
                            <br /><br />
                        </fieldset>

                    </div>
                    <br />
                </td>
            </tr>
        </table>
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
        <!-- FIN - Alerta Información-->
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
    </form>
</body>
</html>
