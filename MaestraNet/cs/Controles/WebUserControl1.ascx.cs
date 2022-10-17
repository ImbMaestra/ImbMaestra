using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaestraNet.cs.Controles
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdCotizaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName.ToString();

            if (cmd.Equals("Cliente"))
            {
                int xRow = Convert.ToInt16(e.CommandArgument);
                lblIdCotizacion.Text = grdCotizaciones.Rows[xRow].Cells[0].Text;
                SqlDataSource3.DataBind();
                grvCliente.DataBind();
                LinkButton lnkCliente = (LinkButton)grdCotizaciones.Rows[xRow].Cells[5].Controls[0];
                lblCliente.Text = "Detalle Cliente :" + lnkCliente.Text;
                string funcionJS = "showCliente();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            }

            if (cmd.Equals("Inmuebles"))
            {
                int xRow = Convert.ToInt16(e.CommandArgument);
                lblIdCotizacion.Text = grdCotizaciones.Rows[xRow].Cells[0].Text;
                SqlDataSource2.DataBind();
                grdDetalle.DataBind();
                lblDetalle.Text = "Detalle de Cotizacion " + lblIdCotizacion.Text;
                string funcionJS = "showDetalle();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ModalLib", funcionJS, true);
            }
        }

        protected void grdCotizaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string hColor = "#B13261";
            string dColor = "#E9E7E2";
            System.Drawing.Color HeaderColor;
            System.Drawing.Color DataColor;
            LinkButton lnkbtn;
            string sort;
            int ind = 0;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    ind++;
                    if (cell.Controls.Count > 0)
                    {
                        lnkbtn = cell.Controls[0] as LinkButton;
                        if (!string.IsNullOrEmpty(lnkbtn.CommandArgument))
                        {
                            if (this.grdCotizaciones.SortExpression == "")
                            {
                                sort = "IdCotizacion";
                            }
                            else
                            {
                                sort = this.grdCotizaciones.SortExpression;
                            }
                            if (lnkbtn.CommandArgument == sort)
                            {
                                HeaderColor = System.Drawing.ColorTranslator.FromHtml(hColor);
                                cell.BackColor = HeaderColor;
                                lnkbtn.BackColor = HeaderColor;
                                //selInd = ind;
                            }
                        }
                    }
                }
            }

            ind = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    ind++;
                    //if (selInd == ind)
                    //{
                    //    DataColor = System.Drawing.ColorTranslator.FromHtml(dColor);
                    //    cell.BackColor = DataColor;
                    //    try
                    //    {
                    //        LinkButton lnk = cell.Controls[0] as LinkButton;
                    //        lnk.BackColor = DataColor;
                    //    }
                    //    catch
                    //    {

                    //    }
                    //}
                }
            }
        }


        //public DataSourceControl data (DataSourceControl dataSourceControl)
        //{
        //    grdCotizaciones.DataSource = dataSourceControl;
        //}

    }
}