namespace Locadora.Controller.Validation
{
    public class Validar
    {
        public static string? ValidarInputString(string text)
        {
            Console.Write(text);
            string input = Console.ReadLine()!;

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("\nCampo obrigatório! Digite novamente ou [9] para sair: ");
                input = Console.ReadLine()!;

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

    }
}
