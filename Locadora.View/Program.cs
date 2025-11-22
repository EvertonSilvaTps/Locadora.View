using Locadora.Controller;
using Locadora.Controller.Crud;
using Locadora.Controller.Menu;


//Funcionario func = new Funcionario("Bruce Wanny", "285646688", "batman@uol.com");


var funcController = new FuncionarioController();


#region INSERT Funcionario
//try
//{
//    funcController.AdicionarFuncionario(func);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion



#region SELECT ALL Funcionarios
//try
//{
//    var lista = funcController.ListarTodosFuncionarios();

//    foreach (var f in lista)
//    {
//        Console.WriteLine(f);
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion


#region UPDATE and Busca de Salario
//try
//{
//    funcController.AtualizarSalarioFuncionario("batman@uol.com", 1300.90m);
//    Console.WriteLine(funcController.BuscarFuncionarioEmail("batman@uol.com"));
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion


#region DELETE Funcionario
//try
//{
//    funcController.DeletarFuncionario("batman@uol.com");
//    Console.WriteLine("Funcionario deletado com sucesso!");
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion



//   ----------------                      <<<   Locação   >>>                      ----------------                      


var locacaoController = new LocacaoController();


#region INSERT Veiculo
//try
//{
//    var veiculo = new Veiculo(1, "XYZ-9876", "Chevrolet", "S10", 2025, EStatusVeiculo.Disponivel.ToString());
//    veiculoController.AdicionarVeiculo(veiculo);
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao criar veículo: " + ex.Message);
//}
#endregion


#region SELECT ALL Veiculos
//try
//{
//    var veiculos = veiculoController.ListarTodosVeiculos();

//    foreach (var item in veiculos)
//    {
//        Console.WriteLine(item);
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion


#region SELECT BY PLACA
//try
//{
//    Console.WriteLine(veiculoController.BuscarVeiculoPlaca("MNO7890"));
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion


#region DELETE Veiculo
//try
//{
//    var veiculo = veiculoController.BuscarVeiculoPlaca("XYZ-9876");

//    veiculoController.DeletarVeiculo(veiculo.VeiculoID);
//    Console.WriteLine("Veiculo deletado com sucesso!");
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion


#region UPDATE Status Veículo
//try
//{
//    veiculoController.AtualizarStatusVeiculo(EStatusVeiculo.Manutencao.ToString(), "MNO7890");
//    Console.WriteLine(veiculoController.BuscarVeiculoPlaca("MNO7890"));
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

var customer = new ClienteMenu();
customer.MenuCliente();

//var category = new CategoriaMenu();
//category.MenuCategoria();

//var vehicle = new VeiculoMenu();
//vehicle.MenuVeiculo();