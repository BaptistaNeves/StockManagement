using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StockManagement.Core.Interfaces.Notification;
using StockManagement.Core.Notification;
using System.Linq;

namespace StockManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _noticador;
        public MainController(INotificador noticador)
        {
            _noticador = noticador;
        }

        [NonAction]
        protected bool OperacaoValida()
        {
            return !_noticador.TemNotificacao();
        }

        [NonAction]
        protected ActionResult Resposta(object resultado = null)
        {
            if(OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = resultado
                });
            }

            return BadRequest(new
            {
                success = false,
                data = _noticador.ObterNoficacao().Select(e => e.Mensagem)
            });
        }

        [NonAction]
        public ActionResult Resposta(ModelStateDictionary modelSate)
        {
            if (!modelSate.IsValid) NotificarErroModelState(modelSate);

            return Resposta();
        }

        [NonAction]
        protected void NotificarErroModelState(ModelStateDictionary modelSate)
        {
            var erros = modelSate.Values.SelectMany(m => m.Errors);

            foreach (var error in erros)
            {
                NotificarErro(error.ErrorMessage);
            }
        }

        [NonAction]
        protected void NotificarErro(string mensagem)
        {
            _noticador.AdicionarNotificacao(new Notificacao(mensagem));
        }
    }
}
