using StockManagement.Core.Notification;
using System.Collections.Generic;

namespace StockManagement.Core.Interfaces.Notification
{
    public interface INotificador
    {
        void AdicionarNotificacao(Notificacao notificacao);
        List<Notificacao> ObterNoficacao();
        bool TemNotificacao();
    }
}
