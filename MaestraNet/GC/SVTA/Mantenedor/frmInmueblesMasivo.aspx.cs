using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MaestraNet.Entidad;
using MaestraNet.Data;
using MaestraNet.Util;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Services;

using System.Web.Script.Services;
using System.ComponentModel;

namespace MaestraNet.GC.SVTA.Mantenedor
{
    public partial class frmInmueblesMasivo : System.Web.UI.Page
    {
        private string SortExpression
        {
            get { return ViewState["SortExpression"] != null ? ViewState["SortExpression"].ToString() : "nombre"; }
            set { ViewState["SortExpression"] = value; }
        }
        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        private void Alerta(string msg, int tipo, bool OcultaGrilla = false)
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
            if (tipo == 3 && OcultaGrilla) // INFORMACION
            {
                lblAlertaMSGInfo.Text = msg;
                funcionJS = "showAlertaInformar();$('#GrillaInmueble').hide();";
            }
            if (tipo == 3 && !OcultaGrilla) // INFORMACION
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
            if (Session["IdPerfil"] == null)
            {
                Response.Redirect("frmInmueblesNew.aspx");
            }

            if (!IsPostBack)
            {
                
                RdoPrecioLista1.Checked = true;
                txtProyecto.Text = Convert.ToString(Session["nombreProyecto"]);
                txtProyecto.Focus();

                if (HttpContext.Current.Session["ProdSelection"] != null)
                {
                    List<int> productsIdSel = HttpContext.Current.Session["ProdSelection"] as List<int>;
                    txtCantidad.Text = productsIdSel.Count.ToString();

                    verificaEstadosInmueble();
                    cargaGrillaActual();
                }

                HabilitaCampos();
                HabilitarJustificacion();
            }
        }

        protected void lnkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmInmueblesNew.aspx");
        }

        protected void lnkConfirmModificar_Click(object sender, EventArgs e)
        {
            //Probar con un evento en el código de aspx llamado por jQuery
            //Alerta("Prueba confirmar.", 2);

            //-------Se comenta para probar------
            if (ddlEstadoInmueble.SelectedValue == "1" && txtJustificacion.Text.Trim() == "")
            {
                Alerta("Dede ingresar una justificación para este Tipo de Inmueble", 4);
                return;
            }

            if (!cmdSgte_Click())
                return;

            ValidaTipoPrecioLista();

            GrabarInmueble();

            Alerta("Datos guardados de forma exitosa.", 3);
            LimpiarCampos();
            HabilitarJustificacion();
            //-----------Fin----------------------
        }

        protected void lnkPrevisualizaCambios_Click(object sender, EventArgs e)
        {
            if (ddlEstadoInmueble.SelectedValue == "1" && txtJustificacion.Text.Trim() == "")
            {
                Alerta("Dede ingresar una justificación para este Tipo de Inmueble", 4);
                return;
            }

            if (!cmdSgte_Click())
                return;

            ValidaTipoPrecioLista();
            CargaNuevosValoresEnGrilla();
        }


        protected bool cmdSgte_Click()
        {
            if (txtM2Terraza.Text.Trim().Length < 1 
                && txtMetroUtil.Text.Trim().Length < 1 
                && txtPrecioLista.Text.Trim().Length < 1
                && ddlEstadoInmueble.SelectedValue == "-1"
                && txtJustificacion.Text.Trim().Length < 1
                && txtAlicuota.Text.Trim().Length < 1
                && txtNumeroRol.Text.Trim().Length < 1
                )
            {
                Alerta("Ingrese al menos un valor en alguno de los campos.", 4);
                txtM2Terraza.Focus();
                return false;
            }
            txtM2Terraza.Text = txtM2Terraza.Text.Replace(".", ",");
            txtMetroUtil.Text = txtMetroUtil.Text.Replace(".", ",");
            return true;
        }

        private void GrabarInmueble()
        {
            BLInmueble blInmueble = new BLInmueble();
            Inmueble oInmueble = new Inmueble();

            try
            {
                oInmueble.Terraza = Convert.ToDouble(txtM2Terraza.Text == "" ? "99999999" : txtM2Terraza.Text);
                oInmueble.M2Util = Convert.ToDouble(txtMetroUtil.Text == "" ? "0" : txtMetroUtil.Text);
                oInmueble.PrecioLista = Convert.ToInt32(txtPrecioLista.Text == "" ? "0" : txtPrecioLista.Text);
                oInmueble.TipoPrecioLista = hddTipoPrecioLista.Value;
                oInmueble.IdEstadoInmueble = Convert.ToInt32(ddlEstadoInmueble.Text);
                oInmueble.JustificacionEstadoInmueble = txtJustificacion.Text.Trim();
                oInmueble.Alicuota = txtAlicuota.Text.Trim();
                oInmueble.NumeroRol = txtNumeroRol.Text.Trim();
                oInmueble.Usuario = Session["IdUsuario"].ToString();

                List<int> dt = (List<int>)HttpContext.Current.Session["ProdSelection"];

            
                blInmueble.ModificarInmuebleMasivo(oInmueble, dt);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message, 1);
                return;
            }
            finally
            {

            }
        }

        private void LimpiarCampos()
        {
            txtM2Terraza.Text = "";
            txtMetroUtil.Text = "";
            RdoPrecioLista1.Checked = true;
            txtPrecioLista.Text = "";
            ddlEstadoInmueble.SelectedValue = "-1";
            txtJustificacion.Text = "";
            txtAlicuota.Text = "";
            txtNumeroRol.Text = "";

            gvInmueblesVista.DataSource = null;
            gvInmueblesVista.DataBind();
        }

        private void ValidaTipoPrecioLista()
        {
            if (RdoPrecioLista1.Checked)
            {
                hddTipoPrecioLista.Value = "0";//Total igual para todos
            }
            else if (RdoPrecioLista2.Checked)
            {
                hddTipoPrecioLista.Value = "1"; //Aumento %
            }
            else if (RdoPrecioLista3.Checked)
            {
                hddTipoPrecioLista.Value = "2"; //Aumento UF
            }
            else if (RdoPrecioLista4.Checked)
            {
                hddTipoPrecioLista.Value = "3"; //Disminuye %
            }
            else if (RdoPrecioLista5.Checked)
            {
                hddTipoPrecioLista.Value = "4"; //Disminuye UF
            }

        }

        protected void ddlEstadoInmueble_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarJustificacion();
        }

        private void HabilitarJustificacion()
        {
            if (ddlEstadoInmueble.SelectedValue == "0" || ddlEstadoInmueble.SelectedValue == "6")
            {
                txtPrecioLista.Enabled = true;
            }
            else if (ddlEstadoInmueble.SelectedValue == "1" || ddlEstadoInmueble.SelectedValue == "2" || ddlEstadoInmueble.SelectedValue == "3" || ddlEstadoInmueble.SelectedValue == "4" || ddlEstadoInmueble.SelectedValue == "5")
            {
                txtPrecioLista.Text = "";
                txtPrecioLista.Enabled = false;
            }
            else if(ddlEstadoInmueble.SelectedValue == "-1")
            {
                //Por defecto debe estar habilitado el campo PrecioLista si el estado es Disponible o Bloqueado
                //Recorrer la lista y verificar si todos los inmuebles están en estado Disponible
                verificaEstadosInmueble();
            }   
        }

        private void verificaEstadosInmueble()
        {
            int valida = 0;

            //Nuevo metodo
            if (Session["DatosSeleccionados"] != null)
            {
                DataTable dt = (DataTable)Session["DatosSeleccionados"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["IdEstadoInmueble"]) == 0 || Convert.ToInt32(dt.Rows[i]["IdEstadoInmueble"]) == 6) //0-Estado Disponible / 6-Bloqueado
                    {
                        valida = 0;
                    }
                    else
                    {
                        valida = 1;
                        break;
                    }
                }

                //Cuando se seleccione sólo un elemento, dejará modificar
                if (dt.Rows.Count == 1)
                {
                    valida = 0;
                }
            }

            if (valida == 1)
            {
                txtPrecioLista.Enabled = false;
                txtPrecioLista.Text = "";
                Alerta("Uno de los estados es distinto a Disponible y Bloqueado, no es posible grabar el Precio de Lista", 4);
                //lnkConfirmModificar.Visible = false;
                RdoPrecioLista1.Checked = true;
                ddlEstadoInmueble.Enabled = false;
            }
            else
            {
                txtPrecioLista.Enabled = true;
                ddlEstadoInmueble.Enabled = true;
                //lnkConfirmModificar.Visible = true;
            }
        }

        private void HabilitaCampos()
        {
            if (txtCantidad.Text == "1")
            {
                txtAlicuota.Enabled = true;
                txtNumeroRol.Enabled = true;
                
            }
            else
            {
                txtAlicuota.Enabled = false;
                txtNumeroRol.Enabled = false;
                txtAlicuota.Text = "";
                txtNumeroRol.Text = "";
            }
        }

        private void cargaGrillaActual()
        {
            if (Session["DatosSeleccionados"] != null)
            {
                DataTable dt = (DataTable)Session["DatosSeleccionados"];
                ViewState["ListadoGrillaActual"] = dt;
                gvDatosActuales.DataSource = dt;
                gvDatosActuales.DataBind();
            }
        }

        protected void gvDatosActuales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDatosActuales.PageIndex = e.NewPageIndex;
            gvDatosActuales.DataSource = (DataTable)ViewState["ListadoGrillaActual"];
            gvDatosActuales.DataBind(); 
        }

        protected void gvInmueblesVista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInmueblesVista.PageIndex = e.NewPageIndex;
            CargaNuevosValoresEnGrilla();
        }

        private void CargaNuevosValoresEnGrilla()
        {
            try
            {
                if (Session["DatosSeleccionados"] != null)
                {
                    List<Inmueble> list1 = (List<Inmueble>)Session["ListadoGrillaActual"];

                    DataTable dt = (DataTable)Session["DatosSeleccionados"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list1[i].TerrazaPrev = txtM2Terraza.Text.Trim() == "" ? dt.Rows[i]["TerrazaPrev"].ToString() : txtM2Terraza.Text.Trim();
                        list1[i].M2UtilPrev = txtMetroUtil.Text.Trim() == "" ? dt.Rows[i]["M2UtilPrev"].ToString() : txtMetroUtil.Text.Trim();
                        if (txtPrecioLista.Text.Trim() != "")
                        {
                            list1[i].PrecioLista = CalculaPrecioLista(Convert.ToInt32(hddTipoPrecioLista.Value), Convert.ToInt32(dt.Rows[i]["PrecioLista"]));//Nuevo precio
                        }
                        list1[i].TipoPrecioLista = hddTipoPrecioLista.Value;
                        list1[i].IdEstadoInmueble = Convert.ToInt32(ddlEstadoInmueble.Text);//Verificar
                        list1[i].JustificacionEstadoInmueble = txtJustificacion.Text.Trim() == "" ? dt.Rows[i]["JustificacionEstadoInmueble"].ToString() : txtJustificacion.Text.Trim();
                        list1[i].Alicuota = txtAlicuota.Text.Trim() == "" ? dt.Rows[i]["Alicuota"].ToString() : txtAlicuota.Text.Trim();
                        list1[i].NumeroRol = txtNumeroRol.Text.Trim() == "" ? dt.Rows[i]["NumeroRol"].ToString() : txtNumeroRol.Text.Trim();
                        list1[i].Usuario = Session["IdUsuario"].ToString();
                        list1[i].NDepto = Convert.ToInt32(dt.Rows[i]["NDepto"].ToString());
                        list1[i].ModeloInmueble = dt.Rows[i]["ModeloInmueble"].ToString();

                    }

                    gvInmueblesVista.DataSource = list1;
                    gvInmueblesVista.DataBind();
                }
                else
                {
                    Alerta("Dede recargar la página", 4);
                    return;
                }
            }
            catch (Exception ex)
            {
                Alerta("Error: " + ex.Message, 4);
                throw;
            }
        }

        private int CalculaPrecioLista(int tipoPrecioLista, int precioLista)
        {
            int valorAplicar = Convert.ToInt32(txtPrecioLista.Text);

            Decimal NewValor = 0;
            switch (tipoPrecioLista)
            {
                case 0: //Total igual para todos
                    precioLista = Convert.ToInt32(txtPrecioLista.Text);
                    break;
                case 1: //Aumento %
                    NewValor = Convert.ToDecimal(Convert.ToDouble(precioLista * valorAplicar) / 100);
                    precioLista = Convert.ToInt32(NewValor) + precioLista;
                    break;
                case 2: //Aumento UF
                    precioLista = precioLista + valorAplicar;
                    break;
                case 3: //Disminuye %
                    NewValor = Convert.ToDecimal(Convert.ToDouble(precioLista * valorAplicar) / 100);
                    precioLista = precioLista - Convert.ToInt32((NewValor));
                    break;
                case 4: //Disminuye UF
                    precioLista = precioLista - valorAplicar;
                    break;
                default:
                    break;
            }

            return precioLista;
        }

        //Llamado Ajax
        //[System.Web.Services.WebMethod]
        //public static string Prueba()    // el método debe ser de static
        //{
        //    return "1";
        //}

        [WebMethod]
        public static string Prueba(int TempId)    // el método debe ser de static
        {
            return "1";
        }


        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
    }
}