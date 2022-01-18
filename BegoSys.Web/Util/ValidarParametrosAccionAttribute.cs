#region Derechos Reservados
// ===================================================
// Desarrollado Por             : mauricio.medina
// Fecha de Creación            : 2017/08/17
// Modificado Por               : mauricio.medina
// Fecha Modificación           : 2017/08/17
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

namespace BegoSys.Web.Util
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// Activa la validación de los parámetros de una acción con tipos de datos primitivos.
    /// </summary>
    public class ValidarParametrosAccionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Construye una nueva instancia de la clase.
        /// </summary>
        public ValidarParametrosAccionAttribute()
        {
            Order = 1;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ActionDescriptor descriptorAccion = filterContext.ActionDescriptor;

            ParameterDescriptor[] parametros = descriptorAccion.GetParameters();

            ModelStateDictionary diccionarioEstadoModelo = ((Controller)filterContext.Controller).ModelState;

            foreach (ParameterDescriptor parametro in parametros)
            {
                object argument = filterContext.ActionParameters.ContainsKey(parametro.ParameterName) ?
                    filterContext.ActionParameters[parametro.ParameterName] : null;

                EvaluarAtributosValidacion(parametro, argument, diccionarioEstadoModelo);
            }

            base.OnActionExecuting(filterContext);
        }

        private void EvaluarAtributosValidacion(ParameterDescriptor parametro, object valorArgumento, ModelStateDictionary diccionarioEstadoModelo)
        {
            object[] validationAttributes = parametro.GetCustomAttributes(typeof(ValidationAttribute), true);

            foreach (object attributeData in validationAttributes)
            {
                ValidationAttribute atributoValidacion = (ValidationAttribute)attributeData;

                bool esValido = atributoValidacion.IsValid(valorArgumento);

                if (!esValido)
                {
                    diccionarioEstadoModelo.AddModelError(parametro.ParameterName, atributoValidacion.FormatErrorMessage(parametro.ParameterName));
                }
            }
        }
    }
}