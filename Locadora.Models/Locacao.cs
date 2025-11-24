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


        public readonly static string SELECTLOCACAOPORCLIENTE = @"SELECT LocacaoID, ClienteID, VeiculoID, DataLocacao, DataDevolucaoPrevista, DataDevolucaoReal, 
                                                                 ValorDiaria, ValorTotal, Multa, Status 
                                                                 FROM tblLocacoes WHERE ClienteID = @ClienteID";

        public readonly static string SELECTLOCACAOPORSTATUS = @"SELECT LocacaoID, ClienteID, VeiculoID, DataLocacao, DataDevolucaoPrevista, DataDevolucaoReal, 
                                                                        ValorDiaria, ValorTotal, Multa, Status 
                                                                        FROM tblLocacoes WHERE Status = @Status";


        public readonly static string UPDATELOCACAODEVOLUCAOREAL = "UPDATE tblLocacoes SET DataDevolucaoReal = @DataDevolucaoReal WHERE LocacaoID = @LocacaoID";

        public readonly static string UPDATELOCACAOVALORTOTAL = "UPDATE tblLocacoes SET ValorTotal = @ValorTotal WHERE LocacaoID = @LocacaoID";

        public readonly static string UPDATELOCACAOSTATUS = @"UPDATE tblLocacoes SET Status = @Status 
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
        public string Status { get; private set; }


        // construtor usado para criar nova locação
        public Locacao(Cliente cliente, Veiculo veiculo, int diasLocacao)
        {
            Cliente = cliente;
            Veiculo = veiculo;
            DataLocacao = DateTime.Now;
            ValorDiaria = Veiculo.Categoria.Diaria;
            ValorTotal = Veiculo.Categoria.Diaria * diasLocacao;
            DataDevolucaoPrevista = DateTime.Now.AddDays(diasLocacao);
            DataDevolucaoReal = null;
            this.Multa = 0.5m * (decimal)this.Veiculo.Categoria.Diaria;    // CategoriaID  == Diaria
        }


        // construtor completo para uso de exibição
        public Locacao(Guid locacaoID, Cliente cliente, Veiculo veiculo, DateTime dataLocacao, DateTime dataDevolucaoPrevista, DateTime? dataDevolucaoReal, decimal valorDiaria, decimal valorTotal, decimal multa, string status)
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
            Status = status;
        }


        // Construtor para fazer o select de locação por cliente e funcionario.
        public Locacao(int clienteID, int veiculoID, DateTime daLocacao, DateTime dataDevolucao, decimal valorDiaria, string? status)
        {
        }


        public void setDataDevolucaoReal(DateTime dataDevolucao)
        {
            this.DataDevolucaoReal = dataDevolucao;
        }


        public override string? ToString()
        {
            return $"Id: {LocacaoID}  |  Cliente: {this.Cliente.Nome}  |  Veiculo: {this.Veiculo.Modelo}  |  Status: {Status}\n" +
                $"Data de Locação: {DataLocacao:dd/MM/yyyy}  |  Data de Devolução Prevista: {DataDevolucaoPrevista:dd/MM/yyyy}  |  Data de Devolução Real: {DataDevolucaoReal:dd/MM/yyyy}\n" +
                $"Valor da Diária: {ValorDiaria:C}  |  Valor Total: {ValorTotal:C}  |  Multa: {Multa:C} + diária por dia de atraso\n" +
                $"------------------------------------------------------------------------------------------------------------------";
        }
    }
}
