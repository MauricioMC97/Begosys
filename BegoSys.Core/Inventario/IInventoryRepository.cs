using System;
//using System.Collections.Generic;
using System.Threading.Tasks;

namespace BegoSys.Core.Inventario
{
    public interface IInventoryRepository
    {
        Task RetirarProducto(long idProducto, long idLocal, long idPersona, DateTime dFecha);
        Task SalvarCompras();
    }
}