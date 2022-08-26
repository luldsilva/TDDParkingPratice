using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;

namespace Alura.Estacionamento.Testes
{
    public class VeiculoTestes
    {
        [Fact(DisplayName = "Teste Acelerando")]
        [Trait("Funcionalidade", "Acelerar")]
        public void TestaVeiculoAcelerar()
        {
            //Arrange todo ferramental necessário, como variáveis etc
            var veiculo = new Veiculo();
            //Act o que eu quero testar, como um método etc
            veiculo.Acelerar(10);
            //Assert o resultado do teste
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact (DisplayName = "Teste Freando")]
        [Trait("Funcionalidade", "Frear")]
        public void TestaVeiculoFrear()
        {
            var veiculo = new Veiculo();
            veiculo.Frear(10);
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Theory]
        [Trait("Faturamento", "Fatura pátio")]
        [InlineData("Lucas Silva", "ASD-1498", "Preto", "Fiesta - sedan")]
        public void DadosVeiculo(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            var  veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;

            //Act
            string dados = veiculo.ToString();

            //Assert
            Assert.Contains("Tipo do Veículo: Automovel", dados);
        }
    }
}
