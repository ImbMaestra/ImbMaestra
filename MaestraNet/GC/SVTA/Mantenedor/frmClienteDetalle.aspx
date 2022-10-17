<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmClienteDetalle.aspx.cs" Inherits="MaestraNet.GC.SVTA.Mantenedor.frmClienteDetalle" %>

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
        </script>
        <title>Mantenedor Cliente</title>
    </head>
    <body>
        <form id="frmClienteDetalle" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <br /><br />
		    <table class="principal">
                <tr>
                    <td>
                        <fieldset class="principal">
                            <legend>
                                <asp:Label ID="lblTituloPromesa" runat="server" Text=" Datos de Cliente" CssClass="principal"></asp:Label>
                            </legend>
                            <div class="container-fluid">
                                <div class="row">
		                            <div style="display: none">
			                            <asp:Label ID="lblIdProyecto" runat="server" Text=""></asp:Label>
			                            <asp:Label ID="lblIdCotizacion" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblTipoOcupacion" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblIdCliente" runat="server" Text=""></asp:Label>
			                            <asp:Label ID="lblIdInMuebleX" runat="server" Text=""></asp:Label>
		                            </div>
                                    <div class="col-md-1">
                                        <label for="r1">
                                            <asp:RadioButton ID="rbPnatural" runat="server" AutoPostBack="True" Checked="True" GroupName="R" Text="&nbsp;P.Natural"  OnCheckedChanged="R_CheckedChanged" />
                                        </label>
                                        
                                    </div>
                                    <div class="col-md-1">
                                        <label for="r2">
                                            <asp:RadioButton ID="rbPjuridica" runat="server" AutoPostBack="True" GroupName="R" Text="&nbsp;P.Juridica" OnCheckedChanged="R_CheckedChanged" />
                                        </label>
                                    </div>	
                                    <div class="col-md-2">
                                        <label for="r3">
                                            <asp:CheckBox ID="chkInversionista" runat="server" Text="&nbsp;Inversionista" />
                                        </label>
                                    </div>	
                                    <div class="col-md-2">
                                        <label for="Nacionalidad">
                                            Nacionalidad: <br />
	                                        <asp:DropDownList ID="ddlNacionalidad" runat="server" 
		                                        DataSourceID="SqlNacionalidad" DataTextField="Nombre" 
		                                        DataValueField="IdNacionalidad" CssClass="BordeRadio10" BackColor="Yellow"></asp:DropDownList>	
	                                        <asp:SqlDataSource ID="SqlNacionalidad" runat="server" 
		                                        ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" 
		                                        SelectCommand="sp_VTA_Combo_Nacionalidad" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        </label>
                                    </div>	
                                    <div class="col-md-3">
                                        <label for="EstadoCivil">
                                            Estado Civil: <br />
								            <asp:DropDownList ID="ddlEstadoCivil" runat="server" 
												DataSourceID="SqlEstadoCivil" DataTextField="Nombre" 
                                                DataValueField="IdEstadoCivil" CssClass="BordeRadio10" BackColor="Yellow"></asp:DropDownList>		
								            <asp:SqlDataSource ID="SqlEstadoCivil" runat="server" 
									            ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" 
									            SelectCommand="sp_VTA_Combo_EstadoCivil" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        </label>
                                    </div>	
                                    <div class="col-md-2">
                                        <label for="Rut">
                                            Rut: <br />
								            <asp:TextBox ID="txtRut" runat="server" Width="80px" CssClass="BordeRadio10" BackColor="Yellow" enabled ="false"></asp:TextBox>
								            -
								            <asp:TextBox ID="txtDigito" runat="server" MaxLength="1" Width="20px" CssClass="BordeRadio10" BackColor="Yellow" enabled ="false"></asp:TextBox>
                                        </label>
                                    </div>	
                                </div>

                                <div class="row">
                                    <div class="col-md-2">
                                        <label for="Genero">
                                            Género: <br />
								            <asp:RadioButton ID="rbMasculino" runat="server" Text="&nbsp;Masculino" GroupName="Genero" Checked="True" />&nbsp;&nbsp;
								            <asp:RadioButton ID="rbFemenino" runat="server" Text="&nbsp;Femenino" GroupName="Genero"/>
                                        </label>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="Nombres">
                                            Nombres: <br />
                                            <asp:TextBox ID="txtNombres" runat="server" Width="120px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                        </label>
                                    </div>	
                                    <div class="col-md-2">
                                        <label for="ApePaterno">
                                            Apellido Paterno: <br />
                                            <asp:TextBox ID="txtPaterno" runat="server" Width="120px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                        </label>
                                    </div>	
                                    <div class="col-md-2">
                                        <label for="ApeMaterno">
                                            Apellido Materno: <br />
                                            <asp:TextBox ID="txtMaterno" runat="server" Width="120px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                        </label>
                                    </div>	
                                    <div class="col-md-2">
                                        <label for="Direccion">
                                            Dirección (calle, número): <br />
                                            <asp:TextBox ID="txtDireccion" runat="server" Width="250px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                        </label>
                                    </div>	
                                </div>

                                <div class="row">
                                    <div class="col-md-3">
                                        <label for="Ciudad">
                                            Ciudad: <br />
								            <asp:DropDownList ID="ddlCiudad" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="CiuNombre" 
                                                DataValueField="CiuCodigo" CssClass="BordeRadio10" BackColor="Yellow"></asp:DropDownList>
								            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" 
									            SelectCommand="sp_VTA_Ciudades" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        </label>
                                    </div>	
                                    <div class="col-md-3">
                                        <label for="Comuna">
                                            Comuna: <br />
								            <asp:DropDownList ID="ddpComunas" runat="server" DataSourceID="SqlDataSource2" DataTextField="CmuNombre" DataValueField="CmuCodigo" 
                                                CssClass="BordeRadio10" BackColor="Yellow"></asp:DropDownList>
								            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" 
                                                SelectCommand="sp_VTA_ComunasXCiudad" SelectCommandType="StoredProcedure">
									            <SelectParameters>
										            <asp:ControlParameter ControlID="ddlCiudad" Name="Ciudad" PropertyName="SelectedValue" Type="Int32" />
									            </SelectParameters>
								            </asp:SqlDataSource>
                                        </label>
                                    </div>	
                                    <div class="col-md-2">
                                        <label for="Telefono">
                                            Teléfono: <br />
                                            <asp:TextBox ID="txtFono" runat="server" Width="100px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                        </label>
                                    </div>	
                                    <div class="col-md-2">
                                        <label for="NombreContacto">
                                            Nombre Contacto: <br />
                                            <asp:TextBox ID="txtNombreContacto" Width="100px" runat="server" CssClass="BordeRadio10"></asp:TextBox>
                                        </label>
                                    </div>	
                                    <div class="col-md-2">
                                        <label for="TelefonoContacto">
                                            Teléfono Contacto: <br />
                                            <asp:TextBox ID="txtFonoContacto" Width="100px" runat="server" CssClass="BordeRadio10"></asp:TextBox>
                                        </label>
                                    </div>		
                                </div>

                                <div class="row">
	
                                    <div class="col-md-3">
                                        <label for="Email">
                                            Email: <br />
                                            <asp:TextBox ID="txtEmail" runat="server" Width="250px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                        </label>
                                    </div>		
                                    <div class="col-md-3">
                                        <label for="TipoOcupacion">
                                            Tipo Ocupación: <br />
								            <asp:DropDownList ID="ddlTipoOcupacion" runat="server" DataSourceID="sdsTipoOpcupacion" DataTextField="Nombre" DataValueField="IdTipoOcupacion" AutoPostBack="True" 
                                                CssClass="BordeRadio10" BackColor="Yellow" Height="25px"></asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsTipoOpcupacion" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" 
                                                SelectCommand="sp_VTA_Combo_Ocupacion" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        </label>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="Ocupación">
                                            Ocupación: <br />
                                            <asp:TextBox ID="txtOcupacion" runat="server" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                        </label>
                                    </div>	
                                    <div class="col-md-3" id="DivFecNac" runat="server">
                                        <label for="FechaNacimiento" >
                                            Fecha de Nacimiento: <br />
                                            <input type="date" value="" id="txtFechaNacimiento" min="" runat="server" size="60" class="BordeRadio10" style="background-color: #FFFF00;" />
                                        </label>
                                    </div>
                                </div>
                                <div class="row">

                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <br /><br />

                                        <fieldset class="principal">
                                            <legend>
                                                <asp:Label runat="server" Text=" Datos Laborales" CssClass="principal"></asp:Label>
                                            </legend>
                                            <div class="container-fluid">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label for="Empresa">
                                                            Empresa: &nbsp;
                                                            <asp:TextBox ID="TxtEmpresa" runat="server" Width="200px" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                                        </label>
                                                    </div>	
                                                    <div class="col-md-4">
                                                        <label for="DireccionLab">
                                                            Dirección: &nbsp;
                                                            <asp:TextBox ID="txtDireccionLaboral" runat="server" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                                        </label>
                                                    </div>	
                                                    <div class="col-md-4">
                                                        <label for="Telefono">
                                                            Teléfono: &nbsp;
                                                            <asp:TextBox ID="txtFonoEmpresa" runat="server" CssClass="BordeRadio10" BackColor="Yellow"></asp:TextBox>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </fieldset> 
					    <br />
                    </td>
                </tr>
            </table>
            <br /><br />   	
		    <table class="principal">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div style="display: none">
                                    <asp:Label ID="lblEsMaestra" runat="server" Text="0"></asp:Label>
                                </div>
                                <div class="btn-group" role="group" aria-label="Basic example">
                                    <asp:LinkButton ID="lbPatrimonio" runat="server" class="btn btn-secondary active border border-dark" OnClick="lbPatrimonio_Click">Origen Patrimonio</asp:LinkButton>
                                    <asp:LinkButton ID="lbInversora" runat="server" class="btn btn-secondary border border-dark" OnClick="lbInversora_Click">Experiencia Inversora</asp:LinkButton>
                                    <asp:LinkButton ID="lbPerfilInversor" runat="server" class="btn btn-secondary border border-dark" OnClick="lbPerfilInversor_Click">Perfil Inversor</asp:LinkButton>
                                </div>               
                                <br />

                                <asp:MultiView ID="MultiView1" runat="server">
                                    <asp:View ID="View4" runat="server">
                                        <br />
                                        <fieldset class="principal">
                                            <br />
			                                <table style="width:100%">
				                                <tr>
						                            <td>
							                            <asp:CheckBox ID="chkAhorro" runat="server" Text="&nbsp;Ahorro" />
						                            </td>
					                                <td>
							                            <asp:CheckBox ID="chkVenta" runat="server" Text="&nbsp;Venta Bienes Raices"  />
					                                </td>
						                            <td>
							                            <asp:CheckBox ID="chkRescate" runat="server" Text ="&nbsp;Rescate Inversiones" />
						                            </td>
						                            <td>
							                            <asp:CheckBox ID="chkHerencia" runat="server" Text="&nbsp;Herencias"  />
						                            </td>
				                                </tr>
				                                <tr>
						                            <td>
							                            <asp:CheckBox ID="chkIndemnizacion" runat="server" Text="&nbsp;Indenmizacion" />
						                            </td>
					                                <td>
							                            <asp:CheckBox ID="chkActividad" runat="server" Text="&nbsp;Actividad Empresarial" />
					                                </td>
						                            <td colspan="2">
							                            Otros: <asp:TextBox ID="txtOtros" Width="250" runat="server" CssClass="BordeRadio10"></asp:TextBox>
						                            </td>
				                                </tr>
			                                </table>
                                            <br />
                                        </fieldset>
                                    </asp:View>

                                    <asp:View ID="View5" runat="server">
                                        <br />
			                            <fieldset class="principal">
                                            <br />
                                            <p>El Cliente declara tener experiencia en los siguientes productos:</p>
				                            <table style="width:100%">
					                            <tr>
							                        <td width="25%">
								                        <asp:CheckBox ID="chkEI_Ahorro" runat="server" Text="&nbsp;Ahorro" />
							                        </td>
							                        <td width="25%">
								                        <asp:CheckBox ID="chkEI_Venta" runat="server" Text="&nbsp;Venta Bienes Raices"  />
							                        </td>
							                        <td width="25%">
								                        <asp:CheckBox ID="chkEI_Rescate" runat="server" Text ="&nbsp;Rescate Inversiones" />
							                        </td>
							                        <td width="25%">
								                        <asp:CheckBox ID="chkEI_Herencia" runat="server" Text="&nbsp;Herencias"  />
							                        </td>
					                            </tr>
				                            </table>
                                            <br />
			                            </fieldset>
                                    </asp:View>

                                    <asp:View ID="View1" runat="server">
			                            <br/>
			                            <fieldset class="principal">
                                            <br />
				                            <table style="width:100%">
					                            <tr>
							                        <td width="25%">
								                        <asp:RadioButton ID="PI1" GroupName="perfil" runat="server" Text="&nbsp;Conservador" Checked="True" />
							                        </td>
							                        <td width="25%">
								                        <asp:RadioButton ID="PI2" GroupName="perfil" runat="server" Text="&nbsp;Moderado"  />
							                        </td>
							                        <td width="25%">
								                        <asp:RadioButton ID="PI3" GroupName="perfil" runat="server" Text ="&nbsp;Arriesgado" />
							                        </td>
							                        <td width="25%">
								                        <asp:RadioButton ID="PI4" GroupName="perfil" runat="server" Text="&nbsp;Muy Arriesgado"  />
							                        </td>
					                            </tr>
				                            </table>
                                            <br />
			                            </fieldset>
                                    </asp:View>
                                </asp:MultiView>

                                <br /><br />	

				                <table class="principal">
					                <tr>
						                <td align="left">
							                <asp:LinkButton ID="lnkVolver" runat="server" CssClass="botoMaestra btn" OnClick="lnkVolver_Click"   >Volver</asp:LinkButton>
						                </td>
						                <td align="right">
							                <asp:LinkButton ID="lnkConfirmModificar" runat="server" CssClass="botoMaestra btn" Width="150" ToolTip="Modificar Cliente" OnClick="lnkConfirmModificar_Click" >Modificar Cliente</asp:LinkButton>
						                </td>
					                </tr>
				                </table>

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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </form>
    </body>
</html>
