using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaestraNet.Entidad
{
    public class Inmueble
    {
        public int IdProyecto
        { get; set; }
        public int IdInmueble
        { get; set; }
        public int IdModeloInmueble
        { get; set; }
        public string ModeloInmueble
        { get; set; }
        public int Piso
        { get; set; }
        public string Edificio
        { get; set; }
        public string Observacion
        { get; set; }
        public int NDepto
        { get; set; }
        public double Terraza
        { get; set; }
        public double M2Util
        { get; set; }
        public double Logia
        { get; set; }
        public string Orientacion
        { get; set; }
        public int PrecioLista
        { get; set; }
        public int IdEstadoInmueble
        { get; set; }
        public string Usuario
        { get; set; }
        public DateTime Fecha
        { get; set; }
        public string Alicuota
        { get; set; }
        public string NumeroRol
        { get; set; }
        public string TipoPrecioLista
        { get; set; }
        public string JustificacionEstadoInmueble
        { get; set; }
        public int IdOrientacion
        { get; set; }
        public string EstadoInmueble
        { get; set; }


        //Para la previsualización en grilla y no mostrar valores
        public string TerrazaPrev
        { get; set; }
        public string M2UtilPrev
        { get; set; }




        //Para tipo Inmueble, consultar si se dejarán los atributos en otra clase
        public int IdTipoInmueble
        { get; set; }
        public string sNombreTipoInmueble
        { get; set; }
        public int iCdiId
        { get; set; }
        public int iServicioId
        { get; set; }

    }
}