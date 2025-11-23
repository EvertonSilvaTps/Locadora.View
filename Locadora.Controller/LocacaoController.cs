using Locadora.Controller.Interfaces;
using Locadora.Models;
using Locadora.Models.Enums;
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
                    // validar documento do cliente
                    using (var command = new SqlCommand(Cliente.SELECTDATAVALIDADEPORID, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@ClienteID", locacao.Cliente.ClienteID);

                        var data = command.ExecuteScalar();

                        DateTime dataValidade = (DateTime)data;

                        if (dataValidade < DateTime.Now.Date)
                            throw new Exception("Documento do cliente está vencido. Não é possível realizar a locação.");
                    }

                    //  verificar disponibilidade do veículo
                    using (var command = new SqlCommand(Veiculo.CHECKVEICULO, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@VeiculoID", locacao.Veiculo.VeiculoID);
                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new Exception("Veículo não encontrado.");

                            string statusVeiculo = reader.GetString(reader.GetOrdinal("StatusVeiculo"));

                            if (!string.Equals(statusVeiculo, EStatusVeiculo.Disponível.ToString(), StringComparison.OrdinalIgnoreCase))
                                throw new Exception("Veículo não está disponível para locação.");
                        }
                    }

                    using (SqlCommand command = new SqlCommand(Locacao.INSERTLOCACAO, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@ClienteID", locacao.Cliente.ClienteID);
                        command.Parameters.AddWithValue("@VeiculoID", locacao.Veiculo.VeiculoID);
                        command.Parameters.AddWithValue("@DataLocacao", locacao.DataLocacao);
                        command.Parameters.AddWithValue("@DataDevolucaoPrevista", locacao.DataDevolucaoPrevista);
                        command.Parameters.AddWithValue("@DataDevolucaoReal", locacao.DataDevolucaoReal.HasValue ? (object)locacao.DataDevolucaoReal : (object)DBNull.Value); // if: insere a data | else if: insere null
                        command.Parameters.AddWithValue("@ValorDiaria", locacao.Veiculo.Categoria.Diaria);
                        command.Parameters.AddWithValue("@ValorTotal", locacao.ValorTotal);
                        command.Parameters.AddWithValue("@Multa", locacao.Multa);
                        command.Parameters.AddWithValue("@Status", locacao.Status.ToString());

                        command.ExecuteNonQuery();
                    }

                    // atualizar status do veículo para alugado
                    using (var command = new SqlCommand(Veiculo.UPDATESTATUSVEICULO, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@StatusVeiculo", EStatusVeiculo.Alugado.ToString());
                        command.Parameters.AddWithValue("@VeiculoID", locacao.Veiculo.VeiculoID);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro de banco ao adicionar locação" + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adicionar locação" + ex.Message);
                }
                finally
                {
                    connection.Close();
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
                    using (SqlCommand command = new SqlCommand(Locacao.UPDATELOCACAOSTATUS, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@LocacaoID", id);

                        command.ExecuteNonQuery();
                    }

                    // Se finalizada, atualiza DataDevolucaoReal e status do veículo
                    if (status == EStatusLocacao.Finalizada.ToString())
                    {
                        using (var commandDate = new SqlCommand(Locacao.UPDATELOCACAODEVOLUCAOREAL, connection, transaction))
                        {
                            commandDate.Parameters.AddWithValue("@DataDevolucaoReal", DateTime.Now);
                            commandDate.Parameters.AddWithValue("@LocacaoID", id);

                            commandDate.ExecuteNonQuery();
                        }

                        // Atualiza status do veículo para 'Disponível'
                        using (var commandVeiculo = new SqlCommand(Veiculo.UPDATESTATUSVEICULO, connection, transaction))
                        {
                            commandVeiculo.Parameters.AddWithValue("@StatusVeiculo", EStatusVeiculo.Disponível.ToString());
                            commandVeiculo.Parameters.AddWithValue("@VeiculoID", locacaoEncontrada.Veiculo.VeiculoID);

                            commandVeiculo.ExecuteNonQuery();
                        }
                    }

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

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Locacao.SELECTALLLOCACOES, connection);
                
                List<Locacao> locacoes = new List<Locacao>();

                using (SqlDataReader reader = command.ExecuteReader())
                {

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


        public List<Locacao> ListarLocacaoPorCliente(int clienteId)
        {
            var locacoes = new List<Locacao>();

            using (var connection = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Locacao.SELECTLOCACAOPORCLIENTE, connection);
                command.Parameters.AddWithValue("@ClienteID", clienteId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            var locacao = new Locacao(
                                    Convert.ToInt32(reader["ClienteID"]),
                                    Convert.ToInt32(reader["VeiculoID"]),
                                    Convert.ToDateTime(reader["DataLocacao"]),
                                    Convert.ToDateTime(reader["DataDevolucaoPrevista"]),
                                    Convert.ToDecimal(reader["ValorDiaria"]),
                                    reader["Status"].ToString()
                                    );

                            locacoes.Add(locacao);
                        }
                    }

                    catch (SqlException ex)
                    {
                        throw new Exception("Erro no banco de dados ao listar locações." + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao listar locações." + ex.Message);
                    }
                    finally
                    {
                        connection?.Close();
                    }
                    return locacoes;
                }
            }
        }

        public List<Locacao> ListarLocacaoPorFuncionario(int funcionarioID)
        {
            var locacoes = new List<Locacao>();

            using (var connection = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Locacao.SELECTLOCACAOPORFUNCIONARIO, connection);
                command.Parameters.AddWithValue("@FuncionarioID", funcionarioID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            var locacao = new Locacao(
                                    Convert.ToInt32(reader["ClienteID"]),
                                    Convert.ToInt32(reader["VeiculoID"]),
                                    Convert.ToDateTime(reader["DataLocacao"]),
                                    Convert.ToDateTime(reader["DataDevolucaoPrevista"]),
                                    Convert.ToDecimal(reader["ValorDiaria"]),
                                    reader["Status"].ToString()
                                    );

                            locacoes.Add(locacao);
                        }
                    }

                    catch (SqlException ex)
                    {
                        throw new Exception("Erro no banco de dados ao listar locações." + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao listar locações." + ex.Message);
                    }
                    finally
                    {
                        connection?.Close();
                    }
                    return locacoes;
                }
            }
        }


    }
}
