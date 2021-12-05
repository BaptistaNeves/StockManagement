using AutoMapper;
using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Application.ViewModels.Catalogo;
using StockManagement.Application.ViewModels.Pessoa;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Entities.Pessoa;

namespace StockManagement.Application.AutoMapper
{
    public class EntitiesToViewModelsProfile : Profile
    {
        public EntitiesToViewModelsProfile()
        {
            CreateMap<Categoria, CategoriaViewModel>();
            CreateMap<Produto, ProdutoViewModel>();

            CreateMap<Cliente, ClienteViewModel>();
        }
    }
}
