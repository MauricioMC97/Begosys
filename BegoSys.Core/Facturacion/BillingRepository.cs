using BegoSys.Domain;
using BegoSys.Domain.Entidades;
using BegoSys.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BegoSys.Core;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BegoSys.Common.Auxiliares;
using BegoSys.Common.Constantes;
using BegoSys.Common.Excepciones;

namespace BegoSys.Core.Facturacion
{
    public class BillingRepository : IBillingRepository
    {
        //Consulta los datos de los ingredientes e insumos para hacer inventario
        public List<PedidoTO> ConsultarListaPedidos(string sDoc, DatosLocalTO dLoc)
        {
            List<PedidoTO> datPed = null;
            //Consulta los ingredientes
            try
            {
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    
                    datPed = (from fact in db.Facturas
                              join df in db.DetalleFacturas on fact.IdRegistro equals df.IdRegFactura
                              join pd in db.Productos on df.IdProducto equals pd.IdProducto
                              where fact.EstadoFactura == "PENDIENTE"
                              select new PedidoTO
                              {
                                  IdPedidoDia = fact.IdPedidoDia,
                                  NombreProd = pd.NombreProducto,
                                  Cantidad = df.Cantidad,
                                  EstadoPedido = fact.EstadoFactura
                              }).ToList();
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine("Se presentó el siguiente error: " + Error.Message + Error.InnerException.Message);
            }
            return datPed;

        }

        public async Task SalvarPedido(FacturaTO DFac)
        {

            BegoSys.Core.Inventario.InventoryRepository CoreInventario = new BegoSys.Core.Inventario.InventoryRepository();
            DateTime dFInicio = DateTime.Today;
            DateTime dFFin = DateTime.Today.AddDays(1);
            long lContProductos = 0;

            //Guarda los datos de los pedidos 
            try
            {
                //AuxiliarBegoSys.EscribirLog(LogCategory.Debug, "Inicio SalvarPedido fecha " + DFac.Fecha.ToLongDateString() + ", Pedido Día: " + DFac.IdPedidoDia, false);

                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    Factura DatosPedidos = new Factura();


                    //Actualiza el número de facturación
                    ResolucionDian DatosResol = (db.ResolucionDians.Where(rds => rds.Activa == 1).Select(rd => rd)).FirstOrDefault();

                    DatosResol.Actual = (Convert.ToInt64(DatosResol.Actual) + 1).ToString();

                    await db.SaveChangesAsync();


                    //Crea la factura en la tabla correspondiente
                    //Continua en el siguiente número de registro                   
                    DatosPedidos.IdRegistro = (db.Facturas.Max(fct => fct.IdRegistro)) + 1;

                    //Si no existen pedidos para la fecha de hoy reinicia el contador con el valor de 1, si si existen continua en el valor siguiente
                    if (DFac.Fecha != DateTime.MinValue)
                    {
                        dFInicio = DFac.Fecha;
                        dFFin = DFac.Fecha.AddDays(1);
                    }
                    else
                    {
                        dFInicio = DateTime.Now;
                        dFFin = DateTime.Now.AddDays(1);
                        DFac.Fecha = DateTime.Now;
                    }

                    DatosPedidos.IdPedidoDia = ((db.Facturas.Where(fcp => fcp.IdLocal == DFac.IdLocal && fcp.Fecha >= dFInicio.Date).Count() > 0) ? db.Facturas.Where(fcp => fcp.IdLocal == DFac.IdLocal && (fcp.Fecha >= dFInicio.Date && fcp.Fecha <= dFFin)).DefaultIfEmpty().Max(f => f == null ? 0 : f.IdPedidoDia) + 1 : 1); //((dFInicio != null) ? ((db.Facturas.Where(fcp => fcp.IdLocal == DFac.IdLocal && fcp.Fecha >= dFInicio.Date).Count() > 0) ? db.Facturas.Where(fcp => fcp.IdLocal == DFac.IdLocal && (fcp.Fecha >= dFInicio.Date && fcp.Fecha <= dFFin)).Max(f => f.IdPedidoDia) + 1 : 1) : ((db.Facturas.Where(fcp => fcp.IdLocal == DFac.IdLocal && fcp.Fecha >= DateTime.Today).Count() > 0) ? db.Facturas.Where(fcp => fcp.IdLocal == DFac.IdLocal && fcp.Fecha >= DateTime.Today).Max(f => f.IdPedidoDia) + 1 : 1));

                    //Busca en la tabla jbResolucionesDian para saber cual el número que continua
                    DatosPedidos.NroResolucionDian = (db.ResolucionDians.Where(rds => rds.Activa == 1).Select(rd => rd.NroResolucionDian)).FirstOrDefault();
                    DFac.NroResolucionDian = DatosPedidos.NroResolucionDian; //Asigna este valor para imprimirlo

                    DatosPedidos.NroFacturaDian = DatosResol.Actual;
                    DFac.NroFacturaDian = DatosPedidos.NroFacturaDian; //Asigna este valor para imprimirlo

                    DatosPedidos.Fecha = ((dFInicio != null) ? dFInicio : DateTime.Now); // Convert.ToDateTime("09/01/2019 09:50"); // ;

                    DatosPedidos.IdTipoDespacho = DFac.TipoDespacho;

                    DatosPedidos.Impuesto = DFac.Impuesto;

                    DatosPedidos.ValorTotal = DFac.ValorTotal;

                    DatosPedidos.EstadoFactura = DFac.EstadoFactura;

                    DatosPedidos.IdPersona = DFac.IdPersona;

                    DatosPedidos.IdLocal = DFac.IdLocal;

                    DatosPedidos.Direccion = DFac.Direccion;

                    DatosPedidos.DocCliente = DFac.DocCliente;

                    DatosPedidos.Cliente = DFac.Cliente;

                    DatosPedidos.Telefono = DFac.Telefono;

                    db.Facturas.Add(DatosPedidos);
                    await db.SaveChangesAsync();

                    //Se selecciona el siguiente registro para anexar los detalles de las facturas
                    long IdRegDetalle = (db.DetalleFacturas.Count() > 0 ? (db.DetalleFacturas.Max(df => df.IdRegistro) + 1) : 1);
                    long iRegistrosP = IdRegDetalle;
                    //Guarda el detalle de las facturas                     
                    foreach (DetalleFacturaTO ProductoPedido in DFac.DetallePedido)
                    {

                        //Se eliminan los registros que no tienen un producto
                        if (ProductoPedido.IdProducto == 0 && ProductoPedido.Cantidad == 0)
                        {
                            continue;
                        }

                        DetalleFactura DatosProductos = new DetalleFactura();
                        DatosProductos.IdRegistro = iRegistrosP;
                        DatosProductos.IdRegFactura = DatosPedidos.IdRegistro;
                        DatosProductos.IdProducto = ProductoPedido.IdProducto;
                        DatosProductos.Cantidad = ProductoPedido.Cantidad;
                        DatosProductos.ValorUnitario = ProductoPedido.ValorUnitario;
                        DatosProductos.Subtotal = ProductoPedido.Subtotal;
                        DatosProductos.Observaciones = ProductoPedido.Observaciones;

                        db.DetalleFacturas.Add(DatosProductos);
                        iRegistrosP = iRegistrosP + 1;
                        await db.SaveChangesAsync();

                        //AuxiliarBegoSys.EscribirLog(LogCategory.Debug, "Voy a RetirarProducto fecha " + DFac.Fecha.ToLongDateString() + ", Pedido Día: " + DFac.IdPedidoDia, false);

                        //Descuenta del inventario los ingredientes vendidos
                        for (lContProductos = 0; lContProductos < ProductoPedido.Cantidad; lContProductos++) {
                            await CoreInventario.RetirarProducto(ProductoPedido.IdProducto, DFac.IdLocal, DFac.IdPersona, dFInicio);
                        }
                    }

                    //Imprime el pedido
                    //DialogResult dialogResult = MessageBox.Show("Desea imprimir el recibo?", "Imprimir", MessageBoxButtons.YesNo);
                    //if (dialogResult == DialogResult.Yes)
                    //{
                        PrintReceiptForTransaction(DFac);
                    //}
                    AuxiliarBegoSys.EscribirLog(LogCategory.Debug, "Fin SalvarPedido fecha " + DFac.Fecha.ToLongDateString() + ", Pedido Día: " + DFac.IdPedidoDia, false);
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine("SalvarPedido: Se presentó el siguiente error al guardar la factura: " + Error.Message + Error.InnerException.Message);
                //AuxiliarBegoSys.EscribirLog(LogCategory.Debug, "SalvarPedido: Se presentó el siguiente error al guardar la factura: " + Error.Message + Error.InnerException.Message, false);
                throw new BegoSysException("SalvarPedido: Se presentó el siguiente error al guardar la factura: " + Error.Message + Error.InnerException.Message);
            }
        }

        public void PrintReceiptForTransaction(FacturaTO DFac)
        {
            /*Debe contener el nombre o razón social de la empresa.
            NIT de la persona o empresa que vende un producto o servicio.
            Fecha en la que se expide la factura.
            El documento debe decir claramente “Factura de Venta”.
            Nombre o razón del cliente.
            NIT o identificación del cliente.
            Descripción de los servicios o productos vendidos.
            Consecutivo de la factura (según la resolución de la DIAN).
            El valor total de la factura, con el discriminado de la tarifa y el valor de IVA pagado.
            Y finalmente la resolución de facturación por computador autorizada por la DIAN.*/

            PrintDocument recordDoc = new PrintDocument();

            recordDoc.DocumentName = "Recibo de compra";
            recordDoc.PrintPage += (sender, e) => PintarRecibo(DFac, e); //new PrintPageEventHandler(PrintReceiptPage); // function below
            recordDoc.PrintController = new StandardPrintController(); // hides status dialog popup

            // Comment if debugging 
            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = "Bixolon SRP-350Plus";
            recordDoc.PrinterSettings = ps;
            recordDoc.Print();
            // --------------------------------------

            // Uncomment if debugging - shows dialog instead
            //PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
            //printPrvDlg.Document = recordDoc;
            //printPrvDlg.Width = 1200;
            //printPrvDlg.Height = 800;
            //printPrvDlg.ShowDialog();
            // --------------------------------------

            recordDoc.Dispose();
        }

        private static void PintarRecibo(FacturaTO DatosF, PrintPageEventArgs e)
        {
            float x = 10;
            float y = 5;
            float width = 270.0F; // max width I found through trial and error
            float height = 0F;

            Font drawFontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
            Font drawFontArial10Regular = new Font("Arial", 10, FontStyle.Regular);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Set format of string.
            StringFormat drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            StringFormat drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            StringFormat drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            // Draw string to screen.
            string text = "JuiceBar.co Unicentro";
            e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

            text = "Tel: 3192319827";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Calle 34B 66 A 02";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Bego Inversiones S.A.S";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "NIT 901.226.468-2";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Resolución DIAN # " + DatosF.NroResolucionDian + " del 28/12/2018 desde el 7499963 hasta el 9000000 Regimen Común";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += (e.Graphics.MeasureString(text, drawFontArial10Regular).Height * 3);

            text = "Medellín, Colombia";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Factura de Venta # " + DatosF.NroFacturaDian + " " + DatosF.Fecha.ToShortDateString();
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Cant.   Producto                Subtotal";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;


            foreach (DetalleFacturaTO ProductoPedido in DatosF.DetallePedido)
            {

                //Se eliminan los registros que no tienen un producto
                if (ProductoPedido.IdProducto == 0 && ProductoPedido.Cantidad == 0)
                {
                    continue;
                }

                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    text = ProductoPedido.Cantidad + "  " + db.Productos.Where(np => np.IdProducto == ProductoPedido.IdProducto).Select(npd => npd.NombreProducto).FirstOrDefault() + "   " + ProductoPedido.Subtotal; 
                }
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            }

            text = "Impoconsumo (8%): " + DatosF.Impuesto;
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Valor Total: " + DatosF.ValorTotal;
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

        }


        //private static void PrintReceiptPage(object sender, PrintPageEventArgs e)
        //{
        //    float x = 10;
        //    float y = 5;
        //    float width = 270.0F; // max width I found through trial and error
        //    float height = 0F;

        //    Font drawFontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
        //    Font drawFontArial10Regular = new Font("Arial", 10, FontStyle.Regular);
        //    SolidBrush drawBrush = new SolidBrush(Color.Black);

        //    // Set format of string.
        //    StringFormat drawFormatCenter = new StringFormat();
        //    drawFormatCenter.Alignment = StringAlignment.Center;
        //    StringFormat drawFormatLeft = new StringFormat();
        //    drawFormatLeft.Alignment = StringAlignment.Near;
        //    StringFormat drawFormatRight = new StringFormat();
        //    drawFormatRight.Alignment = StringAlignment.Far;

        //    // Draw string to screen.
        //    string text = "JuiceBar.co Unicentro";
        //    e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //    y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

        //    text = "Tel: 3192319827";
        //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

        //    text = "Calle 34B 66 A 02";
        //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

        //    text = "Bego Inversiones S.A.S";
        //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

        //    text = "NIT 901.226.468-2";
        //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

        //    text = "Resolución DIAN # 18762011519624 del 2018/12/01 desde el 0000001 hasta el 7499962 Regimen Común";
        //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

        //    text = "Medellín, Colombia";
        //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

        //    text = "Factura de Venta # 0000001 01/12/2018 09:32:05 AM";
        //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

        //    text = "Cant.   Producto  Subtotal";
        //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;


        //    foreach (DetalleFacturaTO ProductoPedido in DFac.DetallePedido)
        //    {

        //    //    //Se eliminan los registros que no tienen un producto
        //        if (ProductoPedido.IdProducto == 0 && ProductoPedido.Cantidad == 0)
        //        {
        //            continue;
        //        }

        //        text = ProductoPedido.Cantidad + "  " + ProductoPedido.IdProducto + "   " + ProductoPedido.Subtotal;
        //        e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //        y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

        //    }

        //}

        public async Task AnulaPedido(long ipd, long idLocal, long idPersona)
        {
            try
            {
                TrasladoIngrTO dDatos;
                List<MedidasReceta> lstMedidas;

                //Se anulan las facturas del día de hoy que estén en estado PENDIENTE solamente
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    Factura FactAnular = new Factura();

                    FactAnular = db.Facturas.Where(fa => fa.IdPedidoDia == ipd && fa.Fecha == DateTime.Today && fa.EstadoFactura == "PENDIENTE").Select(reg => reg).FirstOrDefault();

                    List<DetalleFactura> lstDetallesF = db.DetalleFacturas.Where(dfp => dfp.IdRegFactura == FactAnular.IdRegistro).ToList();

                    //Recorrer los ingredientes para ir retornando al inventario los productos anulados
                    foreach (DetalleFactura ElemDF in lstDetallesF) { 
                        lstMedidas = db.MedidasRecetas.Where(mr => mr.IdProducto == ElemDF.IdProducto).ToList();

                        if (lstMedidas != null)
                        {
                            foreach (MedidasReceta ElemIngr in lstMedidas)
                            {

                                //Se verifica si el ingrediente existe en el local origen
                                var UpdEncOrigen = (from Inv in db.Inventarios where Inv.IdIngrediente == ElemIngr.IdIngrediente && Inv.IdLocal == idLocal select Inv).FirstOrDefault();

                                if (UpdEncOrigen != null)
                                {
                                    //Vuelve y adiciona el ingrediente en el inventario del local
                                    UpdEncOrigen.CantidadActual = (long)(UpdEncOrigen.CantidadActual + ElemIngr.Cantidad);
                                    UpdEncOrigen.UnidadesActuales = (long)(UpdEncOrigen.UnidadesActuales + ElemIngr.Cantidad);

                                     //Registra entrada al local nuevamente en el detalle
                                    DetalleInventario AdicionarDestino = new DetalleInventario();
                                    //Selecciona el máximo registro para aumentar en uno el valor
                                    AdicionarDestino.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                                    AdicionarDestino.IdIngrediente = ElemIngr.IdIngrediente;
                                    AdicionarDestino.IdEnvase = null;
                                    AdicionarDestino.IdInsumo = null;
                                    AdicionarDestino.Cantidad = ElemIngr.Cantidad;
                                    AdicionarDestino.CostoTotal = 0;
                                    AdicionarDestino.Unidades = ElemIngr.Cantidad;
                                    AdicionarDestino.CostoUnidad = 0;
                                    AdicionarDestino.IdMedida = ElemIngr.IdMedida;
                                    AdicionarDestino.FechaHora = DateTime.Now;
                                    AdicionarDestino.IdLocal = idLocal;
                                    AdicionarDestino.IdProveedor = null;
                                    AdicionarDestino.Transaccion = "ENTRA";
                                    AdicionarDestino.IdPersona = idPersona;
                                    AdicionarDestino.ConExistencias = 1; //Cuando llega nuevo hay existencias del producto

                                    db.DetalleInventarios.Add(AdicionarDestino);

                                    db.SaveChanges();
                                }

                                FactAnular.EstadoFactura = "ANULADO";
                                await db.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            throw new BegoSysException("Billing_FactEntregada");
                        }
                    }

                }
            }
            catch (Exception Error)
            {
                throw new BegoSysException("Se presentó el siguiente error" + Error.Message + Error.InnerException.Message);
            }
        }

        public void ImprimirFactura (long NroFactura)
        {
            //Imprime el recibo a solicitod del cliente
            PrintReceiptForTransaction(ConsultarFactura(NroFactura));
        }

        public FacturaTO ConsultarFactura(long NroFactura)
        {
            FacturaTO DatosF;

            using (var db = EntidadesJuicebar.GetDbContext())
            {
                DatosF = (from fc in db.Facturas 
                          where fc.IdRegistro == NroFactura
                          select new FacturaTO {
                              IdRegistro = fc.IdRegistro,
                              IdPedidoDia = fc.IdPedidoDia,
                              Fecha = fc.Fecha,
                              NroFacturaDian = fc.NroFacturaDian,
                              NroResolucionDian = fc.NroResolucionDian,
                              EstadoFactura = fc.EstadoFactura,
                              TipoDespacho = fc.IdTipoDespacho,
                              Cliente = fc.Cliente,
                              Direccion = fc.Direccion,
                              DocCliente = fc.DocCliente,
                              IdLocal = fc.IdLocal,
                              IdPersona = fc.IdPersona,
                              Impuesto = fc.Impuesto,
                              Telefono = fc.Telefono,
                              ValorTotal = fc.ValorTotal,
                              DetallePedido = null
                          }).FirstOrDefault();


                DatosF.DetallePedido = (from dfc in db.DetalleFacturas
                                        where dfc.IdRegFactura == NroFactura
                                        select new DetalleFacturaTO {
                                            IdRegistro = dfc.IdRegistro,
                                            IdRegFactura = dfc.IdRegFactura,
                                            IdProducto = dfc.IdProducto,
                                            Cantidad = dfc.Cantidad,
                                            Observaciones = dfc.Observaciones,
                                            Subtotal = dfc.Subtotal,
                                            ValorUnitario = dfc.ValorUnitario
                                        }).ToList();
            }

            return DatosF;
        }
    }
}