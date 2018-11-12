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

        public DatosLocalTO ConsultarDatosLocal(long lIdenLocal)
        {
            DatosLocalTO DatEstablecimiento;
            PersonasLocalTO DatosPersonas;

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
                                                  idMoneda = l.IdMoneda,
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
                            DatosPersonas = (from p in db.Personas
                                             join pl in db.PersonasLocal on p.idPersona equals pl.idPersona
                                             where pl.idLocal == DatEstablecimiento.IdLocal
                                             select new PersonasLocalTO
                                             {
                                                 idLocalComercial = pl.idLocal,
                                                 idPersona = p.idPersona,
                                                 documento = p.docPersona,
                                                 nombre = p.nombrecompleto
                                             }).FirstOrDefault();

                            DatEstablecimiento.ListaPersonas.Add(DatosPersonas);
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


        public void RealizarInventario ()
        {
            return;
        }

        public void ElaborarPulpa()
        {
            return;
        }

        public void AtenderPedidos()
        {
            return;
        }
    }
}
