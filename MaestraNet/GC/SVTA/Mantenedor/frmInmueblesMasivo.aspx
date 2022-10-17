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
        function showAlertaInformar() {
            $('#modalAlertaInformar').modal('show');
        }

        function showAlertaError() {
            $('#modalAlertaError').modal('show');
        }

        function showAlertaAlert() {
            $('#modalAlertaAlert').modal('show');
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
                            <br />  
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="Terraza"> <!--M2Terreno-->
                                        Cantidad a modificar:
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCantidad" style="text-align:right" runat="server" Width="215px" CssClass="BordeRadio10" enabled="false"></asp:TextBox>
                                </div>
                                
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="Terraza"> <!--M2Terreno-->
                                        Proyecto:
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtProyecto" style="text-align:right" runat="server" Width="215px" CssClass="BordeRadio10" enabled="false"></asp:TextBox>
                                </div>
                                
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="Terraza"> <!--M2Terreno-->
                                        M2 Terraza:
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtM2Terraza" style="text-align:right" runat="server" Width="215px" MaxLength="8" CssClass="BordeRadio10" onkeypress="return filterFloat(event,this);" autocomplete="off"></asp:TextBox>
                                </div>
                                
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="M2 Util"> <!--M2-->
                                        M2 Util:
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtMetroUtil" style="text-align:right" runat="server" Width="215px" MaxLength="8" CssClass="BordeRadio10" onkeypress="return filterFloat(event,this);" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                                                        <div class="row">
                                <div class="col-md-2">
                                    <label for="Precio Lista">  <!-- -->
                                        Estado Inmueble:
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlEstadoInmueble" runat="server" CssClass="BordeRadio10" DataSourceID="sdsEstadoInmueble" DataTextField="Estado" DataValueField="IdEstado" AutoPostBack="True" OnSelectedIndexChanged="ddlEstadoInmueble_SelectedIndexChanged"></asp:DropDownList>
                                        &nbsp;<asp:SqlDataSource ID="sdsEstadoInmueble" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                            SelectCommand="sp_VTA_Estados_Inmuebles" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="Justificacion"> <!--Justificacion-->
                                        Justificacion:
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtJustificacion" style="text-align:left" runat="server" Width="215px" MaxLength="50" CssClass="BordeRadio10" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>


                            <br />

                            <div class="row">
                                 <div class="col-md-2"> 
                                    
                                </div>
                                <div class="col-md-3">
                                    <asp:RadioButton ID="RdoPrecioLista1" runat="server" GroupName="PrecioLista" Text="Fijar" />
                                   &nbsp;
                                    <asp:RadioButton ID="RdoPrecioLista2" runat="server" GroupName="PrecioLista" Text="Aumento %" />
                                   &nbsp;
                                    <asp:RadioButton ID="RdoPrecioLista3" runat="server" GroupName="PrecioLista" Text="Aumento UF" />
                                </div>
                            </div>

                            <div class="row">
                                 <div class="col-md-2"> 
                                     <label for="Precio Lista">  <!--PrecioLista-->
                                        Precio Lista:
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    &nbsp;&nbsp;
                                    <asp:RadioButton ID="RdoPrecioLista4" runat="server" GroupName="PrecioLista" Text="Disminuye %" />
                                   &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="RdoPrecioLista5" runat="server" GroupName="PrecioLista" Text="Disminuye UF" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <asp:hiddenfield id="hddTipoPrecioLista" value="0" runat="server"/>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPrecioLista" style="text-align:right" runat="server" Width="215px" MaxLength="4" CssClass="BordeRadio10" onkeypress="return isNumberKey(event)" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                 <div class="col-md-2">
                                    <label for="Alicuota">  <!--Alicuota-->
                                        Alicuota:
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtAlicuota" style="text-align:right" runat="server" Width="215px" MaxLength="50" CssClass="BordeRadio10" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                 <div class="col-md-2">
                                    <label for="Numero Rol">  <!--NumeroRol-->
                                        Numero Rol:
                                    </label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtNumeroRol" style="text-align:right" runat="server" Width="215px" MaxLength="10" CssClass="BordeRadio10" autocomplete="off"></asp:TextBox>
                                </div>
                               
                            </div>
                            <br />
                            <div class="row">
                                <span style="font-weight:bold;">
                                    <ul>
                                        <li>
                                            Debe ingresar al menos un valor dentro de los campos para poder grabar.
                                        </li>
                                    </ul>
                                </span>
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
                <td align="left">
                    <asp:LinkButton ID="lnkVolver" runat="server" CssClass="botoMaestra btn" OnClick="lnkVolver_Click">Volver</asp:LinkButton>
                </td>
                <td align="right">
                    <asp:LinkButton ID="lnkConfirmModificar" runat="server" CssClass="botoMaestra btn" Width="150" ToolTip="Modificar Inmueble" OnClientClick="return window.confirm('Seguro que desea modificar los registros?')" OnClick="lnkConfirmModificar_Click">Modificar Inmueble</asp:LinkButton>
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
    </form>
</body>
</html>
