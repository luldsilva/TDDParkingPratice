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

namespace Alura.Estacionamento.Testes
{
    public class PatioTestes
    {
        [Fact]
        public void ValidaFaturamento()
        {
            //Arrange
            var estacionamento = new Patio();
            var veiculo = new Veiculo();
            veiculo.Proprietario = "Lucas Silva";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Preto";
            veiculo.Modelo = "Fiesta Sedan";
            veiculo.Placa = "asd-9999";

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

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
            Patio estacionamento = new Patio();
            Veiculo veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();
            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [Trait("Faturamento", "Fatura pátio")]
        [InlineData("Lucas Silva", "ASD-1498", "Preto", "Fiesta - sedan")]
        [InlineData("Sarah Silva", "BCD-4917", "Preto", "Cruze")]
        [InlineData("Deborah Silva", "YYA-7855", "cinza", "Jeep - compass")]

        public void LocalizaVeiculoNoPatio(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            Patio estacionamento = new Patio();
            Veiculo veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculo(veiculo.Placa);

            //Assert
            Assert.Equal(placa, consultado.Placa);

        }

        [Theory]
        [Trait("Faturamento", "Fatura pátio")]
        [InlineData("Lucas Silva", "ASD-1498", "Preto", "Fiesta - sedan")]

        public void AlterarDadosVeiculo(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            Patio estacionamento = new Patio();
            Veiculo veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor; //Cor preto
            veiculo.Modelo = modelo;
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoComAlteracao = new Veiculo();
            veiculoComAlteracao.Proprietario = "Lucas Silva";
            veiculoComAlteracao.Placa = "ASD-1498";
            veiculoComAlteracao.Cor = "Branco"; //Alteracao acontece aqui
            veiculoComAlteracao.Modelo = "Fiesta - sedan";

            //Act
            Veiculo veiculoAlterado = estacionamento.AlteraDadosVeiculo(veiculoComAlteracao);

            //Assert
            Assert.Equal(veiculoAlterado.Cor, veiculoComAlteracao.Cor);


        }

    }
}
