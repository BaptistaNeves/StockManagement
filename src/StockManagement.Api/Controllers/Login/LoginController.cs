using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.InputModels.Usuarios;
using StockManagement.Application.Interface.Services.Usuarios;
using StockManagement.Core.Interfaces.Notification;
using StockManagement.Core.Interfaces.Token;
using System.Threading.Tasks;

namespace StockManagement.Api.Controllers.Login
{
    public class LoginController : MainController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        public LoginController(INotificador noticador, 
                               IUsuarioService usuarioService) : base(noticador)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginInputModel loginModel)
        {
            if (!ModelState.IsValid) return Resposta(ModelState);

            if(await _usuarioService.Login(loginModel.Email, loginModel.Senha))
            {
                return Resposta(await _tokenService.CreateToken(loginModel.Email));
            }

            return Resposta();
        }
    }
}
