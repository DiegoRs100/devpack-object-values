using Bogus;
using Bogus.Extensions.Brazil;
using Devpack.Extensions.Types;
using FluentAssertions;
using Xunit;

namespace Devpack.ObjectValues.Tests
{
    public class CpfTests
    {
        private readonly Faker _faker;

        public CpfTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Theory(DisplayName = "Deve retornar verdadeiro quando o CPF informado for válido.")]
        [InlineData("129.162.080-03")]
        [InlineData("112.334.110-98")]
        [InlineData("724.454.110-41")]
        [InlineData("454.987.540-27")]
        [InlineData("984.330.990-19")]
        [InlineData("797.156.670-07")]
        [InlineData("394.884.750-99")]
        [InlineData("914.242.920-00")]
        [InlineData("809.287.830-73")]
        [InlineData("887.383.960-68")]
        public void IsValid_BeTrue(string cpfNumber)
        {
            var cpf = new Cpf(cpfNumber);
            cpf.IsValid.Should().BeTrue();
        }

        [Theory(DisplayName = "Deve retornar falso quando o CPF informado for inválido.")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("129.162.080-00")]
        [InlineData("129.162.08D-84")]
        [InlineData("112.334.110-08")]
        [InlineData("1123341100")]
        [InlineData("112334110088")]
        public void IsValid_BeFalse(string cpfNumber)
        {
            var cep = new Cep(cpfNumber);
            cep.IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Deve inicializar corretamente o CPF sem caracteres especiais quando o objeto for instanciado.")]
        [InlineData("12916208003")]
        [InlineData("129.162.080-03")]
        public void Constructor(string cpfNumber)
        {
            var cpf = new Cpf(cpfNumber);
            cpf.Number.Should().Be("12916208003");
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando os valores de CPF forem iguais.")]
        public void Compare_BeTrue()
        {
            var cpfString = _faker.Person.Cpf();
            var cpf = new Cpf(cpfString);

            (cpf == cpfString).Should().BeTrue();
            (cpfString == cpf).Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar falso quando os valores de CPF forem diferentes.")]
        public void Compare_BeFalse()
        {
            var cpfString = _faker.Person.Cpf();
            var cpf = new Cpf(_faker.Address.ZipCode());

            (cpf == cpfString).Should().BeFalse();
            (cpfString == cpf).Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando os valores de CPF forem diferentes.")]
        public void Uncompare_BeTrue()
        {
            var cpfString = _faker.Person.Cpf();
            var cpf = new Cpf(_faker.Address.ZipCode());

            (cpf != cpfString).Should().BeTrue();
            (cpfString != cpf).Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar falso quando os valores de CPF forem iguais.")]
        public void Uncompare_BeFalse()
        {
            var cpfString = _faker.Person.Cpf();
            var cpf = new Cpf(cpfString);

            (cpf != cpfString).Should().BeFalse();
            (cpfString != cpf).Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar um objeto nulo quando o CPF informado for inválido.")]
        public void TryParse_WhenInvalidCep()
        {
            var result = Cpf.TryParse(string.Empty, out var cpf);

            cpf.Should().BeNull();
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar um objeto de CPF instanciado quando o CPF informado for válido.")]
        public void TryParse_WhenValidCep()
        {
            var cpfString = _faker.Person.Cpf();
            var result = Cpf.TryParse(cpfString, out var cpf);

            (cpfString == cpf!.Value).Should().BeTrue();
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar o CPF no formato de uma string quando o mesmo for construído corretamente.")]
        public void ToString_Valid()
        {
            var cpfString = _faker.Person.Cpf();
            _ = Cpf.TryParse(cpfString, out var cpf);

            cpf.ToString().Should().Be(cpfString.GetOnlyDigits());
        }

        [Fact(DisplayName = "Deve retornar o cep formatdo no modelo 00000-000 quando o número é válido.")]
        public void ToFormattedString_Valid()
        {
            _ = Cpf.TryParse("11233411098", out var cpf);
            var formattedCpf = cpf!.Value.ToFormattedString();

            formattedCpf.Should().Be("112.334.110-98");
        }

        [Fact(DisplayName = "Deve retornar uma string vazia quando os dados do CPF forem nulos ou vazios.")]
        public void ToFormattedString_Invalid()
        {
            var cpf = new Cpf();
            var formattedCpf = cpf.ToFormattedString();

            formattedCpf.Should().BeEmpty();
        }
    }
}