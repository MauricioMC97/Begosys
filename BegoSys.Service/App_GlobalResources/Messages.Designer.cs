//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Messages", global::System.Reflection.Assembly.Load("App_GlobalResources"));
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Finaliza la ejecución de la acción {0} del controller {1}. Fecha Fin: {2} {3}.
        /// </summary>
        internal static string BegoSysDebug_RegistroFinMetodo {
            get {
                return ResourceManager.GetString("BegoSysDebug_RegistroFinMetodo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Inicia la ejecución de la acción {0} del controller {1}. Fecha Inicio: {2} {3}.
        /// </summary>
        internal static string BegoSysDebug_RegistroInicioMetodo {
            get {
                return ResourceManager.GetString("BegoSysDebug_RegistroInicioMetodo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No.
        /// </summary>
        internal static string Comun_No {
            get {
                return ResourceManager.GetString("Comun_No", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SI.
        /// </summary>
        internal static string Comun_Si {
            get {
                return ResourceManager.GetString("Comun_Si", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La variable {0} no se encuentra en el archivo de configuración y es obligatoria.
        /// </summary>
        internal static string SGDHMError_MensajeValorConfiguracion {
            get {
                return ResourceManager.GetString("SGDHMError_MensajeValorConfiguracion", resourceCulture);
            }
        }
    }
}
