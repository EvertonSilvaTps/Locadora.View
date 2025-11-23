namespace Locadora.Models
{
    public class Funcionario
    {
        public readonly static string INSERTFUNCIONARIO = "INSERT INTO tblFuncionarios VALUES (@Nome, @CPF, @Email, @Salario)";

        public readonly static string SELECTALLFUNCIONARIOS = @"SELECT Nome, CPF, Email, Salario 
                                                                FROM tblFuncionarios";

        public readonly static string SELECTFUNCIONARIOSPOREMAIL = @"SELECT * FROM tblFuncionarios 
                                                                    WHERE Email = @Email";

        public readonly static string UPDATESALARIOFUNCIONARIO = @"UPDATE tblFuncionarios SET Salario = @Salario 
                                                                    WHERE FuncionarioID = @FuncionarioID";

        public readonly static string DELETEFUNCIONARIO = @"DELETE FROM tblFuncionarios 
                                                            WHERE FuncionarioID = @FuncionarioID";


        public int FuncionarioID { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }
        public decimal? Salario { get; private set; }

        public Funcionario(string nome, string cpf, string email)
        {
            Nome = nome;
            CPF = cpf;
            Email = email;
        }

        public Funcionario(string nome, string cpf, string email, decimal? salario)
            : this(nome, cpf, email)
        {
            Salario = salario;
        }

        public void setFuncionarioID(int funcionarioId)
        {
            FuncionarioID = funcionarioId;
        }

        public void setSalario(decimal salario)
        {
            Salario = salario;
        }


        public override string? ToString()
        {
            return $"Nome: {Nome}  |  CPF: {CPF}\n" +
                $"Email: {Email}  |  Salário: {(Salario == null ? "Sem sálario" : Salario)}\n" +
                $"------------------------------------------------------";
        }
    }
}
