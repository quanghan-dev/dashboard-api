using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Email
{
    public class EmailMessage
    {
        public string? ToAddress { get; private set; }
        public string? Body { get; private set; }
        public string? Subject { get; private set; }
    }
}