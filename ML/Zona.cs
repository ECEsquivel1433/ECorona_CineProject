using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Zona
    {
        public int IdZona { get; set; } = default!;
        public string Nombre { get; set; } = default!;
        public List<object> Zonas { get; set; } = default!;

    }
}
