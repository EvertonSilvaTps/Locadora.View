namespace Locadora.Models
{
    public class Funcionario
    {
        public int FuncioonarioID { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }
        public decimal? Salario { get; private set; }

        public Funcionario(string nome, string cPF, string email)
        {
            Nome = nome;
            CPF = cPF;
            Email = email;
        }

        public Funcionario(string nome, string cPF, string email, decimal? salario)
            : this(nome, cPF, email)
        {
            Salario = salario;
        }

        public override string? ToString()
        {
            return $"Nome: {Nome}\nCPF: {CPF}\nEmail: {Email}\nSalário: {Salario}\n";
        }
    }
}
