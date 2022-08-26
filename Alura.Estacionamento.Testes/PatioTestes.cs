using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class PatioTestes : IDisposable
    {
        private Veiculo _veiculo;
        private Patio _estacionamento;
        private ITestOutputHelper SaidaConsoleTeste;
        public PatioTestes(ITestOutputHelper _saidaConsoleTeste)
        {
            _veiculo = new Veiculo();
            _estacionamento = new Patio();
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado.");
        }
        [Fact]
        public void ValidaFaturamentoComVeiculo()
        {
            //Arrange
            _veiculo.Proprietario = "Lucas Silva";
            _veiculo.Tipo = TipoVeiculo.Automovel;
            _veiculo.Cor = "Preto";
            _veiculo.Modelo = "Fiesta Sedan";
            _veiculo.Placa = "asd-9999";

            _estacionamento.RegistrarEntradaVeiculo(_veiculo);
            _estacionamento.RegistrarSaidaVeiculo(_veiculo.Placa);

            //Act
            double faturamento = _estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [Trait("Faturamento", "Fatura pátio")]
        [InlineData("Lucas Silva", "ASD-1498", "Preto", "Fiesta - sedan")]
        [InlineData("Sarah Silva", "BCD-4917", "Preto", "Cruze")]
        [InlineData("Deborah Silva", "YYA-7855", "cinza", "Jeep - compass")]

        public void ValidaFaturamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            _veiculo.Proprietario = proprietario;
            _veiculo.Placa = placa;
            _veiculo.Cor = cor;
            _veiculo.Modelo = modelo;
            _estacionamento.RegistrarEntradaVeiculo(_veiculo);
            _estacionamento.RegistrarSaidaVeiculo(_veiculo.Placa);

            //Act
            double faturamento = _estacionamento.TotalFaturado();
            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [Trait("Faturamento", "Fatura pátio")]
        [InlineData("Lucas Silva", "ASD-1498", "Preto", "Fiesta - sedan")]
        [InlineData("Sarah Silva", "BCD-4917", "Preto", "Cruze")]
        [InlineData("Deborah Silva", "YYA-7855", "cinza", "Jeep - compass")]

        public void LocalizaVeiculoNoPatioComBaseIdTicket(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            _veiculo.Proprietario = proprietario;
            _veiculo.Placa = placa;
            _veiculo.Cor = cor;
            _veiculo.Modelo = modelo;
            _estacionamento.RegistrarEntradaVeiculo(_veiculo);

            SaidaConsoleTeste.WriteLine("Id ticket: "+ _veiculo.IdTicket );
            SaidaConsoleTeste.WriteLine("Ticket: " + _veiculo.Ticket);
            //Act
            var consultado = _estacionamento.PesquisaVeiculo(_veiculo.IdTicket);

            //Assert
            Assert.Contains("### Ticket Estacionamento ###", consultado.Ticket);

        }

        [Theory]
        [InlineData("Lucas Silva", "ASD-1498", "Preto", "Fiesta - sedan")]

        public void AlterarDadosVeiculo(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            _veiculo.Proprietario = proprietario;
            _veiculo.Placa = placa;
            _veiculo.Cor = cor; //Cor preto
            _veiculo.Modelo = modelo;
            _estacionamento.RegistrarEntradaVeiculo(_veiculo);

            var veiculoComAlteracao = new Veiculo();
            veiculoComAlteracao.Proprietario = "Lucas Silva";
            veiculoComAlteracao.Placa = "ASD-1498";
            veiculoComAlteracao.Cor = "Branco"; //Alteracao acontece aqui
            veiculoComAlteracao.Modelo = "Fiesta - sedan";

            //Act
            Veiculo veiculoAlterado = _estacionamento.AlteraDadosVeiculo(veiculoComAlteracao);

            SaidaConsoleTeste.WriteLine("Veiculo com alteracao: " + veiculoComAlteracao.ToString());
            SaidaConsoleTeste.WriteLine("Veiculo consultado e alterado: " + veiculoAlterado.ToString());

            //Assert
            Assert.Equal(veiculoAlterado.Cor, veiculoAlterado.Cor);
        }



        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Disposable invocado.");
        }
    }
}
