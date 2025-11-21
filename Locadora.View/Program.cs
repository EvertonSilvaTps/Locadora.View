using Locadora.Controller;
using Locadora.Models.Enums;

//INPUT Categoria
//Categoria categoria = new Categoria("Grupo H", 139.90m);


//var categoriaController = new CategoriaController();


#region INSERT Categoria
//try
//{
//    categoriaController.AdicionarCategoria(categoria);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion



#region SELECT ALL Categorias
//try
//{
//    var listadeCategorias = categoriaController.ListarTodasCategorias();

//    foreach (var categoriadaLista in listadeCategorias)
//    {
//        Console.WriteLine(categoriadaLista);
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion


#region UPDATE Descrição
//try
//{
//    categoriaController.AtualizarDescricaoCategoria("[descrição]", "Grupo C");
//    Console.WriteLine(categoriaController.BuscaCategoriaPorNome("Grupo C"));
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion


#region UPDATE Diária
//try
//{
//    categoriaController.AtualizarDiariaCategoria(109.90m, "Grupo C");
//    Console.WriteLine(categoriaController.BuscaCategoriaPorNome("Grupo C"));
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion


#region DELETE Categoria
//try
//{
//    categoriaController.DeletarCategoria("Grupo A");
//    Console.WriteLine("Categoria deletado com sucesso!");
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion



//   ----------------                      <<<   Veiculos   >>>                      ----------------                      


var veiculoController = new VeiculoController();


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
try
{
    Console.WriteLine(veiculoController.BuscarVeiculoPlaca("MNO7890"));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
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
