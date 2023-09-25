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
    public class DALInventarios
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=60000;Allow User Variables=true;";

        public static Inventarios ObtenerInventarios()
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"Select
                                        C.categoria,
                                        L.linea,
                                        M.marca,
                                        P.id as idProducto, concat(MP.modelo_producto, ' ', P.tamanio) as Producto,
                                        A.direccion,P.tamanio as talla, P.unidad_medida
                                        I.id as idInvetario,stock,stock_optimo,fav,producto_id,TI.descripcion as tarifa
                                        FROM inventarios as I
                                        INNER
                                            JOIN almacenes as A on A.id = almacen_id
                                            JOIN productos as P on P.id = producto_id
                                            JOIN tarifa_ice_iva as TI on TI.id = P.tarifa_iva_ice_id
                                            JOIN modelo_productos as MP on MP.id = P.modelo_producto_id 
                                            JOIN modelos as Mo on MP.modelo_id = Mo.id
                                            JOIN marcas as M on Mo.marca_id = M.id
                                            JOIN lineas as L on Mo.linea_id = L.id
                                            JOIN categorias as C on C.id = L.categoria_id
                                        WHERE I.es_sincronizado = 1
                                        order by idProducto
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                InventariosEntity inventariosEntity = new InventariosEntity()
                                {
                                    id = dataReader["idInvetario"].ToString(),
                                    almacen_id = dataReader["direccion"].ToString(),
                                    producto_id = dataReader["producto_id"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    Producto = dataReader["Producto"].ToString(),
                                    talla = dataReader["talla"].ToString(),
                                    unidad_medidad = dataReader["unidad_medida"].ToString(),
                                    fav = dataReader["fav"].ToString(),
                                    stock = dataReader["stock"].ToString(),
                                    stock_optimo = dataReader["stock_optimo"].ToString(),
                                    tarifa_ice_iva = dataReader["tarifa"].ToString()
                                };
                                (inventariosResponse.lstInventarios ?? new List<InventariosEntity>()).Add(inventariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(inventariosResponse.codigoError))
                    {
                        if (inventariosResponse.lstInventarios.Count == 0)
                        {
                            inventariosResponse.codigoError = "E";
                            inventariosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            inventariosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1." + ex.Message;
            }
            return inventariosResponse;
        }

        public static Inventarios ObtenerPortafolios(string almacen)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"Select C.categoria, L.linea, I.etiquetas, M.marca, Mo.modelo, I.costo, Co.color, Ge.genero, Atr.atributo,
                                        MP.modelo_producto, P.nombre,T.descripcion as IVA ,TII.descripcion as ICE, MP.url_image,
                                        P.id as idProducto, concat(MP.modelo_producto, ' ', P.tamanio) as Producto,
                                        A.direccion,P.tamanio,P.unidad_medida,
                                        I.id as idInvetario,almacen_id,stock,stock_optimo,fav,pvp2,producto_id,costo,
                                        TI.descripcion
                                        FROM inventarios as I
                                        INNER
                                            JOIN almacenes as A on A.id = almacen_id 
                                            JOIN productos as P on P.id = producto_id
                                            JOIN tarifa_ice_iva as TI on TI.id = P.tarifa_ice_iva_id
                                            JOIN tarifa_ice_iva as TII on TII.id = P.tarifa_ice_iva_id1
                                            JOIN modelo_productos as MP on MP.id = P.modelo_producto_id
                                            JOIN colors as Co on Co.id = MP.color_id
                                            JOIN generos as Ge on Ge.id = MP.genero_id
                                            JOIN atributos as Atr on Atr.id = MP.atributo_id
                                            JOIN modelos as Mo on MP.modelo_id = Mo.id
                                            JOIN tarifa_ice_iva as T on T.id = P.tarifa_ice_iva_id
                                            JOIN marcas as M on Mo.marca_id = M.id
                                            JOIN lineas as L on Mo.linea_id = L.id
                                            JOIN categorias as C on C.id = L.categoria_id
                                            Where A.id = ?palmacenid AND I.es_sincronizado = 1
                                        order by idProducto  
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                InventariosEntity inventariosEntity = new InventariosEntity()
                                {

                                    id = dataReader["idInvetario"].ToString(),
                                    almacen_id = dataReader["direccion"].ToString(),
                                    tarifa_ice_iva = dataReader["IVA"].ToString(),
                                    tarifa_ice_iva1 = dataReader["ICE"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    costo = dataReader["costo"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    color = dataReader["color"].ToString(),
                                    genero = dataReader["genero"].ToString(),
                                    atributo = dataReader["atributo"].ToString(),
                                    Producto = dataReader["Producto"].ToString(),
                                    producto_id = dataReader["producto_id"].ToString(),
                                    modelo = dataReader["modelo"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    fav = dataReader["fav"].ToString(),
                                    url_image = dataReader["url_image"].ToString(),
                                    stock = dataReader["stock"].ToString(),
                                    stock_optimo = dataReader["stock_optimo"].ToString(),
                                    producto_nombre = dataReader["nombre"].ToString(),
                                    pvp2 = dataReader["pvp2"].ToString(),
                                    talla = dataReader["tamanio"].ToString(),
                                    unidad_medidad = dataReader["unidad_medida"].ToString(),
                                    modelo_producto = dataReader["modelo_producto"].ToString(),
                                   
                                };
                                (inventariosResponse.lstInventarios ?? new List<InventariosEntity>()).Add(inventariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(inventariosResponse.codigoError))
                    {
                        if (inventariosResponse.lstInventarios.Count == 0)
                        {
                            inventariosResponse.codigoError = "E";
                            inventariosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            inventariosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1." + ex.Message;
            }
            return inventariosResponse;
        }

        public static Inventarios ObtenerInventarioAlm(string producto, string almacen)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"Select inventarios.id, inventarios.costo, inventarios.stock
	                                    FROM inventarios
	                                    INNER JOIN almacenes as A on A.id = inventarios.almacen_id
                                        Where producto_id = ?pProductoid AND A.id = ?palmacenid";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);
                            myCmd.Parameters.AddWithValue("?pProductoid", producto);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                InventariosEntity inventariosEntity = new InventariosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    costo = dataReader["costo"].ToString(),
                                    stock = dataReader["stock"].ToString()
                                };
                                (inventariosResponse.lstInventarios ?? new List<InventariosEntity>()).Add(inventariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(inventariosResponse.codigoError))
                    {
                        if (inventariosResponse.lstInventarios.Count == 0)
                        {
                            inventariosResponse.codigoError = "NEXISTE";
                            inventariosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            inventariosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1." + ex.Message;
            }
            return inventariosResponse;
        }

        public static Inventarios ObtenerInventarioExiste(string producto, string almacen)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"Select inventarios.id, inventarios.costo, inventarios.stock
	                                    FROM inventarios
	                                    INNER JOIN almacenes as A on A.id = inventarios.almacen_id
                                        Where producto_id = ?pProductoid AND es_sincronizado = 1 AND A.id = ?palmacenid";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);
                            myCmd.Parameters.AddWithValue("?pProductoid", producto);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                InventariosEntity inventariosEntity = new InventariosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    costo = dataReader["costo"].ToString(),
                                    stock = dataReader["stock"].ToString()
                                };
                                (inventariosResponse.lstInventarios ?? new List<InventariosEntity>()).Add(inventariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(inventariosResponse.codigoError))
                    {
                        if (inventariosResponse.lstInventarios.Count == 0)
                        {
                            inventariosResponse.codigoError = "NEXISTE";
                            inventariosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            inventariosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1." + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ObtenerPortafoliosCategoria(string categoria, string almacen)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"Select
                                        C.categoria,
                                        L.linea,
                                        M.marca,
                                        P.id as idProducto, concat(MP.modelo_producto, ' ', P.tamanio) as Producto,
                                        A.direccion,
                                        I.id as idInvetario,stock,stock_optimo,fav,pvp2
                                        FROM inventarios as I
                                        INNER
                                            JOIN almacenes as A on A.id = almacen_id 
                                            JOIN productos as P on P.id = producto_id
                                            JOIN modelo_productos as MP on MP.id = P.modelo_producto_id 
                                            JOIN modelos as Mo on MP.modelo_id = Mo.id
                                            JOIN marcas as M on Mo.marca_id = M.id
                                            JOIN lineas as L on Mo.linea_id = L.id
                                            JOIN categorias as C on C.id = L.categoria_id
                                            Where (A.id = ?palmacenid and C.id =?pcategoriaid and I.es_sincronizado = 1)
                                        order by idProducto  
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcategoriaid", categoria);
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                InventariosEntity inventariosEntity = new InventariosEntity()
                                {
                                    id = dataReader["idInvetario"].ToString(),
                                    almacen_id = dataReader["direccion"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    Producto = dataReader["Producto"].ToString(),
                                    fav = dataReader["fav"].ToString(),
                                    pvp2 = dataReader["pvp2"].ToString(),
                                    stock = dataReader["stock"].ToString(),
                                    stock_optimo = dataReader["stock_optimo"].ToString()
                                };
                                (inventariosResponse.lstInventarios ?? new List<InventariosEntity>()).Add(inventariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(inventariosResponse.codigoError))
                    {
                        if (inventariosResponse.lstInventarios.Count == 0)
                        {
                            inventariosResponse.codigoError = "E";
                            inventariosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            inventariosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1." + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ObtenerPortafoliosLineas(string almacen, string linea, string categoria)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"Select
                                        C.categoria,
                                        L.linea,
                                        M.marca, 
                                        P.id as idProducto, concat(MP.modelo_producto, ' ', P.tamanio) as Producto,
                                        A.direccion,
                                        I.id as idInvetario,stock,stock_optimo,fav,pvp2
                                        FROM inventarios as I
                                        INNER
                                            JOIN almacenes as A on A.id = almacen_id 
                                            JOIN productos as P on P.id = producto_id
                                            JOIN modelo_productos as MP on MP.id = P.modelo_producto_id 
                                            JOIN modelos as Mo on MP.modelo_id = Mo.id
                                            JOIN lineas as L on Mo.linea_id = L.id
                                            JOIN marcas as M on Mo.marca_id = M.id
                                            JOIN categorias as C on C.id = L.categoria_id
                                            Where (A.id = ?palmacenid and L.linea =?plineaid and C.id = ?pcategoria and I.es_sincronizado = 1)
                                        order by idProducto  
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcategoria", categoria);
                            myCmd.Parameters.AddWithValue("?plineaid", linea);
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                InventariosEntity inventariosEntity = new InventariosEntity()
                                {
                                    id = dataReader["idInvetario"].ToString(),
                                    almacen_id = dataReader["direccion"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    Producto = dataReader["Producto"].ToString(),
                                    pvp2 = dataReader["pvp2"].ToString(),
                                    fav = dataReader["fav"].ToString(),
                                    stock = dataReader["stock"].ToString(),
                                    stock_optimo = dataReader["stock_optimo"].ToString()
                                };
                                (inventariosResponse.lstInventarios ?? new List<InventariosEntity>()).Add(inventariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(inventariosResponse.codigoError))
                    {
                        if (inventariosResponse.lstInventarios.Count == 0)
                        {
                            inventariosResponse.codigoError = "E";
                            inventariosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            inventariosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1." + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ObtenerPortafoliosLineasSugerido(string almacen, string linea, string categoria)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"Select
                                        C.categoria,
                                        L.linea,
                                        M.marca, 
                                        P.id as idProducto, concat(MP.modelo_producto, ' ', P.tamanio) as Producto,
                                        A.direccion,
                                        I.id as idInvetario,almacen_id,stock,stock_optimo,fav,pvp2
                                        FROM inventarios as I
                                        INNER
                                            JOIN almacenes as A on A.id = almacen_id 
                                            JOIN productos as P on P.id = producto_id
                                            JOIN modelo_productos as MP on MP.id = P.modelo_producto_id 
                                            JOIN modelos as Mo on MP.modelo_id = Mo.id
                                            JOIN marcas as M on Mo.marca_id = M.id
                                            JOIN lineas as L on Mo.linea_id = L.id
                                            JOIN categorias as C on C.id = L.categoria_id
                                            Where (A.id = ?palmacenid and 
                                                    L.linea =?plineaid and 
                                                    C.id = ?pcategoria and 
                                                    stock_optimo > stock and M.marca like 'VENUS' and I.es_sincronizado = 1)
                                        order by idProducto  
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcategoria", categoria);
                            myCmd.Parameters.AddWithValue("?plineaid", linea);
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                InventariosEntity inventariosEntity = new InventariosEntity()
                                {
                                    id = dataReader["idInvetario"].ToString(),
                                    almacen_id = dataReader["direccion"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    Producto = dataReader["Producto"].ToString(),
                                    pvp2 = dataReader["pvp2"].ToString(),
                                    fav = dataReader["fav"].ToString(),
                                    stock = dataReader["stock"].ToString(),
                                    stock_optimo = dataReader["stock_optimo"].ToString()
                                };
                                (inventariosResponse.lstInventarios ?? new List<InventariosEntity>()).Add(inventariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(inventariosResponse.codigoError))
                    {
                        if (inventariosResponse.lstInventarios.Count == 0)
                        {
                            inventariosResponse.codigoError = "E";
                            inventariosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            inventariosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1." + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ObtenerPortafoliosModelos(string almacen, string modelo, string linea, string categoria)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"Select
                                        C.categoria,
                                        L.linea,
                                        M.marca, 
                                        P.id as idProducto, concat(MP.modelo_producto, ' ', P.tamanio) as Producto,
                                        A.direccion,
                                        I.id as idInvetario,stock,stock_optimo,fav,pvp2
                                        FROM inventarios as I
                                        INNER
                                            JOIN almacenes as A on A.id = almacen_id 
                                            JOIN productos as P on P.id = producto_id
                                            JOIN modelo_productos as MP on MP.id = P.modelo_producto_id 
                                            JOIN modelos as Mo on MP.modelo_id = Mo.id
                                            JOIN marcas as M on Mo.marca_id = M.id
                                            JOIN lineas as L on Mo.linea_id = L.id
                                            JOIN categorias as C on C.id = L.categoria_id
                                            Where (
                                                    A.id = ?palmacenid and 
                                                    C.id = ?pcategoriaid and
                                                    L.linea like ?lineaid and 
                                                    Mo.modelo like ?pmodeloid and
                                                    I.es_sincronizado = 1
                                                   )
                                        order by idProducto  
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmodeloid", modelo);
                            myCmd.Parameters.AddWithValue("?pcategoriaid", categoria);
                            myCmd.Parameters.AddWithValue("?lineaid", linea);
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                InventariosEntity inventariosEntity = new InventariosEntity()
                                {
                                    id = dataReader["idInvetario"].ToString(),
                                    almacen_id = dataReader["direccion"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    pvp2 = dataReader["pvp2"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    Producto = dataReader["Producto"].ToString(),
                                    fav = dataReader["fav"].ToString(),
                                    stock = dataReader["stock"].ToString(),
                                    stock_optimo = dataReader["stock_optimo"].ToString()
                                };
                                (inventariosResponse.lstInventarios ?? new List<InventariosEntity>()).Add(inventariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(inventariosResponse.codigoError))
                    {
                        if (inventariosResponse.lstInventarios.Count == 0)
                        {
                            inventariosResponse.codigoError = "E";
                            inventariosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            inventariosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1." + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ObtenerPortafoliosModelosColores(string almacen, string modelo, string color, string categoria, string linea)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"Select
                                        C.categoria,
                                        L.linea,
                                        M.marca, 
                                        Cl.color,
                                        MP.color_id,
                                        P.id as idProducto, concat(MP.modelo_producto, ' ', P.tamanio) as Producto,
                                        A.direccion,
                                        I.id as idInvetario,stock,stock_optimo,fav,pvp2
                                        FROM inventarios as I
                                        INNER
                                            JOIN almacenes as A on A.id = almacen_id 
                                            JOIN productos as P on P.id = producto_id
                                            JOIN modelo_productos as MP on MP.id = P.modelo_producto_id 
                                            JOIN modelos as Mo on MP.modelo_id = Mo.id
                                            JOIN lineas as L on Mo.linea_id = L.id
                                            JOIN marcas as M on Mo.marca_id = M.id
                                            JOIN categorias as C on C.id = L.categoria_id
                                            JOIN colors as Cl on Cl.id = MP.color_id
                                            Where 
                                            (A.id = ?palmacenid and 
                                             C.id = ?pcategoriaid and
                                             L.linea like ?lineaid and 
                                             Mo.modelo like ?pmodeloid and
                                             Cl.color like ?pcolorid and
                                             I.es_sincronizado = 1 )
                                        order by idProducto    
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pmodeloid", modelo);
                            myCmd.Parameters.AddWithValue("?pcolorid", color);
                            myCmd.Parameters.AddWithValue("?pcategoriaid", categoria);
                            myCmd.Parameters.AddWithValue("?lineaid", linea);
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);

                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                InventariosEntity inventariosEntity = new InventariosEntity()
                                {
                                    id = dataReader["idInvetario"].ToString(),
                                    almacen_id = dataReader["direccion"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    pvp2 = dataReader["pvp2"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    Producto = dataReader["Producto"].ToString(),
                                    fav = dataReader["fav"].ToString(),
                                    stock = dataReader["stock"].ToString(),
                                    stock_optimo = dataReader["stock_optimo"].ToString()
                                };
                                (inventariosResponse.lstInventarios ?? new List<InventariosEntity>()).Add(inventariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(inventariosResponse.codigoError))
                    {
                        if (inventariosResponse.lstInventarios.Count == 0)
                        {
                            inventariosResponse.codigoError = "E";
                            inventariosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            inventariosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1." + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ObtenerPortafoliosCategoriaSugerido(string categoria, string almacen)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"Select
                                        C.categoria,
                                        L.linea,
                                        M.marca,
                                        P.id as idProducto, concat(MP.modelo_producto, ' ', P.tamanio) as Producto,
                                        A.direccion,
                                        I.id as idInvetario,stock,stock_optimo,fav,pvp2
                                        FROM inventarios as I
                                        INNER
                                            JOIN almacenes as A on A.id = almacen_id 
                                            JOIN productos as P on P.id = producto_id
                                            JOIN modelo_productos as MP on MP.id = P.modelo_producto_id 
                                            JOIN modelos as Mo on MP.modelo_id = Mo.id
                                            JOIN lineas as L on Mo.linea_id = L.id
                                            JOIN marcas as M on Mo.marca_id = M.id
                                            JOIN categorias as C on C.id = L.categoria_id
                                            Where (A.id = ?palmacenid and C.id =?pcategoriaid and 
                                                    stock_optimo > stock and M.marca like 'VENUS' and I.es_sincronizado = 1)
                                        order by idProducto  
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pcategoriaid", categoria);
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                InventariosEntity inventariosEntity = new InventariosEntity()
                                {
                                    id = dataReader["idInvetario"].ToString(),
                                    almacen_id = dataReader["direccion"].ToString(),
                                    categoria = dataReader["categoria"].ToString(),
                                    linea = dataReader["linea"].ToString(),
                                    marca = dataReader["marca"].ToString(),
                                    Producto = dataReader["Producto"].ToString(),
                                    fav = dataReader["fav"].ToString(),
                                    stock = dataReader["stock"].ToString(),
                                    pvp2 = dataReader["pvp2"].ToString(),
                                    stock_optimo = dataReader["stock_optimo"].ToString()
                                };
                                (inventariosResponse.lstInventarios ?? new List<InventariosEntity>()).Add(inventariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(inventariosResponse.codigoError))
                    {
                        if (inventariosResponse.lstInventarios.Count == 0)
                        {
                            inventariosResponse.codigoError = "E";
                            inventariosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            inventariosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1." + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios InsertarInventarios(InventariosEntity inventariosEntity)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO inventarios
                                    (producto_id,almacen_id,etiquetas,stock, es_sincronizado,
                                      fav,pvp1,pvp_sugerido,pvp2,costo,created_at)
                                       VALUES(?pProducto_id,?pAlmacen_id,?pEtiquetas,?pStock,?pEs_sincronizado,?pFav,?pPvp1,?pPvp_sugerido,?pPvp2,?pCosto,
                                              ?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pProducto_id", inventariosEntity.producto_id);
                                    myCmd.Parameters.AddWithValue("?pAlmacen_id", inventariosEntity.almacen_id);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", inventariosEntity.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pStock", inventariosEntity.stock);
                                    myCmd.Parameters.AddWithValue("?pPvp1", inventariosEntity.pvp1);
                                    myCmd.Parameters.AddWithValue("?pPvp_sugerido", inventariosEntity.pvp_sugerido);
                                    myCmd.Parameters.AddWithValue("?pCosto", inventariosEntity.costo);
                                    myCmd.Parameters.AddWithValue("?pPvp2", inventariosEntity.pvp2);
                                    myCmd.Parameters.AddWithValue("?pEs_sincronizado", "1");
                                    myCmd.Parameters.AddWithValue("?pFav", 0);
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        inventariosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        inventariosResponse.codigoError = "E";
                                        inventariosResponse.descripcionError = "3. No se pudo crear el Producto.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    inventariosResponse.codigoError = "E";
                                    inventariosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1. " + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios InsertarInventarioUltimo(InventariosEntity inventariosEntity)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"SELECT id
                                        INTO @id
                                        FROM productos
                                        WHERE modelo_producto_id = ?pModeloProducto
                                        ORDER BY id
                                        DESC LIMIT 1;

                                    INSERT INTO inventarios
                                    (producto_id,almacen_id,etiquetas,stock, es_sincronizado,
                                      fav,pvp1,pvp_sugerido,pvp2,created_at)
                                       VALUES(@id,?pAlmacen_id,?pEtiquetas,?pStock,?pEs_sincronizado,?pFav,?pPvp1,?pPvp_sugerido,?pPvp2,
                                              ?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pAlmacen_id", inventariosEntity.almacen_id);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", inventariosEntity.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pStock", inventariosEntity.stock);
                                    myCmd.Parameters.AddWithValue("?pModeloProducto", inventariosEntity.modelo_producto_id);
                                    myCmd.Parameters.AddWithValue("?pPvp1", inventariosEntity.pvp1);
                                    myCmd.Parameters.AddWithValue("?pPvp_sugerido", inventariosEntity.pvp_sugerido);
                                    myCmd.Parameters.AddWithValue("?pPvp2", inventariosEntity.pvp2);
                                    myCmd.Parameters.AddWithValue("?pEs_sincronizado", "1");
                                    myCmd.Parameters.AddWithValue("?pFav", 0);
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        inventariosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        inventariosResponse.codigoError = "E";
                                        inventariosResponse.descripcionError = "3. No se pudo crear el Producto.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    inventariosResponse.codigoError = "E";
                                    inventariosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1. " + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ModificarInventarios(InventariosEntity inventarios)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE inventarios 
                                      SET producto_id = ?pProducto_id,
                                          almacen_id = ?pAlmacen_id,
                                          etiquetas = ?pEtiquetas,
                                          stock = ?pStock,
                                          fav = ?pFav,
                                          pvp2 = ?pPvp2,
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
                                    myCmd.Parameters.AddWithValue("?pProducto_id", inventarios.producto_id);
                                    myCmd.Parameters.AddWithValue("?pAlmacen_id", inventarios.almacen_id);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", inventarios.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pStock", inventarios.stock);
                                    myCmd.Parameters.AddWithValue("?pFav", 0);
                                    myCmd.Parameters.AddWithValue("?pPvp2", inventarios.pvp2);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", inventarios.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        inventariosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        inventariosResponse.codigoError = "E";
                                        inventariosResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    inventariosResponse.codigoError = "E";
                                    inventariosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1. " + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ActualizarCosto(InventariosEntity inventarios)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE inventarios 
                                      SET costo = ?pCosto,
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
                                    myCmd.Parameters.AddWithValue("?pCosto", inventarios.costo);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", inventarios.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        inventariosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        inventariosResponse.codigoError = "E";
                                        inventariosResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    inventariosResponse.codigoError = "E";
                                    inventariosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1. " + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ActualizarInventarios(InventariosEntity inventarios)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE inventarios 
                                      SET producto_id = ?pProducto_id,
                                          almacen_id = ?pAlmacen_id,
                                          etiquetas = ?pEtiquetas,
                                          stock = ?pStock,
                                          costo =?pCosto,
                                          pvp_sugerido =?pPvp_sugerido,
                                          pvp1 = ?pPvp1,
                                          pvp2 = ?pPvp2,
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
                                    myCmd.Parameters.AddWithValue("?pProducto_id", inventarios.producto_id);
                                    myCmd.Parameters.AddWithValue("?pAlmacen_id", inventarios.almacen_id);
                                    myCmd.Parameters.AddWithValue("?pEtiquetas", inventarios.etiquetas);
                                    myCmd.Parameters.AddWithValue("?pStock", inventarios.stock);
                                    myCmd.Parameters.AddWithValue("?pFav", 0);
                                    myCmd.Parameters.AddWithValue("?pPvp1", inventarios.pvp2);
                                    myCmd.Parameters.AddWithValue("?pPvp2", inventarios.pvp2);
                                    myCmd.Parameters.AddWithValue("?pPvp_sugerido", inventarios.pvp_sugerido);
                                    myCmd.Parameters.AddWithValue("?pCosto", inventarios.costo);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", inventarios.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        inventariosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        inventariosResponse.codigoError = "E";
                                        inventariosResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    inventariosResponse.codigoError = "E";
                                    inventariosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1. " + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ActualizarInventarioEx(InventariosEntity inventarios)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE inventarios 
                                      SET stock = ?pStock,
                                          updated_at=?pUpdatedAt,
                                          es_sincronizado=?pEs_sincronizado
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
                                    myCmd.Parameters.AddWithValue("?pStock", inventarios.stock);
                                    myCmd.Parameters.AddWithValue("?pEs_sincronizado", '1');
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", inventarios.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        inventariosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        inventariosResponse.codigoError = "E";
                                        inventariosResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    inventariosResponse.codigoError = "E";
                                    inventariosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1. " + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ActualizarSinc(InventariosEntity inventarios)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE inventarios
                                      SET es_sincronizado=?pEs_sincronizado
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
                                    myCmd.Parameters.AddWithValue("?pEs_sincronizado", '0');
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", inventarios.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        inventariosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        inventariosResponse.codigoError = "E";
                                        inventariosResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    inventariosResponse.codigoError = "E";
                                    inventariosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1. " + ex.Message;
            }
            return inventariosResponse;
        }

        public static Inventarios EliminarInventarios(InventariosEntity inventarios)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE FROM inventarios WHERE id=?pId";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", inventarios.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        inventariosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        inventariosResponse.codigoError = "E";
                                        inventariosResponse.descripcionError = "3. No se pudo eliminar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    inventariosResponse.codigoError = "E";
                                    inventariosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1. " + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios DeshabilitarInventarios(InventariosEntity inventarios)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE inventarios
                                        SET es_sincronizado = 0,
                                        stock = 0
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
                                    myCmd.Parameters.AddWithValue("?pId", inventarios.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        inventariosResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        inventariosResponse.codigoError = "E";
                                        inventariosResponse.descripcionError = "3. No se pudo eliminar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    inventariosResponse.codigoError = "E";
                                    inventariosResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1. " + ex.Message;
            }
            return inventariosResponse;
        }
        
        public static Inventarios ObtenerPortafoliosInventarios(string producto, string almacen)
        {
            Inventarios inventariosResponse = new Inventarios();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT * 
                                        FROM inventarios
                                        WHERE (producto_id = ?pProductoid and almacen_id = ?palmacenid);
                                        ";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pProductoid", producto);
                            myCmd.Parameters.AddWithValue("?palmacenid", almacen);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                InventariosEntity inventariosEntity = new InventariosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    almacen_id = dataReader["almacen_id"].ToString(),
                                    producto_id = dataReader["producto_id"].ToString(),
                                    etiquetas = dataReader["etiquetas"].ToString(),
                                    fav = dataReader["fav"].ToString(),
                                    stock = dataReader["stock"].ToString(),
                                    stock_optimo = dataReader["stock_optimo"].ToString(),
                                    costo = dataReader["costo"].ToString(),
                                    pvp1 = dataReader["pvp1"].ToString(),
                                    pvp2 = dataReader["pvp2"].ToString(),
                                    pvp_sugerido = dataReader["pvp_sugerido"].ToString(),
                                    es_sincronizado = dataReader["es_sincronizado"].ToString(),
                                    cod_principal = dataReader["cod_principal"].ToString(),
                                    cod_secundario = dataReader["cod_secundario"].ToString()

                                };
                                (inventariosResponse.lstInventarios ?? new List<InventariosEntity>()).Add(inventariosEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        inventariosResponse.codigoError = "E";
                        inventariosResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(inventariosResponse.codigoError))
                    {
                        if (inventariosResponse.lstInventarios.Count == 0)
                        {
                            inventariosResponse.codigoError = "E";
                            inventariosResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            inventariosResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                inventariosResponse.codigoError = "E";
                inventariosResponse.descripcionError = "1." + ex.Message;
            }
            return inventariosResponse;
        }
    }
}