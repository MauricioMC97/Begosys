using BegoSys.Domain.Clases;
using BegoSys.TO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Domain
{
    public partial class EntidadesJuicebar
    {
        public IEnumerable<PedidoTO> SpInventarioPerfilesTransversales(DateTime fecha, long idLocal)
        {
            return ExecuteFunction<PedidoTO>(ConstanteProcedimientoAlmacenado.PR_PEDIDOSXENTREGAR,
                new FunctionParameter[] {
                    new FunctionParameter("pFECHA", OracleDbType.Int16, ParameterDirection.Input, fecha),
                    new FunctionParameter("pIDLOCAL", OracleDbType.Int64, ParameterDirection.Input, idLocal),
                    new FunctionParameter("PCURSORDATOS", OracleDbType.RefCursor, ParameterDirection.Output, null)
                });

        }

    }
}
