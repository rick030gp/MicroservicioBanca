using MicroservicioBanca.EntityFrameworkCore;
using System;

namespace MicroservicioBanca.Movimientos
{
    public class MovimientoRepository : GenericRepository<Movimiento, Guid>, IMovimientoRepository
    {
        public MovimientoRepository(IMicroservicioBancaDbContext context) : base(context)
        {
        }
    }
}
