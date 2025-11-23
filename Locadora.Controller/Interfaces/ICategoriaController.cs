using Locadora.Models;

namespace Locadora.Controller.Interfaces
{
    public interface ICategoriaController
    {
        public void AdicionarCategoria(Categoria categoria);

        public List<Categoria> ListarTodasCategorias();

        public Categoria BuscarNomeCategoriaPorId(int id);

        public Categoria BuscarCategoriaNome(string nome);

        public void AtualizarDescricaoCategoria(string descricao, string nome);

        public void AtualizarDiariaCategoria(decimal diaria, string nome);

        public void DeletarCategoria(string nome);
    }
}
