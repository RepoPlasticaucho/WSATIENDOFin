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
    public class DALProductos
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000; Allow User Variables=true;";

        public static ProductosAdm ObtenerProductos()
        {
            ProductosAdm productosResponse = new ProductosAdm();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT P.id,P.nombre,P.tamanio,P.pvp,P.tarifa_ice_iva_id,P.unidad_medida,
                                             TI.descripcion,P.modelo_producto_id,MP.modelo_producto,P.etiquetas,P.cod_sap,
                                             M.marca,C.color,A.atributo,G.genero,MO.modelo, CA.categoria, L.linea
                                        FROM productos as P
                                        INNER
											  JOIN tarifa_ice_iva as TI on TI.id = P.tarifa_ice_iva_id
                                              JOIN modelo_productos as MP ON MP.id=P.modelo_producto_id
                                              JOIN colors as C ON C.id=MP.color_id
                                              JOIN atributos as A on A.id=MP.atributo_id
                                              JOIN generos as G on G.id=MP.genero_id
                                              JOIN modelos as MO on MO.id=MP.modelo_id
                                              JOIN marcas as M ON M.id=MO.marca_id
                                              JOIN lineas as L on L.id = MO.linea_id
                                              JOIN categorias as CA on CA.id = L.categoria_id
                                        WHERE P.es_sincronizado = 1 Order by id";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProductosAdmEntity productosEntity = new ProductosAdmEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    tamanio = dataReader["tamanio"].ToString(),
                                    pvp = dataReader["pvp"].ToString(),
                                    modelo_producto_id = dataReader["modelo_producto_id"].ToString(),
                                    modelo_producto = dataReader["modelo_producto"].ToString(),
                                    marca_nombre = dataReader["marca"].ToString(),
                                    color_nombre = dataReader["color"].ToString(),
                                    atributo_nombre = dataReader["atributo"].ToString(),
                                    genero_nombre = dataReader["genero"].ToString(),
                                    modelo_nombre = dataReader["modelo"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    tarifa_ice_iva_id = dataReader["tarifa_ice_iva_id"].ToString(),
                                    impuesto_nombre = dataReader["descripcion"].ToString(),
                                    unidad_medida =dataReader["unidad_medida"].ToString()
                                };
                                (productosResponse.lstProductos ?? new List<ProductosAdmEntity>()).Add(productosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(productosResponse.codigoError))
                    {
                        if (productosResponse.lstProductos.Count == 0)
                        {
                            productosResponse.codigoError = "E";
                            productosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            productosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1." + ex.Message;
            }
            return productosResponse;
        }


        public static ProductosAdm ObtenerCatalogosProductos(string nombre, string cod_sap)
        {
            ProductosAdm productosResponse = new ProductosAdm();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT P.id,P.nombre,P.tamanio,P.pvp,P.unidad_medida,
                                             P.modelo_producto_id,MP.modelo_producto,P.etiquetas,P.cod_sap,
                                             M.marca,C.color,A.atributo,G.genero,MO.modelo, CA.categoria, L.linea, TI.id as tarifa_id
                                        FROM productos as P
                                        INNER
                                              JOIN modelo_productos as MP ON MP.id=P.modelo_producto_id
                                              JOIN tarifa_ice_iva as TI ON TI.id = P.tarifa_ice_iva_id
                                              JOIN colors as C ON C.id=MP.color_id
                                              JOIN atributos as A on A.id=MP.atributo_id
                                              JOIN generos as G on G.id=MP.genero_id
                                              JOIN modelos as MO on MO.id=MP.modelo_id
                                              JOIN marcas as M ON M.id=MO.marca_id
                                              JOIN lineas as L on L.id = MO.linea_id
                                              JOIN categorias as CA on CA.id = L.categoria_id
                                        WHERE (P.es_sincronizado = 1 AND 
                                               P.nombre=?pnombre AND P.cod_sap =?pcod_sap
                                               )";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pnombre", nombre);
                            myCmd.Parameters.AddWithValue("?pcod_sap", cod_sap);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProductosAdmEntity productosEntity = new ProductosAdmEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    tamanio = dataReader["tamanio"].ToString(),
                                    modelo_producto_id = dataReader["modelo_producto_id"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    pvp = dataReader["pvp"].ToString(),
                                    tarifa_ice_iva_id = dataReader["tarifa_id"].ToString(),
                                    unidad_medida = dataReader["unidad_medida"].ToString(),
                                };
                                (productosResponse.lstProductos ?? new List<ProductosAdmEntity>()).Add(productosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(productosResponse.codigoError))
                    {
                        if (productosResponse.lstProductos.Count == 0)
                        {
                            productosResponse.codigoError = "E";
                            productosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            productosResponse.codigoError = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1." + ex.Message;
            }
            return productosResponse;
        }

        public static ProductosAdm ObtenerProductosProveedor(string id)
        {
            ProductosAdm productosResponse = new ProductosAdm();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT P.id,P.nombre,P.tamanio,P.pvp,
                                             P.modelo_producto_id,MP.modelo_producto,P.etiquetas,P.cod_sap,
                                             M.marca,C.color,A.atributo,G.genero,MO.modelo, CA.categoria, L.linea
                                        FROM productos as P
                                        INNER
                                              JOIN modelo_productos as MP ON MP.id=P.modelo_producto_id
                                              JOIN colors as C ON C.id=MP.color_id
                                              JOIN atributos as A on A.id=MP.atributo_id
                                              JOIN generos as G on G.id=MP.genero_id
                                              JOIN modelos as MO on MO.id=MP.modelo_id
                                              JOIN marcas as M ON M.id=MO.marca_id
                                              JOIN proveedores as PR on PR.id=M.proveedor_id
                                              JOIN lineas as L on L.id = MO.linea_id
                                              JOIN categorias as CA on CA.id = L.categoria_id
                                        WHERE P.es_sincronizado = 1 AND PR.id=?pId";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pId", id);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProductosAdmEntity productosEntity = new ProductosAdmEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    tamanio = dataReader["tamanio"].ToString(),
                                    pvp = dataReader["pvp"].ToString(),
                                    modelo_producto_id = dataReader["modelo_producto_id"].ToString(),
                                    modelo_producto = dataReader["modelo_producto"].ToString(),
                                    marca_nombre = dataReader["marca"].ToString(),
                                    color_nombre = dataReader["color"].ToString(),
                                    atributo_nombre = dataReader["atributo"].ToString(),
                                    genero_nombre = dataReader["genero"].ToString(),
                                    modelo_nombre = dataReader["modelo"].ToString(),
                                    cod_sap = dataReader["cod_sap"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString()
                                };
                                (productosResponse.lstProductos ?? new List<ProductosAdmEntity>()).Add(productosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(productosResponse.codigoError))
                    {
                        if (productosResponse.lstProductos.Count == 0)
                        {
                            productosResponse.codigoError = "E";
                            productosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            productosResponse.codigoError = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1." + ex.Message;
            }
            return productosResponse;
        }

        public static ProductosAdm ObtenerProductosModeloProducto(string modelo_producto)
        {
            ProductosAdm productosResponse = new ProductosAdm();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT P.id,P.nombre,P.modelo_producto_id
                                        FROM productos as P
                                        INNER JOIN modelo_productos as MP on MP.id = P.modelo_producto_id
                                        WHERE (MP.modelo_producto = ?pmodelo_producto AND P.es_sincronizado = 1)";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmodelo_producto", modelo_producto);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProductosAdmEntity productosEntity = new ProductosAdmEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    nombre = dataReader["nombre"].ToString(),
                                    modelo_producto_id = dataReader["modelo_producto_id"].ToString(),
                                };
                                (productosResponse.lstProductos ?? new List<ProductosAdmEntity>()).Add(productosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(productosResponse.codigoError))
                    {
                        if (productosResponse.lstProductos.Count == 0)
                        {
                            productosResponse.codigoError = "E";
                            productosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            productosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1." + ex.Message;
            }
            return productosResponse;
        }

        public static ProductosAdm ObtenerProductosN(string nombre)
        {
            ProductosAdm productosResponse = new ProductosAdm();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT id, nombre
                                        FROM productos
                                        WHERE nombre = ?pnombre AND es_sincronizado = 1";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pnombre", nombre);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProductosAdmEntity productosEntity = new ProductosAdmEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    nombre = dataReader["nombre"].ToString()
                                };
                                (productosResponse.lstProductos ?? new List<ProductosAdmEntity>()).Add(productosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(productosResponse.codigoError))
                    {
                        if (productosResponse.lstProductos.Count == 0)
                        {
                            productosResponse.codigoError = "E";
                            productosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            productosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1." + ex.Message;
            }
            return productosResponse;
        }

        public static ProductosAdm ObtenerProductosNomEti(string nombre, string etiquetas)
        {
            ProductosAdm productosResponse = new ProductosAdm();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT id, nombre
                                        FROM productos
                                        WHERE nombre = ?pNombre OR etiquetas = ?pEtiquetas AND es_sincronizado = 1
                                        LIMIT 1";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pNombre", nombre);
                            myCmd.Parameters.AddWithValue("?pEtiquetas", etiquetas);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProductosAdmEntity productosEntity = new ProductosAdmEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    nombre = dataReader["nombre"].ToString()
                                };
                                (productosResponse.lstProductos ?? new List<ProductosAdmEntity>()).Add(productosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(productosResponse.codigoError))
                    {
                        if (productosResponse.lstProductos.Count == 0)
                        {
                            productosResponse.codigoError = "NEXISTE";
                            productosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            productosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1." + ex.Message;
            }
            return productosResponse;
        }

        public static ProductosAdm VerificarProductosMP(string color_id, string tamanio, string cod_fam)
        {
            ProductosAdm productosResponse = new ProductosAdm();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT P.id, P.tamanio, MP.color_id
                                        FROM productos as P
                                        INNER JOIN modelo_productos as MP on MP.id = P.modelo_producto_id
                                        WHERE MP.color_id = ?pcolor_id and P.tamanio = ?ptamanio and MP.cod_familia = ?pcod_fam";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcolor_id", color_id);
                            myCmd.Parameters.AddWithValue("?ptamanio", tamanio);
                            myCmd.Parameters.AddWithValue("?pcod_fam", cod_fam);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProductosAdmEntity productosEntity = new ProductosAdmEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    tamanio = dataReader["tamanio"].ToString()
                                };
                                (productosResponse.lstProductos ?? new List<ProductosAdmEntity>()).Add(productosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(productosResponse.codigoError))
                    {
                        if (productosResponse.lstProductos.Count == 0)
                        {
                            productosResponse.codigoError = "E";
                            productosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            productosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1." + ex.Message;
            }
            return productosResponse;
        }

        public static ProductosAdm ObtenerProductosID(string tamanio, string color, string cod_fam)
        {
            ProductosAdm productosResponse = new ProductosAdm();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"select productos.id, MP.url_image, productos.pvp
                                        from productos
                                        INNER JOIN modelo_productos as MP on MP.id = productos.modelo_producto_id
                                        INNER JOIN colors as C on C.id = MP.color_id
                                        where tamanio = ?ptamanio and C.color = ?pcolor and cod_familia = ?pcod_fam and productos.es_sincronizado='1'";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?ptamanio", tamanio);
                            myCmd.Parameters.AddWithValue("?pcolor", color);
                            myCmd.Parameters.AddWithValue("?pcod_fam", cod_fam);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProductosAdmEntity productosEntity = new ProductosAdmEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    url_image = dataReader["url_image"].ToString(),
                                    pvp = dataReader["pvp"].ToString()
                                };
                                (productosResponse.lstProductos ?? new List<ProductosAdmEntity>()).Add(productosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(productosResponse.codigoError))
                    {
                        if (productosResponse.lstProductos.Count == 0)
                        {
                            productosResponse.codigoError = "E";
                            productosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            productosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1." + ex.Message;
            }
            return productosResponse;
        }

        public static ProductosAdm ObtenerProductosTamanio(string cod_familia)
        {
            ProductosAdm productosResponse = new ProductosAdm();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"select P.id, P.tamanio
                                        from productos as P
                                        INNER JOIN modelo_productos as MP on MP.id = P.modelo_producto_id
                                        WHERE MP.cod_familia = ?pcod_familia
                                        GROUP BY tamanio";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcod_familia", cod_familia);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                ProductosAdmEntity productosEntity = new ProductosAdmEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    tamanio = dataReader["tamanio"].ToString()
                                };
                                (productosResponse.lstProductos ?? new List<ProductosAdmEntity>()).Add(productosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(productosResponse.codigoError))
                    {
                        if (productosResponse.lstProductos.Count == 0)
                        {
                            productosResponse.codigoError = "E";
                            productosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            productosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1." + ex.Message;
            }
            return productosResponse;
        }

        public static ProductosAdm InsertarProductos(ProductosEntity productosEntity)
        {
            ProductosAdm productosResponse = new ProductosAdm();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO productos
                                    (nombre,tamanio,tarifa_ice_iva_id,modelo_producto_id,unidad_medida,etiquetas,cod_sap,pvp,
                                      es_plasticaucho,es_sincronizado,created_at)
                                       VALUES(?pNombre,?pTamanio,?pTarifa,?pModeloProducto_id,?pUnidad_medida,?pEtiquetas,?pCodSAP,?pPvp,
                                              ?pEsPlasticaucho,?pEsSincronizado,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pNombre", productosEntity.nombre);
                                    myCmd.Parameters.AddWithValue("?pTamanio", productosEntity.tamanio);
                                    myCmd.Parameters.AddWithValue("?pTarifa", productosEntity.tarifa_ice_iva_id);
                                    myCmd.Parameters.AddWithValue("?pModeloProducto_id", productosEntity.modelo_producto_id);
                                    myCmd.Parameters.AddWithValue("?pUnidad_medida", productosEntity.unidad_medida);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", productosEntity.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", productosEntity.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pPvp", productosEntity.pvp);
                                    myCmd.Parameters.AddWithValue("?pEsPlasticaucho", 1);
                                    myCmd.Parameters.AddWithValue("?pEsSincronizado", 1);
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        productosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        productosResponse.codigoError = "E";
                                        productosResponse.descripcionError = "3. No se pudo crear el Producto.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    productosResponse.codigoError = "E";
                                    productosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1. " + ex.Message;
            }
            return productosResponse;
        }

        public static ProductosAdm AgregarProductos(ProductosEntity productosEntity)
        {
            ProductosAdm productosResponse = new ProductosAdm();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO productos
                                    (nombre,tamanio,tarifa_ice_iva_id,modelo_producto_id,unidad_medida,etiquetas,cod_sap,pvp,
                                      es_plasticaucho,es_sincronizado,created_at)
                                       VALUES(?pNombre,?pTamanio,?pTarifa,?pModeloProducto_id,?pUnidad_medida,?pEtiquetas,?pCodSAP,?pPvp,
                                              ?pEsPlasticaucho,?pEsSincronizado,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pNombre", productosEntity.nombre);
                                    myCmd.Parameters.AddWithValue("?pTamanio", productosEntity.tamanio);
                                    myCmd.Parameters.AddWithValue("?pTarifa", productosEntity.tarifa_ice_iva_id);
                                    myCmd.Parameters.AddWithValue("?pModeloProducto_id", productosEntity.modelo_producto_id);
                                    myCmd.Parameters.AddWithValue("?pUnidad_medida", productosEntity.unidad_medida);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", productosEntity.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pCodSAP", productosEntity.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pPvp", productosEntity.pvp);
                                    myCmd.Parameters.AddWithValue("?pEsPlasticaucho", 1);
                                    myCmd.Parameters.AddWithValue("?pEsSincronizado", 1);
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        productosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        productosResponse.codigoError = "E";
                                        productosResponse.descripcionError = "3. No se pudo crear el Producto.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    productosResponse.codigoError = "E";
                                    productosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1. " + ex.Message;
            }
            return productosResponse;
        }

        public static Productos ModificarProductos(ProductosEntity productos)
        {
            Productos productosResponse = new Productos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE productos 
                                      SET nombre=?pNombre,
                                          tamanio=?pTamanio,
                                          tarifa_ice_iva_id=?pTarifa,
                                          modelo_producto_id=?pModelo_producto_id,
                                          etiquetas=?pEtiquetas,
                                          cod_sap=?pCod_sap,
                                          unidad_medida=?pUnidad_Medida,
                                          pvp=?pPvp,
                                          es_plasticaucho=?pEs_plasticaucho,
                                          es_sincronizado=?pEs_sincronizado,
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
                                    myCmd.Parameters.AddWithValue("?pNombre", productos.nombre);
                                    myCmd.Parameters.AddWithValue("?pTamanio", productos.tamanio);
                                    myCmd.Parameters.AddWithValue("?pTarifa", productos.tarifa_ice_iva_id);
                                    myCmd.Parameters.AddWithValue("?pModelo_producto_id", productos.modelo_producto_id);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", productos.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pCod_sap", productos.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pPvp", productos.pvp);
                                    myCmd.Parameters.AddWithValue("?pUnidad_Medida", productos.unidad_medida);
                                    myCmd.Parameters.AddWithValue("?pEs_plasticaucho", productos.es_plasticaucho);
                                    myCmd.Parameters.AddWithValue("?pEs_sincronizado", productos.es_sincronizado);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", productos.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        productosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        productosResponse.codigoError = "E";
                                        productosResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    productosResponse.codigoError = "E";
                                    productosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1. " + ex.Message;
            }
            return productosResponse;
        }

        public static Productos ActualizarProductos(ProductosEntity productos)
        {
            Productos productosResponse = new Productos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE productos 
                                      SET nombre=?pNombre,
                                          tamanio=?pTamanio,
                                          tarifa_ice_iva_id=?pTarifa,
                                          modelo_producto_id=?pModelo_producto_id,
                                          etiquetas=?pEtiquetas,
                                          cod_sap=?pCod_sap,
                                          unidad_medida=?pUnidad_Medida,
                                          pvp=?pPvp,
                                         
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
                                    myCmd.Parameters.AddWithValue("?pNombre", productos.nombre);
                                    myCmd.Parameters.AddWithValue("?pTamanio", productos.tamanio);
                                    myCmd.Parameters.AddWithValue("?pTarifa", productos.tarifa_ice_iva_id);
                                    myCmd.Parameters.AddWithValue("?pModelo_producto_id", productos.modelo_producto_id);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", productos.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pCod_sap", productos.cod_sap);
                                    myCmd.Parameters.AddWithValue("?pPvp", productos.pvp);
                                    myCmd.Parameters.AddWithValue("?pUnidad_Medida", productos.unidad_medida);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", productos.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        productosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        productosResponse.codigoError = "E";
                                        productosResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    productosResponse.codigoError = "E";
                                    productosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1. " + ex.Message;
            }
            return productosResponse;
        }


        public static Productos EliminarProductos(ProductosEntity productos)
        {
            Productos productosResponse = new Productos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE
                                        FROM productos
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
                                    myCmd.Parameters.AddWithValue("?pId", productos.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        productosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        productosResponse.codigoError = "E";
                                        productosResponse.descripcionError = "3. No se pudo eliminar la Sociedad.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    productosResponse.codigoError = "E";
                                    productosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1. " + ex.Message;
            }
            return productosResponse;
        }

        public static Productos DeshabilitarProductos(ProductosEntity productos)
        {
            Productos productosResponse = new Productos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE productos
                                        SET es_sincronizado = 0
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
                                    myCmd.Parameters.AddWithValue("?pId", productos.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        productosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        productosResponse.codigoError = "E";
                                        productosResponse.descripcionError = "3. No se pudo eliminar la Sociedad.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    productosResponse.codigoError = "E";
                                    productosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        productosResponse.codigoError = "E";
                        productosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                productosResponse.codigoError = "E";
                productosResponse.descripcionError = "1. " + ex.Message;
            }
            return productosResponse;
        }
    }
}