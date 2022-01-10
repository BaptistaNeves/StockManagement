using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.InputModels.Usuarios;
using StockManagement.Application.Interface.Services.Usuarios;
using StockManagement.Core.DTOs.Usuario;
using StockManagement.Core.Interfaces.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Api.Controllers.Usuarios
{
    public class UsuariosController : MainController
    {
        private readonly IUsuarioService _usuarioService;
        public UsuariosController(INotificador noticador,
                                  IUsuarioService usuarioService) : base(noticador)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("obter-usuarios")]
        public async Task<ActionResult<ICollection<UsuarioDto>>> ObterTodos()
        {
            return Ok(await _usuarioService.ObterUsuariosERoles());
        }

        [HttpGet("obter-usuario-por-id/{id:guid}")]
        public async Task<ActionResult<UsuarioDto>> ObterPorId(Guid id)
        {
            return Ok(await _usuarioService.ObterUsuarioERolesPorId(id));
        }

        [HttpPost("novo-usuario")]
        public async Task<ActionResult> Adicionar(UsuarioInputModel usuario)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _usuarioService.Adicionar(usuario);

            return Resposta(usuario);
        }

        [HttpPut("atualizar-usuario/{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, UsuarioInputModel usuario)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            await _usuarioService.Atualizar(usuario);

            return Resposta(usuario);
        }

        [HttpPut("alterar-senha/{id:guid}")]
        public async Task<ActionResult> AlterarSenha(Guid id, string senhaAntiga, string senhaNova)
        {
            await _usuarioService.AlterarSenha(id, senhaAntiga, senhaNova);

            return Resposta();
        }

        [HttpPut("alterar-perfil/{id:guid}")]
        public async Task<ActionResult> AlterarPerfil(Guid id, string perfil)
        {
            await _usuarioService.AlterarPerfilDoUsuario(id, perfil);

            return Resposta();
        }

        [HttpDelete("remover-usuario/{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            await _usuarioService.Remover(id);

            return Resposta();
        }
    }
}
