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
    public partial class frmInmuebleDetalleNew : System.Web.UI.Page
    {
        string sIdInmueble;
        BLInmueble oInmueble = new BLInmueble();
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
                txtOrientacion.Text = dtInmueble.Rows[0]["Orientacion"].ToString();
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
            if (ddlModeloInmueble.SelectedValue != "31" && ddlModeloInmueble.SelectedValue != "8" && ddlModeloInmueble.SelectedValue != "5" && ddlModeloInmueble.SelectedValue != "4" && txtOrientacion.Text.Trim().Length < 3)
            {
                Alerta("Ingrese orientación", 4);
                txtOrientacion.Focus();
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
            oInmueble.Orientacion = txtOrientacion.Text;
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
    }
}