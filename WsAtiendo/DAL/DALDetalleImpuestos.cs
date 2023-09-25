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
    public class DALDetalleImpuestos
    {
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000; ConvertZeroDateTime=True;Allow User Variables=true;";
        public static DetalleImpuestos ObtenerDetalleImpuesto(string detalle_movimiento)
        {
            DetalleImpuestos detalleResponse = new DetalleImpuestos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    mConnection.Open();
                    string queryS = @"SELECT *
                                        FROM detalle_impuesto
                                        WHERE detalle_movimiento_id=?pDetalle;";
                    try
                    {
                        using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection))
                        {
                            myCmd.Parameters.AddWithValue("?pDetalle", detalle_movimiento);
                            MySqlDataReader dataReader = myCmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                DetalleImpuestosEntity detalleEntity = new DetalleImpuestosEntity()
                                {
                                    id = dataReader["id"].ToString(),
                                    base_imponible = dataReader["base_imponible"].ToString(),
                                    porcentaje = dataReader["porcentaje"].ToString(),
                                };

                                (detalleResponse.lstDetalleImpuestos ?? new List<DetalleImpuestosEntity>()).Add(detalleEntity);
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
                        if (detalleResponse.lstDetalleImpuestos.Count == 0)
                        {
                            detalleResponse.codigoError = "E";
                            detalleResponse.descripcionError = "3. No existen pedidos.";
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
        public static DetalleImpuestos InsertarDetalleImpuestos(DetalleImpuestosEntity detalle)
        {
            DetalleImpuestos detalleResponse = new DetalleImpuestos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"INSERT INTO detalle_impuesto(detalle_movimiento_id,cod_impuesto,porcentaje,base_imponible,valor,created_at)
                                       VALUES(?pDetalle_movimiento_id,?pCod_impuesto,?pPorcentaje,?pBase_imponible,?pValor,?pCreatedAt)";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pDetalle_movimiento_id", detalle.detalle_movimiento_id);
                                    myCmd.Parameters.AddWithValue("?pCod_impuesto", detalle.cod_impuesto);
                                    myCmd.Parameters.AddWithValue("?pPorcentaje", detalle.porcentaje);
                                    myCmd.Parameters.AddWithValue("?pBase_imponible", detalle.base_imponible);
                                    myCmd.Parameters.AddWithValue("?pValor", detalle.valor);
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
                                        detalleResponse.descripcionError = "3. No se pudo crear el detalle del impuesto.";
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

        public static DetalleImpuestos ModificarDetalleImpuestosBP(DetalleImpuestosEntity detalle)
        {
            DetalleImpuestos detalleResponse = new DetalleImpuestos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE detalle_impuesto as DI
                                        JOIN detalles_movimientos as DM on DI.detalle_movimiento_id = DM.id
                                        SET DI.base_imponible = DM.precio
                                        WHERE DM.id=?pDetallemovimiento_id;";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pDetallemovimiento_id", detalle.detalle_movimiento_id);
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
                                        detalleResponse.descripcionError = "3. No se pudo actualizar la base imponible.";
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

        public static DetalleImpuestos ModificarMovimientoBP(DetalleImpuestosEntity detalle)
        {
            DetalleImpuestos detalleResponse = new DetalleImpuestos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"SELECT SUM(D.precio) into @suma
                                        FROM detalles_movimientos as D
                                        INNER JOIN productos AS P ON P.id = D.producto_id
                                        INNER JOIN tarifa_ice_iva AS T ON T.id = P.tarifa_ice_iva_id
                                        WHERE T.descripcion=?pDescripcion and D.movimiento_id = ?pMovimiento_id;

                                      UPDATE movimientos
                                      SET base_imponible=@suma,
                                          updated_at=?pUpdatedAt
                                      WHERE id=?pMovimiento_id;";
                    mConnection.Open();
                    try
                    {
                        using (MySqlTransaction trans = mConnection.BeginTransaction())
                        {
                            using (MySqlCommand myCmd = new MySqlCommand(queryS, mConnection, trans))
                            {
                                try
                                {
                                    myCmd.Parameters.AddWithValue("?pDescripcion", detalle.porcentaje);
                                    myCmd.Parameters.AddWithValue("?pMovimiento_id", detalle.movimiento_id);
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
                                        detalleResponse.descripcionError = "3. No se pudo actualizar la base imponible.";
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

        public static DetalleImpuestos ModificarDetalleImpuestosVal(DetalleImpuestosEntity detalle)
        {
            DetalleImpuestos detalleResponse = new DetalleImpuestos();
            try
            {
                using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
                {
                    string queryS = @"UPDATE detalle_impuesto
                                      SET valor=?pValor,
                                          updated_at=?pUpdatedAt
                                      WHERE detalle_movimiento_id=?pDetallemovimiento_id;";
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
                                    myCmd.Parameters.AddWithValue("?pDetallemovimiento_id", detalle.detalle_movimiento_id);
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
                                        detalleResponse.descripcionError = "3. No se pudo actualizar la base imponible.";
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