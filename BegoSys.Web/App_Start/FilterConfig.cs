using System.Web;
using System.Web.Mvc;
using BegoSys.Common.Atributos;

namespace BegoSys.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            /*Se adicionar el filtro personalizado del proyecto para el manejo de excepciones
             y la implentación de interceptores*/
            filters.Add(new BegoFiltroExcepcionMvcAttribute());
            //filters.Add(new BegoFiltroExcepcionMvcAttribute());
            //#if !DEBUG
            //            filters.Add(new BegoFiltroAutorizacionMvcAttribute());
            //#endif
        }
    }
}
