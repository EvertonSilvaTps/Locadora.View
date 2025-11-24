using Locadora.Controller.Interfaces;
using Locadora.Controller.Validation;
using Locadora.Models;
using Locadora.Models.Enums;

namespace Locadora.Controller.Menu
{
    public class LocacaoMenu
    {
        private LocacaoController Controller = new LocacaoController();


        private void InsertService()
        {
            string? email = Validar.ValidarInputString("Cliente > Informe o email: ");
            if (email == null) return;

            var customer = Validar.ValidarInputCliente(email);
            if (customer == null) return;

            string? plate = Validar.ValidarInputString("Veículo > Informe a placa: ");
            if (plate == null) return;

            var vehicle = Validar.ValidarInputVeiculo(plate);
            if (vehicle == null)
            {
                Console.WriteLine("Retornando para o menu...");
                Thread.Sleep(5000);
                return;
            }

            int? day = Validar.ValidarInputInt("Dias > Qtde. de dias: ");
            if (day == null) return;

            Locacao locacao = new Locacao(customer, vehicle, day.Value);

            try
            {
                Controller.AdicionarLocacao(locacao);
                Console.WriteLine("\n   >>>   Locação registrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Selecionar funcionarios
            Console.WriteLine("\n     =-=-= Seleção de Funcionários =-=-=\n");

            FuncionarioController funcionarioController = new FuncionarioController();
            LocacaoFuncionarioController locacaoFuncionariosController = new LocacaoFuncionarioController();

            var funcionarios = funcionarioController.ListarTodosFuncionarios();

            foreach (var f in funcionarios)
                Console.WriteLine($"{f.FuncionarioID} - {f.Nome}");

            List<int> funcionariosEscolhidos = new();

            while (true)
            {
                int cont = 1;

                int? funcId = Validar.ValidarInputIntFun("\nDigite o ID do funcionário que prestou o serviço de locação (S para finalizar): ");

                if (funcId == null) break;

                if (!funcionarios.Exists(f => f.FuncionarioID == funcId))
                {
                    Console.WriteLine("\n  Funcionário não encontrado!");
                    continue;
                }

                try
                {
                    locacaoFuncionariosController.AssociarFuncionario(locacao.LocacaoID, funcId.Value);
                    funcionariosEscolhidos.Add(funcId.Value);
                    Console.WriteLine($"Funcionário {funcId} associado!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao associar funcionário: " + ex.Message);
                }
                
                if (funcionariosEscolhidos.Count == 0)
                {
                    Console.WriteLine("\nÉ obrigatório selecionar ao menos 1 funcionário!");
                    continue;
                }
            }


            Console.WriteLine("\n   >>>   Funcionário associado a locação com sucesso!");
        }

        private void SelectAllService()
        {
            Console.Clear();
            Console.WriteLine();

            try
            {
                var list = Controller.ListarTodasLocacoes();

                foreach (var rental in list)
                {
                    Console.WriteLine(rental);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void SelectCustomerService()
        {
            Console.Clear();
            Console.WriteLine();

            string? email = Validar.ValidarInputString("Informe o email do cliente para busca: ");
            if (email == null) return;

            var clienteController = new ClienteController();

            var customer = clienteController.BuscarClienteEmail(email);
            if (customer is null)
            {
                Console.WriteLine("\nNão existe cliente com esse email cadastrado!");
                return;
            }

            Console.WriteLine("\n                   =-=-=   >  Cliente  <   =-=-=\n");
            Console.WriteLine(customer + "\n");

            var idCliente = customer.ClienteID;

            try
            {
                var list = Controller.ListarLocacaoPorCliente(idCliente);

                foreach (var rental in list)
                {
                    Console.WriteLine(rental);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void SelectEmployeerService()
        {
            Console.Clear();
            Console.WriteLine();

            string? email = Validar.ValidarInputString("Informe o email do funcionario para busca: ");
            if (email == null) return;

            var funcionarioController = new FuncionarioController();

            var employeer = funcionarioController.BuscarFuncionarioEmail(email);
            if (employeer is null)
            {
                Console.WriteLine("\nNão existe funcionario com esse email cadastrado!");
                return;
            }

            Console.WriteLine("\n                   =-=-=   >  Funcionario  <   =-=-=\n");
            Console.WriteLine(employeer + "\n");

            var idFuncionario = employeer.FuncionarioID;
            LocacaoFuncionarioController controllerFun = new LocacaoFuncionarioController();

            try
            {
                var list = controllerFun.ListarLocacaoPorFuncionario(idFuncionario);

                foreach (var rental in list)
                {
                    Console.WriteLine(rental);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void SelectStatusService()
        {
            Console.Clear();
            Console.WriteLine();

            Console.WriteLine("  >  [1] Ativa | [2] Finalizada | [3] Cancelada  <");
            int? rentalStatus = Validar.ValidarInputInt("\n Informe o Status desejado: ");
            if (rentalStatus == null || (rentalStatus is not 1 && rentalStatus is not 2 && rentalStatus is not 3)) return;

            string status;

            if (rentalStatus == 1)
                status = EStatusLocacao.Ativa.ToString();
            else if (rentalStatus == 2)
                status = EStatusLocacao.Finalizada.ToString();
            else
                status = EStatusLocacao.Cancelada.ToString();

            Console.WriteLine("\n                                   =-=-=   >  Locaçãoes  <   =-=-=\n");

            try
            {
                var list = Controller.ListarLocacaoPorStatus(status);

                foreach (var rental in list)
                {
                    Console.WriteLine(rental);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void UpdateStatusService()
        {
            var input = Validar.ValidarInputString("ID da locação: ");
            if (input == null) return;

            var guid = Validar.ValidarInputGuid(input);
            if (guid == null) return;

            try
            {
                var rental = Controller.BuscarLocacaoPorId(guid.Value);
                if (rental is null)
                {
                    Console.WriteLine("\nNão existe locação com esse ID!");
                    return;
                }

                bool statusBloqueado = rental.Status == EStatusLocacao.Ativa.ToString();

                if (!statusBloqueado)
                {
                    Console.WriteLine("\nUpdate inválido! Locações já finalizadas ou canceladas não podem ser alteradas");
                    return;
                }

                Console.WriteLine("\n\n                                    =-=-=   >  Locação  <   =-=-=\n");
                Console.WriteLine(rental);

                int? rentalStatus = Validar.ValidarInputInt("\n Informe o novo status [1] Finalizada | [2] Cancelada: ");
                if (rentalStatus == null || (rentalStatus is not 1 && rentalStatus is not 2)) return;

                if (rentalStatus == 1)
                    Controller.AtualizarStatusLocacao(guid.Value, EStatusLocacao.Finalizada.ToString());
                else if (rentalStatus == 2)
                    Controller.AtualizarStatusLocacao(guid.Value, EStatusLocacao.Cancelada.ToString());

                Console.WriteLine("\n >>>  Status atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void MenuLocacao()
        {
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-|");
                Console.WriteLine(" |                     >      Locação      <                   |");
                Console.WriteLine(" |-------------------------------------------------------------|");
                Console.WriteLine(" | [ 1 ] Registrar Locação   |   [ 2 ] Exibir Locações         |");
                Console.WriteLine(" | [ 3 ] Atualizar Status    |   [ 4 ] Exibir Por Cliente      |");
                Console.WriteLine(" | [ 5 ] Exibir Por Status   |   [ 6 ] Exibir Por Funcionario  |");
                Console.WriteLine(" | [ 7 ] Voltar              |                                 |");
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-|");
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
                        SelectAllService();
                        break;
                    case 3:
                        UpdateStatusService();
                        break;
                    case 4:
                        SelectCustomerService();
                        break;
                    case 5:
                        SelectStatusService();
                        break;
                    case 6:
                        SelectEmployeerService();
                        break;
                    case 7:
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
