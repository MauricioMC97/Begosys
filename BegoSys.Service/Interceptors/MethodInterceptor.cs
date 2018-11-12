using AopAlliance.Intercept;
using BegoSys.Common.Atributos;
using BegoSys.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BegoSys.Service.Interceptors
{
    public class MethodInterceptor : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            object result = null;
            object[] customAttributes = invocation.Method.GetCustomAttributes(typeof(BegoSysRetryAttribute), true);

            if (customAttributes != null && customAttributes.Length > 0)
            {
                //Se obtiene el valor del atributo
                var customAttribute = (BegoSysRetryAttribute)customAttributes[0];

                //Se crea el template que ejecutara los reintentos
                BegoSysRetryTemplate template = new BegoSysRetryTemplate();

                TimeSpan delay;

                if (!TimeSpan.TryParse(customAttribute.Delay, out delay))
                    delay = new TimeSpan(0, 0, 30);

                //Se establecen las propiedades para los reintentos
                template.Delay = delay;
                template.MaxRetry = Convert.ToInt32(customAttribute.MaxRetry);
                //Se procede a ejectuar el método
                result = template.ExecuteWithRetry(delegate ()
                {
                    return invocation.Proceed();
                });
            }
            else
            {
                //Se ejecuta el método normalmente
                return invocation.Proceed();
            }
            return result;
        }
    }
}