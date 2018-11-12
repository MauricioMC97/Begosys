using BegoSys.TO;

namespace BegoSys.Core
{
    public interface IOperationRepository
    {
        DatosLocalTO ConsultarDatosLocal(long lIdenLocal);
    }
}