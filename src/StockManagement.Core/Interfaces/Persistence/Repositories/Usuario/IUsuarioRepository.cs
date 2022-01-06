using StockManagement.Core.DTOs.Usuario;
using StockManagement.Core.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagement.Core.Interfaces.Persistence.Repositories.Usuarios
{
    public interface IUsuarioRepository : IDisposable
    {
        Task Adicionar(Usuario usuario);
        Task Atualizar(Usuario usuario);
        Task Remover(Guid id);
        Task AlterarSenha(Guid id, string senhaAntiga, string senhaNova);
        Task AlterarPerfilDoUsuario(Guid id, string perfil);
        Task<IEnumerable<UsuarioDto>> ObterUsuariosERoles();
        Task<UsuarioDto> ObterUsuarioERolesPorId(Guid id);
        Task<UsuarioDto> ObterPorId(Guid id);
        Task<UsuarioDto> ObterPorEmail(string email);
        Task<IEnumerable<UsuarioDto>> ObterTodos();
        Task<string> ObterRoleDoUsuarioPorId(Guid id);
        Task<UsuarioComRolesDto> ObterUsuarioERolesPorEmail(string email);
        Task<IEnumerable<string>> ObterPerfilDoUsuarioPorEmail(string email);
        Task<bool> Login(string email, string senha);
    }
}
