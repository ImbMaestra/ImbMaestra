using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MaestraNet.Entidad;
using MaestraNet.Data;

namespace MaestraNet.GC.SVTA.Mantenedor
{
    public partial class frmInmueblesMasivo : System.Web.UI.Page
    {

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
            if (Session["IdPerfil"] == null)
            {
                Response.Redirect("frmInmueblesNew.aspx");
            }

            if (!IsPostBack)
            {
                
                RdoPrecioLista1.Checked = true;
                txtProyecto.Text = Convert.ToString(Session["nombreProyecto"]);
                txtProyecto.Focus();

                if (HttpContext.Current.Session["ProdSelection"] != null)
                {
                    List<int> productsIdSel = HttpContext.Current.Session["ProdSelection"] as List<int>;
                    txtCantidad.Text = productsIdSel.Count.ToString();
                }
                //HabilitarJustificacion();
                HabilitaCampos();
                HabilitarJustificacion();
            }
        }

        protected void lnkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmInmueblesNew.aspx");
        }

        protected void lnkConfirmModificar_Click(object sender, EventArgs e)
        {
            if (ddlEstadoInmueble.SelectedValue == "1" && txtJustificacion.Text.Trim() == "")
            {
                Alerta("Dede ingresar una justificación para este Tipo de Inmueble", 4);
                return;
            }

            if (!cmdSgte_Click())
                return;

            ValidaTipoPrecioLista();

            GrabarInmueble();

            Alerta("Datos guardados de forma exitosa.", 3);
            LimpiarCampos();
            HabilitarJustificacion();
        }


        protected bool cmdSgte_Click()
        {
            if (txtM2Terraza.Text.Trim().Length < 1 
                && txtMetroUtil.Text.Trim().Length < 1 
                && txtPrecioLista.Text.Trim().Length < 1
                && ddlEstadoInmueble.SelectedValue == "-1"
                && txtJustificacion.Text.Trim().Length < 1
                && txtAlicuota.Text.Trim().Length < 1
                && txtNumeroRol.Text.Trim().Length < 1
                )
            {
                Alerta("Ingrese al menos un valor en alguno de los campos.", 4);
                txtM2Terraza.Focus();
                return false;
            }
            txtM2Terraza.Text = txtM2Terraza.Text.Replace(".", ",");
            txtMetroUtil.Text = txtMetroUtil.Text.Replace(".", ",");
            return true;
        }

        private void GrabarInmueble()
        {
            BLInmueble blInmueble = new BLInmueble();
            Inmueble oInmueble = new Inmueble();
            cs.dbTools db = new cs.dbTools();
            //lblUsuario.Text = db.Nombre_Usuario(Session["IdUsuario"].ToString());

            try
            {
                oInmueble.Terraza = Convert.ToDouble(txtM2Terraza.Text == "" ? "99999999" : txtM2Terraza.Text);
                oInmueble.M2Util = Convert.ToDouble(txtMetroUtil.Text == "" ? "0" : txtMetroUtil.Text);
                oInmueble.PrecioLista = Convert.ToInt32(txtPrecioLista.Text == "" ? "0" : txtPrecioLista.Text);
                oInmueble.TipoPrecioLista = hddTipoPrecioLista.Value;

                oInmueble.IdEstadoInmueble = Convert.ToInt32(ddlEstadoInmueble.Text);
                oInmueble.JustificacionTipoInmueble = txtJustificacion.Text.Trim();
                oInmueble.Alicuota = txtAlicuota.Text.Trim();
                oInmueble.NumeroRol = txtNumeroRol.Text.Trim();
                oInmueble.Usuario = db.Nombre_Usuario(Session["IdUsuario"].ToString());
                //oInmueble.Usuario = Session["IdUsuario"].ToString();

                List<int> dt = (List<int>)HttpContext.Current.Session["ProdSelection"];

            
                blInmueble.ModificarInmuebleMasivo(oInmueble, dt);
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

        private void LimpiarCampos()
        {
            txtM2Terraza.Text = "";
            txtMetroUtil.Text = "";
            RdoPrecioLista1.Checked = true;
            txtPrecioLista.Text = "";
            ddlEstadoInmueble.SelectedValue = "-1";
            txtJustificacion.Text = "";
            txtAlicuota.Text = "";
            txtNumeroRol.Text = "";
        }


        private void ValidaTipoPrecioLista()
        {
            if (RdoPrecioLista1.Checked)
            {
                hddTipoPrecioLista.Value = "0";//Total igual para todos
            }
            else if (RdoPrecioLista2.Checked)
            {
                hddTipoPrecioLista.Value = "1"; //Aumento %
            }
            else if (RdoPrecioLista3.Checked)
            {
                hddTipoPrecioLista.Value = "2"; //Aumento UF
            }
            else if (RdoPrecioLista4.Checked)
            {
                hddTipoPrecioLista.Value = "3"; //Disminuye %
            }
            else if (RdoPrecioLista5.Checked)
            {
                hddTipoPrecioLista.Value = "4"; //Disminuye UF
            }

        }

        protected void ddlEstadoInmueble_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarJustificacion();
        }

        private void HabilitarJustificacion()
        {
            if (ddlEstadoInmueble.SelectedValue == "0" || ddlEstadoInmueble.SelectedValue == "6")
            {
                txtPrecioLista.Enabled = true;
            }
            else if (ddlEstadoInmueble.SelectedValue == "1" || ddlEstadoInmueble.SelectedValue == "2" || ddlEstadoInmueble.SelectedValue == "3" || ddlEstadoInmueble.SelectedValue == "4" || ddlEstadoInmueble.SelectedValue == "5" || ddlEstadoInmueble.SelectedValue == "-1" || ddlEstadoInmueble.SelectedValue == "")
            {
                txtPrecioLista.Text = "";
                txtPrecioLista.Enabled = false;
            }
        }

        private void HabilitaCampos()
        {
            if (txtCantidad.Text == "1")
            {
                txtAlicuota.Enabled = true;
                txtNumeroRol.Enabled = true;
                
            }
            else
            {
                txtAlicuota.Enabled = false;
                txtNumeroRol.Enabled = false;
                txtAlicuota.Text = "";
                txtNumeroRol.Text = "";
            }
        }
    }
}