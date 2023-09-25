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
    public class DALMarcas
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Marcas ObtenerMarcas()
        {
            Marcas marcasResponse = new Marcas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM marcas";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                MarcasEntity marcaEntity = new MarcasEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    url_image = dataReader["url_image"].ToString(),
                                };
                                (marcasResponse.lstMarcas ?? new List<MarcasEntity>()).Add(marcaEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        marcasResponse.codigoError = "E";
                        marcasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(marcasResponse.codigoError))
                    {
                        if (marcasResponse.lstMarcas.Count == 0)
                        {
                            marcasResponse.codigoError = "E";
                            marcasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            marcasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                marcasResponse.codigoError = "E";
                marcasResponse.descripcionError = "1." + ex.Message;
            }
            return marcasResponse;
        }

        public static Marcas ObtenerMarcaNombre(string marca)
        {
            Marcas marcasResponse = new Marcas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM marcas where marca = ?pmarca";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmarca", marca);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                MarcasEntity marcaEntity = new MarcasEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    url_image = dataReader["url_image"].ToString(),
                                };
                                (marcasResponse.lstMarcas ?? new List<MarcasEntity>()).Add(marcaEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        marcasResponse.codigoError = "E";
                        marcasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(marcasResponse.codigoError))
                    {
                        if (marcasResponse.lstMarcas.Count == 0)
                        {
                            marcasResponse.codigoError = "E";
                            marcasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            marcasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                marcasResponse.codigoError = "E";
                marcasResponse.descripcionError = "1." + ex.Message;
            }
            return marcasResponse;
        }

        public static Marcas ObtenerMarcaCategoria(string categoria)
        {
            Marcas marcasResponse = new Marcas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM marcas as M
                                        INNER JOIN modelos as MO on M.id = MO.marca_id
                                        INNER JOIN lineas as L on L.id = MO.linea_id
                                        INNER JOIN categorias as C on C.id = L.categoria_id
                                        WHERE C.categoria = ?pCategoria
                                        GROUP BY M.marca";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pCategoria", categoria);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                MarcasEntity marcaEntity = new MarcasEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    url_image = dataReader["url_image"].ToString(),
                                };
                                (marcasResponse.lstMarcas ?? new List<MarcasEntity>()).Add(marcaEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        marcasResponse.codigoError = "E";
                        marcasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(marcasResponse.codigoError))
                    {
                        if (marcasResponse.lstMarcas.Count == 0)
                        {
                            marcasResponse.codigoError = "E";
                            marcasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            marcasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                marcasResponse.codigoError = "E";
                marcasResponse.descripcionError = "1." + ex.Message;
            }
            return marcasResponse;
        }

        public static Marcas ObtenerMarcaModelo(string modelo)
        {
            Marcas marcasResponse = new Marcas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT M.marca
                                        FROM marcas as M
                                        INNER JOIN modelos as MO on M.id = MO.marca_id
                                        WHERE MO.modelo = ?pModelo
                                        ORDER BY M.marca";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pModelo", modelo);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                MarcasEntity marcaEntity = new MarcasEntity()
                                {
                                    marca = dataReader["marca"].ToString(),
                                };
                                (marcasResponse.lstMarcas ?? new List<MarcasEntity>()).Add(marcaEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        marcasResponse.codigoError = "E";
                        marcasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(marcasResponse.codigoError))
                    {
                        if (marcasResponse.lstMarcas.Count == 0)
                        {
                            marcasResponse.codigoError = "E";
                            marcasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            marcasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                marcasResponse.codigoError = "E";
                marcasResponse.descripcionError = "1." + ex.Message;
            }
            return marcasResponse;
        }
        public static Marcas ObtenerMarcaLinea(string linea)
        {
            Marcas marcasResponse = new Marcas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct M.marca
                                        FROM marcas as M
                                        INNER JOIN modelos as MO on M.id = MO.marca_id
                                        INNER JOIN lineas as L on L.id = MO.linea_id
                                        WHERE L.linea = ?pLinea
                                        ORDER BY M.marca";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pLinea", linea);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                MarcasEntity marcaEntity = new MarcasEntity()
                                {
                                    marca = dataReader["marca"].ToString(),
                                };
                                (marcasResponse.lstMarcas ?? new List<MarcasEntity>()).Add(marcaEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        marcasResponse.codigoError = "E";
                        marcasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(marcasResponse.codigoError))
                    {
                        if (marcasResponse.lstMarcas.Count == 0)
                        {
                            marcasResponse.codigoError = "E";
                            marcasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            marcasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                marcasResponse.codigoError = "E";
                marcasResponse.descripcionError = "1." + ex.Message;
            }
            return marcasResponse;
        }

        public static Marcas ObtenerMarcasProveedor(string proveedor)
        {
            Marcas marcasResponse = new Marcas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct marca
                                        FROM marcas as M   
                                        INNER JOIN proveedores as P on P.id = M.proveedor_id
                                        WHERE P.nombre = ?pProveedor
                                        order by marca
                                     ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pProveedor", proveedor);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                MarcasEntity marcasEntity = new MarcasEntity()
                                {
                                    marca = dataReader["marca"].ToString()

                                };
                                (marcasResponse.lstMarcas ?? new List<MarcasEntity>()).Add(marcasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        marcasResponse.codigoError = "E";
                        marcasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(marcasResponse.codigoError))
                    {
                        if (marcasResponse.lstMarcas.Count == 0)
                        {
                            marcasResponse.codigoError = "E";
                            marcasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            marcasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                marcasResponse.codigoError = "E";
                marcasResponse.descripcionError = "1." + ex.Message;
            }
            return marcasResponse;
        }

        public static Marcas ObtenerMarcaUno(string marca)
        {
            Marcas marcasResponse = new Marcas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM marcas where marca = ?pmarca LIMIT 1";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmarca", marca);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                MarcasEntity marcaEntity = new MarcasEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    url_image = dataReader["url_image"].ToString(),
                                };
                                (marcasResponse.lstMarcas ?? new List<MarcasEntity>()).Add(marcaEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        marcasResponse.codigoError = "E";
                        marcasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(marcasResponse.codigoError))
                    {
                        if (marcasResponse.lstMarcas.Count == 0)
                        {
                            marcasResponse.codigoError = "E";
                            marcasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            marcasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                marcasResponse.codigoError = "E";
                marcasResponse.descripcionError = "1." + ex.Message;
            }
            return marcasResponse;
        }
        public static Marcas InsertarMarca(MarcasEntity marca)
        {
            Marcas marcaResponse = new Marcas();
            if (!marca.url_image.Contains("/"))
            {
                marca.url_image = "https://calidad.atiendo.ec/eojgprlg/Marcas/" + marca.url_image;
            } else
            {
                marca.url_image = "https://calidad.atiendo.ec/eojgprlg/Marcas/producto.png";
            }
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO marcas(marca,etiquetas,es_plasticaucho,es_sincronizado,created_at,url_image)
                                       VALUES(?pMarca,?pEtiquetas,?pEsPlasticaucho,?pEsSincronizado,?pCreatedAt,?pUrl)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pMarca", marca.marca);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", marca.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pEsPlasticaucho", "1");
                                    myCmd.Parameters.AddWithValue("?pEsSincronizado", "1");
                                    myCmd.Parameters.AddWithValue("?pUrl", marca.url_image);
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        marcaResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        marcaResponse.codigoError = "E";
                                        marcaResponse.descripcionError = "3. No se pudo crear la Marca.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    marcaResponse.codigoError = "E";
                                    marcaResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        marcaResponse.codigoError = "E";
                        marcaResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                marcaResponse.codigoError = "E";
                marcaResponse.descripcionError = "1. " + ex.Message;
            }
            return marcaResponse;
        }

        public static Marcas ModificarMarca(MarcasEntity marca)
        {
            Marcas marcaResponse = new Marcas();
            if (!marca.url_image.Contains("/"))
            {
                marca.url_image = "https://calidad.atiendo.ec/eojgprlg/Marcas/" + marca.url_image;
            }
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE marcas 
                                      SET marca=?pMarca,
                                          etiquetas=?pEtiquetas,
                                          updated_at=?pUpdatedAt,
                                          url_image=?pUrl
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
                                    myCmd.Parameters.AddWithValue("?pMarca", marca.marca);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", marca.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pUrl", marca.url_image);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", marca.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        marcaResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        marcaResponse.codigoError = "E";
                                        marcaResponse.descripcionError = "3. No se pudo actualizar la Marca.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    marcaResponse.codigoError = "E";
                                    marcaResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        marcaResponse.codigoError = "E";
                        marcaResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                marcaResponse.codigoError = "E";
                marcaResponse.descripcionError = "1. " + ex.Message;
            }
            return marcaResponse;
        }

        public static Marcas EliminarMarca(MarcasEntity marca)
        {
            Marcas marcaResponse = new Marcas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM marcas WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", marca.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        marcaResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        marcaResponse.codigoError = "E";
                                        marcaResponse.descripcionError = "3. No se pudo eliminar la Marca.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    marcaResponse.codigoError = "E";
                                    marcaResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        marcaResponse.codigoError = "E";
                        marcaResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                marcaResponse.codigoError = "E";
                marcaResponse.descripcionError = "1. " + ex.Message;
            }
            return marcaResponse;
        }

    }
}