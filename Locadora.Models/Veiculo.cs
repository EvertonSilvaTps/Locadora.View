using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Veiculo
    {
        public readonly static string INSERTCATEGORIA = "INSERT INTO tblCategorias VALUES(@Nome, @Descricao, @Diaria);" +
                                                        "SELECT SCOPE_IDENTITY()";

        public readonly static string SELECTALLCATEGORIAS = @"SELECT Nome, Descricao, Diaria
                                                            FROM tblCategorias";

        public readonly static string SELECTCATEGORIAPORNOME = @"SELECT CategoriaId, Nome, Descricao, Diaria 
                                                            FROM tblCategorias c
                                                            WHERE c.Nome = @Nome";

        public readonly static string UPDATEDESCRICAOCATEGORIA = "UPDATE tblCategorias SET Descricao = @Descricao " +
                                                    "WHERE CategoriaId = @CategoriaId";

        public readonly static string UPDATEDIARIACATEGORIA = "UPDATE tblCategorias SET Diaria = @Diaria " +
                                            "WHERE CategoriaId = @CategoriaId";

        public readonly static string DELETECATEGORIA = "DELETE FROM tblCategorias " +
                                                "WHERE CategoriaId = @CategoriaId";


        public int VeiculoID { get; private set; }
        public int CategoriaID { get; private set; }
        public string Placa { get; private set; }
        public string Marca{ get; private set; }
        public string Modelo { get; private set; }
        public int Ano { get; private set; }
        public string StatusVeiculo { get; private set; }

        public Veiculo(int categoriaID, string placa,
                        string marca, string modelo,
                        int ano, string statusVeiculo)
        {
            CategoriaID = categoriaID;
            Placa = placa;
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
            StatusVeiculo = statusVeiculo;
        }

        public void setVeiculo(int veiculoID)
        {
            VeiculoID = veiculoID;
        }

        public void setStatusVeiculo(string statusVeiculo)
        {
            StatusVeiculo = statusVeiculo;
        }


        public override string? ToString()
        {
            return $"Placa: {Placa}\nMarca: {Marca}\nModelo: {Modelo}\n" +
                $"Ano: {Ano}\nStatus:  {StatusVeiculo}\n";
        }
    }
}
