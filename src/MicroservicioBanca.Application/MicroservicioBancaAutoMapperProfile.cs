using AutoMapper;
using MicroservicioBanca.Application.Contracts.Clientes;
using MicroservicioBanca.Application.Contracts.Cuentas;
using MicroservicioBanca.Application.Contracts.Movimientos;
using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Domain.Cuentas;
using MicroservicioBanca.Domain.Movimientos;

namespace MicroservicioBanca.Application
{
    public class MicroservicioBancaAutoMapperProfile : Profile
    {
        public MicroservicioBancaAutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<Cliente, ClienteCompletoDto>();
            CreateMap<Cuenta, CuentaDto>();
            CreateMap<Movimiento, MovimientoDto>();
        }
    }
}
