using Locadora.Models.Enums;

namespace Locadora.Models
{
    public class Locacao
    {
        public readonly static string INSERTLOCACAO = @"INSERT INTO tblLocacoes(ClienteID, VeiculoID, DataLocacao, DataDevolucaoPrevista, DataDevolucaoReal, 
                                                       ValorDiaria, ValorTotal, Multa, Status) 
                                                       VALUES (@ClienteID, @VeiculoID, @DataLocacao, @DataDevolucaoPrevista, @DataDevolucaoReal, 
                                                       @ValorDiaria, @ValorTotal, @Multa, @Status);";

        public readonly static string SELECTALLLOCACOES = @"SELECT LocacaoID, ClienteID, VeiculoID, DataLocacao, DataDevolucaoPrevista, DataDevolucaoReal, 
                                                          ValorDiaria, ValorTotal, Multa, Status 
                                                          FROM tblLocacoes;";

        public readonly static string SELECTLOCACOESBYID = @"SELECT LocacaoID, ClienteID, VeiculoID, DataLocacao, DataDevolucaoPrevista, DataDevolucaoReal, 
                                                           ValorDiaria, ValorTotal, Multa, Status 
                                                           FROM tblLocacoes 
                                                           WHERE LocacaoID = @LocacaoID";


        public readonly static string UPDATELOCACAODEVOLUCAOREAL = "UPDATE tblLocacoes SET DataDevolucaoReal = @DataDEvolucaoReal WHERE LocacaoID = @LocacaoID";
        
        
        public readonly static string UPDATELOCACAOSTATUS = @"UPDATE tblLocacoes SET Status = @Status, 
                                                            WHERE LocacaoID = @LocacaoID";

        public Guid LocacaoID { get; private set; }
        public Cliente Cliente { get; private set; }
        public Veiculo Veiculo { get; private set; }
        public DateTime DataLocacao { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public DateTime? DataDevolucaoReal { get; private set; }
        public decimal ValorDiaria { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal Multa { get; private set; }
        public EStatusLocacao Status { get; private set; }

        public Locacao(Cliente cliente, Veiculo veiculo, decimal valorDiaria, int diasLocacao)
        {
            Cliente = cliente;
            Veiculo = veiculo;
            DataLocacao = DateTime.Now;
            ValorDiaria = valorDiaria;
            ValorTotal = valorDiaria * diasLocacao;
            DataDevolucaoPrevista = DateTime.Now.AddDays(diasLocacao);
            DataDevolucaoReal = null;
            this.Multa = 0.5m * (decimal)this.Veiculo.Categoria.Diaria;    // CategoriaID  == Diaria
            Status = EStatusLocacao.Ativa;
        }


        public Locacao(Guid locacaoID, Cliente cliente, Veiculo veiculo, DateTime dataLocacao,
            DateTime dataDevolucaoPrevista, DateTime? dataDevolucaoReal, decimal valorDiaria,
            decimal valorTotal, decimal multa, string status)
        {
            LocacaoID = locacaoID;
            this.Cliente = cliente;
            this.Veiculo = veiculo;
            DataLocacao = dataLocacao;
            DataDevolucaoPrevista = dataDevolucaoPrevista;
            DataDevolucaoReal = dataDevolucaoReal;
            ValorDiaria = valorDiaria;
            ValorTotal = valorTotal;
            Multa = multa;
            Status = EStatusLocacao.Ativa;
        }

        public void setDataDevolucaoReal(DateTime dataDevolucao)
        {
            this.DataDevolucaoReal = dataDevolucao;
        }


        public override string? ToString()
        {
            return $"Id: {LocacaoID}\n" +
                $"Cliente: {this.Cliente.Nome}  |  Veiculo: {this.Veiculo.Modelo}\n" +
                $"Data de Locação: {DataLocacao}  |  Data de Devolução Prevista: {DataDevolucaoPrevista}  |  Data de Devolução Real: {DataDevolucaoReal}\n" +
                $"Valor da Diária: {ValorDiaria:C}  |  Valor Total: {ValorTotal:C}  |  Multa: {Multa:C} + diária por dia de atraso\n" +
                $"Status: {Status}\n" +
                $"----------------------------------------------------------------------------------------------";
        }
    }
}
