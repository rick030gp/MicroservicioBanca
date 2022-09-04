using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Repository.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MicroservicioBanca.Repository.Clientes
{
    public class ClienteRepository : GenericRepository<Cliente, Guid>, IClienteRepository
    {
        public ClienteRepository(IMicroservicioBancaDbContext context) : base(context)
        {
        }

        public Task<Cliente> GetClientByIdentificationAsync(string identification)
        {
            throw new NotImplementedException();
        }
    }
}
