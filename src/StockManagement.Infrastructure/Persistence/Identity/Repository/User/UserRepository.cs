using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockManagement.Core.DTOs.Usuario;
using StockManagement.Core.Entities.Usuario;
using StockManagement.Core.Interfaces.Notification;
using StockManagement.Core.Interfaces.Persistence.Repositories.Usuarios;
using StockManagement.Core.Notification;
using StockManagement.Infraestructure.Persistence.Identity.Models;
using StockManagement.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Infrastructure.Persistence.Identity.Repository.User
{
    public class UserRepository : IUsuarioRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly INotificador _notificador;
        public UserRepository(UserManager<AppUser> userManager,
                              RoleManager<AppRole> roleManager,
                              AppDbContext context,
                              INotificador notificador, 
                              SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _notificador = notificador;
            _signInManager = signInManager;
        }
        public async Task Adicionar(Usuario usuario)
        {
            var appUser = new AppUser
            {
                Id = usuario.Id,
                NomeDeUtilizador = usuario.NomeDeUtilizador,
                UserName = usuario.Email,
                Email = usuario.Email,
                PhoneNumber = usuario.Telefone,
                Endereco = usuario.Endereco
            };

            var resultado = await _userManager.CreateAsync(appUser, usuario.Senha);

            if(!resultado.Succeeded)
            {
                AdicionarErroDeIdentityNaNotificacao(resultado);
                return;
            }

            await _userManager.AddToRoleAsync(appUser, usuario.Perfil);
        }

        public async Task Atualizar(Usuario usuario)
        {
            var usuarioAtualizar = await _userManager.FindByIdAsync(usuario.Id.ToString());

            await _userManager.UpdateSecurityStampAsync(usuarioAtualizar);

            usuarioAtualizar.NomeDeUtilizador = usuario.NomeDeUtilizador;
            usuarioAtualizar.UserName = usuario.Email;
            usuarioAtualizar.PhoneNumber = usuario.Telefone;
            usuarioAtualizar.Email = usuario.Email;
            usuarioAtualizar.Endereco = usuario.Endereco;

            var resultado = await _userManager.UpdateAsync(usuarioAtualizar);

            if(!resultado.Succeeded)
            {
                AdicionarErroDeIdentityNaNotificacao(resultado);
                return;
            }
        }

        public async Task Remover(Guid id)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());

            var resultado = await _userManager.DeleteAsync(usuario);

            if(!resultado.Succeeded)
            {
                AdicionarErroDeIdentityNaNotificacao(resultado);
                return;
            }
        }

        public async Task<UsuarioDto> ObterPorId(Guid id)
        {
            return await _context.Users.AsNoTracking()
                .Where(u => u.Id == id)
                .Select(usuario => new UsuarioDto
                {
                    Id = usuario.Id,
                    Nome = usuario.NomeDeUtilizador,
                    Email = usuario.Email,
                    Telefone = usuario.PhoneNumber,
                    Endereco = usuario.Endereco
                }).SingleOrDefaultAsync();
        }

        public async Task<UsuarioDto> ObterPorEmail(string email)
        {
            return await _context.Users.AsNoTracking()
                .Where(u => u.Email == email)
                .Select(usuario => new UsuarioDto
                {
                    Id = usuario.Id,
                    Nome = usuario.NomeDeUtilizador,
                    Email = usuario.Email,
                    Telefone = usuario.PhoneNumber,
                    Endereco = usuario.Endereco
                }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UsuarioDto>> ObterTodos()
        {
            return await _context.Users.AsNoTracking()
                .Select(usuario => new UsuarioDto
                {
                    Id = usuario.Id,
                    Nome = usuario.NomeDeUtilizador,
                    Email = usuario.Email,
                    Telefone = usuario.PhoneNumber,
                    Endereco = usuario.Endereco
                }).ToListAsync();
        }

        public async Task<UsuarioDto> ObterUsuarioERolesPorId(Guid id)
        {
            return await _context.Users.AsNoTracking()
                           .Where(u => u.Id == id)
                           .Include(ur => ur.UserRoles)
                           .ThenInclude(r => r.Role)
                           .Select(usuario => new UsuarioDto
                           {
                               Id = usuario.Id,
                               Nome = usuario.NomeDeUtilizador,
                               Email = usuario.Email,
                               Telefone = usuario.PhoneNumber,
                               Endereco = usuario.Endereco,
                               Perfil = usuario.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault()
                           }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UsuarioDto>> ObterUsuariosERoles()
        {
            return await _context.Users.AsNoTracking()
                           .Include(ur => ur.UserRoles)
                           .ThenInclude(r => r.Role)
                           .Select(usuario => new UsuarioDto
                           {
                               Id = usuario.Id,
                               Nome = usuario.NomeDeUtilizador,
                               Email = usuario.Email,
                               Telefone = usuario.PhoneNumber,
                               Endereco = usuario.Endereco,
                               Perfil = usuario.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault()
                           }).ToListAsync();
        }

        private void AdicionarErroDeIdentityNaNotificacao(IdentityResult resultado)
        {
            foreach(var erro in resultado.Errors)
            {
                _notificador.AdicionarNotificacao(new Notificacao(erro.Description));
            }
        }

        public async Task AlterarSenha(Guid id, string senhaAntiga, string senhaNova)
        {
            var usuario = await _userManager.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);

            var resultado = await _userManager.ChangePasswordAsync(usuario, senhaAntiga, senhaNova);

            if(!resultado.Succeeded)
            {
                AdicionarErroDeIdentityNaNotificacao(resultado);
                return;
            }
        }
        public async Task<string> ObterRoleDoUsuarioPorId(Guid id)
        {
            return await _context.Users.AsNoTracking()
                                 .Where(u => u.Id == id)
                                 .Include(ur => ur.UserRoles)
                                 .ThenInclude(r => r.Role)
                                 .Select(ur => ur.UserRoles.Select(r => r.Role.Name).FirstOrDefault())
                                 .SingleOrDefaultAsync();
        }

        public async Task<UsuarioComRolesDto> ObterUsuarioERolesPorEmail(string email)
        {
            return await _context.Users.AsNoTracking()
                                .Where(u => u.Email == email)
                                .Include(ur => ur.UserRoles)
                                .ThenInclude(r => r.Role)
                                .Select(usuario => new UsuarioComRolesDto
                                {
                                    Id = usuario.Id,
                                    Nome = usuario.NomeDeUtilizador,
                                    Email = usuario.Email,
                                    Roles = usuario.UserRoles.Select(ur => ur.Role.Name)
                                }).SingleOrDefaultAsync();

        }

        public async Task AlterarPerfilDoUsuario(Guid id, string perfil)
        {
            var usuario = await _userManager.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);

            var perfilAntigoDoUsuario = await ObterRoleDoUsuarioPorId(id);

            await _userManager.RemoveFromRoleAsync(usuario, perfilAntigoDoUsuario);

            var resultado = await _userManager.AddToRoleAsync(usuario, perfil);

            if(!resultado.Succeeded)
            {
                AdicionarErroDeIdentityNaNotificacao(resultado);
                return;
            }
        }

        public async Task<IEnumerable<string>> ObterPerfilDoUsuarioPorEmail(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            return await _userManager.GetRolesAsync(usuario);
        }
        public async Task<bool> Login(string email, string senha)
        {
            var resultado = await _signInManager.PasswordSignInAsync(email, senha, false, true);

            if(resultado.Succeeded)
            {
                return true;
            }

            if(resultado.IsLockedOut)
            {
                _notificador.AdicionarNotificacao(new Notificacao("Usuário temporariamente bloqueado por tentativas inválidas!"));
                return false;
            }

            _notificador.AdicionarNotificacao(new Notificacao("Usuário ou Senha incorretos!"));
            return false;
        }

        public void Dispose()
        {
            _userManager?.Dispose();
            _roleManager?.Dispose();
        }
    }
}
