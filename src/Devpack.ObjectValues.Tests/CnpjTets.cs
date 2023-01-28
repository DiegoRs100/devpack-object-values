using Bogus;
using Bogus.Extensions.Brazil;
using Devpack.Extensions.Types;
using FluentAssertions;
using Xunit;

namespace Devpack.ObjectValues.Tests
{
    public class CnpjTets
    {
        private readonly Faker _faker;

        public CnpjTets()
        {
            _faker = new Faker("pt_BR");
        }

        [Theory(DisplayName = "Deve retornar verdadeiro quando o CNPJ informado for válido.")]
        [InlineData("99.027.840/0001-05")]
        [InlineData("61.342.578/0001-10")]
        [InlineData("52.543.665/0001-45")]
        [InlineData("27.975.689/0001-26")]
        [InlineData("86.844.069/0001-17")]
        [InlineData("95.828.920/0001-09")]
        [InlineData("74.268.736/0001-81")]
        [InlineData("10.515.485/0001-00")]
        [InlineData("58.780.003/0001-75")]
        [InlineData("58.151.674/0001-77")]
        public void IsValid_BeTrue(string cpfNumber)
        {
            var cnpj = new Cnpj(cpfNumber);
            cnpj.IsValid.Should().BeTrue();
        }

        [Theory(DisplayName = "Deve retornar falso quando o CNPJ informado for inválido.")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("99.027.840/0001-00")]
        [InlineData("95.828.92D/0001-09")]
        [InlineData("58.780.003/0001-05")]
        [InlineData("581516740001777")]
        [InlineData("5815167400017")]
        public void IsValid_BeFalse(string cnpjNumber)
        {
            var cnpj = new Cnpj(cnpjNumber);
            cnpj.IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Deve inicializar corretamente o CNPJ sem caracteres especiais quando o objeto for instanciado.")]
        [InlineData("58780003000175")]
        [InlineData("58.780.003/0001-75")]
        public void Constructor(string cnpjNumber)
        {
            var cnpj = new Cnpj(cnpjNumber);
            cnpj.Number.Should().Be("58780003000175");
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando os valores de CNPJ forem iguais.")]
        public void Compare_BeTrue()
        {
            var cnpjString = _faker.Company.Cnpj();
            var cnpj = new Cnpj(cnpjString);

            (cnpj == cnpjString).Should().BeTrue();
            (cnpjString == cnpj).Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar falso quando os valores de CNPJ forem diferentes.")]
        public void Compare_BeFalse()
        {
            var cnpjString = _faker.Company.Cnpj();
            var cnpj = new Cnpj(_faker.Company.Cnpj());

            (cnpj == cnpjString).Should().BeFalse();
            (cnpjString == cnpj).Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando os valores de CNPJ forem diferentes.")]
        public void Uncompare_BeTrue()
        {
            var cnpjString = _faker.Company.Cnpj();
            var cnpj = new Cnpj(_faker.Company.Cnpj());

            (cnpj != cnpjString).Should().BeTrue();
            (cnpjString != cnpj).Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar falso quando os valores de CNPJ forem iguais.")]
        public void Uncompare_BeFalse()
        {
            var cnpjString = _faker.Company.Cnpj();
            var cnpj = new Cnpj(cnpjString);

            (cnpj != cnpjString).Should().BeFalse();
            (cnpjString != cnpj).Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar um objeto nulo quando o CNPJ informado for inválido.")]
        public void TryParse_WhenInvalidCep()
        {
            var result = Cnpj.TryParse(string.Empty, out var cnpj);

            cnpj.Should().BeNull();
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar um objeto de CNPJ instanciado quando o CNPJ informado for válido.")]
        public void TryParse_WhenValidCep()
        {
            var cnpjString = _faker.Company.Cnpj();
            var result = Cnpj.TryParse(cnpjString, out var cnpj);

            (cnpjString == cnpj!.Value).Should().BeTrue();
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar o CNPJ no formato de uma string quando o mesmo for construído corretamente.")]
        public void ToString_Valid()
        {
            var cnpjString = _faker.Company.Cnpj();
            _ = Cnpj.TryParse(cnpjString, out var cnpj);

            cnpj.ToString().Should().Be(cnpjString.GetOnlyDigits());
        }

        [Fact(DisplayName = "Deve retornar o CNPJ formatdo no modelo 00.000.000/0000-00 quando o número é válido.")]
        public void ToFormattedString_Valid()
        {
            _ = Cnpj.TryParse("27975689000126", out var cnpj);
            var formattedCnpj = cnpj!.Value.ToFormattedString();

            formattedCnpj.Should().Be("27.975.689/0001-26");
        }

        [Fact(DisplayName = "Deve retornar uma string vazia quando os dados do CNPJ forem nulos ou vazios.")]
        public void ToFormattedString_Invalid()
        {
            var cnpj = new Cnpj();
            var formattedCnpj = cnpj.ToFormattedString();

            formattedCnpj.Should().BeEmpty();
        }
    }
}