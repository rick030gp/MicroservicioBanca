using MicroservicioBanca.Domain.Movimientos;
using MicroservicioBanca.Repository.EntityFrameworkCore;
using System;

namespace MicroservicioBanca.Repository.Movimientos
{
    public class MovimientoRepository : GenericRepository<Movimiento, Guid>, IMovimientoRepository
    {
        public MovimientoRepository(IMicroservicioBancaDbContext context) : base(context)
        {
        }
    }
}
