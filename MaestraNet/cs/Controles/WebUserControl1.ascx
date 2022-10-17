<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="MaestraNet.cs.Controles.WebUserControl1" %>
<link href="../../GC/SVTA/sistema_venta.css" rel="stylesheet" />
<link href="../../open-iconic/font/css/open-iconic-bootstrap.css" rel="stylesheet" />
<script src="../../Scripts/jquery-3.0.0.min.js"></script>
<script src="../../Scripts/bootstrap.min.js"></script>
<link href="../../Content/bootstrap.min.css" rel="stylesheet" />

<script type="text/javascript">
    	function showCliente() {
         $('#modalCliente').modal('show');
		}

		function showDetalle(){
         $('#modalDetalle').modal('show');
		}
</script>

<div style="display: none"  >
	<asp:Label ID="lblRut" runat="server" Text=""></asp:Label>
	<asp:Label ID="lblIdCotizacion" runat="server" Text=""></asp:Label>
</div>

<asp:GridView ID="grdCotizaciones" runat="server" 
		AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdCotizacion" OnRowCommand="grdCotizaciones_RowCommand"
		AllowPaging="True" AllowSorting="True" CssClass="grid_data" PageSize="10"
        OnRowDataBound="grdCotizaciones_RowDataBound">
    <AlternatingRowStyle CssClass="grid_linea_alterna" />
    <Columns>
        <asp:BoundField DataField="IdCotizacion" HeaderText="N° Cotizacion" InsertVisible="False" ReadOnly="True" SortExpression="IdCotizacion" HeaderStyle-Font-Underline="true" />
        <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" HeaderStyle-Font-Underline="true" />
        <asp:BoundField DataField="Dias" HeaderText="Dias" SortExpression="Dias" HeaderStyle-Font-Underline="true" />
        <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" ReadOnly="True" SortExpression="Proyecto" HeaderStyle-Font-Underline="true" />
        <asp:ButtonField DataTextField="Inmuebles" HeaderText="Inmuebles" SortExpression="Inmuebles" CommandName="Inmuebles" ControlStyle-Font-Underline="true" HeaderStyle-Font-Underline="true" />
        <asp:ButtonField DataTextField="Cliente" HeaderText="Cliente" SortExpression="Cliente" CommandName="Cliente" ControlStyle-Font-Underline="true" HeaderStyle-Font-Underline="true" />
        <asp:BoundField DataField="Rut" HeaderText="Rut" ReadOnly="True" SortExpression="Rut" HeaderStyle-Font-Underline="true" />
        <asp:BoundField DataField="UF" HeaderText="UF" ReadOnly="True" SortExpression="UF" DataFormatString="{0:N0} UF" HeaderStyle-Font-Underline="true" />
        <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" SortExpression="Estado" HeaderStyle-Font-Underline="true" />
        <asp:BoundField DataField="Vendedor" HeaderText="Ejecutivo" ReadOnly="True" SortExpression="Vendedor" HeaderStyle-Font-Underline="true" />
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
									Font-Size="X-Small" ForeColor="#333333" AutoGenerateColumns="False"
                                    DataSourceID="SqlDataSource3">
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
                        </div>      
                        <div class="modal-footer">
					        <button type="button" class="btn botonCerrar" data-dismiss="modal">Cerrar</button>
				        </div>                    
                    </div>   
                </div>
        </div>
		<!-- Fin Modal Detalle Cotizacion -->
