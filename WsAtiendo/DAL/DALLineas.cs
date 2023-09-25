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
    public class DALLineas
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static Lineas ObtenerLineas()
        {
            Lineas lineasResponse = new Lineas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT lineas.*,categorias.categoria
                                        FROM lineas
                                        INNER JOIN categorias ON lineas.categoria_id=categorias.id";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                LineasEntity lineasEntity = new LineasEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    categoria_id = dataReader["categoria_id"].ToString(),
                                    categoria_nombre = dataReader["categoria"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    linea = dataReader["linea"].ToString()

                                };
                                (lineasResponse.lstLineas ?? new List<LineasEntity>()).Add(lineasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lineasResponse.codigoError = "E";
                        lineasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(lineasResponse.codigoError))
                    {
                        if (lineasResponse.lstLineas.Count == 0)
                        {
                            lineasResponse.codigoError = "E";
                            lineasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            lineasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lineasResponse.codigoError = "E";
                lineasResponse.descripcionError = "1." + ex.Message;
            }
            return lineasResponse;
        }

        public static Lineas InsertarLineas(LineasEntity linea)
        {

            Lineas lineasResponse = new Lineas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO lineas(categoria_id,linea,etiquetas,cod_sap,es_plasticaucho,es_sincronizado,created_at)
                                       VALUES(?pCategoriaId,?pLinea,?pEtiqueta,?pCodSAP,?pEsPlasticaucho,?pEsSincronizado,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pCategoriaId", linea.categoria_id);
                                    myCmd.Parameters.AddWithValue("?pLinea", linea.linea);
                                    myCmd.Parameters.AddWithValue("?pEtiqueta", linea.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", linea.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pEsPlasticaucho", "1");
                                    myCmd.Parameters.AddWithValue("?pEsSincronizado", "1");
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        lineasResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        lineasResponse.codigoError = "E";
                                        lineasResponse.descripcionError = "3. No se pudo crear la Línea.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    lineasResponse.codigoError = "E";
                                    lineasResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lineasResponse.codigoError = "E";
                        lineasResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                lineasResponse.codigoError = "E";
                lineasResponse.descripcionError = "1. " + ex.Message;
            }
            return lineasResponse;
        }

        public static Lineas ModificarLinea(LineasEntity linea)
        {
            Lineas lineaResponse = new Lineas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE lineas
                                      SET categoria_id=?pCategoriaId,
                                          linea=?pLinea,
                                          etiquetas=?pEtiquetas,
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
                                    myCmd.Parameters.AddWithValue("?pCategoriaId", linea.categoria_id);
                                    myCmd.Parameters.AddWithValue("?pLinea", linea.linea);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", linea.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", linea.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", linea.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        lineaResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        lineaResponse.codigoError = "E";
                                        lineaResponse.descripcionError = "3. No se pudo actualizar la Línea.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    lineaResponse.codigoError = "E";
                                    lineaResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lineaResponse.codigoError = "E";
                        lineaResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                lineaResponse.codigoError = "E";
                lineaResponse.descripcionError = "1. " + ex.Message;
            }
            return lineaResponse;
        }

        public static Lineas EliminarLinea(LineasEntity linea)
        {
            Lineas lineasResponse = new Lineas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM lineas WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", linea.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        lineasResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        lineasResponse.codigoError = "E";
                                        lineasResponse.descripcionError = "3. No se pudo eliminar la Línea.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    lineasResponse.codigoError = "E";
                                    lineasResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lineasResponse.codigoError = "E";
                        lineasResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                lineasResponse.codigoError = "E";
                lineasResponse.descripcionError = "1. " + ex.Message;
            }
            return lineasResponse;
        }

        public static Lineas ObtenerLineasCategoria(string categoria, string almacen)
        {
            Lineas lineasResponse = new Lineas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct linea
                                        FROM lineas as L   
										INNER JOIN categorias as Ca ON Ca.id = L.categoria_id	
                                        INNER JOIN modelos as MO on MO.linea_id = L.id
										INNER JOIN modelo_productos as MP ON MP.modelo_id = MO.id
										INNER JOIN productos as PR on PR.modelo_producto_id = MP.id
										INNER JOIN inventarios as I on I.producto_id = PR.id
                                        WHERE (categoria_id = ?pcategoria_id and I.almacen_id = ?palmacen_id)
                                        order by linea
                                     ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?palmacen_id", almacen);
                            myCmd.Parameters.AddWithValue("?pcategoria_id", categoria);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                LineasEntity lineasEntity = new LineasEntity()
                                {
                                    linea = dataReader["linea"].ToString()

                                };
                                (lineasResponse.lstLineas ?? new List<LineasEntity>()).Add(lineasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lineasResponse.codigoError = "E";
                        lineasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(lineasResponse.codigoError))
                    {
                        if (lineasResponse.lstLineas.Count == 0)
                        {
                            lineasResponse.codigoError = "E";
                            lineasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            lineasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lineasResponse.codigoError = "E";
                lineasResponse.descripcionError = "1." + ex.Message;
            }
            return lineasResponse;
        }
        public static Lineas ObtenerLineasCategoriaAdm(string categoria)
        {
            Lineas lineasResponse = new Lineas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct linea
                                        FROM lineas as L   
										INNER JOIN categorias as Ca ON Ca.id = L.categoria_id	
                                        INNER JOIN modelos as MO on MO.linea_id = L.id
										INNER JOIN modelo_productos as MP ON MP.modelo_id = MO.id
										INNER JOIN productos as PR on PR.modelo_producto_id = MP.id
										INNER JOIN inventarios as I on I.producto_id = PR.id
                                        WHERE categoria = ?pcategoria
                                        order by linea
                                     ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcategoria", categoria);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                LineasEntity lineasEntity = new LineasEntity()
                                {
                                    linea = dataReader["linea"].ToString()

                                };
                                (lineasResponse.lstLineas ?? new List<LineasEntity>()).Add(lineasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lineasResponse.codigoError = "E";
                        lineasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(lineasResponse.codigoError))
                    {
                        if (lineasResponse.lstLineas.Count == 0)
                        {
                            lineasResponse.codigoError = "E";
                            lineasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            lineasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lineasResponse.codigoError = "E";
                lineasResponse.descripcionError = "1." + ex.Message;
            }
            return lineasResponse;
        }

        public static Lineas ObtenerLineasCategoriaMarca(string categoria, string marca)
        {
            Lineas lineasResponse = new Lineas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct linea
                                        FROM lineas as L   
										INNER JOIN categorias as Ca ON Ca.id = L.categoria_id	
                                        INNER JOIN modelos as MO on MO.linea_id = L.id
                                        INNER JOIN marcas as M on M.id = MO.marca_id
                                        INNER JOIN proveedores as P on P.id = M.proveedor_id
										INNER JOIN modelo_productos as MP ON MP.modelo_id = MO.id
										INNER JOIN productos as PR on PR.modelo_producto_id = MP.id
										INNER JOIN inventarios as I on I.producto_id = PR.id
                                        WHERE Ca.categoria = ?pCategoria AND M.marca = ?pMarca
                                        order by linea
                                           ;";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pCategoria", categoria);
                            myCmd.Parameters.AddWithValue("?pMarca", marca);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                LineasEntity lineasEntity = new LineasEntity()
                                {

                                    linea = dataReader["linea"].ToString(),

                                };
                                (lineasResponse.lstLineas ?? new List<LineasEntity>()).Add(lineasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lineasResponse.codigoError = "E";
                        lineasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(lineasResponse.codigoError))
                    {
                        if (lineasResponse.lstLineas.Count == 0)
                        {
                            lineasResponse.codigoError = "E";
                            lineasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            lineasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lineasResponse.codigoError = "E";
                lineasResponse.descripcionError = "1." + ex.Message;
            }
            return lineasResponse;
        }
        public static Lineas ObtenerCatalogoLineas(string categoria_nombre, string linea)
        {
            Lineas lineasResponse = new Lineas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"select linea, categoria_id
                                        from lineas as L
                                        inner join	categorias as Ca ON Ca.id = L.categoria_id	
                                        WHERE (Ca.categoria = ?pcategoria and linea = ?plinea) ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?plinea", linea);
                            myCmd.Parameters.AddWithValue("?pcategoria", categoria_nombre);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                LineasEntity lineasEntity = new LineasEntity()
                                {
                                    linea = dataReader["linea"].ToString()

                                };
                                (lineasResponse.lstLineas ?? new List<LineasEntity>()).Add(lineasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lineasResponse.codigoError = "E";
                        lineasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(lineasResponse.codigoError))
                    {
                        if (lineasResponse.lstLineas.Count == 0)
                        {
                            lineasResponse.codigoError = "E";
                            lineasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            lineasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lineasResponse.codigoError = "E";
                lineasResponse.descripcionError = "1." + ex.Message;
            }
            return lineasResponse;
        }
        public static Lineas ObtenerCategoriaLinea(string linea)
        {
            Lineas lineasResponse = new Lineas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT L.id, Ca.categoria
                                        from lineas as L
                                        inner join categorias as Ca ON Ca.id = L.categoria_id	
                                        WHERE (linea = ?plinea) ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?plinea", linea);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                LineasEntity lineasEntity = new LineasEntity()
                                {
                                    categoria_nombre = dataReader["categoria"].ToString()

                                };
                                (lineasResponse.lstLineas ?? new List<LineasEntity>()).Add(lineasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lineasResponse.codigoError = "E";
                        lineasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(lineasResponse.codigoError))
                    {
                        if (lineasResponse.lstLineas.Count == 0)
                        {
                            lineasResponse.codigoError = "E";
                            lineasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            lineasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lineasResponse.codigoError = "E";
                lineasResponse.descripcionError = "1." + ex.Message;
            }
            return lineasResponse;
        }

        public static Lineas ObtenerLineasNombre(string categoria_nombre, string linea)
        {
            Lineas lineasResponse = new Lineas();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"select L.id
                                        from lineas as L
                                        inner join	categorias as Ca ON Ca.id = L.categoria_id	
                                        WHERE (Ca.categoria = ?pcategoria and linea = ?plinea) ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?plinea", linea);
                            myCmd.Parameters.AddWithValue("?pcategoria", categoria_nombre);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                LineasEntity lineasEntity = new LineasEntity()
                                {
                                    id = dataReader["id"].ToString()

                                };
                                (lineasResponse.lstLineas ?? new List<LineasEntity>()).Add(lineasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lineasResponse.codigoError = "E";
                        lineasResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(lineasResponse.codigoError))
                    {
                        if (lineasResponse.lstLineas.Count == 0)
                        {
                            lineasResponse.codigoError = "E";
                            lineasResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            lineasResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lineasResponse.codigoError = "E";
                lineasResponse.descripcionError = "1." + ex.Message;
            }
            return lineasResponse;
        }
    }
}