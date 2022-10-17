using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MaestraNet.Entidad;
namespace MaestraNet.Data
{
    public class BLProyecto
    {
        public DataSet ListaProyecto(int iRegion, int iComuna, string sProyecto, int iEstado)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdCliente = new SqlCommand();
            SqlDataAdapter daCliente = new SqlDataAdapter();
            DataSet dsCliente = new DataSet();

            cmdCliente.CommandText = "sp_VTA_ListaProyectos";

            cmdCliente.CommandType = CommandType.StoredProcedure;

            cmdCliente.Connection = oConnection;

            cmdCliente.Parameters.Add("@Region", SqlDbType.Int).Value = iRegion;
            cmdCliente.Parameters.Add("@Comuna", SqlDbType.Int).Value = iComuna;
            cmdCliente.Parameters.Add("@Proyecto", SqlDbType.VarChar,50).Value = sProyecto;
            cmdCliente.Parameters.Add("@Estado", SqlDbType.Int).Value = iEstado;

            daCliente.SelectCommand = cmdCliente;



            try
            {
                oConnection.Open();
                daCliente.Fill(dsCliente);
                return dsCliente;

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

        public void IngresaProyecto(Proyecto oProyecto)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdProyecto = new SqlCommand();
            int iResultado;

            cmdProyecto.CommandText = "sp_VTA_ProyectoCreaModificaNET";

            cmdProyecto.CommandType = CommandType.StoredProcedure;

            cmdProyecto.Connection = oConnection;

            cmdProyecto.Parameters.Add("@idProyecto", SqlDbType.Int).Value = oProyecto.IdProyecto;
            cmdProyecto.Parameters.Add("@idsalaventa", SqlDbType.Int).Value = oProyecto.IdSalaVenta;
            cmdProyecto.Parameters.Add("@idregion", SqlDbType.Int).Value = oProyecto.IdRegion;
            cmdProyecto.Parameters.Add("@idcomuna", SqlDbType.Int).Value = oProyecto.IdComuna;
            cmdProyecto.Parameters.Add("@idempresa", SqlDbType.Int).Value = oProyecto.IdEmpresa;
            cmdProyecto.Parameters.Add("@idEstadoEntrega", SqlDbType.Int).Value = oProyecto.IdEstadoEntrega;
            cmdProyecto.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = oProyecto.NombreProyecto;
            cmdProyecto.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = oProyecto.Email;
            cmdProyecto.Parameters.Add("@direccion", SqlDbType.VarChar, 80).Value = oProyecto.Direccion;
            cmdProyecto.Parameters.Add("@FechaInicioVenta", SqlDbType.Date).Value = oProyecto.FechaInicioVenta;
            cmdProyecto.Parameters.Add("@FechaRecepcion", SqlDbType.Date).Value = oProyecto.FechaRecepcion;
            cmdProyecto.Parameters.Add("@M2", SqlDbType.Float).Value = oProyecto.MetroCuadrados;
            cmdProyecto.Parameters.Add("@longitud", SqlDbType.VarChar, 100).Value = oProyecto.Longitud;
            cmdProyecto.Parameters.Add("@latitud", SqlDbType.VarChar, 100).Value = oProyecto.Latitud;
            cmdProyecto.Parameters.Add("@Division", SqlDbType.Int).Value = oProyecto.CodigoDivision;
            cmdProyecto.Parameters.Add("@ValorTerreno", SqlDbType.Float).Value = oProyecto.ValorTerreno;

            try
            {
                oConnection.Open();
                iResultado = (int)cmdProyecto.ExecuteScalar();

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
        public DataSet ConsultaProyecto(int idProyecto)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdProyecto = new SqlCommand();
            SqlDataAdapter daProyecto= new SqlDataAdapter();
            DataSet dsProyecto = new DataSet();
            

            cmdProyecto.CommandText = "[sp_VTA_Proyecto_Busca_todo]";

            cmdProyecto.CommandType = CommandType.StoredProcedure;

            cmdProyecto.Connection = oConnection;

            cmdProyecto.Parameters.Add("@idProyecto", SqlDbType.Int).Value = idProyecto;

            daProyecto.SelectCommand = cmdProyecto;

            try
            {
                oConnection.Open();
                daProyecto.Fill(dsProyecto);
                return dsProyecto;

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

        public DataSet TorresProyecto(int iIdProyecto)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdTorres = new SqlCommand();
            SqlDataAdapter daTorres = new SqlDataAdapter();
            DataSet dsTorres = new DataSet();


            cmdTorres.CommandText = "sp_VTA_TorresProyecto";

            cmdTorres.CommandType = CommandType.StoredProcedure;

            cmdTorres.Connection = oConnection;

            cmdTorres.Parameters.Add("@idProyecto", SqlDbType.Int).Value = iIdProyecto;

            daTorres.SelectCommand = cmdTorres;

            try
            {
                oConnection.Open();
                daTorres.Fill(dsTorres);
                return dsTorres;

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
        public DataSet DivisionEmpresa(int iIdEmpresa)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdTorres = new SqlCommand();
            SqlDataAdapter daTorres = new SqlDataAdapter();
            DataSet dsTorres = new DataSet();


            cmdTorres.CommandText = "sp_VTA_DivisionEmpresa";

            cmdTorres.CommandType = CommandType.StoredProcedure;

            cmdTorres.Connection = oConnection;

            cmdTorres.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = iIdEmpresa;

            daTorres.SelectCommand = cmdTorres;

            try
            {
                oConnection.Open();
                daTorres.Fill(dsTorres);
                return dsTorres;

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