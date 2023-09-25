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
    public class DALDetallesPagos
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000;AllowZeroDateTime=True;";

        public static DetallePagos ObtenerDetallePagoMovimiento(string movimiento)
        {
            DetallePagos pagoResponse = new DetallePagos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT FP.nombre, DP.valor
                                        FROM detalle_pago AS DP
                                        INNER JOIN formas_pago as FP on FP.id = DP.forma_pago_id
                                        INNER JOIN movimientos as M on M.id = DP.movimiento_id
                                        WHERE DP.movimiento_id = ?pMovimiento AND M.es_sincronizado = 1
                                        GROUP BY FP.nombre";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pMovimiento", movimiento);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetallePagosEntity detalleEntity = new DetallePagosEntity()
                                {
                                    valor = dataReader["valor"].ToString(),
                                    nombre = dataReader["nombre"].ToString()
                                };
                                (pagoResponse.lstDetallePagos ?? new List<DetallePagosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        pagoResponse.codigoError = "E";
                        pagoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(pagoResponse.codigoError))
                    {
                        if (pagoResponse.lstDetallePagos.Count == 0)
                        {
                            pagoResponse.codigoError = "E";
                            pagoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            pagoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                pagoResponse.codigoError = "E";
                pagoResponse.descripcionError = "1." + ex.Message;
            }
            return pagoResponse;
        }

        public static DetallePagos ObtenerDetallePagoMov(string movimiento)
        {
            DetallePagos pagoResponse = new DetallePagos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT distinct DP.id, FP.nombre, DP.valor
                                        FROM detalle_pago AS DP
                                        INNER JOIN formas_pago as FP on FP.id = DP.forma_pago_id
                                        INNER JOIN movimientos as M on M.id = DP.movimiento_id
                                        WHERE DP.movimiento_id = ?pMovimiento
                                        GROUP BY FP.nombre";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pMovimiento", movimiento);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetallePagosEntity detalleEntity = new DetallePagosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    valor = dataReader["valor"].ToString(),
                                    nombre = dataReader["nombre"].ToString()
                                };
                                (pagoResponse.lstDetallePagos ?? new List<DetallePagosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        pagoResponse.codigoError = "E";
                        pagoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(pagoResponse.codigoError))
                    {
                        if (pagoResponse.lstDetallePagos.Count == 0)
                        {
                            pagoResponse.codigoError = "E";
                            pagoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            pagoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                pagoResponse.codigoError = "E";
                pagoResponse.descripcionError = "1." + ex.Message;
            }
            return pagoResponse;
        }

        public static DetallePagos ObtenerDetallePagoE(string sociedad)
        {
            DetallePagos pagoResponse = new DetallePagos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT sum(D.valor) as valor
                                        FROM detalle_pago as D
                                        INNER JOIN movimientos as M on M.id = D.movimiento_id
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        WHERE A.sociedad_id = ?pSociedad AND D.forma_pago_id = 1 AND M.es_sincronizado = 1";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pSociedad", sociedad);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetallePagosEntity detalleEntity = new DetallePagosEntity()
                                {
                                    valorE = dataReader["valor"].ToString(),
                                };
                                (pagoResponse.lstDetallePagos ?? new List<DetallePagosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        pagoResponse.codigoError = "E";
                        pagoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(pagoResponse.codigoError))
                    {
                        if (pagoResponse.lstDetallePagos.Count == 0)
                        {
                            pagoResponse.codigoError = "E";
                            pagoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            pagoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                pagoResponse.codigoError = "E";
                pagoResponse.descripcionError = "1." + ex.Message;
            }
            return pagoResponse;
        }

        public static DetallePagos ModificarDetallePago(DetallePagosEntity detalle)
        {
            DetallePagos detalleResponse = new DetallePagos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE detalle_pago
                                    SET valor=?pValor,
                                        updated_at=?pUpdatedAt
                                    WHERE id=?pId;";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pValor", detalle.valor);
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
                                        detalleResponse.descripcionError = "3. No se pudo actualizar el pago.";
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

        public static DetallePagos EliminarDetallePago(DetallePagosEntity detalle)
        {
            DetallePagos detalleResponse = new DetallePagos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"DELETE
                                        FROM detalle_pago
                                        WHERE id=?pId;";
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

        public static DetallePagos ObtenerDetallePagoTD(string sociedad)
        {
            DetallePagos pagoResponse = new DetallePagos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT sum(D.valor) as valor
                                        FROM detalle_pago as D
                                        INNER JOIN movimientos as M on M.id = D.movimiento_id
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        WHERE A.sociedad_id = ?pSociedad AND D.forma_pago_id = 2 AND M.es_sincronizado = 1";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pSociedad", sociedad);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetallePagosEntity detalleEntity = new DetallePagosEntity()
                                {
                                    valorTD = dataReader["valor"].ToString(),
                                };
                                (pagoResponse.lstDetallePagos ?? new List<DetallePagosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        pagoResponse.codigoError = "E";
                        pagoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(pagoResponse.codigoError))
                    {
                        if (pagoResponse.lstDetallePagos.Count == 0)
                        {
                            pagoResponse.codigoError = "E";
                            pagoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            pagoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                pagoResponse.codigoError = "E";
                pagoResponse.descripcionError = "1." + ex.Message;
            }
            return pagoResponse;
        }

        public static DetallePagos ObtenerDetallePagoTC(string sociedad)
        {
            DetallePagos pagoResponse = new DetallePagos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT sum(D.valor) as valor
                                        FROM detalle_pago as D
                                        INNER JOIN movimientos as M on M.id = D.movimiento_id
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        WHERE A.sociedad_id = ?pSociedad AND D.forma_pago_id = 3 AND M.es_sincronizado = 1";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pSociedad", sociedad);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetallePagosEntity detalleEntity = new DetallePagosEntity()
                                {
                                    valorTC = dataReader["valor"].ToString(),
                                };
                                (pagoResponse.lstDetallePagos ?? new List<DetallePagosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        pagoResponse.codigoError = "E";
                        pagoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(pagoResponse.codigoError))
                    {
                        if (pagoResponse.lstDetallePagos.Count == 0)
                        {
                            pagoResponse.codigoError = "E";
                            pagoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            pagoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                pagoResponse.codigoError = "E";
                pagoResponse.descripcionError = "1." + ex.Message;
            }
            return pagoResponse;
        }

        public static DetallePagos ObtenerDetallePagoAlm(string almacen, string forma)
        {
            DetallePagos pagoResponse = new DetallePagos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT sum(D.valor) as valor
                                        FROM detalle_pago as D
                                        INNER JOIN movimientos as M on M.id = D.movimiento_id
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        WHERE A.nombre_almacen = ?pAlmacen AND D.forma_pago_id = ?pForma AND M.es_sincronizado = 1";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pAlmacen", almacen);
                            myCmd.Parameters.AddWithValue("?pForma", forma);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetallePagosEntity detalleEntity = new DetallePagosEntity()
                                {
                                    valor = dataReader["valor"].ToString(),
                                };
                                (pagoResponse.lstDetallePagos ?? new List<DetallePagosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        pagoResponse.codigoError = "E";
                        pagoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(pagoResponse.codigoError))
                    {
                        if (pagoResponse.lstDetallePagos.Count == 0)
                        {
                            pagoResponse.codigoError = "E";
                            pagoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            pagoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                pagoResponse.codigoError = "E";
                pagoResponse.descripcionError = "1." + ex.Message;
            }
            return pagoResponse;
        }

        public static DetallePagos ObtenerDetallePagoAlmF(string almacen, string forma, string fechadesde, string fechahasta)
        {
            DetallePagos pagoResponse = new DetallePagos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT sum(D.valor) as valor
                                        FROM detalle_pago as D
                                        INNER JOIN movimientos as M on M.id = D.movimiento_id
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        WHERE A.nombre_almacen = ?pAlmacen AND D.forma_pago_id = ?pForma AND M.es_sincronizado = 1 AND (D.fecha_recaudo BETWEEN STR_TO_DATE(?pFechadesde, '%Y-%m-%d') AND STR_TO_DATE(CONCAT(?pFechahasta, '23:59:59'), '%Y-%m-%d %H:%i:%s'))";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pAlmacen", almacen);
                            myCmd.Parameters.AddWithValue("?pForma", forma);
                            myCmd.Parameters.AddWithValue("?pFechadesde", fechadesde);
                            myCmd.Parameters.AddWithValue("?pFechahasta", fechahasta);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetallePagosEntity detalleEntity = new DetallePagosEntity()
                                {
                                    valor = dataReader["valor"].ToString(),
                                };
                                (pagoResponse.lstDetallePagos ?? new List<DetallePagosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        pagoResponse.codigoError = "E";
                        pagoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(pagoResponse.codigoError))
                    {
                        if (pagoResponse.lstDetallePagos.Count == 0)
                        {
                            pagoResponse.codigoError = "E";
                            pagoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            pagoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                pagoResponse.codigoError = "E";
                pagoResponse.descripcionError = "1." + ex.Message;
            }
            return pagoResponse;
        }

        public static DetallePagos ObtenerDetallePagoF(string sociedad, string forma, string fechadesde, string fechahasta)
        {
            DetallePagos pagoResponse = new DetallePagos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT sum(D.valor) as valor
                                        FROM detalle_pago as D
                                        INNER JOIN movimientos as M on M.id = D.movimiento_id
                                        INNER JOIN almacenes as A on A.id = M.almacen_id
                                        WHERE A.sociedad_id = ?pSociedad_id AND D.forma_pago_id = ?pForma AND M.es_sincronizado = 1 AND (D.fecha_recaudo BETWEEN STR_TO_DATE(?pFechadesde, '%Y-%m-%d') AND STR_TO_DATE(CONCAT(?pFechahasta, '23:59:59'), '%Y-%m-%d %H:%i:%s'))";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pSociedad_id", sociedad);
                            myCmd.Parameters.AddWithValue("?pForma", forma);
                            myCmd.Parameters.AddWithValue("?pFechadesde", fechadesde);
                            myCmd.Parameters.AddWithValue("?pFechahasta", fechahasta);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetallePagosEntity detalleEntity = new DetallePagosEntity()
                                {
                                    valor = dataReader["valor"].ToString(),
                                };
                                (pagoResponse.lstDetallePagos ?? new List<DetallePagosEntity>()).Add(detalleEntity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        pagoResponse.codigoError = "E";
                        pagoResponse.descripcionError = "2." + ex.Message + "SQL" + queryS;
                    }
                    if (String.IsNullOrEmpty(pagoResponse.codigoError))
                    {
                        if (pagoResponse.lstDetallePagos.Count == 0)
                        {
                            pagoResponse.codigoError = "E";
                            pagoResponse.descripcionError = "3. No se encontro datos";
                        }
                        else
                        {
                            pagoResponse.codigoError = "OK";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                pagoResponse.codigoError = "E";
                pagoResponse.descripcionError = "1." + ex.Message;
            }
            return pagoResponse;
        }

        public static DetallePagos InsertarDetallePago(DetallePagosEntity detalle)
        {
            DetallePagos detalleResponse = new DetallePagos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO detalle_pago(movimiento_id,forma_pago_id,descripcion,valor,fecha_recaudo,created_at)
                                       VALUES(?pMovimiento_id,?pForma_pago_id,?pDescripcion,?pValor,?pFecha_recaudo,?pCreated_At)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pMovimiento_id", detalle.movimiento_id);
                                    myCmd.Parameters.AddWithValue("?pForma_pago_id", detalle.forma_pago_id);
                                    myCmd.Parameters.AddWithValue("?pDescripcion", detalle.descripcion);
                                    myCmd.Parameters.AddWithValue("?pValor", detalle.valor);
                                    myCmd.Parameters.AddWithValue("?pFecha_recaudo", System.DateTime.Now);
                                    myCmd.Parameters.AddWithValue("?pCreated_At", System.DateTime.Now);
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
                                        detalleResponse.descripcionError = "3. No se pudo abonar.";
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