using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MaestraNet.Util;
using MaestraNet.Data;
using System.Data;

namespace MaestraNet.GC.SVTA.Mantenedor
{
    public partial class frmPerfilBoton : System.Web.UI.Page
    {
        DataTable dtPerfil;
        private string SortExpression
        {
            get { return ViewState["SortExpression"] != null ? ViewState["SortExpression"].ToString() : "ID_ROL"; }
            set { ViewState["SortExpression"] = value; }
        }
        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }
        private void BindGrid(string sortExpression = null, bool reOrdena = true)
        {
            dtPerfil = (DataTable)ViewState["Perfil"];
            if (sortExpression != null)
            {
                DataView dv = dtPerfil.AsDataView();
                if (reOrdena)
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                else
                    this.SortDirection = "ASC";
                dv.Sort = sortExpression + " " + this.SortDirection;
                gvPerfil.DataSource = dv;



            }
            else
            {
                gvPerfil.DataSource = dtPerfil;


            }
            gvPerfil.DataBind();

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dvPaginas.Visible = true;
                dvUsuarios.Visible = false;

            }


        }


        protected void lnkVolver_Click(object sender, EventArgs e)
        {

        }

        private DataTable selectControl()
        {
            string funcionJS;
            DataTable dtControles = new DataTable();
            dtControles.Columns.Add("idBoton");
            dtControles.Columns.Add("idPagina");
            DataRow drControl;

            foreach (ListItem oLista in lbControles.Items)
            {
                if (oLista.Selected)
                {
                    drControl = dtControles.NewRow();
                    drControl[0] = oLista.Value;
                    drControl[1] = ddlPaginas.SelectedItem.Text;
                    dtControles.Rows.Add(drControl);
                }

            }
            return dtControles;
        }
        protected void lnkModificarAcceso_Click(object sender, EventArgs e)
        {
            string funcionJS;
            BLPerfiles oGrabar = new BLPerfiles();
            DataTable dtControles;
            string spagina = ddlPaginas.SelectedItem.Text;

            dtControles = selectControl();

            try
            {
                oGrabar.GrabarAcceso(dtControles, Session["Sistema"].ToString(), ddlPerfiles.SelectedValue, spagina);
                lblAlertaMSGInfo.Text = "Perfil actualizado de forma correcta.";
                funcionJS = "$(function() { $('[id*=lbControles]').multiselect({includeSelectAllOption: true });});$( document ).ready(function() {showAlertaInformar();});";
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
            }


        }

        protected void ddlPaginas_SelectedIndexChanged1(object sender, EventArgs e)
        {
            Funciones oFunc = new Funciones();
            BLPerfiles bLPerfiles = new BLPerfiles();
            List<string> lsButtons;
            //int i = 0;
            //ListItem oItem = new ListItem();
            DataTable dtControles = new DataTable();
            //DataRow oRow;
            string funcionJS;

            //dtControles.Columns.Add("idControl");
            //dtControles.Columns.Add("Control");

            string sRutaCompleta = ddlPaginas.SelectedItem.Value;
            string spagina = ddlPaginas.SelectedItem.Text;

            //sRutaCompleta = sRutaCompleta + "?Load=1";
            Session["Carga_Ruta_WEB"] = "1";
            lsButtons = oFunc.ControlsPage(sRutaCompleta);

            funcionJS = " $(function() { $('[id*=lbControles]').multiselect({includeSelectAllOption: true });});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);

            lbControles.DataSource = lsButtons;
            lbControles.DataBind();
            Session["Carga_Ruta_WEB"] = "0";

            try
            {
                dtControles = bLPerfiles.ConsultaAcceso(Session["Sistema"].ToString(), ddlPerfiles.SelectedValue, ddlPaginas.SelectedItem.Text);
            }
            catch (Exception ex)
            {

            }

            foreach (DataRow drControl in dtControles.Rows)
            {
                for (int i = 0; i < lbControles.Items.Count; i++)
                {
                    if (lbControles.Items[i].Text == drControl[0].ToString())
                        lbControles.Items[i].Selected = true;
                }
            }


            //lbControles.DataValueField = "idControl";
            //lbControles.DataTextField = "Control";
            //lbControles.DataSource = dtControles;



        }

        protected void ddlPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string funcionJS;
            funcionJS = " $(function() { $('[id*=lbControles]').multiselect({includeSelectAllOption: true });});";
            ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        }

        protected void lbPerfilPagina_Click(object sender, EventArgs e)
        {
            lbPerfilPagina.CssClass = "btn btn-secondary active border border-dark";
            lbPerfilUsuario.CssClass = "btn btn-secondary border border-dark";
            dvPaginas.Visible = true;
            dvUsuarios.Visible = false;
        }

        protected void lbPerfilUsuario_Click(object sender, EventArgs e)
        {
            lbPerfilPagina.CssClass = "btn btn-secondary border border-dark";
            lbPerfilUsuario.CssClass = "btn btn-secondary active border border-dark";
            dvPaginas.Visible = false;
            dvUsuarios.Visible = true;
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            BLPerfiles oPerfil = new BLPerfiles();
            DataSet dsPerfil = new DataSet();

            try
            {
                dsPerfil = oPerfil.ConsultaPerfiles(txtusuario.Text, ddlPerfilUsu.SelectedValue);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
            }
            gvPerfil.DataSource = dsPerfil.Tables[0];
            gvPerfil.DataBind();
            ViewState["Perfil"] = gvPerfil.DataSource;
            SortExpression = "ID_ROL";
            this.BindGrid(SortExpression, false);

        }

        protected void gvPerfil_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPerfil.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["Perfil"];
            dt.DefaultView.Sort = SortExpression + " " + this.SortDirection;

            gvPerfil.DataSource = dt;

            gvPerfil.DataBind();
        }
    }
}