using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALAtributos
    {

        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Atributos ObtenerAtributos()
        {
            Atributos atributosResponse = new Atributos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM atributos";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                AtributosEntity atributoEntity = new AtributosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    atributo = dataReader["atributo"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                };
                                (atributosResponse.lstAtributos ?? new List<AtributosEntity>()).Add(atributoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        atributosResponse.codigoError = "E";
                        atributosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(atributosResponse.codigoError))
                    {
                        if (atributosResponse.lstAtributos.Count == 0)
                        {
                            atributosResponse.codigoError = "E";
                            atributosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            atributosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                atributosResponse.codigoError = "E";
                atributosResponse.descripcionError = "1." + ex.Message;
            }
            return atributosResponse;
        }

        public static Atributos ObtenerAtributosNombre(string atributo)
        {
            Atributos atributosResponse = new Atributos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM atributos where atributo = ?patributo";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?patributo", atributo);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                AtributosEntity atributoEntity = new AtributosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    atributo = dataReader["atributo"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString()
                                };
                                (atributosResponse.lstAtributos ?? new List<AtributosEntity>()).Add(atributoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        atributosResponse.codigoError = "E";
                        atributosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(atributosResponse.codigoError))
                    {
                        if (atributosResponse.lstAtributos.Count == 0)
                        {
                            atributosResponse.codigoError = "E";
                            atributosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            atributosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                atributosResponse.codigoError = "E";
                atributosResponse.descripcionError = "1." + ex.Message;
            }
            return atributosResponse;
        }

        public static Atributos InsertarAtributo(AtributosEntity atributo)
        {
            Atributos atributoResponse = new Atributos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO atributos(atributo,etiquetas,es_plasticaucho,es_sincronizado,created_at)
                                       VALUES(?pAtributo,?pEtiquetas,?pEsPlasticaucho,?pEsSincronizado,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pAtributo", atributo.atributo);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", atributo.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pEsPlasticaucho", "1");
                                    myCmd.Parameters.AddWithValue("?pEsSincronizado", "1");
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        atributoResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        atributoResponse.codigoError = "E";
                                        atributoResponse.descripcionError = "3. No se pudo crear el Atributo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    atributoResponse.codigoError = "E";
                                    atributoResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        atributoResponse.codigoError = "E";
                        atributoResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                atributoResponse.codigoError = "E";
                atributoResponse.descripcionError = "1. " + ex.Message;
            }
            return atributoResponse;
        }

        public static Atributos ModificarAtributo(AtributosEntity atributo)
        {
            Atributos atributoResponse = new Atributos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE atributos 
                                      SET atributo=?pAtributo,
                                          etiquetas=?pEtiquetas,
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
                                    myCmd.Parameters.AddWithValue("?pAtributo", atributo.atributo);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", atributo.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", atributo.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        atributoResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        atributoResponse.codigoError = "E";
                                        atributoResponse.descripcionError = "3. No se pudo actualizar el Atributo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    atributoResponse.codigoError = "E";
                                    atributoResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        atributoResponse.codigoError = "E";
                        atributoResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                atributoResponse.codigoError = "E";
                atributoResponse.descripcionError = "1. " + ex.Message;
            }
            return atributoResponse;
        }

        public static Atributos EliminarAtributo(AtributosEntity atributo)
        {
            Atributos atributoResponse = new Atributos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM atributos WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", atributo.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        atributoResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        atributoResponse.codigoError = "E";
                                        atributoResponse.descripcionError = "3. No se pudo eliminar el Atributo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    atributoResponse.codigoError = "E";
                                    atributoResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        atributoResponse.codigoError = "E";
                        atributoResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                atributoResponse.codigoError = "E";
                atributoResponse.descripcionError = "1. " + ex.Message;
            }
            return atributoResponse;
        }

    }
}