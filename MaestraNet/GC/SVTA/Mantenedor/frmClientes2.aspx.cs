using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaestraNet.GC.SVTA.Mantenedor
{
    public partial class frmClientes2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                // lblAlertaMSGConfirmar.Text = msg;
                funcionJS = "showAlertaConfirmar();";
            }
            if (tipo == 3) // INFORMACION
            {
                lblAlertaMSGInfo.Text = msg;
                funcionJS = "showAlertaInformar();";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {

            cs.Utiles U = new cs.Utiles();
            cs.dbTools db = new cs.dbTools();

            string rut = txtRutBuscar.Text + "-" + txtdvBuscar.Text;
            if (txtRutBuscar.Text.Length > 1)
            {
                string sRut = txtRutBuscar.Text + "-" + txtdvBuscar.Text;
                if (!U.validarRut(sRut))
                {
                    Alerta("Rut no es Valido", 3);
                    return;
                }
                else
                {
                    lblRut.Text = sRut;
                }
                int iRut = 0;
                iRut = Convert.ToInt32(txtRutBuscar.Text);
                string sNombre = db.Nombre_Usuario(iRut);
                if (sNombre == "")
                {

                    Alerta("Rut no existe en Fin700", 3);
                    return;
                }

            }
            else
            {
                lblRut.Text = "0";
            }

            if (lblNombre.Text.Equals("") || lblNombre.Text.Equals("0"))
            {
                lblNombre.Text = "0";
            }
            else
            {
                lblNombre.Text = txtNombreBuscar.Text;
            }
            if (lblAPaterno.Text.Equals("") || lblAPaterno.Text.Equals("0"))
            {
                lblAPaterno.Text = "0";
            }
            else
            {
                lblAPaterno.Text = txtPaternoBuscar.Text;
            }

            if (lblAMaterno.Text.Equals("") || lblAMaterno.Text.Equals("0"))
            {
                lblAMaterno.Text = "0";
            }
            else
            {
                lblAMaterno.Text = txtAMaternoBuscar.Text;
            }

            string query = "exec dbo.sp_VTA_ListaClientes '" + lblRut.Text + "','" + lblNombre.Text + "','" + lblAPaterno.Text + "','" + lblAMaterno.Text + "';";





        }

        protected void grdCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName.ToString();
            int x = 0;

            if (!cmd.Contains("Sort"))
            {
                x = int.Parse(e.CommandArgument.ToString());
            }



            if (cmd.Equals("Edita"))
            {

                lblIdCliente.Text = x.ToString();
                Response.Redirect("frmClienteDetalle.aspx?IdCliente=" + lblIdCliente.Text);

            }





        }
    }
}