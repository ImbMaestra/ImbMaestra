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
    public class BLInmueble
    {
        public DataSet ListaInmueble(int iIdProyecto, int iIdTipoModelo, string sTorre, int iNumero, int iModeloInmueble, int iPiso)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdInmueble = new SqlCommand();
            SqlDataAdapter daInmueble = new SqlDataAdapter();
            DataSet dsInmueble = new DataSet();

            cmdInmueble.CommandText = "sp_VTA_ListaInmueble";

            cmdInmueble.CommandType = CommandType.StoredProcedure;

            cmdInmueble.Connection = oConnection;

            cmdInmueble.Parameters.Add("@idproyecto", SqlDbType.Int).Value = iIdProyecto;
            cmdInmueble.Parameters.Add("@idTipoInmueble", SqlDbType.Int).Value = iIdTipoModelo;
            cmdInmueble.Parameters.Add("@Edificio", SqlDbType.VarChar).Value = sTorre;
            cmdInmueble.Parameters.Add("@Numero", SqlDbType.Int).Value = iNumero;
            cmdInmueble.Parameters.Add("@idModeloInmueble", SqlDbType.Int).Value = iModeloInmueble;
            cmdInmueble.Parameters.Add("@Piso", SqlDbType.Int).Value = iPiso;

            daInmueble.SelectCommand = cmdInmueble;



            try
            {
                oConnection.Open();
                daInmueble.Fill(dsInmueble);
                return dsInmueble;

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

        public DataSet ListaInmueble2(int iIdProyecto, int iIdTipoModelo, string sTorre, int iNumero, int iModeloInmueble, int iPiso, string sOrientacion)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdInmueble = new SqlCommand();
            SqlDataAdapter daInmueble = new SqlDataAdapter();
            DataSet dsInmueble = new DataSet();

            cmdInmueble.CommandText = "sp_VTA_ListaInmueble2";

            cmdInmueble.CommandType = CommandType.StoredProcedure;

            cmdInmueble.Connection = oConnection;

            cmdInmueble.Parameters.Add("@idproyecto", SqlDbType.Int).Value = iIdProyecto;
            cmdInmueble.Parameters.Add("@idTipoInmueble", SqlDbType.Int).Value = iIdTipoModelo;
            cmdInmueble.Parameters.Add("@Edificio", SqlDbType.VarChar).Value = sTorre;
            cmdInmueble.Parameters.Add("@Numero", SqlDbType.Int).Value = iNumero;
            cmdInmueble.Parameters.Add("@idModeloInmueble", SqlDbType.Int).Value = iModeloInmueble;
            cmdInmueble.Parameters.Add("@Piso", SqlDbType.Int).Value = iPiso;
            cmdInmueble.Parameters.Add("@Orientacion", SqlDbType.VarChar).Value = sOrientacion;

            daInmueble.SelectCommand = cmdInmueble;



            try
            {
                oConnection.Open();
                daInmueble.Fill(dsInmueble);
                return dsInmueble;

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

        public void IngresaInmueble(int iIdProyecto, DataTable dtInmueble)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdProyecto = new SqlCommand();
            SqlTransaction transaction;
            int iResultado;


            cmdProyecto.Connection = oConnection;
            oConnection.Open();
            transaction = oConnection.BeginTransaction();
            cmdProyecto.Transaction = transaction;


            cmdProyecto.CommandText = "sp_VTA_EliminaInmuebles";
            cmdProyecto.CommandType = CommandType.StoredProcedure;
            cmdProyecto.Parameters.Add("@idProyecto", SqlDbType.Int).Value = iIdProyecto;

            try
            {
                cmdProyecto.ExecuteNonQuery();

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
                finally
                {
                    oConnection.Close();
                }
            }
            cmdProyecto.Parameters.Clear();
            foreach (DataRow oRow in dtInmueble.Rows)
            {

                cmdProyecto.CommandText = "sp_VTA_IngresaInmueble";

                cmdProyecto.CommandType = CommandType.StoredProcedure;
                try
                {

                    cmdProyecto.Parameters.Add("@idProyecto", SqlDbType.Int).Value = iIdProyecto;
                    cmdProyecto.Parameters.Add("@idModeloInmueble", SqlDbType.Int).Value = oRow["modelo"];
                    cmdProyecto.Parameters.Add("@Piso", SqlDbType.VarChar, 150).Value = oRow["Piso"];
                    cmdProyecto.Parameters.Add("@ndepto", SqlDbType.Int).Value = oRow["ndepto"];
                    cmdProyecto.Parameters.Add("@edificio", SqlDbType.VarChar,15).Value = oRow["edificio"];
                    cmdProyecto.Parameters.Add("@orientacion", SqlDbType.VarChar, 50).Value = oRow["orientacion"];
                    cmdProyecto.Parameters.Add("@DeptoUtil", SqlDbType.Float).    Value = oRow["DeptoUtil"];
                    cmdProyecto.Parameters.Add("@Balcon", SqlDbType.Float).Value = string.IsNullOrEmpty(oRow["Balcon"].ToString()) ? "0" : oRow["Balcon"];
                    cmdProyecto.Parameters.Add("@Logia", SqlDbType.Float).Value = string.IsNullOrEmpty(oRow["Logia"].ToString()) ? "0" : oRow["Balcon"];
                    cmdProyecto.Parameters.Add("@PrecioLista", SqlDbType.Int).Value = oRow["PrecioLista"];
                    cmdProyecto.Parameters.Add("@Observacion", SqlDbType.VarChar,50).Value = oRow["Observacion"].ToString();
                    cmdProyecto.Parameters.Add("@estadoinmueble", SqlDbType.Int).Value = oRow["estadoinmueble"];
                    cmdProyecto.Parameters.Add("@idInmuebleTemp", SqlDbType.Int).Value = oRow["idinmueble"];
                    cmdProyecto.Parameters.Add("@IdInmueble_PackTemp", SqlDbType.Int).Value = oRow["idpack"];
                    cmdProyecto.Parameters.Add("@usogoce", SqlDbType.Bit).Value = oRow["usogoce"];

                    iResultado = (int)cmdProyecto.ExecuteScalar();

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
                    finally
                    {
                        oConnection.Close();
                    }

                }
                cmdProyecto.Parameters.Clear();
            }
            cmdProyecto.Parameters.Clear();
            cmdProyecto.CommandText = "sp_VTA_UpdateInmuebleTemp";
            cmdProyecto.CommandType = CommandType.StoredProcedure;
            try
            {


                cmdProyecto.ExecuteNonQuery();

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
                finally
                {
                    oConnection.Close();
                }

            }

            transaction.Commit();
            oConnection.Close();
        }
        
        public DataSet ConsultaInmueble(int idInmueble)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdInmueble = new SqlCommand();
            SqlDataAdapter daInmueble = new SqlDataAdapter();
            DataSet dsInmueble = new DataSet();


            cmdInmueble.CommandText = "[sp_VTA_ConsultaInmueble]";

            cmdInmueble.CommandType = CommandType.StoredProcedure;

            cmdInmueble.Connection = oConnection;

            cmdInmueble.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = idInmueble;

            daInmueble.SelectCommand = cmdInmueble;

            try
            {
                oConnection.Open();
                daInmueble.Fill(dsInmueble);
                return dsInmueble;

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

        public void ModificarInmueble(Inmueble oInmueble)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdInmueble = new SqlCommand();


            cmdInmueble.CommandText = "sp_VTA_ModificaInmueble";

            cmdInmueble.CommandType = CommandType.StoredProcedure;

            cmdInmueble.Connection = oConnection;

            cmdInmueble.Parameters.Add("@IdProyecto", SqlDbType.Int).Value = oInmueble.IdProyecto;
            cmdInmueble.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = oInmueble.IdInmueble;
            cmdInmueble.Parameters.Add("@IdModeloInmueble", SqlDbType.Int).Value = oInmueble.IdModeloInmueble;
            cmdInmueble.Parameters.Add("@Piso", SqlDbType.Int).Value = oInmueble.Piso;
            cmdInmueble.Parameters.Add("@Edificio", SqlDbType.VarChar, 15).Value = oInmueble.Edificio;
            cmdInmueble.Parameters.Add("@Observacion", SqlDbType.VarChar, 50).Value = oInmueble.Observacion;
            cmdInmueble.Parameters.Add("@numero", SqlDbType.Int).Value = oInmueble.NDepto;
            cmdInmueble.Parameters.Add("@m2terreno", SqlDbType.Float).Value = oInmueble.Terraza;
            cmdInmueble.Parameters.Add("@m2", SqlDbType.Float).Value = oInmueble.M2Util;
            cmdInmueble.Parameters.Add("@orientacion", SqlDbType.VarChar, 50).Value = oInmueble.Orientacion;
            cmdInmueble.Parameters.Add("@preciolista", SqlDbType.Int).Value = oInmueble.PrecioLista;
            cmdInmueble.Parameters.Add("@estadoinmueble", SqlDbType.Int).Value = oInmueble.IdEstadoInmueble;
            cmdInmueble.Parameters.Add("@logia", SqlDbType.Float).Value = oInmueble.Logia;
            cmdInmueble.Parameters.Add("@usuario", SqlDbType.VarChar, 30).Value = oInmueble.Usuario;
            cmdInmueble.Parameters.Add("@Alicuota", SqlDbType.Float).Value = oInmueble.Alicuota;
            cmdInmueble.Parameters.Add("@NumeroRol", SqlDbType.VarChar, 30).Value = oInmueble.NumeroRol;

            try
            {
                oConnection.Open();
                cmdInmueble.ExecuteNonQuery();

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

        public void ModificarPrecios(int iIdProyecto, string sModeloInmuebles, string sPisos, string sTorres, string  sOrientacion, int iDepto, string sSubeBaja, string sPorcentajePrecio, int iValor, string sUsuario)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdInmueble = new SqlCommand();


            cmdInmueble.CommandText = "sp_updatePrecioInmueble2";

            cmdInmueble.CommandType = CommandType.StoredProcedure;

            cmdInmueble.Connection = oConnection;

            cmdInmueble.Parameters.Add("@idProyecto", SqlDbType.Int).Value = iIdProyecto;
            cmdInmueble.Parameters.Add("@ModeloInmueble", SqlDbType.Int).Value = sModeloInmuebles;
            cmdInmueble.Parameters.Add("@Piso", SqlDbType.VarChar, 20).Value = sPisos;
            cmdInmueble.Parameters.Add("@Torre", SqlDbType.VarChar, 15).Value = sTorres;
            cmdInmueble.Parameters.Add("@Orientacion", SqlDbType.Int).Value = sOrientacion;
            cmdInmueble.Parameters.Add("@ndepto", SqlDbType.Int).Value = iDepto;
            cmdInmueble.Parameters.Add("@sube", SqlDbType.VarChar, 1).Value = sSubeBaja;
            cmdInmueble.Parameters.Add("@porcenaje_precio", SqlDbType.VarChar, 2).Value = sPorcentajePrecio;
            cmdInmueble.Parameters.Add("@Valor", SqlDbType.Int).Value = iValor;
            cmdInmueble.Parameters.Add("@Usuario", SqlDbType.VarChar, 30).Value = sUsuario;

            try
            {
                oConnection.Open();
                cmdInmueble.ExecuteNonQuery();

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
        public void ModificaPreciosGrupo(DataTable dtInmueble, string Usuario)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdProyecto = new SqlCommand();
            SqlTransaction transaction;
            int iResultado;


            cmdProyecto.Connection = oConnection;
            oConnection.Open();
            transaction = oConnection.BeginTransaction();
            cmdProyecto.Transaction = transaction;
            foreach (DataRow oRow in dtInmueble.Rows)
            {

                cmdProyecto.CommandText = "sp_CambiaPrecioInmueble";

                cmdProyecto.CommandType = CommandType.StoredProcedure;

                cmdProyecto.Parameters.Add("@idInmueble", SqlDbType.Int).Value = oRow["idInmueble"];
                cmdProyecto.Parameters.Add("@PrecioLista", SqlDbType.Int).Value = oRow["PrecioLista"];
                cmdProyecto.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = Usuario;
                try
                {


                    iResultado = (int)cmdProyecto.ExecuteScalar();

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
                cmdProyecto.Parameters.Clear();

            }
            transaction.Commit();

        }
        public DataSet PisosInmueble(int iIdProyecto, string sIdModeloInmueble)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdPisos = new SqlCommand();
            SqlDataAdapter daPisos = new SqlDataAdapter();
            DataSet dsPisos = new DataSet();


            cmdPisos.CommandText = "sp_VTA_PisoModeloInmueble";

            cmdPisos.CommandType = CommandType.StoredProcedure;

            cmdPisos.Connection = oConnection;

            cmdPisos.Parameters.Add("@idProyecto", SqlDbType.Int).Value = iIdProyecto;
            cmdPisos.Parameters.Add("@IdModeloInmueble", SqlDbType.VarChar, 50).Value = sIdModeloInmueble;

            daPisos.SelectCommand = cmdPisos;

            try
            {
                oConnection.Open();
                daPisos.Fill(dsPisos);
                return dsPisos;

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

        public DataSet ModeloInmueble(int iIdTipoInmueble)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdPisos = new SqlCommand();
            SqlDataAdapter daPisos = new SqlDataAdapter();
            DataSet dsPisos = new DataSet();


            cmdPisos.CommandText = "sp_VTA_ModeloInmueble";

            cmdPisos.CommandType = CommandType.StoredProcedure;

            cmdPisos.Connection = oConnection;

            cmdPisos.Parameters.Add("@idTipoInmueble", SqlDbType.Int).Value = iIdTipoInmueble;

            daPisos.SelectCommand = cmdPisos;

            try
            {
                oConnection.Open();
                daPisos.Fill(dsPisos);
                return dsPisos;

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

        public DataSet FiltraInmueble(int iIdProyecto, string sModeloInmueble, string sPiso, string sTorre, string sOrientacion, int iDepto, string sSubeBaja, string sPorcentajePrecio, int iValor)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdInmueble = new SqlCommand();
            SqlDataAdapter daInmueble = new SqlDataAdapter();
            DataSet dsInmueble = new DataSet();

            cmdInmueble.CommandText = "sp_BuscaInmuebles";

            cmdInmueble.CommandType = CommandType.StoredProcedure;

            cmdInmueble.Connection = oConnection;

            cmdInmueble.Parameters.Add("@idProyecto", SqlDbType.Int).Value = iIdProyecto;
            cmdInmueble.Parameters.Add("@ModeloInmueble", SqlDbType.VarChar, 20).Value = sModeloInmueble;
            cmdInmueble.Parameters.Add("@Piso", SqlDbType.VarChar, 200).Value = sPiso;
            cmdInmueble.Parameters.Add("@Torre", SqlDbType.VarChar, 20).Value = sTorre;
            cmdInmueble.Parameters.Add("@Orientacion", SqlDbType.VarChar, 20).Value = sOrientacion;
            cmdInmueble.Parameters.Add("@ndepto", SqlDbType.Int).Value = iDepto;
            cmdInmueble.Parameters.Add("@sube", SqlDbType.VarChar, 1).Value = sSubeBaja;
            cmdInmueble.Parameters.Add("@porcenaje_precio", SqlDbType.VarChar,2).Value = sPorcentajePrecio;
            cmdInmueble.Parameters.Add("@Valor", SqlDbType.Int).Value = iValor;

            daInmueble.SelectCommand = cmdInmueble;



            try
            {
                oConnection.Open();
                daInmueble.Fill(dsInmueble);
                return dsInmueble;

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
        public int ConsultaCotizacionProyecto(int idProyecto)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdProyecto = new SqlCommand();
            SqlDataAdapter daInmueble = new SqlDataAdapter();
            int iResultado;

            cmdProyecto.CommandText = "sp_VTA_consultaCotizacionProyecto";

            cmdProyecto.CommandType = CommandType.StoredProcedure;
            cmdProyecto.Connection = oConnection;

            cmdProyecto.Parameters.Add("@idProyecto", SqlDbType.Int).Value = idProyecto;
           
            try
            {

                oConnection.Open();
                iResultado = (int)cmdProyecto.ExecuteScalar();
                return iResultado;

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
        public void EliminarInmueble(int iIdInmueble)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdInmueble = new SqlCommand();


            cmdInmueble.CommandText = "sp_VTA_EliminarInmueble";

            cmdInmueble.CommandType = CommandType.StoredProcedure;

            cmdInmueble.Connection = oConnection;

            cmdInmueble.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = iIdInmueble;

            try
            {
                oConnection.Open();
                cmdInmueble.ExecuteNonQuery();

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
        public int ValidaInmueble (int iIdInmueble)
        {
            int iInmueble;
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdInmueble = new SqlCommand();


            cmdInmueble.CommandText = "sp_VTA_ProyectoInmueble";

            cmdInmueble.CommandType = CommandType.StoredProcedure;

            cmdInmueble.Connection = oConnection;

            cmdInmueble.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = iIdInmueble;   

            try
            {
                oConnection.Open();
                iInmueble=(int)cmdInmueble.ExecuteScalar();
                return iInmueble;
 
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

        public void UpdateInmueblePack (int iIdinmueble, int iIdInmueblePack)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdInmueble = new SqlCommand();


            cmdInmueble.CommandText = "sp_VTA_UpdateInmueblePack";

            cmdInmueble.CommandType = CommandType.StoredProcedure;

            cmdInmueble.Connection = oConnection;

            cmdInmueble.Parameters.Add("@idInmueble", SqlDbType.Int).Value = iIdinmueble;
            cmdInmueble.Parameters.Add("@idInmueblePack", SqlDbType.Int).Value = iIdInmueblePack;

            try
            {
                oConnection.Open();
                cmdInmueble.ExecuteNonQuery();

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

        public DataSet BuscaTipoInmueble(string sTipoProyecto)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdCliente = new SqlCommand();
            SqlDataAdapter daCliente = new SqlDataAdapter();
            DataSet dsCliente = new DataSet();

            cmdCliente.CommandText = "sp_BuscaTipoInmueble";

            cmdCliente.CommandType = CommandType.StoredProcedure;

            cmdCliente.Connection = oConnection;

            cmdCliente.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = sTipoProyecto;

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

        public void IngresaTipoInmueble(Inmueble oInmueble)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdProyecto = new SqlCommand();
            int iResultado;

            cmdProyecto.CommandText = "sp_VTA_TipoInmuebleModifica";

            cmdProyecto.CommandType = CommandType.StoredProcedure;

            cmdProyecto.Connection = oConnection;

            cmdProyecto.Parameters.Add("@IdTipoInmueble", SqlDbType.Int).Value = oInmueble.IdTipoInmueble;
            cmdProyecto.Parameters.Add("@Nombre", SqlDbType.Int).Value = oInmueble.sNombreTipoInmueble;
            cmdProyecto.Parameters.Add("@CdiId", SqlDbType.Int).Value = oInmueble.iCdiId;
            cmdProyecto.Parameters.Add("@ServicioId", SqlDbType.Int).Value = oInmueble.iServicioId;

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

        public DataSet ListaTipoInmueble(int sTipoProyecto)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdCliente = new SqlCommand();
            SqlDataAdapter daCliente = new SqlDataAdapter();
            DataSet dsCliente = new DataSet();

            cmdCliente.CommandText = "sp_ListaTipoInmueble";

            cmdCliente.CommandType = CommandType.StoredProcedure;

            cmdCliente.Connection = oConnection;

            cmdCliente.Parameters.Add("@IdTipoInmueble", SqlDbType.VarChar, 50).Value = sTipoProyecto;

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

        public void EliminarTipoInmueble(int iIdInmueble)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdInmueble = new SqlCommand();


            cmdInmueble.CommandText = "sp_VTA_EliminarInmueble";

            cmdInmueble.CommandType = CommandType.StoredProcedure;

            cmdInmueble.Connection = oConnection;

            cmdInmueble.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = iIdInmueble;

            try
            {
                oConnection.Open();
                cmdInmueble.ExecuteNonQuery();

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

        public void ModificarInmuebleMasivo(Inmueble oInmueble, List<int> listado)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdInmueble = new SqlCommand();

            cmdInmueble.Connection = oConnection;

            try
            {
                oConnection.Open();

                foreach (var item in listado)
                {
                    cmdInmueble.CommandText = "sp_VTA_ModificaInmuebleMasivo";

                    cmdInmueble.CommandType = CommandType.StoredProcedure;

                    cmdInmueble.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = item;
                    cmdInmueble.Parameters.Add("@m2terreno", SqlDbType.VarChar).Value = oInmueble.Terraza;
                    cmdInmueble.Parameters.Add("@m2", SqlDbType.VarChar).Value = oInmueble.M2Util;
                    cmdInmueble.Parameters.Add("@preciolista", SqlDbType.Int).Value = oInmueble.PrecioLista;
                    cmdInmueble.Parameters.Add("@TipoPrecioLista", SqlDbType.VarChar).Value = oInmueble.TipoPrecioLista;
                    cmdInmueble.Parameters.Add("@IdEstadoInmueble", SqlDbType.Int).Value = oInmueble.IdEstadoInmueble;
                    cmdInmueble.Parameters.Add("@JustificacionTipoInmueble", SqlDbType.VarChar).Value = oInmueble.JustificacionTipoInmueble;
                    cmdInmueble.Parameters.Add("@Alicuota", SqlDbType.VarChar).Value = oInmueble.Alicuota;
                    cmdInmueble.Parameters.Add("@NumeroRol", SqlDbType.VarChar).Value = oInmueble.NumeroRol;
                    cmdInmueble.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = oInmueble.Usuario;
                    cmdInmueble.ExecuteNonQuery();
                    cmdInmueble.Parameters.Clear();
                }
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