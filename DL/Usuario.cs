using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = default!;

    public string ApellidoPaterno { get; set; } = default!;

    public string ApellidoMaterno { get; set; } = default!;

    public string Username { get; set; } = default!;

    public byte[] Password { get; set; } = default!;

    public string Email { get; set; } = default!;
}
