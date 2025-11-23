using Locadora.Controller.Interfaces;
using Locadora.Models;
using Microsoft.Data.SqlClient;
using Utils.Databases;

namespace Locadora.Controller
{
    public class FuncionarioController : IFuncionarioController
    {
        public void AdicionarFuncionario(Funcionario funcionario)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Funcionario.INSERTFUNCIONARIO, connection, transaction);

                    command.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    command.Parameters.AddWithValue("@CPF", funcionario.CPF);
                    command.Parameters.AddWithValue("@Email", funcionario.Email);
                    command.Parameters.AddWithValue("@Salario", funcionario.Salario ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adicionar funcionario: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao adicionar funcionario: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Funcionario> ListarTodosFuncionarios()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Funcionario.SELECTALLFUNCIONARIOS, connection);

                SqlDataReader reader = command.ExecuteReader();

                List<Funcionario> listaFuncionarios = new List<Funcionario>();

                while (reader.Read())
                {
                    var funcionario = new Funcionario(reader["Nome"].ToString()!,
                                                    reader["CPF"].ToString()!,
                                                    reader["Email"].ToString()!,
                                                    reader["Salario"] != DBNull.Value ?
                                                    reader.GetDecimal(3) : null
                                                    );

                    listaFuncionarios.Add(funcionario);
                }
                return listaFuncionarios;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao listar funcionarios: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao listar funcionarios: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public Funcionario BuscarFuncionarioEmail(string email)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand(Funcionario.SELECTFUNCIONARIOSPOREMAIL, connection);

                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var funcionario = new Funcionario(reader["Nome"].ToString()!,
                                                reader["CPF"].ToString()!,
                                                reader["Email"].ToString()!,
                                                reader["Salario"] != DBNull.Value ?
                                                reader.GetDecimal(4) : null
                                                );

                    funcionario.setFuncionarioID(Convert.ToInt32(reader["FuncionarioID"]));

                    return funcionario;
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar funcionario por email: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao buscar funcionario por email: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void AtualizarSalarioFuncionario(string email, decimal salario)
        {
            var funcionarioBuscado = this.BuscarFuncionarioEmail(email);

            if (funcionarioBuscado is null)
                throw new Exception("Não existe funcionario com esse email cadastrado!");

            funcionarioBuscado.setSalario(salario);

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Funcionario.UPDATESALARIOFUNCIONARIO, connection, transaction);
                    command.Parameters.AddWithValue("@Salario", funcionarioBuscado.Salario);
                    command.Parameters.AddWithValue("@FuncionarioID", funcionarioBuscado.FuncionarioID);
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar salario do funcionario: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao atualizar salario do funcionario: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeletarFuncionario(string email)
        {
            var funcionarioBuscado = this.BuscarFuncionarioEmail(email);

            if (funcionarioBuscado is null)
                throw new Exception("Não existe funcionario com esse email cadastrado!");

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Funcionario.DELETEFUNCIONARIO, connection, transaction);

                    command.Parameters.AddWithValue("@FuncionarioID", funcionarioBuscado.FuncionarioID);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar o funcionario: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao deletar o funcionario: " + ex.Message);
                }
            }
        }
    }
}
