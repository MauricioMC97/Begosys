using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BegoSys.Common.Excepciones;
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
                                where (ing.IdTipoIngrediente == 1 || ing.IdTipoIngrediente == 3 || ing.IdTipoIngrediente == 5 || ing.IdTipoIngrediente == 6 || ing.IdTipoIngrediente == 7) && ing.Inventario == 1
                                select new InventarioTO
                                {
                                    IdIngrediente = ing.IdIngrediente,
                                    NombreIngrediente = ing.NombreIngrediente,
                                    IdMedida = ing.IdMedida,
                                    NombreMedida = med.nombreMedida,
                                    IdTipoIngrediente = ing.IdTipoIngrediente
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
        public bool AdicionarInventario(long? idingr, long? idenv, long? idins, double cantidad, double valor, double? nrounidades, double? costounidades, long idMed, long idloc, string nitprov, long idpers)
        {
            try
            {
                var CoreContable = new BegoSys.Core.Contabilidad.AccountingRepository();
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    //Actualizando el encabezado del inventario
                    var UpdEncabezado = (from Inv in db.Inventarios where Inv.IdIngrediente == idingr && Inv.IdLocal == idloc select Inv).FirstOrDefault();

                    if (UpdEncabezado != null) {
                        UpdEncabezado.CantidadActual = (long)(UpdEncabezado.CantidadActual + cantidad);
                        UpdEncabezado.CantidadMinima = (long)(UpdEncabezado.CantidadActual * 0.2); //La cantidad mínima es el 30% de la cantidad actual
                        UpdEncabezado.CostoPromDiaActual = valor / (nrounidades ?? 1);
                        UpdEncabezado.CostoPromDiaUnidad = (UpdEncabezado.CostoPromDiaActual + (valor / (nrounidades ?? 1))) / 2;
                        UpdEncabezado.UnidadesActuales = (long)(UpdEncabezado.UnidadesActuales + (nrounidades ?? 1));
                        UpdEncabezado.UnidadesMinimas = (long)(UpdEncabezado.UnidadesActuales * 0.2);
                    }

                    DetalleInventario DatosNuevoInventario = new DetalleInventario();

                    //Selecciona el máximo registro para aumentar en uno el valor
                    DatosNuevoInventario.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                    DatosNuevoInventario.IdIngrediente = idingr;
                    DatosNuevoInventario.IdEnvase = idenv;
                    DatosNuevoInventario.IdInsumo = idins;
                    DatosNuevoInventario.Cantidad = cantidad;
                    DatosNuevoInventario.CostoTotal = valor;
                    DatosNuevoInventario.Unidades = nrounidades;
                    DatosNuevoInventario.CostoUnidad = costounidades;
                    DatosNuevoInventario.IdMedida = idMed;
                    DatosNuevoInventario.FechaHora = DateTime.Now;
                    DatosNuevoInventario.IdLocal = idloc;
                    DatosNuevoInventario.IdProveedor = (from pv in db.Proveedores where pv.NitProveedor == nitprov select pv.IdProveedor).FirstOrDefault();
                    DatosNuevoInventario.Transaccion = "ENTRA";
                    DatosNuevoInventario.IdPersona = idpers;
                    DatosNuevoInventario.ConExistencias = 1; //Cuando llega nuevo hay existencias del producto

                    db.DetalleInventarios.Add(DatosNuevoInventario);

                    db.SaveChanges();


                    //Se va a registrar la compra de un ingrediente
                    if (idingr != null)
                    {

                        //Registra en el libro mayor el asiento contable de la compra de materia prima
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
                        RegistroContab.Observaciones = "Compra materia prima " + (db.Ingredientes.Where(ingr => ingr.IdIngrediente == idingr).Select(dni => dni.NombreIngrediente)).FirstOrDefault(); ;

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
                        RegistroContab.NroDocPersonaCR = nitprov;
                        RegistroContab.ValorCredito = valor;
                        RegistroContab.Observaciones = "Compra materia prima " + (db.Ingredientes.Where(ingr => ingr.IdIngrediente == idingr).Select(dni => dni.NombreIngrediente)).FirstOrDefault(); ;

                        CoreContable.RegistrarLibroMayor(RegistroContab);

                    }

                    //Se va a registrar la compra de un envase
                    if (idenv != null)
                    {
                        //Se hace el registro contable de la compra de los envases
                        DatosLMTO RegistroContab = new DatosLMTO();

                        //Debito de compra de envases
                        RegistroContab.IdRegistro = 0;
                        RegistroContab.IdClase = "1";
                        RegistroContab.IdGrupo = "14";
                        RegistroContab.IdCuenta = "1460";
                        RegistroContab.IdSubCuenta = "146099";
                        RegistroContab.FechaHora = DateTime.Now;
                        RegistroContab.NroDocPersonaDB = "901226468";
                        RegistroContab.ValorDebito = valor;
                        RegistroContab.NroDocPersonaCR = "";
                        RegistroContab.ValorCredito = 0;
                        RegistroContab.Observaciones = "Compra envases " + (db.Envases.Where(env => env.IdEnvase == idenv).Select(dne => dne.NombreEnvase)).FirstOrDefault(); 

                        CoreContable.RegistrarLibroMayor(RegistroContab);

                        //Crédito de compra de envases
                        RegistroContab.IdRegistro = 0;
                        RegistroContab.IdClase = "1";
                        RegistroContab.IdGrupo = "11";
                        RegistroContab.IdCuenta = "1105";
                        RegistroContab.IdSubCuenta = "110505";
                        RegistroContab.FechaHora = DateTime.Now;
                        RegistroContab.NroDocPersonaDB = "";
                        RegistroContab.ValorDebito = 0;
                        RegistroContab.NroDocPersonaCR = nitprov;
                        RegistroContab.ValorCredito = valor;
                        RegistroContab.Observaciones = "Compra envases " + (db.Envases.Where(env => env.IdEnvase == idenv).Select(dne => dne.NombreEnvase)).FirstOrDefault();

                        CoreContable.RegistrarLibroMayor(RegistroContab);
                    }

                    //Se va a registrar la compra de un insumo
                    if (idins != null)
                    {
                        //Se hace el registro contable de la compra de los insumos
                    }
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
                    //Actualizando el encabezado del inventario
                    var UpdEncabezado = (from Inv in db.Inventarios where Inv.IdIngrediente == idingr && Inv.IdLocal == idloc select Inv).FirstOrDefault();

                    if (UpdEncabezado != null) {
                        UpdEncabezado.CantidadActual = (long)(UpdEncabezado.CantidadActual - cantidad);
                        UpdEncabezado.CantidadMinima = (long)(UpdEncabezado.CantidadActual * 0.3); //La cantidad mínima es el 30% de la cantidad actual
                        UpdEncabezado.CostoPromDiaActual = (UpdEncabezado.CantidadActual / valor);
                        UpdEncabezado.CostoPromDiaUnidad = (UpdEncabezado.CantidadActual / nrounidades);
                        UpdEncabezado.UnidadesActuales = (long)(UpdEncabezado.UnidadesActuales - nrounidades);
                        UpdEncabezado.UnidadesMinimas = (long)(UpdEncabezado.UnidadesActuales * 0.3);
                    }

                    DetalleInventario DatosRetiroInventario = new DetalleInventario();

                    //Se elimina un ingrediente del inventario porque fue procesado o fue vendido
                    if (idingr != null)
                    {
                        lTipoIngr = (from iingr in db.Ingredientes where iingr.IdIngrediente == idingr select iingr.IdTipoIngrediente).FirstOrDefault();

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
                            DatosRetiroInventario.ConExistencias = 0;

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
                            DatosRetiroInventario.ConExistencias = 1;

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
                            DatosRetiroInventario.ConExistencias = 0;

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
                        RegistroContab.Observaciones = "Uso materia prima en elaboración de pulpa";

                        CoreContable.RegistrarLibroMayor(RegistroContab);


                    }

                    //Se va a registrar el retiro de inventario de un envase

                    //Se va a registrar el retiro de inventario de un insumo
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

        public long ConvertirBolsasaGramos(long idIngrediente, long iCantBolsas, long? idProducto)
        {
            //Convierte la cantidad de bolsas en gramos
            MedidasReceta dMedidas = new MedidasReceta();
            using (var db = EntidadesJuicebar.GetDbContext())
            {
                if (idProducto != null)
                {
                     dMedidas = (from medr in db.MedidasRecetas where medr.IdIngrediente == idIngrediente && medr.IdProducto == idProducto select medr).OrderBy(x => x.Cantidad).FirstOrDefault();
                }
                else
                {
                    //Trae la menor cantidad porque de ese tamaño se deben empacar las bolsas
                    dMedidas = (from medr in db.MedidasRecetas where medr.IdIngrediente == idIngrediente select medr).OrderBy(x => x.Cantidad).FirstOrDefault();
                }
                return dMedidas.Cantidad;
            }
        }


        //Trasladar ingrediente de un local a otro
        public bool TrasladarIngrediente(TrasladoIngrTO dDatos) // long idProducto, long iCantidad, DatosLocalTO dLocalOrigen, long idLocalDestino, DateTime dFecha)
        {
            BegoSys.Core.Contabilidad.AccountingRepository CoreContable = new BegoSys.Core.Contabilidad.AccountingRepository();
            try
            {
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    //Se verifica si el ingrediente existe en el local origen
                    var UpdEncOrigen = (from Inv in db.Inventarios where Inv.IdIngrediente == dDatos.idIngrediente && Inv.IdLocal == dDatos.dDatosLocalOrigen.IdLocal select Inv).FirstOrDefault();

                    var UpdEncDestino = (from Inv in db.Inventarios where Inv.IdIngrediente == dDatos.idIngrediente && Inv.IdLocal == dDatos.idLocalDestino select Inv).FirstOrDefault();

                    if (UpdEncOrigen != null)
                    {
                        //Retira Encabezado local origen
                        UpdEncOrigen.CantidadActual = (long)(UpdEncOrigen.CantidadActual - dDatos.iCantidadGramos);
                        UpdEncOrigen.UnidadesActuales = (long)(UpdEncOrigen.UnidadesActuales - dDatos.iCantidadGramos);

                        //Consulta los datos del ingrediente en el local origen
                        DetalleInventario DatosIngrLO = (from dp in db.DetalleInventarios where dp.IdIngrediente == dDatos.idIngrediente && dp.IdLocal == dDatos.dDatosLocalOrigen.IdLocal select dp).FirstOrDefault();

                        //Registra salida del local origen en el detalle
                        DetalleInventario RetirarOrigen = new DetalleInventario();
                        RetirarOrigen.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                        RetirarOrigen.FechaHora = dDatos.dFEcha;
                        RetirarOrigen.Transaccion = "SALE";
                        RetirarOrigen.Cantidad = dDatos.iCantidadGramos ?? 0;
                        RetirarOrigen.CostoTotal = DatosIngrLO.CostoTotal;
                        RetirarOrigen.Unidades = dDatos.iCantidadGramos;
                        RetirarOrigen.CostoUnidad = DatosIngrLO.CostoUnidad;
                        RetirarOrigen.IdMedida = DatosIngrLO.IdMedida;
                        RetirarOrigen.IdIngrediente = dDatos.idIngrediente;
                        RetirarOrigen.IdEnvase = null;
                        RetirarOrigen.IdInsumo = null;
                        RetirarOrigen.IdLocal = dDatos.dDatosLocalOrigen.IdLocal;
                        RetirarOrigen.IdProveedor = null;
                        RetirarOrigen.IdPersona = DatosIngrLO.IdPersona;
                        RetirarOrigen.TiempoProduccion = DatosIngrLO.TiempoProduccion;
                        RetirarOrigen.ConExistencias = 0;

                        //Adiciona Encabezado local destino
                        if (UpdEncDestino != null)
                        {
                            UpdEncDestino.CantidadActual = (long)(UpdEncDestino.CantidadActual + dDatos.iCantidadGramos);
                            UpdEncDestino.UnidadesActuales = (long)(UpdEncDestino.UnidadesActuales + dDatos.iCantidadGramos);
                        }

                        //Registra entrada al local destino en el detalle
                        DetalleInventario AdicionarDestino = new DetalleInventario();
                        //Selecciona el máximo registro para aumentar en uno el valor
                        AdicionarDestino.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                        AdicionarDestino.IdIngrediente = dDatos.idIngrediente;
                        AdicionarDestino.IdEnvase = null;
                        AdicionarDestino.IdInsumo = null;
                        AdicionarDestino.Cantidad = dDatos.iCantidadGramos ?? 0;
                        AdicionarDestino.CostoTotal = DatosIngrLO.CostoTotal;
                        AdicionarDestino.Unidades = dDatos.iCantidadGramos;
                        AdicionarDestino.CostoUnidad = DatosIngrLO.CostoUnidad;
                        AdicionarDestino.IdMedida = DatosIngrLO.IdMedida;
                        AdicionarDestino.FechaHora = dDatos.dFEcha;
                        AdicionarDestino.IdLocal = dDatos.idLocalDestino;
                        AdicionarDestino.IdProveedor = DatosIngrLO.IdProveedor;
                        AdicionarDestino.Transaccion = "ENTRA";
                        AdicionarDestino.IdPersona = DatosIngrLO.IdPersona;
                        AdicionarDestino.ConExistencias = 1; //Cuando llega nuevo hay existencias del producto

                        db.DetalleInventarios.Add(AdicionarDestino);

                        db.SaveChanges();
                    }
                }
            }
            catch 
            {
                return false;
            }
            return true;
        }

        //Adicionar producto al inventario
        public bool AdicionarProducto(long idProducto, long idLocal, long idPersona, DateTime dFecha)
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
                        //Se busca el costo de la materia prima para calcular el costo en el momento de la venta
                        /*select * from jbdetalleinventarios jbdia 
                          where jbdia.idingrediente = Vidingr 
                            and jbdia.conexistencias = Vexist 
                            and jbdia.transaccion = 'ENTRA'
                            and jbdia.fechahora = (select min(jbdib.fechahora) 
                                                     from jbdetalleinventarios jbdib 
                                                    where jbdib.idingrediente = jbdia.idingrediente 
                                                      and jbdib.transaccion = jbdia.transaccion 
                                                      and jbdib.fechahora <= SYSDATE)*/
                        var iCostoIngr = (from cing in db.DetalleInventarios
                                          where (cing.IdIngrediente == Elem.IdIngrediente
                                             && cing.ConExistencias == 1
                                             && cing.Transaccion == "ENTRA"
                                             && cing.FechaHora == (db.DetalleInventarios.Where(dii => dii.FechaHora <= DateTime.Now
                                                                                        && dii.IdIngrediente == cing.IdIngrediente
                                                                                        && dii.IdLocal == cing.IdLocal
                                                                                        && dii.ConExistencias == cing.ConExistencias).Min(diifh => diifh.FechaHora)))
                                          select new { CostoU = cing.CostoUnidad }).FirstOrDefault();

                        //Actualizando el encabezado del inventario
                        var UpdEncabezado = (from Inv in db.Inventarios where Inv.IdIngrediente == Elem.IdIngrediente && Inv.IdLocal == idLocal select Inv).FirstOrDefault();

                        if (UpdEncabezado != null)
                        {
                            UpdEncabezado.CantidadActual = (long)(UpdEncabezado.CantidadActual - Elem.Cantidad);
                            UpdEncabezado.CostoPromDiaActual = (UpdEncabezado.CantidadActual / ((iCostoIngr.CostoU ?? 1) * Elem.Cantidad));
                            UpdEncabezado.UnidadesActuales = (long)(UpdEncabezado.UnidadesActuales - Elem.Cantidad);
                            UpdEncabezado.CostoPromDiaUnidad = (long)(iCostoIngr.CostoU ?? 1);
                        }


                        DetalleInventario ProductoaAdicionar = new DetalleInventario();
                        if (iCostoIngr != null)
                        {
                            ProductoaAdicionar.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                            ProductoaAdicionar.FechaHora = dFecha;
                            ProductoaAdicionar.Transaccion = "ENTRA";
                            ProductoaAdicionar.Cantidad = Elem.Cantidad;
                            ProductoaAdicionar.CostoTotal = (iCostoIngr.CostoU ?? 1) * Elem.Cantidad; //Se calcula en el proceso en las noches Tiempo en segundos de elaboracion por salario en segundos
                            ProductoaAdicionar.Unidades = Elem.Cantidad;
                            ProductoaAdicionar.CostoUnidad = iCostoIngr.CostoU;
                            ProductoaAdicionar.IdMedida = Elem.IdMedida;
                            ProductoaAdicionar.IdIngrediente = Elem.IdIngrediente;
                            ProductoaAdicionar.IdEnvase = Elem.IdEnvase;
                            ProductoaAdicionar.IdInsumo = null;
                            ProductoaAdicionar.IdLocal = idLocal;
                            ProductoaAdicionar.IdProveedor = null;
                            ProductoaAdicionar.IdPersona = idPersona;
                            ProductoaAdicionar.TiempoProduccion = null;
                            ProductoaAdicionar.ConExistencias = 0;
                        }
                        else
                        {
                            ProductoaAdicionar.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                            ProductoaAdicionar.FechaHora = dFecha;
                            ProductoaAdicionar.Transaccion = "ENTRA";
                            ProductoaAdicionar.Cantidad = Elem.Cantidad;
                            ProductoaAdicionar.CostoTotal = 0; //El costo de los ingredientes no inventariados se calcula diferente
                            ProductoaAdicionar.Unidades = Elem.Cantidad;
                            ProductoaAdicionar.CostoUnidad = 0; //El costo de los ingredientes no inventariados se calcula diferente
                            ProductoaAdicionar.IdMedida = Elem.IdMedida;
                            ProductoaAdicionar.IdIngrediente = Elem.IdIngrediente;
                            ProductoaAdicionar.IdEnvase = Elem.IdEnvase;
                            ProductoaAdicionar.IdInsumo = null;
                            ProductoaAdicionar.IdLocal = idLocal;
                            ProductoaAdicionar.IdProveedor = null;
                            ProductoaAdicionar.IdPersona = idPersona;
                            ProductoaAdicionar.TiempoProduccion = null;
                            ProductoaAdicionar.ConExistencias = 0;
                        }

                        db.DetalleInventarios.Add(ProductoaAdicionar);

                        db.SaveChanges();

                        //Calcular Costos

                        //Contabiliza la venta y los costos de la venta

                        DatosLMTO RegistroContab = new DatosLMTO();

                        //Crédito a la cuenta 143020 que pasa de subproducto terminado a producto vendido
                        if (iCostoIngr != null)
                        {
                            RegistroContab.IdRegistro = 0;
                            RegistroContab.IdClase = "1";
                            RegistroContab.IdGrupo = "14";
                            RegistroContab.IdCuenta = "1430";
                            RegistroContab.IdSubCuenta = "143020";
                            RegistroContab.FechaHora = dFecha;
                            RegistroContab.NroDocPersonaDB = "";
                            RegistroContab.ValorDebito = 0;
                            RegistroContab.NroDocPersonaCR = "";
                            RegistroContab.ValorCredito = (iCostoIngr.CostoU ?? 1) * Elem.Cantidad;
                            RegistroContab.Observaciones = "Pulpa adicionada";
                        }
                        else
                        {
                            RegistroContab.IdRegistro = 0;
                            RegistroContab.IdClase = "1";
                            RegistroContab.IdGrupo = "14";
                            RegistroContab.IdCuenta = "1430";
                            RegistroContab.IdSubCuenta = "143020";
                            RegistroContab.FechaHora = dFecha;
                            RegistroContab.NroDocPersonaDB = "";
                            RegistroContab.ValorDebito = 0; //El costo de los ingredientes no inventariados se calcula diferente
                            RegistroContab.NroDocPersonaCR = "";
                            RegistroContab.ValorCredito = 0; //El costo de los ingredientes no inventariados se calcula diferente
                            RegistroContab.Observaciones = "Pulpa adicionada";
                        }

                        CoreContable.RegistrarLibroMayor(RegistroContab);

                        //Debito de la venta
                        //Subproducto que se va para la venta
                        if (iCostoIngr != null)
                        {
                            RegistroContab.IdRegistro = 0; //Se pone cero porque RegistrarLibroMayor asigna el valor siguiente
                            RegistroContab.IdClase = "4";
                            RegistroContab.IdGrupo = "41";
                            RegistroContab.IdCuenta = "4140";
                            RegistroContab.IdSubCuenta = "414015";
                            RegistroContab.FechaHora = dFecha;
                            RegistroContab.NroDocPersonaDB = "901226468";
                            RegistroContab.ValorDebito = (iCostoIngr.CostoU ?? 1) * Elem.Cantidad;
                            RegistroContab.NroDocPersonaCR = "";
                            RegistroContab.ValorCredito = 0;
                            RegistroContab.Observaciones = "Ingreso de subproductos";
                        }
                        else
                        {
                            RegistroContab.IdRegistro = 0; //Se pone cero porque RegistrarLibroMayor asigna el valor siguiente
                            RegistroContab.IdClase = "4";
                            RegistroContab.IdGrupo = "41";
                            RegistroContab.IdCuenta = "4140";
                            RegistroContab.IdSubCuenta = "414015";
                            RegistroContab.FechaHora = dFecha;
                            RegistroContab.NroDocPersonaDB = "901226468";
                            RegistroContab.ValorDebito = 0;
                            RegistroContab.NroDocPersonaCR = "";
                            RegistroContab.ValorCredito = 0;
                            RegistroContab.Observaciones = "Ingreso de subproductos";
                        }

                        CoreContable.RegistrarLibroMayor(RegistroContab);

                    }

                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Retirar el producto del inventario
        public async Task RetirarProducto(long idProducto, long idLocal, long idPersona, DateTime dFecha)
        {
            List<MedidasReceta> DatIngrR = new List<MedidasReceta>();
            var CoreContable = new BegoSys.Core.Contabilidad.AccountingRepository();
            try
            {
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    
                    AccountingRepository CoreContabilidad = new AccountingRepository();
                    Domain.Entidades.Inventario UpdEncabezado;

                    //Consulta las medidas de los ingredientes del producto, que se utilizarán luego para retirar del inventario
                    DatIngrR = (from mr in db.MedidasRecetas
                       join pr in db.Productos on mr.IdProducto equals pr.IdProducto
                      where mr.IdProducto == idProducto
                     select mr).ToList();

                    foreach (MedidasReceta Elem in DatIngrR)
                    {
                        //Se busca el costo de la materia prima para calcular el costo en el momento de la venta
                        /*select * from jbdetalleinventarios jbdia 
                          where jbdia.idingrediente = Vidingr 
                            and jbdia.conexistencias = Vexist 
                            and jbdia.transaccion = 'ENTRA'
                            and jbdia.fechahora = (select min(jbdib.fechahora) 
                                                     from jbdetalleinventarios jbdib 
                                                    where jbdib.idingrediente = jbdia.idingrediente 
                                                      and jbdib.transaccion = jbdia.transaccion 
                                                      and jbdib.fechahora <= SYSDATE)*/
                        var iCostoIngr = (from cing in db.DetalleInventarios
                                      where (cing.IdIngrediente == Elem.IdIngrediente
                                         && cing.ConExistencias == 1
                                         && cing.Transaccion == "ENTRA"
                                         && cing.FechaHora == (db.DetalleInventarios.Where(dii => dii.FechaHora <= DateTime.Now
                                                                                    && dii.IdIngrediente == cing.IdIngrediente
                                                                                    && dii.IdLocal == cing.IdLocal
                                                                                    && dii.Transaccion == cing.Transaccion
                                                                                    && dii.ConExistencias == cing.ConExistencias).Min(diifh => diifh.FechaHora)))
                                      select new { CostoU = cing.CostoUnidad }).FirstOrDefault();

                        //Actualizando el encabezado del inventario
                        if ((Elem.IdEnvase.ToString() != null) && ((Elem.IdProducto == 50) || (Elem.IdProducto == 51)))
                        {
                            UpdEncabezado = (from Inv in db.Inventarios where Inv.IdIngrediente == Elem.IdIngrediente && Inv.IdLocal == idLocal && Inv.IdEnvase == Elem.IdEnvase select Inv).FirstOrDefault();
                        }
                        else
                        {
                            UpdEncabezado = (from Inv in db.Inventarios where Inv.IdIngrediente == Elem.IdIngrediente && Inv.IdLocal == idLocal select Inv).FirstOrDefault();
                        }

                        if (UpdEncabezado != null)
                        {
                            UpdEncabezado.CantidadActual = (long)(UpdEncabezado.CantidadActual - Elem.Cantidad);
                            UpdEncabezado.CostoPromDiaActual = (UpdEncabezado.CantidadActual / ((iCostoIngr.CostoU ?? 1) * Elem.Cantidad));
                            UpdEncabezado.UnidadesActuales = (long)(UpdEncabezado.UnidadesActuales - Elem.Cantidad);
                            UpdEncabezado.CostoPromDiaUnidad = (long)(iCostoIngr.CostoU ?? 1);
                        }

                        await db.SaveChangesAsync();

                        DetalleInventario ProductoaRetirar = new DetalleInventario();
                        if (iCostoIngr != null)
                        {
                            ProductoaRetirar.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                            ProductoaRetirar.FechaHora = dFecha;
                            ProductoaRetirar.Transaccion = "SALE";
                            ProductoaRetirar.Cantidad = Elem.Cantidad;
                            ProductoaRetirar.CostoTotal = (iCostoIngr.CostoU ?? 1) * Elem.Cantidad; //Se calcula en el proceso en las noches Tiempo en segundos de elaboracion por salario en segundos
                            ProductoaRetirar.Unidades = Elem.Cantidad;
                            ProductoaRetirar.CostoUnidad = iCostoIngr.CostoU;
                            ProductoaRetirar.IdMedida = Elem.IdMedida;
                            ProductoaRetirar.IdIngrediente = Elem.IdIngrediente;
                            ProductoaRetirar.IdEnvase = Elem.IdEnvase;
                            ProductoaRetirar.IdInsumo = null;
                            ProductoaRetirar.IdLocal = idLocal;
                            ProductoaRetirar.IdProveedor = null;
                            ProductoaRetirar.IdPersona = idPersona;
                            ProductoaRetirar.TiempoProduccion = null;
                            ProductoaRetirar.ConExistencias = 0;
                        }
                        else
                        {
                            ProductoaRetirar.IdRegistroDetInv = ((db.DetalleInventarios.Count() == 0) ? 1 : db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1);
                            ProductoaRetirar.FechaHora = dFecha;
                            ProductoaRetirar.Transaccion = "SALE";
                            ProductoaRetirar.Cantidad = Elem.Cantidad;
                            ProductoaRetirar.CostoTotal = 0; //El costo de los ingredientes no inventariados se calcula diferente
                            ProductoaRetirar.Unidades = Elem.Cantidad;
                            ProductoaRetirar.CostoUnidad = 0; //El costo de los ingredientes no inventariados se calcula diferente
                            ProductoaRetirar.IdMedida = Elem.IdMedida;
                            ProductoaRetirar.IdIngrediente = Elem.IdIngrediente;
                            ProductoaRetirar.IdEnvase = Elem.IdEnvase;
                            ProductoaRetirar.IdInsumo = null;
                            ProductoaRetirar.IdLocal = idLocal;
                            ProductoaRetirar.IdProveedor = null;
                            ProductoaRetirar.IdPersona = idPersona;
                            ProductoaRetirar.TiempoProduccion = null;
                            ProductoaRetirar.ConExistencias = 0;
                        }

                        db.DetalleInventarios.Add(ProductoaRetirar);

                        await db.SaveChangesAsync();

                        //Calcular Costos

                        //Contabiliza la venta y los costos de la venta

                        DatosLMTO RegistroContab = new DatosLMTO();

                        //Crédito a la cuenta 143020 que pasa de subproducto terminado a producto vendido
                        if (iCostoIngr != null)
                        {
                            RegistroContab.IdRegistro = 0;
                            RegistroContab.IdClase = "1";
                            RegistroContab.IdGrupo = "14";
                            RegistroContab.IdCuenta = "1430";
                            RegistroContab.IdSubCuenta = "143020";
                            RegistroContab.FechaHora = dFecha;
                            RegistroContab.NroDocPersonaDB = "";
                            RegistroContab.ValorDebito = 0;
                            RegistroContab.NroDocPersonaCR = "";
                            RegistroContab.ValorCredito = (iCostoIngr.CostoU ?? 1) * Elem.Cantidad;
                            RegistroContab.Observaciones = "Pulpa vendida";
                        }
                        else
                        {
                            RegistroContab.IdRegistro = 0;
                            RegistroContab.IdClase = "1";
                            RegistroContab.IdGrupo = "14";
                            RegistroContab.IdCuenta = "1430";
                            RegistroContab.IdSubCuenta = "143020";
                            RegistroContab.FechaHora = dFecha;
                            RegistroContab.NroDocPersonaDB = "";
                            RegistroContab.ValorDebito = 0; //El costo de los ingredientes no inventariados se calcula diferente
                            RegistroContab.NroDocPersonaCR = "";
                            RegistroContab.ValorCredito = 0; //El costo de los ingredientes no inventariados se calcula diferente
                            RegistroContab.Observaciones = "Pulpa vendida";
                        }

                        CoreContable.RegistrarLibroMayor(RegistroContab);

                        //Debito de la venta
                        //Subproducto que se va para la venta
                        if (iCostoIngr != null)
                        {
                            RegistroContab.IdRegistro = 0; //Se pone cero porque RegistrarLibroMayor asigna el valor siguiente
                            RegistroContab.IdClase = "4";
                            RegistroContab.IdGrupo = "41";
                            RegistroContab.IdCuenta = "4140";
                            RegistroContab.IdSubCuenta = "414015";
                            RegistroContab.FechaHora = dFecha;
                            RegistroContab.NroDocPersonaDB = "901226468";
                            RegistroContab.ValorDebito = (iCostoIngr.CostoU ?? 1) * Elem.Cantidad;
                            RegistroContab.NroDocPersonaCR = "";
                            RegistroContab.ValorCredito = 0;
                            RegistroContab.Observaciones = "Venta de subproductos";
                        }
                        else
                        {
                            RegistroContab.IdRegistro = 0; //Se pone cero porque RegistrarLibroMayor asigna el valor siguiente
                            RegistroContab.IdClase = "4";
                            RegistroContab.IdGrupo = "41";
                            RegistroContab.IdCuenta = "4140";
                            RegistroContab.IdSubCuenta = "414015";
                            RegistroContab.FechaHora = dFecha;
                            RegistroContab.NroDocPersonaDB = "901226468";
                            RegistroContab.ValorDebito = 0;
                            RegistroContab.NroDocPersonaCR = "";
                            RegistroContab.ValorCredito = 0;
                            RegistroContab.Observaciones = "Venta de subproductos";
                        }

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
            catch (Exception Error)
            {
                Console.WriteLine("Se presentó el siguiente error: " + Error.Message + Error.InnerException.Message);
                throw new BegoSysException("RetirarProducto: Se presentó el siguiente error al retirar el producto: " + Error.Message + Error.InnerException.Message);
            }
        }

        //Consulta el inventario del ingrediente enviado
        public List<InventarioTO> ConsultarDatosInventario(long IdIngr, DatosLocalTO dLoc)
        {
            List<InventarioTO> ListaDatosI = null;
            //Consulta el inventario del ingrediente ordenado por la existenia más vieja
            try
            {
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    //Consulta el inventario del ingrediente enviado que tengan inventario en el local actual
                    //Consulta la entrada mas antigua que todavía tiene existencia
                    ListaDatosI = (from di in db.DetalleInventarios
                                   join inv in db.Inventarios on di.IdIngrediente equals inv.IdIngrediente
                                   where (inv.IdIngrediente == IdIngr
                                       && inv.IdLocal == dLoc.IdLocal
                                       && di.ConExistencias == 1
                                       && di.FechaHora == (db.DetalleInventarios.Where(dii => dii.FechaHora <= DateTime.Now 
                                                                                    && dii.IdIngrediente == di.IdIngrediente 
                                                                                    && dii.IdLocal == di.IdLocal
                                                                                    && dii.ConExistencias == di.ConExistencias).Min(diifh => diifh.FechaHora)))
                                     select new InventarioTO
                                     {
                                         IdIngrediente = inv.IdIngrediente ?? 0,
                                         NombreMedida = null,
                                         Cantidad = di.Cantidad,
                                         CostoTotal = di.CostoTotal
                                     }).ToList();
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine("Se presentó el siguiente error: " + Error.Message + Error.InnerException.Message);
            }
            return ListaDatosI;
            //Consulta los insumos de la tabla jbInsumos
        }

        public List<InventarioInsumosTo> ConsultarInventarioInsumos(long lInsu, DatosLocalTO datosLocal)
        {
            List<InventarioInsumosTo> ListaDatosI = null;
            //Consulta el inventario del ingrediente ordenado por la existenia más vieja
            try
            {
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    //Consulta el inventario del insumo enviado que tengan inventario en el local actual
                    //Consulta la entrada mas antigua que todavía tiene existencia
                    ListaDatosI = (from di in db.DetalleInventarios
                                   join inv in db.Inventarios on di.IdInsumo equals inv.IdInsumo
                                   where (inv.IdInsumo == lInsu
                                       && inv.IdLocal == datosLocal.IdLocal
                                       && di.ConExistencias == 1
                                       && di.FechaHora == (db.DetalleInventarios.Where(dii => dii.FechaHora <= DateTime.Now
                                                                                    && dii.IdInsumo == di.IdInsumo
                                                                                    && dii.IdLocal == di.IdLocal
                                                                                    && dii.ConExistencias == di.ConExistencias).Min(diifh => diifh.FechaHora)))
                                   select new InventarioInsumosTo
                                   {
                                       idInsumo = inv.IdInsumo ?? 0,
                                       nombreInsumo = null,
                                       Cantidad = di.Cantidad,
                                       CostoTotal = di.CostoTotal
                                   }).ToList();
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine("Se presentó el siguiente error: " + Error.Message + Error.InnerException.Message);
            }
            return ListaDatosI;
        }

    }
}
