using AutoMapper;
using StockManagement.Application.InputModels.Movimentacao;
using StockManagement.Application.Interface.Services.Movimentacao;
using StockManagement.Application.Validations.Movimentacao;
using StockManagement.Core.DTOs.Movimentacao;
using StockManagement.Core.Entities.Movimentacao;
using StockManagement.Core.Interfaces.Notification;
using StockManagement.Core.Interfaces.Persistence.Repositories.Movimentacao;
using StockManagement.Shared.Pagination;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Application.Services.Movimentacao
{
    public class EstoqueService : BaseService, IEstoqueService
    {
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IMapper _mapper;

        public EstoqueService(INotificador notificador,
                              IEstoqueRepository estoqueRepository, 
                              IMapper mapper) : base(notificador)
        {
            _estoqueRepository = estoqueRepository;
            _mapper = mapper;
        }

        public async Task Adicionar(EstoqueInputModel estoqueModel)
        {
            if (!ExecutarValidacao(new ValidacaoEstoque(), estoqueModel)) return;

            if (VerificarProdutoNoEstoqueParaAdicionar(estoqueModel.ProdutoId))
            {
                AdicionarNotificacao("Este produto já foi adicionado ao estoque!");
                return;
            }

            var estoque = _mapper.Map<Estoque>(estoqueModel);

            _estoqueRepository.Adicionar(estoque);

            await _estoqueRepository.UnitOfWork.Salvar();
        }

        public async  Task Atualizar(EstoqueInputModel estoqueModel)
        {
            if (!ExecutarValidacao(new ValidacaoEstoque(), estoqueModel)) return;

            var estoque = _mapper.Map<Estoque>(estoqueModel);

            _estoqueRepository.Atualizar(estoque);

            await _estoqueRepository.UnitOfWork.Salvar();
        }

        public async Task Remover(Guid id)
        {
            await _estoqueRepository.Remover(id);

            await _estoqueRepository.UnitOfWork.Salvar();
        }

        public async Task<EstoqueDto> ObterPorId(Guid id)
        {
            return _mapper.Map<EstoqueDto>(await _estoqueRepository.ObterPorId(id));
        }

        public async Task<PagedList<EstoqueDto>> ObterProdutosEmAbaixaNoEstoque(PaginationParams paginationParams)
        {
            return await _estoqueRepository.ObterProdutosEmAbaixaNoEstoque(paginationParams);
        }

        public async Task<PagedList<EstoqueDto>> ObterProdutosNoEstoques(PaginationParams paginationParams)
        {
            return await _estoqueRepository.ObterProdutosNoEstoques(paginationParams);
        }

        public async Task<PagedList<EstoqueDto>> ObterProdutosVaziosNoEstoque(PaginationParams paginationParams)
        {
            return await _estoqueRepository.ObterProdutosVaziosNoEstoque(paginationParams);
        }

        public bool VerificarProdutoNoEstoqueParaAdicionar(Guid produtoId)
        {
            return _estoqueRepository.Buscar(e => e.ProdutoId == produtoId).Result.Any();
        }

        public void Dispose()
        {
            _estoqueRepository?.Dispose();
        }
    }
}
