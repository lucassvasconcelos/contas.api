using AutoMapper;
using Contas.API.ViewModels;
using Contas.Domain;

namespace Contas.API.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
            => CreateMap<Usuario, UsuarioViewModel>();
    }
}