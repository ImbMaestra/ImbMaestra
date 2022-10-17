using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MaestraNet.Data
{
    public class BLPerfiles
    {
        public DataSet PerfilBotones(string sPerfil, string sPagina)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdBotones = new SqlCommand();
            SqlDataAdapter daBotones = new SqlDataAdapter();
            DataSet dsBotones = new DataSet();


            cmdBotones.CommandText = "sp_VTA_PerfilBotones";

            cmdBotones.CommandType = CommandType.StoredProcedure;

            cmdBotones.Connection = oConnection;

            cmdBotones.Parameters.Add("@Perfil", SqlDbType.VarChar, 20).Value = sPerfil;
            cmdBotones.Parameters.Add("@Pagina", SqlDbType.VarChar, 40).Value = sPagina;
            daBotones.SelectCommand = cmdBotones;



            try
            {
                oConnection.Open();
                daBotones.Fill(dsBotones);
                return dsBotones;

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

        public void GrabarAcceso (DataTable dtAccesos, string sSistema, string sPerfil,string sPagina)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdAcceso= new SqlCommand();
            SqlTransaction transaction;
            int iResultado;


            cmdAcceso.Connection = oConnection;
            oConnection.Open();
            transaction = oConnection.BeginTransaction();
            cmdAcceso.Transaction = transaction;

            cmdAcceso.CommandText = "sp_vta_EliminarAcceso";
            cmdAcceso.CommandType = CommandType.StoredProcedure;

            cmdAcceso.Parameters.Add("@idSistema", SqlDbType.VarChar, 40).Value = sSistema;
            cmdAcceso.Parameters.Add("@idPerfil", SqlDbType.VarChar, 20).Value = sPerfil;
            cmdAcceso.Parameters.Add("@idPagina", SqlDbType.VarChar, 40).Value = sPagina;

            try
            {
                iResultado = (int)cmdAcceso.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
                catch (Exception ex2)
                {
                    throw new Exception(ex2.Message);
                }
            }
            cmdAcceso.Parameters.Clear();


            foreach (DataRow oRow in dtAccesos.Rows)
            {

                cmdAcceso.CommandText = "sp_vta_CreaAcceso";

                cmdAcceso.CommandType = CommandType.StoredProcedure;

                cmdAcceso.Parameters.Add("@idSistema", SqlDbType.VarChar,40).Value = sSistema;
                cmdAcceso.Parameters.Add("@idPerfil", SqlDbType.VarChar,20).Value = sPerfil;
                cmdAcceso.Parameters.Add("@idPagina", SqlDbType.VarChar, 40).Value = oRow["idPagina"];
                cmdAcceso.Parameters.Add("@idBoton", SqlDbType.VarChar, 30).Value = oRow["idBoton"];

                try
                {
                    iResultado = (int)cmdAcceso.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception(ex2.Message);
                    }

                }
                cmdAcceso.Parameters.Clear();
            }
            transaction.Commit();
        }

        public DataTable ListaPaginas(string sIdSistemas)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdPaginas = new SqlCommand();
            SqlDataAdapter daPaginas = new SqlDataAdapter();
            DataTable dtPaginas = new DataTable();

            cmdPaginas.CommandText = "sp_vta_ListaPaginas";

            cmdPaginas.CommandType = CommandType.StoredProcedure;

            cmdPaginas.Connection = oConnection;

            cmdPaginas.Parameters.Add("@idSistema", SqlDbType.VarChar, 40).Value = sIdSistemas;

            daPaginas.SelectCommand = cmdPaginas;



            try
            {
                oConnection.Open();
                daPaginas.Fill(dtPaginas);
                return dtPaginas;

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

        public DataTable ListaPerfiles()
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdPerfiles = new SqlCommand();
            SqlDataAdapter daPerfiles = new SqlDataAdapter();
            DataTable dtPerfiles = new DataTable();

            cmdPerfiles.CommandText = "SP_VTA_PerfilesVenta";

            cmdPerfiles.CommandType = CommandType.StoredProcedure;

            cmdPerfiles.Connection = oConnection;

            daPerfiles.SelectCommand = cmdPerfiles;



            try
            {
                oConnection.Open();
                daPerfiles.Fill(dtPerfiles);
                return dtPerfiles;

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

        public DataTable ConsultaAcceso(string sSistema, string sPerfil, string sPagina)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdAcceso = new SqlCommand();
            SqlDataAdapter daPerfiles = new SqlDataAdapter();
            DataTable dtPerfiles = new DataTable();

            cmdAcceso.CommandText = "sp_vta_ConsultarAcceso";

            cmdAcceso.CommandType = CommandType.StoredProcedure;

            cmdAcceso.Parameters.Add("@idSistema", SqlDbType.VarChar, 40).Value = sSistema;
            cmdAcceso.Parameters.Add("@idPerfil", SqlDbType.VarChar, 20).Value = sPerfil;
            cmdAcceso.Parameters.Add("@idPagina", SqlDbType.VarChar, 40).Value = sPagina;

            cmdAcceso.Connection = oConnection;

            daPerfiles.SelectCommand = cmdAcceso;

            try
            {
                oConnection.Open();
                daPerfiles.Fill(dtPerfiles);
                return dtPerfiles;

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

        public DataTable DescuentoXperfil()
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdDescuento = new SqlCommand();
            SqlDataAdapter daDescuento = new SqlDataAdapter();
            DataTable dtDescuento = new DataTable();

            cmdDescuento.CommandText = "sp_VTA_descuentoXperfil";

            cmdDescuento.CommandType = CommandType.StoredProcedure;

            cmdDescuento.Connection = oConnection;

            daDescuento.SelectCommand = cmdDescuento;

            try
            {
                oConnection.Open();
                daDescuento.Fill(dtDescuento);
                return dtDescuento;

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

        public DataSet ConsultaPerfiles(string sNombre, string sPerfil)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdBotones = new SqlCommand();
            SqlDataAdapter daBotones = new SqlDataAdapter();
            DataSet dsBotones = new DataSet();


            cmdBotones.CommandText = "sp_VTA_Consulta_Perfiles";

            cmdBotones.CommandType = CommandType.StoredProcedure;

            cmdBotones.Connection = oConnection;

            cmdBotones.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = sNombre;
            cmdBotones.Parameters.Add("@IdPerfil", SqlDbType.VarChar, 50).Value = sPerfil;
            daBotones.SelectCommand = cmdBotones;



            try
            {
                oConnection.Open();
                daBotones.Fill(dsBotones);
                return dsBotones;

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