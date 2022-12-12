using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MaestraNet.Data;
using MaestraNet.Util;
using System.Data.OleDb;
using System.IO;
using System.Collections;
using MaestraNet.Entidad;
using System.ComponentModel;

namespace MaestraNet.GC.SVTA.Mantenedor
{
    public partial class frmInmueblesNew : System.Web.UI.Page
    {
        private void llenaTorres(int idProyecto)
        {
            BLProyecto blProyecto = new BLProyecto();
            DataSet dsTorres;
            ListItem liElemento;
            string funcionJS;
            dsTorres = blProyecto.TorresProyecto(idProyecto);
            ddlTorre.Items.Clear();

            gvInmuebles.DataBind();

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

        private bool CheckBoxAllCabecera
        {
            get { return ViewState["CheckBoxAllCabecera"] == null ? false : (bool)ViewState["CheckBoxAllCabecera"]; }
            set { ViewState["CheckBoxAllCabecera"] = value; }
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
                //funcionJS = "showAlertaInformar();$('#GrillaInmueble').hide();"; //comentado Miguel, desacomentar cuando hayan datos en la BD
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
                CheckBoxAllCabecera = false;
                HttpContext.Current.Session["ProdSelection"] = null;
                string funcionJS = "$('#GrillaInmueble').hide();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);

                if (Session["datosBusquedaInmueble"] != null)
                {
                    //DataSet dsInmueble;
                    BLInmueble oInmueble = new BLInmueble();
                    Funciones oFunciones = new Funciones();
                    string[] datosBusqueda = Session["datosBusquedaInmueble"].ToString().Split(',');

                    ddlProyecto.SelectedValue = datosBusqueda[0];
                    llenaTorres(Convert.ToInt32(datosBusqueda[0]));
                    ddlTorre.SelectedValue = datosBusqueda[2];
                    ddlTipoInmueble.SelectedValue = datosBusqueda[1];
                    ddlModeloInmueble.SelectedValue = datosBusqueda[4];
                    txtDepto.Text = datosBusqueda[3];
                    txtPiso.Text = datosBusqueda[5];
                    ddlOrientacion.SelectedValue = datosBusqueda[6];
                    ddlEstadoInmueble.SelectedValue = datosBusqueda[7];

                }
            }
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            DataSet dsInmueble;
            string funcionJS;
            BLInmueble oInmueble = new BLInmueble();
            Funciones oFunciones = new Funciones();
            int ndepto = txtDepto.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(txtDepto.Text.Trim());
            int iPiso;

            HttpContext.Current.Session["ProdSelection"] = null;

            if (ddlProyecto.SelectedValue == "0")
            {
                Alerta("Seleccione un Proyecto", 3, true);
                ddlProyecto.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPiso.Text))
                iPiso = 0;
            else
                iPiso = Convert.ToInt32(txtPiso.Text);

            try
            {
                Session["datosBusquedaInmueble"] = ddlProyecto.SelectedValue + "," +
                                    ddlTipoInmueble.SelectedValue + "," +
                                    ddlTorre.SelectedValue + "," +
                                    txtDepto.Text.Trim() + "," +
                                    ddlModeloInmueble.SelectedValue + "," +
                                    txtPiso.Text.Trim() + "," +
                                    ddlOrientacion.SelectedValue + "," +
                                    ddlEstadoInmueble.SelectedValue;

                dsInmueble = oInmueble.ListaInmueble2(Convert.ToInt32(ddlProyecto.SelectedValue), Convert.ToInt32(ddlTipoInmueble.SelectedValue), ddlTorre.SelectedValue, ndepto, Convert.ToInt32(ddlModeloInmueble.SelectedValue), iPiso, Convert.ToInt32(ddlOrientacion.SelectedValue), Convert.ToInt32(ddlEstadoInmueble.SelectedValue));
                ViewState["Inmueble"] = dsInmueble.Tables[0];

                SortExpression = "Descripcion";

                // if (dsInmueble.Tables[0].Rows.Count > 0)
                //{

                gvInmuebles.DataSource = oFunciones.BindGrid((DataTable)ViewState["Inmueble"], SortDirection, SortExpression);
                gvInmuebles.DataBind();

                //}
                funcionJS = "$('#GrillaInmueble').show();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }
        }

        //private DataTable CreateTable()
        //{
        //    employeeTable.Columns.Add("Employee_ID", typeof(string));
        //}
        //protected void btnCargaInmuebles_Click(object sender, EventArgs e)
        //{
            //    string connectionString = "";
            //    BLInmueble oInmueble = new BLInmueble();
            //    int iCotizacion = 0;

            //    if (FileUpload1.HasFile)
            //    {
            //        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //        string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //        string fileLocation = Server.MapPath("~/Documentos/");// + fileName);
            //        FileUpload1.SaveAs(fileLocation + fileName);

            //        //Check whether file extension is xls or xslx
            //        if (fileExtension == ".xls")
            //        {
            //            connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + fileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            //        }
            //        else if (fileExtension == ".xlsx")
            //        {
            //            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            //        }
            //        else
            //        {
            //            Alerta("Formatos soportados XLS y XLSX, favor revise el archivo", 4);
            //        }
            //        //Create OleDB Connection and OleDb Command
            //        OleDbConnection con = new OleDbConnection(connectionString);
            //        OleDbCommand cmd = new OleDbCommand();
            //        cmd.CommandType = System.Data.CommandType.Text;
            //        cmd.Connection = con;
            //        OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
            //        DataTable dtExcelRecords = new DataTable();
            //        try
            //        {
            //            con.Open();
            //            DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //            string getExcelSheetName = "";
            //            foreach (DataRow item in dtExcelSheetName.Rows)
            //            {
            //                if (item["TABLE_NAME"].ToString() == "Inmuebles$")
            //                {
            //                    //getExcelSheetName = dtExcelSheetName.Rows[2]["Table_Name"].ToString();
            //                    getExcelSheetName = item["TABLE_NAME"].ToString();
            //                    break;
            //                }
            //                else
            //                {
            //                    Alerta("No existe la pestaña Inmueble en la planilla", 3);
            //                    return;
            //                }
            //            }

            //            cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
            //            dAdapter.SelectCommand = cmd;
            //            dAdapter.Fill(dtExcelRecords);
            //            con.Close();
            //        }
            //        catch (Exception ex)
            //        {
            //            Alerta(ex.Message, 1);
            //        }
            //        if (dtExcelRecords.Columns.Count != 14)
            //        {
            //            Alerta("Excel debe tener 14 columnas con los siguientes nombres \" IdInmueble, Piso, Ndepto, Edificio, Modelo, Orientacion, DeptoUtil, Balcon, Logia, PrecioLista, Observacion, EstadoInmueble, IdPack, usogoce \"", 1);
            //            return;
            //        }

            //        try
            //        {
            //            iCotizacion = oInmueble.ConsultaCotizacionProyecto(Convert.ToInt32(ddlProyecto.SelectedValue));
            //        }
            //        catch (Exception ex)
            //        {
            //            Alerta(ex.Message, 1);
            //        }
            //        if (iCotizacion > 0)
            //        {
            //            Alerta("El proyecto contiene inmueble con cotizaciones, no se puede cargar mas inmuebles", 3);
            //            return;
            //        }

            //        try
            //        {
            //            oInmueble.IngresaInmueble(Convert.ToInt32(ddlProyecto.SelectedValue), dtExcelRecords);
            //            Alerta("Inmuebles ingresados de forma correcta", 3);
            //        }
            //        catch (Exception ex)
            //        {
            //            Alerta(ex.Message, 1);
            //            return;
            //        }

            //        llenaTorres(Convert.ToInt32(ddlProyecto.SelectedValue));
            //    }
            //    else
            //    {
            //        Alerta("Seleccione un archivo para subir", 4);
            //    }
        //}

        protected void gvInmuebles_Sorting(object sender, GridViewSortEventArgs e)
        {
            //-----se guarda el estado actual de los check de la página en curso.----------
            if (!CheckBoxAllCabecera)
            {
                ProductsSelectionManager.KeepSelection((GridView)sender);
            }
            //---------------------------------------------------------------
            Funciones ofunciones = new Funciones();
            DataTable dtOrden = new DataTable();

            SortExpression = e.SortExpression;
            SortDirection = SortDirection == "ASC" ? "DESC" : "ASC";

            dtOrden = ofunciones.BindGrid((DataTable)ViewState["Inmueble"], SortDirection, e.SortExpression);

            ViewState["Inmueble"] = dtOrden;
            gvInmuebles.DataSource = dtOrden;
            gvInmuebles.DataBind();

            //se restablece las marcas que pudiera haber para la misma.
            if (!CheckBoxAllCabecera)
            {
                ProductsSelectionManager.RestoreSelection((GridView)sender);
            }
        }

        protected void gvInmuebles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string hex = "#B13261";
            System.Drawing.Color _color;
            LinkButton lnkbtn;
            CheckBox chklnk;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Controls.Count > 0)
                    {
                        if (cell.Controls.Count == 3)
                        {
                            //var a = cell.Controls[2].Controls.GetType();
                            chklnk = cell.Controls[1] as CheckBox;
                        }
                        else
                        {
                            lnkbtn = cell.Controls[0] as LinkButton;
                            if (!string.IsNullOrEmpty(lnkbtn.CommandArgument))
                            {
                                if (lnkbtn.CommandArgument == SortExpression)
                                {
                                    _color = System.Drawing.ColorTranslator.FromHtml(hex);
                                    cell.BackColor = _color;
                                    lnkbtn.BackColor = _color;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void gvInmuebles_DataBound(object sender, EventArgs e)
        {
            int sortedColumnPosition = 0;
            LinkButton lnkbtn;
            CheckBox chklnk;
            string hex = "#E9E7E2";
            System.Drawing.Color _color;

            if (gvInmuebles.HeaderRow == null)
                return;
            foreach (TableCell cell in gvInmuebles.HeaderRow.Cells)
            {
                if (cell.Controls.Count > 0)
                {
                    if (cell.Controls.Count == 3)
                    {
                        chklnk = cell.Controls[1] as CheckBox;
                    }
                    else
                    {
                        lnkbtn = (LinkButton)cell.Controls[0];
                        if (lnkbtn.CommandArgument == SortExpression)
                        {
                            break;
                        }
                    }

                }
                sortedColumnPosition++;
            }
            if (!string.IsNullOrEmpty(SortExpression))
            {
                foreach (GridViewRow row in gvInmuebles.Rows)
                {
                    if (sortedColumnPosition < 16)
                    {
                        _color = System.Drawing.ColorTranslator.FromHtml(hex);
                        row.Cells[sortedColumnPosition].BackColor = _color;
                    }
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }
        protected void btnEditarInmueble_Click(object sender, EventArgs e)
        {
            Session["Proyecto"] = ddlProyecto.SelectedValue;
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            HiddenField theHiddenField = gvrow.FindControl("HiddenFieldDifferentUsers") as HiddenField;
            Response.Redirect("frmInmuebleDetalleNew.aspx?IdInmueble=" + theHiddenField.Value);
        }

        protected void gvInmuebles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //-----se guarda el estado actual de los check de la página en curso.----------
            if (!CheckBoxAllCabecera)
            {
                ProductsSelectionManager.KeepSelection((GridView)sender);
            }
            //---------------------------------------------------------------

            gvInmuebles.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["Inmueble"];
            dt.DefaultView.Sort = SortExpression + " " + this.SortDirection;

            gvInmuebles.DataSource = dt;

            gvInmuebles.DataBind();

            verificaEstadoCheckBoxAll();
            marcaDesmarcaCheckBoxAll();
        }

        protected void gvProducts_PageIndexChanged(object sender, EventArgs e)
        {
            //se restablece las marcas que pudiera haber para la misma.
            if (!CheckBoxAllCabecera)
            {
                ProductsSelectionManager.RestoreSelection((GridView)sender);
            }
        }

        protected void ddlProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaTorres(Convert.ToInt32(ddlProyecto.SelectedValue));
        }

        protected void btnNuevoInmueble_Click(object sender, EventArgs e)
        {
            Session["Proyecto"] = ddlProyecto.SelectedValue;
            Response.Redirect("frmInmuebleDetalleNew.aspx");
        }

        protected void btnEliminarInmueble_Click(object sender, EventArgs e)
        {
            BLInmueble oInmueble = new BLInmueble();
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            HiddenField theHiddenField = gvrow.FindControl("HiddenFieldDifferentUsers") as HiddenField;
            Funciones oFunciones = new Funciones();
            DataSet dsInmueble;
            string funcionJS;
            int ndepto = txtDepto.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(txtDepto.Text.Trim());
            try
            {
                oInmueble.EliminarInmueble(Convert.ToInt32(theHiddenField.Value));
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
            }
            try
            {
                //dsInmueble = oInmueble.ListaInmueble(Convert.ToInt32(ddlProyecto.SelectedValue), Convert.ToInt32(ddlTipoInmueble.SelectedValue), ddlTorre.SelectedValue, ndepto, Convert.ToInt32(ddlModeloInmueble.SelectedValue), Convert.ToInt32(txtPiso.Text));
                dsInmueble = oInmueble.ListaInmueble2(Convert.ToInt32(ddlProyecto.SelectedValue), Convert.ToInt32(ddlTipoInmueble.SelectedValue), ddlTorre.SelectedValue, ndepto, Convert.ToInt32(ddlModeloInmueble.SelectedValue), Convert.ToInt32(txtPiso.Text), Convert.ToInt32(ddlOrientacion.SelectedValue), Convert.ToInt32(ddlEstadoInmueble.SelectedValue));
                ViewState["Inmueble"] = dsInmueble.Tables[0];

                SortExpression = "Descripcion";

                // if (dsInmueble.Tables[0].Rows.Count > 0)
                //{

                gvInmuebles.DataSource = oFunciones.BindGrid((DataTable)ViewState["Inmueble"], SortDirection, SortExpression);
                gvInmuebles.DataBind();

                //}
                funcionJS = "$('#GrillaInmueble').show();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }
        }

        protected void btnAsociar_Click(object sender, EventArgs e)
        {
            BLInmueble oInmueble = new BLInmueble();
            int iIdProyecto;
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            TextBox txtPack = gvrow.FindControl("txtInmueblePack") as TextBox;
            HiddenField theHiddenField = gvrow.FindControl("HiddenFieldDifferentUsers") as HiddenField;
            if (txtPack.Text.Length == 0)
            {
                Alerta("Ingrese un id Inmueble", 3);
                return;
            }
            try
            {
                iIdProyecto = oInmueble.ValidaInmueble(Convert.ToInt32(txtPack.Text));
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }

            if (iIdProyecto != Convert.ToInt32(ddlProyecto.SelectedValue))
            {
                Alerta("El inmueble ingresado no pertene al proyecto", 3);
                return;
            }

            try
            {
                oInmueble.UpdateInmueblePack(Convert.ToInt32(theHiddenField.Value), Convert.ToInt32(txtPack.Text));
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }
        }

        //Se ejecuta cada vez que se presiona el CheckBox de la cabecera
        protected void checkTodos_CheckedChanged(object sender, EventArgs e)
        {
            cambiaEstadoVariableCheckBoxAll();
            marcaDesmarcaCheckBoxAll();
            guardaDatosEnMemoria();
        }


        //Se ejecuta cada vez que se presiona un CheckBox dentro de la grilla
        protected void checkListado_CheckedChanged(object sender, EventArgs e)
        {
            var SelectListCheck = (List<int>)HttpContext.Current.Session["ProdSelection"] ?? new List<int>();
            //--Si está seleccionado el CheckBox de cabecera, hay que verificar si se está
            //marcando o desmarcando un 
            if (CheckBoxAllCabecera)
            {
                //Hay que cambiar el estado de la variable checkBoxAll de la cabecera
                cambiaEstadoVariableCheckBoxAll();
                //Hay que marcar o desmarcar el checkBox de la cabecera comparando las listas
                verificaEstadoCheckBoxAll();

                //Hay que conservar la lista total y quitar el item desmarcado
                foreach (GridViewRow row in gvInmuebles.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("ChkEdicion");
                    int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
                    if (chckrw.Checked && !SelectListCheck.Contains(valor))
                    {
                        //int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
                        SelectListCheck.Add(valor);
                    }
                    else if(!chckrw.Checked && SelectListCheck.Contains(valor))
                    {
                        //int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
                        SelectListCheck.RemoveAll(s => s == valor);
                    }
                }

            }
            else
            {
                //Hay que conservar la lista total y quitar el item desmarcado
                foreach (GridViewRow row in gvInmuebles.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("ChkEdicion");
                    int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
                    if (chckrw.Checked && !SelectListCheck.Contains(valor))
                    {
                        //int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
                        SelectListCheck.Add(valor);
                    }
                    else if (!chckrw.Checked && SelectListCheck.Contains(valor))
                    {
                        //int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
                        SelectListCheck.RemoveAll(s => s == valor);
                    }
                }

                HttpContext.Current.Session["ProdSelection"] = SelectListCheck;

                //Hay que marcar o desmarcar el checkBox de la cabecera comparando las listas
                DataTable dt = (DataTable)ViewState["Inmueble"];
                int totalRows = dt.Rows.Count;
                if (totalRows == SelectListCheck.Count)
                {
                    cambiaEstadoVariableCheckBoxAll();
                    verificaEstadoCheckBoxAll();
                }
                else if (SelectListCheck.Count == 0)
                {
                    LimpiaListaSeleccion();
                }
            }

            //-----se guarda el estado actual de los check de la pagina en curso.----------
            if (!CheckBoxAllCabecera)
            {
                //ProductsSelectionManager.KeepSelection((GridView)sender);
                ProductsSelectionManager.KeepSelection(gvInmuebles);
            }
            //---------------------------------------------------------------
        }

        //Marca o desmarca los CheckBox dentro de la grilla
        public void marcaDesmarcaCheckBoxAll()
        {
            foreach (GridViewRow row in gvInmuebles.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("ChkEdicion");
                if (CheckBoxAllCabecera == true)
                {
                    chckrw.Checked = true;

                }
                else
                {
                    chckrw.Checked = false;
                }
            }
        }

        public void guardaDatosEnMemoria()
        {
            List<int> checkedProd = new List<int>();
            if (CheckBoxAllCabecera == false)
            {
                HttpContext.Current.Session["ProdSelection"] = null;
            }
            else
            {
                DataTable dt = (DataTable)ViewState["Inmueble"];
                foreach (DataRow item in dt.Rows)
                {
                    checkedProd.Add(Convert.ToInt32(item[0]));
                }
                HttpContext.Current.Session["ProdSelection"] = checkedProd;
            }
        }

        //Verifica si está chequeado el CheckAll de la cabecera, para marcarlo al presionar una nueva página
        public void verificaEstadoCheckBoxAll() 
        {
            CheckBox chckheader = (CheckBox)gvInmuebles.HeaderRow.FindControl("checkAll");
            if (CheckBoxAllCabecera == false)
            {
                chckheader.Checked = false;
            }
            else
            {
                chckheader.Checked = true;
            }
        }

        protected void btnEdicionMasiva_Click(object sender, EventArgs e)
        {
            Session["nombreProyecto"] = ddlProyecto.SelectedItem.Text;

            List<int> list1 = (List<int>)HttpContext.Current.Session["ProdSelection"];

            if (list1 == null)
            {
                list1 = new List<int>();
            }

            //Recorrer tabla identificando los items de la lista
            //Verificar el campo IdEstado, verificar que todos sean disponibles
            List<Inmueble> list2 = new List<Inmueble>();

            DataTable dt = (DataTable)ViewState["Inmueble"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                foreach (var item in list1)
                {
                    if (item == Convert.ToInt32(dt.Rows[i]["IdInmueble"]))
                    {
                        Inmueble list3 = new Inmueble();
                        list3.IdInmueble = Convert.ToInt32(dt.Rows[i]["IdInmueble"].ToString());
                        list3.IdEstadoInmueble = Convert.ToInt32(dt.Rows[i]["IdEstado"]);
                        //list3.Terraza = Convert.ToDouble(dt.Rows[i]["M2Terreno"]);
                        //list3.M2Util = Convert.ToDouble(dt.Rows[i]["M2"]);
                        list3.TerrazaPrev = dt.Rows[i]["M2Terreno"].ToString();
                        list3.M2UtilPrev = dt.Rows[i]["M2"].ToString();
                        list3.Piso = Convert.ToInt32(dt.Rows[i]["Piso"].ToString());
                        list3.EstadoInmueble = dt.Rows[i]["Estado"].ToString();
                        list3.JustificacionEstadoInmueble = dt.Rows[i]["JustificacionEstadoInmueble"].ToString();
                        list3.PrecioLista = Convert.ToInt32(dt.Rows[i]["PrecioLista"]);
                        list3.NumeroRol = dt.Rows[i]["NumeroRol"].ToString();
                        list3.Alicuota = dt.Rows[i]["Alicuota"].ToString();
                        list3.NDepto = Convert.ToInt32(dt.Rows[i]["Numero"].ToString());
                        list3.ModeloInmueble = dt.Rows[i]["modeloInmueble"].ToString();

                        list2.Add(list3);
                    }
                }
            }

            Session["DatosSeleccionados"] = ConvertToDataTable(list2);

            Session["ListadoGrillaActual"] = list2;
            

            if (list1.Count == 0)
            {
                Alerta("Debe buscar inmuebles y seleccionar al menos un ítems dentro de la tabla de resultados.", 4);
            }
            else
            {
                Response.Redirect("frmInmueblesMasivo.aspx");
            }   
        }

        //Cambia el estado del CheckBox que se encuentra en cabecera de la grilla
        private void cambiaEstadoVariableCheckBoxAll()
        {
            if (CheckBoxAllCabecera == false)
            {
                CheckBoxAllCabecera = true;
            }
            else
            {
                CheckBoxAllCabecera = false;
            }
        }

        private void LimpiaListaSeleccion()
        {
            HttpContext.Current.Session["ProdSelection"] = null;
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

    }
}