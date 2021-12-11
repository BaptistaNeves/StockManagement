using AutoMapper;
using StockManagement.Core.DTOs.Catalogo;
using StockManagement.Core.DTOs.Pessoa;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Entities.Pessoa;
using StockManagement.Shared.Pagination;

namespace StockManagement.Application.AutoMapper
{
    public class EntitiesToViewModelsProfile : Profile
    {
        public EntitiesToViewModelsProfile()
        {
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<Produto, ProdutoDto>();

            CreateMap<Cliente, ClienteDto>();
        }
    }
}
