﻿using AutoMapper;
using StockManagement.Application.InputModels.Pessoa;
using StockManagement.Application.Interface.Services.Pessoa;
using StockManagement.Application.Interfaces.Notification;
using StockManagement.Application.Validations.Pessoa;
using StockManagement.Application.ViewModels.Pessoa;
using StockManagement.Core.Entities.Pessoa;
using StockManagement.Core.Interfaces.Persistence.Repositories.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Application.Services.Pessoa
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(INotificador notificador,
                              IClienteRepository clienteRepository, 
                              IMapper mapper) : base(notificador)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task Adicionar(ClienteInputModel clienteModel)
        {
            if (!ExecutarValidacao(new ValidacaoCliente(), clienteModel)) return;

            if (VerificarEmailParaAdicionar(clienteModel.Email))
            {
                AdicionarNotificacao("Já existe um Cliente com este Email!");
                return;
            }

            var cliente = _mapper.Map<Cliente>(clienteModel);

            _clienteRepository.Adicionar(cliente);

            await _clienteRepository.UnitOfWork.Salvar();
        }

        public async Task Atualizar(ClienteInputModel clienteModel)
        {
            if (!ExecutarValidacao(new ValidacaoCliente(), clienteModel)) return;

            if (VerificarEmailParaAtualizar(clienteModel.Id, clienteModel.Email))
            {
                AdicionarNotificacao("Já existe um Cliente com este Email!");
                return;
            }

            var cliente = _mapper.Map<Cliente>(clienteModel);

            _clienteRepository.Atualizar(cliente);

            await _clienteRepository.UnitOfWork.Salvar();
        }

        public async Task Remover(Guid id)
        {
            if (_clienteRepository.ObterClienteVendasPorId(id).Result.Vendas.Any())
            {
                AdicionarNotificacao("Este Cliente possui vendas registradas não pode ser excluido!");
                return;
            }

            await _clienteRepository.Remover(id);

            await _clienteRepository.UnitOfWork.Salvar();
        }

        public async Task<ClienteViewModel> ObterPorId(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.ObterPorId(id));
        }

        public async Task<ICollection<ClienteViewModel>> ObterTodos()
        {
            return _mapper.Map<ICollection<ClienteViewModel>>(await _clienteRepository.ObterTodos());
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
        }
        private bool VerificarEmailParaAdicionar(string email)
        {
            return _clienteRepository.Buscar(c => c.Email == email).Result.Any();
        }

        private bool VerificarEmailParaAtualizar(Guid id, string email)
        {
            return _clienteRepository
                .Buscar(c => c.Email == email && c.Id != id).Result.Any();
        }
    }
}
