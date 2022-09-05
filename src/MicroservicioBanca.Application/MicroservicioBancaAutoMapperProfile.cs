using AutoMapper;
using MicroservicioBanca.Application.Contracts.Clientes;
using MicroservicioBanca.Application.Contracts.Cuentas;
using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Domain.Cuentas;

namespace MicroservicioBanca.Application
{
    public class MicroservicioBancaAutoMapperProfile : Profile
    {
        public MicroservicioBancaAutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<Cuenta, CuentaDto>();
        }
    }
}
