using Locadora.Controller.Interfaces;
using Locadora.Models;
using Microsoft.Data.SqlClient;
using Utils.Databases;

namespace Locadora.Controller
{
    public class ClienteController : IClienteController
    {

        public void AdicionarCliente(Cliente cliente, Documento documento)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            
            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())    // Begin > vai executar o Transaction (foi atribuido a variável 'transaction'
            {
                try
                {
                    SqlCommand command = new SqlCommand(Cliente.INSERTCLIENTE, connection, transaction);

                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? (object)DBNull.Value);

                    int clienteId = Convert.ToInt32(command.ExecuteScalar());  // buscou o id desta inserção e guardou numa variável

                    cliente.setClienteID(clienteId);    // serve pra guardar o id gerado do banco para o campo do ID do cliente
                    //cliente.setClienteID(Convert.ToInt32(command.ExecuteScalar()));   uma outra forma de executar o camando acima

                    var documentoController = new DocumentoController();

                    documento.setClienteID(clienteId);   // serve pra guardar o id gerado do banco para o campo do ID do cliente

                    documentoController.AdicionarDocumento(documento, connection, transaction);  // Aqui ele entra na DocumentoController
                    
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adicionar cliente: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao adicionar cliente: " + ex.Message);
                }
                finally  // Não importa se entrou no try ou catch, ele vai cair no finally para encerrar a conexão com o BD
                {
                    connection.Close();
                }
            }
        }


        public List<Cliente> ListarTodosClientes()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Cliente.SELECTALLCLIENTES, connection);

                SqlDataReader reader = command.ExecuteReader();

                List<Cliente> listaClientes = new List<Cliente>();

                while (reader.Read())
                {
                    var cliente = new Cliente(reader["Nome"].ToString(),
                                                reader["Email"].ToString(),
                                                reader["Telefone"] != DBNull.Value ?    // ? = if
                                                reader["Telefone"].ToString() : null    // : = else
                                                );

                    //cliente.setClienteID(Convert.ToInt32(reader["ClienteID"]));

                    var documento = new Documento(reader["TipoDocumento"].ToString(),
                                                reader["Numero"].ToString(),
                                                DateOnly.FromDateTime(reader.GetDateTime(5)),
                                                DateOnly.FromDateTime(reader.GetDateTime(6))
                                                );

                    cliente.setDocumento(documento);

                    listaClientes.Add(cliente);
                }
                return listaClientes;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao listar clientes: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao listar clientes: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public Cliente BuscarClienteEmail(string email)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand(Cliente.SELECTCLIENTEPOREMAIL, connection);

                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var cliente = new Cliente(reader["Nome"].ToString(),
                                                reader["Email"].ToString(),
                                                reader["Telefone"] != DBNull.Value ?
                                                reader["Telefone"].ToString() : null
                                                );
                    cliente.setClienteID(Convert.ToInt32(reader["ClienteID"]));

                    var documento = new Documento(reader["TipoDocumento"].ToString(),
                                                reader["Numero"].ToString(),
                                                DateOnly.FromDateTime(reader.GetDateTime(6)),
                                                DateOnly.FromDateTime(reader.GetDateTime(7))
                                                );

                    cliente.setDocumento(documento);

                    return cliente;
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar cliente por email: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao buscar cliente por email: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public void AtualizarTelefoneCliente(string telefone, string email)
        {
            // buscar o cliente
            // atualizar a propriedade telefone
            // salvar no banco

            var clienteEncontrado = this.BuscarClienteEmail(email);

            if (clienteEncontrado is null)
                throw new Exception("Não existe cliente com esse email cadastrado!");

            clienteEncontrado.setTelefone(telefone);

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Cliente.UPDATEFONECLIENTE, connection, transaction);
                    command.Parameters.AddWithValue("@Telefone", clienteEncontrado.Telefone);
                    command.Parameters.AddWithValue("@IdCliente", clienteEncontrado.ClienteID);
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar telefone do cliente: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao atualizar telefone do cliente: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public void AtualizarDocumentoCliente(string email, Documento documento)
        {
            var clienteEncontrado = BuscarClienteEmail(email) ??    // ?? Ternario =  se der tudo bem retorna o cliente, se não cai no throw
                throw new Exception("Não existe cliente com esse email cadastrado!");

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    documento.setClienteID(clienteEncontrado.ClienteID);
                    DocumentoController documentoController = new DocumentoController();

                    documentoController.AtualizarDocumento(documento, connection, transaction);

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar documento do cliente: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao atualizar documento do cliente: " + ex.Message);
                }
            }
        }


        public void DeletarCliente(string email)
        {
            var clienteEncontrado = BuscarClienteEmail(email);

            if (clienteEncontrado is null)
                throw new Exception("Não existe cliente com esse email cadastrado!");

            SqlConnection connection = new SqlConnection (ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Cliente.DELETECLIENTE, connection, transaction);

                    command.Parameters.AddWithValue("@IdCliente", clienteEncontrado.ClienteID);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar o cliente: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao deletar o cliente: " + ex.Message);
                }
            }

        }


    }
}
