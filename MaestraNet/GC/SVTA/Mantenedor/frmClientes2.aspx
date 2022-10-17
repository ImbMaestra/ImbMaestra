<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmClientes2.aspx.cs" Inherits="MaestraNet.GC.SVTA.Mantenedor.frmClientes2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>



    <link href="../../../open-iconic/font/css/open-iconic-bootstrap.css" rel="stylesheet" />
    <script src="../../../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../../../Scripts/bootstrap.min.js"></script>
    <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../sistema_venta.css" rel="stylesheet" />

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

    </script>

</head>
<body>
    <form id="form1" runat="server">


                    <br /><br />

            <div style="display:none">

                <asp:Label ID="lblIdCliente" runat="server" Text="0"></asp:Label>
                <asp:Label ID="lblRut" runat="server" Text="0"></asp:Label>
                <asp:Label ID="lblNombre" runat="server" Text="0"></asp:Label>
                <asp:Label ID="lblAPaterno" runat="server" Text="0"></asp:Label>
                <asp:Label ID="lblAMaterno" runat="server" Text="0"></asp:Label>

            </div>

        <table class="principal">
            <tr>
                <td>
                    <!-- Filtros -->
                    <fieldset class="principal">
                        <legend>
                            <asp:Label runat="server" Text=" Buscar Cliente" CssClass="principal"></asp:Label>
                        </legend>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md">
                                    <label for="Rut">
                                        Rut: &nbsp;
                                        <asp:TextBox ID="txtRutBuscar" runat="server" Width="80px" onkeypress="return isNumberKey(event)" CssClass="BordeRadio10"></asp:TextBox>
                                        -
						                <asp:TextBox ID="txtdvBuscar" runat="server" MaxLength="1" Width="20px" CssClass="BordeRadio10"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md">
                                    <label for="Nombre">
                                        Nombre: &nbsp;
                                        <asp:TextBox ID="txtNombreBuscar" runat="server" Width="100px" CssClass="BordeRadio10"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md">
                                    <label for="ApellidoPaterno">
                                        Apellido Paterno: &nbsp;
                                        <asp:TextBox ID="txtPaternoBuscar" runat="server" CssClass="BordeRadio10" Width="100px"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md">
                                    <label for="ApellidoMaterno">
                                        Apellido Materno: &nbsp;
                                        <asp:TextBox ID="txtAMaternoBuscar" runat="server" CssClass="BordeRadio10" Width="100px"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md" style="text-align:right">
                                    <label for="Buscar">
                                        <asp:LinkButton ID="lnkBuscar" runat="server" CssClass="botoMaestra btn" OnClick="lnkBuscar_Click">
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
                    <div id="GrillaClientes">
				        <fieldset class="principal">
				            <legend>
                                <asp:Label ID="lblLista" runat="server" Text=" Seleccione Cliente" CssClass="principal"></asp:Label>
				            </legend>

                             <table width="100%">
                                <tr>
                                    <td width="80%">
                                    
                                    </td>
                                    <td align="right">
                                     <%--<asp:LinkButton ID="btnNuevoCliente" runat="server" CssClass  ="botoMaestra btn"  ToolTip="Nuevo Cliente" OnClick="btnNuevoCliente_Click">
                                        Nuevo Cliente <span class="oi oi-home"></span>
                                        </asp:LinkButton>--%>
                                    </td>
                            </tr>
                            </table>
                            <br />
                            
                            <asp:GridView ID="grdCliente" runat="server" AutoGenerateColumns="False" DataSourceID="sqlCliente2" AllowPaging="True" Width="1021px"
                                OnRowCommand="grdCliente_RowCommand"
                                
                                >
                                <AlternatingRowStyle CssClass="grid_linea_alterna" />
                                <Columns>
                                    <asp:BoundField DataField="EntConNombres" HeaderText="Nombre" ReadOnly="True" SortExpression="EntConNombres" HeaderStyle-Font-Underline="true"/>
                                    <asp:BoundField DataField="EntConApePaterno" HeaderText="Apellido Paterno" ReadOnly="True" SortExpression="EntConApePaterno" HeaderStyle-Font-Underline="true" ControlStyle-Font-Underline="true" />
                                    <asp:BoundField DataField="EntConApeMaterno" HeaderText="Apellido Materno" ReadOnly="True" SortExpression="EntConApeMaterno" HeaderStyle-Font-Underline="true"/>
                                    <asp:BoundField DataField="EntRut" HeaderText="Rut" ReadOnly="True" SortExpression="EntRut" HeaderStyle-Font-Underline="true"/>
                                    <asp:BoundField DataField="EntConMail" HeaderText="Email" ReadOnly="True" SortExpression="EntConMail" HeaderStyle-Font-Underline="true"/>
                                    <asp:BoundField DataField="Inversionista" HeaderText="idInversionista" ReadOnly="True" SortExpression="Inversionista" Visible="False" />
                                    <asp:BoundField DataField="DescInversionista" HeaderText="Inversionista" ReadOnly="True" SortExpression="DescInversionista" Visible="False" />
                                    <asp:BoundField DataField="IdNacionalidad" HeaderText="IdNacionalidad" ReadOnly="True" SortExpression="IdNacionalidad" Visible="False" />
                                    <asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad" ReadOnly="True" SortExpression="Nacionalidad" Visible="False" />
                                    <asp:BoundField DataField="IdEstadoCivil" HeaderText="IdEstadoCivil" ReadOnly="True" SortExpression="IdEstadoCivil" Visible="False" />
                                    <asp:BoundField DataField="EstadoCivil" HeaderText="Estado Civil" ReadOnly="True" SortExpression="EstadoCivil" Visible="False" />
                                    <asp:BoundField DataField="sexo" HeaderText="idsexo" SortExpression="sexo" Visible="False" />
                                    <asp:BoundField DataField="DescSexo" HeaderText="Sexo" ReadOnly="True" SortExpression="DescSexo" Visible="False" />
                                    <asp:BoundField DataField="PersonaJuridica" HeaderText="idPersonaJuridica" ReadOnly="True" SortExpression="PersonaJuridica" Visible="False" />
                                    <asp:BoundField DataField="DescPersonaJuridica" HeaderText="Persona Juridica" ReadOnly="True" SortExpression="DescPersonaJuridica" Visible="False" />
                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditarCliente" CssClass="btn grid_boton"  runat="server" Text="Editar"
                                                 CommandArgument   ='<%# Eval("IdCliente") %>'
                                                 CommandName="Edita" 
                                                 ForeColor="#ffffff">
                                                <i class="oi oi-pencil"></i>
                                            </asp:LinkButton>
                                            <asp:HiddenField ID="HiddenFieldDifferentUsers" Value='<%# Eval("IdCliente") %>' runat="server" />
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
                                <SortedDescendingCellStyle  CssClass="grid_selected_row_des" />
                                <SortedDescendingHeaderStyle CssClass="grid_selected_header_des" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="sqlCliente2" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" SelectCommand="sp_VTA_ListarClientes" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="lblRut" Name="Rut" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="lblNombre" Name="Nombre" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="lblAPaterno" Name="Paterno" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="lblAMaterno" Name="Materno" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>


                            <br />
                            <br />
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
