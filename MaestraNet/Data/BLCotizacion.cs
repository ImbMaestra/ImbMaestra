using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MaestraNet.Data
{
    public class BLCotizacion
    {
        public void EnviarCotizacion (int IidCotizacion, string sCorreo, string sPDF)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdCliente = new SqlCommand();

            cmdCliente.CommandText = "sp_VTA_EnviaCotizacion";

            cmdCliente.CommandType = CommandType.StoredProcedure;

            cmdCliente.Connection = oConnection;

            cmdCliente.Parameters.Add("@id", SqlDbType.Int).Value = IidCotizacion;
            cmdCliente.Parameters.Add("@correo", SqlDbType.VarChar,20).Value = sCorreo;
            cmdCliente.Parameters.Add("@pdf", SqlDbType.VarChar,100).Value = sPDF;
            try
            {
                oConnection.Open();
                cmdCliente.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oConnection.Close();
            }
        }

        public Boolean VBcotizacion(int iIdCotizacion)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdDescuento = new SqlCommand();
            SqlDataAdapter daDescuento = new SqlDataAdapter();
            DataTable dtDescuento = new DataTable();
            int iVB = 0;

            cmdDescuento.CommandText = "sp_VTA_VBCotizacion";

            cmdDescuento.CommandType = CommandType.StoredProcedure;

            cmdDescuento.Parameters.Add("@idCotizacion", SqlDbType.Int).Value = iIdCotizacion;

            cmdDescuento.Connection = oConnection;

            daDescuento.SelectCommand = cmdDescuento;

            try
            {
                oConnection.Open();
                iVB=(int)cmdDescuento.ExecuteScalar();
                if (iVB == 1)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oConnection.Close();
            }
        }
    }
}