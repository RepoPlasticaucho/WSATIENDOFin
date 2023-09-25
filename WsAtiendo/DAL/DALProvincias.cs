using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALProvincias
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Provincias ObtenerProvincias()
        {
            Provincias provinciasResponse = new Provincias();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM provincia
                                      ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProvinciasEntity provinciasEntity = new ProvinciasEntity()
                                {
                                    idProvincia = dataReader["id"].ToString(),
                                    provincia = dataReader ["provincia"].ToString(),
                                    codigo = dataReader["codigo"].ToString(),
                                    created_at = dataReader["created_at"].ToString()
                                };
                                (provinciasResponse.lstProvincias ?? new List<ProvinciasEntity>()).Add(provinciasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        provinciasResponse.codigoError = "E";
                        provinciasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(provinciasResponse.codigoError))
                    {
                        if (provinciasResponse.lstProvincias.Count == 0)
                        {
                            provinciasResponse.codigoError = "E";
                            provinciasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            provinciasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                provinciasResponse.codigoError = "E";
                provinciasResponse.descripcionError = "1." + ex.Message;
            }
            return provinciasResponse;
        }
    }
}