using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALGrupos
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Grupos ObtenerGrupos()
        {
            Grupos gruposResponse = new Grupos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM grupos";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                GruposEntity gruposEntity = new GruposEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    grupo = dataReader["grupo"].ToString(),
                                    idFiscal = dataReader["id_fiscal"].ToString()
                                };
                                (gruposResponse.lstGrupos ?? new List<GruposEntity>()).Add(gruposEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        gruposResponse.codigoError = "E";
                        gruposResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(gruposResponse.codigoError))
                    {
                        if (gruposResponse.lstGrupos.Count == 0)
                        {
                            gruposResponse.codigoError = "E";
                            gruposResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            gruposResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gruposResponse.codigoError = "E";
                gruposResponse.descripcionError = "1." + ex.Message;
            }
            return gruposResponse;
        }

        public static Grupos InsertarGrupo(GruposEntity grupo)
        {
            Grupos gruposResponse = new Grupos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO grupos(grupo,id_fiscal,created_at)
                                       VALUES(?pGrupo,?pIdFiscal,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pGrupo", grupo.grupo);
                                    myCmd.Parameters.AddWithValue("?pIdFiscal", grupo.idFiscal);
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        gruposResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        gruposResponse.codigoError = "E";
                                        gruposResponse.descripcionError = "3. No se pudo crear el Grupo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    gruposResponse.codigoError = "E";
                                    gruposResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        gruposResponse.codigoError = "E";
                        gruposResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                gruposResponse.codigoError = "E";
                gruposResponse.descripcionError = "1. " + ex.Message;
            }
            return gruposResponse;
        }

        public static Grupos ModificarGrupo(GruposEntity grupo)
        {
            Grupos gruposResponse = new Grupos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE grupos 
                                      SET grupo=?pGrupo,
                                          id_fiscal=?pIdFiscal,
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
                                    myCmd.Parameters.AddWithValue("?pGrupo", grupo.grupo);
                                    myCmd.Parameters.AddWithValue("?pIdFiscal", grupo.idFiscal);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", grupo.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        gruposResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        gruposResponse.codigoError = "E";
                                        gruposResponse.descripcionError = "3. No se pudo actualizar el Grupo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    gruposResponse.codigoError = "E";
                                    gruposResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        gruposResponse.codigoError = "E";
                        gruposResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                gruposResponse.codigoError = "E";
                gruposResponse.descripcionError = "1. " + ex.Message;
            }
            return gruposResponse;
        }

        public static Grupos EliminarGrupo(GruposEntity grupo)
        {
            Grupos gruposResponse = new Grupos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM grupos WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", grupo.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        gruposResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        gruposResponse.codigoError = "E";
                                        gruposResponse.descripcionError = "3. No se pudo eliminar el Grupo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    gruposResponse.codigoError = "E";
                                    gruposResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        gruposResponse.codigoError = "E";
                        gruposResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                gruposResponse.codigoError = "E";
                gruposResponse.descripcionError = "1. " + ex.Message;
            }
            return gruposResponse;
        }

    }
}