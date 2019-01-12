using BegoSys.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BegoSys.Domain;
using BegoSys.Domain.Entidades;

namespace BegoSys.Core.Contabilidad
{
    public class AccountingRepository: IAccountingRepository
    {

        public bool RegistrarLibroMayor(DatosLMTO DLM)
        {
            
            using (var db = EntidadesJuicebar.GetDbContext())
            {
                LibroMayor DatosLibroM = new LibroMayor();

                DatosLibroM.IdRegistro = ((db.LibroMayor.Count() == 0) ? 1 : db.LibroMayor.Max(x => x.IdRegistro) + 1);
                DatosLibroM.IdClase = DLM.IdClase;
                DatosLibroM.IdGrupo = DLM.IdGrupo;
                DatosLibroM.IdCuenta = DLM.IdCuenta;
                DatosLibroM.IdSubCuenta = DLM.IdSubCuenta;
                DatosLibroM.FechaHora = DateTime.Now;
                DatosLibroM.ValorDebito = DLM.ValorDebito;
                DatosLibroM.NroDocPersonaDB = DLM.NroDocPersonaDB;
                DatosLibroM.ValorCredito = DLM.ValorCredito;
                DatosLibroM.NroDocPersonaCR = DLM.NroDocPersonaCR;
                DatosLibroM.Observaciones = DLM.Observaciones;

                db.LibroMayor.Add(DatosLibroM);
                db.SaveChanges();
            }

            return true;
        }
    }
}
