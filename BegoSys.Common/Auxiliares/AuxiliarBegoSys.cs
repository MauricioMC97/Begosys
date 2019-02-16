#region Derechos Reservados
// ===================================================
// Desarrollado Por             : esteban.giraldo
// Fecha de Creación            : 2017/05/19
// Modificado Por               : esteban.giraldo
// Fecha Modificación           : 2017/05/19
// Empresa                      : BEGO INVERSIONES SAS
// ===================================================
#endregion

#region Referencias
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using Common.Logging;
using Spring.Caching;
using Spring.Context.Support;
using BegoSys.Common.Constantes;
using BegoSys.Common.Excepciones;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
#endregion

namespace BegoSys.Common.Auxiliares
{
    /// <summary>
    /// Clase utilitaria que contiene métodos que son comunmente usados a nivel de todas 
    /// las capas del proyecto
    /// </summary>
    public static partial class AuxiliarBegoSys
    {
        #region Localización y cultura
        /// <summary>
        /// Establece en el hilo actual la información de localización que se encuentra
        /// definida en el archivo de configuración del contexto de la aplicación.
        /// </summary>
        public static void ActualizarCulturaActual()
        {
            //TODO: Verificar como se aplicaria el hilo de cultura dependiendo de las preferencias del usuario (juanes)
            CultureInfo culture = new CultureInfo(ObtenerAtributoDeConfiguracion("Cultura", true));
            String formatoHora = ObtenerAtributoDeConfiguracion("FormatoHoraSistema", true);
            String formatoFecha = ObtenerAtributoDeConfiguracion("FormatoFechaSistema", true);
            String separadorDecimal = ObtenerAtributoDeConfiguracion("SeparadorDecimalSistema", true);
            String separadorMiles = ObtenerAtributoDeConfiguracion("SeparadorMilesSistema", true);

            culture.DateTimeFormat.ShortDatePattern = formatoFecha;
            culture.DateTimeFormat.ShortTimePattern = formatoHora;
            culture.DateTimeFormat.LongTimePattern = formatoHora;
            culture.DateTimeFormat.LongDatePattern = formatoFecha + " " + formatoHora;
            culture.NumberFormat.NumberDecimalSeparator = separadorDecimal == "ç" ? "," : separadorDecimal;
            culture.NumberFormat.CurrencyDecimalSeparator = separadorDecimal == "ç" ? "," : separadorDecimal;
            culture.NumberFormat.NumberGroupSeparator = separadorMiles == "ç" ? "," : separadorMiles;
            culture.NumberFormat.CurrencyGroupSeparator = separadorMiles == "ç" ? "," : separadorMiles;
            culture.NumberFormat.CurrencySymbol = ObtenerAtributoDeConfiguracion("FormatoMoneda", true);
            Thread.CurrentThread.CurrentCulture = culture;
        }

        /// <summary>
        /// Traduce un mensaje de la aplicación.
        /// </summary>
        /// <param name="claveMensaje">Nombre del mensaje a traducir.</param>
        /// <returns>Un texto con el mensaje traducido, o la clave en caso que no exista.</returns>
        public static string TraducirMensaje(string claveMensaje)
        {
            try
            { return ContextRegistry.GetContext().GetMessage(claveMensaje); }
            catch { return claveMensaje; }

        }

        /// <summary>
        /// Traduce un mensaje de la aplicación con los respectivos parámetros.
        /// </summary>
        /// <param name="claveMensaje">Nombre del mensaje a traducir.</param>
        /// <param name="args">Parámetros a reemplazar en el mensaje.</param>
        /// <returns>Un texto con el mensaje traducido y los parámetros integrados.</returns>
        public static string TraducirMensaje(string claveMensaje, params object[] args)
        {
            string mensaje = TraducirMensaje(claveMensaje);
            string mensajeTraducido = (args != null) ? string.Format(mensaje, args) : mensaje;
            return mensajeTraducido;
        }
        #endregion

        #region Logging
        private static ILog _log = LogManager.GetLogger(typeof(AuxiliarBegoSys));

        /// <summary>
        /// Permite escribir una entrada de error al log
        /// </summary>
        /// <param name="mensage">Mensaje de error a escribir</param>
        /// <param name="ex">Excepción a registrar</param>
        public static void EscribirError(string mensage, Exception ex)
        {
            if (_log.IsErrorEnabled)
                _log.Error(mensage, ex);
        }

        /// <summary>
        /// Permite escribir una entrada de log con un mensaje localizado
        /// </summary>
        /// <param name="categoria">Categoría de log a utilizar</param>
        /// <param name="mensaje">Llave del mensaje a localizar</param>
        /// <param name="args">Parámetros del mensaje</param>
        public static void EscribirLog(LogCategory categoria, string mensaje, params object[] args)
        {
            EscribirLog(categoria, mensaje, true, args);
        }

        /// <summary>
        /// Permite escribir una entrada de log
        /// </summary>
        /// <param name="categoria">Categoría de log a utilizar</param>
        /// <param name="mensaje">Llave del mensaje a localizar o el mensaje</param>
        /// <param name="traducir">Indica si se debe localizar o no el mensaje</param>
        /// <param name="args">Parámetros del mensaje</param>
        public static void EscribirLog(LogCategory categoria, string mensaje, bool traducir, params object[] args)
        {
            string localizedMessage = (traducir) ? TraducirMensaje(mensaje, args) : string.Format(mensaje, args);

            switch (categoria)
            {
                case LogCategory.Error:
                    if (_log.IsErrorEnabled)
                        _log.Error(localizedMessage);
                    break;
                case LogCategory.Fatal:
                    if (_log.IsFatalEnabled)
                        _log.Fatal(mensaje);
                    break;
                case LogCategory.Warning:
                    if (_log.IsWarnEnabled)
                        _log.Warn(localizedMessage);
                    break;
                case LogCategory.Information:
                    if (_log.IsInfoEnabled)
                        _log.Info(localizedMessage);
                    break;
                case LogCategory.Debug:
                    if (_log.IsDebugEnabled)
                        _log.Debug(localizedMessage);
                    break;
                case LogCategory.Trace:
                    if (_log.IsTraceEnabled)
                        _log.Trace(localizedMessage);
                    break;
                default:
                    if (_log.IsErrorEnabled)
                        _log.Error(localizedMessage);
                    break;
            }
        }
        #endregion

        #region Seguridad
        /// <summary>
        /// Permite obtener el usuario actual autenticado en el contexto de la aplicación
        /// </summary>
        /// <returns>Retorna el id del usuario sin el dominio</returns>
        //public static string ObtenerUsuarioId()
        //{
        //    string usuarioId = string.Empty;
        //    IIdentity identity = null;

        //    if (HttpContext.Current != null)
        //    {
        //        identity = HttpContext.Current.User.Identity;
        //    }
        //    else
        //    {
        //        usuarioId = Thread.CurrentThread.Name;
        //    }

        //    if (identity != null)
        //    {
        //        usuarioId = identity.Name;
        //    }

        //    if (string.IsNullOrEmpty(usuarioId))
        //    {
        //        identity = WindowsIdentity.GetCurrent();
        //        if (identity != null)
        //        {
        //            usuarioId = identity.Name;
        //        }
        //    }

        //    if (string.IsNullOrEmpty(usuarioId))
        //    {
        //        usuarioId = ObtenerAtributoDeConfiguracion("UsuarioPredeterminado", true);
        //    }

        //    int posDominio = usuarioId.IndexOf(@"\");

        //    if (posDominio != -1)
        //    {
        //        usuarioId = usuarioId.Substring(posDominio + 1);
        //    }

        //    return usuarioId;
        //}
        #endregion

        #region Generales
        /// <summary>
        /// Obtiene el numero de horas del mes de una fecha.
        /// </summary>
        /// <param name="mesCalculo">Fecha para calcular las horas del mes.</param>
        /// <returns>Numero de horas del mes.</returns>
        public static int ObtenerHorasMes(DateTime mesCalculo)
        {
            return (DateTime.DaysInMonth(mesCalculo.Year, mesCalculo.Month) * 24);
        }

        /// <summary>
        /// Retorna el valor decimal del texto o nulo si no se puede convertir.
        /// </summary>
        /// <param name="value">El texto a convertir.</param>
        /// <returns>Un valor decimal o nulo.</returns>
        public static decimal? DecimalONull(string value)
        {
            decimal testValue = -1;
            if (decimal.TryParse(value, out testValue))
            {
                return testValue;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna el valor entero del texto o nulo si no se puede convertir.
        /// </summary>
        /// <param name="value">El texto a convertir.</param>
        /// <returns>Un valor entero o nulo.</returns>
        public static int? EnteroONull(string value)
        {
            int testValue = -1;
            if (int.TryParse(value, out testValue))
            {
                return testValue;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna el valor fecha o nulo si no se puede convertir
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? FechaONull(string value)
        {
            DateTime testValue;
            if (DateTime.TryParse(value, out testValue))
            {
                return testValue;
            }
            return null;
        }

        /// <summary>
        /// Permite obtener un valor desde la cache
        /// </summary>
        /// <param name="key">clave del objeto que se obtiene</param>
        /// <returns>retorna el objeto</returns>
        public static object ObtenerDesdeCache(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            object result = null;

            //Se obtiene la cache de spring
            var cache = ContextRegistry.GetContext().GetObject("BegoSysCache") as ICache;

            if (cache != null)
                result = cache.Get(key);

            return result;
        }

        /// <summary>
        /// Permite almacenar los datos en la cache
        /// </summary>
        /// <param name="key">Identificador con la información a almacenear en Cache</param>
        /// <param name="data">Datos a almacenar en cache</param>
        public static void AlmacenarEnCache(string key, object data)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            object result = null;

            //Se obtiene la cache de spring
            var cache = ContextRegistry.GetContext().GetObject("BegoSysCache") as ICache;

            if (cache == null)
                throw new ArgumentNullException("cache");

            result = cache.Get(key);

            if (result != null)
            {
                cache.Remove(key);
            }

            cache.Insert(key, data);
        }

        /// <summary>
        /// Permite eliminar un elmento desde la cachce
        /// </summary>
        /// <param name="key">Clave del elemento a eliminar</param>
        public static void EliminarDeCache(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            //Se obtiene la cache de spring
            var cache = ContextRegistry.GetContext().GetObject("BegoSysCache") as ICache;

            if (cache != null)
                cache.Remove(key);
        }

        /// <summary>
        /// Retorna el valor de configuración para la llave enviada.
        /// </summary>
        /// <param name="llaveAtributo">String con el valor de llave.</param>
        /// <param name="esObligatorio">Indica si el valor de configuración es obligatorio.</param>
        /// <returns>Valor de configuración para la llave enviada.</returns>
        /// <remarks>Lanza excepción de configuracion cuando no encuentra valor de la llave y el dato es obligatorio.</remarks>
        public static string ObtenerAtributoDeConfiguracion(string llaveAtributo, bool esObligatorio)
        {
            string valor = ConfigurationManager.AppSettings.Get(llaveAtributo);

            if (valor == null && esObligatorio)
            {
                throw new BegoSysException("BegoSysError_MensajeValorConfiguracion", llaveAtributo);
            }

            return valor;
        }

        /// <summary>
        /// Detetmina si una fecha enviada como String es valida para enviar a SQL Server. Válida que sea mayor a 1/1/1753
        /// </summary>
        /// <param name="sFecha">Fecha</param>
        /// <returns>Es valida o no</returns>
        public static bool EsFechaSql(string sFecha)
        {

            DateTime dFecha;
            if (DateTime.TryParseExact(sFecha, CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern,
                                        CultureInfo.CurrentCulture, DateTimeStyles.None, out dFecha)
                && dFecha > new DateTime(1753, 1, 1))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Permite convertir un enumerado con atributos CodeAttribute y
        /// DescriptionAttribute en una lista de GenericItem.
        /// </summary>
        /// <typeparam name="T">Enumeración de la que se extraerá la lista de valores.
        /// </typeparam>
        public static List<ElementoLista> EnumALista<T>()
        {
            var list = new List<ElementoLista>();
            var enumType = typeof(T);
            string descriptionValue = string.Empty;

            if (enumType.IsEnum)
            {
                foreach (var value in Enum.GetValues(typeof(T)))
                {
                    var memberInfo = enumType.GetMember(value.ToString())[0];
                    var descriptionAttr = (DescriptionAttribute)
                        memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];

                    if (descriptionAttr != null)
                        descriptionValue = TraducirMensaje(descriptionAttr.Description);


                    list.Add(new ElementoLista
                    {
                        Clave = value.ToString(),
                        Valor = descriptionValue
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// Genera las fechas con hora dado un rango de fechas
        /// </summary>
        public static List<DateTime> FechasConHoras(DateTime fechaInicio, DateTime fechaFin)
        {
            var horas = Enumerable.Range(0, 24).Select(h => new TimeSpan(h, 0, 0));
            var fechas = Enumerable.Range(0, (fechaFin - fechaInicio).Days + 1).Select(d => fechaInicio.AddDays(d));
            var fechasConHoras = fechas.SelectMany(f => horas, (f, h) => f + h).ToList();

            return fechasConHoras;
        }

        /// <summary>
        /// Genera las fechas con hora dado un rango de fechas
        /// </summary>
        public static List<DateTime> Fechas(DateTime fechaInicio, DateTime fechaFin)
        {
            var fechas = Enumerable.Range(0, (fechaFin - fechaInicio).Days + 1).Select(d => fechaInicio.AddDays(d)).ToList();

            return fechas.ToList();
        }

        /// <summary>
        /// Toma un rango de fechas y permite realizar la descripción mes1 (separadorMeses) mes2... mesn 
        /// Año1 (separadorAños) Año1
        /// </summary>
        /// <param name="fechaInicio">Fecha inicio</param>
        /// <param name="fechaFin">Fecha fin</param>
        /// <returns>Retorna texto</returns>
        public static string DescripcionRangoFechas(DateTime fechaInicio, DateTime fechaFin, string separadorMeses, string separadorAños)
        {
            var diferenciaMeses = 12 * (fechaInicio.Year - fechaFin.Year) + fechaInicio.Month - (fechaFin.Month + 1);
            var meses = Math.Abs(diferenciaMeses);

            var fechasMensuales = Enumerable.Range(0, meses).Select(offset => fechaInicio.AddMonths(offset))
              .ToArray();

            var fechasTexto = fechasMensuales.GroupBy(f => new { Ano = f.Year }).Select(f => new {
                Ano = f.Key.Ano,
                Meses = string.Join(separadorMeses, f.Select(m => m.ToString("MMMM")).ToArray())
            });

            var descripciones = fechasTexto.Select(f => $"{f.Meses} {separadorAños} {f.Ano}");
            var descripcionCompleta = string.Join($"{separadorMeses} ", descripciones);

            return descripcionCompleta;
        }

        #endregion
    }
}

