using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaestraNet.Data;
using System.Data;
using MaestraNet.Entidad;
using MaestraNet.Util;
using System.IO;

namespace MaestraNet.GC.SVTA.Mantenedor
{
    public partial class frmTipoInmuebleDetalle : System.Web.UI.Page
    {
        BLProyecto oProyecto = new BLProyecto();
        BLInmueble oInmueble = new BLInmueble();
        //string sIdProyecto;
        string sIdTipoInmueble;
        private void Acceso()
        {
            BLPerfiles oAcceso = new BLPerfiles();
            Funciones oFunc = new Funciones();
            DataSet dsAcceso = new DataSet();
            string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);

            try
            {
                dsAcceso = oAcceso.PerfilBotones(Session["IdPerfil"].ToString(), pageName);
            }
            catch (Exception ex)
            {
                this.Alerta(ex.Message, 1);
            }
            oFunc.DisableControls(Page, false);

            if (dsAcceso.Tables.Count > 0)
            {
                foreach (DataRow oRow in dsAcceso.Tables[0].Rows)
                {
                    oFunc.FindControlRecursive(Page, oRow["idboton"].ToString());
                }
            }

        }
        private void Alerta(string msg, int tipo)
        {
            string funcionJS = "";
            if (tipo == 1) //ERROR
            {
                lblAlertaMSGError.Text = msg;
                funcionJS = "$( document ).ready(function() {showAlertaError();});";
            }
            if (tipo == 2) // CONFIRMACION
            {
                //lblAlertaMSGConfirmar.Text = msg;
                funcionJS = "$( document ).ready(function() {showAlertaConfirmar();});";
            }
            if (tipo == 3) // INFORMACION
            {
                lblAlertaMSGInfo.Text = msg;
                funcionJS = "$( document ).ready(function() {showAlertaInformar();});";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        }
        protected bool cmdSgte_Click()
        {


            cs.Utiles u = new cs.Utiles();

            //if (txtPaterno.Text.Trim().Length < 2)
            //{
            //    Alerta("Ingrese el Apellido",3);
            //    txtPaterno.Focus();
            //    return false;
            //}

            //if (ddlSalaVenta.SelectedValue == "0")
            //{
            //    Alerta("Seleccion una sala de venta", 3);
            //    ddlSalaVenta.Focus();
            //    return false;
            //}

            //if (ddlRegion.SelectedValue == "0")
            //{
            //    Alerta("Seleccione una región", 3);
            //    ddlRegion.Focus();
            //    return false;
            //}

            //if (ddlComuna.SelectedValue == "0")
            //{
            //    Alerta("Seleccione una comuna", 3);
            //    ddlComuna.Focus();
            //    return false;
            //}

            //if (ddlEmpresa.SelectedValue == "0")
            //{
            //    Alerta("Seleccion una empresa", 3);
            //    ddlEmpresa.Focus();
            //    return false;
            //}
            //if (ddlDivision.SelectedValue == "0")
            //{
            //    Alerta("Seleccion Division", 3);
            //    ddlEmpresa.Focus();
            //    return false;
            //}

            //if (ddlEstadoEntrega.SelectedValue == "0")
            //{
            //    Alerta("Seleccione una estado de entrega", 3);
            //    ddlEstadoEntrega.Focus();
            //    return false;
            //}

            //if (txtProyecto.Text.Trim().Length < 5)
            //{
            //    Alerta("Ingrese nombre del proyecto", 3);
            //    txtProyecto.Focus();
            //    return false;
            //}


            //if (!txtEmail.Text.Contains("@"))
            //{
            //    Alerta("Ingrese un correo valido", 3);
            //    txtEmail.Focus();
            //    return false;
            //}

            //if (txtDireccion.Text.Trim().Length < 5)
            //{
            //    Alerta("Ingrese direccion del proyecto", 3);
            //    txtDireccion.Focus();
            //    return false;
            //}
            //DateTime dt;
            //if (!DateTime.TryParseExact(txtFechaVenta.Value, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dt))
            //{
            //    Alerta("Ingrese una fecha de inicio valida", 3);
            //    txtFechaVenta.Focus();
            //    return false;
            //}
            //if (!DateTime.TryParseExact(txtFechaRecepcion.Value, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dt))
            //{
            //    Alerta("Ingrese una fecha de recepcion", 3);
            //    txtFechaRecepcion.Focus();
            //    return false;
            //}
            //double ent;
            //if (!double.TryParse(txtM2.Text, out ent))
            //{

            //    Alerta("Ingrese metros cuadrados", 3);
            //    txtM2.Focus();
            //    return false;

            //}
            //if (txtM2.Text.Contains("."))
            //{
            //    Alerta("Ingrese metros cuadrados", 3);
            //    txtM2.Focus();
            //    return false;
            //}

            //if (!double.TryParse(txtValorTerreno.Text, out ent))
            //{

            //    Alerta("Ingrese el valor del terreno", 3);
            //    txtValorTerreno.Focus();
            //    return false;

            //}

            //if (txtLatitud.Text.Trim().Length == 0 || txtLongitud.Text.Trim().Length == 0)
            //{
            //    Alerta("Busque la dirección en el mapa para guardar las coordenadas", 3);
            //    txtDireccion.Focus();
            //    return false;
            //}
            //if (Convert.ToDateTime(txtFechaVenta.Value) >= Convert.ToDateTime(txtFechaRecepcion.Value))
            //{
            //    Alerta("La fecha inicio de venta es mayor que fecha de recepción", 3);
            //    txtDireccion.Focus();
            //    return false;
            //}
            return true;
        }
        private void llenaDatosProyecto()
        {
            //DataTable dtProyecto;
            DataTable dtInmueble;

            //dtProyecto = oProyecto.ConsultaProyecto(Convert.ToInt32(lblIdProyecto.Text)).Tables[0];
            dtInmueble = oInmueble.ListaTipoInmueble(Convert.ToInt32(lblidTipoInmueble.Text)).Tables[0];

            if (dtInmueble.Rows.Count > 0)
            {
                //ddlSalaVenta.DataBind();
                //ddlSalaVenta.SelectedValue = dtProyecto.Rows[0]["IdSalaVenta"].ToString();
                //ddlRegion.DataBind();
                //ddlRegion.SelectedValue = dtProyecto.Rows[0]["idRegion"].ToString();
                //ddlComuna.DataBind();
                //ddlComuna.SelectedValue = dtProyecto.Rows[0]["idComuna"].ToString();
                //txtProyecto.Text = dtProyecto.Rows[0]["Nombre"].ToString();
                //ddlEmpresa.DataBind();
                //ddlEmpresa.SelectedValue = dtProyecto.Rows[0]["IdEmpresa"].ToString();
                //ddlDivision.DataBind();
                //ddlDivision.SelectedValue = dtProyecto.Rows[0]["CodigoDivision"].ToString();
                //txtEmail.Text = dtProyecto.Rows[0]["Email"].ToString();
                //txtDireccion.Text = dtProyecto.Rows[0]["Direccion"].ToString();
                //txtFechaVenta.Value = Convert.ToDateTime(dtProyecto.Rows[0]["FechaInicioVenta"].ToString()).ToString("yyyy-MM-dd");
                //txtFechaRecepcion.Value = Convert.ToDateTime(dtProyecto.Rows[0]["FechaRecepcion"].ToString()).ToString("yyyy-MM-dd");
                //txtM2.Text = dtProyecto.Rows[0]["M2"].ToString();
                //ddlEstadoEntrega.DataBind();
                //ddlEstadoEntrega.SelectedValue = dtProyecto.Rows[0]["IdEstadoEntrega"].ToString();
                //txtLatitud.Text = dtProyecto.Rows[0]["latitud"].ToString();
                //txtLongitud.Text = dtProyecto.Rows[0]["longitud"].ToString();
                //txtValorTerreno.Text = dtProyecto.Rows[0]["ValorTerreno"].ToString();


                txtTipoInmueble.Text = dtInmueble.Rows[0]["Nombre"].ToString();
                txtCdiId.Text = dtInmueble.Rows[0]["CdiId"].ToString();
                txtServicioId.Text = dtInmueble.Rows[0]["ServicioId"].ToString();

            }
        }

        private void GrabarProyecto()
        {
            //BLProyecto blProyecto = new BLProyecto();
            //Proyecto oProyecto = new Proyecto();
            BLInmueble BLInmueble = new BLInmueble();
            Inmueble oInmueble = new Inmueble();


            //oProyecto.IdProyecto = Convert.ToInt32(ViewState["IdProyecto"]);
            //oProyecto.IdSalaVenta = Convert.ToInt32(ddlSalaVenta.SelectedValue);
            //oProyecto.IdRegion = Convert.ToInt32(ddlRegion.SelectedValue);
            //oProyecto.IdComuna = Convert.ToInt32(ddlComuna.SelectedValue);
            //oProyecto.NombreProyecto = txtProyecto.Text;
            //oProyecto.IdEmpresa = Convert.ToInt32(ddlEmpresa.SelectedValue);
            //oProyecto.Email = txtEmail.Text;
            //oProyecto.Direccion = txtDireccion.Text;
            //oProyecto.FechaInicioVenta = Convert.ToDateTime(txtFechaVenta.Value);
            //oProyecto.FechaRecepcion = Convert.ToDateTime(txtFechaRecepcion.Value);
            //oProyecto.MetroCuadrados = Convert.ToDouble(txtM2.Text);
            //oProyecto.IdEstadoEntrega = Convert.ToInt32(ddlEstadoEntrega.SelectedValue);
            //oProyecto.Latitud = txtLatitud.Text;
            //oProyecto.Longitud = txtLongitud.Text;
            //oProyecto.CodigoDivision = Convert.ToInt32(ddlDivision.SelectedValue);
            //oProyecto.ValorTerreno = Convert.ToDouble(txtValorTerreno.Text);

            oInmueble.IdTipoInmueble = Convert.ToInt32(ViewState["sIdTipoInmueble"]);
            oInmueble.sNombreTipoInmueble = txtTipoInmueble.Text;
            //oInmueble.iCdiId = 
            //oInmueble.iServicioId =

            try
            {
                BLInmueble.IngresaTipoInmueble(oInmueble);
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
        private void llenaTorres(int idProyecto)
        {
            BLProyecto blProyecto = new BLProyecto();
            DataSet dsTorres;
            ListItem liElemento;
            string funcionJS;
            dsTorres = blProyecto.TorresProyecto(idProyecto);

            foreach (DataRow oRow in dsTorres.Tables[0].Rows)
            {
                liElemento = new ListItem();
                liElemento.Text = "Torre " + oRow[0];
                liElemento.Value = oRow[0].ToString();
                lstTorre.Items.Add(liElemento);
            }
            funcionJS = " $(function() { $('[id*=lstTorre]').multiselect({includeSelectAllOption: true });});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);


        }
        private string SeleccionaModeloInmueble()
        {
            string sInmueble = "";
            var items = from ListItem li in lstModeloInmueble.Items
                        where li.Selected == true
                        select li;

            foreach (ListItem li in items)
            {
                //selected item text and value.
                sInmueble += li.Value + ",";
            }
            if (sInmueble.Length > 0)
                sInmueble = sInmueble.Substring(0, sInmueble.Length - 1);
            else
                sInmueble = "0";

            return sInmueble;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            string funcionJS = "";

            if (!IsPostBack)
            {

                pnCambiaPrecio.Visible = false;
                map.Visible = true;
                //GMap1.Visible = true;
                sIdTipoInmueble = Request.QueryString["IdTipoInmueble"] == null ? "0" : Request.QueryString["IdTipoInmueble"];
                //ViewState["IdProyecto"] = sIdProyecto;
                //lblIdProyecto.Text = sIdProyecto;
                lblidTipoInmueble.Text = sIdTipoInmueble;
                //llenaTorres(Convert.ToInt32(sIdProyecto));
                //ViewState["Pisos"] = 0;
                //ViewState["Torres"] = 0;
                ViewState["sIdTipoInmueble"] = sIdTipoInmueble;

                //if (sIdProyecto == "0")
                //{
                //    lnkConfirmModificar.Text = "Crear Proyecto";
                //    funcionJS = "initMap();";
                //    Page.ClientScript.RegisterClientScriptBlock(GetType(), "ModalLib", funcionJS, true);
                //}
                //else
                //{
                //    lnkConfirmModificar.Text = "Modificar Proyecto";
                    llenaDatosProyecto();
                //    funcionJS = "$( document ).ready(function() {getmap();});";
                //    Page.ClientScript.RegisterClientScriptBlock(GetType(), "ModalLib", funcionJS, true);
                //}
                Acceso();
            }

        }



        protected void lnkVolver_Click(object sender, EventArgs e)
        {

            Response.Redirect("frmProyectos.aspx");
        }

        protected void lnkConfirmModificar_Click(object sender, EventArgs e)
        {
            string funcionJS = "";
            if (!cmdSgte_Click())
                return;

            GrabarProyecto();

            Alerta("Datos guardados de forma exitosa", 3);
            funcionJS = "$( document ).ready(function() {getmap();});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            lnkConfirmModificar.Enabled = false;
            //funcionJS = "$(function() { getmap(); });";
            //ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        }

        protected void lnkPrecios_Click(object sender, EventArgs e)
        {
            BLInmueble blProyecto = new BLInmueble();
            Inmueble oInmueble = new Inmueble();
            DataTable dtInmueble = new DataTable();
            DataRow drInmueble;
            string funcionJS;
            DataSet dsInmueble = new DataSet();

            dtInmueble.Columns.Add("idInmueble", typeof(int));
            dtInmueble.Columns.Add("PrecioLista", typeof(int));


            //dtInmueble = (DataTable)(gvInmueble.DataSource);
            foreach (GridViewRow r in gvInmueble.Rows)
            {
                TextBox txtPrecio = (TextBox)r.Cells[6].FindControl("txtNuevoPrecio");
                string sPrecioLista = r.Cells[5].Text;

                HiddenField txtidInmueble = (HiddenField)r.Cells[6].FindControl("idInmueble");

                if (Convert.ToInt32(txtPrecio.Text) <= 0)
                {
                    Alerta("Nuevo precio no puede ser 0 o negativo", 3);
                    return;
                }
                drInmueble = dtInmueble.NewRow();
                drInmueble["idInmueble"] = txtidInmueble.Value;
                drInmueble["PrecioLista"] = txtPrecio.Text;
                dtInmueble.Rows.Add(drInmueble);
            }
            if (dtInmueble.Rows.Count == 0)
            {
                Alerta("No existen registros para modificar", 3);
                return;
            }
            try
            {
                blProyecto.ModificaPreciosGrupo(dtInmueble, Session["IdUsuario"].ToString());
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }
            lblAlertaMSGInfo.Text = "Información grabada de forma exitosa";
            funcionJS = "$( document ).ready(function() {showAlertaInformar();});";
            funcionJS = " $(function() { $('[id*=lstPisos]').multiselect({includeSelectAllOption: true });}); $(function() { $('[id*=lstTorre]').multiselect({ includeSelectAllOption: true }); });$(function() { $('[id*=lstModeloInmueble]').multiselect({includeSelectAllOption: true });}); $( document ).ready(function() {showAlertaInformar();});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            lnkPrecios.Enabled = false;

            string sTorres = "";
            string spisos = "";
            int iDepto = 0;
            string sAumenta, sPorcentaje;
            int sValor;


            foreach (ListItem oItem in lstPisos.Items)
            {
                if (oItem.Selected)
                {
                    spisos = spisos + "'" + oItem.Value.Trim() + "',";
                }
            }
            if (spisos.Length > 0)
                spisos = spisos.Substring(0, spisos.Length - 1);
            else
                spisos = "0";

            ViewState["Pisos"] = spisos;


            foreach (ListItem oItem in lstTorre.Items)
            {
                if (oItem.Selected)
                {
                    sTorres = sTorres + "'" + oItem.Value.Trim() + "',";
                }
            }
            if (sTorres.Length > 0)
                sTorres = sTorres.Substring(0, sTorres.Length - 1);
            else
                sTorres = "0";



            ViewState["Torres"] = sTorres;
            iDepto = txtDepto.Text == "" ? 0 : Convert.ToInt32(txtDepto.Text);

            if (rbBaja.Checked)
                sAumenta = "B"; //BAJA
            else
                sAumenta = "S"; //SUBE
            if (rbPorcentaje.Checked)
                sPorcentaje = "PO"; //PORCENTAJE
            else
                sPorcentaje = "PR"; //PRECIO
            sValor = txtMonto.Text == "" ? 0 : Convert.ToInt32(txtMonto.Text);

            try
            {
                //dsInmueble = blProyecto.FiltraInmueble(Convert.ToInt32(ViewState["IdProyecto"]), SeleccionaModeloInmueble(), spisos, sTorres, ddlOrientacion.SelectedValue, iDepto, sAumenta, sPorcentaje, sValor);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
            }

            if (dsInmueble.Tables.Count > 0)
            {
                gvInmueble.DataSource = dsInmueble.Tables[0];
                gvInmueble.DataBind();
            }
            funcionJS = " $(function() { $('[id*=lstPisos]').multiselect({includeSelectAllOption: true });}); $(function() { $('[id*=lstTorre]').multiselect({ includeSelectAllOption: true }); });$(function() { $('[id*=lstModeloInmueble]').multiselect({includeSelectAllOption: true });});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);


        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //MultiView1.ActiveViewIndex = 0;
            string funcionJS;
            map.Visible = true;
            //GMap1.Visible = true;
            pnCambiaPrecio.Visible = false;
            LinkButton1.CssClass = "botonTabActive btn";
            LinkButton2.CssClass = "botonTab btn";
            funcionJS = "$( document ).ready(function() {getmap();});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            map.Visible = false;
            //GMap1.Visible = false;
            pnCambiaPrecio.Visible = true;
            LinkButton2.CssClass = "botonTabActive btn";
            LinkButton1.CssClass = "botonTab btn";
        }
        protected void LnkFiltrar_Click(object sender, EventArgs e)
        {
            BLInmueble blProyecto = new BLInmueble();
            Inmueble oInmueble = new Inmueble();
            DataSet dsInmueble = new DataSet();
            string sAumenta, sPorcentaje;
            int sValor;
            string sTorres = "";
            string spisos = "";
            int iDepto = 0;
            string funcionJS;

            foreach (ListItem oItem in lstPisos.Items)
            {
                if (oItem.Selected)
                {
                    spisos = spisos + "'" + oItem.Value.Trim() + "',";
                }
            }
            if (spisos.Length > 0)
                spisos = spisos.Substring(0, spisos.Length - 1);
            else
                spisos = "0";

            ViewState["Pisos"] = spisos;


            foreach (ListItem oItem in lstTorre.Items)
            {
                if (oItem.Selected)
                {
                    sTorres = sTorres + "'" + oItem.Value.Trim() + "',";
                }
            }
            if (sTorres.Length > 0)
                sTorres = sTorres.Substring(0, sTorres.Length - 1);
            else
                sTorres = "0";



            ViewState["Torres"] = sTorres;
            iDepto = txtDepto.Text == "" ? 0 : Convert.ToInt32(txtDepto.Text);

            if (rbBaja.Checked)
                sAumenta = "B"; //BAJA
            else
                sAumenta = "S"; //SUBE
            if (rbPorcentaje.Checked)
                sPorcentaje = "PO"; //PORCENTAJE
            else
                sPorcentaje = "PR"; //PRECIO
            sValor = txtMonto.Text == "" ? 0 : Convert.ToInt32(txtMonto.Text);

            try
            {
                //dsInmueble = blProyecto.FiltraInmueble(Convert.ToInt32(ViewState["IdProyecto"]), SeleccionaModeloInmueble(), spisos, sTorres, ddlOrientacion.SelectedValue, iDepto, sAumenta, sPorcentaje, sValor);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
            }

            if (dsInmueble.Tables.Count > 0)
            {
                gvInmueble.DataSource = dsInmueble.Tables[0];
                gvInmueble.DataBind();
            }
            funcionJS = " $(function() { $('[id*=lstPisos]').multiselect({includeSelectAllOption: true });}); $(function() { $('[id*=lstTorre]').multiselect({ includeSelectAllOption: true }); });$(function() { $('[id*=lstModeloInmueble]').multiselect({includeSelectAllOption: true });});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            lnkPrecios.Enabled = true;
        }

        //protected void ddlTipoInmueble_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BLInmueble blInmueble = new BLInmueble();
        //    DataSet dsInmueble;

        //    string funcionJS;
        //    funcionJS = " $(function() { $('[id*=lstPisos]').multiselect({includeSelectAllOption: true });}); $(function() { $('[id*=lstTorre]').multiselect({ includeSelectAllOption: true }); });$(function() { $('[id*=lstModeloInmueble]').multiselect({includeSelectAllOption: true });});";
        //    ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        //    lstModeloInmueble.SelectedIndex = -1;

        //    dsInmueble = blInmueble.ModeloInmueble(Convert.ToInt32(ddlTipoInmueble.SelectedValue));

        //    if (dsInmueble.Tables.Count > 0)
        //    {
        //        lstModeloInmueble.DataSource = dsInmueble.Tables[0];
        //        lstModeloInmueble.DataBind();
        //    }
        //}

        protected void lstModeloInmueble_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ListItem liElemento;
            string funcionJS;
            BLInmueble blProyecto = new BLInmueble();
            DataSet dsMinMax = new DataSet();
            string sInmuebles = "";

            var items = from ListItem li in lstModeloInmueble.Items
                        where li.Selected == true
                        select li;

            foreach (ListItem li in items)
            {
                //selected item text and value.
                sInmuebles += li.Value + ",";
            }
            if (sInmuebles.Length > 0)
                sInmuebles = sInmuebles.Substring(0, sInmuebles.Length - 1);
            else
                sInmuebles = "0";

            lstPisos.Items.Clear();
            dsMinMax = blProyecto.PisosInmueble(Convert.ToInt32(ViewState["IdProyecto"]), sInmuebles);

            foreach (DataRow oRow in dsMinMax.Tables[0].Rows)
            {
                liElemento = new ListItem();
                liElemento.Text = "Piso " + oRow[0];
                liElemento.Value = oRow[0].ToString();
                lstPisos.Items.Add(liElemento);
            }
            funcionJS = " $(function() { $('[id*=lstPisos]').multiselect({includeSelectAllOption: true });}); $(function() { $('[id*=lstTorre]').multiselect({ includeSelectAllOption: true }); });$(function() { $('[id*=lstModeloInmueble]').multiselect({includeSelectAllOption: true });});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BLProyecto blProyecto = new BLProyecto();
            //DataSet dsTorres;
            //dsTorres = blProyecto.DivisionEmpresa(Convert.ToInt32(ddlEmpresa.SelectedValue));
            //ddlDivision.DataSource = dsTorres.Tables[0];
            //ddlDivision.DataTextField = "DivGlosa";
            //ddlDivision.DataValueField = "DivCodigo";
            //ddlDivision.DataBind();
            //sdsDivision.DataBind();
            //ddlDivision.DataBind();
        }
    }
}