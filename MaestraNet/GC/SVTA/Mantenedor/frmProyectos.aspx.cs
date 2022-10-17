using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Subgurim.Controles;
using MaestraNet.Data;
using System.Data;
using MaestraNet.Util;
using System.IO;

namespace MaestraNet.GC.SVTA.Mantenedor
{
    public partial class frmProyectos : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Comentado Miguel
                //ddpRegion.DataBind();
                //ddpComuna.DataBind();
                //ddlEstado.DataBind();
                lnkBuscar_Click(null, null);
                Acceso();
            }
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            DataSet dsProyecto;
            BLProyecto oCliente = new BLProyecto();
            Funciones oFunciones = new Funciones();


            try
            {
                dsProyecto = oCliente.ListaProyecto(Convert.ToInt32(ddpRegion.SelectedValue), Convert.ToInt32(ddpComuna.SelectedValue), txtProyecto.Text, Convert.ToInt32(ddlEstado.SelectedValue));
                ViewState["Proyectos"] = dsProyecto.Tables[0];
                SortExpression = "nombre";
                gvProyectos.DataSource = oFunciones.BindGrid((DataTable)ViewState["Proyectos"], SortDirection, SortExpression);
                gvProyectos.DataBind();
                if (dsProyecto.Tables[0].Rows.Count == 0)
                {
                    Alerta("No existen datos con el criterio de busqueda.", 3);
                }
                else
                {
                    string funcionJS = "$('#GrillaClientes').show();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;

            }

        }
        protected void btnEditarCliente_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            HiddenField theHiddenField = gvrow.FindControl("HiddenFieldDifferentUsers") as HiddenField;
            Response.Redirect("frmProyectoDetalle.aspx?IdProyecto=" + theHiddenField.Value);
            //string funcionJS = " $('#GrillaClientes').hide();$('#BuscarCliente').hide();$('#DatosCliente').show();";
            //ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        }

        protected void gvProyectos_Sorting(object sender, GridViewSortEventArgs e)
        {
            Funciones ofunciones = new Funciones();
            DataTable dtOrden = new DataTable();

            SortExpression = e.SortExpression;
            SortDirection = SortDirection == "ASC" ? "DESC" : "ASC";

            dtOrden = ofunciones.BindGrid((DataTable)ViewState["Proyectos"], SortDirection, e.SortExpression);

            ViewState["Proyectos"] = dtOrden;
            gvProyectos.DataSource = dtOrden;
            gvProyectos.DataBind();
        }

        protected void gvProyectos_DataBound(object sender, EventArgs e)
        {
            int sortedColumnPosition = 0;
            LinkButton lnkbtn;
            string hex = "#E9E7E2";
            System.Drawing.Color _color;

            // Gets position of column whose header text matches SortExpression
            // of the GridView when column is sorted
            if (gvProyectos.HeaderRow == null)
                return;
            foreach (TableCell cell in gvProyectos.HeaderRow.Cells)
            {
                if (cell.Controls.Count > 0)
                {
                    lnkbtn = (LinkButton)cell.Controls[0];
                    if (lnkbtn.CommandArgument == SortExpression)
                    {
                        break;
                    }
                }
                sortedColumnPosition++;
            }
            if (!string.IsNullOrEmpty(SortExpression))
            {
                foreach (GridViewRow row in gvProyectos.Rows)
                {
                    if (sortedColumnPosition < 16)
                    {
                        _color = System.Drawing.ColorTranslator.FromHtml(hex);
                        row.Cells[sortedColumnPosition].BackColor = _color;
                    }
                }
            }
        }

        protected void gvProyectos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string hex = "#B13261";
            System.Drawing.Color _color;
            LinkButton lnkbtn;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Controls.Count > 0)
                    {
                        lnkbtn = cell.Controls[0] as LinkButton;
                        if (!string.IsNullOrEmpty(lnkbtn.CommandArgument))
                        {
                            if (lnkbtn.CommandArgument == SortExpression)
                            {
                                _color = System.Drawing.ColorTranslator.FromHtml(hex);
                                cell.BackColor = _color;
                                lnkbtn.BackColor = _color;
                            }
                        }
                    }
                }
            }
        }

        protected void gvProyectos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProyectos.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["Proyectos"];
            dt.DefaultView.Sort = SortExpression + " " + this.SortDirection;

            gvProyectos.DataSource = dt;

            gvProyectos.DataBind();

        }

        protected void btnNuevoProyecto_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmProyectoDetalle.aspx");
        }
    }
}