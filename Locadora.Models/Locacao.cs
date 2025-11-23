using Locadora.Models.Enums;

namespace Locadora.Models
{
    public class Locacao
    {
        public Guid LocacaoID { get; private set; }
        public int ClienteID { get; private set; }
        public int VeiculoID { get; private set; }
        public DateTime DataLocacao { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public DateTime? DataDevolucaoReal { get; private set; }
        public decimal ValorDiaria { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal Multa { get; private set; }
        public EStatusLocacao Status { get; private set; }

        public Locacao(int clienteID, int veiculoID, decimal valorDiaria, int diasLocacao)
        {
            ClienteID = clienteID;
            VeiculoID = veiculoID;
            DataLocacao = DateTime.Now;
            ValorDiaria = valorDiaria;
            ValorTotal = valorDiaria * diasLocacao;
            DataDevolucaoPrevista = DateTime.Now.AddDays(diasLocacao);
            Status = EStatusLocacao.Ativa;
        }

        //TODO: Definir os valores de cliente e veículo como nome e modelo respectivamente
        public override string? ToString()
        {
            return $"Cliente ID: {ClienteID}  |  Veiculo ID: {VeiculoID}\n" +
            $"Data de Locação: {DataLocacao}  |  Data de Devolução Prevista: {DataDevolucaoPrevista}  |  Data de Devolução Real: {DataDevolucaoReal}\n" +
            $"Valor da Diária: {ValorDiaria:C}  |  Valor Total: {ValorTotal:C}  |  Multa: {Multa:C}  |  Status: {Status}\n" +
            $"--------------------------------------------------------------";
        }
    }
}
