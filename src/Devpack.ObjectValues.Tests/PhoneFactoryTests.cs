using Devpack.ObjectValues.Phones;
using FluentAssertions;
using Xunit;

namespace Devpack.ObjectValues.Tests
{
    public class PhoneFactoryTests
    {
        [Theory(DisplayName = "Deve retornar um telefone celular inválido quando o tamanho do número for inválido.")]
        [InlineData("+455 (11) 94424-02075")]
        [InlineData("455119442402078")]
        [InlineData("144240207")]
        public void CreateCellPhoneNumber_WhenInvalidLength(string number)
        {
            var phone = PhoneFactory.CreateCellPhoneNumber(number);
            phone.PhoneType.Should().Be(PhoneType.Invalid);
        }

        [Theory(DisplayName = "Deve montar um telefone celular quando um número com tamanhp válido for passado.")]
        [InlineData("+455 (11) 94424-0207", "11", "944240207")]
        [InlineData("5511944240200", "11", "944240200")]
        [InlineData("14942402071", "14", "942402071")]
        public void CreateCellPhoneNumber_WhenValidNumber(string number, string expectedDdd, string expectedNumber)
        {
            var phone = PhoneFactory.CreateCellPhoneNumber(number);

            phone.Ddd.Should().Be(expectedDdd);
            phone.Number.Should().Be(expectedNumber);
            phone.PhoneType.Should().Be(PhoneType.CellPhone);
        }

        [Theory(DisplayName = "Deve retornar um telefone fixo inválido quando o tamanho do número for inválido.")]
        [InlineData("+455 (11) 4424-02075")]
        [InlineData("45511442402078")]
        [InlineData("14424020")]
        public void CreateLandlineNumber_WhenInvalidLength(string number)
        {
            var phone = PhoneFactory.CreateLandlineNumber(number);
            phone.PhoneType.Should().Be(PhoneType.Invalid);
        }

        [Theory(DisplayName = "Deve montar um telefone fixo quando um número com tamanhp válido for passado.")]
        [InlineData("+455 (11) 4424-0207", "11", "44240207")]
        [InlineData("551144240200", "11", "44240200")]
        [InlineData("1442402071", "14", "42402071")]
        public void CreateLandlineNumber_WhenValidNumber(string number, string expectedDdd, string expectedNumber)
        {
            var phone = PhoneFactory.CreateLandlineNumber(number);

            phone.Ddd.Should().Be(expectedDdd);
            phone.Number.Should().Be(expectedNumber);
            phone.PhoneType.Should().Be(PhoneType.Landline);
        }
    }
}