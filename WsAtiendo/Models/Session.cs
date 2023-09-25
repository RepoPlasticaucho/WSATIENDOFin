using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsAtiendo.Models
{
    public class Session
    {

        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Name { get; set; }
        public string AppServerHost { get; set; }
        public string Client { get; set; }
        public string SystemNumber { get; set; }
        public string Language { get; set; }
        public string PoolSize { get; set; }
        public string MaxPoolSize { get; set; }
        public string IdleTimeout { get; set; }
        public string SAPRouter { get; set; }
        public string Mandante { get; set; }
        public string CodCliente { get; set; }
        public string Comp { get; set; }

        public int E_TIPO { get; set; }
        public string E_MENS { get; set; }

        public Session()
        {

        }

    }
}