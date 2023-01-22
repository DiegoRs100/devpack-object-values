using Devpack.ObjectValues.Phones;
using FluentAssertions;
using Xunit;

namespace Devpack.ObjectValues.Tests
{
    public class PhoneTests
    {
        [Theory(DisplayName = "Deve retornar um telefone celular válido quando o formato do número estiver correto.")]
        [InlineData("+55(011) 96111-1111", "11961111111")]
        [InlineData("+5 (011)98111-1111", "11981111111")]
        [InlineData("(011)99111-1111", "11991111111")]
        [InlineData("(011)96111 -1111", "11961111111")]
        [InlineData("(11)9711 1-1111", "11971111111")]
        [InlineData("(11) 97111-1111", "11971111111")]
        public void TryParse_CellPhone_Valid(string number, string expectedPhone)
        {
            var result = Phone.TryParse(number, out var phone);

            result.Should().BeTrue();
            phone.Value.PhoneType.Should().Be(PhoneType.CellPhone);
            phone.ToString().Should().Be(expectedPhone);
        }

        [Theory(DisplayName = "Deve retornar nulo quando o número de celular for inválido.")]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("(01)1111-1111")]
        [InlineData("1111-1111")]
        [InlineData("91111-1111")]
        [InlineData("+55(011) 2111-1111")]
        [InlineData("+5(011)3111-1111")]
        [InlineData("(011)4111-1111")]
        [InlineData("(011)05111-1111")]
        [InlineData("(011)39111-1111")]
        [InlineData("+55 (011) 39111-1111")]
        [InlineData("+5 (011)39111-1111")]
        [InlineData("+55(011) 7111-1111")]
        [InlineData("+5(011)9111-1111")]
        public void TryParse_NumeroInvalido_Deve_Retornar_Nulo(string value)
        {
            var result = Phone.TryParse(value, out var actual);

            result.Should().BeFalse();
            actual.Should().BeNull();
        }

        [Theory(DisplayName = "Deve retornar verdadeiro quando os valores de telefone forem iguais.")]
        [InlineData(PhoneType.CellPhone)]
        [InlineData(PhoneType.Landline)]
        public void Compare_BeTrue(PhoneType phoneType)
        {
            var phoneCompare = "(11) 9 6111-1111";
            var telefone = new Phone("11", "961111111", phoneType);

            var result = telefone == phoneCompare;

            result.Should().BeTrue();
        }

        [Theory(DisplayName = "Deve retornar falso quando os valores de telefone forem diferentes.")]
        [InlineData(PhoneType.CellPhone)]
        [InlineData(PhoneType.Landline)]
        public void Compare_BeFalse(PhoneType phoneType)
        {
            var phoneCompare = "11961111111";
            var telefone = new Phone("11", "961111112", phoneType);

            var result = telefone == phoneCompare;

            result.Should().BeFalse();
        }

        [Theory(DisplayName = "Deve retornar verdadeiro quando os valores de telefone forem diferentes.")]
        [InlineData(PhoneType.CellPhone)]
        [InlineData(PhoneType.Landline)]
        public void Uncompare_BeTrue(PhoneType phoneType)
        {
            var phoneCompare = "11961111111";
            var telefone = new Phone("11", "11111111", phoneType);

            var result = telefone != phoneCompare;

            result.Should().BeTrue();
        }

        [Theory(DisplayName = "Deve retornar falso quando os valores de telefone forem iguais.")]
        [InlineData(PhoneType.CellPhone)]
        [InlineData(PhoneType.Landline)]
        public void Uncompare_BeFalse(PhoneType phoneType)
        {
            var phoneCompare = "(11) 9 6111-1111";
            var telefone = new Phone("11", "961111111", phoneType);

            var result = telefone != phoneCompare;

            result.Should().BeFalse();
        }

        [Theory(DisplayName = "Deve retornar o ddd e número concatenados quando ambos tiverem valores.")]
        [InlineData(PhoneType.CellPhone)]
        [InlineData(PhoneType.Landline)]
        public void ToString_Valid(PhoneType phoneType)
        {
            var expected = "11961111111";

            var phone = new Phone("11", "961111111", phoneType);
            var fullNumber = phone.ToString();

            fullNumber.Should().Be(expected);
        }

        [Fact(DisplayName = "Deve retornar uma string vazia quando os dados do telefone forem nulos ou vazios (ToString).")]
        public void ToString_Invalid()
        {
            var phone = new Phone();
            var formattedPhone = phone.ToString();

            formattedPhone.Should().BeEmpty();
        }

        [Theory(DisplayName = "Deve retornar o telefone formatdo no modelo (00) 00000-0000 quando o número é válido.")]
        [InlineData(PhoneType.CellPhone)]
        [InlineData(PhoneType.Landline)]
        public void GetFormattedNumber_Valid(PhoneType phoneType)
        {
            var expectedPhone = "(11) 96111-1111";

            var phone = new Phone("11", "961111111", phoneType);
            var formattedPhone = phone.GetFormattedNumber();

            formattedPhone.Should().Be(expectedPhone);
        }

        [Fact(DisplayName = "Deve retornar uma string vazia quando os dados do telefone forem nulos ou vazios (GetFormattedNumber).")]
        public void GetFormattedNumber_Invalid()
        {
            var phone = new Phone();
            var formattedPhone = phone.GetFormattedNumber();

            formattedPhone.Should().BeEmpty();
        }

        //        [Theory(DisplayName = "Deve retornar telefone nulo quando o DDD é inválido.")]
        //        [InlineData("01")]
        //        [InlineData("10")]
        //        [InlineData("23")]
        //        [InlineData("25")]
        //        [InlineData("26")]
        //        [InlineData("29")]
        //        [InlineData("30")]
        //        [InlineData("36")]
        //        [InlineData("39")]
        //        [InlineData("40")]
        //        [InlineData("50")]
        //        [InlineData("52")]
        //        [InlineData("56")]
        //        [InlineData("57")]
        //        [InlineData("58")]
        //        [InlineData("59")]
        //        [InlineData("60")]
        //        [InlineData("70")]
        //        [InlineData("72")]
        //        [InlineData("76")]
        //        [InlineData("78")]
        //        [InlineData("80")]
        //        [InlineData("90")]
        //        public void DddInvalido_Deve_Retornar_False(string ddd)
        //        {
        //            // Arrange
        //            var numero = $"+55{ddd}961111111";

        //            // Act
        //            var result = Telefone.TryParse(numero, out var actual);

        //            // Assert
        //            result.Should().BeFalse();
        //            actual.Should().BeNull();
        //        }

        //        [Theory(DisplayName = "Deve realizar a sanitização do telefone quando o DDD é válido.")]
        //        [InlineData("11")]
        //        [InlineData("12")]
        //        [InlineData("13")]
        //        [InlineData("14")]
        //        [InlineData("15")]
        //        [InlineData("16")]
        //        [InlineData("17")]
        //        [InlineData("18")]
        //        [InlineData("19")]
        //        [InlineData("21")]
        //        [InlineData("22")]
        //        [InlineData("24")]
        //        [InlineData("27")]
        //        [InlineData("28")]
        //        [InlineData("31")]
        //        [InlineData("32")]
        //        [InlineData("33")]
        //        [InlineData("34")]
        //        [InlineData("35")]
        //        [InlineData("37")]
        //        [InlineData("38")]
        //        [InlineData("41")]
        //        [InlineData("42")]
        //        [InlineData("43")]
        //        [InlineData("44")]
        //        [InlineData("45")]
        //        [InlineData("46")]
        //        [InlineData("47")]
        //        [InlineData("48")]
        //        [InlineData("49")]
        //        [InlineData("51")]
        //        [InlineData("53")]
        //        [InlineData("54")]
        //        [InlineData("55")]
        //        [InlineData("61")]
        //        [InlineData("62")]
        //        [InlineData("63")]
        //        [InlineData("64")]
        //        [InlineData("65")]
        //        [InlineData("66")]
        //        [InlineData("67")]
        //        [InlineData("68")]
        //        [InlineData("69")]
        //        [InlineData("71")]
        //        [InlineData("73")]
        //        [InlineData("74")]
        //        [InlineData("75")]
        //        [InlineData("77")]
        //        [InlineData("79")]
        //        [InlineData("81")]
        //        [InlineData("82")]
        //        [InlineData("83")]
        //        [InlineData("84")]
        //        [InlineData("85")]
        //        [InlineData("86")]
        //        [InlineData("87")]
        //        [InlineData("88")]
        //        [InlineData("89")]
        //        [InlineData("91")]
        //        [InlineData("92")]
        //        [InlineData("93")]
        //        [InlineData("94")]
        //        [InlineData("95")]
        //        [InlineData("96")]
        //        [InlineData("97")]
        //        [InlineData("98")]
        //        [InlineData("99")]
        //        public void DddValido_Deve_Retornar_True(string ddd)
        //        {
        //            // Arrange
        //            var numero = $"+55{ddd}961111111";

        //            // Act
        //            var result = Telefone.TryParse(numero, out var actual);

        //            // Assert
        //            result.Should().BeTrue();
        //            actual!.Value.Ddd.Should().Be(ddd);
        //        }
    }
}
