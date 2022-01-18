#region Derechos Reservados
// ===================================================
// Desarrollado Por             : esteban.giraldo
// Fecha de Creación            : 2017/05/19
// Modificado Por               : esteban.giraldo
// Fecha Modificación           : 2017/05/19
// Empresa                      : MVM INGENIERIA DE SOFTWARE S.A.S
// ===================================================
#endregion

#region Referencias
using BegoSys.Common.Atributos;
using BegoSys.Common.ProveedoresDependencias;
//using Microsoft.Owin.Security.OAuth;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
#endregion

namespace BegoSys.Service
{
    /// <summary>
    /// Clase estática que contiene la configuración de servicios y rutas en la API
    /// </summary>
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Enable CORS
            string origins = ConfigurationManager.AppSettings["Origins"].ToString();
            var corsAttribute = new EnableCorsAttribute(origins, "*", "*");
            config.EnableCors(corsAttribute);

            // Web API filters
            // Configure Web API para usar solo la autenticación de token de portador
            config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Formatters.Add(new BsonMediaTypeFormatter());

            config.Filters.Add(new BegoFiltroExcepcionApiWebAttribute());
            config.Filters.Add(new BegoFiltroAccionHttpAttribute());

            // Web API configuration and services
            //Se registra el encargado de resolver las instancias y dependencias
            IApplicationContext context = ContextRegistry.GetContext();
            config.DependencyResolver = new SolucionadorDependenciaBegoSystem(context);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}