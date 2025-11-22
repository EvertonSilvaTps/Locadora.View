using Locadora.Models;
using Microsoft.Data.SqlClient;

namespace Locadora.Controller.Interfaces
{
    public interface IDocumentoController
    {
        public void AdicionarDocumento(Documento documento, SqlConnection connection, SqlTransaction transaction);

        public void AtualizarDocumento(Documento documento, SqlConnection connection, SqlTransaction transaction);
    }
}
