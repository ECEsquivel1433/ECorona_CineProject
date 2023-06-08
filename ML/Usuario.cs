using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = default!;
        public string ApellidoPaterno { get; set; } = default!;
        public string ApellidoMaterno { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
