using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaestraNet.Entidad
{
    public class Proyecto
    {
        public Boolean Nuevo
        { get; set; }
        public int IdProyecto
        { get; set; }
        public int IdSalaVenta
        { get; set; }
        public int IdRegion
        { get; set; }
        public int IdComuna
        { get; set; }
        public string NombreProyecto
        { get; set; }
        public int IdEmpresa
        { get; set; }
        public string Email
        { get; set; }
        public string Direccion
        { get; set; }
        public DateTime  FechaInicioVenta
        { get; set; }
        public DateTime FechaRecepcion
        { get; set; }
        public double MetroCuadrados
        { get; set; }
        public int IdEstadoEntrega
        { get; set; }
        public string Latitud
        { get; set; }
        public string Longitud
        { get; set; }
        public int CodigoDivision
        { get; set; }
        public double ValorTerreno
        { get; set; }
    }
}