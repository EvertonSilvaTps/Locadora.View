using Locadora.Controller;
using Locadora.Models;

Cliente cliente = new Cliente("Novo cliente agora com o transaction", "newtentativa@uol.com.br");
//Documento documento = new Documento(1, "RG", "123456789", new DateOnly(2020, 1, 1), new DateOnly(2030, 1, 1));

//Console.WriteLine(cliente);

var clienteController = new ClienteController();

//try
//{
//    clienteController.AdicionarCliente(cliente);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

try
{
    var listadeClientes = clienteController.ListarTodosClientes();

    foreach (var clientedaLista in listadeClientes)
    {
        Console.WriteLine(clientedaLista);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

//clienteController.AtualizarTelefoneCliente("[telefone]", "[email]");
//Console.WriteLine(clienteController.BuscaClientePorEmail("[email]"));