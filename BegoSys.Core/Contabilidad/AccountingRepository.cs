using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BegoSys.Core.Contabilidad
{
    public class AccountingRepository: IAccountingRepository
    {
        public bool RegistrarLibroMayor(string sNroSubCuenta, long Debito, long Credito)
        {
            return true;
        }
    }
}
