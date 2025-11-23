using Locadora.Controller.Interfaces;
using Locadora.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Utils.Databases;

namespace Locadora.Controller
{
    public class CategoriaController : ICategoriaController
    {
        public void AdicionarCategoria(Categoria categoria)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using(SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Categoria.INSERTCATEGORIA, connection, transaction);

                    command.Parameters.AddWithValue("@Nome", categoria.Nome);
                    command.Parameters.AddWithValue("@Descricao", categoria.Descricao ?? (object)DBNull.Value);
                    var p = command.Parameters.Add("@Diaria", SqlDbType.Decimal);
                    p.Precision = 10;
                    p.Scale = 2;
                    p.Value = categoria.Diaria;

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception ("Erro ao adicionar categoria: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao adicionar categoria: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public List<Categoria> ListarTodasCategorias()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Categoria.SELECTALLCATEGORIAS, connection);

                SqlDataReader reader = command.ExecuteReader();

                List<Categoria> listaCategorias = new List<Categoria>();

                while (reader.Read())
                {
                    var categoria = new Categoria(reader["Nome"].ToString()!,
                                                    reader.GetDecimal(2),
                                                    reader["Descricao"] != DBNull.Value ?
                                                    reader["Descricao"].ToString() : null);

                    listaCategorias.Add(categoria);
                }
                return listaCategorias;

            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao listar categorias: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao listar categorias: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public Categoria BuscarNomeCategoriaPorId(int id)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            try
            {
                SqlCommand command = new SqlCommand(Categoria.SELECTCATEGORIAPORID, connection);
                command.Parameters.AddWithValue("@CategoriaID", id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var categoria = new Categoria(
                       reader["Nome"].ToString(),
                       reader.GetDecimal(reader.GetOrdinal("Diaria")),
                       reader["Descricao"] != DBNull.Value ? reader["Descricao"].ToString() : null
                    );
                    categoria.setCategoriaId(Convert.ToInt32(reader["CategoriaID"]));


                    return categoria;
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar categoria." + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao buscar categoria." + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public Categoria? BuscarCategoriaNome(string nome)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand(Categoria.SELECTCATEGORIAPORNOME, connection);

                command.Parameters.AddWithValue("@Nome", nome);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var categoria = new Categoria(reader["Nome"].ToString()!,
                                                    reader.GetDecimal(3),
                                                    reader["Descricao"] != DBNull.Value ?
                                                    reader["Descricao"].ToString() : null);
                    
                    categoria.setCategoriaId(Convert.ToInt32(reader["CategoriaId"]));

                    return categoria;
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar categoria por nome: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao buscar categoria por nome: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public void AtualizarDescricaoCategoria(string descricao, string nome)
        {
            var categoriaEncontrado = this.BuscarCategoriaNome(nome);

            if (categoriaEncontrado is null)
                throw new Exception("Não existe categoria com esse nome cadastrado!");

            categoriaEncontrado.setDescricao(descricao);

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Categoria.UPDATEDESCRICAOCATEGORIA, connection, transaction);
                    command.Parameters.AddWithValue("@Descricao", categoriaEncontrado.Descricao);
                    command.Parameters.AddWithValue("@CategoriaId", categoriaEncontrado.CategoriaId);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar descrição da categoria: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao atualizar descrição da categoria: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public void AtualizarDiariaCategoria(decimal diaria, string nome)
        {
            var categoriaEncontrado = this.BuscarCategoriaNome(nome);

            if (categoriaEncontrado is null)
                throw new Exception("Não existe categoria com esse nome cadastrado!");

            categoriaEncontrado.setDiaria(diaria);

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Categoria.UPDATEDIARIACATEGORIA, connection, transaction);
                    var p = command.Parameters.Add("@Diaria", SqlDbType.Decimal);
                    p.Precision = 10;
                    p.Scale = 2;
                    p.Value = categoriaEncontrado.Diaria;
                    command.Parameters.AddWithValue("@CategoriaId", categoriaEncontrado.CategoriaId);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar diaria da categoria: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao atualizar diaria da categoria: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public void DeletarCategoria(string nome)
        {
            var categoriaEncontrado = this.BuscarCategoriaNome(nome);

            if (categoriaEncontrado is null)
                throw new Exception("Não existe categoria com esse nome cadastrado!");

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Categoria.DELETECATEGORIA, connection, transaction);

                    command.Parameters.AddWithValue("@CategoriaId", categoriaEncontrado.CategoriaId);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar a categoria: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao deletar a categoria: " + ex.Message);
                }
            }
        }
    }
}
