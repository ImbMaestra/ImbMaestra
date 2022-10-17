using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MaestraNet.Util;
using MaestraNet.Data;
using System.IO;

namespace MaestraNet.GC.SVTA
{
    public partial class frmTab : System.Web.UI.Page
    {
        private void Acceso()
        {
            BLPerfiles oAcceso = new BLPerfiles();
            Funciones oFunc = new Funciones();
            DataSet dsAcceso = new DataSet();
            string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);

            Session["IdPerfil"] = "1"; //Linea de prueba (Borrar)
            Session["Origen"] = "Cotizacion"; //Linea de prueba (Borrar)

            try
            {
                dsAcceso = oAcceso.PerfilBotones(Session["IdPerfil"].ToString(), pageName);
            }
            catch (Exception ex)
            {
                //this.Master.Alerta(ex.Message, 1);
                //Se debería mostrar mensaje (Miguel)
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
                Acceso();
                try
                    {
                    string sQuery = "select dbo.fn_SV_Valor_UF_Dia(GETDATE())";
                    cs.dbTools db = new cs.dbTools();
                    string StrvalorUF = db.ResultQueryRun(sQuery);
                    double uf = Convert.ToDouble(StrvalorUF);
                    lblValorUF.Text = string.Format(CultureInfo.CurrentCulture, "$ {0:N2}", uf);

                    if (Session["Origen"] == "Cotizacion")
                    {
                        //Iframe1.Src = "~/GC/SVTA/Reserva/frmTabReserva2.aspx";
                        //lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        //lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        //lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        //lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        //lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        //lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        //lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        //lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        //lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        ////lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

                        //lnkReserva.ControlStyle.CssClass = "botoMaestraActivo btn";

                        lnkMantenedores.Enabled = true;

                        Iframe1.Src = "~/GC/SVTA/Mantenedor/frmTabMantenedor.aspx";
                        lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
                        lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

                        lnkMantenedores.ControlStyle.CssClass = "botoMaestraActivo btn";
                    }
                    else
                    {
                        lnkCotizacion_Click(this, null);
                    }
                }
                catch
                {

                }

            }
        }

        protected void lnkCotizacion_Click(object sender, EventArgs e)
        {
            Iframe1.Src = "~/GC/SVTA/Cotizacion/frmTabCotizacion4.aspx";
            lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            //lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

            lnkCotizacion.ControlStyle.CssClass = "botoMaestraActivo btn";
        }

        protected void lnkReserva_Click(object sender, EventArgs e)
        {
            Iframe1.Src = "~/GC/SVTA/Reserva/frmTabReserva5.aspx";
            lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            //lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

            lnkReserva.ControlStyle.CssClass = "botoMaestraActivo btn";
        }

        protected void lnkPromesa_Click(object sender, EventArgs e)
        {
            Iframe1.Src = "~/GC/SVTA/Promesa/frmPromesa1.aspx";
            lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            //lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

            lnkPromesa.ControlStyle.CssClass = "botoMaestraActivo btn";
        }

        protected void lnkContabilidad_Click(object sender, EventArgs e)
        {
            Iframe1.Src = "~/GC/SVTA/Contabilidad/frmTabContabilidad.aspx";
            lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

            lnkContabilidad.ControlStyle.CssClass = "botoMaestraActivo btn";
        }
        protected void lnkEscrituracion_Click(object sender, EventArgs e)
        {
            Iframe1.Src = "~/GC/SVTA/Escrituracion/frmEscrituracionTemp.aspx";
            lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            //lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

            lnkEscrituracion.ControlStyle.CssClass = "botoMaestraActivo btn";
        }

        protected void lnkCobranza_Click(object sender, EventArgs e)
        {
            Iframe1.Src = "~/GC/SVTA/Cobranza/frmTabCobranza.aspx";
            lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            //lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

            lnkCobranza.ControlStyle.CssClass = "botoMaestraActivo btn";
        }

        protected void lnkRecuperacion_Click(object sender, EventArgs e)
        {
            Iframe1.Src = "~/GC/SVTA/Recuperacion/frmTabRecuperacion.aspx";
            lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            //lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

            lnkRecuperacion.ControlStyle.CssClass = "botoMaestraActivo btn";
        }


        protected void lnkEventos_Click(object sender, EventArgs e)
        {
            Iframe1.Src = "~/GC/SVTA/Bitacora/frmBitacora.aspx";
            lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            //lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

            lnkEventos.ControlStyle.CssClass = "botoMaestraActivo btn";
        }

        protected void lnkReportes_Click(object sender, EventArgs e)
        {
            Iframe1.Src = "~/GC/SVTA/Reportes/frmTabReportes.aspx";
            lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            //lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

            lnkReportes.ControlStyle.CssClass = "botoMaestraActivo btn";
        }

        protected void lnkMantenedores_Click(object sender, EventArgs e)
        {
            Iframe1.Src = "~/GC/SVTA/Mantenedor/frmTabMantenedor.aspx";
            lnkCotizacion.ControlStyle.CssClass = lnkCotizacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReserva.ControlStyle.CssClass = lnkReserva.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkPromesa.ControlStyle.CssClass = lnkPromesa.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkContabilidad.ControlStyle.CssClass = lnkContabilidad.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEscrituracion.ControlStyle.CssClass = lnkEscrituracion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkCobranza.ControlStyle.CssClass = lnkCobranza.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkRecuperacion.ControlStyle.CssClass = lnkRecuperacion.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkEventos.ControlStyle.CssClass = lnkEventos.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            lnkReportes.ControlStyle.CssClass = lnkReportes.Enabled ? "botoMaestra btn" : "tabDisabled btn";
            //lnkMantenedores.ControlStyle.CssClass = lnkMantenedores.Enabled ? "botoMaestra btn" : "tabDisabled btn";

            lnkMantenedores.ControlStyle.CssClass = "botoMaestraActivo btn";
        }
    }
}