using AutoMapper;
using StockManagement.Application.InputModels.Catalogo;
using StockManagement.Application.Interface.Services.Catalogo;
using StockManagement.Application.Validations.Catalogo;
using StockManagement.Core.DTOs.Catalogo;
using StockManagement.Core.Entities.Catalogo;
using StockManagement.Core.Interfaces.Notification;
using StockManagement.Core.Interfaces.Persistence.Repositories.Catalogo;
using StockManagement.Shared.Pagination;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Application.Services.Catalogo
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        public CategoriaService(INotificador notificador,
                                ICategoriaRepository categoriaRepository,
                                IMapper mapper) : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task Adicionar(CategoriaInputModel categoriaModel)
        {
            if (!ExecutarValidacao(new ValidacaoCategoria(), categoriaModel)) return;

            if(VerificarDescricaoParaAdicionar(categoriaModel.Descricao))
            {
                AdicionarNotificacao("Já existe uma Categoria com esta descrição!");
                return;
            }

            var categoria = _mapper.Map<Categoria>(categoriaModel);

            _categoriaRepository.Adicionar(categoria);

            await _categoriaRepository.UnitOfWork.Salvar();
        }

        public async Task Atualizar(CategoriaInputModel categoriaModel)
        {
            if (!ExecutarValidacao(new ValidacaoCategoria(), categoriaModel)) return;

            if(VerificarDescricaoParaAtualizar(categoriaModel.Id, categoriaModel.Descricao))
            {
                AdicionarNotificacao("Já existe uma Categoria com esta descrição!");
                return;
            }

            var categoria = _mapper.Map<Categoria>(categoriaModel);

            _categoriaRepository.Atualizar(categoria);

            await _categoriaRepository.UnitOfWork.Salvar();
        }

        public async Task Remover(Guid id)
        {
            if(_categoriaRepository.ObterCategoriaComProdutosPorId(id).Result.Produtos.Any())
            {
                AdicionarNotificacao("Esta categoria possui produtos cadastrados!");
                return;
            }

            await _categoriaRepository.Remover(id);

            await _categoriaRepository.UnitOfWork.Salvar();
        }

        public async Task<CategoriaDto> ObterPorId(Guid id)
        {
            return _mapper.Map<CategoriaDto>(await _categoriaRepository.ObterPorId(id));
        }

        public async Task<PagedList<CategoriaDto>> ObterTodos(PaginationParams paginationParams)
        {
            return await _categoriaRepository.ObterCategorias(paginationParams);
        }

        private bool VerificarDescricaoParaAdicionar(string descricao)
        {
            return _categoriaRepository.Buscar(c => c.Descricao == descricao).Result.Any();  
        }

        private bool VerificarDescricaoParaAtualizar(Guid id, string descricao)
        {
            return _categoriaRepository
                .Buscar(c => c.Descricao == descricao && c.Id != id).Result.Any();
        }

        public void Dispose()
        {
            _categoriaRepository?.Dispose();
        }
    }
}
