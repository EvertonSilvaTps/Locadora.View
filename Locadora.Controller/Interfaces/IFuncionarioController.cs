using Locadora.Models;

namespace Locadora.Controller.Interfaces
{
    public interface IFuncionarioController
    {
        public void AdicionarFuncionario(Funcionario funcionario);

        public List<Funcionario> ListarTodosFuncionarios();

        public Funcionario BuscarFuncionarioEmail(string email);

        public void AtualizarSalarioFuncionario(string email, decimal salario);

        public void DeletarFuncionario(string email);
    }
}
