using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MaestraNet.Entidad;
using MaestraNet.Data;
using MaestraNet.Util;

namespace MaestraNet.GC.SVTA.Mantenedor
{
    public partial class frmInmuebleDetalleNew : System.Web.UI.Page
    {
        string sIdInmueble;
        BLInmueble oInmueble = new BLInmueble();

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
        private void llenaDatosProyecto()
        {
            DataTable dtInmueble;
            DataTable dtlista;

            dtlista = oInmueble.ConsultaTipoOrientacion();

            dtInmueble = oInmueble.ConsultaInmueble(Convert.ToInt32(lblIdInmueble.Text)).Tables[0];
            if (dtInmueble.Rows.Count > 0)
            {
                ddlEstadoInmueble.DataBind();
                ddlEstadoInmueble.SelectedValue = dtInmueble.Rows[0]["idestado"].ToString();
                ddlModeloInmueble.DataBind();
                ddlModeloInmueble.SelectedValue = dtInmueble.Rows[0]["IdModeloInmueble"].ToString();
                txtPiso.Text = dtInmueble.Rows[0]["Piso"].ToString();
                txtEdificio.Text = dtInmueble.Rows[0]["Edificio"].ToString();
                txtObservacion.Text = dtInmueble.Rows[0]["Observacion"].ToString();
                txtM2Util.Text = dtInmueble.Rows[0]["M2"].ToString();
                txtTerraza.Text = dtInmueble.Rows[0]["M2Terreno"].ToString();
                txtNDepto.Text = dtInmueble.Rows[0]["Numero"].ToString();
                //txtOrientacion.Text = dtInmueble.Rows[0]["Orientacion"].ToString();
                if (dtInmueble.Rows[0]["Orientacion"].ToString().Trim() != "")
                {
                    string valorOrientacio1 = dtInmueble.Rows[0]["Orientacion"].ToString();
                    string valorOrientacio2 = valorOrientacio1.Substring(0, 1).ToUpper() + valorOrientacio1.Substring(1, valorOrientacio1.Length - 1).ToLower();

                    int contador = 0;
                    int i = 0;
                    string valueOrientacion = "0";
                    foreach (DataRow dataRow in dtlista.Rows)
                    {
                        string valorOrientacio3 = dtlista.Rows[i]["Orientacion"].ToString();
                        if (valorOrientacio3 == valorOrientacio2)
                        {
                            valueOrientacion = dtlista.Rows[i]["IdOrientacion"].ToString();
                            contador = 1;
                            break;
                        }
                        i++;
                    }

                    if (contador == 1)
                    {
                        ddlOrientacion.SelectedValue = valueOrientacion;
                    }
                    else
                    {
                        ddlOrientacion.SelectedValue = "0";
                    }
                }
                else
                {
                    ddlOrientacion.SelectedValue = "0.";
                }

                txtPrecioLista.Text = dtInmueble.Rows[0]["PrecioLista"].ToString();
                txtLogia.Text = dtInmueble.Rows[0]["Logia"].ToString();
                txtAlicuota.Text = dtInmueble.Rows[0]["Alicuota"].ToString();
                txtNumeroRol.Text = dtInmueble.Rows[0]["NumeroRol"].ToString();
            }
        }
        protected bool cmdSgte_Click()
        {

            if (ddlEstadoInmueble.SelectedValue == "-1")
            {
                Alerta("Seleccione estado del inmueble", 4);
                ddlEstadoInmueble.Focus();
                return false;
            }

            if (ddlModeloInmueble.SelectedValue == "0")
            {
                Alerta("Seleccione modelo inmueble", 4);
                ddlModeloInmueble.Focus();
                return false;
            }

            if (txtEdificio.Text.Trim().Length < 1)
            {
                Alerta("Ingrese edificio", 4);
                txtEdificio.Focus();
                return false;
            }

            double dSalida;
            if (!double.TryParse(txtLogia.Text == "" ? "0" : txtLogia.Text, out dSalida))
            {
                Alerta("Logia debe ser numero", 4);
                txtLogia.Focus();
                return false;
            }

            if (txtLogia.Text.Contains("."))
            {
                Alerta("Separador de decimal en logia debe ser \",\"", 4);
                txtLogia.Focus();
                return false;
            }

            if (!double.TryParse(txtM2Util.Text, out dSalida))
            {
                Alerta(" M2 util debe ser numero", 4);
                txtM2Util.Focus();
                return false;
            }
            if (txtM2Util.Text.Contains("."))
            {
                Alerta("Separador de decimal en M2 util debe ser \",\"", 4);
                txtM2Util.Focus();
                return false;
            }

            int iSalida;
            if (!int.TryParse(txtNDepto.Text, out iSalida))
            {

                Alerta("N° depto debe ser numérico", 4);
                txtNDepto.Focus();
                return false;

            }
            /* if (txtObservacion.Text.Trim().Length<5)
             {
                 Alerta("Ingrese Observación", 4);
                 txtObservacion.Focus();
                 return false;
             }*/

            //if (ddlModeloInmueble.SelectedValue != "31" && ddlModeloInmueble.SelectedValue != "8" && ddlModeloInmueble.SelectedValue != "5" && ddlModeloInmueble.SelectedValue != "4" && txtOrientacion.Text.Trim().Length < 3)
            //{
            //    Alerta("Ingrese orientación", 4);
            //    txtOrientacion.Focus();
            //    return false;
            //}
            if (ddlModeloInmueble.SelectedValue != "31" && ddlModeloInmueble.SelectedValue != "8" && ddlModeloInmueble.SelectedValue != "5" && ddlModeloInmueble.SelectedValue != "4" && ddlOrientacion.SelectedValue == "0")
            {
                Alerta("Seleccione orientación", 4);
                ddlOrientacion.Focus();
                return false;
            }

            if (!int.TryParse(txtPiso.Text, out iSalida))
            {

                Alerta("Piso debe ser numérico", 4);
                txtPiso.Focus();
                return false;

            }
            if (!int.TryParse(txtPrecioLista.Text, out iSalida))
            {

                Alerta("Precio de lista debe ser numérico", 4);
                txtPrecioLista.Focus();
                return false;

            }
            if (!double.TryParse(txtTerraza.Text, out dSalida))
            {

                Alerta("terraza debe ser numérico", 4);
                txtTerraza.Focus();
                return false;

            }
            if (txtTerraza.Text.Contains("."))
            {
                Alerta("Separador de decimal en terraza debe ser \",\"", 4);
                txtTerraza.Focus();
                return false;
            }
            return true;
        }
        private void GrabarInmueble()
        {
            BLInmueble blInmueble = new BLInmueble();
            Inmueble oInmueble = new Inmueble();

            oInmueble.IdProyecto = Convert.ToInt32(Session["Proyecto"].ToString());
            oInmueble.IdInmueble = Convert.ToInt32(ViewState["IdInmueble"]);
            oInmueble.IdModeloInmueble = Convert.ToInt32(ddlModeloInmueble.SelectedValue);
            oInmueble.IdEstadoInmueble = Convert.ToInt32(ddlEstadoInmueble.SelectedValue);
            oInmueble.M2Util = Convert.ToDouble(txtM2Util.Text);
            oInmueble.NDepto = Convert.ToInt32(txtNDepto.Text);
            oInmueble.Observacion = txtObservacion.Text;
            //oInmueble.Orientacion = txtOrientacion.Text;
            oInmueble.IdOrientacion = Convert.ToInt32(ddlOrientacion.SelectedValue);
            oInmueble.Piso = Convert.ToInt32(txtPiso.Text);
            oInmueble.PrecioLista = Convert.ToInt32(txtPrecioLista.Text);
            oInmueble.Terraza = Convert.ToDouble(txtTerraza.Text);
            oInmueble.Usuario = Session["IdUsuario"].ToString();
            oInmueble.Logia = Convert.ToDouble(string.IsNullOrEmpty(txtLogia.Text) ? "0" : txtLogia.Text);
            oInmueble.Edificio = txtEdificio.Text;
            oInmueble.Alicuota = txtAlicuota.Text;
            oInmueble.NumeroRol = txtNumeroRol.Text;


            try
            {
                blInmueble.ModificarInmueble(oInmueble);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;

            }
            finally
            {

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sIdInmueble = Request.QueryString["IdInmueble"] == null ? "0" : Request.QueryString["IdInmueble"];
                ViewState["IdInmueble"] = sIdInmueble;
                lblIdInmueble.Text = sIdInmueble;
                if (sIdInmueble == "0")
                    lnkConfirmModificar.Text = "Grabar Inmueble";
                else
                    lnkConfirmModificar.Text = "Modificar Inmueble";

                llenaDatosProyecto();
                ListaAsociacionInmuebleRol();

                //string[] datosBusqueda = Session["datosBusquedaInmueble"].ToString().Split(',');
            }
        }

        protected void lnkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmInmueblesNew.aspx");
        }

        protected void lnkConfirmModificar_Click(object sender, EventArgs e)
        {
            if (!cmdSgte_Click())
                return;

            GrabarInmueble();

            Alerta("Datos guardados de forma exitosa", 3);
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            string[] datosBusqueda = Session["datosBusquedaInmueble"].ToString().Split(',');

            int idProyecto = Convert.ToInt32(datosBusqueda[0].ToString());
            //int idTipoInmueble = Convert.ToInt32(datosBusqueda[1].ToString());
            string Edificio = datosBusqueda[2].ToString();
            int NDepto = Convert.ToInt32(datosBusqueda[3].ToString());
            //int IdModeloInmueble = Convert.ToInt32(datosBusqueda[4].ToString());
            int Piso = Convert.ToInt32(datosBusqueda[5].ToString());
            int IdOrientacion = Convert.ToInt32(datosBusqueda[6].ToString());


            DataSet dsInmueble;
            string funcionJS;
            BLInmueble oInmueble = new BLInmueble();
            Funciones oFunciones = new Funciones();
            //int ndepto = txtDepto.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(txtDepto.Text.Trim());
            //int iPiso;

            //HttpContext.Current.Session["ProdSelection"] = null;

            //if (ddlProyecto.SelectedValue == "0")
            //{
            //    Alerta("Seleccione un Proyecto", 3, true);
            //    ddlProyecto.Focus();
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtPiso.Text))
            //    iPiso = 0;
            //else
            //    iPiso = Convert.ToInt32(txtPiso.Text);

            try
            {
                dsInmueble = oInmueble.ListaInmueble2(idProyecto, Convert.ToInt32(ddlTipoInmueble.SelectedValue), Edificio, NDepto, 0, Piso, IdOrientacion);
                ////dsInmueble = oInmueble.ListaInmueble2(Convert.ToInt32(ddlProyecto.SelectedValue), Convert.ToInt32(ddlTipoInmueble.SelectedValue), ddlTorre.SelectedValue, ndepto, Convert.ToInt32(ddlModeloInmueble.SelectedValue), iPiso, Convert.ToInt32(ddlOrientacion.SelectedValue));
                ViewState["Inmueble"] = dsInmueble.Tables[0];

                SortExpression = "Descripcion";

                //// if (dsInmueble.Tables[0].Rows.Count > 0)
                ////{

                gvInmuebles.DataSource = oFunciones.BindGrid((DataTable)ViewState["Inmueble"], SortDirection, SortExpression);
                gvInmuebles.DataBind();

                ////}
                funcionJS = "$('#GrillaInmueble').show();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }
        }
        protected void gvInmuebles_DataBound(object sender, EventArgs e)
        {

        }
        protected void gvInmuebles_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvInmuebles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ////-----se guarda el estado actual de los check de la página en curso.----------
            //if (!CheckBoxAllCabecera)
            //{
            //    ProductsSelectionManager.KeepSelection((GridView)sender);
            //}
            ////---------------------------------------------------------------

            //gvInmuebles.PageIndex = e.NewPageIndex;
            //DataTable dt = (DataTable)ViewState["Inmueble"];
            //dt.DefaultView.Sort = SortExpression + " " + this.SortDirection;

            //gvInmuebles.DataSource = dt;

            //gvInmuebles.DataBind();

            //verificaEstadoCheckBoxAll();
            //marcaDesmarcaCheckBoxAll();
        }
        protected void gvProducts_PageIndexChanged(object sender, EventArgs e)
        {
            //se restablece las marcas que pudiera haber para la misma.
            //if (!CheckBoxAllCabecera)
            //{
            //    ProductsSelectionManager.RestoreSelection((GridView)sender);
            //}
        }

        //Se ejecuta cada vez que se presiona un CheckBox dentro de la grilla
        protected void checkListado_CheckedChanged(object sender, EventArgs e)
        {
            //var SelectListCheck = (List<int>)HttpContext.Current.Session["ProdSelection"] ?? new List<int>();
            ////--Si está seleccionado el CheckBox de cabecera, hay que verificar si se está
            ////marcando o desmarcando un 
            //if (CheckBoxAllCabecera)
            //{
            //    //Hay que cambiar el estado de la variable checkBoxAll de la cabecera
            //    cambiaEstadoVariableCheckBoxAll();
            //    //Hay que marcar o desmarcar el checkBox de la cabecera comparando las listas
            //    verificaEstadoCheckBoxAll();

            //    //Hay que conservar la lista total y quitar el item desmarcado
            //    foreach (GridViewRow row in gvInmuebles.Rows)
            //    {
            //        CheckBox chckrw = (CheckBox)row.FindControl("ChkEdicion");
            //        int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
            //        if (chckrw.Checked && !SelectListCheck.Contains(valor))
            //        {
            //            //int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
            //            SelectListCheck.Add(valor);
            //        }
            //        else if (!chckrw.Checked && SelectListCheck.Contains(valor))
            //        {
            //            //int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
            //            SelectListCheck.RemoveAll(s => s == valor);
            //        }
            //    }

            //}
            //else
            //{
            //    //Hay que conservar la lista total y quitar el item desmarcado
            //    foreach (GridViewRow row in gvInmuebles.Rows)
            //    {
            //        CheckBox chckrw = (CheckBox)row.FindControl("ChkEdicion");
            //        int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
            //        if (chckrw.Checked && !SelectListCheck.Contains(valor))
            //        {
            //            //int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
            //            SelectListCheck.Add(valor);
            //        }
            //        else if (!chckrw.Checked && SelectListCheck.Contains(valor))
            //        {
            //            //int valor = Convert.ToInt32(gvInmuebles.Rows[row.RowIndex].Cells[1].Text);
            //            SelectListCheck.RemoveAll(s => s == valor);
            //        }
            //    }

            //    HttpContext.Current.Session["ProdSelection"] = SelectListCheck;

            //    //Hay que marcar o desmarcar el checkBox de la cabecera comparando las listas
            //    DataTable dt = (DataTable)ViewState["Inmueble"];
            //    int totalRows = dt.Rows.Count;
            //    if (totalRows == SelectListCheck.Count)
            //    {
            //        cambiaEstadoVariableCheckBoxAll();
            //        verificaEstadoCheckBoxAll();
            //    }
            //    else if (SelectListCheck.Count == 0)
            //    {
            //        LimpiaListaSeleccion();
            //    }
            //}

            ////-----se guarda el estado actual de los check de la pagina en curso.----------
            //if (!CheckBoxAllCabecera)
            //{
            //    //ProductsSelectionManager.KeepSelection((GridView)sender);
            //    ProductsSelectionManager.KeepSelection(gvInmuebles);
            //}
            //---------------------------------------------------------------
        }

        protected void btnAsociar_Click(object sender, EventArgs e)
        {
            BLInmueble oInmueble = new BLInmueble();
            Inmueble inm = new Inmueble();
            int iIdProyecto;
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            //TextBox txtPack = gvInmuebles.FindControl("txtInmueblePack") as TextBox;
            HiddenField theHiddenField = gvrow.FindControl("HiddenFieldDifferentUsers") as HiddenField;

            //int idProyecto = Convert.ToInt32(datosBusqueda[0].ToString());
            if (txtNumeroRol.Text.Trim() == "")
            {
                Alerta("El inmueble no tiene Nro. de Rol", 3);
                return;
            }

            int idInmuebleA = Convert.ToInt32(lblIdInmueble.Text);
            string[] valores = theHiddenField.Value.ToString().Split('|');

            int idInmuebleB = Convert.ToInt32(valores[0]);
            string NroRolB = valores[1].ToString();
            string NroRolA = txtNumeroRol.Text;

            try
            {
                oInmueble.AsociaInmuebleRol(idInmuebleA, idInmuebleB, NroRolA, NroRolB, 1);
                //inm.IdInmueble = 123;

                //iIdProyecto = oInmueble.AsociaInmuebleRol(inm);
                ListaAsociacionInmuebleRol();

            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }
        }

        

        private void ListaAsociacionInmuebleRol()
        {
            try
            {
                DataSet dsInmueble;
                int idInmuebleA = Convert.ToInt32(lblIdInmueble.Text);
                //dsInmueble = oInmueble.ListaInmueble(Convert.ToInt32(ddlProyecto.SelectedValue), Convert.ToInt32(ddlTipoInmueble.SelectedValue), ddlTorre.SelectedValue, ndepto, Convert.ToInt32(ddlModeloInmueble.SelectedValue), Convert.ToInt32(txtPiso.Text));
                dsInmueble = oInmueble.ListaAsociaInmuebleRol(idInmuebleA);
                string funcionJS;
                //ListaAsociaInmuebleRol
                //ViewState["Inmueble"] = dsInmueble.Tables[0];

                SortExpression = "Descripcion";

                // if (dsInmueble.Tables[0].Rows.Count > 0)
                //{

                //gvInmuebles.DataSource = oFunciones.BindGrid((DataTable)ViewState["Inmueble"], SortDirection, SortExpression);
                gvAsociadoRol.DataSource = dsInmueble;
                gvAsociadoRol.DataBind();

                //}
                funcionJS = "$('#gvAsociadoRol').show();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }
        }

        protected void gvAsociadoRol_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvAsociadoRol_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvAsociadoRol_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ////-----se guarda el estado actual de los check de la página en curso.----------
            //if (!CheckBoxAllCabecera)
            //{
            //    ProductsSelectionManager.KeepSelection((GridView)sender);
            //}
            ////---------------------------------------------------------------

            //gvInmuebles.PageIndex = e.NewPageIndex;
            //DataTable dt = (DataTable)ViewState["Inmueble"];
            //dt.DefaultView.Sort = SortExpression + " " + this.SortDirection;

            //gvInmuebles.DataSource = dt;

            //gvInmuebles.DataBind();

            //verificaEstadoCheckBoxAll();
            //marcaDesmarcaCheckBoxAll();
        }
        protected void gvAsociadoRol_PageIndexChanged(object sender, EventArgs e)
        {
            //se restablece las marcas que pudiera haber para la misma.
            //if (!CheckBoxAllCabecera)
            //{
            //    ProductsSelectionManager.RestoreSelection((GridView)sender);
            //}
        }

        protected void btnEliminarInmueble_Click(object sender, EventArgs e)
        {
           BLInmueble oInmueble = new BLInmueble();
            Inmueble inm = new Inmueble();
            //int iIdProyecto;
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            HiddenField theHiddenField = gvrow.FindControl("HiddenFieldDifferentUsers") as HiddenField;

            string[] valores = theHiddenField.Value.ToString().Split('|');

            int idInmuebleA = Convert.ToInt32(valores[0]);
            int idInmuebleB = Convert.ToInt32(valores[1]);
            string NroRolA = valores[2].ToString();
            string NroRolB = valores[3].ToString();

            try
            {
                oInmueble.AsociaInmuebleRol(idInmuebleA, idInmuebleB, NroRolA, NroRolB, 0);
                //iIdProyecto = oInmueble.AsociaInmuebleRol(inm);
                ListaAsociacionInmuebleRol();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }
        }
    }
}