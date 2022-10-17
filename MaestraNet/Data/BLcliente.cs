using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MaestraNet.Entidad;
using System.Configuration;

namespace MaestraNet.Data
{
    public class BLcliente
    {
        public DataSet ListaCliente(string sRut, string sNombre, string sPaterno, string sMaterno)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdCliente = new SqlCommand();
            SqlDataAdapter daCliente = new SqlDataAdapter();
            DataSet dsCliente = new DataSet();
            int iResultado;

            cmdCliente.CommandText = "sp_VTA_ListaClientes";

            cmdCliente.CommandType = CommandType.StoredProcedure;

            cmdCliente.Connection = oConnection;

            cmdCliente.Parameters.Add("@Rut", SqlDbType.VarChar, 50).Value = sRut.Replace(".", "");
            cmdCliente.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = sNombre;
            cmdCliente.Parameters.Add("@Paterno", SqlDbType.VarChar, 50).Value = sPaterno;
            cmdCliente.Parameters.Add("@Materno", SqlDbType.VarChar, 50).Value = sMaterno;

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

        public int IngresaCliente(Cliente oCliente)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdCliente = new SqlCommand();
            int iResultado;

            cmdCliente.CommandText = "sp_VTA_Cliente_Fin700_Check_2";

            cmdCliente.CommandType = CommandType.StoredProcedure;

            cmdCliente.Connection = oConnection;

            cmdCliente.Parameters.Add("@Rut", SqlDbType.VarChar, 50).Value = oCliente.Clienterut.Replace(".", "");
            cmdCliente.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = oCliente.Nombres;
            cmdCliente.Parameters.Add("@Paterno", SqlDbType.VarChar, 50).Value = oCliente.Paterno;
            cmdCliente.Parameters.Add("@Materno", SqlDbType.VarChar, 50).Value = oCliente.Materno;
            cmdCliente.Parameters.Add("@Fono", SqlDbType.VarChar, 25).Value = oCliente.Fono;
            cmdCliente.Parameters.Add("@Direccion", SqlDbType.VarChar, 50).Value = oCliente.Direccion;
            cmdCliente.Parameters.Add("@Ciudad", SqlDbType.Int).Value = oCliente.CodCiudad;
            cmdCliente.Parameters.Add("@Comuna", SqlDbType.Int).Value = oCliente.CodComuna;
            cmdCliente.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = oCliente.Email;
            cmdCliente.Parameters.Add("@Inv", SqlDbType.Int).Value = oCliente.Inversionista;
            cmdCliente.Parameters.Add("@PJuridica", SqlDbType.Int).Value = oCliente.PersonaJuridica;
            cmdCliente.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = oCliente.UsuarioCreacion;
            cmdCliente.Parameters.Add("@IdNacionalidad", SqlDbType.Int).Value = oCliente.IdNacionalidad;
            cmdCliente.Parameters.Add("@IdEstadoCivil", SqlDbType.Int).Value = oCliente.IdEstadoCivil;
            cmdCliente.Parameters.Add("@TipoOcupacion", SqlDbType.Int).Value = oCliente.IdTipoOcupacion;
            cmdCliente.Parameters.Add("@Ocupacion", SqlDbType.VarChar).Value = oCliente.Ocupacion;
            cmdCliente.Parameters.Add("@NombreEmpresa", SqlDbType.VarChar).Value = oCliente.NombreEmpresa;
            cmdCliente.Parameters.Add("@DireccionEmpresa", SqlDbType.VarChar).Value = oCliente.DireccionEmpresa;
            cmdCliente.Parameters.Add("@TelefonoEmpresa", SqlDbType.VarChar).Value = oCliente.TelefonoEmpresa;
            cmdCliente.Parameters.Add("@NombreContacto", SqlDbType.VarChar).Value = oCliente.NombreContacto;
            cmdCliente.Parameters.Add("@TelefonoContacto", SqlDbType.VarChar).Value = oCliente.TeleFonoContacto;
            cmdCliente.Parameters.Add("@Sexo", SqlDbType.Char).Value = oCliente.Sexo;
            cmdCliente.Parameters.Add("@FechaNacimiento", SqlDbType.VarChar).Value = oCliente.FechaNacimiento;

            try
            {
                oConnection.Open();
                iResultado = (int)cmdCliente.ExecuteScalar();
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

        public DataSet ConsultaCliente(int idCliente)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdCliente = new SqlCommand();
            SqlDataAdapter daCliente = new SqlDataAdapter();
            DataSet dsCliente = new DataSet();

            cmdCliente.CommandText = "sp_VTA_ConsultaCliente";

            cmdCliente.CommandType = CommandType.StoredProcedure;

            cmdCliente.Connection = oConnection;

            cmdCliente.Parameters.Add("@idCliente", SqlDbType.VarChar, 50).Value = idCliente;

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

        public DataSet ConsultaPatrimonio(int idCliente)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdCliente = new SqlCommand();
            SqlDataAdapter daCliente = new SqlDataAdapter();
            DataSet dsCliente = new DataSet();
            

            cmdCliente.CommandText = "sp_VTA_Consulta_PatrimonioCliente";

            cmdCliente.CommandType = CommandType.StoredProcedure;

            cmdCliente.Connection = oConnection;

            cmdCliente.Parameters.Add("@idCliente", SqlDbType.Int).Value = idCliente;

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

        public DataSet ConsultaExperienciaInversora (int idCliente)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdCliente = new SqlCommand();
            SqlDataAdapter daCliente = new SqlDataAdapter();
            DataSet dsCliente = new DataSet();
            

            cmdCliente.CommandText = "sp_VTA_Consulta_Exp_InversoraCliente";

            cmdCliente.CommandType = CommandType.StoredProcedure;

            cmdCliente.Connection = oConnection;

            cmdCliente.Parameters.Add("@idCliente", SqlDbType.Int).Value = idCliente;

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

        public void IngresarPatrimonioCliente(Patrimonio oPatrimonio)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdCliente = new SqlCommand();
            int iResultado;

            cmdCliente.CommandText = "sp_VTA_Cliente_Patrimonio";

            cmdCliente.CommandType = CommandType.StoredProcedure;

            cmdCliente.Connection = oConnection;

            cmdCliente.Parameters.Add("@IdCliente", SqlDbType.Int).Value = oPatrimonio.IdCliente;
            cmdCliente.Parameters.Add("@A", SqlDbType.Int).Value = oPatrimonio.libreta;
            cmdCliente.Parameters.Add("@I", SqlDbType.Int).Value = oPatrimonio.Indemizacion;
            cmdCliente.Parameters.Add("@V", SqlDbType.Int).Value = oPatrimonio.VentasBienesRaices;
            cmdCliente.Parameters.Add("@AE", SqlDbType.Int).Value = oPatrimonio.ActividadEmpresarial;
            cmdCliente.Parameters.Add("@R", SqlDbType.Int).Value = oPatrimonio.RescateInversiones;
            cmdCliente.Parameters.Add("@H", SqlDbType.Int).Value = oPatrimonio.Herencias;
            cmdCliente.Parameters.Add("@obs", SqlDbType.VarChar, 50).Value = oPatrimonio.Observaciones;

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

        public void IngresarExperienciaInversora(ExperienciaInversora Inversora)
        {
            SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString);
            SqlCommand cmdCliente = new SqlCommand();

            cmdCliente.CommandText = "sp_VTA_Ingresa_ClienteExperienciaInversora";

            cmdCliente.CommandType = CommandType.StoredProcedure;

            cmdCliente.Connection = oConnection;

            cmdCliente.Parameters.Add("@IdCliente", SqlDbType.Int).Value = Inversora.IdCliente;
            cmdCliente.Parameters.Add("@Ahorro", SqlDbType.Int).Value = Inversora.Ahorro;
            cmdCliente.Parameters.Add("@Venta_Bienes_Raices", SqlDbType.Int).Value = Inversora.VentasBienesRaices;
            cmdCliente.Parameters.Add("@Herencia", SqlDbType.Int).Value = Inversora.Herencias;
            cmdCliente.Parameters.Add("@RescateInversion", SqlDbType.Int).Value = Inversora.RescateInversiones;
            cmdCliente.Parameters.Add("@IdPerfilInversor", SqlDbType.Int).Value = Inversora.IdPerfilInversor;
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
    }
}