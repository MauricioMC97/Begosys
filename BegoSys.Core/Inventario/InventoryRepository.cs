using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool RetirarInventario(long? idingr, long? idenv, long? idins, double cantidad, double valor, double? nrounidades, double? costounidades, long idloc, string nitprov, long idpers, long TiempoElab, long idmed)
        {
            long lTipoIngr;
            try
            {
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    DetalleInventario DatosRetiroInventario = new DetalleInventario();

                    //Se elimina un ingrediente del inventario porque fue procesado o fue vendido
                    if (idingr != null)
                    {
                        lTipoIngr = (from iingr in db.Ingredientes where iingr.IdTipoIngrediente == idingr select iingr.IdTipoIngrediente).FirstOrDefault();

                        //Si el tipo de ingrediente es una fruta se elimina y se generan residuos organicos
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

                        db.SaveChanges();


                        //Si el tipo de ingrediente es otro se elimina solamente








                        //Registra en el libro mayor el asiento contable del cambio de materia prima a producto en proceso
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
                        NombreMedida = med.nombreMedida
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
    }
}
