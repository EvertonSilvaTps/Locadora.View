using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Categoria
    {
        public readonly static string INSERTCATEGORIA = "INSERT INTO tblCategorias VALUES(@Nome, @Descricao, @Diaria)";

        //public readonly static string INSERTCATEGORIA_ = "EXEC sp_INSERIRCATEGORIA @nOME, @Descricao, @Diaria";

        public readonly static string SELECTALLCATEGORIAS = @"SELECT Nome, Descricao, Diaria
                                                            FROM tblCategorias";

        // *Por ID depois ajustar onde precisar inclusive o buscar
        public readonly static string SELECTCATEGORIAPORNOME = @"SELECT CategoriaId, Nome, Descricao, Diaria 
                                                            FROM tblCategorias c
                                                            WHERE c.Nome = @Nome";

        public readonly static string SELECTNOMECATEGORIAPORID = "SELECT Nome FROM tblCategorias WHERE CategoriaID = @Id";  // novo

        public readonly static string UPDATEDESCRICAOCATEGORIA = "UPDATE tblCategorias SET Descricao = @Descricao " +
                                                    "WHERE CategoriaId = @CategoriaId";

        public readonly static string UPDATEDIARIACATEGORIA = "UPDATE tblCategorias SET Diaria = @Diaria " +
                                            "WHERE CategoriaId = @CategoriaId";

        public readonly static string DELETECATEGORIA = "DELETE FROM tblCategorias " +
                                                "WHERE CategoriaId = @CategoriaId";
        public int CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public decimal Diaria { get; private set; }

        public Categoria(string nome, decimal diaria)
        {
            Nome = nome;
            Diaria = diaria;
        }

        public Categoria(string nome, decimal diaria, string? descricao) : this (nome, diaria)
        {
            Descricao = descricao;
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
