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
    public class DALModelo_Productos
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;";

        public static ModeloProductos ObtenerModeloProductos()
        {
            ModeloProductos modeloProductoResponse = new ModeloProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT MP.id,MP.modelo_id,MP.color_id,MP.atributo_id,MP.cod_sap,MP.cod_familia,
                                                MP.genero_id,MP.modelo_producto,MP.etiquetas, MP.url_image,
                                                MO.marca_id,MO.modelo,
                                                M.marca,
                                                C.color,
                                                A.atributo,
                                                G.genero,
                                                CA.categoria,
                                                L.linea
                                                FROM modelo_productos as MP
                                                INNER 
                                                      JOIN modelos as MO on MO.id=MP.modelo_id
                                                      JOIN marcas as M ON M.id=MO.marca_id
                                                      JOIN colors as C ON C.id=MP.color_id
                                                      JOIN atributos as A on A.id=MP.atributo_id
                                                      JOIN generos as G on G.id=MP.genero_id
                                                      JOIN lineas as L on MO.linea_id=L.id
                                                      JOIN categorias as CA on L.categoria_id=CA.id
                                                WHERE MP.es_sincronizado = 1";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModeloProductosEntity modeloProductoEntity = new ModeloProductosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    marca_id = dataReader["marca_id"].ToString(),
                                    modelo_id = dataReader["modelo_id"].ToString(),
                                    color_id = dataReader["color_id"].ToString(),
                                    atributo_id = dataReader["atributo_id"].ToString(),
                                    cod_familia = dataReader["cod_familia"].ToString(),
                                    genero_id = dataReader["genero_id"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    modelo_producto = dataReader["modelo_producto"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    color = dataReader["color"].ToString(),
                                    atributo = dataReader["atributo"].ToString(),
                                    genero = dataReader["genero"].ToString(),
                                    modelo = dataReader["modelo"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    url_image = dataReader["url_image"].ToString(),
                                };
                                (modeloProductoResponse.lstModelo_Productos ?? new List<ModeloProductosEntity>()).Add(modeloProductoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductoResponse.codigoError = "E";
                        modeloProductoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modeloProductoResponse.codigoError))
                    {
                        if (modeloProductoResponse.lstModelo_Productos.Count == 0)
                        {
                            modeloProductoResponse.codigoError = "E";
                            modeloProductoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modeloProductoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modeloProductoResponse.codigoError = "E";
                modeloProductoResponse.descripcionError = "1." + ex.Message;
            }
            return modeloProductoResponse;
        }


        public static ModeloProductos InsertarModeloProductos(ModeloProductosEntity modeloProductosEntity)
        {
            ModeloProductos modeloProductosResponse = new ModeloProductos();
            if (!modeloProductosEntity.url_image.Contains("/"))
            {
                modeloProductosEntity.url_image = "https://calidad.atiendo.ec/eojgprlg/ModeloProducto/" + modeloProductosEntity.url_image;
            } else
            {
                modeloProductosEntity.url_image = "https://calidad.atiendo.ec/eojgprlg/ModeloProducto/producto.png";
            }
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO modelo_productos(modelo_id,color_id,atributo_id,genero_id,modelo_producto,etiquetas,cod_sap,cod_familia,es_plasticaucho,es_sincronizado,created_at,url_image)
                                       VALUES(?pModeloId,?pColorId,?pAtributoId,?pGeneroId,?pModeloProducto,?pEtiquetas,?pCodSAP,?pFamilia,?pEsPlasticaucho,?pEsSincronizado,?pCreatedAt,?pUrl)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pModeloId", modeloProductosEntity.modelo_id);
                                    myCmd.Parameters.AddWithValue("?pColorId", modeloProductosEntity.color_id);
                                    myCmd.Parameters.AddWithValue("?pAtributoId", modeloProductosEntity.atributo_id);
                                    myCmd.Parameters.AddWithValue("?pGeneroId", modeloProductosEntity.genero_id);
                                    myCmd.Parameters.AddWithValue("?pModeloProducto", modeloProductosEntity.modelo_producto);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", modeloProductosEntity.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", modeloProductosEntity.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pFamilia", modeloProductosEntity.cod_familia);
                                    myCmd.Parameters.AddWithValue("?pEsPlasticaucho", "1");
                                    myCmd.Parameters.AddWithValue("?pEsSincronizado", "1");
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pUrl", modeloProductosEntity.url_image);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        modeloProductosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        modeloProductosResponse.codigoError = "E";
                                        modeloProductosResponse.descripcionError = "3. No se pudo crear el Modelo Producto.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    modeloProductosResponse.codigoError = "E";
                                    modeloProductosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductosResponse.codigoError = "E";
                        modeloProductosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                modeloProductosResponse.codigoError = "E";
                modeloProductosResponse.descripcionError = "1. " + ex.Message;
            }
            return modeloProductosResponse;
        }
        public static ModeloProductos ModificarModeloProductos(ModeloProductosEntity modeloProductosEntity)
        {
            ModeloProductos modeloProductosResponse = new ModeloProductos();
            if (!modeloProductosEntity.url_image.Contains("/"))
            {
                modeloProductosEntity.url_image = "https://calidad.atiendo.ec/eojgprlg/ModeloProducto/" + modeloProductosEntity.url_image;
            }
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE modelo_productos 
                                      SET modelo_id=?pModeloId,
                                          color_id=?pColorId,
                                          atributo_id=?pAtributoId,
                                          genero_id=?pGeneroId,
                                          modelo_producto=?pModeloProducto,
                                          etiquetas=?pEtiquetas,
                                          cod_familia=?pFamilia,
                                          cod_sap=?pCodSAP,
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
                                    myCmd.Parameters.AddWithValue("?pModeloId", modeloProductosEntity.modelo_id);
                                    myCmd.Parameters.AddWithValue("?pColorId", modeloProductosEntity.color_id);
                                    myCmd.Parameters.AddWithValue("?pAtributoId", modeloProductosEntity.atributo_id);
                                    myCmd.Parameters.AddWithValue("?pGeneroId", modeloProductosEntity.genero_id);
                                    myCmd.Parameters.AddWithValue("?pModeloProducto", modeloProductosEntity.modelo_producto);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", modeloProductosEntity.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pFamilia", modeloProductosEntity.cod_familia);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", modeloProductosEntity.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", modeloProductosEntity.id);
                                    myCmd.Parameters.AddWithValue("?pUrl", modeloProductosEntity.url_image);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        modeloProductosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        modeloProductosResponse.codigoError = "E";
                                        modeloProductosResponse.descripcionError = "3. No se pudo actualizar el Atributo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    modeloProductosResponse.codigoError = "E";
                                    modeloProductosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductosResponse.codigoError = "E";
                        modeloProductosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                modeloProductosResponse.codigoError = "E";
                modeloProductosResponse.descripcionError = "1. " + ex.Message;
            }
            return modeloProductosResponse;
        }

        public static ModeloProductos ActualizarModeloProductos(ModeloProductosEntity modeloProductosEntity)
        {
            ModeloProductos modeloProductosResponse = new ModeloProductos();
            if (!modeloProductosEntity.url_image.Contains("/"))
            {
                modeloProductosEntity.url_image = "https://calidad.atiendo.ec/eojgprlg/ModeloProducto/" + modeloProductosEntity.url_image;
            }
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE modelo_productos 
                                      SET cod_familia = ?pCodFamilia,
                                          cod_sap=?pCodSAP,
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
                                    myCmd.Parameters.AddWithValue("?pCodSAP", modeloProductosEntity.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pCodFamilia", modeloProductosEntity.cod_familia);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", modeloProductosEntity.id);
                                    myCmd.Parameters.AddWithValue("?pUrl", modeloProductosEntity.url_image);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        modeloProductosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        modeloProductosResponse.codigoError = "E";
                                        modeloProductosResponse.descripcionError = "3. No se pudo actualizar el Atributo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    modeloProductosResponse.codigoError = "E";
                                    modeloProductosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductosResponse.codigoError = "E";
                        modeloProductosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                modeloProductosResponse.codigoError = "E";
                modeloProductosResponse.descripcionError = "1. " + ex.Message;
            }
            return modeloProductosResponse;
        }
        public static ModeloProductos EliminarModeloProductos(ModeloProductosEntity modeloProductosEntity)
        {
            ModeloProductos modeloProductosResponse = new ModeloProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE modelo_productos,productos 
                                        FROM modelo_productos
                                        LEFT JOIN productos ON modelo_productos.id =productos.modelo_producto_id 
                                        WHERE modelo_productos.id=?pIdModelProduct";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pIdModelProduct", modeloProductosEntity.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        modeloProductosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        modeloProductosResponse.codigoError = "E";
                                        modeloProductosResponse.descripcionError = "3. No se pudo eliminar el Atributo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    modeloProductosResponse.codigoError = "E";
                                    modeloProductosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductosResponse.codigoError = "E";
                        modeloProductosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                modeloProductosResponse.codigoError = "E";
                modeloProductosResponse.descripcionError = "1. " + ex.Message;
            }
            return modeloProductosResponse;
        }

        public static ModeloProductos DeshabilitarModeloProductos(ModeloProductosEntity modeloProductosEntity)
        {
            ModeloProductos modeloProductosResponse = new ModeloProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE modelo_productos
                                        LEFT JOIN productos ON modelo_productos.id =productos.modelo_producto_id
                                        SET modelo_productos.es_sincronizado = 0,
                                        productos.es_sincronizado = 0
                                        WHERE modelo_productos.id=?pIdModelProduct";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pIdModelProduct", modeloProductosEntity.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        modeloProductosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        modeloProductosResponse.codigoError = "E";
                                        modeloProductosResponse.descripcionError = "3. No se pudo actualizar el Atributo.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    modeloProductosResponse.codigoError = "E";
                                    modeloProductosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductosResponse.codigoError = "E";
                        modeloProductosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                modeloProductosResponse.codigoError = "E";
                modeloProductosResponse.descripcionError = "1. " + ex.Message;
            }
            return modeloProductosResponse;
        }

        public static ModeloProductos ObtenerModeloProductosModelos(string modelo, string almacen)
        {
            ModeloProductos modeloProductoResponse = new ModeloProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct color
                                                FROM modelo_productos as MP
                                                INNER 
                                                      JOIN modelos as MO on MO.id=MP.modelo_id
                                                      JOIN marcas as M ON M.id=MO.marca_id
                                                      JOIN colors as C ON C.id=MP.color_id
                                                      JOIN atributos as A on A.id=MP.atributo_id
                                                      JOIN generos as G on G.id=MP.genero_id
                                                      JOIN productos as PR on PR.modelo_producto_id = MP.id
                                                      JOIN inventarios as I on I.producto_id = PR.id
                                                Where (modelo = ?pmodeloid and I.almacen_id = ?palmacenid and MP.es_sincronizado = 1)
                                                order by color";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmodeloid", modelo);
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModeloProductosEntity modeloProductoEntity = new ModeloProductosEntity()
                                {
                                    color = dataReader["color"].ToString()
                                  
                                };
                                (modeloProductoResponse.lstModelo_Productos ?? new List<ModeloProductosEntity>()).Add(modeloProductoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductoResponse.codigoError = "E";
                        modeloProductoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modeloProductoResponse.codigoError))
                    {
                        if (modeloProductoResponse.lstModelo_Productos.Count == 0)
                        {
                            modeloProductoResponse.codigoError = "E";
                            modeloProductoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modeloProductoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modeloProductoResponse.codigoError = "E";
                modeloProductoResponse.descripcionError = "1." + ex.Message;
            }
            return modeloProductoResponse;
        }
        public static ModeloProductos ObtenerModeloProductosModelosAdm(string modelo)
        {
            ModeloProductos modeloProductoResponse = new ModeloProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct MP.modelo_producto, MP.id
                                                FROM modelo_productos as MP
                                                INNER JOIN modelos as MO on MO.id=MP.modelo_id
                                                Where (modelo = ?pmodelo AND MP.es_sincronizado = 1)";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmodelo", modelo);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModeloProductosEntity modeloProductoEntity = new ModeloProductosEntity()
                                {
                                    modelo_producto = dataReader["modelo_producto"].ToString(),
                                    id = dataReader["id"].ToString()

                                };
                                (modeloProductoResponse.lstModelo_Productos ?? new List<ModeloProductosEntity>()).Add(modeloProductoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductoResponse.codigoError = "E";
                        modeloProductoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modeloProductoResponse.codigoError))
                    {
                        if (modeloProductoResponse.lstModelo_Productos.Count == 0)
                        {
                            modeloProductoResponse.codigoError = "E";
                            modeloProductoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modeloProductoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modeloProductoResponse.codigoError = "E";
                modeloProductoResponse.descripcionError = "1." + ex.Message;
            }
            return modeloProductoResponse;
        }

        public static ModeloProductos ObtenerModeloProductosMarca(string marca)
        {
            ModeloProductos modeloProductoResponse = new ModeloProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT MP.id,MP.modelo_id,MP.color_id,MP.atributo_id,MP.cod_sap,MP.cod_familia,
                                                MP.genero_id,MP.modelo_producto,MP.etiquetas, MP.url_image,
                                                MO.marca_id,MO.modelo,
                                                M.marca,
                                                C.color,
                                                A.atributo,
                                                G.genero,
                                                CA.categoria,
                                                L.linea
                                                FROM modelo_productos as MP
                                                INNER 
                                                      JOIN modelos as MO on MO.id=MP.modelo_id
                                                      JOIN marcas as M ON M.id=MO.marca_id
                                                      JOIN colors as C ON C.id=MP.color_id
                                                      JOIN atributos as A on A.id=MP.atributo_id
                                                      JOIN generos as G on G.id=MP.genero_id
                                                      JOIN lineas as L on MO.linea_id=L.id
                                                      JOIN categorias as CA on L.categoria_id=CA.id
                                                WHERE MP.es_sincronizado = 1 AND M.marca = ?pmarca
                                                GROUP BY MP.cod_familia
												HAVING COUNT(DISTINCT MP.cod_familia) = 1
                                                ORDER BY MO.modelo";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmarca", marca);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModeloProductosEntity modeloProductoEntity = new ModeloProductosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    marca_id = dataReader["marca_id"].ToString(),
                                    modelo_id = dataReader["modelo_id"].ToString(),
                                    color_id = dataReader["color_id"].ToString(),
                                    atributo_id = dataReader["atributo_id"].ToString(),
                                    cod_familia = dataReader["cod_familia"].ToString(),
                                    genero_id = dataReader["genero_id"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    modelo_producto = dataReader["modelo_producto"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    color = dataReader["color"].ToString(),
                                    atributo = dataReader["atributo"].ToString(),
                                    genero = dataReader["genero"].ToString(),
                                    modelo = dataReader["modelo"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    url_image = dataReader["url_image"].ToString(),
                                };
                                (modeloProductoResponse.lstModelo_Productos ?? new List<ModeloProductosEntity>()).Add(modeloProductoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductoResponse.codigoError = "E";
                        modeloProductoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modeloProductoResponse.codigoError))
                    {
                        if (modeloProductoResponse.lstModelo_Productos.Count == 0)
                        {
                            modeloProductoResponse.codigoError = "E";
                            modeloProductoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modeloProductoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modeloProductoResponse.codigoError = "E";
                modeloProductoResponse.descripcionError = "1." + ex.Message;
            }
            return modeloProductoResponse;
        }

        public static ModeloProductos ObtenerModeloProductosAlmacen(string almacen)
        {
            ModeloProductos modeloProductoResponse = new ModeloProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT MP.id,MP.modelo_id,MP.color_id,MP.atributo_id,MP.cod_sap,MP.cod_familia,
                                                MP.genero_id,MP.modelo_producto,MP.etiquetas, MP.url_image,
                                                MO.marca_id,MO.modelo,
                                                M.marca,
                                                C.color,
                                                A.atributo,
                                                G.genero,
                                                CA.categoria,
                                                L.linea
                                                FROM modelo_productos as MP
                                                INNER 
                                                      JOIN modelos as MO on MO.id=MP.modelo_id
                                                      JOIN marcas as M ON M.id=MO.marca_id
                                                      JOIN colors as C ON C.id=MP.color_id
                                                      JOIN atributos as A on A.id=MP.atributo_id
                                                      JOIN generos as G on G.id=MP.genero_id
                                                      JOIN lineas as L on MO.linea_id=L.id
                                                      JOIN productos as P on P.modelo_producto_id = MP.id
                                                      JOIN inventarios as I on I.producto_id = P.id
                                                      JOIN categorias as CA on L.categoria_id=CA.id
                                                WHERE MP.es_sincronizado = 1 AND I.almacen_id = ?pAlmacen_id
                                                GROUP BY MP.cod_familia
												HAVING COUNT(DISTINCT MP.cod_familia) = 1
                                                ORDER BY MO.modelo";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pAlmacen_id", almacen);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModeloProductosEntity modeloProductoEntity = new ModeloProductosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    marca_id = dataReader["marca_id"].ToString(),
                                    modelo_id = dataReader["modelo_id"].ToString(),
                                    color_id = dataReader["color_id"].ToString(),
                                    atributo_id = dataReader["atributo_id"].ToString(),
                                    cod_familia = dataReader["cod_familia"].ToString(),
                                    genero_id = dataReader["genero_id"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    modelo_producto = dataReader["modelo_producto"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    color = dataReader["color"].ToString(),
                                    atributo = dataReader["atributo"].ToString(),
                                    genero = dataReader["genero"].ToString(),
                                    modelo = dataReader["modelo"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    url_image = dataReader["url_image"].ToString(),
                                };
                                (modeloProductoResponse.lstModelo_Productos ?? new List<ModeloProductosEntity>()).Add(modeloProductoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductoResponse.codigoError = "E";
                        modeloProductoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modeloProductoResponse.codigoError))
                    {
                        if (modeloProductoResponse.lstModelo_Productos.Count == 0)
                        {
                            modeloProductoResponse.codigoError = "E";
                            modeloProductoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modeloProductoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modeloProductoResponse.codigoError = "E";
                modeloProductoResponse.descripcionError = "1." + ex.Message;
            }
            return modeloProductoResponse;
        }

        public static ModeloProductos ObtenerModeloProductosColor(string cod_familia)
        {
            ModeloProductos modeloProductoResponse = new ModeloProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"select MP.id, MP.color_id, C.color from modelo_productos as MP
                                      INNER JOIN colors as C on C.id = MP.color_id where cod_familia=?pcod_familia";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcod_familia", cod_familia);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModeloProductosEntity modeloProductoEntity = new ModeloProductosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    color_id = dataReader["color_id"].ToString(),
                                    color = dataReader["color"].ToString()
                                };
                                (modeloProductoResponse.lstModelo_Productos ?? new List<ModeloProductosEntity>()).Add(modeloProductoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductoResponse.codigoError = "E";
                        modeloProductoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modeloProductoResponse.codigoError))
                    {
                        if (modeloProductoResponse.lstModelo_Productos.Count == 0)
                        {
                            modeloProductoResponse.codigoError = "E";
                            modeloProductoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modeloProductoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modeloProductoResponse.codigoError = "E";
                modeloProductoResponse.descripcionError = "1." + ex.Message;
            }
            return modeloProductoResponse;
        }

        public static ModeloProductos ObtenerCatalogoModeloProductos(string modelo_id, string color_id, string atributo_id, string genero_id)
        {
            ModeloProductos modeloProductoResponse = new ModeloProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT MP.color_id,MP.modelo_id,MP.genero_id,MP.atributo_id,
                                             MP.modelo_producto,MP.cod_sap,MP.cod_familia,MP.id
	                                    FROM modelo_productos as MP
	                                    INNER
	                                          JOIN colors as C ON C.id=MP.color_id
	                                          JOIN atributos as A on A.id=MP.atributo_id
	                                          JOIN generos as G on G.id=MP.genero_id
	                                          JOIN modelos as MO on MO.id=MP.modelo_id
	                                    where ( C.id = ?pcolor and 
                                                A.id = ?patributo and G.id =?pgenero and 
                                                MO.id = ?pmodelo and MP.es_sincronizado = 1)
	                                   order by modelo_id";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmodelo", modelo_id);
                            myCmd.Parameters.AddWithValue("?pgenero", genero_id);
                            myCmd.Parameters.AddWithValue("?patributo", atributo_id);
                            myCmd.Parameters.AddWithValue("?pcolor", color_id);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModeloProductosEntity modeloProductoEntity = new ModeloProductosEntity()
                                {   
                                    id = dataReader["id"].ToString(),
                                    color_id = dataReader["color_id"].ToString(),
                                    genero_id = dataReader["genero_id"].ToString(),
                                    modelo_id = dataReader["modelo_id"].ToString(),
                                    atributo_id = dataReader["atributo_id"].ToString(),
                                    modelo_producto = dataReader["modelo_producto"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    cod_familia = dataReader["cod_familia"].ToString()

                                };
                                (modeloProductoResponse.lstModelo_Productos ?? new List<ModeloProductosEntity>()).Add(modeloProductoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductoResponse.codigoError = "E";
                        modeloProductoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modeloProductoResponse.codigoError))
                    {
                        if (modeloProductoResponse.lstModelo_Productos.Count == 0)
                        {
                            modeloProductoResponse.codigoError = "E";
                            modeloProductoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modeloProductoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modeloProductoResponse.codigoError = "E";
                modeloProductoResponse.descripcionError = "1." + ex.Message;
            }
            return modeloProductoResponse;
        }
        public static ModeloProductos ObtenerModeloProductosNombre(string modelo_producto)
        {
            ModeloProductos modeloProductoResponse = new ModeloProductos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT id, modelo_producto
                                                FROM modelo_productos
                                                Where (modelo_producto = ?pmodelo_producto AND es_sincronizado = 1)
                                                order by id";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmodelo_producto", modelo_producto);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ModeloProductosEntity modeloProductoEntity = new ModeloProductosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    modelo_producto = dataReader["modelo_producto"].ToString()

                                };
                                (modeloProductoResponse.lstModelo_Productos ?? new List<ModeloProductosEntity>()).Add(modeloProductoEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        modeloProductoResponse.codigoError = "E";
                        modeloProductoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(modeloProductoResponse.codigoError))
                    {
                        if (modeloProductoResponse.lstModelo_Productos.Count == 0)
                        {
                            modeloProductoResponse.codigoError = "E";
                            modeloProductoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            modeloProductoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                modeloProductoResponse.codigoError = "E";
                modeloProductoResponse.descripcionError = "1." + ex.Message;
            }
            return modeloProductoResponse;
        }

        
    }
}