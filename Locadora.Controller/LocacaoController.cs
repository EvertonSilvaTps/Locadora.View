using Locadora.Controller.Interfaces;
using Locadora.Models;
using Microsoft.Data.SqlClient;
using Utils.Databases;

namespace Locadora.Controller
{
    public class LocacaoController : ILocacaoController
    {
        public void AdicionarLocacao(Locacao locacao)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Locacao.INSERTLOCACAO, connection, transaction);

                    command.Parameters.AddWithValue("@ClienteID", locacao.Cliente.ClienteID);
                    command.Parameters.AddWithValue("VeiculoID", locacao.Veiculo.VeiculoID);
                    command.Parameters.AddWithValue("@DataLocacao", locacao.DataLocacao);
                    command.Parameters.AddWithValue("@DataDevolucaoPrevista", locacao.DataDevolucaoPrevista);
                    command.Parameters.AddWithValue("@DataDevolucaoReal", locacao.DataDevolucaoReal.HasValue ? (object)locacao.DataDevolucaoReal : (object)DBNull.Value); // if: insere a data | else if: insere null
                    command.Parameters.AddWithValue("@ValorDiaria", locacao.Veiculo.Categoria.Diaria);
                    command.Parameters.AddWithValue("@ValorTotal", locacao.ValorTotal);
                    command.Parameters.AddWithValue("@Multa", locacao.Multa);
                    command.Parameters.AddWithValue("@Status", locacao.Status);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao criar a locação" + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao criar a locação" + ex.Message);
                }
            }
        }




        public void AtualizarDataDevolucaoRealLocacao(Guid id, DateTime devolucao)
        {
            var locacaoEncontrada = BuscarLocacaoPorId(id);

            if (locacaoEncontrada is null)
                throw new Exception("Não existe locação com este ID!");

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Locacao.UPDATELOCACAODEVOLUCAOREAL, connection, transaction);
                    command.Parameters.AddWithValue("@DataDevolucaoReal", devolucao);
                    command.Parameters.AddWithValue("@LocacaoID", id);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar a data de devolução do veículo: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao atualizar a data de devolução do veículo: " + ex.Message);
                }
            }
        }




        public void AtualizarStatusLocacao(Guid id, string status)
        {
            var locacaoEncontrada = BuscarLocacaoPorId(id);

            if (locacaoEncontrada is null)
                throw new Exception("Não existe locação com este ID!");

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Locacao.UPDATELOCACAOSTATUS, connection, transaction);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@LocacaoID", id);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar o status da locação: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao atualizar o status da locação: " + ex.Message);
                }
            }
        }



        public Locacao? BuscarLocacaoPorId(Guid id)
        {
            var clienteController = new ClienteController();
            var veiculoController = new VeiculoController();

            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlCommand command = new SqlCommand(Locacao.SELECTLOCACOESBYID, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@LocacaoID", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var cliente = clienteController.BuscaClientePorId(reader.GetInt32(1));
                            var veiculo = veiculoController.BuscarVeiculoId(reader.GetInt32(2));

                            var locacao = new Locacao(
                                reader.GetGuid(0),
                                cliente,
                                veiculo,
                                reader.GetDateTime(3),
                                reader.GetDateTime(4),
                                reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                reader.GetDecimal(6),
                                reader.GetDecimal(7),
                                reader.GetDecimal(8),
                                reader.GetString(9)
                             );
                            return locacao;
                        }
                    }
                    return null;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar locação por Id: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar locação por Id" + ex.Message);
                }
            }
        }



        public List<Locacao> ListarTodasLocacoes()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            var clienteController = new ClienteController();
            var veiculoController = new VeiculoController();
            var categoriaController = new CategoriaController();

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Locacao.SELECTALLLOCACOES, connection);

                SqlDataReader reader = command.ExecuteReader();
                
                List<Locacao> locacoes = new List<Locacao>();

                while (reader.Read())
                {
                    var cliente = clienteController.BuscaClientePorId(reader.GetInt32(1));
                    var veiculo = veiculoController.BuscarVeiculoId(reader.GetInt32(2));

                    var locacao = new Locacao(reader.GetGuid(0),
                                            cliente,
                                            veiculo,
                                            reader.GetDateTime(3),
                                            reader.GetDateTime(4),
                                            reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                            reader.GetDecimal(6),
                                            reader.GetDecimal(7),
                                            reader.GetDecimal(8),
                                            reader.GetString(9)
                                            );
                    
                    locacoes.Add(locacao);
                }
                return locacoes;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao listar locações: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao listar locações: " + ex.Message);
            }
        }
    }
}
        // Aqui eu vou chamar a função BuscarVeiculoPlaca (de Veículo) - somente trazer opções de veículos com status > 'Disponível"


        // A partir da mudança de dias, vai atualizar a "DataDevolucaoPrevista" e "ValorTotal"

        // Se mudar pra 'finalizada', altera também a DataDevReal e ValorTotal (considerando dias locado, e multa (se aplicado)
        // Se mudar pra 'cancelado', altera também a DataDevReal
