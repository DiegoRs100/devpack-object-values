using Bogus;
using Devpack.ObjectValues.Phones;
using FluentAssertions;
using Xunit;

namespace Devpack.ObjectValues.Tests.Phones
{
    public class PhoneTests
    {
        [Theory(DisplayName = "Deve retornar falso quando o DDD for inválido.")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("01")]
        [InlineData("10")]
        [InlineData("23")]
        [InlineData("25")]
        [InlineData("26")]
        [InlineData("29")]
        [InlineData("30")]
        [InlineData("36")]
        [InlineData("39")]
        [InlineData("40")]
        [InlineData("50")]
        [InlineData("52")]
        [InlineData("56")]
        [InlineData("57")]
        [InlineData("58")]
        [InlineData("59")]
        [InlineData("60")]
        [InlineData("70")]
        [InlineData("72")]
        [InlineData("76")]
        [InlineData("78")]
        [InlineData("80")]
        [InlineData("90")]
        public void IsValid_BeFalse_WhenDddIsInvalid(string ddd)
        {
            var phone = new Phone(ddd, "961142535", new Faker().Random.Enum<PhoneType>());
            phone.IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Deve retornar verdadeiro quando o DDD for válido.")]
        [InlineData("11")]
        [InlineData("12")]
        [InlineData("13")]
        [InlineData("14")]
        [InlineData("15")]
        [InlineData("16")]
        [InlineData("17")]
        [InlineData("18")]
        [InlineData("19")]
        [InlineData("21")]
        [InlineData("22")]
        [InlineData("24")]
        [InlineData("27")]
        [InlineData("28")]
        [InlineData("31")]
        [InlineData("32")]
        [InlineData("33")]
        [InlineData("34")]
        [InlineData("35")]
        [InlineData("37")]
        [InlineData("38")]
        [InlineData("41")]
        [InlineData("42")]
        [InlineData("43")]
        [InlineData("44")]
        [InlineData("45")]
        [InlineData("46")]
        [InlineData("47")]
        [InlineData("48")]
        [InlineData("49")]
        [InlineData("51")]
        [InlineData("53")]
        [InlineData("54")]
        [InlineData("55")]
        [InlineData("61")]
        [InlineData("62")]
        [InlineData("63")]
        [InlineData("64")]
        [InlineData("65")]
        [InlineData("66")]
        [InlineData("67")]
        [InlineData("68")]
        [InlineData("69")]
        [InlineData("71")]
        [InlineData("73")]
        [InlineData("74")]
        [InlineData("75")]
        [InlineData("77")]
        [InlineData("79")]
        [InlineData("81")]
        [InlineData("82")]
        [InlineData("83")]
        [InlineData("84")]
        [InlineData("85")]
        [InlineData("86")]
        [InlineData("87")]
        [InlineData("88")]
        [InlineData("89")]
        [InlineData("91")]
        [InlineData("92")]
        [InlineData("93")]
        [InlineData("94")]
        [InlineData("95")]
        [InlineData("96")]
        [InlineData("97")]
        [InlineData("98")]
        [InlineData("99")]
        public void IsValid_BeTrue_WhenDddIsValid(string ddd)
        {
            var phone = new Phone(ddd, "961142535", PhoneType.CellPhone);
            phone.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando o número informado representar um celular.")]
        public void IsValid_BeTrue_WhenCellPhone()
        {
            var phone = new Phone("11", "961111111", PhoneType.CellPhone);
            phone.IsValid.Should().BeTrue();
        }

        [Theory(DisplayName = "Deve retornar falso quando o número informado não representar um celular.")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("1111-1111")]
        public void IsValid_BeFalse_WhenCellPhone(string number)
        {
            var phone = new Phone("11", number, PhoneType.CellPhone);
            phone.IsValid.Should().BeFalse();
        }

        [Theory(DisplayName = "Deve retornar verdadeiro quando o número informado representar um telefone fixo.")]
        [InlineData("21111111")]
        [InlineData("31111111")]
        [InlineData("41111111")]
        [InlineData("51111111")]
        public void IsValid_BeTrue_WhenLandline(string number)
        {
            var phone = new Phone("11", number, PhoneType.Landline);
            phone.IsValid.Should().BeTrue();
        }

        [Theory(DisplayName = "Deve retornar falso quando o número informado não representar um telefone fixo.")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("921111111")]
        [InlineData("01111111")]
        [InlineData("11111111")]
        [InlineData("61111111")]
        [InlineData("71111111")]
        [InlineData("81111111")]
        [InlineData("91111111")]
        public void IsValid_BeFalse_WhenLandline(string number)
        {
            var phone = new Phone("11", number, PhoneType.Landline);
            phone.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Deve inicializar corretamente o número e o DDD quando o objeto for instanciado.")]
        public void Constructor()
        {
            var faker = new Faker();

            var ddd = faker.Random.Number(10, 90).ToString();
            var number = faker.Random.String2(8, "0123456789");
            var phoneType = faker.Random.Enum<PhoneType>();

            var phone = new Phone(ddd, number, phoneType);

            phone.Ddd.Should().Be(ddd);
            phone.Number.Should().Be(number);
            phone.PhoneType.Should().Be(phoneType);
        }

        [Theory(DisplayName = "Deve retornar verdadeiro quando os valores de telefone forem iguais.")]
        [InlineData("(11) 9 5111-1111")]
        [InlineData("(11) 5111-1111")]
        public void Compare_BeTrue(string phoneNumber)
        {
            _ = Phone.TryParse(phoneNumber, out var phone);

            (phone!.Value == phoneNumber).Should().BeTrue();
            (phoneNumber == phone!.Value).Should().BeTrue();
        }

        [Theory(DisplayName = "Deve retornar falso quando os valores de telefone forem diferentes.")]
        [InlineData(PhoneType.CellPhone)]
        [InlineData(PhoneType.Landline)]
        public void Compare_BeFalse(PhoneType phoneType)
        {
            var phoneCompare = "11961111111";
            var phone = new Phone("11", "961111112", phoneType);

            (phone == phoneCompare).Should().BeFalse();
            (phoneCompare == phone).Should().BeFalse();
        }

        [Theory(DisplayName = "Deve retornar verdadeiro quando os valores de telefone forem diferentes.")]
        [InlineData(PhoneType.CellPhone)]
        [InlineData(PhoneType.Landline)]
        public void Uncompare_BeTrue(PhoneType phoneType)
        {
            var phoneCompare = "11961111111";
            var phone = new Phone("11", "11111111", phoneType);

            (phone != phoneCompare).Should().BeTrue();
            (phoneCompare != phone).Should().BeTrue();
        }

        [Theory(DisplayName = "Deve retornar falso quando os valores de telefone forem iguais.")]
        [InlineData("(11) 9 5111-1111")]
        [InlineData("(11) 5111-1111")]
        public void Uncompare_BeFalse(string phoneNumber)
        {
            _ = Phone.TryParse(phoneNumber, out var phone);

            (phone!.Value != phoneNumber).Should().BeFalse();
            (phoneNumber != phone!.Value).Should().BeFalse();
        }

        [Theory(DisplayName = "Deve retornar nulo quando o número de celular for inválido.")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("1111-1111")]
        public void TryParse_NumeroInvalido_Deve_Retornar_Nulo(string value)
        {
            var result = Phone.TryParse(value, out var actual);

            result.Should().BeFalse();
            actual.Should().BeNull();
        }

        [Theory(DisplayName = "Deve retornar o ddd e número concatenados quando ambos tiverem valores.")]
        [InlineData("(11) 9 5111-1111", "11951111111")]
        [InlineData("(11) 5111-1111", "1151111111")]
        public void ToString_Valid(string phoneNumber, string expectedPhoneString)
        {
            _ = Phone.TryParse(phoneNumber, out var phone);
            var fullNumber = phone.ToString();

            fullNumber.Should().Be(expectedPhoneString);
        }

        [Fact(DisplayName = "Deve retornar uma string vazia quando os dados do telefone forem nulos ou vazios (ToString).")]
        public void ToString_Invalid()
        {
            var phone = new Phone();
            var formattedPhone = phone.ToString();

            formattedPhone.Should().BeEmpty();
        }

        [Theory(DisplayName = "Deve retornar o telefone formatdo no modelo (00) 00000-0000 quando o número é válido.")]
        [InlineData("11951111111", "(11) 95111-1111")]
        [InlineData("1151111111", "(11) 5111-1111")]
        public void GetFormattedNumber_Valid(string phoneNumber, string expectedFormattedPhone)
        {
            _ = Phone.TryParse(phoneNumber, out var phone);
            var formattedPhone = phone!.Value.GetFormattedNumber();

            formattedPhone.Should().Be(expectedFormattedPhone);
        }

        [Fact(DisplayName = "Deve retornar uma string vazia quando os dados do telefone forem nulos ou vazios (GetFormattedNumber).")]
        public void GetFormattedNumber_Invalid()
        {
            var phone = new Phone();
            var formattedPhone = phone.GetFormattedNumber();

            formattedPhone.Should().BeEmpty();
        }

        [Fact(DisplayName = "Deve retornar um objeto nulo quando o número informado estiver em um formato inválido.")]
        public void TryParse_WhenInvalidFormat()
        {
            var result = Phone.TryParse(string.Empty, out var phone);

            phone.Should().BeNull();
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar um objeto nulo quando o número informado estiver em um formato conhecido " +
            "mas os valores em si não representarem um telefone válido.")]
        public void TryParse_WhenInvalidNumber()
        {
            var result = Phone.TryParse("1022102525", out var phone);

            phone.Should().BeNull();
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Deve retornar telefone celuar quando o número informado for válido.")]
        public void TryParse_UsingCellPhone_Valid()
        {
            var phoneNumber = "(11) 9 4425-4265";

            var result = Phone.TryParse(phoneNumber, out var phone);

            (phone!.Value == phoneNumber).Should().BeTrue();
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar telefone fixo quando o número informado for válido.")]
        public void TryParse_UsingLandline_Valid()
        {
            var phoneNumber = "(11) 5225-4265";

            var result = Phone.TryParse(phoneNumber, out var phone);

            (phone!.Value == phoneNumber).Should().BeTrue();
            result.Should().BeTrue();
        }
    }
}