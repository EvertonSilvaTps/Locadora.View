using Locadora.Models;

namespace Locadora.Controller.Interfaces
{
    public interface ILocacaoController
    {
        public void AdicionarLocacao(Locacao locacao);

        public void AtualizarStatusLocacao(Guid id, string status);

        public Locacao? BuscarLocacaoPorId(Guid id);

        public List<Locacao> ListarTodasLocacoes();

        public List<Locacao> ListarLocacaoPorCliente(int clienteId);

        public List<Locacao> ListarLocacaoPorStatus(string status);

    }
}
