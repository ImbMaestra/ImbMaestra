using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaestraNet.Data;
using System.Data;
using MaestraNet.Util;
using System.IO;

namespace MaestraNet.GC.SVTA.Mantenedor
{
    public partial class frmTabMantenedor : System.Web.UI.Page
    {
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
                //this.Alerta(ex.Message, 1);
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Comentado Miguel
                //myFrame.Src = "frmProyectos.aspx";
                //lnkPerfil.ControlStyle.CssClass = lnkPerfil.Enabled ? "botonTab btn" : "tabDisabled btn";
                //lnkPlantilla.ControlStyle.CssClass = lnkPlantilla.Enabled ? "botonTab btn" : "tabDisabled btn";
                //lnkInmueble.ControlStyle.CssClass = lnkInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
                //lnkClientes.ControlStyle.CssClass = lnkClientes.Enabled ? "botonTab btn" : "tabDisabled btn";
                //lnkProyectos.ControlStyle.CssClass = lnkProyectos.Enabled ? "botoMaestraActivo btn" : "tabDisabled btn";

                //myFrame.Src = "frmMantenedorGeneral.aspx";
                myFrame.Src = "frmInmueblesNew.aspx";
                lnkPerfil.ControlStyle.CssClass = lnkPerfil.Enabled ? "botonTab btn" : "tabDisabled btn";
                lnkPlantilla.ControlStyle.CssClass = lnkPlantilla.Enabled ? "botonTab btn" : "tabDisabled btn";
                lnkInmueble.ControlStyle.CssClass = lnkInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
                lnkClientes.ControlStyle.CssClass = lnkClientes.Enabled ? "botonTab btn" : "tabDisabled btn";
                lnkProyectos.ControlStyle.CssClass = lnkProyectos.Enabled ? "botonTab btn" : "tabDisabled btn";
                lnkTipoInmueble.ControlStyle.CssClass = lnkTipoInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
                //lnkMantenedorGeneral.Enabled = true;
                //lnkMantenedorGeneral.ControlStyle.CssClass = lnkMantenedorGeneral.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                //lnkMantenedorGeneral.ControlStyle.CssClass = "botoMaestraActivo btn";
                lnkInmuebleNew.Enabled = true;
                lnkInmuebleNew.ControlStyle.CssClass = lnkInmuebleNew.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                lnkInmuebleNew.ControlStyle.CssClass = "botoMaestraActivo btn";

                //Acceso(); //Comentado, bloquea los botones según el perfil
            }
        }

        protected void lnkPerfil_Click(object sender, EventArgs e)
        {
            lnkPerfil.ControlStyle.CssClass = lnkPerfil.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkPlantilla.ControlStyle.CssClass = lnkPlantilla.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmueble.ControlStyle.CssClass = lnkInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkClientes.ControlStyle.CssClass = lnkClientes.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkProyectos.ControlStyle.CssClass = lnkProyectos.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkTipoInmueble.ControlStyle.CssClass = lnkTipoInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmuebleNew.ControlStyle.CssClass = lnkInmuebleNew.Enabled ? "botonTab btn" : "tabDisabled btn";

            lnkPerfil.ControlStyle.CssClass = "botoMaestraActivo btn";

            myFrame.Src = "frmPerfilBoton.aspx";
        }

        protected void lnkProyectos_Click(object sender, EventArgs e)
        {

            lnkPerfil.ControlStyle.CssClass = lnkPerfil.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkPlantilla.ControlStyle.CssClass = lnkPlantilla.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmueble.ControlStyle.CssClass = lnkInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkClientes.ControlStyle.CssClass = lnkClientes.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkProyectos.ControlStyle.CssClass = lnkProyectos.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkTipoInmueble.ControlStyle.CssClass = lnkTipoInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmuebleNew.ControlStyle.CssClass = lnkInmuebleNew.Enabled ? "botonTab btn" : "tabDisabled btn";

            lnkProyectos.ControlStyle.CssClass = "botoMaestraActivo btn";

            myFrame.Src = "frmProyectos.aspx";

        }

        protected void lnkInmueble_Click(object sender, EventArgs e)
        {
            lnkPerfil.ControlStyle.CssClass = lnkPerfil.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkPlantilla.ControlStyle.CssClass = lnkPlantilla.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmueble.ControlStyle.CssClass = lnkInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkClientes.ControlStyle.CssClass = lnkClientes.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkProyectos.ControlStyle.CssClass = lnkProyectos.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkTipoInmueble.ControlStyle.CssClass = lnkTipoInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmuebleNew.ControlStyle.CssClass = lnkInmuebleNew.Enabled ? "botonTab btn" : "tabDisabled btn";

            lnkInmueble.ControlStyle.CssClass = "botoMaestraActivo btn";

            myFrame.Src = "frmInmuebles.aspx";
        }

        protected void lnkPlantilla_Click(object sender, EventArgs e)
        {
            lnkPerfil.ControlStyle.CssClass = lnkPerfil.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkPlantilla.ControlStyle.CssClass = lnkPlantilla.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmueble.ControlStyle.CssClass = lnkInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkClientes.ControlStyle.CssClass = lnkClientes.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkProyectos.ControlStyle.CssClass = lnkProyectos.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkTipoInmueble.ControlStyle.CssClass = lnkTipoInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmuebleNew.ControlStyle.CssClass = lnkInmuebleNew.Enabled ? "botonTab btn" : "tabDisabled btn";

            lnkPlantilla.ControlStyle.CssClass = "botoMaestraActivo btn";

            myFrame.Src = "frmPlantillas.aspx";

        }

        protected void lnkClientes_Click(object sender, EventArgs e)
        {
            lnkPerfil.ControlStyle.CssClass = lnkPerfil.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkPlantilla.ControlStyle.CssClass = lnkPlantilla.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmueble.ControlStyle.CssClass = lnkInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkClientes.ControlStyle.CssClass = lnkClientes.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkProyectos.ControlStyle.CssClass = lnkProyectos.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkTipoInmueble.ControlStyle.CssClass = lnkTipoInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmuebleNew.ControlStyle.CssClass = lnkInmuebleNew.Enabled ? "botonTab btn" : "tabDisabled btn";

            lnkClientes.ControlStyle.CssClass = "botoMaestraActivo btn";

            myFrame.Src = "frmClientes2.aspx";

        }

        //protected void lnkMantenedorGeneral_Click(object sender, EventArgs e)
        //{
        //    lnkPerfil.ControlStyle.CssClass = lnkPerfil.Enabled ? "botonTab btn" : "tabDisabled btn";
        //    lnkPlantilla.ControlStyle.CssClass = lnkPlantilla.Enabled ? "botonTab btn" : "tabDisabled btn";
        //    lnkInmueble.ControlStyle.CssClass = lnkInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
        //    lnkClientes.ControlStyle.CssClass = lnkClientes.Enabled ? "botonTab btn" : "tabDisabled btn";
        //    lnkProyectos.ControlStyle.CssClass = lnkProyectos.Enabled ? "botonTab btn" : "tabDisabled btn";

        //    lnkMantenedorGeneral.ControlStyle.CssClass = "botoMaestraActivo btn";

        //    myFrame.Src = "frmMantenedorGeneral.aspx";

        //}

        protected void lnkInmuebleNew_Click(object sender, EventArgs e)
        {
            lnkPerfil.ControlStyle.CssClass = lnkPerfil.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkPlantilla.ControlStyle.CssClass = lnkPlantilla.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmueble.ControlStyle.CssClass = lnkInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkClientes.ControlStyle.CssClass = lnkClientes.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkProyectos.ControlStyle.CssClass = lnkProyectos.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkTipoInmueble.ControlStyle.CssClass = lnkTipoInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmuebleNew.ControlStyle.CssClass = lnkInmuebleNew.Enabled ? "botonTab btn" : "tabDisabled btn";

            lnkInmuebleNew.ControlStyle.CssClass = "botoMaestraActivo btn";

            myFrame.Src = "frmInmueblesNew.aspx";

        }

        protected void lnkTipoInmueble_Click(object sender, EventArgs e)
        {
            lnkPerfil.ControlStyle.CssClass = lnkPerfil.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkPlantilla.ControlStyle.CssClass = lnkPlantilla.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmueble.ControlStyle.CssClass = lnkInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkClientes.ControlStyle.CssClass = lnkClientes.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkProyectos.ControlStyle.CssClass = lnkProyectos.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkTipoInmueble.ControlStyle.CssClass = lnkTipoInmueble.Enabled ? "botonTab btn" : "tabDisabled btn";
            lnkInmuebleNew.ControlStyle.CssClass = lnkInmuebleNew.Enabled ? "botonTab btn" : "tabDisabled btn";

            lnkTipoInmueble.ControlStyle.CssClass = "botoMaestraActivo btn";

            myFrame.Src = "frmTipoInmueble.aspx";

        }
    }
}