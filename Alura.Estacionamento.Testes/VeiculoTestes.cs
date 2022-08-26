using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class VeiculoTestes : IDisposable
    {
        private Veiculo _veiculo;
        private ITestOutputHelper SaidaConsoleTeste;

        public VeiculoTestes(ITestOutputHelper _saidaConsoleTeste)
        {
            _veiculo = new Veiculo();
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado.");
        }

        [Fact]
        public void Testa_veiculoAcelerar()
        {
            //Arrange todo ferramental necessário, como variáveis etc
            //Act o que eu quero testar, como um método etc
            _veiculo.Acelerar(10);
            //Assert o resultado do teste
            Assert.Equal(100, _veiculo.VelocidadeAtual);
        }

        [Fact]
        public void Testa_veiculoFrear()
        {
            _veiculo.Frear(10);
            Assert.Equal(-150, _veiculo.VelocidadeAtual);
        }

        [Theory]
        [InlineData("Lucas Silva", "ASD-1498", "Preto", "Fiesta - sedan")]
        public void Dados_veiculo(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = TipoVeiculo.Automovel;
            _veiculo.Placa = placa;
            _veiculo.Cor = cor;
            _veiculo.Modelo = modelo;

            //Act
            string dados = _veiculo.ToString();

            SaidaConsoleTeste.WriteLine(dados);

            //Assert
            Assert.Contains("Tipo do Veículo: Automovel", dados);
        }

        [Fact]
        public void TestaNomeProprietarioVeiculoComMenosDeTresCaracteres()
        {
            //Arrange
            string nomeProprietario = "Lu";

            //Assert
            Assert.Throws<System.FormatException>(
                    //Act
                    () => new Veiculo(nomeProprietario)
                );
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose invocado.");
        }
    }
}
