using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Common.Constantes
{
    public static class ConstantesApi
    {
        #region Operacion
        public const string ConsultarDatosLocalUri = "api/Operation/ConsultarDatosLocal/{id}";
        #endregion

        #region Accounting
        public const string GuardarenLibroMayor = "api/Operation/GuardarenLibroMayor/{subcuenta}{debito}{personadebito}{credito}{personacredito}{fecha}{observaciones}";
        #endregion
    }
}
