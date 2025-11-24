using Locadora.Models;

namespace Locadora.Controller.Validation
{
    public class Validar
    {
        public static string? ValidarInputString(string text)
        {
            Console.Write(text);
            string input = Console.ReadLine()!.Trim();

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("\nCampo obrigatório! Digite novamente ou [9] para sair: ");
                input = Console.ReadLine()!.Trim();

                if (input == "9")
                    return null;
            }
            return input;
        }


        public static string? ValidarInputOpcional(string text)
        {
            Console.Write(text);
            string input = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(input))
                return null;

            return input;
        }


        public static decimal ValidarInputDecimal(string text)
        {
            Console.Write(text);
            string input = Console.ReadLine()!.Trim();

            var validation = Decimal.TryParse(input, out decimal result);

            while (!validation)
            {
                Console.Write("\nInválido! Digite apenas numerico ou [S] para sair: ");
                input = Console.ReadLine()!.Trim();

                if (input.ToUpper() == "S")
                    return 0;

                validation = Decimal.TryParse(input, out result);
            }
            return result;
        }


        public static decimal? ValidarInputDecimalOpcional(string text)
        {
            Console.Write(text);
            string input = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(input)) return null;

            var validation = Decimal.TryParse(input, out decimal result);

            while (!validation)
            {
                Console.Write("\nInválido! Digite apenas numerico ou [S] para sair: ");
                input = Console.ReadLine()!.Trim();

                if (input.ToUpper() == "S")
                    return null;

                validation = Decimal.TryParse(input, out result);
            }
            return result;
        }


        public static int? ValidarInputInt(string text)
        {
            Console.Write(text);
            string input = Console.ReadLine()!.Trim();

            var validation = int.TryParse(input, out int result);

            while (!validation)
            {
                Console.Write("\nInválido! Digite apenas numerico ou [S] para sair: ");
                input = Console.ReadLine()!;

                if (input.Trim().ToUpper() == "S")
                    return null;

                validation = int.TryParse(input, out result);
            }
            return result;
        }


        public static DateOnly ValidarInputDateOnly(string text)
        {
            Console.Write(text);
            string input = Console.ReadLine()!;
            DateOnly data;

            while (!DateOnly.TryParseExact(input, "dd/MM/yyyy", out data))
            {
                Console.Write("\nData inválida! Digite novamente [dd/MM/yyyy]: ");
                input = Console.ReadLine()!;
            }
            return data;
        }


        public static Veiculo? ValidarInputVeiculo(string placa)
        {
            var busca = new VeiculoController();

            var vehicle = busca.BuscarVeiculoPlaca(placa);

            Console.WriteLine("\n=-=-=-=-=-=-=-=-=      >   Veículo   <      =-=-=-=-=-=-=-=-=\n");

            Console.WriteLine($"Modelo: {vehicle.Modelo} |  Categoria: {vehicle.Categoria.Nome}  |  Valor da Diária: R$ {vehicle.Categoria.Diaria}\n");

            Console.Write("Deseja seguir com o veículo acima? [S/N]: ");
            string input = Console.ReadLine()!.Trim().ToUpper();

            while (input != "S" && input != "N")
            {
                Console.Write("\nCampo obrigatório! Digite [S/N]: ");
                input = Console.ReadLine()!.Trim().ToUpper();
            }

            if (input == "N")
                return null;

            return vehicle;
        }


        public static Cliente? ValidarInputCliente(string email)
        {
            var busca = new ClienteController();

            var customer = busca.BuscarClienteEmail(email);

            if (customer is null)
            {
                Console.WriteLine("Não existe cliente com esse email cadastrado!");
                return null;
            }

            return customer;
        }


        public static Guid? ValidarInputGuid(string id)
        {
            id = id.Trim();

            bool valido = Guid.TryParse(id, out Guid guid);

            while (!valido)
            {
                Console.Write("Inválido! Digite um ID em formato GUID ou [S] para sair: ");
                id = Console.ReadLine()!.Trim();

                if (id.Equals("S", StringComparison.OrdinalIgnoreCase))
                    return null;

                valido = Guid.TryParse(id, out guid);
            }

            return guid;
        }

    }
}
