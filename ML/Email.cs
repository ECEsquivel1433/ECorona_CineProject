using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Email
    {
        public string EmailDirection { get; set; } = default!;
        public string EmailOrigen { get; set; } = default!;
        public string EmailRoute { get; set; } = default!;
        public string EmailPassword { get; set; } = default!;
        public string UrlDomain { get; set; } = default!;
    }
}
