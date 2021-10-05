using AutoMapper;
using Contas.API.ViewModels;
using Contas.Domain;

namespace Contas.API.Profiles
{
    public class ContaProfile : Profile
    {
        public ContaProfile()
            => CreateMap<Conta, ContaViewModel>();
    }
}