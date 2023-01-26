using Bogus;
using FluentAssertions;
using Xunit;

namespace Devpack.ObjectValues.Tests
{
    public class EmailTests
    {
        private readonly Faker _faker;

        public EmailTests()
        {
            _faker = new Faker();
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando o email informado for válido.")]
        public void IsValid_BeTrue()
        {
            var email = new Email(_faker.Internet.Email());
            email.IsValid.Should().BeTrue();
        }

        [Theory(DisplayName = "Deve retornar falso quando o email informado for inválido.")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("email.hotmail.com")]
        [InlineData(".@hotmail.com")]
        [InlineData("@hotmail.com")]
        [InlineData("teste@@hotmail.com")]
        [InlineData("teste@hotmail")]
        [InlineData("teste@")]
        public void IsValid_BeFalse(string address)
        {
            var email = new Email(address);
            email.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Deve inicializar corretamente o endereço de email quando o objeto for instanciado.")]
        public void Constructor()
        {
            var address = _faker.Internet.Email();

            var email = new Email(address);

            email.Address.Should().Be(address);
            email.Domain.Should().Be(address.Split('@').Last());
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando os valores de email forem iguais.")]
        public void Compare_BeTrue()
        {
            var address = _faker.Internet.Email();
            var email = new Email(address);
            
            (email == address).Should().BeTrue();
            (address == email).Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar falso quando os valores de email forem diferentes.")]
        public void Compare_BeFalse()
        {
            var address = _faker.Internet.Email();
            var email = new Email(_faker.Internet.Email());

            (email == address).Should().BeFalse();
            (address == email).Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando os valores de email forem diferentes.")]
        public void Uncompare_BeTrue()
        {
            var address = _faker.Internet.Email();
            var email = new Email(_faker.Internet.Email());

            (email != address).Should().BeTrue();
            (address != email).Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar falso quando os valores de email forem iguais.")]
        public void Uncompare_BeFalse()
        {
            var address = _faker.Internet.Email();
            var email = new Email(address);

            (email != address).Should().BeFalse();
            (address != email).Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar um objeto nulo quando o email informado for inválido.")]
        public void TryParse_WhenInvalidEmail()
        {
            var result = Email.TryParse(string.Empty, out var email);

            email.Should().BeNull();
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar um objeto de email instanciado quando o email informado for válido.")]
        public void TryParse_WhenValidEmail()
        {
            var address = _faker.Internet.Email();
            var result = Email.TryParse(address, out var email);

            (address == email.Value).Should().BeTrue();
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar o endereço de email no formato de uma string quando o email for construído corretamente.")]
        public void ToString_Valid()
        {
            var address = _faker.Internet.Email();
            _ = Email.TryParse(address, out var email);

            email.ToString().Should().Be(address);
        }
    }
}