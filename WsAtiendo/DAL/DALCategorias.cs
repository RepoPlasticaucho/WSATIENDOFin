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
    public class DALCategorias
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;Allow User Variables=true;";

        public static Categorias ObtenerCategorias()
        {
            Categorias categoriaResponse = new Categorias();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM categorias";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CategoriasEntity categoriasEntity = new CategoriasEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    es_plasticaucho = dataReader["es_plasticaucho"].ToString(),
                                    es_sincronizado = dataReader["es_sincronizado"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                };
                                (categoriaResponse.lstCategorias ?? new List<CategoriasEntity>()).Add(categoriasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        categoriaResponse.codigoError = "E";
                        categoriaResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(categoriaResponse.codigoError))
                    {
                        if (categoriaResponse.lstCategorias.Count == 0)
                        {
                            categoriaResponse.codigoError = "E";
                            categoriaResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            categoriaResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                categoriaResponse.codigoError = "E";
                categoriaResponse.descripcionError = "1." + ex.Message;
            }
            return categoriaResponse;
        }

        public static Categorias ObtenerCategoriasid(string categoria)
        {
            Categorias categoriaResponse = new Categorias();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM categorias where id = ?pcategoriaid";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcategoria", categoria);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CategoriasEntity categoriasEntity = new CategoriasEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    es_plasticaucho = dataReader["es_plasticaucho"].ToString(),
                                    es_sincronizado = dataReader["es_sincronizado"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                };
                                (categoriaResponse.lstCategorias ?? new List<CategoriasEntity>()).Add(categoriasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        categoriaResponse.codigoError = "E";
                        categoriaResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(categoriaResponse.codigoError))
                    {
                        if (categoriaResponse.lstCategorias.Count == 0)
                        {
                            categoriaResponse.codigoError = "E";
                            categoriaResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            categoriaResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                categoriaResponse.codigoError = "E";
                categoriaResponse.descripcionError = "1." + ex.Message;
            }
            return categoriaResponse;
        }

        public static Categorias ObtenerCategoriaNombre(string categoria)
        {
            Categorias categoriaResponse = new Categorias();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * FROM categorias where categoria = ?pcategoria";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcategoria", categoria);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CategoriasEntity categoriasEntity = new CategoriasEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    es_plasticaucho = dataReader["es_plasticaucho"].ToString(),
                                    es_sincronizado = dataReader["es_sincronizado"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                };
                                (categoriaResponse.lstCategorias ?? new List<CategoriasEntity>()).Add(categoriasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        categoriaResponse.codigoError = "E";
                        categoriaResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(categoriaResponse.codigoError))
                    {
                        if (categoriaResponse.lstCategorias.Count == 0)
                        {
                            categoriaResponse.codigoError = "E";
                            categoriaResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            categoriaResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                categoriaResponse.codigoError = "E";
                categoriaResponse.descripcionError = "1." + ex.Message;
            }
            return categoriaResponse;
        }

        public static Categorias ObtenerCategoriaMarca(string marca)
        {
            Categorias categoriaResponse = new Categorias();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct categoria
                                        FROM categorias as C
                                        INNER JOIN lineas as L on L.categoria_id = C.id
                                        INNER JOIN modelos as M on M.linea_id = L.id
                                        INNER JOIN marcas as Ma on Ma.id = M.marca_id
                                        WHERE Ma.marca = ?pMarca";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pMarca", marca);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CategoriasEntity categoriasEntity = new CategoriasEntity()
                                {
                                    categoria = dataReader["categoria"].ToString()
                                };
                                (categoriaResponse.lstCategorias ?? new List<CategoriasEntity>()).Add(categoriasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        categoriaResponse.codigoError = "E";
                        categoriaResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(categoriaResponse.codigoError))
                    {
                        if (categoriaResponse.lstCategorias.Count == 0)
                        {
                            categoriaResponse.codigoError = "E";
                            categoriaResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            categoriaResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                categoriaResponse.codigoError = "E";
                categoriaResponse.descripcionError = "1." + ex.Message;
            }
            return categoriaResponse;
        }

        public static Categorias ObtenerCategoriasPro(string proveedor)
        {
            Categorias categoriaResponse = new Categorias();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct categoria
                                        FROM categorias as C
                                        INNER JOIN lineas as L on L.categoria_id = C.id
                                        INNER JOIN modelos as M on M.linea_id = L.id
                                        INNER JOIN modelo_productos as MP on MP.modelo_id = M.id
                                        INNER JOIN productos as P on P.modelo_producto_id = MP.id
                                        INNER JOIN proveedores_productos as PP on PP.producto_id = P.id
                                        INNER JOIN proveedores as Pr on Pr.id = PP.provedor_id
                                        WHERE Pr.nombre = ?pProveedor";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pProveedor", proveedor);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                CategoriasEntity categoriasEntity = new CategoriasEntity()
                                {
                                    categoria = dataReader["categoria"].ToString()
                                };
                                (categoriaResponse.lstCategorias ?? new List<CategoriasEntity>()).Add(categoriasEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        categoriaResponse.codigoError = "E";
                        categoriaResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(categoriaResponse.codigoError))
                    {
                        if (categoriaResponse.lstCategorias.Count == 0)
                        {
                            categoriaResponse.codigoError = "E";
                            categoriaResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            categoriaResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                categoriaResponse.codigoError = "E";
                categoriaResponse.descripcionError = "1." + ex.Message;
            }
            return categoriaResponse;
        }

        public static Categorias InsertarCategorias(CategoriasEntity categoria)
        {
            Categorias categoriaResponse = new Categorias();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO categorias(categoria,cod_sap,etiquetas,es_plasticaucho,es_sincronizado,created_at)
                                       VALUES(?pCategoria,?pCodSAP,?pEtiquetas,?pEsPlasticaucho,?pEsSincronizado,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pCategoria", categoria.categoria);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", categoria.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", categoria.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pEsPlasticaucho", "1");
                                    myCmd.Parameters.AddWithValue("?pEsSincronizado", "1");
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        categoriaResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        categoriaResponse.codigoError = "E";
                                        categoriaResponse.descripcionError = "3. No se pudo crear la Categoria.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    categoriaResponse.codigoError = "E";
                                    categoriaResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        categoriaResponse.codigoError = "E";
                        categoriaResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                categoriaResponse.codigoError = "E";
                categoriaResponse.descripcionError = "1. " + ex.Message;
            }
            return categoriaResponse;
        }

        public static Categorias ModificarCategoria(CategoriasEntity categoria)
        {
            Categorias categoriaResponse = new Categorias();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE categorias 
                                      SET categoria=?pCategoria,
                                          cod_sap=?pCodSAP,
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
                                    myCmd.Parameters.AddWithValue("?pCategoria", categoria.categoria);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", categoria.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", categoria.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", categoria.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        categoriaResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        categoriaResponse.codigoError = "E";
                                        categoriaResponse.descripcionError = "3. No se pudo actualizar la Categoria.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    categoriaResponse.codigoError = "E";
                                    categoriaResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        categoriaResponse.codigoError = "E";
                        categoriaResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                categoriaResponse.codigoError = "E";
                categoriaResponse.descripcionError = "1. " + ex.Message;
            }
            return categoriaResponse;
        }

        public static Categorias EliminarCategoria(CategoriasEntity categoria)
        {
            Categorias categoriasResponse = new Categorias();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM categorias WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", categoria.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        categoriasResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        categoriasResponse.codigoError = "E";
                                        categoriasResponse.descripcionError = "3. No se pudo eliminar la Categoria.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    categoriasResponse.codigoError = "E";
                                    categoriasResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        categoriasResponse.codigoError = "E";
                        categoriasResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                categoriasResponse.codigoError = "E";
                categoriasResponse.descripcionError = "1. " + ex.Message;
            }
            return categoriasResponse;
        }

    }

}