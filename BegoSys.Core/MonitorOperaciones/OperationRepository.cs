using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BegoSys.TO;
using BegoSys.Domain;
using AutoMapper;
using BegoSys.Common.Excepciones;
using BegoSys.Domain.Entidades;
using AutoMapper.QueryableExtensions;
using BegoSys.Common.Auxiliares;
using BegoSys.TO;

namespace BegoSys.Core
{
    public class OperationRepository : IOperationRepository
    {
        //Consulta los datos del establecimiento como moneda, fecha y hora de apertura fecha y hora de cierre
        public DatosLocalTO ConsultarDatosLocal(long lIdenLocal)
        {
            DatosLocalTO DatEstablecimiento;
            List<PersonasLocalTO> DatosPersonas;

            try
            {
                using (var db = EntidadesJuicebar.GetDbContext())
                {
                    try
                    {
                        var Usuario = db.Database.SqlQuery<string>("select user from dual").First();
                        //var Prueba = (from p in db.Personas where p.idPersona == lIdenLocal select (p)).ToList();

                        DatEstablecimiento = (from l in db.Locales
                                              where l.IdLocal == lIdenLocal
                                              select new DatosLocalTO
                                              {
                                                  IdLocal = l.IdLocal,
                                                  NombreLocal = l.NombreLocal,
                                                  IdMoneda = l.IdMoneda,
                                                  HoraAbre = l.HoraAbre,
                                                  HoraCierra = l.HoraCierra,
                                                  HoraIInventario = l.HoraIInventario,
                                                  HoraFInventario = l.HoraFInventario,
                                                  HoraIPulpa = l.HoraIPulpa,
                                                  HoraFPulpa = l.HoraFPulpa,
                                                  HoraIVentas = l.HoraIVentas,
                                                  HoraFVentas = l.HoraFVentas
                                              }).FirstOrDefault();

                        if (DatEstablecimiento != null)
                        {
                            //Busca a las personas que trabajan en el local
                            DatosPersonas = (from p in db.Personas
                                             join pl in db.PersonasLocal on p.idPersona equals pl.idPersona
                                             join cmo in db.CostoManoObra on p.idPersona equals cmo.IdPersona
                                             where pl.idLocal == DatEstablecimiento.IdLocal
                                             select new PersonasLocalTO
                                             {
                                                 idLocalComercial = pl.idLocal,
                                                 idPersona = p.idPersona,
                                                 documento = p.docPersona,
                                                 nombre = p.nombrecompleto,
                                                 SalarioSeg = cmo.SalarioSegundo
                                             }).ToList();

                            DatEstablecimiento.ListaPersonas = DatosPersonas;
                        }
                        
                    }
                    catch (Exception Ex)
                    {
                        db.Database.Log = s => {
                            System.Diagnostics.Debug.WriteLine(s);
                            Console.WriteLine(s);
                            Console.ReadKey();
                        };
                        Console.WriteLine("Error" + Ex.Message);
                        return null;
                    }

                }
                return DatEstablecimiento;
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error" + Ex.Message);

                return null;
            }
        }

        //Verifica que las personas pertenezcan al local desde donde están solicitando ejecutar el proceso
        public bool ValidarPersonaProceso(long? idp, string doc, DatosLocalTO DatLoc, long idProc)
        {
            bool bPerteneceLocal = false;
            int iPersonaTieneRol = 0;
            bool bEsValidaPersona = false; //Identifica si la persona cumple con el perfil para realizar el proceso

            //Valida si la persona si pertenece al local donde está solicitando el permiso
            foreach(PersonasLocalTO Person in DatLoc.ListaPersonas)
            {
                //Si el documento de la persona es igual al documento ingresado
                if (doc != null)
                {
                    if (Person.documento == doc)
                    {
                        bPerteneceLocal = true;

                        using (var db = EntidadesJuicebar.GetDbContext())
                        {
                            iPersonaTieneRol = (from dr in db.DetalleRoles where dr.IdPersona == Person.idPersona && dr.IdProceso == idProc select dr).Count();

                            if (iPersonaTieneRol > 0)
                                bEsValidaPersona = true;
                            
                        }

                    }
                }
                if (idp != null)
                {
                    if (Person.idPersona == idp)
                    {
                        bPerteneceLocal = true;

                        using (var db = EntidadesJuicebar.GetDbContext())
                        {
                            bEsValidaPersona = true;
                            //(from dr in db.DetalleRoles where dr.IdPersona == Persona.idpersona).
                        }
                    }
                }
            }

            if (!bPerteneceLocal)
            {
                bEsValidaPersona = false;
                Console.WriteLine("Error la persona con cédula " + doc + "no pertenece al local " + DatLoc.NombreLocal);
            }
            return bEsValidaPersona;
        }

        public DatosLocalTO BuscarRoles(DatosLocalTO Lista, long idProc)
        {
            using (var db = EntidadesJuicebar.GetDbContext())
            {
                foreach (PersonasLocalTO Persona in Lista.ListaPersonas)
                {
                    Persona.idRol = (from dr in db.DetalleRoles where dr.IdPersona == Persona.idPersona && dr.IdProceso == idProc select dr.IdRol).FirstOrDefault();
                }
            }
            return Lista;
        }

        //Verifica si hay empleados disponibles de acuerdo al rol y al local solicitados
        public bool HayEmpleadosDisponibles(long idRol, long idLoc)
        {
            bool bHayDisponibles = false;
            using (var db = EntidadesJuicebar.GetDbContext())
            {
                return bHayDisponibles;
            }
        }


        public long ConsultarMedidasenReceta(long id)
        {
            long lMedida = 0;

            using (var db = EntidadesJuicebar.GetDbContext())
            {
                //Se consulta la medida en el vaso de 16 onzas (codigo 1) que es el mas pequeño que se va a usar
                lMedida = (from mr in db.MedidasRecetas where (mr.IdIngrediente == id && mr.IdEnvase == 1) select mr.Cantidad).FirstOrDefault();
            }

            return lMedida;
        }

        public void AtenderPedidos()
        {
            return;
        }
    }
}
