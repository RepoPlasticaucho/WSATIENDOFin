using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using WsAtiendo.Models;
using WsAtiendo.SRI;

namespace WsAtiendo.DAL
{
    public class DALSociedades
    {

        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Sociedades ObtenerSociedades()
        {
            Sociedades sociedadResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT G.id as idGrupo,G.grupo,S.id as idSociedad,S.nombre_comercial,S.razon_social,S.email,S.id_fiscal,S.telefono,S.funcion,S.tipo_ambienteid
                                        FROM sociedades as S
                                        INNER JOIN grupos as G ON G.id=S.grupo_id";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                SociedadesEntity sociedadesEntity = new SociedadesEntity()
                                {
                                    idGrupo = dataReader["idGrupo"].ToString(),
                                    nombreGrupo = dataReader["grupo"].ToString(),
                                    idSociedad = dataReader["idSociedad"].ToString(),
                                    nombre_comercial = dataReader["nombre_comercial"].ToString(),
                                    razon_social = dataReader["razon_social"].ToString(),
                                    email = dataReader["email"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    tipo_ambienteid = dataReader["tipo_ambienteid"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    funcion = dataReader["funcion"].ToString()
                                };
                                (sociedadResponse.lstSociedades ?? new List<SociedadesEntity>()).Add(sociedadesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadResponse.codigoError = "E";
                        sociedadResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(sociedadResponse.codigoError))
                    {
                        if (sociedadResponse.lstSociedades.Count == 0)
                        {
                            sociedadResponse.codigoError = "E";
                            sociedadResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            sociedadResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                sociedadResponse.codigoError = "E";
                sociedadResponse.descripcionError = "1." + ex.Message;
            }
            return sociedadResponse;
        }

        public static Sociedades EliminarSociedad(SociedadesEntity sociedad)
        {
            Sociedades sociedadesResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE
                                        FROM sociedades
                                        WHERE sociedades.id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", sociedad.idSociedad);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        sociedadesResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        sociedadesResponse.codigoError = "E";
                                        sociedadesResponse.descripcionError = "3. No se pudo eliminar la Sociedad.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    sociedadesResponse.codigoError = "E";
                                    sociedadesResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadesResponse.codigoError = "E";
                        sociedadesResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                sociedadesResponse.codigoError = "E";
                sociedadesResponse.descripcionError = "1. " + ex.Message;
            }
            return sociedadesResponse;
        }

        public static Sociedades InsertarSociedad(SociedadesEntity sociedad)
        {
            Sociedades sociedadesResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO sociedades(grupo_id,nombre_comercial,id_fiscal,email,telefono,pass,created_at,gven,tipologia,cod_sap,razon_social,funcion,tipo_ambienteid)
                                      VALUES(?pIdGrupo,?pNombreComercial,?pIdFiscal,?pEmail,?pTelefono,?pPassword,?pCreatedAt,?pGVen,?pTipologia,?pCodSAP,?pRazonSocial,?pFuncion,?pAmbiente_id)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pIdGrupo", sociedad.idGrupo);
                                    myCmd.Parameters.AddWithValue("?pNombreComercial", sociedad.nombre_comercial);
                                    myCmd.Parameters.AddWithValue("?pIdFiscal", sociedad.id_fiscal);
                                    myCmd.Parameters.AddWithValue("?pEmail", sociedad.email);
                                    myCmd.Parameters.AddWithValue("?pTelefono", sociedad.telefono);
                                    myCmd.Parameters.AddWithValue("?pPassword", "");
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pGVen", sociedad.gven);
                                    myCmd.Parameters.AddWithValue("?pTipologia", sociedad.tipologia);
                                    myCmd.Parameters.AddWithValue("?pAmbiente_id", sociedad.tipo_ambienteid);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", sociedad.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pRazonSocial", sociedad.razon_social);
                                    myCmd.Parameters.AddWithValue("?pFuncion", sociedad.funcion);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        sociedadesResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        sociedadesResponse.codigoError = "E";
                                        sociedadesResponse.descripcionError = "3. No se pudo crear la Sociedad.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    sociedadesResponse.codigoError = "E";
                                    sociedadesResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadesResponse.codigoError = "E";
                        sociedadesResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                sociedadesResponse.codigoError = "E";
                sociedadesResponse.descripcionError = "1. " + ex.Message;
            }
            return sociedadesResponse;
        }

        public static Sociedades ModificarSociedad(SociedadesEntity sociedad)
        {
            Sociedades sociedadesResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE sociedades 
                                      SET grupo_id=?pGrupoId,
                                          tipo_ambienteid=?pAmbiente_id,
                                          razon_social=?pRazonSocial,
                                          nombre_comercial=?pNombreComercial,
                                          id_fiscal=?pIdFiscal,
                                          email=?pEMail,
                                          telefono=?pTelefono,
                                          funcion=?pFuncion,
                                          gven=?pGVen,
                                          tipologia=?pTipologia,
                                          cod_sap=?pCodSAP,
                                          updated_at=?pUpdatedAt
                                      WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pGrupoId", sociedad.idGrupo);
                                    myCmd.Parameters.AddWithValue("?pRazonSocial", sociedad.razon_social);
                                    myCmd.Parameters.AddWithValue("?pNombreComercial", sociedad.nombre_comercial);
                                    myCmd.Parameters.AddWithValue("?pIdFiscal", sociedad.id_fiscal);
                                    myCmd.Parameters.AddWithValue("?pEMail", sociedad.email);
                                    myCmd.Parameters.AddWithValue("?pTelefono", sociedad.telefono);
                                    myCmd.Parameters.AddWithValue("?pAmbiente_id", sociedad.tipo_ambienteid);
                                    myCmd.Parameters.AddWithValue("?pFuncion", sociedad.funcion);
                                    myCmd.Parameters.AddWithValue("?pGVen", sociedad.gven);
                                    myCmd.Parameters.AddWithValue("?pTipologia", sociedad.tipologia);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", sociedad.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", sociedad.idSociedad);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        sociedadesResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        sociedadesResponse.codigoError = "E";
                                        sociedadesResponse.descripcionError = "3. No se pudo actualizar la Sociedad.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    sociedadesResponse.codigoError = "E";
                                    sociedadesResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadesResponse.codigoError = "E";
                        sociedadesResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                sociedadesResponse.codigoError = "E";
                sociedadesResponse.descripcionError = "1. " + ex.Message;
            }
            return sociedadesResponse;
        }

        public static Sociedades ObtenerSociedad(SociedadesEntity sociedad)
        {
            Sociedades sociedadResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT G.id as idGrupo,G.grupo,S.id as idSociedad,S.nombre_comercial,S.razon_social,S.email,S.id_fiscal,S.telefono, S.funcion
                                        FROM sociedades as S
                                        INNER JOIN grupos as G ON G.id=S.grupo_id
                                        WHERE (email LIKE ?pemailid and pass like ?ppasswid)
                                     ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pemailid", sociedad.email);
                            myCmd.Parameters.AddWithValue("?ppasswid", sociedad.password);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                SociedadesEntity sociedadesEntity = new SociedadesEntity()
                                {
                                    idGrupo = dataReader["idGrupo"].ToString(),
                                    nombreGrupo = dataReader["grupo"].ToString(),
                                    idSociedad = dataReader["idSociedad"].ToString(),
                                    nombre_comercial = dataReader["nombre_comercial"].ToString(),
                                    razon_social = dataReader["razon_social"].ToString(),
                                    email = dataReader["email"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    funcion = dataReader["funcion"].ToString()
                                };
                                (sociedadResponse.lstSociedades ?? new List<SociedadesEntity>()).Add(sociedadesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadResponse.codigoError = "E";
                        sociedadResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(sociedadResponse.codigoError))
                    {
                        if (sociedadResponse.lstSociedades.Count == 0)
                        {
                            sociedadResponse.codigoError = "E";
                            sociedadResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            sociedadResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                sociedadResponse.codigoError = "E";
                sociedadResponse.descripcionError = "1." + ex.Message;
            }
            return sociedadResponse;
        }

        public static Sociedades ObtenerUsuario(SociedadesEntity sociedad)
        {
            Sociedades sociedadResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT G.id as idGrupo,G.grupo,S.id as idSociedad,S.nombre_comercial,S.razon_social,S.email,S.id_fiscal,S.telefono
                                        FROM sociedades as S
                                        INNER JOIN grupos as G ON G.id=S.grupo_id
                                        WHERE (email LIKE ?pemailid)
                                     ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pemailid", sociedad.email);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                SociedadesEntity sociedadesEntity = new SociedadesEntity()
                                {
                                    idGrupo = dataReader["idGrupo"].ToString(),
                                    nombreGrupo = dataReader["grupo"].ToString(),
                                    idSociedad = dataReader["idSociedad"].ToString(),
                                    nombre_comercial = dataReader["nombre_comercial"].ToString(),
                                    razon_social = dataReader["razon_social"].ToString(),
                                    email = dataReader["email"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    telefono = dataReader["telefono"].ToString()
                                };
                                (sociedadResponse.lstSociedades ?? new List<SociedadesEntity>()).Add(sociedadesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadResponse.codigoError = "E";
                        sociedadResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(sociedadResponse.codigoError))
                    {
                        if (sociedadResponse.lstSociedades.Count == 0)
                        {
                            sociedadResponse.codigoError = "E";
                            sociedadResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            sociedadResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                sociedadResponse.codigoError = "E";
                sociedadResponse.descripcionError = "1." + ex.Message;
            }
            return sociedadResponse;
        }

        public static Sociedades ObtenerUser(SociedadesEntity sociedad)
        {
            string key = "Venus2022@!";
            Sociedades sociedadResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT G.id as idGrupo,G.grupo,G.id_fiscal as idFiscalGrupo,
                                        S.id as idSociedad,S.nombre_comercial,S.razon_social,S.email,S.id_fiscal,S.telefono,
                                        S.paginacion,S.funcion,S.url_certificado,S.clave_certificado,S.email_certificado,S.pass_certificado
                                        FROM sociedades as S
                                        INNER JOIN grupos as G ON G.id=S.grupo_id
                                        WHERE (S.id = ?psociedadid)
                                     ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?psociedadid", sociedad.idSociedad);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                SociedadesEntity sociedadesEntity = new SociedadesEntity()
                                {
                                    idGrupo = dataReader["idGrupo"].ToString(),
                                    nombreGrupo = dataReader["grupo"].ToString(),
                                    idSociedad = dataReader["idSociedad"].ToString(),
                                    nombre_comercial = dataReader["nombre_comercial"].ToString(),
                                    razon_social = dataReader["razon_social"].ToString(),
                                    email = dataReader["email"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    id_fiscal_grupo = dataReader["idFiscalGrupo"].ToString(),
                                    telefono = dataReader["telefono"].ToString(),
                                    funcion = dataReader["funcion"].ToString(),
                                    paginacion = dataReader["paginacion"].ToString(),
                                    url_certificado = dataReader["url_certificado"].ToString(),
                                    clave_certificado = EncryptionAlgorithm.Decrypt(dataReader["clave_certificado"].ToString(),key),
                                    email_certificado = dataReader["email_certificado"].ToString(),                            
                                    pass_certificado = EncryptionAlgorithm.Decrypt(dataReader["pass_certificado"].ToString(), key)

                                };
                                (sociedadResponse.lstSociedades ?? new List<SociedadesEntity>()).Add(sociedadesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadResponse.codigoError = "E";
                        sociedadResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(sociedadResponse.codigoError))
                    {
                        if (sociedadResponse.lstSociedades.Count == 0)
                        {
                            sociedadResponse.codigoError = "E";
                            sociedadResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            sociedadResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                sociedadResponse.codigoError = "E";
                sociedadResponse.descripcionError = "1." + ex.Message;
            }
            return sociedadResponse;
        }

        public static Sociedades ObtenerSociedadDatos(SociedadesEntity sociedad)
        {
            Sociedades sociedadResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT S.*, A.*, TA.*
                                        FROM sociedades as S
                                        INNER JOIN almacenes as A on S.id = A.sociedad_id
                                        INNER JOIN tipo_ambiente as TA on TA.id = S.tipo_ambienteid
                                        WHERE S.id=?pSociedadid
                                        GROUP BY S.id";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pSociedadid", sociedad.idSociedad);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                SociedadesEntity sociedadesEntity = new SociedadesEntity()
                                {
                                    nombre_comercial = dataReader["nombre_comercial"].ToString(),
                                    razon_social = dataReader["razon_social"].ToString(),
                                    dir1 = dataReader["dir1"].ToString(),
                                    direccion = dataReader["direccion"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    ambiente = dataReader["tipo_ambiente"].ToString(),                                   
                                };
                                (sociedadResponse.lstSociedades ?? new List<SociedadesEntity>()).Add(sociedadesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadResponse.codigoError = "E";
                        sociedadResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(sociedadResponse.codigoError))
                    {
                        if (sociedadResponse.lstSociedades.Count == 0)
                        {
                            sociedadResponse.codigoError = "E";
                            sociedadResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            sociedadResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                sociedadResponse.codigoError = "E";
                sociedadResponse.descripcionError = "1." + ex.Message;
            }
            return sociedadResponse;
        }

        public static Sociedades ActualizarSociedad(SociedadesEntity sociedad)
        {
            Sociedades sociedadesResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE sociedades 
                                      SET 
                                          razon_social=?pRazonSocial,
                                          nombre_comercial=?pNombreComercial,
                                          id_fiscal=?pIdFiscal,
                                          email=?pEMail,
                                          telefono=?pTelefono,
                                          updated_at=?pUpdatedAt
                                      WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pRazonSocial", sociedad.razon_social);
                                    myCmd.Parameters.AddWithValue("?pNombreComercial", sociedad.nombre_comercial);
                                    myCmd.Parameters.AddWithValue("?pIdFiscal", sociedad.id_fiscal);
                                    myCmd.Parameters.AddWithValue("?pEMail", sociedad.email);
                                    myCmd.Parameters.AddWithValue("?pTelefono", sociedad.telefono);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", sociedad.idSociedad);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        sociedadesResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        sociedadesResponse.codigoError = "E";
                                        sociedadesResponse.descripcionError = "3. No se pudo actualizar la Sociedad.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    sociedadesResponse.codigoError = "E";
                                    sociedadesResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadesResponse.codigoError = "E";
                        sociedadesResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                sociedadesResponse.codigoError = "E";
                sociedadesResponse.descripcionError = "1. " + ex.Message;
            }
            return sociedadesResponse;
        }

        public static Sociedades ActualizarPass(SociedadesEntity sociedad)
        {
            Sociedades sociedadesResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE sociedades 
                                      SET pass=?pPassword
                                      WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pPassword", sociedad.password);
                                    myCmd.Parameters.AddWithValue("?pId", sociedad.idSociedad);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        sociedadesResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        sociedadesResponse.codigoError = "E";
                                        sociedadesResponse.descripcionError = "3. No se pudo actualizar la Sociedad.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    sociedadesResponse.codigoError = "E";
                                    sociedadesResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadesResponse.codigoError = "E";
                        sociedadesResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                sociedadesResponse.codigoError = "E";
                sociedadesResponse.descripcionError = "1. " + ex.Message;
            }
            return sociedadesResponse;
        }

        public static Sociedades ActualizarCertificado(SociedadesEntity sociedad)
        {
            string key = "Venus2022@!";
            Sociedades sociedadesResponse = new Sociedades();
            if (!sociedad.url_certificado.Contains("/"))
            {
                sociedad.url_certificado = "https://calidad.atiendo.ec/eojgprlg/Certificados/" + sociedad.url_certificado;
            }
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE sociedades 
                                      SET clave_certificado=?pClave_certificado,
                                          url_certificado=?pUrl_certificado,
                                          updated_At=?pUpdated_at
                                      WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pClave_certificado", EncryptionAlgorithm.Encrypt(sociedad.clave_certificado, key));
                                    myCmd.Parameters.AddWithValue("?pId", sociedad.idSociedad);
                                    myCmd.Parameters.AddWithValue("?pUrl_certificado", sociedad.url_certificado);
                                    myCmd.Parameters.AddWithValue("?pUpdated_at", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        sociedadesResponse.codigoError = "OK";

                                        SendEmail enviarCorreoC = new SendEmail();
                                        enviarCorreoC.EnviarC("sistemaAtiendo@plasticaucho.com", "Inicio.2023", sociedad.email_certificado);
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        sociedadesResponse.codigoError = "E";
                                        sociedadesResponse.descripcionError = "3. No se pudo actualizar la Sociedad.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    sociedadesResponse.codigoError = "E";
                                    sociedadesResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadesResponse.codigoError = "E";
                        sociedadesResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                sociedadesResponse.codigoError = "E";
                sociedadesResponse.descripcionError = "1. " + ex.Message;
            }
            return sociedadesResponse;
        }

        public static Sociedades ActualizarClaveCorreo(SociedadesEntity sociedad)
        {
            string key = "Venus2022@!";
            Sociedades sociedadesResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE sociedades 
                                      SET pass_certificado=?pPass_certificado,
                                          email_certificado=?pEmail_certificado,
                                          updated_At=?pUpdated_at
                                      WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pPass_certificado", EncryptionAlgorithm.Encrypt(sociedad.pass_certificado, key));
                                    myCmd.Parameters.AddWithValue("?pId", sociedad.idSociedad);
                                    myCmd.Parameters.AddWithValue("?pEmail_certificado", sociedad.email_certificado);
                                    myCmd.Parameters.AddWithValue("?pUpdated_at", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        sociedadesResponse.codigoError = "OK";

                                        SendEmail enviarCorreoC = new SendEmail();
                                        enviarCorreoC.EnviarC("sistemaAtiendo@plasticaucho.com", "Inicio.2023", sociedad.email_certificado);
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        sociedadesResponse.codigoError = "E";
                                        sociedadesResponse.descripcionError = "3. No se pudo actualizar la Sociedad.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    sociedadesResponse.codigoError = "E";
                                    sociedadesResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadesResponse.codigoError = "E";
                        sociedadesResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                sociedadesResponse.codigoError = "E";
                sociedadesResponse.descripcionError = "1. " + ex.Message;
            }
            return sociedadesResponse;
        }

        public static Sociedades ObtenerSociedadesN(string name)
        {
            Sociedades sociedadResponse = new Sociedades();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT G.id as idGrupo,G.grupo,S.id as idSociedad,S.nombre_comercial,S.razon_social,S.email,S.id_fiscal,S.telefono, S.funcion
                                        FROM sociedades as S
                                        INNER JOIN grupos as G ON G.id=S.grupo_id
                                        WHERE nombre_comercial LIKE ?tNombre_comercial";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?tNombre_comercial", name);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                SociedadesEntity sociedadesEntity = new SociedadesEntity()
                                {
                                    idGrupo = dataReader["idGrupo"].ToString(),
                                    nombreGrupo = dataReader["grupo"].ToString(),
                                    idSociedad = dataReader["idSociedad"].ToString(),
                                    nombre_comercial = dataReader["nombre_comercial"].ToString(),
                                    razon_social = dataReader["razon_social"].ToString(),
                                    email = dataReader["email"].ToString(),
                                    id_fiscal = dataReader["id_fiscal"].ToString(),
                                    telefono = dataReader["telefono"].ToString()
                                };
                                (sociedadResponse.lstSociedades ?? new List<SociedadesEntity>()).Add(sociedadesEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sociedadResponse.codigoError = "E";
                        sociedadResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(sociedadResponse.codigoError))
                    {
                        if (sociedadResponse.lstSociedades.Count == 0)
                        {
                            sociedadResponse.codigoError = "E";
                            sociedadResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            sociedadResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                sociedadResponse.codigoError = "E";
                sociedadResponse.descripcionError = "1." + ex.Message;
            }
            return sociedadResponse;
        }
    }
}
