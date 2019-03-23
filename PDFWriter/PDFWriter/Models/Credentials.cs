using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDFWriter.Models
{
    internal class Credentials
    {
         internal string Password { get; set; }
         internal string Email { get; set; }
         internal string HostAddress { get; set; }
         internal int HostPort { get; set; }

        internal Credentials(string email, string password, string hostAddress, int hostPort)
        {
            Email = email;
            Password = password;
            HostAddress = hostAddress;
            HostPort = hostPort;
        }
    }
}