using Locadora.Models;

namespace Locadora.Controller.Interfaces
{
    public interface ILocacaoController
    {
        public void AdicionarLocacao(Locacao locacao);

        public List<Locacao> ListarTodasLocacoes();

        public Locacao BuscarLocacaoPorNomeCliente(string nomeCliente);

        public void AtualizarVeiculoIDLocacao(string nomeCliente, string placa);
        
        public void AtualizarDiasLocacao(decimal nomeCliente, string diasLocacao);

        public void AtualizarStatus(decimal nomeCliente, string diasLocacao);
    }
}
