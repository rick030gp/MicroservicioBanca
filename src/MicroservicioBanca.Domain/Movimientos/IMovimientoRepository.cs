using System;

namespace MicroservicioBanca.Movimientos
{
    public interface IMovimientoRepository : IGenericRepository<Movimiento, Guid>
    {
    }
}
