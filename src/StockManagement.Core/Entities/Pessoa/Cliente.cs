using StockManagement.Core.Entities.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Core.Entities.Pessoa
{
    public class Cliente : Entidade
    {
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public string Endereco { get; private set; }
        public string Observacao { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public ICollection<Venda> Vendas { get; private set; }

        public Cliente(string nome, string telefone, string email, 
                       string endereco, string observacao)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Endereco = endereco;
            Observacao = observacao;
        }
    }
}
