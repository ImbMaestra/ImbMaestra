using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MaestraNet.cs.Entidades
{
    public class Cliente
    {
        private string _rut = "";
        private string _nombre = "";
        private string _paterno = "";
        private string _materno = "";
        private string _email = "";
        private string _fono = "";
        public string _direccion = "";
        public int _iConvenio = 0;
        public int _iLanpass = 0;
        public int _iExpo = 0;
        public double _iDescto = 0;
        public string _sConvenioDescripcion = "";
        private bool _existe = false;
        public int _iIdCotizacion = 0;
        public int _idRegion = 0;
        public int _idCiudad = 0;
        public int _idComuna = 0;
        public int _idNacionalidad = 0;
        public string _sexo = "";
        public string _sexoLetra = "";
        public string _celular = "";
        public int _idEstadoCivil = 0;
        public int _inversionista = 0;
        public int _IdBroker = 0;
        public int _personaJuridica = 0;
        public int _idTipoOcupacion = 0;
        public int _idCliente = 0;
        public int _IdEmpresaMaestra = 0;
        public int _IdDivisionMaestra = 0;
        public string _funcionarioRut = "";
        public string _FechaNacimiento = "";

        enum  cliente
        {
            EntConNombres,
            EntConApePaterno,
            EntConApeMaterno,
            EntDirFono,
            EntMail,
            EntDirDireccion,
            EntRut,
            iDTipoOcupacion,
            IdEmpresaMaestra,
            funcionarioRut,
            Sexo,
            CiuCodigo,
            CmuCodigo,
            IdEstadoCivil,
            IdNacionalidad,
            PersonaJuridica,
            IdCliente,
            IdDivisionMaestra,
            FechaNacimiento,
            Inversionista,
            IdBroker
        }



        public Cliente (string rut)
        {
            cs.dbTools db = new dbTools();
            string strQuery = "exec dbo.sp_VTA_Cliente '" + rut.Replace(".","") + "'";
            DataTable rs = db.ResultQuery(strQuery);
            if (rs.Rows.Count == 0)
            {
                _existe = false;
            }
            else
            {
                _nombre            = rs.Rows[0].ItemArray[(int)cliente.EntConNombres].ToString().Trim();
                _paterno           = rs.Rows[0].ItemArray[(int)cliente.EntConApePaterno].ToString().Trim();
                _materno		   = rs.Rows[0].ItemArray[(int)cliente.EntConApeMaterno].ToString().Trim();
                _fono			   = rs.Rows[0].ItemArray[(int)cliente.EntDirFono].ToString().Trim();
                _email			   = rs.Rows[0].ItemArray[(int)cliente.EntMail].ToString().Trim();
                _direccion		   = rs.Rows[0].ItemArray[(int)cliente.EntDirDireccion].ToString().Trim();
                _rut               = rs.Rows[0].ItemArray[(int)cliente.EntRut].ToString().Replace(".", "").Trim();
                if (!rs.Rows[0].ItemArray[(int)cliente.iDTipoOcupacion].ToString().Equals(""))
                {
                    _idTipoOcupacion = Convert.ToInt16(rs.Rows[0].ItemArray[(int)cliente.iDTipoOcupacion]);
                }                    
                _IdEmpresaMaestra  = Convert.ToInt16(rs.Rows[0].ItemArray[(int)cliente.IdEmpresaMaestra]);
                _IdDivisionMaestra = Convert.ToInt16(rs.Rows[0].ItemArray[(int)cliente.IdDivisionMaestra]);
                _funcionarioRut	   = rs.Rows[0].ItemArray[(int)cliente.funcionarioRut].ToString().Trim();
                _sexo			   = rs.Rows[0].ItemArray[(int)cliente.Sexo].ToString().Trim();
                _idCiudad          = Convert.ToInt32(rs.Rows[0].ItemArray[(int)cliente.CiuCodigo].ToString().Trim());
                _idComuna          = Convert.ToInt32(rs.Rows[0].ItemArray[(int)cliente.CmuCodigo].ToString().Trim());
                _idEstadoCivil     = Convert.ToInt32(rs.Rows[0].ItemArray[(int)cliente.IdEstadoCivil].ToString().Trim());
                _idNacionalidad    = Convert.ToInt32(rs.Rows[0].ItemArray[(int)cliente.IdNacionalidad].ToString().Trim());
                _personaJuridica   = Convert.ToInt32(rs.Rows[0].ItemArray[(int)cliente.PersonaJuridica].ToString().Trim());
                _inversionista     = Convert.ToInt32(rs.Rows[0].ItemArray[(int)cliente.Inversionista].ToString().Trim());
                _IdBroker          = Convert.ToInt16(rs.Rows[0].ItemArray[(int)cliente.IdBroker].ToString().Trim());
                _idCliente         = Convert.ToInt32(rs.Rows[0].ItemArray[(int)cliente.IdCliente].ToString().Trim());
                _FechaNacimiento   = rs.Rows[0].ItemArray[(int)cliente.FechaNacimiento].ToString().Trim();
                _existe            = true;
            }
        }

        public void ClienteReserva(string rut, int Reserva)
        {
            cs.dbTools db = new dbTools();
            string strQuery = "exec dbo.sp_VTA_ClienteReserva '" + rut.Replace(".", "") + "','" + Reserva.ToString() + "'";
            DataTable rs = db.ResultQuery(strQuery);
            if (rs.Rows.Count == 0)
            {
                _existe = false;
            }
            else
            {
                _nombre				= rs.Rows[0].ItemArray[0].ToString().Trim();
                _paterno			= rs.Rows[0].ItemArray[1].ToString().Trim();
                _materno			= rs.Rows[0].ItemArray[2].ToString().Trim();
                _fono				= rs.Rows[0].ItemArray[3].ToString().Trim();
                _email				= rs.Rows[0].ItemArray[4].ToString().Trim();
                _direccion			= rs.Rows[0].ItemArray[5].ToString().Trim();
                _rut				= rs.Rows[0].ItemArray[6].ToString().Replace(".", "").Trim();
                _iIdCotizacion		= (int)rs.Rows[0].ItemArray[7];
                _idCiudad			= Convert.ToInt16(rs.Rows[0].ItemArray[8]);
                _idComuna			= (int)rs.Rows[0].ItemArray[9];
                //_sexo= rs.Rows[0].ItemArray[10].ToString().Trim();
                _celular			= rs.Rows[0].ItemArray[11].ToString().Trim();
                _idEstadoCivil		= (int)rs.Rows[0].ItemArray[13];
                _sexo				= rs.Rows[0].ItemArray[14].ToString().Trim();
                _inversionista		= (int)rs.Rows[0].ItemArray[15];
                _personaJuridica	= (int)rs.Rows[0].ItemArray[16];
                _idCliente			= (int)rs.Rows[0].ItemArray[17];
                _idTipoOcupacion	= (int)rs.Rows[0].ItemArray[18];
                _FechaNacimiento	= rs.Rows[0].ItemArray[19].ToString().Trim();
                _existe				= true;
            }
        }

        public Cliente(string rut, string nombre, string paterno, string materno, string email, string fono, string direccion, int iLanpass, int iExpo, int iConvenio, double dDescuento, string sConvenioDescripcion, int idTipoOcupacion, int IdEmpresaMaestra, int IdDivisionMaestra, string funcionarioRut, string FechaNacimiento)
        {
            _existe = false;
            _rut = rut.Replace(".", "");
            _paterno = paterno;
            _materno = materno;
            _email = email;
            _fono = fono;
            _direccion = direccion;
            _iLanpass = iLanpass;
            _iExpo = iExpo;
            _iConvenio = iConvenio;
            _iDescto = dDescuento;
            _sConvenioDescripcion = sConvenioDescripcion;
            _idTipoOcupacion = idTipoOcupacion;
            _IdEmpresaMaestra = IdEmpresaMaestra;
            _IdDivisionMaestra = IdDivisionMaestra;
            _funcionarioRut = funcionarioRut;
            _FechaNacimiento = FechaNacimiento;
        }

        public Cliente(string rut, string nombre, string paterno, string materno, string email, string fono, string direccion, int iLanpass, int iExpo, int iConvenio, double dDescuento, string sConvenioDescripcion, int IdEmpresaMaestra, int IdDivisionMaestra, string funcionarioRut)
        {
            _existe = false;
            _rut = rut.Replace(".", "");
            _paterno = paterno;
            _materno = materno;
            _email = email;
            _fono = fono;
            _direccion = direccion;
            _iLanpass = iLanpass;
            _iExpo = iExpo;
            _iConvenio = iConvenio;
            _iDescto = dDescuento;
            _sConvenioDescripcion = sConvenioDescripcion;
            _IdEmpresaMaestra = IdEmpresaMaestra;
            _IdDivisionMaestra = IdDivisionMaestra;
            _funcionarioRut = funcionarioRut;
        }

        public Cliente(string rut, string nombre, string paterno, string materno, string email, string fono, string direccion)
        {
            _existe = false;
            _rut = rut.Replace(".", "");
            _paterno = paterno;
            _materno = materno;
            _email = email;
            _fono = fono;
            _direccion = direccion;

        }


        public bool Existe()
        {
            return _existe;
        }


        public string Rut()
        {
            return _rut;
        }

        public string Nombre()
        {
            return _nombre;
        }

        public string Paterno()
        {
            return _paterno;
        }

        public string Materno()
        {
            return _materno;
        }

        public string Email()
        {
            return _email;
        }

        public string Fono()
        {
            return _fono;
        }
        public int IdConvenio()
        {
            return _iConvenio;
        }

        public int Lanpass()
        {
            return _iLanpass;
        }
        public int Expo()
        {
            return _iExpo;
        }

        public int IdTipoOcupacion()
        {
            return _idTipoOcupacion;
        }

        public string FechaNacimiento()
        {
            return _FechaNacimiento;
        }
        public int IdBroker()
        {
            return _IdBroker;
        }

        public int Inversionista()
        {
            return _inversionista;
        }

        public int PersonaJuridica()
        {
            return _personaJuridica;
        }

        
    }
}