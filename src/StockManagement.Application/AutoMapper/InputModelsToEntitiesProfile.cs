using AutoMapper;
using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Application.InputModels.Movimentacao;
using StockManagement.Application.InputModels.Pessoa;
using StockManagement.Application.InputModels.Vendas;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Entities.Movimentacao;
using StockManagement.Core.Entities.Pessoa;
using StockManagement.Core.Entities.Vendas;

namespace StockManagement.Application.AutoMapper
{
    public class InputModelsToEntitiesProfile : Profile
    {
        public InputModelsToEntitiesProfile()
        {
            CreateMap<CategoriaInputModel, Categoria>();
            CreateMap<ProdutoInputModel, Produto>();

            CreateMap<ClienteInputModel, Cliente>();

            CreateMap<EstoqueInputModel, Estoque>();

            CreateMap<VendaInputModel, Venda>();
            CreateMap<VendaInputModel, VendaProduto>();
        }
    }
}
