using System;
using System.Configuration;
using System.Runtime.Caching;
using System.Web;

namespace BegoSys.Web.Cache
{
    public class InMemoryCache : ICacheService
    {
        public int MinutesExpirationCache { get; set; }
        public InMemoryCache()
        {
            try
            {
                MinutesExpirationCache = Convert.ToInt32(ConfigurationManager.AppSettings["MinutesExpirationCache"]);
            }
            catch
            {
                MinutesExpirationCache = 30;
            }
        }
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            var usuario = HttpContext.Current.User.Identity.Name;
            var nombreUsuario = HttpContext.Current.User.Identity.Name.Contains("\\") ? usuario.Split('\\')[1] : usuario;

            var identificador = $"{cacheKey}_{nombreUsuario}";

            T item = MemoryCache.Default.Get(identificador) as T;
            if (item == null)
            {
                item = getItemCallback();
                if (item != null)
                    MemoryCache.Default.Add(identificador, item, DateTime.Now.AddMinutes(MinutesExpirationCache));
                //MemoryCache.Default.Add(identificador, item, DateTime.Now.AddMinutes(10));
            }
            return item;
        }

        public T Remove<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            var usuario = HttpContext.Current.User.Identity.Name;
            var nombreUsuario = HttpContext.Current.User.Identity.Name.Contains("\\") ? usuario.Split('\\')[1] : usuario;

            var identificador = $"{cacheKey}_{nombreUsuario}";

            T item = MemoryCache.Default.Get(identificador) as T;
            if (item != null)
            {
                MemoryCache.Default.Remove(identificador);
            }
            return null;
        }
    }

    interface ICacheService
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
        T Remove<T>(string cacheKey, Func<T> getItemCallback) where T : class;
    }
}