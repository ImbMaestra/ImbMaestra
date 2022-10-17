<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPerfilBoton.aspx.cs" Inherits="MaestraNet.GC.SVTA.Mantenedor.frmPerfilBoton" %>

<!DOCTYPE html>
        
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    
    <link href="../sistema_venta.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>

   
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">  
        $(function () {
            $('[id*=lbControles]').multiselect({
                includeSelectAllOption: true
            });
        });
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
    <title>Perfilamiento paginas</title>
</head>
<body>
    <form id="frmPerfilBoton" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <br />
        <br />
        <table class="principal">
            <tr>
                <td>
                    
                            <div class="btn-group" role="group" aria-label="Basic example">
                                    <asp:LinkButton ID="lbPerfilPagina" runat="server" class="btn btn-secondary active border border-dark" OnClick="lbPerfilPagina_Click">Perfil Paginas</asp:LinkButton>
                                    <asp:LinkButton ID="lbPerfilUsuario" runat="server" class="btn btn-secondary border border-dark" OnClick="lbPerfilUsuario_Click">Perfiles por Usuario</asp:LinkButton>
                           </div> 
                    <asp:UpdatePanel ID="pnCambiaPrecio" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                    <div  runat="server" id="dvPaginas">
                    <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-3">
                                     <label for="Perfiles">
                                            Perfiles: <br />                                         
                                        <asp:DropDownList ID="ddlPerfiles" runat="server" CssClass="BordeRadio10" DataSourceID="SqlDataSource1"    
                                            DataTextField="descripcion" DataValueField="id_rol" BackColor="Yellow" AutoPostBack="True" OnSelectedIndexChanged="ddlPerfiles_SelectedIndexChanged">
                                        </asp:DropDownList>
                                         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" 
									            SelectCommand="SP_VTA_PerfilesVenta" SelectCommandType="StoredProcedure">
                                        </asp:SqlDataSource>
                                     </label>
                                    
                                </div>
                                <div class="col-md-2">
                                     <label for="Paginas">
                                            Paginas: <br />                                         
                                        <asp:DropDownList ID="ddlPaginas" runat="server" CssClass="BordeRadio10" DataSourceID="SqlDataSource2"    
                                            DataTextField="idPagina" DataValueField="ruta" BackColor="Yellow" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlPaginas_SelectedIndexChanged1" >
                                        </asp:DropDownList>
                                         <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" 
									            SelectCommand="sp_vta_ListaPaginas" SelectCommandType="StoredProcedure">
                                     <SelectParameters>
										            <asp:Parameter Name="idSistema"  Type="String" DefaultValue="SVT" />
									            </SelectParameters>
                                        </asp:SqlDataSource>
                                     </label>
                                    
                                </div>
                                <div class="col-md-2">
                                    <label for="Controles">
                                            Controles: <br />
                                     </label>
                                    <asp:ListBox ID="lbControles" runat="server" SelectionMode="Multiple"  Width="100px" CssClass="BordeRadio10"></asp:ListBox>
                                </div>
                           </div>
                        <div class="row">
                                <div class="col-md-2">
                                    <asp:LinkButton ID="lnkVolver" runat="server" CssClass="botoMaestra btn" OnClick="lnkVolver_Click">Volver</asp:LinkButton>
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton ID="lnkModificarAcceso" runat="server" CssClass="botoMaestra btn" Width="150" ToolTip="Modificar Accesos" OnClick="lnkModificarAcceso_Click">Modificar</asp:LinkButton>
                                </div>
                        </div>

                        </div>
                    </div>
                            </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlPaginas" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div runat="server" id="dvUsuarios">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-3">
                                     <label for="Perfiles">
                                            Perfiles: <br />                                         
                                        <asp:DropDownList ID="ddlPerfilUsu" runat="server" CssClass="BordeRadio10" DataSourceID="SqlDataSource1"    
                                            DataTextField="descripcion" DataValueField="id_rol" BackColor="Yellow" AutoPostBack="True" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0" Text ="Todos"/>
                                        </asp:DropDownList>
                                     </label>
                                    
                                </div>
                                <div class="col-md-2">
                                     <label for="usuario">
                                            Usuario: <br />                                         
                                         <asp:TextBox ID="txtusuario" runat="server" CssClass="BordeRadio10"></asp:TextBox>
                                     </label>
                                    
                                </div>
                                <div class="col-md-2">
                                    <label for="Buscar">
                                        <asp:LinkButton ID="lnkBuscar" runat="server" CssClass="botoMaestra btn" OnClick="lnkBuscar_Click">
                                            Buscar <span class="oi oi-magnifying-glass"></span>
                                        </asp:LinkButton>
                                    </label>
                                    
                                </div>
                           </div>
                        <div class="row">
                                <div class="col-md-6">
                                    <asp:GridView ID="gvPerfil" runat="server" CellPadding="4" AutoGenerateColumns="False" AllowPaging="True" 
                                        AllowSorting="True"  CssClass="grid_data" PageSize="10" OnPageIndexChanging="gvPerfil_PageIndexChanging">
                                        <AlternatingRowStyle CssClass="grid_linea_alterna" />
                                        <Columns>
                                            <asp:BoundField DataField="ID_ROL" HeaderText="Id Rol" ReadOnly="True" SortExpression="ID_ROL" HeaderStyle-Font-Underline="true"/>
                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Nombre Rol" ReadOnly="True" SortExpression="DESCRIPCION" HeaderStyle-Font-Underline="true"/>
                                            <asp:BoundField DataField="ID_USUARIO" HeaderText="Id Usuario" ReadOnly="True" SortExpression="ID_USUARIO" HeaderStyle-Font-Underline="true"/>
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre Usuario" ReadOnly="True" SortExpression="NOMBRE" HeaderStyle-Font-Underline="true"/>
                                        </Columns>
                                        <FooterStyle CssClass="grid_footer" />
                                        <HeaderStyle CssClass="grid_header" />
                                        <PagerStyle CssClass="grid_pager" HorizontalAlign="Center" />
                                        <RowStyle CssClass="grid_row" />
                                        <SelectedRowStyle CssClass="grid_selected_row" />
                                        <SortedAscendingCellStyle CssClass="grid_selected_row_asc" />
                                        <SortedAscendingHeaderStyle CssClass="grid_selected_header_asc" />
                                        <SortedDescendingCellStyle  CssClass="grid_selected_row_des" />
                                        <SortedDescendingHeaderStyle CssClass="grid_selected_header_des" />
                                    </asp:GridView>
                                </div>
                        </div>

                        </div>
                    </div>
                                   <br />
                    

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
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
                         
    </form>
</body>
</html>
