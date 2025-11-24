using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class LocacaoFuncionario
    {
        public static readonly string ASSOCIARFUNCIONARIO = @"INSERT INTO tblLocacaoFuncionarios (LocacaoID, FuncionarioID) 
                                                              VALUES (@LocacaoID, @FuncionarioID)";
        public static readonly string DESASSOCIARFUNCIONARIO = @"DELETE FROM tblLocacaoFuncionarios WHERE LocacaoID = @LocacaoID
                                                             AND FuncionarioID = @FuncionarioID";

        public readonly static string SELECTLOCACAOPORFUNCIONARIO = @"SELECT LocacaoFuncionarioID, LocacaoID, FuncionarioID 
                                                                 FROM tblLocacaoFuncionarios WHERE FuncionarioID = @FuncionarioID";

        public int LocacaoFuncionarioID { get; set; }
        public Guid LocacaoID { get; set; }
        public int FuncionarioID { get; set; }

        public LocacaoFuncionario(int locacaoFuncionarioID, Guid locacaoID, int funcionarioID)
        {
            LocacaoFuncionarioID = locacaoFuncionarioID;
            LocacaoID = locacaoID;
            FuncionarioID = funcionarioID;
        }

        public override string? ToString()
        {
            return $"ID Associação: {LocacaoFuncionarioID}  |  LocaçãoID: {LocacaoID}  |  FuncionarioID: {FuncionarioID}\n" +
                $"--------------------------------------------------------------------------------------";
        }


    }
}
