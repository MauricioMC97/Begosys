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
        public bool AdicionarInventario(long? idingr, long? idenv, long? idins, long cantidad, long valor, long? nrounidades, long? costounidades, long idloc, long? idprov)
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
                        DatosNuevoInventario.IdRegistroDetInv = db.DetalleInventarios.Max(x => x.IdRegistroDetInv) + 1;
                        DatosNuevoInventario.IdIngrediente = idingr;
                        DatosNuevoInventario.IdEnvase = idenv;
                        DatosNuevoInventario.IdInsumo = idins;
                        DatosNuevoInventario.Cantidad = cantidad;
                        DatosNuevoInventario.CostoMedida = valor;
                        DatosNuevoInventario.Unidades = nrounidades;
                        DatosNuevoInventario.CostoUnidad = costounidades;
                        DatosNuevoInventario.FechaHora = DateTime.Now;
                        DatosNuevoInventario.IdLocal = idloc;
                        DatosNuevoInventario.IdProveedor = 11; // idprov;
                        DatosNuevoInventario.Transaccion = "ENTRA";

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

        public void RetirarInventario()
        {

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

    }
}
