using AutoMapper;
using MicroservicioBanca.Application.Contracts.Clientes;
using MicroservicioBanca.Application.Contracts.Cuentas;
using MicroservicioBanca.Application.Contracts.Movimientos;
using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Domain.Cuentas;
using MicroservicioBanca.Domain.Movimientos;
using System.Linq;

namespace MicroservicioBanca.Application
{
    public class MicroservicioBancaAutoMapperProfile : Profile
    {
        public MicroservicioBancaAutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<Cuenta, ReporteCuentaDto>()
                .ForMember(dest =>
                    dest.Movimiento,
                    opt => opt.MapFrom(src => src.Movimientos.Sum(m => m.Valor)));
            CreateMap<Cuenta, CuentaDto>();
            CreateMap<Movimiento, MovimientoDto>();
        }
    }
}
