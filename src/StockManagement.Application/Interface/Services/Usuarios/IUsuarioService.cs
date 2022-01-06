using StockManagement.Application.InputModels.Usuarios;
using StockManagement.Core.DTOs.Usuario;
using StockManagement.Core.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagement.Application.Interface.Services.Usuarios
{
    public interface IUsuarioService : IDisposable
    {
        Task Adicionar(UsuarioInputModel usuarioModel);
        Task Atualizar(UsuarioInputModel usuarioModel);
        Task Remover(Guid id);
        Task AlterarSenha(Guid id, string senhaAntiga, string senhaNova);
        Task AlterarPerfilDoUsuario(Guid id, string perfil);
        Task<IEnumerable<UsuarioDto>> ObterUsuariosERoles();
        Task<UsuarioDto> ObterUsuarioERolesPorId(Guid id);
        Task<UsuarioComRolesDto> ObterUsuarioERolesPorEmail(string email);
        Task<bool> Login(string email, string senha);
    }
}
