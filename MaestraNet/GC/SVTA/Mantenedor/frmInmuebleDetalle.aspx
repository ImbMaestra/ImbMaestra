<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmInmuebleDetalle.aspx.cs" Inherits="MaestraNet.GC.SVTA.Mantenedor.frmInmuebleDetalle" %>

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
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCSuOjgm4Ivz6-lSMx8FKB7gkchlCuypm0"></script>
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
                                        <asp:TextBox ID="txtOrientacion" runat="server" Width="80px" ReadOnly="true" CssClass="BordeRadio10" BackColor="Yellow" MaxLength="50"></asp:TextBox>
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
