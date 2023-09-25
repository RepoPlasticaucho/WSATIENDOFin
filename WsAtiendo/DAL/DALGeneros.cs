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
    public class DALGeneros
    {

        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Generos ObtenerGeneros()
        {
            Generos generoResponse = new Generos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM generos";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                GenerosEntity generoEntity = new GenerosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    genero = dataReader["genero"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                };
                                (generoResponse.lstGeneros ?? new List<GenerosEntity>()).Add(generoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        generoResponse.codigoError = "E";
                        generoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(generoResponse.codigoError))
                    {
                        if (generoResponse.lstGeneros.Count == 0)
                        {
                            generoResponse.codigoError = "E";
                            generoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            generoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                generoResponse.codigoError = "E";
                generoResponse.descripcionError = "1." + ex.Message;
            }
            return generoResponse;
        }

        public static Generos ObtenerGenerosNombre(string genero)
        {
            Generos generoResponse = new Generos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM generos where genero = ?pgenero";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pgenero", genero);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                GenerosEntity generoEntity = new GenerosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    genero = dataReader["genero"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                };
                                (generoResponse.lstGeneros ?? new List<GenerosEntity>()).Add(generoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        generoResponse.codigoError = "E";
                        generoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(generoResponse.codigoError))
                    {
                        if (generoResponse.lstGeneros.Count == 0)
                        {
                            generoResponse.codigoError = "E";
                            generoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            generoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                generoResponse.codigoError = "E";
                generoResponse.descripcionError = "1." + ex.Message;
            }
            return generoResponse;
        }

        public static Generos InsertarGenero(GenerosEntity genero)
        {
            Generos generoResponse = new Generos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO generos(genero,etiquetas,es_plasticaucho,es_sincronizado,created_at)
                                       VALUES(?pGenero,?pEtiquetas,?pEsPlasticaucho,?pEsSincronizado,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pGenero", genero.genero);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", genero.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pEsPlasticaucho", "1");
                                    myCmd.Parameters.AddWithValue("?pEsSincronizado", "1");
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        generoResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        generoResponse.codigoError = "E";
                                        generoResponse.descripcionError = "3. No se pudo crear el Genero.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    generoResponse.codigoError = "E";
                                    generoResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        generoResponse.codigoError = "E";
                        generoResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                generoResponse.codigoError = "E";
                generoResponse.descripcionError = "1. " + ex.Message;
            }
            return generoResponse;
        }

        public static Generos ModificarGenero(GenerosEntity genero)
        {
            Generos generoResponse = new Generos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE generos 
                                      SET genero=?pGenero,
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
                                    myCmd.Parameters.AddWithValue("?pGenero", genero.genero);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", genero.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", genero.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        generoResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        generoResponse.codigoError = "E";
                                        generoResponse.descripcionError = "3. No se pudo actualizar el Genero.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    generoResponse.codigoError = "E";
                                    generoResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        generoResponse.codigoError = "E";
                        generoResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                generoResponse.codigoError = "E";
                generoResponse.descripcionError = "1. " + ex.Message;
            }
            return generoResponse;
        }

        public static Generos EliminarGenero(GenerosEntity Genero)
        {
            Generos generoResponse = new Generos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM generos WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", Genero.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        generoResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        generoResponse.codigoError = "E";
                                        generoResponse.descripcionError = "3. No se pudo eliminar el Genero.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    generoResponse.codigoError = "E";
                                    generoResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        generoResponse.codigoError = "E";
                        generoResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                generoResponse.codigoError = "E";
                generoResponse.descripcionError = "1. " + ex.Message;
            }
            return generoResponse;
        }

    }
}