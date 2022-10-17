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
    public partial class frmTipoInmueble : System.Web.UI.Page
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
            //oFunc.DisableControls(Page, false); //DEsactiva botones

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
            BLInmueble oInmueble = new BLInmueble();
            Funciones oFunciones = new Funciones();

            try
            {
                dsProyecto = oInmueble.BuscaTipoInmueble(txtProyecto.Text.Trim());
                txtProyecto.Text = txtProyecto.Text.Trim();
                ViewState["TipoInmueble"] = dsProyecto.Tables[0];
                SortExpression = "nombre";
                //gvTipoInmueble.DataSource = oFunciones.BindGrid((DataTable)ViewState["TipoInmueble"], SortDirection, SortExpression);
                gvTipoInmueble.DataSource = oFunciones.BindGrid((DataTable)ViewState["TipoInmueble"], SortDirection, SortExpression);
                gvTipoInmueble.DataBind();
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

        protected void btnEditarTipoInmueble_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            HiddenField theHiddenField = gvrow.FindControl("HiddenFieldDifferentUsers") as HiddenField;
            Response.Redirect("frmTipoInmuebleDetalle.aspx?IdTipoInmueble=" + theHiddenField.Value);
            //string funcionJS = " $('#GrillaClientes').hide();$('#BuscarCliente').hide();$('#DatosCliente').show();";
            //ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
        }

        protected void btnEliminarTipoInmueble_Click(object sender, EventArgs e)
        {
            BLInmueble oInmueble = new BLInmueble();
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            HiddenField theHiddenField = gvrow.FindControl("HiddenFieldDifferentUsers") as HiddenField;
            Funciones oFunciones = new Funciones();
            DataSet dsInmueble;
            string funcionJS;
            //int ndepto = txtDepto.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(txtDepto.Text.Trim());
            try
            {
                oInmueble.EliminarTipoInmueble(Convert.ToInt32(theHiddenField.Value));
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
            }
            try
            {
                //dsInmueble = oInmueble.ListaInmueble(Convert.ToInt32(ddlProyecto.SelectedValue), Convert.ToInt32(ddlTipoInmueble.SelectedValue), ddlTorre.SelectedValue, ndepto, Convert.ToInt32(ddlModeloInmueble.SelectedValue), Convert.ToInt32(txtPiso.Text));
                //ViewState["Inmueble"] = dsInmueble.Tables[0];

                SortExpression = "Descripcion";

                // if (dsInmueble.Tables[0].Rows.Count > 0)
                //{

                //gvInmuebles.DataSource = oFunciones.BindGrid((DataTable)ViewState["Inmueble"], SortDirection, SortExpression);
                //gvInmuebles.DataBind();

                //}
                funcionJS = "$('#GrillaInmueble').show();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;

            }
        }

        protected void gvTipoInmueble_Sorting(object sender, GridViewSortEventArgs e)
        {
            Funciones ofunciones = new Funciones();
            DataTable dtOrden = new DataTable();

            SortExpression = e.SortExpression;
            SortDirection = SortDirection == "ASC" ? "DESC" : "ASC";

            dtOrden = ofunciones.BindGrid((DataTable)ViewState["TipoInmueble"], SortDirection, e.SortExpression);

            //ViewState["Proyectos"] = dtOrden;
            //gvTipoInmueble.DataSource = dtOrden;
            //gvTipoInmueble.DataBind();
        }

        protected void gvTipoInmueble_DataBound(object sender, EventArgs e)
        {
            //int sortedColumnPosition = 0;
            //LinkButton lnkbtn;
            //string hex = "#E9E7E2";
            //System.Drawing.Color _color;

            //// Gets position of column whose header text matches SortExpression
            //// of the GridView when column is sorted
            //if (gvTipoInmueble.HeaderRow == null)
            //    return;
            //foreach (TableCell cell in gvTipoInmueble.HeaderRow.Cells)
            //{
            //    if (cell.Controls.Count > 0)
            //    {
            //        lnkbtn = (LinkButton)cell.Controls[0];
            //        if (lnkbtn.CommandArgument == SortExpression)
            //        {
            //            break;
            //        }
            //    }
            //    sortedColumnPosition++;
            //}
            //if (!string.IsNullOrEmpty(SortExpression))
            //{
            //    foreach (GridViewRow row in gvTipoInmueble.Rows)
            //    {
            //        if (sortedColumnPosition < 16)
            //        {
            //            _color = System.Drawing.ColorTranslator.FromHtml(hex);
            //            row.Cells[sortedColumnPosition].BackColor = _color;
            //        }
            //    }
            //}
        }

        protected void gvTipoInmueble_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvTipoInmueble_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvTipoInmueble.PageIndex = e.NewPageIndex;
            //DataTable dt = (DataTable)ViewState["Proyectos"];
            //dt.DefaultView.Sort = SortExpression + " " + this.SortDirection;

            //gvTipoInmueble.DataSource = dt;

            //gvTipoInmueble.DataBind();

        }

        protected void btnNuevoProyecto_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmTipoInmuebleDetalle.aspx");
        }
    }
}