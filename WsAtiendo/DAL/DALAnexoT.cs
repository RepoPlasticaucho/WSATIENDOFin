using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using WsAtiendo.Models;
using System.Xml;
using System.IO;
using System.Net;
using System.Text;

namespace WsAtiendo.DAL
{
    public class DALAnexoT
    {
        /*
        private static string ConnectionString = "Server=atiendo.ec;Uid=eojgprlg_calidad;Pwd=C@l!d@d2o21;database=" + ConfigurationManager.AppSettings["BDD"] + ";default command timeout=50000; Allow User Variables=true;";
        public static String CrearXML(string sociedad)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    XmlDocument xmlDocument = new XmlDocument();

                    string query = @"SELECT TA.codigo as cod1,S.razon_social,S.nombre_comercial,S.id_fiscal,M.clave_acceso,TC.codigo,M.estab,A.pto_emision,
                                    M.secuencial,S.dir1,DATE_FORMAT(M.fecha_emision, '%d/%m/%Y') as fecha_emision,TT.codigo as cod2,T.nombre,T.id_fiscal as ced,T.direccioncl,M.total_si,M.total_desc,
                                    M.propina,M.importe_total,M.moneda,M.valor_rete_iva,M.valor_rete_renta
                                    FROM movimientos as M
                                    INNER JOIN almacenes as A on A.id = M.almacen_id
                                    INNER JOIN sociedades as S on S.id = A.sociedad_id
                                    INNER JOIN tipo_ambiente as TA on TA.id = S.tipo_ambienteid
                                    INNER JOIN tipo_comprobante as TC on TC.id = M.tipo_comprb_id
                                    INNER JOIN terceros as T on M.tercero_id = T.id
                                    INNER JOIN tipo_tercero as TT on TT.id = T.tipo_tercero
                                    WHERE M.id = ?pMovimiento_id";

                    string query2 = @"SELECT I.codigo as cod3, TIV.codigo as cod4, SUM(DI.base_imponible) as sumBP, SUM(DI.valor) as sumVal
                                    FROM detalles_movimientos as DM
                                    INNER JOIN movimientos as M on M.id = DM.movimiento_id
                                    INNER JOIN detalle_impuesto as DI on DI.detalle_movimiento_id = DM.id
                                    INNER JOIN impuestos as I on I.codigo = DI.cod_impuesto
                                    INNER JOIN tarifa_ice_iva as TIV on TIV.descripcion = DI.porcentaje
                                    WHERE M.id = ?pMovimiento_id
                                    GROUP BY cod4";

                    string query3 = @"SELECT FP.codigo as cod5, DP.valor as vTotal
                                    FROM detalle_pago as DP
                                    INNER JOIN formas_pago as FP on FP.id = DP.forma_pago_id
                                    INNER JOIN movimientos as M on M.id = DP.movimiento_id
                                    WHERE M.id = ?pMovimiento_id";

                    string query4 = @"SELECT DM.inventario_id,DM.producto_id,P.nombre as nomPro,DM.cantidad,DM.costo,DM.desc_add,DM.precio,
                                    I.codigo as cod6, CAST(TIV.porcentaje*100 as unsigned) as tarifa, TIV.codigo as cod7, DI.base_imponible, DI.valor
                                    FROM detalles_movimientos as DM
                                    INNER JOIN movimientos as M on M.id = DM.movimiento_id
                                    INNER JOIN productos as P on P.id = DM.producto_id
                                    INNER JOIN detalle_impuesto as DI on DI.detalle_movimiento_id = DM.id
                                    INNER JOIN impuestos as I on I.codigo = DI.cod_impuesto
                                    INNER JOIN tarifa_ice_iva as TIV on TIV.descripcion = DI.porcentaje
                                    WHERE M.id = ?pMovimiento_id
                                    ORDER BY DM.id asc";

                    string query5 = @"SELECT S.url_certificado, S.clave_certificado, M.clave_acceso
                                    FROM movimientos as M
                                    INNER JOIN almacenes as A on A.id = M.almacen_id
                                    INNER JOIN sociedades as S on S.id = A.sociedad_id
                                    WHERE M.id = ?pMovimiento_id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?pMovimiento_id", movimiento);


                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataSet dataSet = new DataSet();
                            adapter.Fill(dataSet);

                            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
                            xmlDocument.AppendChild(xmlDeclaration);

                            XmlElement rootElement = xmlDocument.CreateElement("factura");
                            rootElement.SetAttribute("id", "comprobante");
                            rootElement.SetAttribute("version", "1.1.0");
                            xmlDocument.AppendChild(rootElement);

                            DataTable dataTable = dataSet.Tables[0];
                            foreach (DataRow row in dataTable.Rows)
                            {
                                XmlElement rowElement = xmlDocument.CreateElement("infoTributaria");

                                XmlElement columna1Element = xmlDocument.CreateElement("ambiente");
                                columna1Element.InnerText = row["cod1"].ToString();
                                rowElement.AppendChild(columna1Element);

                                XmlElement columna2Element = xmlDocument.CreateElement("tipoEmision");
                                columna2Element.InnerText = "1";
                                rowElement.AppendChild(columna2Element);

                                XmlElement columna3Element = xmlDocument.CreateElement("razonSocial");
                                columna3Element.InnerText = row["razon_social"].ToString();
                                rowElement.AppendChild(columna3Element);

                                XmlElement columna4Element = xmlDocument.CreateElement("nombreComercial");
                                columna4Element.InnerText = row["nombre_comercial"].ToString();
                                rowElement.AppendChild(columna4Element);

                                XmlElement columna5Element = xmlDocument.CreateElement("ruc");
                                columna5Element.InnerText = row["id_fiscal"].ToString();
                                rowElement.AppendChild(columna5Element);

                                XmlElement columna6Element = xmlDocument.CreateElement("claveAcceso");
                                columna6Element.InnerText = row["clave_acceso"].ToString();
                                rowElement.AppendChild(columna6Element);

                                XmlElement columna7Element = xmlDocument.CreateElement("codDoc");
                                columna7Element.InnerText = row["codigo"].ToString();
                                rowElement.AppendChild(columna7Element);

                                XmlElement columna8Element = xmlDocument.CreateElement("estab");
                                columna8Element.InnerText = row["estab"].ToString();
                                rowElement.AppendChild(columna8Element);

                                XmlElement columna9Element = xmlDocument.CreateElement("ptoEmi");
                                columna9Element.InnerText = row["pto_emision"].ToString();
                                rowElement.AppendChild(columna9Element);

                                XmlElement columna10Element = xmlDocument.CreateElement("secuencial");
                                columna10Element.InnerText = row["secuencial"].ToString();
                                rowElement.AppendChild(columna10Element);

                                XmlElement columna11Element = xmlDocument.CreateElement("dirMatriz");
                                columna11Element.InnerText = row["dir1"].ToString();
                                rowElement.AppendChild(columna11Element);


                                rootElement.AppendChild(rowElement);

                                XmlElement rowElement2 = xmlDocument.CreateElement("infoFactura");

                                XmlElement columna12Element = xmlDocument.CreateElement("fechaEmision");
                                columna12Element.InnerText = row["fecha_emision"].ToString();
                                rowElement2.AppendChild(columna12Element);

                                XmlElement columna0Element = xmlDocument.CreateElement("obligadoContabilidad");
                                columna0Element.InnerText = "SI";
                                rowElement2.AppendChild(columna0Element);

                                XmlElement columna13Element = xmlDocument.CreateElement("tipoIdentificacionComprador");
                                columna13Element.InnerText = row["cod2"].ToString();
                                rowElement2.AppendChild(columna13Element);

                                XmlElement columna14Element = xmlDocument.CreateElement("razonSocialComprador");
                                columna14Element.InnerText = row["nombre"].ToString();
                                rowElement2.AppendChild(columna14Element);

                                XmlElement columna15Element = xmlDocument.CreateElement("identificacionComprador");
                                columna15Element.InnerText = row["ced"].ToString();
                                rowElement2.AppendChild(columna15Element);

                                XmlElement columna16Element = xmlDocument.CreateElement("direccionComprador");
                                columna16Element.InnerText = row["direccioncl"].ToString();
                                rowElement2.AppendChild(columna16Element);

                                XmlElement columna17Element = xmlDocument.CreateElement("totalSinImpuestos");
                                columna17Element.InnerText = row["total_si"].ToString();
                                rowElement2.AppendChild(columna17Element);

                                XmlElement columna18Element = xmlDocument.CreateElement("totalDescuento");
                                columna18Element.InnerText = row["total_desc"].ToString();
                                rowElement2.AppendChild(columna18Element);

                                rootElement.AppendChild(rowElement2);

                                XmlElement rowElement3 = xmlDocument.CreateElement("totalConImpuestos");
                                using (MySqlCommand command2 = new MySqlCommand(query2, connection))
                                {
                                    command2.Parameters.AddWithValue("?pMovimiento_id", movimiento);

                                    using (MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2))
                                    {
                                        DataSet dataSet2 = new DataSet();
                                        adapter2.Fill(dataSet2);

                                        DataTable dataTable2 = dataSet2.Tables[0];

                                        foreach (DataRow row2 in dataTable2.Rows)
                                        {
                                            XmlElement rowElement4 = xmlDocument.CreateElement("totalImpuesto");

                                            XmlElement columna19Element = xmlDocument.CreateElement("codigo");
                                            columna19Element.InnerText = row2["cod3"].ToString();
                                            rowElement4.AppendChild(columna19Element);

                                            XmlElement columna20Element = xmlDocument.CreateElement("codigoPorcentaje");
                                            columna20Element.InnerText = row2["cod4"].ToString();
                                            rowElement4.AppendChild(columna20Element);

                                            XmlElement columna21Element = xmlDocument.CreateElement("baseImponible");
                                            columna21Element.InnerText = row2["sumBP"].ToString();
                                            rowElement4.AppendChild(columna21Element);

                                            XmlElement columna22Element = xmlDocument.CreateElement("valor");
                                            columna22Element.InnerText = row2["sumVal"].ToString();
                                            rowElement4.AppendChild(columna22Element);

                                            rowElement3.AppendChild(rowElement4);
                                        }
                                        rowElement2.AppendChild(rowElement3);
                                    }
                                }

                                XmlElement columna23Element = xmlDocument.CreateElement("propina");
                                columna23Element.InnerText = row["propina"].ToString();
                                rowElement2.AppendChild(columna23Element);

                                XmlElement columna24Element = xmlDocument.CreateElement("importeTotal");
                                columna24Element.InnerText = row["importe_total"].ToString();
                                rowElement2.AppendChild(columna24Element);

                                XmlElement columna25Element = xmlDocument.CreateElement("moneda");
                                columna25Element.InnerText = row["moneda"].ToString();
                                rowElement2.AppendChild(columna25Element);

                                using (MySqlCommand command3 = new MySqlCommand(query3, connection))
                                {
                                    command3.Parameters.AddWithValue("?pMovimiento_id", movimiento);

                                    using (MySqlDataAdapter adapter3 = new MySqlDataAdapter(command3))
                                    {
                                        DataSet dataSet3 = new DataSet();
                                        adapter3.Fill(dataSet3);

                                        DataTable dataTable3 = dataSet3.Tables[0];
                                        XmlElement rowElement5 = xmlDocument.CreateElement("pagos");
                                        foreach (DataRow row3 in dataTable3.Rows)
                                        {
                                            XmlElement rowElement6 = xmlDocument.CreateElement("pago");

                                            XmlElement columna26Element = xmlDocument.CreateElement("formaPago");
                                            columna26Element.InnerText = row3["cod5"].ToString();
                                            rowElement6.AppendChild(columna26Element);

                                            XmlElement columna27Element = xmlDocument.CreateElement("total");
                                            columna27Element.InnerText = row3["vTotal"].ToString();
                                            rowElement6.AppendChild(columna27Element);

                                            rowElement5.AppendChild(rowElement6);
                                        }

                                        rowElement2.AppendChild(rowElement5);
                                    }
                                }
                                XmlElement columna28Element = xmlDocument.CreateElement("valorRetIva");
                                columna28Element.InnerText = row["valor_rete_iva"].ToString();
                                rowElement2.AppendChild(columna28Element);

                                XmlElement columna29Element = xmlDocument.CreateElement("valorRetRenta");
                                columna29Element.InnerText = row["valor_rete_renta"].ToString();
                                rowElement2.AppendChild(columna29Element);

                                using (MySqlCommand command4 = new MySqlCommand(query4, connection))
                                {
                                    command4.Parameters.AddWithValue("?pMovimiento_id", movimiento);

                                    using (MySqlDataAdapter adapter4 = new MySqlDataAdapter(command4))
                                    {
                                        DataSet dataSet4 = new DataSet();
                                        adapter4.Fill(dataSet4);

                                        DataTable dataTable4 = dataSet4.Tables[0];
                                        XmlElement rowElement7 = xmlDocument.CreateElement("detalles");
                                        foreach (DataRow row4 in dataTable4.Rows)
                                        {
                                            XmlElement rowElement8 = xmlDocument.CreateElement("detalle");

                                            XmlElement columna30Element = xmlDocument.CreateElement("codigoPrincipal");
                                            columna30Element.InnerText = row4["inventario_id"].ToString();
                                            rowElement8.AppendChild(columna30Element);

                                            XmlElement columna31Element = xmlDocument.CreateElement("codigoAuxiliar");
                                            columna31Element.InnerText = row4["producto_id"].ToString();
                                            rowElement8.AppendChild(columna31Element);

                                            XmlElement columna32Element = xmlDocument.CreateElement("descripcion");
                                            columna32Element.InnerText = row4["nomPro"].ToString();
                                            rowElement8.AppendChild(columna32Element);

                                            XmlElement columna33Element = xmlDocument.CreateElement("cantidad");
                                            columna33Element.InnerText = row4["cantidad"].ToString();
                                            rowElement8.AppendChild(columna33Element);

                                            XmlElement columna34Element = xmlDocument.CreateElement("precioUnitario");
                                            columna34Element.InnerText = row4["costo"].ToString();
                                            rowElement8.AppendChild(columna34Element);

                                            XmlElement columna35Element = xmlDocument.CreateElement("descuento");
                                            columna35Element.InnerText = row4["desc_add"].ToString();
                                            rowElement8.AppendChild(columna35Element);

                                            XmlElement columna36Element = xmlDocument.CreateElement("precioTotalSinImpuesto");
                                            columna36Element.InnerText = row4["precio"].ToString();
                                            rowElement8.AppendChild(columna36Element);

                                            XmlElement rowElement9 = xmlDocument.CreateElement("impuestos");
                                            XmlElement rowElement10 = xmlDocument.CreateElement("impuesto");

                                            XmlElement columna37Element = xmlDocument.CreateElement("codigo");
                                            columna37Element.InnerText = row4["cod6"].ToString();
                                            rowElement10.AppendChild(columna37Element);

                                            XmlElement columna38Element = xmlDocument.CreateElement("codigoPorcentaje");
                                            columna38Element.InnerText = row4["cod7"].ToString();
                                            rowElement10.AppendChild(columna38Element);

                                            XmlElement columna39Element = xmlDocument.CreateElement("tarifa");
                                            columna39Element.InnerText = row4["tarifa"].ToString();
                                            rowElement10.AppendChild(columna39Element);

                                            XmlElement columna40Element = xmlDocument.CreateElement("baseImponible");
                                            columna40Element.InnerText = row4["base_imponible"].ToString();
                                            rowElement10.AppendChild(columna40Element);

                                            XmlElement columna41Element = xmlDocument.CreateElement("valor");
                                            columna41Element.InnerText = row4["valor"].ToString();
                                            rowElement10.AppendChild(columna41Element);

                                            rowElement9.AppendChild(rowElement10);
                                            rowElement8.AppendChild(rowElement9);
                                            rowElement7.AppendChild(rowElement8);
                                        }

                                        rootElement.AppendChild(rowElement7);
                                    }
                                }
                            }

                        }
                    }

                    using (MySqlCommand command5 = new MySqlCommand(query5, connection))
                    {
                        command5.Parameters.AddWithValue("?pMovimiento_id", movimiento);

                        using (MySqlDataAdapter adapter5 = new MySqlDataAdapter(command5))
                        {
                            DataSet dataSet5 = new DataSet();
                            adapter5.Fill(dataSet5);

                            DataTable dataTable5 = dataSet5.Tables[0];

                            foreach (DataRow row5 in dataTable5.Rows)
                            {
                                // Convertir el documento XML a una cadena
                                string xmlContent = xmlDocument.OuterXml;

                                    // Abrir el archivo XML a firmar
                                    using (MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent)))
                                    {

                                        // Detalles de la URL del servidor remoto
                                        string urlServidorRemoto = "ftp://calidad.atiendo.ec/FacturasXML/";
                                        string usuario = "eojgprlg@calidad.atiendo.ec";
                                        string contraseña = "rs2NB4XN94we";

                                        // Nombre del archivo basado en el movimiento_id
                                        string nombrePre = row5["clave_acceso"].ToString();
                                        string nombreArchivo = $"factura_{nombrePre}.xml"; // Cambiar el formato según tus preferencias

                                        // Crear una solicitud FTP para cargar el archivo en el servidor
                                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"{urlServidorRemoto}{nombreArchivo}");
                                        request.Method = WebRequestMethods.Ftp.UploadFile;
                                        request.Credentials = new NetworkCredential(usuario, contraseña);

                                        // Obtener el contenido del archivo XML como una matriz de bytes
                                        byte[] fileContents = Encoding.UTF8.GetBytes(xmlContent);

                                        // Enviar el contenido del archivo al servidor
                                        using (Stream requestStream = request.GetRequestStream())
                                        {
                                            requestStream.Write(fileContents, 0, fileContents.Length);
                                        }

                                        // Obtener la respuesta del servidor
                                        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                                        {
                                            Console.WriteLine($"Estado de carga: {response.StatusDescription}");
                                        }
                                    }
                                
                            }
                        }
                    }
                }
                return "CREADO";
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + ex.StackTrace + ex.Source + ex.HelpLink + ex.Data + ex.InnerException;
            }
        }
        */
    }
}