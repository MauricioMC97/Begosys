using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BegoSys.Core;
using BegoSys.TO;
using System.Web.Http.Description;
using System.Web.Http;
using System.Web.Http.Cors;
using BegoSys.Common.Constantes;

namespace BegoSys.Service.Controllers
{
    public class OperationController : ApiController
    {
        private IOperationRepository _repository;

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="BegoSys.Service.Controllers.OperationController"/>
        /// </summary>
        /// <param name="repository">Instancia del repositorio inyectada a través de spring.</param>
        public OperationController(IOperationRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la información del local y de las personas que trabajan en el.
        /// </summary>
        /// <param name="idLocal"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(DatosLocalTO))]
        [Route(ConstantesApi.ConsultarDatosLocalUri)]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult ConsultarDatosLocal(long id)
        {
            DatosLocalTO infoLocal = _repository.ConsultarDatosLocal(id);

            if (infoLocal != null)
            {
                return this.Ok(infoLocal);
            }
            else
            {
                return this.NotFound();
            }
        }

    }
}