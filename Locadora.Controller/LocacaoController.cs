using Locadora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Controller
{
    public class LocacaoController
    {
        public void AdicionarLocacao(Locacao locacao)
        {

        }

        public List<Locacao> ListarTodasLocacoes()
        {
            throw new NotImplementedException();
        }


        // busca locação se estiver ativa ; senão devolve mensagem de locação inexistente ou status de 'finalizada' ou 'cancelada'
        public Locacao BuscarLocacaoPorNomeCliente(string nomeCliente)
        {
            throw new NotImplementedException();
        }


        // Aqui eu vou chamar a função BuscarVeiculoPlaca (de Veículo) - somente trazer opções de veículos com status > 'Disponível"
        public void AtualizarVeiculoIDLocacao(string nomeCliente, string placa)
        {

        }

        // A partir da mudança de dias, vai atualizar a "DataDevolucaoPrevista" e "ValorTotal"
        public void AtualizarDiasLocacao(decimal nomeCliente, string diasLocacao)
        {

        }

        // Se mudar pra 'finalizada', altera também a DataDevReal e ValorTotal (considerando dias locado, e multa (se aplicado)
        // Se mudar pra 'cancelado', altera também a DataDevReal
        public void AtualizarStatus(decimal nomeCliente, string diasLocacao)
        {

        }
    }
}
