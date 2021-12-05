using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Application.ViewModels.Pessoa
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public string Endereco { get; private set; }
        public string Observacao { get; private set; }
        public DateTime DataCadastro { get; private set; }

    }
}
