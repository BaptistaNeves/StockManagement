using FluentValidation;
using FluentValidation.Results;
using StockManagement.Application.Interfaces.Notification;
using StockManagement.Application.Notification;

namespace StockManagement.Application.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        public BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : class
        {
            var resultadoValidacao = validacao.Validate(entidade);

            if (resultadoValidacao.IsValid) return true;

            AdicionarNotificacao(resultadoValidacao);

            return false;
        }

        protected void AdicionarNotificacao(ValidationResult validationResult)
        {
            foreach(var erro in validationResult.Errors)
            {
                AdicionarNotificacao(erro.ErrorMessage);
            }
        }

        protected void AdicionarNotificacao(string mensagem)
        {
            _notificador.AdicionarNotificacao(new Notificacao(mensagem));
        }
    }
}
