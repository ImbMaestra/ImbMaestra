using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaestraNet.Entidad
{
    enum Juridica
        {
           no,
           si
        }
    enum Inversionista
    {
        no,
        si
    }
    enum Sexo
    {
        Masculino='M',
        Femenino='F'

    }
    public class Cliente
    {
        public Boolean Nuevo
        { get; set; }
        public int IdNacionalidad
        { get; set; }
        public int IdCliente
        { get; set; }
        public int IdEstadoCivil
        { get; set; }
        public string Profesion
        { get; set; }
        public string Sexo
        { get; set; }
        public string Dicom
        { get; set; }
        public int Virtual
        { get; set; }
        public string UsuarioCreacion
        { get; set; }
        public DateTime FechaCreacion
        { get; set; }
        public string UsuarioEdicion
        { get; set; }
        public DateTime FechaEdicion
        { get; set; }
        public string Clienterut
        { get; set; }
        public string Vtaclienterut
        { get; set; }
        public int Inversionista
        { get; set; }
        public int PersonaJuridica
        { get; set; }
        public int IdPerfilInversor
        { get; set; }
        public string Nombres
        { get; set; }
        public string Paterno
        { get; set; }
        public string Materno
        { get; set; }
        public int IdTipoOcupacion
        { get; set; }
        public string  Fono
        { get; set; }
        public string Email
        { get; set; }
        public string Direccion
        { get; set; }
        public int CodComuna
        { get; set; }
        public int CodCiudad
        { get; set; }

        public int TipoOcupacion
        { get; set; }

        public string NombreEmpresa
        { get; set; }
        public string DireccionEmpresa
        { get; set; }
        public string TelefonoEmpresa
        { get; set; }
        public string NombreContacto
        { get; set; }
        public string TeleFonoContacto
        { get; set; }
        public string Ocupacion
        { get; set; }
        public DateTime FechaNacimiento
        { get; set; }
    }
}