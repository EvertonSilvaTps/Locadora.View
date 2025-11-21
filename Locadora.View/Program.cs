using Locadora.Controller;
using Locadora.Models;


//INPUT Categoria
Categoria categoria = new Categoria("Grupo H", 139.90m);


var categoriaController = new CategoriaController();


#region Adicionar Categoria
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
try
{
    var listadeCategorias = categoriaController.ListarTodasCategorias();

    foreach (var categoriadaLista in listadeCategorias)
    {
        Console.WriteLine(categoriadaLista);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
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
