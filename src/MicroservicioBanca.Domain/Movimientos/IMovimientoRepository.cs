using System;

namespace MicroservicioBanca.Domain.Movimientos
{
    public interface IMovimientoRepository : IGenericRepository<Movimiento, Guid>
    {
    }
}
