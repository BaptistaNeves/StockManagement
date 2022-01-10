using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.InputModels.Pessoa;
using StockManagement.Application.Interface.Services.Pessoa;
using StockManagement.Core.DTOs.Pessoa;
using StockManagement.Core.Interfaces.Notification;
using StockManagement.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagement.Api.Controllers.Pessoa
{
    [Authorize(Roles ="Admin, Moderador")]
    public class ClientesController : MainController
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClientesController(INotificador noticador,
                                  IClienteService clienteService, 
                                  IMapper mapper) : base(noticador)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        [HttpGet("obter-clientes")]
        public async Task<ActionResult<ICollection<ClienteDto>>> ObterTodos([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _clienteService.ObterTodos(paginationParams));
        }

        [HttpGet("obter-cliente-por-id/{id:guid}")]
        public async Task<ActionResult<ClienteDto>> ObterPorId(Guid id)
        {
            return Ok(await _clienteService.ObterPorId(id));
        }

        [HttpPost("novo-cliente")]
        public async Task<ActionResult> Adicionar(ClienteInputModel clienteModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _clienteService.Adicionar(clienteModel);

            return Resposta(clienteModel);
        }

        [HttpPut("editar-cliente/{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, ClienteInputModel clienteModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _clienteService.Atualizar(clienteModel);

            return Resposta(clienteModel);
        }

        [HttpDelete("remover-cliente/{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var cliente = await _clienteService.ObterPorId(id);

            if (cliente == null) return NotFound();

            await _clienteService.Remover(id);

            return Resposta();
        }
    }
}
