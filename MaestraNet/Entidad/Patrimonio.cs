using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaestraNet.Entidad
{
    public class Patrimonio
    {
        public int IdCliente
        { get; set; }
        public int libreta
        { get; set; }
        public int Indemizacion
        { get; set; }
        public int VentasBienesRaices
        { get; set; }
        public int ActividadEmpresarial
        { get; set; }
        public int RescateInversiones
        { get; set; }
        public int Herencias
        { get; set; }
        public string Observaciones
        { get; set; }

    }
}