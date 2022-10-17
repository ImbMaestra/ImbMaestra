<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmTabCotizacion4.aspx.cs" Inherits="MaestraNet.GC.SVTA.Cotizacion.frmTabCotizacion4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../../../open-iconic/font/css/open-iconic-bootstrap.css" rel="stylesheet" />
    <script src="../../../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../../../Scripts/bootstrap.min.js"></script>
    <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../sistema_venta.css" rel="stylesheet" />
    <title></title>

	<script type="text/javascript">
		function showCliente() {
            $('#modalCliente').modal('show');
		}

		function showDetalle(){
            $('#modalDetalle').modal('show');
		}

		function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;    
            return true;
		    }
        function showEnviarEmail() {
                $('#modalEnviarCotizacion').modal('show');
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
        <div>
        </div> <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<%--
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
--%>
        

        <br /><br />
		<table class="principal">
            <tr>
                <td>
                    <!-- Filtros -->
                    <fieldset class="principal">
                        <legend>
                            <asp:Label ID="lblTituloPromesa" runat="server" Text=" Filtrar Cotizaciones" CssClass="principal"></asp:Label>
                        </legend>
                        <div class="container-fluid">
                            <div class="row">
			                    <div style="display: none">
				                    <asp:Label ID="lblIdCotizacion" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lblIdCotizacionBuscar" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lblRut" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lblClienteBuscar" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lblIdProyecto" runat="server" Text="0"></asp:Label>
									<asp:Label ID="lblFechaDesde" runat="server" Text="01-01-1900"></asp:Label>
									<asp:Label ID="lblFechaHasta" runat="server" Text="01-01-1900"></asp:Label>
                                    <asp:Label ID="lblIdVendedor" runat="server" Text="0"></asp:Label>
			                    </div>
								<div class="col-md-3">
									<label for="Proyecto">
                                        Proyecto: &nbsp;&nbsp;
										<asp:DropDownList ID="drpIdProyecto" runat="server" DataSourceID="SqlDataSource5" DataTextField="Proyecto" DataValueField="IdProyecto" CssClass="BordeRadio10"></asp:DropDownList>
										<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" SelectCommand="sp_VTA_Combo_Proyecto" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
									</label>
								</div>
								<div class="col-md-2">
									Desde:&nbsp;&nbsp;
									<input id="fechaDesde" type="date" runat="server" class="BordeRadio10" style="width:120px" />
								</div>
								<div class="col-md-2">
									Hasta:&nbsp;&nbsp;
									<input id="fechaHasta" type="date" runat="server" class="BordeRadio10"  style="width:120px"/>
								</div>
                                <div class="col-md-3">
                                    <label for="Estado">
                                        Estado: &nbsp;
                                        <asp:DropDownList ID="ddlEstadoCoti" runat="server" CssClass="BordeRadio10" DataSourceID="sdsEstadoCoti" DataTextField="glosa" DataValueField="codigo" Width="110"></asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsEstadoCoti" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>"
                                         SelectCommand="VTA_consultaParametro" SelectCommandType="StoredProcedure">
                                             <SelectParameters>
                                                <asp:Parameter  DbType="String" Name="nombreParametro" DefaultValue="CotizaEstado" />
							                </SelectParameters>
                                        </asp:SqlDataSource>                                        
                                    </label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <label for="Cliente">
                                        Cliente: &nbsp;
                                        <asp:TextBox ID="txtCliente" runat="server" CssClass="BordeRadio10" placeholder="Nombre Apellido" ></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="RUT">
                                        Rut: &nbsp;
                                        <asp:TextBox ID="txtRut" runat="server" CssClass="BordeRadio10"  placeholder="11111111-1" Width="100"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <label for="NumCotizacion">
                                        Nº de Cotizacion: &nbsp;
                                        <asp:TextBox ID="txtCotizacion" runat="server" Width="70px" placeholder="12345" onkeypress="return isNumberKey(event)" CssClass="BordeRadio10"></asp:TextBox>
                                    </label>
                                </div>
								<div class="col-md-3">
									<label for="NombreEjecutivo">
                                        Ejecutivo: &nbsp;
										<asp:DropDownList ID="drpEjecutivo" runat="server" DataSourceID="SqldrpEjecutivo" DataTextField="Ejecutivo" DataValueField="IdVendedor" CssClass="BordeRadio10"></asp:DropDownList>
										<asp:SqlDataSource ID="SqldrpEjecutivo" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" SelectCommand="sp_VTA_Combo_Vendedor_Cot" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
									</label>
								</div>

                                <div class="col-md-2" style="text-align:right">
                                    <label for="Buscar">     
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="botoMaestra btn" OnClick="lnkBuscar_Click"  ToolTip="Buscar cotizacion" >
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

<%-- 
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modal1">
                    <div class="center1">
                    <img alt="" src="../../../Images/cargando.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
--%>



        <br />

		<table class="principal">
            <tr>
                <td>
				    <fieldset class="principal">
				        <legend>
                            <asp:Label ID="lblLista" runat="server" Text=" Cotizaciones Vigentes" CssClass="principal"></asp:Label>
				        </legend>
                        <table width="100%">
                            <tr>
                                <td width="60%"> 
		                            <asp:LinkButton ID="cmdExcel" runat="server" CssClass="botoMaestra btn" OnClick="cmdExcel_Click" ToolTip="Exportar a Excel" BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="0px" ForeColor="White" style="box-shadow: 0 0 0px;">
			                            <img src="../../../img/excel.gif"/>
		                            </asp:LinkButton>
                                </td>
                                <td width="20%"> 
		                            <asp:LinkButton ID="cmdExcelDetalle" runat="server" CssClass="botoMaestra btn" OnClick="cmdExcelDetalle_Click" ToolTip="Exportar a Excel" BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="0px" ForeColor="White" style="box-shadow: 0 0 0px;">
			                            <img src="../../../img/excel.gif"/> <span style="color:black;">Detalle por Inmueble</span>
		                            </asp:LinkButton>
                                </td>
                                <td align="right">
                                    <asp:LinkButton ID="cmdNewCoti" runat="server" CssClass  ="botoMaestra btn" OnClick="cmdNewCoti_Click" ToolTip="Crear Cotizacion" >
                                        Nueva Cotizacion <span class="oi oi-calculator"></span>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <!-- Grid principal -->
                        <asp:GridView ID="grdCotizaciones" runat="server"
                            DataSourceID="SqlGrdCotizaciones" OnRowCommand="grdCotizaciones_RowCommand"
						    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                            CellPadding="4" DataKeyNames="IdCotizacion" OnRowDataBound="grdCotizaciones_RowDataBound"  
						    CssClass="grid_data" PageSize="10">

                            <AlternatingRowStyle CssClass="grid_linea_alterna" />
                            <Columns>
                                <asp:ButtonField DataTextField="IdCotizacion" HeaderText="N° Cotizacion"  CommandName="Cotizacion"  SortExpression="IdCotizacion" ControlStyle-Font-Underline="true" />
                                <asp:BoundField DataField="NombreEmpresa" HeaderText="Empresa" SortExpression="NombreEmpresa" HeaderStyle-Font-Underline="true"/>
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" HeaderStyle-Font-Underline="true"/>
                                <asp:BoundField DataField="Dias" HeaderText="Dias" SortExpression="Dias" HeaderStyle-Font-Underline="true"/>
                                <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" ReadOnly="True" SortExpression="Proyecto" HeaderStyle-Font-Underline="true"/>
                                <asp:ButtonField DataTextField="Inmuebles" HeaderText="Inmuebles" SortExpression="Inmuebles" CommandName="Inmuebles" ControlStyle-Font-Underline="true" HeaderStyle-Font-Underline="true"/>
                                <asp:ButtonField DataTextField="Cliente" HeaderText="Cliente" SortExpression="Cliente" CommandName="Cliente" ControlStyle-Font-Underline="true" HeaderStyle-Font-Underline="true"/>
                                <asp:BoundField DataField="Rut" HeaderText="Rut" ReadOnly="True" SortExpression="Rut" HeaderStyle-Font-Underline="true"/>
                                <asp:BoundField DataField="UF" HeaderText="UF" ReadOnly="True" SortExpression="UF" DataFormatString="{0:N0} UF" HeaderStyle-Font-Underline="true"/>
                                <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" SortExpression="Estado" HeaderStyle-Font-Underline="true"/>
                                <asp:BoundField DataField="Vendedor" HeaderText="Ejecutivo" ReadOnly="True" SortExpression="Vendedor" HeaderStyle-Font-Underline="true"/>
                                <asp:BoundField DataField="mail" HeaderText="mail" ReadOnly="True" SortExpression="mail" HeaderStyle-Font-Underline="true" FooterStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" ItemStyle-CssClass="ColumnaOculta"/>
                                <asp:TemplateField HeaderText="Crear Reserva" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate >
                                        <asp:LinkButton ID="lnkNuevaReserva" runat="server" CssClass="btn grid_boton" Text="Crear"
                                                ToolTip="Crear Reserva"  CommandName="Reserva" CommandArgument='<%# Container.DataItemIndex %>' ForeColor="#ffffff">
                                            <span class="oi oi-document"></span>
					                    </asp:LinkButton>
                                        <asp:LinkButton ID="LnkSendMail" runat="server" CssClass="btn grid_boton" Text="Crear"
                                                ToolTip="Enviar Cotizacion"  CommandName="Email" CommandArgument='<%# Container.DataItemIndex %>' ForeColor="#ffffff">
                                            <span class="oi oi-envelope-closed"></span>
					                    </asp:LinkButton>
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
                        <asp:SqlDataSource ID="SqlGrdCotizaciones" runat="server" ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" 
                                SelectCommand="sp_VTA_Buscar_Cotizacion_Generales_v3" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lblIdCotizacionBuscar" Name="IdCotizacion" PropertyName="Text" Type="Int32" />
                                <asp:ControlParameter ControlID="lblRut" Name="Rut" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblClienteBuscar" Name="Cliente" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="ddlEstadoCoti" Name="EstadoCoti" Type="String" />
								<asp:ControlParameter ControlID="lblIdProyecto" DefaultValue="0"  Name="IdProyecto" PropertyName="Text" Type="Int32" />
								<asp:ControlParameter ControlID="lblFechaDesde" DbType="Date" Name="fechaDesde" PropertyName="Text" />
								<asp:ControlParameter ControlID="lblFechaHasta" DbType="Date" Name="fechaHasta" PropertyName="Text" />
                                <asp:ControlParameter ControlID="lblIdVendedor" DefaultValue="0" Name="IdVendedor" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <!-- Grid principal -->
                        <br /><br />
                    </fieldset>
					<br />
                </td>
            </tr>
        </table>




		<!-- Modal Cliente-->
        <div class="modal fade" id="modalCliente" tabindex="-1" role="dialog" aria-labelledby="exampleModalDetalle" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">    
                        <div class="modal-header">
                        <h3><asp:Label ID="lblCliente" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium" ForeColor="Black"></asp:Label></h3>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div> 
                        <div class="modal-body">                  
                            <center>                   
								<asp:GridView ID="grvCliente" runat="server" CellPadding="4" Font-Names="Arial"  
									Font-Size="X-Small" ForeColor="#333333" AutoGenerateColumns="False" DataSourceID="SqlDataSource3">
									<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
									<Columns>
										<asp:BoundField DataField="EntRazonSocial" HeaderText="Cliente" SortExpression="EntRazonSocial" />
										<asp:BoundField DataField="EntRut" HeaderText="Rut" SortExpression="EntRut" />
										<asp:BoundField DataField="EntMail" HeaderText="E-Mail" SortExpression="EntMail" />
										<asp:BoundField DataField="EntDirFono" HeaderText="Fono" SortExpression="EntDirFono" />
										<asp:BoundField DataField="EntDirDireccion" HeaderText="Direccion" SortExpression="EntDirDireccion" />
                                        <asp:BoundField DataField="CmuNombre" HeaderText="Comuna" SortExpression="CmuNombre" />
									</Columns>
									<EditRowStyle BackColor="#999999" />
									<FooterStyle BackColor="#0c3a63" Font-Bold="True" ForeColor="White" />
									<HeaderStyle BackColor="#0c3a63" Font-Bold="True" ForeColor="White" />
									<PagerStyle BackColor="#0c3a63" ForeColor="White" HorizontalAlign="Center" />
									<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
									<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
									<SortedAscendingCellStyle BackColor="#E9E7E2" />
									<SortedAscendingHeaderStyle BackColor="#506C8C" />
									<SortedDescendingCellStyle BackColor="#FFFDF8" />
									<SortedDescendingHeaderStyle BackColor="#6F8DAE" />
								</asp:GridView>

								<asp:SqlDataSource ID="SqlDataSource3" runat="server" 
									ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" 
									SelectCommand="sp_VTA_Cliente_Cotizacion" SelectCommandType="StoredProcedure">
									<SelectParameters>
										<asp:ControlParameter ControlID="lblIdCotizacion" Name="IdCoti" PropertyName="Text" Type="Int32" />
									</SelectParameters>
								</asp:SqlDataSource>
                                                            
                            </center>

                        </div>   
                        <div class="modal-footer">
						    <button type="button" class="btn botonCerrar" data-dismiss="modal">Cerrar</button>
					    </div>                    
                    </div>   
                </div>
        </div>
		<!-- Fin Modal Detalle Cotizacion -->


		<!-- Modal Detalle Cotizacion-->
        <div class="modal fade" id="modalDetalle" tabindex="-1" role="dialog" aria-labelledby="exampleModalDetalle" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">    
                        <div class="modal-header">
                        <h3><asp:Label ID="lblDetalle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium" ForeColor="Black"></asp:Label></h3>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div> 
                        <div class="modal-body">                  
                            <!-- <div id="idScroll" style="overflow-y: scroll;"> -->
                                        <center>                   
											<asp:GridView ID="grdDetalle" runat="server" CellPadding="4" Font-Names="Arial" Font-Size="X-Small" ForeColor="#333333" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
												<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
												<Columns>
													<asp:BoundField DataField="Item" HeaderText="Item" ReadOnly="True" SortExpression="Item" />
													<asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
													<asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
													<asp:BoundField DataField="Piso" HeaderText="Piso" SortExpression="Piso" />
													<asp:BoundField DataField="Edificio" HeaderText="Edificio" SortExpression="Edificio" />
													<asp:BoundField DataField="Numero" HeaderText="N°" SortExpression="Numero" />
													<asp:BoundField DataField="M2" HeaderText="M2" SortExpression="M2" />
													<asp:BoundField DataField="M2Terreno" HeaderText="M2 Balcon" SortExpression="M2Terreno" />
													<asp:BoundField DataField="Orientacion" HeaderText="Orientacion" SortExpression="Orientacion" />
													<asp:BoundField DataField="MontoPrecioFinal" HeaderText="Precio" SortExpression="MontoPrecioFinal" />
												</Columns>
												<EditRowStyle BackColor="#999999" />
												<FooterStyle BackColor="#0c3a63" Font-Bold="True" ForeColor="White" />
												<HeaderStyle BackColor="#0c3a63" Font-Bold="True" ForeColor="White" />
												<PagerStyle BackColor="#0c3a63" ForeColor="White" HorizontalAlign="Center" />
												<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
												<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
												<SortedAscendingCellStyle BackColor="#E9E7E2" />
												<SortedAscendingHeaderStyle BackColor="#506C8C" />
												<SortedDescendingCellStyle BackColor="#FFFDF8" />
												<SortedDescendingHeaderStyle BackColor="#6F8DAE" />
											</asp:GridView>

											<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
												ConnectionString="<%$ ConnectionStrings:Sistemas_Maestra %>" 
												SelectCommand="sp_VTA_Detalle_Cotizacion" SelectCommandType="StoredProcedure">
												<SelectParameters>
													<asp:ControlParameter ControlID="lblIdCotizacion" Name="IdCotizacion" PropertyName="Text" Type="Int32" />
												</SelectParameters>
											</asp:SqlDataSource>
                                                            
                                        </center>

                              <!--  </div> -->
                        </div>    
                        <div class="modal-footer">
						    <button type="button" class="btn botonCerrar" data-dismiss="modal">Cerrar</button>
					    </div>
                    </div>   
                </div>
        </div>
		<!-- Fin Modal Detalle Cotizacion -->


    <%--   
            </ContentTemplate>
           </asp:UpdatePanel> 
        --%>  
         <!-- Modal Envia Cotizacion-->
        <div class="modal fade" id="modalEnviarCotizacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalDetalle" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document" style="max-width: 1100px">
                <div class="modal-content" style="width:100%">     
						<div class="modal-header">
						    <h3>
                                <asp:Label ID="lblCotizacionEmail" runat="server" Text="Enviar Cotizacion"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                <br />
                                <asp:Label ID="lblTituloEmail" runat="server"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="Medium" ForeColor="Black"></asp:Label>
						    </h3>
                            
						    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
							    <span aria-hidden="true">&times;</span>
						    </button>
						</div>
                        <div class="modal-body">
                            <center>
                                <asp:Label ID="lblCorreo" runat="server" Font-Bold="True" Text="Email:  "></asp:Label>
                                <asp:TextBox ID="txtCorreo" runat="server"  CssClass="BordeRadio10" Width="250"></asp:TextBox>
                                <asp:Label ID="lblEmailidCotizacion" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblEmailPDF" runat="server" Visible="false"></asp:Label>
                            </center>
                        </div>
                        <div class="modal-footer">
                            <div style="margin-left:auto; margin-right:auto;">
                                    <asp:LinkButton ID="lnkEnviarCorreo" runat="server" onclick="lnkEnviarCorreo_Click" CssClass="btn grid_boton" OnClientClick="CloseModalContabilizarPago()">
                                        <span class="oi oi-envelope-closed"></span>
                                        Enviar
                                    </asp:LinkButton>
                            </div>
                        </div>
					</div>
				</div>
		</div>
		<!-- Fin Modal Contabilizar -->
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
    </form>
</body>
</html>
