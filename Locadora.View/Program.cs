using Locadora.Controller.Crud;
using Locadora.Controller.Menu;


var customer = new ClienteMenu();
var category = new CategoriaMenu();
var vehicle = new VeiculoMenu();
var employeer = new FuncionarioMenu();
var rental = new LocacaoMenu();

int opcao = 0;
do
{
    Console.Clear();
    Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
    Console.WriteLine(" |       >    Sistema Locadora de Veículos    <       |");
    Console.WriteLine(" |----------------------------------------------------|");
    Console.WriteLine(" | [ 1 ] Menu Locação       |   [ 2 ] Menu Veículo    |");
    Console.WriteLine(" | [ 3 ] Menu Funcionario   |   [ 4 ] Menu Categoria  |");
    Console.WriteLine(" | [ 5 ] Menu Cliente       |   [ 6 ] Sair            |");
    Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
    Console.WriteLine();
    Console.Write("  >>> Informe o menu desejado: ");
    string entrada = Console.ReadLine()!;
    bool conversao = int.TryParse(entrada, out opcao);
    Console.WriteLine("---------------------------------------");

    switch (opcao)
    {
        case 1:
            rental.MenuLocacao();
            break;
        case 2:
            vehicle.MenuVeiculo();
            break;
        case 3:
            employeer.MenuFuncionario();
            break;
        case 4:
            category.MenuCategoria();
            break;
        case 5:
            customer.MenuCliente();
            break;
        case 6:
            Console.WriteLine("Encerrando o programa...");
            Thread.Sleep(4000);
            return;
        default:
            Console.WriteLine("\nOpção Inválida. Tente novamente.");
            break;
    }

    Console.Write("\n  >  Pressione qualquer Tecla para prosseguir ");
    Console.ReadLine();

} while (true);