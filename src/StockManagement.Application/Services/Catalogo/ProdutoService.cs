using AutoMapper;
using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Application.Interface.Services.Catalogo;
using StockManagement.Application.Interfaces.Notification;
using StockManagement.Application.Validations.Catalogo;
using StockManagement.Application.ViewModels.Catalogo;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Interfaces.Persistence.Repositories.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.Services.Catalogo
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public ProdutoService(INotificador notificador,
                              IProdutoRepository produtoRepository, 
                              IMapper mapper) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }
        public async Task Adicionar(ProdutoInputModel produtoModel)
        {
            if (!ExecutarValidacao(new ValidacaoProduto(), produtoModel)) return;

            if (VerificarNomeParaAdicionar(produtoModel.Nome))
            {
                AdicionarNotificacao("Já existe um Produto cadastrado com este nome!");
                return;
            }

            var produto = _mapper.Map<Produto>(produtoModel);

            _produtoRepository.Adicionar(produto);

            await _produtoRepository.UnitOfWork.Salvar();
        }

        public async Task Atualizar(ProdutoInputModel produtoModel)
        {
            if (!ExecutarValidacao(new ValidacaoProduto(), produtoModel)) return;

            if (VerificarNomeParaAtualizar(produtoModel.Id, produtoModel.Nome))
            {
                AdicionarNotificacao("Já existe um Produto cadastrado com este nome!");
                return;
            }

            var produto = _mapper.Map<Produto>(produtoModel);

            _produtoRepository.Atualizar(produto);

            await _produtoRepository.UnitOfWork.Salvar();
        }
        public async Task Remover(Guid id)
        {
            if(_produtoRepository.ObterProdutoVendasEstoquePorId(id).Result.Vendas.Any()
               || _produtoRepository.ObterProdutoVendasEstoquePorId(id).Result.Estoque != null)
            {
                AdicionarNotificacao("Este Produto não pode ser removido" +
                                    " foi cadastrado no estoque ou possui vendas registradas!");
            }

            _produtoRepository.Remover(id);

            await _produtoRepository.UnitOfWork.Salvar();
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
        }

        public async Task<ICollection<ProdutoViewModel>> ObterTodos()
        {
            return _mapper.Map<ICollection<ProdutoViewModel>>
                   (await _produtoRepository.ObterTodos());
        }

        private bool VerificarNomeParaAdicionar(string nome)
        {
            return _produtoRepository.Buscar(p => p.Nome == nome).Result.Any();
        }

        private bool VerificarNomeParaAtualizar(Guid id, string nome)
        {
            return _produtoRepository
                .Buscar(p => p.Nome == nome && p.Id != id).Result.Any();
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
