using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockManagement.Api.Extensions;
using StockManagement.Application.Interface.Services.Usuarios;
using StockManagement.Core.DTOs.Usuario;
using StockManagement.Core.Interfaces.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;
        private readonly IUsuarioService _usuarioService;
        public TokenService(IOptions<AppSettings> appSettings, 
                            IUsuarioService usuarioService)
        {
            _appSettings = appSettings.Value;
            _usuarioService = usuarioService;
        }
        public async Task<RespostaDeLoginDto> CreateToken(string email)
        {
            var usuario = await _usuarioService.ObterUsuarioERolesPorEmail(email);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, usuario.Nome)
            };

            claims.AddRange(usuario.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new RespostaDeLoginDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Roles.FirstOrDefault(),
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
