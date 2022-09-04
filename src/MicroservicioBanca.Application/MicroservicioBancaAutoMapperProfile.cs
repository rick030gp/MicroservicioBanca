using AutoMapper;
using MicroservicioBanca.Application.Contracts.Clientes;
using MicroservicioBanca.Domain.Clientes;

namespace MicroservicioBanca.Application
{
    public class MicroservicioBancaAutoMapperProfile : Profile
    {
        public MicroservicioBancaAutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDto>();
        }
    }
}
