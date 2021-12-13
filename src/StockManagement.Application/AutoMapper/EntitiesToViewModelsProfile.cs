using AutoMapper;
using StockManagement.Core.DTOs.Catalogo;
using StockManagement.Core.DTOs.Movimentacao;
using StockManagement.Core.DTOs.Pessoa;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Entities.Movimentacao;
using StockManagement.Core.Entities.Pessoa;

namespace StockManagement.Application.AutoMapper
{
    public class EntitiesToViewModelsProfile : Profile
    {
        public EntitiesToViewModelsProfile()
        {
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<Produto, ProdutoDto>();

            CreateMap<Cliente, ClienteDto>();

            CreateMap<Estoque, EstoqueDto>();
        }
    }
}
