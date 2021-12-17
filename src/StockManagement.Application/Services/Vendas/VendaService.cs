using AutoMapper;
using StockManagement.Application.InputModels.Vendas;
using StockManagement.Application.Interface.Services.Vendas;
using StockManagement.Application.Interfaces.Notification;
using StockManagement.Application.Validations.Vendas;
using StockManagement.Core.DTOs.Vendas;
using StockManagement.Core.Entities.Vendas;
using StockManagement.Core.Interfaces.Persistence.Repositories.Movimentacao;
using StockManagement.Core.Interfaces.Persistence.Repositories.Vendas;
using StockManagement.Shared.Pagination;
using System;
using System.Threading.Tasks;

namespace StockManagement.Application.Services.Vendas
{
    public class VendaService : BaseService, IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IVendaProdutoRepository _vendaProdutoRepository;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IMapper _mapper;

        public VendaService(INotificador notificador,
                            IVendaRepository vendaRepository,
                            IVendaProdutoRepository vendaProdutoRepository,
                            IMapper mapper, 
                            IEstoqueRepository estoqueRepository) : base(notificador)
        {
            _vendaRepository = vendaRepository;
            _vendaProdutoRepository = vendaProdutoRepository;
            _mapper = mapper;
            _estoqueRepository = estoqueRepository;
        }

        public async Task Adicionar(VendaInputModel vendaModel)
        {
            if (!ExecutarValidacao(new ValidacaoVenda(), vendaModel)) return;

            var venda = _mapper.Map<Venda>(vendaModel);
            var vendaProduto = _mapper.Map<VendaProduto>(vendaModel);

            vendaProduto.CalcularOSubTotalDaVenda();
            venda.CalcularOTotalDaVenda((double) vendaProduto.Subtotal);

            if(await _estoqueRepository.VerificarQuantidadeDeProdutoNoEstoque(vendaModel.ProdutoId,
                vendaModel.Quantidade))
            {
                AdicionarNotificacao("A quantidade do produto no estoque é inferior a requerida!");
                return;
            }

            _vendaRepository.Adicionar(venda);
            await _vendaRepository.UnitOfWork.Salvar();

            vendaProduto.AtribuirOIdDaVenda(venda.Id);

            _vendaProdutoRepository.Adicionar(vendaProduto);
            await _vendaProdutoRepository.UnitOfWork.Salvar();

            await _estoqueRepository.DecrementarQuantidadeProdutoNoEstoque((Guid) 
                                    vendaProduto.ProdutoId, (int) vendaProduto.Quantidade);
        }

        public async Task Remover(Guid id)
        {
            var vendaProduto = await _vendaProdutoRepository.ObterVendaProdutoPorVendaId(id);

            await _vendaProdutoRepository.Remover(vendaProduto.Id);
            await _vendaProdutoRepository.UnitOfWork.Salvar();

            await _vendaRepository.Remover(id);
            await _vendaRepository.UnitOfWork.Salvar();
        }

        public async Task AnularVenda(Guid vendaId, Guid produtoId, int quantidadeProduto)
        {
            await _vendaRepository.AnularVenda(vendaId);
            await _vendaRepository.UnitOfWork.Salvar();

            await _estoqueRepository.IncrementarQuantidadeProdutoNoEstoque(produtoId, quantidadeProduto);
            await _estoqueRepository.UnitOfWork.Salvar();
        }

        public async Task<VendaDto> ObterVendaPorId(Guid id)
        {
            return await _vendaRepository.ObterVendaPorId(id);
        }

        public async Task<PagedList<VendaDto>> ObterVendas(PaginationParams paginationParams)
        {
            return await _vendaRepository.ObterVendas(paginationParams);
        }

        public async Task<PagedList<VendaDto>> ObterVendasAnuladas(PaginationParams paginationParams)
        {
            return await _vendaRepository.ObterVendasAnuladas(paginationParams);
        }

        public async  Task<PagedList<VendaDto>> ObterVendasDeHoje(PaginationParams paginationParams)
        {
            return await _vendaRepository.ObterVendasDeHoje(paginationParams);
        }

        public async Task<PagedList<VendaDto>> ObterVendasPorMes(int mes, PaginationParams paginationParams)
        {
            return await _vendaRepository.ObterVendasPorMes(mes, paginationParams);
        }


        public async Task<Venda> ConsultarVendaPorId(Guid id)
        {
            return await _vendaRepository.ObterPorId(id);
        }

        public void Dispose()
        {
            _vendaRepository?.Dispose();
            _vendaProdutoRepository?.Dispose();
        }
    }
}
