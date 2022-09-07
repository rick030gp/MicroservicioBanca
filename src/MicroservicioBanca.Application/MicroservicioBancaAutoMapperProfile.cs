using AutoMapper;
using MicroservicioBanca.Clientes;
using MicroservicioBanca.Cuentas;
using MicroservicioBanca.Movimientos;
using System.Linq;

namespace MicroservicioBanca
{
    public class MicroservicioBancaAutoMapperProfile : Profile
    {
        public MicroservicioBancaAutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<Cuenta, ReporteCuentaDto>()
                .ForMember(dest =>
                    dest.Movimiento,
                    opt => opt.MapFrom(src => src.Movimientos.Sum(m => m.Valor)))
                .ForMember(dest =>
                    dest.SaldoDisponible,
                    opt => opt.MapFrom(src => src.Saldo));
            CreateMap<Cuenta, CuentaDto>();
            CreateMap<Movimiento, MovimientoDto>();
        }
    }
}
