using BegoSys.Common.Auxiliares;
using BegoSys.Domain;
using BegoSys.Domain.Clases;
using BegoSys.Domain.Entidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace BegoSys.Domain
{
    public partial class EntidadesJuicebar : DbContext
    {
        #region Constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase.
        /// </summary>
        /// <param name="connectionStringName">Nombre de la cadena de conexión a ser
        /// utilizada por el DBContext.</param>
        public EntidadesJuicebar(string connectionStringName)
            : base("name=" + connectionStringName)
        {
            ConnectionStringName = connectionStringName;
            // Permite desactivar las migraciones a la base de datos
            Database.SetInitializer<EntidadesJuicebar>(null);
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Nombre de la cadena de conexión a la base de datos.
        /// </summary>
        public string ConnectionStringName { get; set; }
        #endregion

        #region Entidades
        /// <summary>
        /// Paises
        /// </summary>
        public DbSet<Pais> Paises{ get; set; }

        ///<summary>
        ///Departamentos
        ///</summary>
        public DbSet<Departamento> Departamentos { get; set; }

        ///<summary>
        ///Municipios
        ///</summary>
        public DbSet<Municipio> Municipios { get; set; }

        ///<summary>
        ///Tipo Productos
        ///</summary>
        //public DbSet<TipoProducto> TipoProductos { get; set; }

        ///<summary>
        ///Productos
        ///</summary>
        //public DbSet<Producto> Productos { get; set; }

        ///<summary>
        ///Tipo Ingredientes
        ///</summary>
        public DbSet<TipoIngrediente> TipoIngredientes { get; set; }

        ///<summary>
        ///Ingredientes
        ///</summary>
        public DbSet<Ingrediente> Ingredientes { get; set; }

        ///<summary>
        ///Medidas
        ///</summary>
        public DbSet<Medida> Medidas { get; set; }

        ///<summary>
        ///Envases
        ///</summary>
        //public DbSet<Envase> Envases { get; set; }

        ///<summary>
        ///Procesos
        ///</summary>

        ///<summary>
        ///Insumos
        ///</summary>

        ///<summary>
        ///MedidasRecetas
        ///</summary>
        public DbSet<MedidasReceta> MedidasRecetas { get; set; }

        ///<summary>
        ///Monedas
        ///</summary>

        ///<summary>
        ///Locales
        ///</summary>
        public DbSet<Local> Locales { get; set; }

        ///<summary>
        ///Personas
        ///</summary>
        public DbSet<Persona> Personas { get; set; }

        ///<summary>
        ///Personas Local
        ///</summary>
        public DbSet<PersonasLocal> PersonasLocal { get; set; }

        ///<summary>
        ///Inventarios
        ///</summary>
        public DbSet<Inventario> Inventarios { get; set; }

        ///<summary>
        ///Detalle de los movimientos del inventario
        ///</summary>
        public DbSet<DetalleInventario> DetalleInventarios { get; set; }

        ///<summary>
        ///Detalla a cuales procesos tiene acceso el rol
        ///</summary>
        public DbSet<DetalleRol> DetalleRoles { get; set; }

        ///<summary>
        ///Tabla que contiene los costos asociados a la mano de obra para ser usada en calculos durante la operación
        ///</summary>
        public DbSet<CostoManoObra> CostoManoObra { get; set; }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Método que se utiliza durante la creación del contexto.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.HasDefaultSchema("JUICEBAR");
                modelBuilder.Properties().Configure(cppc => cppc.HasColumnName(cppc.ClrPropertyInfo.Name.ToUpper()));

                modelBuilder.HasDefaultSchema(AuxiliarBegoSys.ObtenerAtributoDeConfiguracion("EsquemaBaseDatos", true));

                modelBuilder.Entity<Local>().ToTable("JBLOCALES");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
                
        }

        /// <summary>
        /// Añade más Detalle a las excepciones de EntityFramework al momento de guardar.
        /// Cuando fallan las validaciones, el framework lanza mensajes genéricos del tipo 
        /// "Validation failed for one or more entities." La idea es incluir en el mensaje
        /// de la excepción cuales fueron las validaciones que fallaron y que campos.
        /// </summary>
        /// <see cref="http://devillers.nl/improving-dbentityvalidationexception/"/>
        /// <returns>Resultado del guardado.</returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw MejorarExcepcion(ex);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw MejorarExcepcion(ex);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Permite crear una nueva instancia del DbContext para el repositorio principal.
        /// </summary>
        /// <returns>Instancia del DbContext para el repositorio principal.</returns>
        public static EntidadesJuicebar GetDbContext()
        {
            var dbContext = (EntidadesJuicebar)null;
            var connectionStringName = AuxiliarBegoSys.ObtenerAtributoDeConfiguracion("NombreCadenaConexion", true);

            if (!string.IsNullOrEmpty(connectionStringName))
            {
                dbContext = new EntidadesJuicebar(connectionStringName);
            }

            return dbContext;
        }

        /// <summary>
        /// Establece que los comandos se enlacen por nombre para no mantener el orden o la posicion de los mismo y tambien asigna los parametros de la funcion al comando.
        /// </summary>
        /// <param name="command">comando con que se ejecutará la función.</param>
        /// <param name="functionParameters">parametros de la función.</param>
        private void SetUpCommandAndParameters(IDbCommand command, params FunctionParameter[] functionParameters)
        {
            OracleCommand oracleCommand = (OracleCommand)command;

            oracleCommand.BindByName = true;

            foreach (FunctionParameter functionParameter in functionParameters)
            {
                IDbDataParameter parameter = command.CreateParameter();

                parameter.ParameterName = functionParameter.ParameterName;

                OracleParameter oracleParameter = (OracleParameter)parameter;

                oracleParameter.OracleDbType = functionParameter.OracleDbType;

                parameter.Direction = functionParameter.Direction;

                parameter.Value = functionParameter.Value;

                command.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// Ejecuta una función de la base de datos y devuelve una colleción de objetos especificado.
        /// </summary>
        /// <typeparam name="T">entidad de la cual se retornará una colección.</typeparam>
        /// <param name="functionName">nombre de la función a ejecutar.</param>
        /// <param name="functionParameters">parámetros de la función a ejecutar.</param>
        /// <returns>colección de objetos especificados en el parametro de tipo.</returns>
        public IEnumerable<T> ExecuteFunction<T>(string functionName, params FunctionParameter[] functionParameters) where T : class
        {
            EnsureOpenConnection();

            IDbConnection connection = Database.Connection;

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = functionName;

                SetUpCommandAndParameters(command, functionParameters);

                IDataReader dataReader = command.ExecuteReader();

                DbDataReader dbDataReader = (DbDataReader)dataReader;

                return ((IObjectContextAdapter)this).ObjectContext.Translate<T>(dbDataReader);
            }
        }

        /// <summary>
        /// Ejecuta una funcion en la base de datos y devuelve un único valor.
        /// </summary>
        /// <typeparam name="T">tipo del valor que devuelve la función.</typeparam>
        /// <param name="functionName">nombre de la función a ejecutar.</param>
        /// <param name="defaultValue">valor por defecto a devolver en caso que la funcion no devuelva nada.</param>
        /// <param name="functionParameters">parámetros de la función a ejecutar.</param>
        /// <returns></returns>
        public T ExecuteFunctionScalar<T>(string functionName, T defaultValue, params FunctionParameter[] functionParameters) where T : IConvertible
        {
            EnsureOpenConnection();

            IDbConnection connection = Database.Connection;

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = functionName;

                SetUpCommandAndParameters(command, functionParameters);

                object valor = command.ExecuteScalar();

                if (valor != null)
                {
                    return (T)Convert.ChangeType(valor, typeof(T));
                }

                return defaultValue;
            }
        }

        /// <summary>
        /// Ejecuta una función de la base de datos que no devuelve ningun valor.
        /// </summary>
        /// <param name="functionName">nombre de la función a ejecutar.</param>
        /// <param name="functionParameters">parámetros de la función a ejecutar.</param>
        public void ExecuteFunction(string functionName, params FunctionParameter[] functionParameters)
        {
            EnsureOpenConnection();

            IDbConnection connection = Database.Connection;

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = functionName;

                SetUpCommandAndParameters(command, functionParameters);

                command.ExecuteNonQuery();
            }
        }
#endregion

        #region Métodos privados
        /// <summary>
        /// Se encarga de abrir la conexion con el motor de base de datos.
        /// </summary>
        private void EnsureOpenConnection()
        {
            IDbConnection connection = Database.Connection;

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// Añade más Detalle al mensaje de la excepción recibida como argumento.
        /// </summary>
        /// <param name="ex">Excepción a la que se le añadirá más Detalle.</param>
        /// <returns>Excepción creada a partir de la excepción recibida como argumento.
        /// </returns>
        private static Exception MejorarExcepcion(Exception ex)
        {
            var mensaje = string.Concat(
                ex.Message, "---> The inner exception is: ", ex.InnerException.Message
            );

            var excepcion = new Exception(mensaje, ex);

            return excepcion;
        }

        /// <summary>
        /// Añade más Detalle al mensaje de la excepción recibida como argumento.
        /// </summary>
        /// <param name="ex">Excepción a la que se le añadirá más Detalle.</param>
        /// <returns>Excepción creada a partir de la excepción recibida como argumento.
        /// </returns>
        private static DbEntityValidationException MejorarExcepcion(
            DbEntityValidationException ex)
        {
            var listaMensajes = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            var mensaje = string.Concat(
                ex.Message, "---> The validation errors are: ",
                string.Join("; ", listaMensajes)
            );

            var excepcion = new DbEntityValidationException(
                mensaje, ex.EntityValidationErrors
            );

            return excepcion;
        }
        #endregion

    }
}
