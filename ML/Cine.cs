using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cine
    {
        public int IdCine { get; set; } = default!;
        public string Nombre { get; set; } = default!;
        public string Direccion { get; set; } = default!;
        public int IdZona { get; set; } = default!;
        public int Venta { get; set; } = default!;
        public ML.Zona Zona { get; set; } = default!;
        public ML.Venta Ventas { get; set; } = default!;
        public List<object> Cines { get; set; } = default!;
    }
}
