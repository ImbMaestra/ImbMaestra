using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MaestraNet.Util;
using MaestraNet.Data;
using System.Diagnostics;

namespace MaestraNet.GC.SVTA.Cotizacion
{
    public partial class frmTabCotizacion4 : System.Web.UI.Page
    {
        int selInd = 0;
        private void Acceso()
        {
            BLPerfiles oAcceso = new BLPerfiles();
            Funciones oFunc = new Funciones();
            DataSet dsAcceso = new DataSet();
            string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);

            //Comentado Miguel
            //try
            //{
            //    dsAcceso = oAcceso.PerfilBotones(Session["IdPerfil"].ToString(), pageName);
            //}
            //catch (Exception ex)
            //{
            //    //this.Alerta(ex.Message, 1);
            //}
            //oFunc.DisableControls(Page, false);

            //if (dsAcceso.Tables.Count > 0)
            //{
            //    foreach (DataRow oRow in dsAcceso.Tables[0].Rows)
            //    {
            //        oFunc.FindControlRecursive(Page, oRow["idboton"].ToString());
            //    }
            //}

        }
        private void Alerta(string msg, int tipo)
        {
            string funcionJS = "";
            if (tipo == 1) //ERROR
            {
                lblAlertaMSGError.Text = msg;
                funcionJS = "showAlertaError();";
            }

            if (tipo == 3) // INFORMACION
            {
                lblAlertaMSGInfo.Text = msg;
                funcionJS = "showAlertaInformar();";
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    Acceso();
                    ddlEstadoCoti.DataBind();
                    //ddlEstadoCoti.Items.FindByValue("99").Selected = true;

                    ddlEstadoCoti.Items.FindByValue("1").Selected = true;

                    drpEjecutivo.DataBind();
                    if (drpEjecutivo.Items.FindByValue(Session["IdUsuario"].ToString().ToUpper()) != null)
                    {
                        drpEjecutivo.Items.FindByValue(Session["IdUsuario"].ToString().ToUpper()).Selected = true;
                    }

                    string sFecha = DateTime.Today.ToString("yyyy-MM-dd");
                    fechaHasta.Value = sFecha;
                    fechaDesde.Value = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
                    lnkBuscar_Click(this, null);
                }
            }
            catch (Exception ex)
            {
                cs.dbTools db = new cs.dbTools();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                db.Guardar_Log_Errores(Path.GetFileName(Page.AppRelativeVirtualPath), currentMethodName, Session["IdUsuario"].ToString(), ex.Message);
            }
        }

        protected void grdCotizaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string cmd = e.CommandName.ToString();
                if (cmd.Equals("Cliente"))
                {
                    int xRow = Convert.ToInt16(e.CommandArgument);
                    LinkButton lnkCotizacion = (LinkButton)grdCotizaciones.Rows[xRow].Cells[0].Controls[0];
                    lblIdCotizacion.Text = lnkCotizacion.Text;//grdCotizaciones.Rows[xRow].Cells[0].Text;
                    SqlDataSource3.DataBind();
                    grvCliente.DataBind();
                    LinkButton lnkCliente = (LinkButton)grdCotizaciones.Rows[xRow].Cells[5].Controls[0];
                    lblCliente.Text = "Detalle Cliente :" + lnkCliente.Text;
                    string funcionJS = "showCliente();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
                }

                if (cmd.Equals("Inmuebles"))
                {
                    int xRow = Convert.ToInt16(e.CommandArgument);
                    LinkButton lnkCotizacion = (LinkButton)grdCotizaciones.Rows[xRow].Cells[0].Controls[0];
                    lblIdCotizacion.Text = lnkCotizacion.Text;//grdCotizaciones.Rows[xRow].Cells[0].Text;
                    SqlDataSource2.DataBind();
                    grdDetalle.DataBind();
                    lblDetalle.Text = "Detalle de Cotizacion " + lblIdCotizacion.Text;
                    string funcionJS = "showDetalle();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
                }

                if (cmd.Equals("Cotizacion"))
                {
                    int xRow = Convert.ToInt16(e.CommandArgument);
                    LinkButton lnkCotizacion = (LinkButton)grdCotizaciones.Rows[xRow].Cells[0].Controls[0];
                    string strFile = "Cotizacion_" + lnkCotizacion.Text + ".pdf";
                    string strPathWS = System.Configuration.ConfigurationManager.AppSettings["Path_WS_Print"];
                    string strFilePDF = strPathWS + strPathWS;
                    if (!File.Exists(strFilePDF))
                    {
                        PathPDF(lnkCotizacion.Text);
                    }
                    //Comentado (Miguel)
                    //wsReportFin.wsServidorImpresion ws = new wsReportFin.wsServidorImpresion();
                    //string strURL_PDF = ws.wmCheckReport(strFile, "RPT");
                    //string IP_Local = System.Configuration.ConfigurationManager.AppSettings["Server_Local"];
                    //string IP_Publica = System.Configuration.ConfigurationManager.AppSettings["Server_Publico"];
                    //strURL_PDF = strURL_PDF.Replace(IP_Local, IP_Publica);
                    //cs.ResponseUtlility.Redirect(strURL_PDF, "_blank", "menubar=0,width=600,height=800");
                }

                if (cmd.Equals("Reserva"))
                {
                    cs.dbTools db = new cs.dbTools();
                    int xRow = Convert.ToInt16(e.CommandArgument);
                    LinkButton lnkCotizacion = (LinkButton)grdCotizaciones.Rows[xRow].Cells[0].Controls[0];
                    Session["IdCotizacion"] = lnkCotizacion.Text;
                    Session["Origen"] = "Cotizacion";
                    Response.Redirect("~/GC/SVTA/Reserva/frmTabReserva2.aspx");
                }
                if (cmd.Equals("Email"))
                {
                    int xRow = Convert.ToInt16(e.CommandArgument);
                    LinkButton lnkCotizacion = (LinkButton)grdCotizaciones.Rows[xRow].Cells[0].Controls[0];
                    LinkButton lnkCliente = (LinkButton)grdCotizaciones.Rows[xRow].Cells[6].Controls[0];

                    string strFile = "Cotizacion_" + lnkCotizacion.Text + ".pdf";
                    string funcionJS = "showEnviarEmail();";
                    lblEmailPDF.Text = strFile;
                    lblEmailidCotizacion.Text = lnkCotizacion.Text;
                    txtCorreo.Text = grdCotizaciones.Rows[xRow].Cells[11].Text;
                    lblCotizacionEmail.Text = "Enviar Cotizacion Nro. " + lnkCotizacion.Text;
                    lblTituloEmail.Text = "Cliente: " + lnkCliente.Text;
                    ScriptManager.RegisterStartupScript(this, GetType(), "ModaBit", funcionJS, true);
                }
            }
            catch (Exception ex)
            {
                cs.dbTools db = new cs.dbTools();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                db.Guardar_Log_Errores(Path.GetFileName(Page.AppRelativeVirtualPath), currentMethodName, Session["IdUsuario"].ToString(), ex.Message);
            }
        }

        private string PathPDF(string IdCotizacion)
        {
            string srcPDF = "";

            try
            {
                //Comentado Miguel
                //wsReportFin.wsServidorImpresion WS = new wsReportFin.wsServidorImpresion();
                //string pdfName = "Cotizacion_" + IdCotizacion + ".pdf";
                //List<wsReportFin.Parameter> listParametros = new List<wsReportFin.Parameter>();
                //wsReportFin.Parameter p1 = new wsReportFin.Parameter();
                //p1.name = "Id";
                //p1.value = IdCotizacion;
                //listParametros.Add(p1);

                cs.dbTools db = new cs.dbTools();
                string queryProyecto = "exec dbo.sp_VTA_Cotizacion_Proyecto " + IdCotizacion;
                string Proyecto = db.ResultQueryRun(queryProyecto);
                int IdProyecto = Convert.ToInt32(Proyecto);

                //Comentado Miguel
                //if (IdProyecto == 25)
                //{
                //    Object obj = WS.wmExportar(wsReportFin.ReportType.PDF, "ImprimirCotizacionAlpha.rpt.rpt", listParametros.ToArray(), null, null, pdfName, "RPT", "ADQOPEOCOMPRA");
                //}
                //else
                //{
                //    Object obj = WS.wmExportar(wsReportFin.ReportType.PDF, "ImprimirCotizacion.rpt.rpt", listParametros.ToArray(), null, null, pdfName, "RPT", "ADQOPEOCOMPRA");

                //}


                //srcPDF = WS.wmCheckReport(pdfName, "RPT");

            }
            catch (Exception err)
            {
                srcPDF = err.Message.ToString();
            }
            return srcPDF;
        }

        protected void cmdNewCoti_Click(object sender, EventArgs e)
        {

            try
            {
                Response.Redirect("~/GC/SVTA/Cotizacion/frmTabCotizacion2.aspx");
            }
            catch (Exception ex)
            {
                cs.dbTools db = new cs.dbTools();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                db.Guardar_Log_Errores(Path.GetFileName(Page.AppRelativeVirtualPath), currentMethodName, Session["IdUsuario"].ToString(), ex.Message);
            }
        }

        protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            lblIdProyecto.Text = drpIdProyecto.Text;
            lblIdVendedor.Text = drpEjecutivo.Text;

            if (fechaDesde.Value.ToString().Length < 2)
            {
                lblFechaDesde.Text = "1900-01-01";
            }
            else
            {
                lblFechaDesde.Text = fechaDesde.Value;
            }

            if (fechaHasta.Value.ToString().Length < 2)
            {
                lblFechaHasta.Text = "1900-01-01";
                string sFecha = DateTime.Today.ToString("yyyy-MM-dd");
                lblFechaHasta.Text = sFecha;
            }
            else
            {
                lblFechaHasta.Text = fechaHasta.Value;
            }

            if (txtCotizacion.Text.Length > 0)
            {
                lblIdCotizacionBuscar.Text = txtCotizacion.Text;
            }
            else
            {
                lblIdCotizacionBuscar.Text = "0";
            }

            if (txtRut.Text.Length > 0)
            {
                lblRut.Text = txtRut.Text;
            }
            else
            {
                lblRut.Text = "0";
            }
            if (txtCliente.Text.Length > 0)
            {
                lblClienteBuscar.Text = txtCliente.Text;
            }
            else
            {
                lblClienteBuscar.Text = "0";
            }

            SqlGrdCotizaciones.DataBind();
            grdCotizaciones.DataBind();
            if (grdCotizaciones.Rows.Count == 0) { cmdExcel.Visible = false; } else { cmdExcel.Visible = true; }
        }

        protected void grdCotizaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                string hColor = "#B13261";
                string dColor = "#E9E7E2";
                System.Drawing.Color HeaderColor;
                System.Drawing.Color DataColor;
                LinkButton lnkbtn;
                string sort;
                int ind = 0;

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        ind++;
                        if (cell.Controls.Count > 0)
                        {
                            lnkbtn = cell.Controls[0] as LinkButton;
                            if (!string.IsNullOrEmpty(lnkbtn.CommandArgument))
                            {
                                if (this.grdCotizaciones.SortExpression == "")
                                {
                                    sort = "IdCotizacion";
                                }
                                else
                                {
                                    sort = this.grdCotizaciones.SortExpression;
                                }
                                if (lnkbtn.CommandArgument == sort)
                                {
                                    HeaderColor = System.Drawing.ColorTranslator.FromHtml(hColor);
                                    cell.BackColor = HeaderColor;
                                    lnkbtn.BackColor = HeaderColor;
                                    selInd = ind;
                                }
                            }
                        }
                    }
                }

                ind = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        ind++;
                        if (selInd == ind)
                        {
                            DataColor = System.Drawing.ColorTranslator.FromHtml(dColor);
                            cell.BackColor = DataColor;
                            try
                            {
                                LinkButton lnk = cell.Controls[0] as LinkButton;
                                lnk.BackColor = DataColor;
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cs.dbTools db = new cs.dbTools();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                db.Guardar_Log_Errores(Path.GetFileName(Page.AppRelativeVirtualPath), currentMethodName, Session["IdUsuario"].ToString(), ex.Message);
            }
        }

        protected void cmdExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string EstadoCoti = ddlEstadoCoti.SelectedValue;
                string Ejecutivo = drpEjecutivo.SelectedValue;

                lblIdProyecto.Text = drpIdProyecto.Text;
                string sQuery = "exec dbo.sp_VTA_Buscar_Cotizacion_Generales_v3 " + lblIdCotizacionBuscar.Text + ",'" + lblRut.Text + "','" + lblClienteBuscar.Text + "','" + EstadoCoti + "'," + lblIdProyecto.Text + ",'" + lblFechaDesde.Text + "','" + lblFechaHasta.Text + "','" + Ejecutivo + "';";
                cs.dbTools db = new cs.dbTools();
                GridView gvExcel = new GridView();
                System.Data.DataTable tb = db.ResultQuery(sQuery);
                gvExcel.DataSource = tb;
                gvExcel.DataBind();
                GridViewExportUtil.Export("Datos.xls", gvExcel);
            }
            catch (Exception ex)
            {
                cs.dbTools db = new cs.dbTools();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                db.Guardar_Log_Errores(Path.GetFileName(Page.AppRelativeVirtualPath), currentMethodName, Session["IdUsuario"].ToString(), ex.Message);
            }
        }

        protected void cmdExcelDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                string EstadoCoti = ddlEstadoCoti.SelectedValue;
                lblIdProyecto.Text = drpIdProyecto.Text;
                string sQuery = "exec dbo.sp_VTA_Buscar_Cotizacion_Detalle_x_Inmueble " + lblIdCotizacionBuscar.Text + ",'" + lblRut.Text + "','" + lblClienteBuscar.Text + "','" + Session["IdUsuario"].ToString() + "','" + EstadoCoti + "'," + lblIdProyecto.Text + ",'" + lblFechaDesde.Text + "','" + lblFechaHasta.Text + "';";
                cs.dbTools db = new cs.dbTools();
                GridView gvExcel = new GridView();
                System.Data.DataTable tb = db.ResultQuery(sQuery);
                gvExcel.DataSource = tb;
                gvExcel.DataBind();
                GridViewExportUtil.Export("Datos.xls", gvExcel);
            }
            catch (Exception ex)
            {
                cs.dbTools db = new cs.dbTools();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                db.Guardar_Log_Errores(Path.GetFileName(Page.AppRelativeVirtualPath), currentMethodName, Session["IdUsuario"].ToString(), ex.Message);
            }
        }

        protected void lnkEnviarCorreo_Click(object sender, EventArgs e)
        {
            BLCotizacion oCorreo = new BLCotizacion();
            try
            {
                oCorreo.EnviarCotizacion(Convert.ToInt32(lblEmailidCotizacion.Text), txtCorreo.Text, lblEmailPDF.Text);
                Alerta("Cotización enviada", 3);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                cs.dbTools db = new cs.dbTools();
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                db.Guardar_Log_Errores(Path.GetFileName(Page.AppRelativeVirtualPath), currentMethodName, Session["IdUsuario"].ToString(), ex.Message);
            }
        }
    }
}