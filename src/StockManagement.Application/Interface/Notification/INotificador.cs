using StockManagement.Application.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.Interfaces.Notification
{
    public interface INotificador
    {
        void AdicionarNotificacao(Notificacao notificacao);
        List<Notificacao> ObterNoficacao();
        bool TemNotificacao();
    }
}
