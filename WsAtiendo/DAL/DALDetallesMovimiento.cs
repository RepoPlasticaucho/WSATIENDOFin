using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using WsAtiendo.Models;

namespace WsAtiendo.DAL
{
    public class DALDetallesMovimiento
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000; Allow User Variables=true;";


        public static DetalleMovimientos ObtenerDetalleMovimiento(string movimiento_id)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT D.*,
                                             P.tamanio,P.nombre,P.unidad_medida,
                                             MP.modelo_producto,MP.url_image,
                                             T.descripcion,
                                             C.color
                                        from detalles_movimientos as D
                                        INNER JOIN productos as P on P.id = D.producto_id
                                        INNER JOIN tarifa_ice_iva as T on T.id = P.tarifa_ice_iva_id
                                        INNER JOIN modelo_productos as MP on MP.id = P.modelo_producto_id
                                        INNER JOIN colors as C on C.id = MP.color_id
                                        where movimiento_id = ?pMovimiento_id";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pMovimiento_id", movimiento_id);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetalleMovimientosEntity detalleEntity = new DetalleMovimientosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    producto_id = dataReader["producto_id"].ToString(),
                                    producto_nombre = dataReader["nombre"].ToString(),
                                    movimiento_id = dataReader["movimiento_id"].ToString(),
                                    cantidad = dataReader["cantidad"].ToString(),
                                    costo = dataReader["costo"].ToString(),
                                    tarifa = dataReader["descripcion"].ToString(),
                                    precio = dataReader["precio"].ToString(),
                                    desc_add = dataReader["desc_add"].ToString(),
                                    unidad_medida = dataReader["unidad_medida"].ToString(),
                                    tamanio = dataReader["tamanio"].ToString(),
                                    color = dataReader["color"].ToString(),
                                    modelo_producto_nombre = dataReader["modelo_producto"].ToString(),
                                    url_image = dataReader["url_image"].ToString(),
                                };
                                (detalleResponse.lstDetalleMovimientos ?? new List<DetalleMovimientosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(detalleResponse.codigoError))
                    {
                        if (detalleResponse.lstDetalleMovimientos.Count == 0)
                        {
                            detalleResponse.codigoError = "E";
                            detalleResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            detalleResponse.codigoError = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1." + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos ObtenerDetalleMovimientoEx(string movimiento_id, string producto_id)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct *
                                        FROM detalles_movimientos as DM
                                        WHERE DM.movimiento_id = ?pMovimiento_id AND DM.producto_id = ?pProducto_id
                                        GROUP BY DM.producto_id";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pMovimiento_id", movimiento_id);
                            myCmd.Parameters.AddWithValue("?pProducto_id", producto_id);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetalleMovimientosEntity detalleEntity = new DetalleMovimientosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    producto_id = dataReader["producto_id"].ToString(),
                                    cantidad = dataReader["cantidad"].ToString(),
                                    movimiento_id = dataReader["movimiento_id"].ToString(),
                                };
                                (detalleResponse.lstDetalleMovimientos ?? new List<DetalleMovimientosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(detalleResponse.codigoError))
                    {
                        if (detalleResponse.lstDetalleMovimientos.Count == 0)
                        {
                            detalleResponse.codigoError = "E";
                            detalleResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            detalleResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1." + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos ObtenerUltDetalleMovimiento(string movimiento_id)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT D.*,
                                        P.tamanio, P.nombre, P.unidad_medida,
                                        MP.modelo_producto, MP.url_image,
                                        T.descripcion,T.codigo,
                                        C.color
                                    FROM detalles_movimientos AS D
                                    INNER JOIN productos AS P ON P.id = D.producto_id
                                    INNER JOIN tarifa_ice_iva AS T ON T.id = P.tarifa_ice_iva_id
                                    INNER JOIN modelo_productos AS MP ON MP.id = P.modelo_producto_id
                                    INNER JOIN colors AS C ON C.id = MP.color_id
                                    WHERE movimiento_id = ?pMovimiento_id
                                        AND D.id = (
                                            SELECT MAX(id)
                                            FROM detalles_movimientos
                                            WHERE movimiento_id = ?pMovimiento_id
                                        );";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pMovimiento_id", movimiento_id);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetalleMovimientosEntity detalleEntity = new DetalleMovimientosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    producto_id = dataReader["producto_id"].ToString(),
                                    producto_nombre = dataReader["nombre"].ToString(),
                                    movimiento_id = dataReader["movimiento_id"].ToString(),
                                    cantidad = dataReader["cantidad"].ToString(),
                                    costo = dataReader["costo"].ToString(),
                                    tarifa = dataReader["descripcion"].ToString(),
                                    cod_tarifa = dataReader["codigo"].ToString(),
                                    precio = dataReader["precio"].ToString(),
                                    desc_add = dataReader["desc_add"].ToString(),
                                    unidad_medida = dataReader["unidad_medida"].ToString(),
                                    tamanio = dataReader["tamanio"].ToString(),
                                    color = dataReader["color"].ToString(),
                                    modelo_producto_nombre = dataReader["modelo_producto"].ToString(),
                                    url_image = dataReader["url_image"].ToString(),
                                };
                                (detalleResponse.lstDetalleMovimientos ?? new List<DetalleMovimientosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(detalleResponse.codigoError))
                    {
                        if (detalleResponse.lstDetalleMovimientos.Count == 0)
                        {
                            detalleResponse.codigoError = "E";
                            detalleResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            detalleResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1." + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos ObtenerDetalleMovimientoSociedad(string sociedad)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT D.id, P.nombre, D.created_at, A.pto_emision, T.descripcion, D.cantidad, D.precio
                                        from detalles_movimientos as D
                                        INNER JOIN productos as P on P.id = D.producto_id
                                        INNER JOIN movimientos as M on M.id = D.movimiento_id
                                        INNER JOIN tipo_movimientos as T on T.id = M.tipo_id
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        INNER JOIN sociedades as S on S.id = A.sociedad_id
                                        WHERE S.id = ?pSociedad_id AND M.es_sincronizado = 1
                                        ORDER BY created_at DESC";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pSociedad_id", sociedad);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetalleMovimientosEntity detalleEntity = new DetalleMovimientosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    producto_nombre = dataReader["nombre"].ToString(),
                                    precio = dataReader["precio"].ToString(),
                                    created_at = dataReader["created_at"].ToString(),
                                    cantidad = dataReader["cantidad"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString(),
                                    tipo_movimiento = dataReader["descripcion"].ToString(),
                                };
                                (detalleResponse.lstDetalleMovimientos ?? new List<DetalleMovimientosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(detalleResponse.codigoError))
                    {
                        if (detalleResponse.lstDetalleMovimientos.Count == 0)
                        {
                            detalleResponse.codigoError = "E";
                            detalleResponse.descripcionError = "3. No se encontraron datos";
                        }
                        else
                        {
                            detalleResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1." + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos ObtenerDetalleMovimientoAlm(string almacen)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT D.id, P.nombre, D.created_at, A.pto_emision, T.descripcion, D.cantidad, D.precio
                                        from detalles_movimientos as D
                                        INNER JOIN productos as P on P.id = D.producto_id
                                        INNER JOIN movimientos as M on M.id = D.movimiento_id
                                        INNER JOIN tipo_movimientos as T on T.id = M.tipo_id
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        WHERE A.nombre_almacen = ?pAlmacen_nombre AND M.es_sincronizado = 1
                                        ORDER BY created_at DESC";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pAlmacen_nombre", almacen);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetalleMovimientosEntity detalleEntity = new DetalleMovimientosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    producto_nombre = dataReader["nombre"].ToString(),
                                    precio = dataReader["precio"].ToString(),
                                    created_at = dataReader["created_at"].ToString(),
                                    cantidad = dataReader["cantidad"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString(),
                                    tipo_movimiento = dataReader["descripcion"].ToString(),
                                };
                                (detalleResponse.lstDetalleMovimientos ?? new List<DetalleMovimientosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(detalleResponse.codigoError))
                    {
                        if (detalleResponse.lstDetalleMovimientos.Count == 0)
                        {
                            detalleResponse.codigoError = "E";
                            detalleResponse.descripcionError = "3. No se encontraron datos";
                        }
                        else
                        {
                            detalleResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1." + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos ObtenerDetalleMovimientoAlmF(string almacen, string fechaDesde, string fechaHasta)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT D.id, P.nombre, D.created_at, A.pto_emision, T.descripcion, D.cantidad, D.precio
                                        from detalles_movimientos as D
                                        INNER JOIN productos as P on P.id = D.producto_id
                                        INNER JOIN movimientos as M on M.id = D.movimiento_id
                                        INNER JOIN tipo_movimientos as T on T.id = M.tipo_id
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        WHERE A.nombre_almacen = ?pAlmacen AND M.es_sincronizado = 1 AND (D.created_at BETWEEN STR_TO_DATE(?pFechadesde, '%Y-%m-%d') AND STR_TO_DATE(CONCAT(?pFechahasta, '23:59:59'), '%Y-%m-%d %H:%i:%s'))
                                        ORDER BY created_at DESC";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pAlmacen", almacen);
                            myCmd.Parameters.AddWithValue("?pFechahasta", fechaHasta);
                            myCmd.Parameters.AddWithValue("?pFechadesde", fechaDesde);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetalleMovimientosEntity detalleEntity = new DetalleMovimientosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    producto_nombre = dataReader["nombre"].ToString(),
                                    precio = dataReader["precio"].ToString(),
                                    created_at = dataReader["created_at"].ToString(),
                                    cantidad = dataReader["cantidad"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString(),
                                    tipo_movimiento = dataReader["descripcion"].ToString(),
                                };
                                (detalleResponse.lstDetalleMovimientos ?? new List<DetalleMovimientosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(detalleResponse.codigoError))
                    {
                        if (detalleResponse.lstDetalleMovimientos.Count == 0)
                        {
                            detalleResponse.codigoError = "E";
                            detalleResponse.descripcionError = "3. No se encontraron datos";
                        }
                        else
                        {
                            detalleResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1." + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos ObtenerDetalleMovimientoAlmFTipo(string almacen, string fechaDesde, string fechaHasta, string tipo)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT D.id, P.nombre, D.created_at, A.pto_emision, T.descripcion, D.cantidad, D.precio
                                        from detalles_movimientos as D
                                        INNER JOIN productos as P on P.id = D.producto_id
                                        INNER JOIN movimientos as M on M.id = D.movimiento_id
                                        INNER JOIN tipo_movimientos as T on T.id = M.tipo_id
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        WHERE (A.nombre_almacen = ?pAlmacen AND M.es_sincronizado = 1 AND (D.created_at BETWEEN STR_TO_DATE(?pFechadesde, '%Y-%m-%d') AND (STR_TO_DATE(CONCAT(?pFechahasta, '23:59:59'), '%Y-%m-%d %H:%i:%s'))) AND T.descripcion = ?pTipo)
                                        ORDER BY created_at DESC";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pAlmacen", almacen);
                            myCmd.Parameters.AddWithValue("?pFechahasta", fechaHasta);
                            myCmd.Parameters.AddWithValue("?pFechadesde", fechaDesde);
                            myCmd.Parameters.AddWithValue("?pTipo", tipo);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetalleMovimientosEntity detalleEntity = new DetalleMovimientosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    producto_nombre = dataReader["nombre"].ToString(),
                                    precio = dataReader["precio"].ToString(),
                                    created_at = dataReader["created_at"].ToString(),
                                    cantidad = dataReader["cantidad"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString(),
                                    tipo_movimiento = dataReader["descripcion"].ToString(),
                                };
                                (detalleResponse.lstDetalleMovimientos ?? new List<DetalleMovimientosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(detalleResponse.codigoError))
                    {
                        if (detalleResponse.lstDetalleMovimientos.Count == 0)
                        {
                            detalleResponse.codigoError = "E";
                            detalleResponse.descripcionError = "3. No se encontraron datos";
                        }
                        else
                        {
                            detalleResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1." + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos ObtenerDetalleMovimientoAlmTipo(string almacen, string tipo)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT D.id, P.nombre, D.created_at, A.pto_emision, T.descripcion, D.cantidad, D.precio
                                        from detalles_movimientos as D
                                        INNER JOIN productos as P on P.id = D.producto_id
                                        INNER JOIN movimientos as M on M.id = D.movimiento_id
                                        INNER JOIN tipo_movimientos as T on T.id = M.tipo_id
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        WHERE A.nombre_almacen = ?pAlmacen AND M.es_sincronizado = 1 AND T.descripcion = ?pTipo
                                        ORDER BY created_at DESC";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pAlmacen", almacen);
                            myCmd.Parameters.AddWithValue("?pTipo", tipo);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetalleMovimientosEntity detalleEntity = new DetalleMovimientosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    producto_nombre = dataReader["nombre"].ToString(),
                                    precio = dataReader["precio"].ToString(),
                                    created_at = dataReader["created_at"].ToString(),
                                    cantidad = dataReader["cantidad"].ToString(),
                                    pto_emision = dataReader["pto_emision"].ToString(),
                                    tipo_movimiento = dataReader["descripcion"].ToString(),
                                };
                                (detalleResponse.lstDetalleMovimientos ?? new List<DetalleMovimientosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(detalleResponse.codigoError))
                    {
                        if (detalleResponse.lstDetalleMovimientos.Count == 0)
                        {
                            detalleResponse.codigoError = "E";
                            detalleResponse.descripcionError = "3. No se encontraron datos";
                        }
                        else
                        {
                            detalleResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1." + ex.Message;
            }
            return detalleResponse;
        }


        public static DetalleMovimientos InsertarDetallePedido(DetalleMovimientosEntity detalleEntity)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO detalles_movimientos
                                    (producto_id,inventario_id,movimiento_id,cantidad,costo,precio,
                                      created_at)
                                       SELECT ?pProducto_id, ?pInventario_id, ?pMovimiento_id, ?pCantidad, ?pCosto, ?pPrecio, ?pCreatedAt
                                        FROM inventarios
                                        WHERE id = ?pInventario_id AND ?pCantidad <= inventarios.stock;
                                    
                                       UPDATE inventarios
                                        SET stock = stock - ?pCantidad
                                        WHERE id = ?pInventario_id AND ?pCantidad <= stock;";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pProducto_id", detalleEntity.producto_id);
                                    myCmd.Parameters.AddWithValue("?pInventario_id", detalleEntity.inventario_id);
                                    myCmd.Parameters.AddWithValue("?pMovimiento_id", detalleEntity.movimiento_id);
                                    myCmd.Parameters.AddWithValue("?pCantidad", detalleEntity.cantidad);
                                    myCmd.Parameters.AddWithValue("?pCosto", detalleEntity.costo);
                                    myCmd.Parameters.AddWithValue("?pPrecio", detalleEntity.precio);
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        detalleResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        detalleResponse.codigoError = "E";
                                        detalleResponse.descripcionError = "3. No se pudo crear el Producto.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    detalleResponse.codigoError = "E";
                                    detalleResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1. " + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos InsertarDetalleCompras(DetalleMovimientosEntity detalleEntity)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO detalles_movimientos
                                    (producto_id,inventario_id,movimiento_id,cantidad,costo,precio,
                                      created_at)
                                       VALUES(?pProducto_id,?pInventario_id,?pMovimiento_id,?pCantidad,?pCosto,?pPrecio,?pCreatedAt);
                                    
                                       UPDATE inventarios
                                        SET stock = stock + ?pCantidad
                                        WHERE id = ?pInventario_id";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pProducto_id", detalleEntity.producto_id);
                                    myCmd.Parameters.AddWithValue("?pInventario_id", detalleEntity.inventario_id);
                                    myCmd.Parameters.AddWithValue("?pMovimiento_id", detalleEntity.movimiento_id);
                                    myCmd.Parameters.AddWithValue("?pCantidad", detalleEntity.cantidad);
                                    myCmd.Parameters.AddWithValue("?pCosto", detalleEntity.costo);
                                    myCmd.Parameters.AddWithValue("?pPrecio", detalleEntity.precio);
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        detalleResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        detalleResponse.codigoError = "E";
                                        detalleResponse.descripcionError = "3. No se pudo crear el Producto.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    detalleResponse.codigoError = "E";
                                    detalleResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1. " + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos InsertarDetalleCompra(DetalleMovimientosEntity detalleEntity)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO detalles_movimientos
                                    (producto_id,inventario_id,movimiento_id,cantidad,costo,precio,
                                      created_at)
                                       VALUES(?pProducto_id,?pInventario_id,?pMovimiento_id,?pCantidad,?pCosto,?pPrecio,?pCreatedAt);";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pProducto_id", detalleEntity.producto_id);
                                    myCmd.Parameters.AddWithValue("?pInventario_id", detalleEntity.inventario_id);
                                    myCmd.Parameters.AddWithValue("?pMovimiento_id", detalleEntity.movimiento_id);
                                    myCmd.Parameters.AddWithValue("?pCantidad", detalleEntity.cantidad);
                                    myCmd.Parameters.AddWithValue("?pCosto", detalleEntity.costo);
                                    myCmd.Parameters.AddWithValue("?pPrecio", detalleEntity.precio);
                                    myCmd.Parameters.AddWithValue("?pCreatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        detalleResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        detalleResponse.codigoError = "E";
                                        detalleResponse.descripcionError = "3. No se pudo crear el Producto.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    detalleResponse.codigoError = "E";
                                    detalleResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1. " + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos ModificarDetallePedidoVenta(DetalleMovimientosEntity detalle)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"SELECT id into @detid
                                        FROM detalles_movimientos
                                        WHERE inventario_id = ?pInventario and movimiento_id = ?pMovimiento;

                                    SELECT cantidad, inventario_id
                                        INTO @cantidad_anterior, @inventario
                                        FROM detalles_movimientos
                                        WHERE id = @detid;

                                    SELECT stock INTO @stock
                                    FROM inventarios
                                    WHERE id = ?pInventario;

                                    UPDATE detalles_movimientos 
                                      SET cantidad=?pCantidad,
                                          updated_at=?pUpdatedAt
                                      WHERE id=@detid AND cantidad <= @stock;

                                    UPDATE inventarios
                                    SET stock = stock + @cantidad_anterior - ?pCantidad
                                    WHERE id = @inventario AND (?pCantidad <= (stock + @cantidad_anterior));";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pCantidad", detalle.cantidad);
                                    myCmd.Parameters.AddWithValue("?pInventario", detalle.inventario_id);
                                    myCmd.Parameters.AddWithValue("?pMovimiento", detalle.movimiento_id);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        detalleResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        detalleResponse.codigoError = "E";
                                        detalleResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    detalleResponse.codigoError = "E";
                                    detalleResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1. " + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos ModificarDetallePedido(DetalleMovimientosEntity detalle)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"SELECT cantidad, inventario_id
                                        INTO @cantidad_anterior, @inventario
                                        FROM detalles_movimientos
                                        WHERE id = ?pId;

                                        SELECT stock INTO @stock
                                        FROM inventarios
                                        WHERE id = @inventario;

                                    UPDATE detalles_movimientos 
                                      SET cantidad=?pCantidad,
                                          costo=?pCosto,
                                          desc_add=?pDesc_add,
                                          precio=?pPrecio,
                                          updated_at=?pUpdatedAt
                                      WHERE id=?pId AND cantidad <= @stock;

                                    UPDATE detalle_impuesto
                                    SET valor=?pCosto,
                                        updated_at=?pUpdatedAt
                                    WHERE detalle_movimiento_id=?pId;

                                    UPDATE inventarios
                                    SET stock = stock + @cantidad_anterior - ?pCantidad
                                    WHERE id = @inventario AND (?pCantidad <= (stock + @cantidad_anterior));";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pCantidad", detalle.cantidad);
                                    myCmd.Parameters.AddWithValue("?pCosto", detalle.costo);
                                    myCmd.Parameters.AddWithValue("?pPrecio", detalle.precio);
                                    myCmd.Parameters.AddWithValue("?pDesc_add", detalle.desc_add);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", detalle.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        detalleResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        detalleResponse.codigoError = "E";
                                        detalleResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    detalleResponse.codigoError = "E";
                                    detalleResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1. " + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos ModificarDetalleCompra(DetalleMovimientosEntity detalle)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"SELECT cantidad, inventario_id
                                        INTO @cantidad_anterior, @inventario
                                        FROM detalles_movimientos
                                        WHERE id = ?pId;

                                    UPDATE detalles_movimientos 
                                      SET cantidad=?pCantidad,
                                          costo=?pCosto,
                                          precio=?pPrecio,
                                          updated_at=?pUpdatedAt
                                      WHERE id=?pId;

                                    UPDATE inventarios
                                    SET stock = stock - @cantidad_anterior + ?pCantidad
                                    WHERE id = @inventario;";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pCantidad", detalle.cantidad);
                                    myCmd.Parameters.AddWithValue("?pCosto", detalle.costo);
                                    myCmd.Parameters.AddWithValue("?pPrecio", detalle.precio);
                                    myCmd.Parameters.AddWithValue("?pUpdatedAt", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pId", detalle.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        detalleResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        detalleResponse.codigoError = "E";
                                        detalleResponse.descripcionError = "3. No se pudo actualizar el Almacen.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    detalleResponse.codigoError = "E";
                                    detalleResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1. " + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos EliminarDetalleMovimiento(DetalleMovimientosEntity detalle)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"SELECT cantidad, inventario_id
                                        INTO @cantidad, @inventario
                                        FROM detalles_movimientos
                                        WHERE id = ?pId;

                                        DELETE
                                        FROM detalles_movimientos
                                        WHERE id=?pId;

                                        DELETE
                                        FROM detalle_impuesto
                                        WHERE detalle_movimiento_id=?pId;

                                        UPDATE inventarios
                                        SET stock = stock + @cantidad
                                        WHERE id = @inventario;";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", detalle.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        detalleResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        detalleResponse.codigoError = "E";
                                        detalleResponse.descripcionError = "3. No se pudo eliminar el detalle.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    detalleResponse.codigoError = "E";
                                    detalleResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1. " + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos EliminarDetalleMovimientoVenta(DetalleMovimientosEntity detalle)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"SELECT id into @detid
                                        FROM detalles_movimientos
                                        WHERE inventario_id = ?pInventario and movimiento_id = ?pMovimiento;

                                    SELECT cantidad, inventario_id
                                        INTO @cantidad, @inventario
                                        FROM detalles_movimientos
                                        WHERE id = @detid;

                                        DELETE
                                        FROM detalles_movimientos
                                        WHERE id=@detid;

                                        DELETE
                                        FROM detalle_impuesto
                                        WHERE detalle_movimiento_id=@detid;

                                        UPDATE inventarios
                                        SET stock = stock + @cantidad
                                        WHERE id = @inventario;";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pInventario", detalle.inventario_id);
                                    myCmd.Parameters.AddWithValue("?pMovimiento", detalle.movimiento_id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        detalleResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        detalleResponse.codigoError = "E";
                                        detalleResponse.descripcionError = "3. No se pudo eliminar el detalle.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    detalleResponse.codigoError = "E";
                                    detalleResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1. " + ex.Message;
            }
            return detalleResponse;
        }

        public static DetalleMovimientos EliminarDetalleCompra(DetalleMovimientosEntity detalle)
        {
            DetalleMovimientos detalleResponse = new DetalleMovimientos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"SELECT cantidad, inventario_id
                                        INTO @cantidad, @inventario
                                        FROM detalles_movimientos
                                        WHERE id = ?pId;

                                        DELETE
                                        FROM detalles_movimientos
                                        WHERE id=?pId;

                                        UPDATE inventarios
                                        SET stock = stock - @cantidad
                                        WHERE id = @inventario;";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pId", detalle.id);
                                    myCmd.CommandType = CommandType.Text;
                                    var rowCount = myCmd.ExecuteNonQuery();
                                    if (rowCount > 0)
                                    {
                                        trans.Commit();
                                        detalleResponse.codigoError = "OK";
                                    }
                                    else
                                    {
                                        trans.Rollback();
                                        detalleResponse.codigoError = "E";
                                        detalleResponse.descripcionError = "3. No se pudo eliminar el detalle.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    detalleResponse.codigoError = "E";
                                    detalleResponse.descripcionError = "2. " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        detalleResponse.codigoError = "E";
                        detalleResponse.descripcionError = "2. " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                detalleResponse.codigoError = "E";
                detalleResponse.descripcionError = "1. " + ex.Message;
            }
            return detalleResponse;
        }
    }
}