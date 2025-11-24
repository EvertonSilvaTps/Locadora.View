using Locadora.Models;
using Microsoft.Data.SqlClient;
using Utils.Databases;

namespace Locadora.Controller
{
    public class LocacaoFuncionarioController
    {
        public void AssociarFuncionario(Guid locacaoId, int funcionarioId)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            using (SqlCommand command = new SqlCommand(LocacaoFuncionario.ASSOCIARFUNCIONARIO, connection))
            {
                command.Parameters.AddWithValue("LocacaoID", locacaoId);
                command.Parameters.AddWithValue("FuncionarioID", funcionarioId);

                int linhas = command.ExecuteNonQuery();
                if (linhas == 0)
                {
                    throw new Exception("Falha ao associar locação.");
                }
                connection.Close();
            }
        }
        public void DesassociarFuncionario(Guid locacaoId, int funcionarioId)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            using (SqlCommand command = new SqlCommand(LocacaoFuncionario.DESASSOCIARFUNCIONARIO, connection))
            {
                command.Parameters.AddWithValue("LocacaoID", locacaoId);
                command.Parameters.AddWithValue("FuncionarioID", funcionarioId);

                int linhas = command.ExecuteNonQuery();
                if (linhas == 0)
                {
                    throw new Exception("Falha ao desassociar locação.");
                }
                connection.Close();
            }
        }


        public List<LocacaoFuncionario> ListarLocacaoPorFuncionario(int funcionarioId)
        {
            var funcionarioController = new FuncionarioController();
            var veiculoController = new VeiculoController();

            using (var connection = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                connection.Open();

                var locacoesFuncionario = new List<LocacaoFuncionario>();

                SqlCommand command = new SqlCommand(LocacaoFuncionario.SELECTLOCACAOPORFUNCIONARIO, connection);
                command.Parameters.AddWithValue("@FuncionarioID", funcionarioId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    try
                    {
                        Console.WriteLine("                   =-=-=   >  Locaçãoes  <   =-=-=\n");
                        while (reader.Read())
                        {

                            var funcionario = funcionarioController.BuscarFuncionarioPorID(reader.GetInt32(2));

                            var locacao = new LocacaoFuncionario(reader.GetInt32(0),
                                                    reader.GetGuid(1),
                                                    reader.GetInt32(funcionario.FuncionarioID)
                                                    );

                            locacoesFuncionario.Add(locacao);
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
                    return locacoesFuncionario;
                }
            }

        }
    }
}
