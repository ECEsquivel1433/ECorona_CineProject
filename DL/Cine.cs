using System;
using System.Collections.Generic;

namespace DL;

public partial class Cine
{
    public int IdCine { get; set; }

    public string Nombre { get; set; } = default!;

    public string Direccion { get; set; } = default!;

    public int IdZona { get; set; }

    public int Venta { get; set; }

    public string Latitud { get; set; } = default!;

    public string Longitud { get; set; } = default!;

    public virtual Zona? IdZonaNavigation { get; set; }



    public string Zona { get; set; } = default!;
}
