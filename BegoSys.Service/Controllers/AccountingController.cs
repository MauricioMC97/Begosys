using BegoSys.Common.Constantes;
using BegoSys.Core.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace BegoSys.Service.Controllers
{
    public class AccountingController : ApiController
    {
        private IAccountingRepository _repository;

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSys.Service.Controllers.OperationController"/>
        /// </summary>
        /// <param name="repository">Instancia del repositorio inyectada a través de spring.</param>
        public AccountingController(IAccountingRepository repository)
        {
            _repository = repository;
        }

        ///<summary>
        ///Registra las transacciones contables en el libro mayor
        ///</summary>
        /// <summary>
        /// Obtiene la información del local y de las personas que trabajan en el.
        /// </summary>
        /// <param name="idLocal"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ResponseType(typeof(DatosLibroMayorTO))]
        //[Route(ConstantesApi.GuardarenLibroMayor)]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        //public IHttpActionResult GuardarLibroMayor(DatosLibroMayorTO DatosLM)
        //{
        //    bool RegistroOK = _repository.RegistrarLibroMayor(DatosLM);

        //    if (RegistroOK)
        //    {
        //        return this.Ok(infoLocal);
        //    }
        //    else
        //    {
        //        return this.NotFound();
        //    }
        //}
    }
}