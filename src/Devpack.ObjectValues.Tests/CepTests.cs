using Bogus;
using Devpack.Extensions.Types;
using FluentAssertions;
using Xunit;

namespace Devpack.ObjectValues.Tests
{
    public class CepTests
    {
        private readonly Faker _faker;

        public CepTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando o cep informado for válido.")]
        public void IsValid_BeTrue()
        {
            var cep = new Cep(_faker.Address.ZipCode());
            cep.IsValid.Should().BeTrue();
        }

        [Theory(DisplayName = "Deve retornar falso quando o cep informado for inválido.")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("1234567")]
        [InlineData("123456789")]
        public void IsValid_BeFalse(string cepNumber)
        {
            var cep = new Cep(cepNumber);
            cep.IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Deve inicializar corretamente o cep sem caracteres especiais quando o objeto for instanciado.")]
        [InlineData("12345678")]
        [InlineData("1234-5678")]
        public void Constructor(string cepNumber)
        {
            var cep = new Cep(cepNumber);
            cep.Number.Should().Be("12345678");
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando os valores de cep forem iguais.")]
        public void Compare_BeTrue()
        {
            var cepString = _faker.Address.ZipCode();
            var cep = new Cep(cepString);

            (cep == cepString).Should().BeTrue();
            (cepString == cep).Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar falso quando os valores de cep forem diferentes.")]
        public void Compare_BeFalse()
        {
            var cepString = _faker.Address.ZipCode();
            var cep = new Cep(_faker.Address.ZipCode());

            (cep == cepString).Should().BeFalse();
            (cepString == cep).Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando os valores de cep forem diferentes.")]
        public void Uncompare_BeTrue()
        {
            var cepString = _faker.Address.ZipCode();
            var cep = new Cep(_faker.Address.ZipCode());

            (cep != cepString).Should().BeTrue();
            (cepString != cep).Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar falso quando os valores de cep forem iguais.")]
        public void Uncompare_BeFalse()
        {
            var cepString = _faker.Address.ZipCode();
            var cep = new Cep(cepString);

            (cep != cepString).Should().BeFalse();
            (cepString != cep).Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar um objeto nulo quando o cep informado for inválido.")]
        public void TryParse_WhenInvalidCep()
        {
            var result = Cep.TryParse(string.Empty, out var cep);

            cep.Should().BeNull();
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar um objeto de cep instanciado quando o cep informado for válido.")]
        public void TryParse_WhenValidCep()
        {
            var cepString = _faker.Address.ZipCode();
            var result = Cep.TryParse(cepString, out var cep);

            (cepString == cep!.Value).Should().BeTrue();
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar o cep no formato de uma string quando o mesmo for construído corretamente.")]
        public void ToString_Valid()
        {
            var cepString = _faker.Address.ZipCode();
            _ = Cep.TryParse(cepString, out var cep);

            cep.ToString().Should().Be(cepString.GetOnlyDigits());
        }

        [Fact(DisplayName = "Deve retornar o cep formatdo no modelo 00000-000 quando o número é válido.")]
        public void ToFormattedString_Valid()
        {
            _ = Cep.TryParse("52365001", out var cep);
            var formattedCep = cep!.Value.ToFormattedString();

            formattedCep.Should().Be("52365-001");
        }

        [Fact(DisplayName = "Deve retornar uma string vazia quando os dados do cep forem nulos ou vazios.")]
        public void ToFormattedString_Invalid()
        {
            var cep = new Cep();
            var formattedCep = cep.ToFormattedString();

            formattedCep.Should().BeEmpty();
        }
    }
}