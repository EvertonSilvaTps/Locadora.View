using Locadora.Controller;
using Locadora.Models;


//INPUT Cliente
Cliente cliente = new Cliente("ESilva&Silva", "sul@uol.com.br");

//INPUT Documento
Documento documento = new Documento("CPF", "516541521", new DateOnly(2025, 1, 1), new DateOnly(2035, 1, 1));


var clienteController = new ClienteController();


//#region Adicionar Cliente & Documento
//try
//{
//clienteController.AdicionarCliente(cliente, documento);
//}
//catch (Exception ex)
//{
//Console.WriteLine(ex.Message);
//}
//#endregion



#region SELECT ALL Clientes & Documentos
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
#endregion


//#region UPDATE Telefone
//try
//{
//    clienteController.AtualizarTelefoneCliente("[telefone]", "[email]");
//    Console.WriteLine(clienteController.BuscaClientePorEmail("[email]"));
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//#endregion


#region DELETE Cliente
try
{
    clienteController.DeletarCliente("sul@uol.com.br");
    Console.WriteLine("Cliente deletado com sucesso!");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
#endregion


//#region UPDATE Documento do Cliente
//try
//{
//    clienteController.AtualizarDocumentoCliente("alo@uol.com.br", documento);
//    Console.WriteLine(clienteController.BuscaClientePorEmail("alo@uol.com.br"));
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//#endregion