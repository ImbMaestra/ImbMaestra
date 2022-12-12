using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using MaestraNet.Entidad;
using MaestraNet.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;
using MaestraNet.Util;

namespace MaestraNet.GC.SVTA.Mantenedor
{
    public partial class frmCargaMasiva : System.Web.UI.Page
    {
        private void llenaTorres(int idProyecto)
        {
            BLProyecto blProyecto = new BLProyecto();
            DataSet dsTorres;
            ListItem liElemento;
            string funcionJS;
            dsTorres = blProyecto.TorresProyecto(idProyecto);
            ddlTorre.Items.Clear();

            //gvInmuebles.DataBind();

            foreach (DataRow oRow in dsTorres.Tables[0].Rows)
            {
                liElemento = new ListItem();
                liElemento.Text = oRow[0].ToString();
                liElemento.Value = oRow[1].ToString();
                ddlTorre.Items.Add(liElemento);
            }
            funcionJS = " $(function() { $('[id*=lstTorre]').multiselect({includeSelectAllOption: true });});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        private string SortExpression
        {
            get { return ViewState["SortExpression"] != null ? ViewState["SortExpression"].ToString() : "nombre"; }
            set { ViewState["SortExpression"] = value; }
        }

        private void Alerta(string msg, int tipo, bool OcultaGrilla = false)
        {
            string funcionJS = "";
            if (tipo == 1) //ERROR
            {
                lblAlertaMSGError.Text = msg;
                funcionJS = "showAlertaError();";
            }
            //if (tipo == 2) // CONFIRMACION
            //{
            //    lblAlertaMSGConfirmar.Text = msg;
            //    funcionJS = "showAlertaConfirmar();";
            //}
            if (tipo == 3 && OcultaGrilla) // INFORMACION
            {
                lblAlertaMSGInfo.Text = msg;
                funcionJS = "showAlertaInformar();$('#GrillaInmueble').hide();";
            }
            if (tipo == 3 && !OcultaGrilla) // INFORMACION
            {
                lblAlertaMSGInfo.Text = msg;
                funcionJS = "showAlertaInformar();";
            }
            if (tipo == 4) // ALERTA
            {
                lblAlertaMSGAlert.Text = msg;
                funcionJS = "showAlertaAlert();";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string funcionJS = "$('#GrillaInmueble').hide();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            }
        }

        protected void ddlProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaTorres(Convert.ToInt32(ddlProyecto.SelectedValue));
        }


        protected void btnCargaInmuebles_Click(object sender, EventArgs e)
        {
            string funcionJS;

            if (ddlProyecto.SelectedValue == "0")
            {
                Alerta("Seleccione un Proyecto y luego el archivo.", 3, true);
                ddlProyecto.Focus();
                return;
            }

            if (FileUpload1.HasFile)
            {
                //Funcionalidad para respaldo de archivo
                //string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                //string fileLocation = Server.MapPath("~/Documentos/");// + fileName);
                //FileUpload1.SaveAs(fileLocation + fileName);
                DataTable dt = new DataTable();

                //using (XLWorkbook workBook = new XLWorkbook(fileLocation + fileName))
                using (XLWorkbook workBook = new XLWorkbook(FileUpload1.PostedFile.InputStream))
                {
                    //var workBook = new XLWorkbook(path);
                    //Read the first Sheet from Excel file.
                    try
                    {
                        IXLWorksheet workSheet = workBook.Worksheet("Inmuebles");
                        //Loop through the Worksheet rows.
                        bool firstRow = true;
                        int detener = 0;

                        foreach (IXLRow row in workSheet.Rows())
                        {
                            //Use the first row to add columns to DataTable.
                            if (firstRow)
                            {
                                foreach (IXLCell cell in row.Cells())
                                {
                                    dt.Columns.Add(cell.Value.ToString());
                                }
                                firstRow = false;
                            }
                            else
                            {
                                int filasCount = workSheet.RowsUsed().Count();

                                if (filasCount - 1 == detener)
                                {
                                    break;
                                }

                                //Add rows to DataTable.
                                dt.Rows.Add();
                                int i = 0;

                                foreach (IXLCell cell in row.Cells(row.FirstCell().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                    i++;
                                }
                                detener++;
                            }
                        }
                        workSheet = null;
                    }
                    catch (Exception ex)
                    {
                        Alerta("Verifique el archivo, columnas y nombre de pestaña.", 1);
                        return;
                    }

                    try
                    {
                        //Cargar valores en grilla, luego aplicar ventana confirmar para subir los datos
                        Session["DatosArchivo"] = dt;
                        gvInmuebles.DataSource = dt;
                        gvInmuebles.DataBind();

                        funcionJS = "$('#GrillaInmueble').show();";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
                    }
                    catch (Exception ex)    
                    {
                        Alerta(ex.Message, 1);
                        return;
                    }
                }
            }
            else
            {
                Alerta("Seleccione un archivo para subir", 4);
            }
        }

        protected void btnDescargaInmuebles_Click(object sender, EventArgs e)
        {
            DataSet dsInmueble;
            //string funcionJS;
            BLInmueble oInmueble = new BLInmueble();
            Funciones oFunciones = new Funciones();
            int ndepto = txtDepto.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(txtDepto.Text.Trim());
            int iPiso;

            if (ddlProyecto.SelectedValue == "0")
            {
                Alerta("Seleccione un Proyecto.", 3, true);
                ddlProyecto.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPiso.Text))
                iPiso = 0;
            else
                iPiso = Convert.ToInt32(txtPiso.Text);

            try
            {
                dsInmueble = oInmueble.ListaInmuebleArchivo(Convert.ToInt32(ddlProyecto.SelectedValue), Convert.ToInt32(ddlTipoInmueble.SelectedValue), ddlTorre.SelectedValue, ndepto, Convert.ToInt32(ddlModeloInmueble.SelectedValue), iPiso, Convert.ToInt32(ddlOrientacion.SelectedValue), Convert.ToInt32(ddlEstadoInmueble.SelectedValue));
                if (dsInmueble.Tables[0].Rows.Count == 0)
                {
                    Alerta("Archivo sin datos", 1);
                    return;
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dsInmueble.Tables[0], "Inmuebles");
                    //wb.Worksheets.Add(dsInmueble.Tables[1], "Modelo");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=PlanillaDeCarga.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }


            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }
        }


        protected void btnDescargaFormatoArchivo_Click(object sender, EventArgs e)
        {
            BLInmueble blInmueble = new BLInmueble();
            Inmueble oInmueble = new Inmueble();
            //DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = blInmueble.GeneraFormatoExcelInmuebles();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds.Tables[0], "Inmuebles");
                wb.Worksheets.Add(ds.Tables[1], "Modelo");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PlanillaDeCarga.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }

        protected void btnSubirDatosArchivo_Click(object sender, EventArgs e)
        {
            BLInmueble oInmueble = new BLInmueble();
            DataTable dt = new DataTable();

            dt = (DataTable)Session["DatosArchivo"];

            if (ddlProyecto.SelectedValue == "0")
            {
                Alerta("Seleccione un Proyecto.", 3, true);
                ddlProyecto.Focus();
                return;
            }

            if (Session["DatosArchivo"] == null)
            {
                Alerta("Seleccione un archivo y visualice los datos.", 3, true);
                ddlProyecto.Focus();
                return;
            }

            bool revisaIDInmueble = false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
            
                if (dt.Rows[i]["IdInmueble"].ToString().Trim() != "")
                {
                    revisaIDInmueble = true;
                    break;
                }
            }

            try
            {
                oInmueble.IngresaInmueble2(Convert.ToInt32(ddlProyecto.SelectedValue), dt, Session["IdUsuario"].ToString());
                if (revisaIDInmueble == true)
                {
                    Alerta("Inmuebles editados de forma exitosa.", 3);
                }
                else
                {
                    Alerta("Inmuebles ingresados de forma exitosa.", 3);
                }
                
                dt = null;
                Session["DatosArchivo"] = null;
                gvInmuebles.DataSource = dt;
                gvInmuebles.DataBind();

                string funcionJS = "$('#GrillaInmueble').hide();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }
        }

        protected void gvDatosActuales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInmuebles.PageIndex = e.NewPageIndex;
            gvInmuebles.DataSource = (DataTable)Session["DatosArchivo"];
            gvInmuebles.DataBind();
        }

        protected void gvInmuebles_Sorting(object sender, GridViewSortEventArgs e)
        {
            Funciones ofunciones = new Funciones();
            DataTable dtOrden = new DataTable();

            SortExpression = e.SortExpression;
            SortDirection = SortDirection == "ASC" ? "DESC" : "ASC";

            dtOrden = ofunciones.BindGrid((DataTable)Session["DatosArchivo"], SortDirection, e.SortExpression);

            Session["DatosArchivo"] = dtOrden;
            gvInmuebles.DataSource = dtOrden;
            gvInmuebles.DataBind();
        }
    }
}