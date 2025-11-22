using Locadora.Models;

namespace Locadora.Controller.Interfaces
{
    public interface IClienteController
    {
        public void AdicionarCliente(Cliente cliente, Documento documento);

        public List<Cliente> ListarTodosClientes();

        public Cliente BuscarClienteEmail(string email);

        public void AtualizarTelefoneCliente(string telefone, string email);

        public void AtualizarDocumentoCliente(string email, Documento documento);

        public void DeletarCliente(string email);
    }
}
