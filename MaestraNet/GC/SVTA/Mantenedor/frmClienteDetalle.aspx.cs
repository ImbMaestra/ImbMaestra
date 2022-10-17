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
using System.Diagnostics;

namespace MaestraNet.GC.SVTA.Mantenedor
{
    public partial class frmClienteDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["Carga_Ruta_WEB"].Equals("1")) { return; }
            try
            {
                if (!IsPostBack)
                {
                    MultiView1.ActiveViewIndex = 0;
                    lblIdCliente.Text = Request.QueryString["IdCliente"];
                    sIdCliente = Request.QueryString["IdCliente"] == null ? "0" : Request.QueryString["IdCliente"];
                    if (sIdCliente == "0")
                    {
                        txtRut.Enabled = true;
                        txtDigito.Enabled = true;
                    }
                    else
                    {
                        txtRut.Enabled = false;
                        txtDigito.Enabled = false;
                    }
                    ViewState["IdCliente"] = sIdCliente;
                    llenaDatosClientes();
                    llenaPatrimoniCliente();
                    LlenaExperienciaInversora();
                    Acceso();
                    lbPatrimonio.CssClass = "btn btn-secondary active border border-dark";
                    lbInversora.CssClass = "btn btn-secondary border border-dark";
                    lbPerfilInversor.CssClass = "btn btn-secondary border border-dark";
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

        BLcliente oCliente = new BLcliente();
        string sIdCliente;
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
                funcionJS = "showAlertaError();";
            }
            if (tipo == 2) // CONFIRMACION
            {
                //lblAlertaMSGConfirmar.Text = msg;
                funcionJS = "showAlertaConfirmar();";
            }
            if (tipo == 3) // INFORMACION
            {
                lblAlertaMSGInfo.Text = msg;
                funcionJS = "showAlertaInformar();";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        }
        protected bool cmdSgte_Click()
        {
            txtRut.Text = txtRut.Text.Replace(".", "");
            string srut = txtRut.Text + "-" + txtDigito.Text;
            cs.Utiles Util = new cs.Utiles();
            if (!Util.validarRut(srut))
            {
                Alerta("Verifique Rut", 1);
                return false;
            }

            //Validar Campos
            if (chkInversionista.Checked)
            {
                if (!chkEI_Ahorro.Checked && !chkEI_Herencia.Checked && !chkEI_Rescate.Checked && !chkEI_Venta.Checked)
                {
                    MultiView1.ActiveViewIndex = 1;
                    Alerta("Ingrese experiencia inversora", 3);
                    return false;

                }
                if (PI1.Checked && !PI2.Checked && !PI3.Checked && PI4.Checked)
                {
                    MultiView1.ActiveViewIndex = 1;
                    Alerta("Ingrese experiencia inversora", 3);
                    return false;
                }
            }
            if (rbPjuridica.Checked && ddlTipoOcupacion.SelectedValue != "3")
            {
                ddlTipoOcupacion.Focus();
                Alerta("Tipo Ocupacion debe ser socio empresa si es persona juridica", 3);
                return false;
            }

            if (txtNombres.Text.Trim().Length < 2)
            {
                Alerta("Ingrese el Nombre", 3);
                txtNombres.Focus();
                return false;
            }

            if (txtPaterno.Text.Trim().Length < 2)
            {
                Alerta("Ingrese el Apellido", 3);
                txtPaterno.Focus();
                return false;
            }

            if (txtEmail.Text.Trim().Length < 4)
            {
                Alerta("Ingrese un correo", 3);
                txtEmail.Focus();
                return false;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                Alerta("Ingrese un correo valido", 3);
                txtEmail.Focus();
                return false;
            }


            if (txtFono.Text.Trim().Length < 5)
            {
                Alerta("Ingrese el telefono del contacto", 3);
                txtFono.Focus();
                return false;
            }

            if (ddlTipoOcupacion.SelectedValue.Equals("0"))
            {
                Alerta("Seleccione el tipo de Ocupacion", 3);
                ddlTipoOcupacion.Focus();
                return false;
            }
            if (txtOcupacion.Text.Trim().Length < 2)
            {
                Alerta("Ingrese Ocupación", 3);
                txtOcupacion.Focus();
                return false;
            }
            if (TxtEmpresa.Text.Trim().Length < 2)
            {
                Alerta("Ingrese empresa", 3);
                TxtEmpresa.Focus();
                return false;
            }
            if (txtDireccionLaboral.Text.Trim().Length < 2)
            {
                Alerta("Ingrese direccion laboral", 3);
                txtDireccionLaboral.Focus();
                return false;
            }
            if (txtFonoEmpresa.Text.Trim().Length < 5)
            {
                Alerta("Ingrese Fono laboral", 3);
                txtFonoEmpresa.Focus();
                return false;
            }
            if (ddlNacionalidad.SelectedValue == "0")
            {
                Alerta("Ingrese Nacionalidad", 3);
                ddlNacionalidad.Focus();
                return false;
            }
            if (ddlEstadoCivil.SelectedValue == "0")
            {
                Alerta("Ingrese Estado Civil", 3);
                ddlNacionalidad.Focus();
                return false;
            }
            if (rbPnatural.Checked && txtFechaNacimiento.Value.ToString().Length < 2)
            {
                Alerta("Ingrese la fecha de nacimiento", 3);
                txtFechaNacimiento.Focus();
                return false;
            }
            return true;
        }
        private void llenaDatosClientes()
        {
            try
            {
                DataTable dtCliente = new DataTable();
                dtCliente = oCliente.ConsultaCliente(Convert.ToInt32(lblIdCliente.Text)).Tables[0];

                if (dtCliente.Rows.Count > 0)
                {

                    txtRut.Text = dtCliente.Rows[0]["EntRut"].ToString().Split('-')[0];
                    txtDigito.Text = dtCliente.Rows[0]["EntRut"].ToString().Split('-')[1];
                    txtNombres.Text = dtCliente.Rows[0]["EntConNombres"].ToString();
                    txtPaterno.Text = dtCliente.Rows[0]["EntConApePaterno"].ToString();
                    txtMaterno.Text = dtCliente.Rows[0]["EntConApeMaterno"].ToString();
                    txtEmail.Text = dtCliente.Rows[0]["EntConMail"].ToString();
                    if (dtCliente.Rows[0]["Inversionista"].ToString() != "1")
                        chkInversionista.Checked = false;
                    else
                        chkInversionista.Checked = true;

                    ddlNacionalidad.SelectedValue = dtCliente.Rows[0]["IdNacionalidad"].ToString();
                    ddlEstadoCivil.SelectedValue = dtCliente.Rows[0]["IdEstadoCivil"].ToString();
                    ddlTipoOcupacion.SelectedValue = dtCliente.Rows[0]["IdTipoOcupacion"].ToString().Length == 0 ? "0" : dtCliente.Rows[0]["IdTipoOcupacion"].ToString();
                    if (dtCliente.Rows[0]["sexo"].ToString().ToCharArray()[0] == 'M')
                    {
                        rbMasculino.Checked = true;
                        rbFemenino.Checked = false;
                    }
                    else
                    {
                        rbFemenino.Checked = true;
                        rbMasculino.Checked = false;
                    }
                    if (dtCliente.Rows[0]["PersonaJuridica"].ToString() != "1")
                        rbPnatural.Checked = true;
                    else
                        rbPjuridica.Checked = true;
                    ddlCiudad.DataBind();

                    ddlCiudad.SelectedValue = dtCliente.Rows[0]["CiuCodigo"].ToString();

                    SqlDataSource2.SelectParameters[0].DefaultValue = dtCliente.Rows[0]["CiuCodigo"].ToString();
                    ddpComunas.DataBind();

                    ddpComunas.SelectedValue = dtCliente.Rows[0]["CmuCodigo"].ToString();
                    txtOcupacion.Text = dtCliente.Rows[0]["Ocupacion"].ToString();
                    TxtEmpresa.Text = dtCliente.Rows[0]["NombreEmpresa"].ToString();
                    txtDireccionLaboral.Text = dtCliente.Rows[0]["DireccionEmpresa"].ToString();
                    txtFonoEmpresa.Text = dtCliente.Rows[0]["TelefonoEmpresa"].ToString();
                    txtNombreContacto.Text = dtCliente.Rows[0]["NombreContacto"].ToString();
                    txtFonoContacto.Text = dtCliente.Rows[0]["TelefonoContacto"].ToString();
                    txtDireccion.Text = dtCliente.Rows[0]["EntDirDireccion"].ToString();
                    txtFono.Text = dtCliente.Rows[0]["EntDirFono"].ToString();
                    txtFechaNacimiento.Value = Convert.ToDateTime(dtCliente.Rows[0]["FechaNacimiento"].ToString()).ToString("yyyy-MM-dd");
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

        protected void R_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbPjuridica.Checked)
                {
                    DivFecNac.Visible = false;
                }
                else
                {
                    DivFecNac.Visible = true;
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

        private void llenaPatrimoniCliente()
        {
            try
            {
                DataTable dtCliente = new DataTable();
                dtCliente = oCliente.ConsultaPatrimonio(Convert.ToInt32(lblIdCliente.Text)).Tables[0];
                if (dtCliente.Rows.Count > 0)
                {

                    int ahorro = int.Parse(dtCliente.Rows[0]["Libreta"].ToString());
                    if (ahorro > 0) { chkAhorro.Checked = true; }
                    int indemizacion = int.Parse(dtCliente.Rows[0]["Indemizacion"].ToString());
                    if (indemizacion > 0) { chkIndemnizacion.Checked = true; }
                    int ventaBienes = int.Parse(dtCliente.Rows[0]["Ventas_Bienes_Raices"].ToString());
                    if (ventaBienes > 0) { chkVenta.Checked = true; }
                    int actividadEmpresarial = int.Parse(dtCliente.Rows[0]["Actividad_Empresarial"].ToString());
                    if (actividadEmpresarial > 0) { chkActividad.Checked = true; }
                    int rescateInversion = int.Parse(dtCliente.Rows[0]["Rescate_Inversiones"].ToString());
                    if (rescateInversion > 0) { chkRescate.Checked = true; }
                    int Herencia = int.Parse(dtCliente.Rows[0]["Herencias"].ToString());
                    if (Herencia > 0) { chkHerencia.Checked = true; }
                    txtOtros.Text = dtCliente.Rows[0]["Otro_Obs"].ToString();
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

        private void LlenaExperienciaInversora()
        {
            try
            {
                DataTable dtCliente = new DataTable();
                dtCliente = oCliente.ConsultaExperienciaInversora(Convert.ToInt32(lblIdCliente.Text)).Tables[0];
                if (dtCliente.Rows.Count > 0)
                {
                    int ahorroInv = int.Parse(dtCliente.Rows[0]["Ahorro"].ToString());
                    if (ahorroInv > 0) { chkEI_Ahorro.Checked = true; }
                    int herenciasInv = int.Parse(dtCliente.Rows[0]["Herencias"].ToString());
                    if (herenciasInv > 0) { chkEI_Herencia.Checked = true; }
                    int rescateInversionInv = int.Parse(dtCliente.Rows[0]["Rescate_Inversiones"].ToString());
                    if (rescateInversionInv > 0) { chkEI_Rescate.Checked = true; }
                    int perfilInv = int.Parse(dtCliente.Rows[0]["IdPerfilInversor"].ToString());
                    if (perfilInv == 1) { PI1.Checked = true; }
                    if (perfilInv == 2) { PI2.Checked = true; }
                    if (perfilInv == 3) { PI3.Checked = true; }
                    if (perfilInv == 4) { PI4.Checked = true; }
                    int BienesRaices = int.Parse(dtCliente.Rows[0]["Venta_Bienes_Raices"].ToString());
                    if (BienesRaices > 0) { chkEI_Venta.Checked = true; }
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

        private void GrabaExperienciaInversora()
        {
            try
            {
                ExperienciaInversora oExperencia = new ExperienciaInversora();
                oExperencia.IdCliente = Convert.ToInt32(lblIdCliente.Text);
                oExperencia.Ahorro = chkEI_Ahorro.Checked ? 1 : 0;
                oExperencia.VentasBienesRaices = chkEI_Venta.Checked ? 1 : 0;
                oExperencia.Herencias = chkEI_Herencia.Checked ? 1 : 0;
                oExperencia.RescateInversiones = chkEI_Rescate.Checked ? 1 : 0;

                if (PI1.Checked)
                    oExperencia.IdPerfilInversor = 1;
                else if (PI2.Checked)
                    oExperencia.IdPerfilInversor = 2;
                else if (PI3.Checked)
                    oExperencia.IdPerfilInversor = 3;
                else if (PI4.Checked)
                    oExperencia.IdPerfilInversor = 4;
                oCliente.IngresarExperienciaInversora(oExperencia);
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

        private void GrabaPatrimonio()
        {
            try
            {
                BLcliente blcliente = new BLcliente();
                Patrimonio oPatrimonio = new Patrimonio();
                if (chkAhorro.Checked) { oPatrimonio.libreta = 1; }
                if (chkIndemnizacion.Checked) { oPatrimonio.Indemizacion = 1; }
                if (chkVenta.Checked) { oPatrimonio.VentasBienesRaices = 1; }
                if (chkActividad.Checked) { oPatrimonio.ActividadEmpresarial = 1; }
                if (chkRescate.Checked) { oPatrimonio.RescateInversiones = 1; }
                if (chkHerencia.Checked) { oPatrimonio.Herencias = 1; }
                oPatrimonio.Observaciones = txtOtros.Text;
                oPatrimonio.IdCliente = Convert.ToInt32(lblIdCliente.Text);
                blcliente.IngresarPatrimonioCliente(oPatrimonio);
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

        private void GrabarCliente()
        {
            try
            {
                BLcliente blcliente = new BLcliente();
                Cliente oCliente = new Cliente();
                oCliente.Clienterut = txtRut.Text + "-" + txtDigito.Text;
                oCliente.Nombres = txtNombres.Text;
                oCliente.Paterno = txtPaterno.Text;
                oCliente.Materno = txtMaterno.Text;
                oCliente.Email = txtEmail.Text;
                oCliente.Fono = txtFono.Text;
                oCliente.Direccion = txtDireccion.Text;
                oCliente.CodCiudad = Convert.ToInt32(ddlCiudad.SelectedValue);
                oCliente.CodComuna = Convert.ToInt32(ddpComunas.SelectedValue);
                oCliente.IdEstadoCivil = Convert.ToInt32(ddlEstadoCivil.SelectedValue);
                oCliente.IdNacionalidad = Convert.ToInt32(ddlNacionalidad.SelectedValue);
                oCliente.Inversionista = chkInversionista.Checked ? (int)Inversionista.si : (int)Inversionista.no;
                oCliente.PersonaJuridica = rbPjuridica.Checked ? (int)Juridica.si : (int)Juridica.no;
                oCliente.Sexo = rbMasculino.Checked ? Sexo.Masculino.ToString() : Sexo.Femenino.ToString();
                oCliente.UsuarioCreacion = Session["IdUsuario"].ToString();
                oCliente.IdTipoOcupacion = Convert.ToInt32(ddlTipoOcupacion.SelectedValue);
                //oCliente.Profesion = txtProfesion.Text;
                oCliente.NombreEmpresa = TxtEmpresa.Text;
                oCliente.DireccionEmpresa = txtDireccionLaboral.Text;
                oCliente.TelefonoEmpresa = txtFonoEmpresa.Text;
                oCliente.NombreContacto = txtNombreContacto.Text;
                oCliente.TeleFonoContacto = txtFonoContacto.Text;
                oCliente.Ocupacion = txtOcupacion.Text;
                oCliente.FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Value);

                lblIdCliente.Text = blcliente.IngresaCliente(oCliente).ToString();
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




        protected void lbPatrimonio_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            lbPatrimonio.CssClass = "btn btn-secondary active border border-dark";
            lbInversora.CssClass = "btn btn-secondary border border-dark";
            lbPerfilInversor.CssClass = "btn btn-secondary border border-dark";
        }
        protected void lbInversora_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            lbPatrimonio.CssClass = "btn btn-secondary border border-dark";
            lbInversora.CssClass = "btn btn-secondary active border border-dark";
            lbPerfilInversor.CssClass = "btn btn-secondary border border-dark";
        }
        protected void lbPerfilInversor_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            lbPerfilInversor.CssClass = "btn btn-secondary active border border-dark";
            lbPatrimonio.CssClass = "btn btn-secondary border border-dark";
            lbInversora.CssClass = "btn btn-secondary border border-dark";
        }


        protected void lnkVolver_Click(object sender, EventArgs e)
        {

            Response.Redirect("frmClientes2.aspx");
        }

        protected void lnkConfirmModificar_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["Carga_Ruta_WEB"].Equals("1")) { return; }
            try
            {
                if (!cmdSgte_Click())
                    return;

                GrabarCliente();
                GrabaPatrimonio();
                GrabaExperienciaInversora();
                Alerta("Datos guardados de forma exitosa", 3);
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
    }
}