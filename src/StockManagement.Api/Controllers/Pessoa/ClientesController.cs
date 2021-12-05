using StockManagement.Application.Interfaces.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Api.Controllers.Pessoa
{
    public class ClientesController : MainController
    {
        public ClientesController(INotificador noticador) : base(noticador)
        {

        }
    }
}
