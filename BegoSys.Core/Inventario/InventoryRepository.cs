using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BegoSys.Core.Contabilidad;
using BegoSys.Domain;
using BegoSys.Domain.Entidades;
using BegoSys.TO;

namespace BegoSys.Core.Inventario
{
    public class InventoryRepository : IInventoryRepository
    {
        //Consulta los datos de los ingredientes e insumos para hacer inventario
        public List<InventarioTO> ConsultarListaInventario(string sDoc, DatosLocalTO dLoc)
        {
            List<InventarioTO> datInven = null;
            //Consulta los ingredientes
            try
            {
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    //Selecciona solamente las frutas e insumos que se estén manejando en inventario actualmente
                    datInven = (from ing in db.Ingredientes
                                join med in db.Medidas on ing.IdMedida equals med.idMedida
                                where (ing.IdTipoIngrediente == 1 || ing.IdTipoIngrediente == 3) && ing.Inventario == 1
                                select new InventarioTO
                                {
                                    IdIngrediente = ing.IdIngrediente,
                                    NombreIngrediente = ing.NombreIngrediente,
                                    NombreMedida = med.nombreMedida
                                }).ToList();
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine("Se presentó el siguiente error: " + Error.Message + Error.InnerException.Message);
            }
            return datInven;
            //Consulta los insumos de la tabla jbInsumos
        }

        //Adiciona un elemento al inventario y llama al procedimiento para contabilizar su compra
        public bool AdicionarInventario(long? idingr, long? idenv, long? idins, double cantidad, double valor, double? nrounidades, double? costounidades, long idloc, string nitprov, long idpers)
        {
            try
            {
                var CoreContable = new BegoSys.Core.Contabilidad.AccountingRepository();
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    DetalleInventario DatosNuevoInventario = new DetalleInventario();

                    //Se va a registrar la compra de un ingrediente
                    if (idingr != null)
                    {
                        //Selecciona el máximo registro para aumentar en uno el valor
                        DatosNuevoInventario.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                        DatosNuevoInventario.IdIngrediente = idingr;
                        DatosNuevoInventario.IdEnvase = idenv;
                        DatosNuevoInventario.IdInsumo = idins;
                        DatosNuevoInventario.Cantidad = cantidad;
                        DatosNuevoInventario.CostoTotal = valor;
                        DatosNuevoInventario.Unidades = nrounidades;
                        DatosNuevoInventario.CostoUnidad = costounidades;
                        DatosNuevoInventario.FechaHora = DateTime.Now;
                        DatosNuevoInventario.IdLocal = idloc;
                        DatosNuevoInventario.IdProveedor = (from pv in db.Proveedores where pv.NitProveedor == nitprov select pv.IdProveedor).FirstOrDefault();
                        DatosNuevoInventario.Transaccion = "ENTRA";
                        DatosNuevoInventario.IdPersona = idpers;

                        db.DetalleInventarios.Add(DatosNuevoInventario);

                        db.SaveChanges();



                        //Registra en el libro mayor el asiento contable de la compra
                        //Clase
                        //1 Activo
                        //Grupo
                        //14 Inventarios
                        //Cuenta
                        //1405 Materias primas
                        //Subcuenta
                        //140599 Ajustes por inflación
                        DatosLMTO RegistroContab = new DatosLMTO();

                        //Debito de compra de materia prima
                        RegistroContab.IdRegistro = 0;
                        RegistroContab.IdClase = "1";
                        RegistroContab.IdGrupo = "14";
                        RegistroContab.IdCuenta = "1405";
                        RegistroContab.IdSubCuenta = "140599";
                        RegistroContab.FechaHora = DateTime.Now;
                        RegistroContab.NroDocPersonaDB = "901226468";
                        RegistroContab.ValorDebito = valor;
                        RegistroContab.NroDocPersonaCR = "";
                        RegistroContab.ValorCredito = 0;
                        RegistroContab.Observaciones = "Compra materia prima";

                        CoreContable.RegistrarLibroMayor(RegistroContab);

                        //Crédito de compra de materia prima
                        RegistroContab.IdRegistro = 0; 
                        RegistroContab.IdClase = "1";
                        RegistroContab.IdGrupo = "11";
                        RegistroContab.IdCuenta = "1105";
                        RegistroContab.IdSubCuenta = "110505";
                        RegistroContab.FechaHora = DateTime.Now;
                        RegistroContab.NroDocPersonaDB = "";
                        RegistroContab.ValorDebito = 0;
                        RegistroContab.NroDocPersonaCR = "";
                        RegistroContab.ValorCredito = valor;
                        RegistroContab.Observaciones = "Compra materia prima";

                        CoreContable.RegistrarLibroMayor(RegistroContab);

                    }

                    //Se va a registrar la compra de un envase

                    //Se va a registrar la compra de un insumo
                }
            }
            catch
            {
                return false;
            }

            //El inventario se registra en la subcuenta 621099 y luego en 140599
            return true;
        }

        //Retira del inventario el elemento solicitado
        public bool RetirarInventario(long? idingr, long? idenv, long? idins, long cantidad, double valor, long nrounidades, double costounidades, long idloc, string nitprov, long idpers, long TiempoElab, long idmed, long idgrresi)
        {
            long lTipoIngr;
            var CoreContable = new BegoSys.Core.Contabilidad.AccountingRepository();
            try
            {
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    DetalleInventario DatosRetiroInventario = new DetalleInventario();

                    //Se elimina un ingrediente del inventario porque fue procesado o fue vendido
                    if (idingr != null)
                    {
                        lTipoIngr = (from iingr in db.Ingredientes where iingr.IdTipoIngrediente == idingr select iingr.IdTipoIngrediente).FirstOrDefault();

                            //Selecciona el máximo registro para aumentar en uno el valor
                            DatosRetiroInventario.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                            DatosRetiroInventario.FechaHora = DateTime.Now;
                            DatosRetiroInventario.Transaccion = "SALE";
                            DatosRetiroInventario.Cantidad = cantidad;
                            DatosRetiroInventario.CostoTotal = valor;
                            DatosRetiroInventario.Unidades = nrounidades;
                            DatosRetiroInventario.CostoUnidad = costounidades;
                            DatosRetiroInventario.IdMedida = idmed;
                            DatosRetiroInventario.IdIngrediente = idingr;
                            DatosRetiroInventario.IdEnvase = idenv;
                            DatosRetiroInventario.IdInsumo = idins;
                            DatosRetiroInventario.IdLocal = idloc;
                            DatosRetiroInventario.IdProveedor = (from pv in db.Proveedores where pv.NitProveedor == nitprov select pv.IdProveedor).FirstOrDefault();
                            DatosRetiroInventario.IdPersona = idpers;
                            DatosRetiroInventario.TiempoProduccion = TiempoElab;

                            //No se borran registros de la tabla siempre se adicionan
                            db.DetalleInventarios.Add(DatosRetiroInventario);

                        //Si el tipo de ingrediente es una fruta se elimina y se generan residuos organicos
                        //Si el tipo de ingrediente es otro se elimina solamente y no se generan residuos organicos
                        if (lTipoIngr == 1)
                        {
                            //Se generan registros de Entrada y de salida de residuos solidos organicos
                            DatosRetiroInventario.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                            DatosRetiroInventario.FechaHora = DateTime.Now;
                            DatosRetiroInventario.Transaccion = "ENTRA";
                            DatosRetiroInventario.Cantidad = idgrresi; //Variable que contiene los gramos que pesan los residuos solidos esto son perdidas para la empresa 
                            DatosRetiroInventario.Unidades = idgrresi;
                            DatosRetiroInventario.CostoTotal = 0; //Se calcula en el proceso en las noches Tiempo en segundos de elaboracion por salario en segundos
                            DatosRetiroInventario.CostoUnidad = 0;
                            DatosRetiroInventario.IdMedida = idmed;
                            DatosRetiroInventario.IdIngrediente = 9999999998;
                            DatosRetiroInventario.IdEnvase = idenv;
                            DatosRetiroInventario.IdInsumo = idins;
                            DatosRetiroInventario.IdLocal = idloc;
                            DatosRetiroInventario.IdProveedor = (from pv in db.Proveedores where pv.NitProveedor == nitprov select pv.IdProveedor).FirstOrDefault();
                            DatosRetiroInventario.IdPersona = idpers;
                            DatosRetiroInventario.TiempoProduccion = TiempoElab;

                            db.DetalleInventarios.Add(DatosRetiroInventario);

                            DatosRetiroInventario.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                            DatosRetiroInventario.FechaHora = DateTime.Now;
                            DatosRetiroInventario.Transaccion = "SALE";
                            DatosRetiroInventario.Cantidad = idgrresi; //Variable que contiene los gramos que pesan los residuos solidos esto son perdidas para la empresa
                            DatosRetiroInventario.CostoTotal = 0; //Se calcula en el proceso en las noches Tiempo en segundos de elaboracion por salario en segundos
                            DatosRetiroInventario.Unidades = idgrresi;
                            DatosRetiroInventario.CostoUnidad = 0;
                            DatosRetiroInventario.IdMedida = idmed;
                            DatosRetiroInventario.IdIngrediente = 9999999998;
                            DatosRetiroInventario.IdEnvase = idenv;
                            DatosRetiroInventario.IdInsumo = idins;
                            DatosRetiroInventario.IdLocal = idloc;
                            DatosRetiroInventario.IdProveedor = (from pv in db.Proveedores where pv.NitProveedor == nitprov select pv.IdProveedor).FirstOrDefault();
                            DatosRetiroInventario.IdPersona = idpers;
                            DatosRetiroInventario.TiempoProduccion = null;

                            db.DetalleInventarios.Add(DatosRetiroInventario);

                            db.SaveChanges();
                        }


                        //Registra en el libro mayor el asiento contable del cambio de materia prima a producto en proceso
                        DatosLMTO RegistroContab = new DatosLMTO();
                        //Debito a la 143020 en subproductos para ser vendidos
                        RegistroContab.IdRegistro = 0; 
                        RegistroContab.IdClase = "1";
                        RegistroContab.IdGrupo = "14";
                        RegistroContab.IdCuenta = "1430";
                        RegistroContab.IdSubCuenta = "143020";
                        RegistroContab.FechaHora = DateTime.Now;
                        RegistroContab.NroDocPersonaDB = "901226468";
                        RegistroContab.ValorDebito = valor;
                        RegistroContab.NroDocPersonaCR = "";
                        RegistroContab.ValorCredito = 0;
                        RegistroContab.Observaciones = "Elaboración pulpa";

                        CoreContable.RegistrarLibroMayor(RegistroContab);

                        //Credito a la 1405 materia prima
                        RegistroContab.IdRegistro = 0; 
                        RegistroContab.IdClase = "1";
                        RegistroContab.IdGrupo = "14";
                        RegistroContab.IdCuenta = "1405";
                        RegistroContab.IdSubCuenta = "140599";
                        RegistroContab.FechaHora = DateTime.Now;
                        RegistroContab.NroDocPersonaDB = "";
                        RegistroContab.ValorDebito = 0;
                        RegistroContab.NroDocPersonaCR = "";
                        RegistroContab.ValorCredito = valor;
                        RegistroContab.Observaciones = "Uso materia prima";

                        CoreContable.RegistrarLibroMayor(RegistroContab);


                    }

                    //Se va a registrar la compra de un envase

                    //Se va a registrar la compra de un insumo
                }
            }
            catch
            {
                return false;
            }

            //El inventario se registra en la subcuenta 621099 y luego en 140599
            return true;
        }


        //Consulta los datos de los ingredientes e insumos para hacer inventario
        public List<InventarioTO> ConsultarInventarioPulpas(string sDoc, DatosLocalTO dLoc)
        {
            List<InventarioTO> ListaPulp = null;
            //Consulta los ingredientes
            try
            {
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    //Consulta a cuales frutas se les puede hacer pulpa que tengan inventario en el local actual
                    ListaPulp = (from ingr in db.Ingredientes
                                 join inv in db.Inventarios on ingr.IngredienteOrigen equals inv.IdIngrediente
                                 join med in db.Medidas on ingr.IdMedida equals med.idMedida
                                where (ingr.IdTipoIngrediente == 2 && inv.CantidadActual > 0 && inv.IdLocal == dLoc.IdLocal)
                    select new InventarioTO
                    {
                        IdIngrediente = ingr.IdIngrediente,
                        NombreIngrediente = ingr.NombreIngrediente,
                        NombreMedida = med.nombreMedida,
                        Cantidad = inv.CantidadActual
                    }).ToList();
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine("Se presentó el siguiente error: " + Error.Message + Error.InnerException.Message);
            }
            return ListaPulp;
            //Consulta los insumos de la tabla jbInsumos
        }

        //Consulta el ingrediente origen de la pulpa enviada como parametro
        public long BuscarIngredienteOrigen(long idPulpa)
        {
            long lidIngr = 0;
            using (var db = EntidadesJuicebar.GetDbContext())
            {
                lidIngr = (from ing in db.Ingredientes where ing.IdIngrediente == idPulpa select ing.IngredienteOrigen).FirstOrDefault();
            }
            return lidIngr;
        }

        //Retirar el producto del inventario
        public bool RetirarProducto(long idProducto, long idLocal, long idPersona)
        {
            List<MedidasReceta> DatIngrR = new List<MedidasReceta>();
            var CoreContable = new BegoSys.Core.Contabilidad.AccountingRepository();
            try
            {
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    
                    AccountingRepository CoreContabilidad = new AccountingRepository();

                    //Consulta las medidas de los ingredientes del producto, que se utilizarán luego para retirar del inventario
                    DatIngrR = (from mr in db.MedidasRecetas
                       join pr in db.Productos on mr.IdProducto equals pr.IdProducto
                      where mr.IdProducto == idProducto
                     select mr).ToList();

                    foreach (MedidasReceta Elem in DatIngrR)
                    {

                        DetalleInventario ProductoaRetirar = new DetalleInventario();
                        ProductoaRetirar.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                        ProductoaRetirar.FechaHora = DateTime.Now;
                        ProductoaRetirar.Transaccion = "SALE";
                        ProductoaRetirar.Cantidad = Elem.Cantidad;
                        ProductoaRetirar.CostoTotal = 0; //Se calcula en el proceso en las noches Tiempo en segundos de elaboracion por salario en segundos
                        ProductoaRetirar.Unidades = Elem.Cantidad;
                        ProductoaRetirar.CostoUnidad = 0;
                        ProductoaRetirar.IdMedida = Elem.IdMedida;
                        ProductoaRetirar.IdIngrediente = Elem.IdIngrediente;
                        ProductoaRetirar.IdEnvase = Elem.IdEnvase;
                        ProductoaRetirar.IdInsumo = null;
                        ProductoaRetirar.IdLocal = idLocal;
                        ProductoaRetirar.IdProveedor = null;
                        ProductoaRetirar.IdPersona = idPersona;
                        ProductoaRetirar.TiempoProduccion = null;

                        db.DetalleInventarios.Add(ProductoaRetirar);

                        db.SaveChanges();

                        //Calcular Costos

                        //Contabiliza la venta y los costos de la venta

                        DatosLMTO RegistroContab = new DatosLMTO();

                        //Crédito a la cuenta 143020 que pasa de subproducto terminado a producto vendido
                        RegistroContab.IdRegistro = 0;
                        RegistroContab.IdClase = "1";
                        RegistroContab.IdGrupo = "14";
                        RegistroContab.IdCuenta = "1430";
                        RegistroContab.IdSubCuenta = "143020";
                        RegistroContab.FechaHora = DateTime.Now;
                        RegistroContab.NroDocPersonaDB = "";
                        RegistroContab.ValorDebito = 0;
                        RegistroContab.NroDocPersonaCR = "";
                        RegistroContab.ValorCredito = 1;
                        RegistroContab.Observaciones = "Pulpa vendida";

                        CoreContable.RegistrarLibroMayor(RegistroContab);

                        //Debito de la venta
                        //Subproducto que se va para la venta
                        RegistroContab.IdRegistro = 0; //Se pone cero porque RegistrarLibroMayor asigna el valor siguiente
                        RegistroContab.IdClase = "4";
                        RegistroContab.IdGrupo = "41";
                        RegistroContab.IdCuenta = "4140";
                        RegistroContab.IdSubCuenta = "414015";
                        RegistroContab.FechaHora = DateTime.Now;
                        RegistroContab.NroDocPersonaDB = "901226468";
                        RegistroContab.ValorDebito = 1;
                        RegistroContab.NroDocPersonaCR = "";
                        RegistroContab.ValorCredito = 0;
                        RegistroContab.Observaciones = "Venta de subproductos";

                        CoreContable.RegistrarLibroMayor(RegistroContab);

                        ////Crédito de la venta
                        //RegistroContab.IdRegistro = 0;
                        //RegistroContab.IdClase = "1";
                        //RegistroContab.IdGrupo = "11";
                        //RegistroContab.IdCuenta = "1105";
                        //RegistroContab.IdSubCuenta = "110505";
                        //RegistroContab.FechaHora = DateTime.Now;
                        //RegistroContab.NroDocPersonaDB = "";
                        //RegistroContab.ValorDebito = 0;
                        //RegistroContab.NroDocPersonaCR = "";
                        //RegistroContab.ValorCredito = valor;
                        //RegistroContab.Observaciones = "Compra materia prima";


                        //CoreContabilidad.RegistrarLibroMayor(DatosLMTO); 

                    }

                }
            }
            catch
            {
                return false;
            }
            return true;

        }


    }
}
