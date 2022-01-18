using System;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using BegoSys.Web.Context;
using BegoSys.Web.Cache;

namespace BegoSys.Web.Models
{
    /// <summary>
    /// Clase para validar el usuario contra el directorio activo
    /// y generar el Hashtable htCredencial utilizado para poder navegar por el aplicativo
    /// </summary>
    public class AutenticacionDirActivo
    {
        /// <summary>
        /// Valida usuario contra el directorio activo
        /// Y arma el Hashtable htCredencial y lo almacena en variable de sesion
        /// </summary>
        public static void ObtenerUsuario()
        {
            DirectoryEntry ADEntry;
            Contexto.NombreUsuario = string.Empty;
            Contexto.Usuario = string.Empty;
            InMemoryCache CacheService = new InMemoryCache();

            try
            {
                if (Contexto.NombreUsuario.Length == 0)
                {
                    string[] a = System.Web.HttpContext.Current.User.Identity.Name.Split('\\');

                    ADEntry = ConsultarUsuario(a[0], a[1]);

                    //verificacion de caducidad
                    TimeSpan fecha = GetTimeRemainingUntilPasswordExpiration(ADEntry);
                    var datosFechaCaducidad = CacheService.GetOrSet(Constantes.ConstantesSession.Caducidad, () => fecha.ToString());

                    string Name = ADEntry.Properties["FullName"].Value.ToString();
                    Contexto.NombreUsuario = Name;
                    Contexto.Usuario = ADEntry.Properties["name"].Value.ToString();
                }

                string sRol = string.Empty;

                //isInRole = true;
                Hashtable htCredencial = (Hashtable)CacheService.GetOrSet(Constantes.ConstantesSession.CredencialUsuario, () => new Hashtable());

                string[] DatosUsuario = System.Web.HttpContext.Current.User.Identity.Name.Split('\\');

                string sUsuario = Contexto.Usuario.ToString();
                string sSesionID = System.Web.HttpContext.Current.Session.SessionID.ToString();
                string sHoraFinSesion = System.Web.HttpContext.Current.Session.Timeout.ToString();
                string sUsuarioID = DatosUsuario[1];
              
                htCredencial = new Hashtable();

                //Configura el hashtable con la Credencial del usuario
                htCredencial.Add("Usuario", sUsuario);
                htCredencial.Add("Aplicacion", "DHIME");
                htCredencial.Add("Role", sRol);
                htCredencial.Add("SesionID", sSesionID);
                htCredencial.Add("HoraFinSesion", sHoraFinSesion);
                htCredencial.Add("UsuarioID", sUsuarioID);

                var datosCredencial = CacheService.GetOrSet(Constantes.ConstantesSession.CredencialUsuario, () => htCredencial);
            }
            catch (Exception Exc)
            {
                throw new Exception("Verificación Usuario: " + Exc.Message);
            }
        }

        private static DirectoryEntry ConsultarUsuario(string sDominio, string sUsuario)
        {
            String sDominioConfig = System.Configuration.ConfigurationManager.AppSettings["DefaultDomain"].ToString();
            DirectoryEntry deNameReturn = null;
            if (!sDominioConfig.ToUpper().Equals(sDominio))
                throw new Exception("El Dominio (" + sDominio + ") ingresado por el Usuario (" + sUsuario + ") es diferente al configurado en el sistema (" + sDominioConfig + ")");

            DirectoryEntry ADEntry = new DirectoryEntry(string.Format("WinNT://{0}", sDominioConfig));

            Parallel.ForEach(ADEntry.Children.Cast<Object>(), (currentElement, state) =>
            {
                // The more computational work you do here, the greater  
                // the speedup compared to a sequential foreach loop. 
                DirectoryEntry deName = (DirectoryEntry)currentElement;
                if (deName.Name.ToString().ToUpper().Equals(sUsuario.ToUpper()))
                {
                    deNameReturn = deName;
                    state.Break();
                }

            } //close lambda expression
             ); //close method invocation 

            if (deNameReturn != null)
                return deNameReturn;
            throw new Exception("El Usuario (" + sUsuario + ") No esta inscrito en el directorio activo");
        }
        //INCGB0006181139

        public static TimeSpan GetMaxPasswordAge(DirectoryEntry entry)
        {
            TimeSpan maxPwdAge = TimeSpan.MinValue;

            long fechaCaducidad = ObtenerInt64(entry, "MaxPasswordAge");
            maxPwdAge = TimeSpan.FromTicks(fechaCaducidad);
            return maxPwdAge.Duration();
        }

        private static Int64 ObtenerInt64(DirectoryEntry entry, string attr)
        {
            if (entry.Properties.Contains(attr))
            {
                return (Int64)entry.Properties[attr].Value;
            }
            return -1;
        }

        private static TimeSpan GetTimeRemainingUntilPasswordExpiration(DirectoryEntry entry)
        {

            var maxPasswordAge = (int)entry.Properties.Cast<System.DirectoryServices.PropertyValueCollection>().First(p => p.PropertyName == "MaxPasswordAge").Value;
            var passwordAge = (int)entry.Properties.Cast<System.DirectoryServices.PropertyValueCollection>().First(p => p.PropertyName == "PasswordAge").Value;
            return TimeSpan.FromSeconds(maxPasswordAge) - TimeSpan.FromSeconds(passwordAge);
        }
    }
}