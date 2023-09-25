using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DALSAP
{
    public class SAPRfc
    {

        public RfcConfigParameters ObtenerConexionSAP(Session session)
        {
            RfcConfigParameters rfc = new RfcConfigParameters();
            try
            {
                rfc.Clear();
                rfc.Add(RfcConfigParameters.Name, session.Name);
                rfc.Add(RfcConfigParameters.AppServerHost, session.AppServerHost);
                rfc.Add(RfcConfigParameters.Client, session.Mandante);
                rfc.Add(RfcConfigParameters.User, session.Usuario);
                rfc.Add(RfcConfigParameters.Password, session.Contraseña);
                rfc.Add(RfcConfigParameters.SystemNumber, session.SystemNumber);
                rfc.Add(RfcConfigParameters.Language, session.Language);
                rfc.Add(RfcConfigParameters.PoolSize, session.PoolSize);
                rfc.Add(RfcConfigParameters.MaxPoolSize, session.MaxPoolSize);
                rfc.Add(RfcConfigParameters.IdleTimeout, session.IdleTimeout);
                rfc.Add(RfcConfigParameters.SAPRouter, session.SAPRouter);

            }
            catch (Exception)
            {
            }
            return rfc;
        }

    }
}