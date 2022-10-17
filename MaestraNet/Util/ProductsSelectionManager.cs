using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MaestraNet.Util
{
    public class ProductsSelectionManager
    {
        public static void KeepSelection(GridView grid)
        {
            // Se obtienen los id de producto checkeados de la pagina actual
            List<int> checkedProd = (from item in grid.Rows.Cast<GridViewRow>()
                                     let check = (CheckBox)item.FindControl("ChkEdicion")
                                     where check.Checked
                                     select Convert.ToInt32(grid.DataKeys[item.RowIndex].Value)).ToList();


            // Se recupera de session la lista de seleccionados previamente
            List<int> productsIdSel = HttpContext.Current.Session["ProdSelection"] as List<int>;
            //List<int> productsIdSel = Session["ProdSelection"] as List<int>;

            if (productsIdSel == null)
                productsIdSel = new List<int>();

            // Se cruzan todos los registros de la pagina actual del gridview con la lista de seleccionados,
            // si algún item de esa página fue marcado previamente no se devuelve.
            productsIdSel = (from item in productsIdSel
                             join item2 in grid.Rows.Cast<GridViewRow>()
                             on item equals Convert.ToInt32(grid.DataKeys[item2.RowIndex].Value) into g
                             where !g.Any()
                             select item).ToList();

            // Se agregan los seleccionados
            productsIdSel.AddRange(checkedProd);

            HttpContext.Current.Session["ProdSelection"] = productsIdSel;
        }

        public static void RestoreSelection(GridView grid)
        {
            List<int> productsIdSel = HttpContext.Current.Session["ProdSelection"] as List<int>;

            if (productsIdSel == null)
                return;

            // se comparan los registros de la pagina del grid con los recuperados de la Session
            // los coincidentes se devuelven para ser seleccionados
            List<GridViewRow> result = (from item in grid.Rows.Cast<GridViewRow>()
                                        join item2 in productsIdSel
                                        on Convert.ToInt32(grid.DataKeys[item.RowIndex].Value) equals item2 into g
                                        where g.Any()
                                        select item).ToList();

            // se recorre cada item para marcarlo
            result.ForEach(x => ((CheckBox)x.FindControl("ChkEdicion")).Checked = true);
        }
    }
}