using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace MaestraNet
{
    public partial class SiteMaster : MasterPage
    {
        public void Alerta(string msg, int tipo)
        {
            string funcionJS = "";
            if (tipo == 1) //ERROR
            {
                lblAlertaMSGError.Text = msg;
                funcionJS = "showAlertaError();";
            }
            if (tipo == 2) // CONFIRMACION
            {
                lblAlertaMSGConfirmar.Text = msg;
                funcionJS = "showAlertaConfirmar();";
            }
            if (tipo == 3) // INFORMACION
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
                try
                {
                    cs.dbTools db = new cs.dbTools();
                    //lblUsuario.Text = db.Nombre_Usuario(Session["IdUsuario"].ToString());
                    Session["IdUsuario"] = "MiguelM";
                    lblUsuario.Text = "MiguelM";
                    Indicadores();
                }
                catch
                {

                }

                if (lblUsuario.Text.Length < 1)
                {
                    Response.Redirect("~/Default.aspx");
                }

            }
            else
            {
                string s = Session["IdUsuario"].ToString();
                if (s.Length < 1)
                {           
                    Session.Clear();
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        private void Indicadores()
        {
            try
            {
                double Indicador_UF = 0;
                double Indicador_Dolar = 0;
                string Fecha = DateTime.Now.ToString("yyyy-MM-dd");
                cs.dbTools db = new cs.dbTools();
                string Query = "exec dbo.sp_VTA_Indicadores '" + Fecha + "';";
                //System.Data.SqlClient.SqlDataReader rs = db.sqlRS(Query);
                //while (rs.Read())
                //{
                //    Indicador_Dolar = Convert.ToDouble(rs.GetValue(0).ToString());
                //    Indicador_UF = Convert.ToDouble(rs.GetValue(1).ToString());
                //}
                //rs.Close();
                lblUF.Text = String.Format("Valor UF: $ {0:n}", Indicador_UF);
                lblDollar.Text = String.Format("Valor Dolar: $ {0:n}", Indicador_Dolar);

                Session["valorUF"] = Indicador_UF;
                Session["valorDollar"] = Indicador_Dolar;

            }
            catch (Exception err)
            {

                string sQuery = "select dbo.fn_SV_Valor_UF_Dia(GETDATE())";
                //cs.dbTools db = new cs.dbTools();
                //string StrvalorUF = db.ResultQueryRun(sQuery);
                //double uf = Convert.ToDouble(StrvalorUF);
                //Session["valorUF"] = uf;
                Session["valorUF"] = "30000";

                string errs = err.Message.ToString();

            }
        }

        protected void lnkSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Default.aspx");
        }

        protected void imgLogo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmHome.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmHome.aspx");
        }

        protected void lnkConfig_Click(object sender, EventArgs e)
        {

        }
    }
}