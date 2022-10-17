using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Linq;
using System.Data.SqlClient;

namespace MaestraNet.cs
{
    public class dbTools
    {

        public string[] ErrosMSG;
		public List<string> ListaErrores;

		public string ResultQueryRun(string sQuery)
        {
            string ret = "";
            ErrosMSG = null;
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString;
                conn.Open();
                conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
                conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage2);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sQuery;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();


                while (rs.Read())
                {
                    ret = rs.GetValue(0).ToString();
                }

                rs.Close();
                //conn.Close();
            }
            catch (Exception err)
            {
                ret = "Error causado por " + err.Message.ToString();
            }

            return ret;
        }

        private void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            List<string> ListaErrors = new List<string>();

            foreach ( SqlError err in  e.Errors)
            {
                ListaErrors.Add(err.Message.ToString());
            }

            ErrosMSG = ListaErrors.ToArray();
        }


		private void conn_InfoMessage2(object sender, SqlInfoMessageEventArgs e)
		{
			List<string> ListaErrors2 = new List<string>();
			foreach (SqlError err in e.Errors)
			{
				ListaErrors2.Add(err.Message.ToString());
			}
			ListaErrores = ListaErrors2;
		}


		public string ResultQueryRun(string sQuery, bool flag)
        {
            string ret = "";
            ErrosMSG = null;
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString;
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = conn;
                conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
                cmd.CommandText = sQuery;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();

                if (flag)
                {
                    rs.NextResult();
                }

                while (rs.Read())
                {
                    ret = rs.GetValue(0).ToString();
                }


                rs.Close();
                conn.Close();
            }
            catch (Exception err)
            {
                ret = "Error causado por " + err.Message.ToString();
            }
            return ret;
        }

        public DataTable ResultQuery(string sQuery)
        {
            ErrosMSG = null;

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString;
            conn.Open();
            conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sQuery;
            System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(rs);

            rs.Close();
            conn.Close();

            return dataTable;
        }

        public SqlDataReader sqlRS (string sQuery)
        {
            ErrosMSG = null;

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString;
            conn.Open();
            conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sQuery;
            System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //conn.Close();
            return rs;

        }

        public string Nombre_Usuario(int Rut)
        {
            string ret = "";
            dbTools db = new dbTools();
            string squery = "exec dbo.sp_Nombre_Usuario_Rut '" + Rut.ToString() + "'";
            ret = db.ResultQueryRun(squery);
            return ret;
        }

        public string Nombre_Usuario(string IdUsuario)
        {
            string ret = "";
            dbTools db = new dbTools();
            ret = db.ResultQueryRun("exec dbo.sp_Nombre_Usuario '" + IdUsuario + "'");
            return ret;
        }
        public string Rut_Usuario(string IdUsuario)
        {
            string ret = "";
            dbTools db = new dbTools();
            ret = db.ResultQueryRun("exec dbo.sp_Rut_Usuario_Rut'" + IdUsuario + "'");
            return ret;
        }

        public string IdProyectoVTA(string IdCotizacion)
        {
            string ret = "";

            string sQuery = "SELECT C.idProyecto FROM VTA_Cotizacion C WHERE C.IdCotizacion=" + IdCotizacion;

            ret = ResultQueryRun(sQuery);

            return ret;
        }

        public string Guardar_Log_Errores(string Formulario, string Metodo, string Usuario, string Error)
        {
            string ret = "";
            ErrosMSG = null;
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Sistemas_Maestra"].ConnectionString;
                conn.Open();
                conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "exec dbo.sp_VTA_Guardar_Log_Errores '" + 
					Formulario + "','" + 
					Metodo + "','" +
                    Usuario + "','" + 
					Error.Replace("'","") + "';";
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();


                while (rs.Read())
                {
                    ret = rs.GetValue(0).ToString();
    }

                rs.Close();
                conn.Close();
            }
            catch (Exception err)
            {
                ret = "Error causado por " + err.Message.ToString();
            }

            return ret;
        }

    }
}