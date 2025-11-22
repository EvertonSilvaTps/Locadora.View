using Locadora.Controller.Validation;
using Locadora.Models;
using Locadora.Models.Enums;

namespace Locadora.Controller.Menu
{
    public class VeiculoMenu
    {
        private VeiculoController Controller = new VeiculoController();

        private void InsertService()
        {
            string? category = Validar.ValidarInputString("Nome da categoria do Veículo: ");
            if (category == null) return;
            var Id = new CategoriaController().BuscarCategoriaNome(category);

            string? plate = Validar.ValidarInputString("Placa: ");
            if (plate == null) return;

            string? mark = Validar.ValidarInputString("Marca: ");
            if (mark == null) return;

            string? model = Validar.ValidarInputString("Modelo: ");
            if (model == null) return;

            int year = Validar.ValidarInputInt("Ano do Veículo: ");
            if (year == 0) return;

            string? vehicleStatus = Validar.ValidarInputString("Modelo: ");
            if (vehicleStatus == null) return;

            Veiculo vehicle = new Veiculo(Id.CategoriaId, plate, mark, model, year, EStatusVeiculo.Disponivel.ToString());

            try
            {
                Controller.AdicionarVeiculo(vehicle);
                Console.WriteLine("\n   >>>   Veículo inserido com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void SelectAllService()
        {
            Console.Clear();
            Console.WriteLine();

            try
            {
                var list = Controller.ListarTodosVeiculos();

                foreach (var vehicle in list)
                {
                    Console.WriteLine(vehicle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void UpdateStatusService()
        {
            string? plate = Validar.ValidarInputString("Informe a placa para busca do veículo: ");
            if (plate == null) return;

            try
            {
                var vlr = Controller.BuscarVeiculoPlaca(plate);
                if (vlr is null)
                {
                    Console.WriteLine("\nNão existe veículo com essa placa cadastrado!");
                    return;
                }

                Console.WriteLine("\n=-=-=   >  Veículo  <   =-=-=\n");
                Console.WriteLine(vlr + "\n");

                int? vehicleStatus = Validar.ValidarInputInt("Atualizar Stutas [1] Disponivel | [2] Alugado | [3] Manutencao | [4] Reservado: ");
                if (vehicleStatus == 0) return;

                if (vehicleStatus == 1)
                    Controller.AtualizarStatusVeiculo(EStatusVeiculo.Disponivel.ToString(), plate);
                else if (vehicleStatus == 2)
                    Controller.AtualizarStatusVeiculo(EStatusVeiculo.Alugado.ToString(), plate);
                else if (vehicleStatus == 3)
                    Controller.AtualizarStatusVeiculo(EStatusVeiculo.Manutencao.ToString(), plate);
                else if (vehicleStatus == 4)
                    Controller.AtualizarStatusVeiculo(EStatusVeiculo.Reservado.ToString(), plate);

                Console.WriteLine("\n >>>  Status atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void DeleteService()
        {
            string? plate = Validar.ValidarInputString("Informe a placa para busca do veículo: ");
            if (plate == null) return;

            try
            {
                var vlr = Controller.BuscarVeiculoPlaca(plate);
                if (vlr is null)
                {
                    Console.WriteLine("\nNão existe veículo com essa placa cadastrado!");
                    return;
                }

                Console.WriteLine("\n=-=-=   >  Veículo  <   =-=-=\n");
                Console.WriteLine(vlr + "\n");

                Console.Write("Tem certeza que deseja deletar o veículo? [S/N]: ");
                string res = Console.ReadLine()!.ToUpper();

                while (res is not "S" && res is not "N")
                {
                    Console.Write("Error! Informe apenas [S/N] pra continuar: ");
                    res = Console.ReadLine()!.ToUpper();
                }

                if (res == "N")
                {
                    Console.WriteLine("\nEncerrando a operação de deletar...");
                    return;
                }

                Controller.DeletarVeiculo(vlr.VeiculoID);
                Console.WriteLine("\n >>>  Veículo deletado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void MenuVeiculo()
        {
            var listCategory = new CategoriaMenu();

            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
                Console.WriteLine(" |                       >      Veículo      <                      |");
                Console.WriteLine(" |------------------------------------------------------------------|");
                Console.WriteLine(" | [ 1 ] Cadastrar Veículo   |   [ 2 ] Exibir Veículos Alugados     |");
                Console.WriteLine(" | [ 3 ] Atualizar Stutus    |   [ 4 ] Exibir Veículos Disponíveis  |");
                Console.WriteLine(" | [ 5 ] Exibir Categorias   |   [ 6 ] Exibir Todos os Veículos     |");
                Console.WriteLine(" | [ 7 ] Deletar Veículo     |   [ 8 ] Voltar                       |");
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
                Console.WriteLine();
                Console.Write("  >>> Informe o menu desejado: ");
                string entrada = Console.ReadLine()!;
                bool conversao = int.TryParse(entrada, out opcao);
                Console.WriteLine("---------------------------------------");

                switch (opcao)
                {
                    case 1:
                        InsertService();
                        break;
                    case 2:
                        SelectAllService();  // fazer ainda
                        break;
                    case 3:
                        UpdateStatusService();
                        break;
                    case 4:
                        SelectAllService();  // fazer ainda
                        break;
                    case 5:
                        listCategory.SelectAllService();
                        break;
                    case 6:
                        SelectAllService();
                        break;
                    case 7:
                        DeleteService();
                        break;
                    case 8:
                        return;
                    default:
                        Console.WriteLine("\nOpção Inválida. Tente novamente.");
                        break;
                }

                Console.Write("\n  >  Pressione qualquer Tecla para prosseguir ");
                Console.ReadLine();

            } while (true);
        }
    }
}
