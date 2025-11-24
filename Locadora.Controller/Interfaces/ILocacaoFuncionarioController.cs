using Locadora.Models;

namespace Locadora.Controller.Interfaces
{
    public interface ILocacaoFuncionarioController
    {
        public void AdicionarLocacaoFuncionario(LocacaoFuncionario locacaoFuncionario, Funcionario funcionario);

        public List<LocacaoFuncionario> ListarLocaoesFuncionarios();

        public LocacaoFuncionario BuscarLocacaoFuncionarioPorId(int id);
    }
}
