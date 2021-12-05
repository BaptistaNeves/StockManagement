﻿using StockManagement.Core.Entities.Movimentacao;
using StockManagement.Core.Entities.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Core.Entities.Catalogo
{
    public class Produto : Entidade
    {
        public Guid? CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public double? Preco { get; private set; }
        public bool? Estocavel { get; private set; }
        public int? EstoqueMinimo { get; private set; }
        public string Imagem { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public Categoria Categoria { get; private set; }
        public Estoque Estoque { get; private set; }
        public ICollection<VendaProduto> Vendas { get; private set; }

        public Produto(Guid categoriaId, string nome, double preco, bool estocavel, 
                       int estoqueMinimo, string imagem, string descricao, 
                       DateTime dataCadastro)
        {
            CategoriaId = categoriaId;
            Nome = nome;
            Preco = preco;
            Estocavel = estocavel;
            EstoqueMinimo = estoqueMinimo;
            Imagem = imagem;
            Descricao = descricao;
            DataCadastro = dataCadastro;
        }
    }
}
