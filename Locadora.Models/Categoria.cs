using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Categoria
    {
        public int CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public decimal Diaria { get; private set; }

        public Categoria(string nome, decimal diaria)
        {
            Nome = nome;
            Diaria = diaria;
        }

        public Categoria(string nome, string? descricao, decimal diaria)
        {
            Nome = nome;
            Descricao = descricao;
            Diaria = diaria;
        }

        public void setCategoriaId(int categoriaId)
        {
            CategoriaId = categoriaId;
        }

        public void setDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void setDiaria(decimal diaria)
        {
            Diaria = diaria;
        }

        public override string? ToString()
        {
            return $"Nome: {Nome}\nDescrição: {(Descricao != null ? Descricao : "Sem descrição")}\nDiária: {Diaria}\n";
        }


    }
}
