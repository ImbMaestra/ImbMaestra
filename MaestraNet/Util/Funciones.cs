using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Web.UI;
using System.Web.Compilation;

namespace MaestraNet.Util
{
    public class Funciones
    {
        private List<string> lsControles=new List<string>();
        /// <summary>
        /// 
        /// </summary>

        /// <param name="dtOrdena"> tabla del viewstate</param>
        /// <param name="SortDirection"> Orden ASC o DESC</param>
        /// <param name="sortExpression">campo de la tabla que se ordenara</param>

        public DataTable BindGrid(DataTable dtOrdena, string SortDirection, string sortExpression = null)
        {
            
            if (sortExpression != null)
            {
                DataView dv = dtOrdena.AsDataView();
               
                dv.Sort = sortExpression + " " + SortDirection;
                return dv.ToTable();
            }
            else
            {
                return dtOrdena;
            }
            
        }

        public Control FindControlRecursive(Control rootControl, string controlID)
        {

            if (rootControl.ID == controlID)
            {
                if (rootControl.GetType() == typeof(LinkButton))
                {
                    ((LinkButton)rootControl).ControlStyle.Dispose();
                    if (rootControl.Parent is DataControlFieldCell)
                    {
                        ((LinkButton)rootControl).ControlStyle.CssClass = "btn grid_boton";
                    }
                    else
                        //((LinkButton)rootControl).ControlStyle.CssClass = "botoMaestra btn";
                    ((LinkButton)rootControl).Enabled = true;
                    
                    return (rootControl);
                }
                else if (rootControl.GetType() == typeof(ImageButton))
                {
                    ((ImageButton)rootControl).ControlStyle.Dispose();
                    if (rootControl.Parent is DataControlFieldCell)
                    {
                        ((ImageButton)rootControl).ControlStyle.CssClass = "btn grid_boton";
                    }
                    else
                        //((LinkButton)rootControl).ControlStyle.CssClass = "botoMaestra btn";
                        ((ImageButton)rootControl).Enabled  = true;

                    return (rootControl);
                }
            }


            foreach (Control controlToSearch in rootControl.Controls)
            {

                if (controlToSearch != null)
                    if (controlToSearch.GetType() == typeof(GridView))
                    {

                        foreach (GridViewRow row in ((GridView)(controlToSearch)).Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                LinkButton myHyperLink = row.FindControl(controlID) as LinkButton;
                                if (myHyperLink != null)
                                {
                                    myHyperLink.CssClass = "btn grid_boton";
                                    myHyperLink.Enabled = true;
                                }
                            }
                        }
                    }
                    else if (controlToSearch.GetType() == typeof(LinkButton))
                    {
                        LinkButton myHyperLink = controlToSearch as LinkButton;
                        if (myHyperLink.ID == controlID)
                        {
                            myHyperLink.Enabled = true;
                            myHyperLink.Visible = true;
                            myHyperLink.CssClass = "botoMaestra btn";
                        }
                    }
                    else if (controlToSearch.GetType() == typeof(ImageButton))
                    {
                        ImageButton myHyperLink = controlToSearch as ImageButton;
                        if (myHyperLink.ID == controlID)
                            myHyperLink.Enabled = true;
                    }
                Control controlToReturn = FindControlRecursive(controlToSearch, controlID);
                if (controlToReturn != null)
                {
                    return (controlToReturn);
                }

            }
            return (null);
        }
        public void DisableControls(Control parent, bool State)
        {
            try
            {
                foreach (Control c in parent.Controls)
                {
                    if (c is LinkButton)
                    {
                        if (c.ID != "lnkHome" && c.ID != "lnkSalir" && c.ID != "lnkConfig")
                        {
                            if (c.Parent is DataControlFieldCell && c.ID == null)
                                ((LinkButton)(c)).Enabled = !State;
                            else if ((c.GetType().Name == "DataControlLinkButton") || (c.GetType().Name == "DataControlPagerLinkButton"))
                            {
                                ((LinkButton)(c)).Enabled = !State;
                            }
                            else
                            {
                                ((LinkButton)(c)).Enabled = false;
                                //((LinkButton)(c)).Visible = false;
                                ((LinkButton)(c)).ControlStyle.CssClass = "tabDisabled btn_disabled";
                            }

                        }
                    }
                    else if (c is ImageButton)
                    {
                        if (c.ID != "imgLogo")
                            ((ImageButton)(c)).Enabled = State;
                    }
                    else if (c is HtmlAnchor)
                        ((HtmlAnchor)(c)).Disabled = State;
                    DisableControls(c, State);
                }
            }
            catch
            {

            }
            
        }

        private void getLinkButton(Control parent)
        {

            foreach (Control c in parent.Controls)
            {
                if (c is LinkButton)
                {
                    if (c.ID != null)
                    { 
                        if (!lsControles.Contains(c.ID))
                        {
                            lsControles.Add(c.ID);
                        }
                    }
                }
                getLinkButton(c);
            }
        }
        public List<string> ControlsPage(string ruta)
        {
            

            var httpRequest = new HttpRequest(string.Empty, HttpContext.Current.Request.Url.AbsoluteUri, string.Empty);
            var stringWriter = new System.IO.StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            

          if (lsControles!=null)
            lsControles.Clear();

            Type type = BuildManager.GetCompiledType(ruta);
            Page myPage = (Page)Activator.CreateInstance(type);
            myPage.ProcessRequest(httpContext);

            getLinkButton(myPage.Form);
            return lsControles;
            

        }
    }
}