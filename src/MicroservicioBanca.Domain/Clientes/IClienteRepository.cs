﻿using System;
using System.Threading.Tasks;

namespace MicroservicioBanca.Domain.Clientes
{
    public interface IClienteRepository : IGenericRepository<Cliente, Guid>
    {
        Task<Cliente> GetClientByIdentificationAsync(string identification);
    }
}
