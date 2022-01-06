using AutoMapper;
using StockManagement.Application.InputModels.Usuarios;
using StockManagement.Application.Interface.Services.Usuarios;
using StockManagement.Application.Validations.Usuarios;
using StockManagement.Core.DTOs.Usuario;
using StockManagement.Core.Entities.Usuario;
using StockManagement.Core.Interfaces.Notification;
using StockManagement.Core.Interfaces.Persistence.Repositories.Usuarios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagement.Application.Services.Usuarios
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioService(INotificador notificador,
                              IUsuarioRepository usuarioRepository, 
                              IMapper mapper) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task Adicionar(UsuarioInputModel usuarioModel)
        {
            if (!ExecutarValidacao(new ValidacaoUsuario(), usuarioModel)) return;

            var usuario = _mapper.Map<Usuario>(usuarioModel);

            await _usuarioRepository.Adicionar(usuario);
        }

        public async Task Atualizar(UsuarioInputModel usuarioModel)
        {
            if (!ExecutarValidacao(new ValidacaoUsuario(), usuarioModel)) return;

            var usuario = _mapper.Map<Usuario>(usuarioModel);

            await _usuarioRepository.Atualizar(usuario);
        }

        public async Task Remover(Guid id)
        {
            await _usuarioRepository.Remover(id);
        }

        public async Task<UsuarioComRolesDto> ObterUsuarioERolesPorEmail(string email)
        {
            return await _usuarioRepository.ObterUsuarioERolesPorEmail(email);
        }

        public async Task<UsuarioDto> ObterUsuarioERolesPorId(Guid id)
        {
            return await _usuarioRepository.ObterUsuarioERolesPorId(id);
        }

        public async Task<IEnumerable<UsuarioDto>> ObterUsuariosERoles()
        {
            return await _usuarioRepository.ObterUsuariosERoles();
        }

        public async Task AlterarSenha(Guid id, string senhaAntiga, string senhaNova)
        {
            await _usuarioRepository.AlterarSenha(id, senhaAntiga, senhaNova);
        }

        public async Task AlterarPerfilDoUsuario(Guid id, string perfil)
        {
            await _usuarioRepository.AlterarPerfilDoUsuario(id, perfil);
        }

        public async Task<bool> Login(string email, string senha)
        {
            if (!await _usuarioRepository.Login(email, senha)) return false;

            return true;
        }

        public void Dispose()
        {
            _usuarioRepository?.Dispose();
        }
    }
}
