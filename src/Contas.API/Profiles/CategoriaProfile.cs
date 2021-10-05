using AutoMapper;
using Contas.API.ViewModels;
using Contas.Domain;

namespace Contas.API.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
            => CreateMap<Categoria, CategoriaViewModel>();
    }
}