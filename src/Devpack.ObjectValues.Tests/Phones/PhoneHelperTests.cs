using Devpack.ObjectValues.Phones;
using FluentAssertions;
using Xunit;

namespace Devpack.ObjectValues.Tests.Phones
{
    public class PhoneHelperTests
    {
        [Theory(DisplayName = "Deve retornar formato inválido de telefone " +
            "quando um número com menos de 8 caracteres for passada.")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("7654321")]
        [InlineData("(7)65")]
        public void GetPhoneType_WhenNotMinLenght(string number)
        {
            PhoneHelper.GetPhoneType(number).Should().Be(PhoneType.Invalid);
        }

        // Considera-se aqui o primeiro dígito do número de telefone sem considerar o DDD, logo, o 8° dígito de trás para frente.
        [Theory(DisplayName = "Deve retornar formato inválido de telefone " +
            "quando o número informado não tiver o primeiro dígito de telefone válido.")]
        [InlineData("(11) 05251240")]
        [InlineData("1110256620")]
        public void GetPhoneType_WhenFirstDigitInvalid(string number)
        {
            PhoneHelper.GetPhoneType(number).Should().Be(PhoneType.Invalid);
        }

        [Theory(DisplayName = "Deve retornar (CellPhone) quando o número informado for um número de calular.")]
        [InlineData("(11) 98525-1240")]
        [InlineData("11965251240")]
        [InlineData("+55 11 970256620")]
        [InlineData("+5 11980256620")]
        public void GetPhoneType_WhenCellPhone(string number)
        {
            PhoneHelper.GetPhoneType(number).Should().Be(PhoneType.CellPhone);
        }

        [Theory(DisplayName = "Deve retornar (Landline) quando o número informado for um número de telefone fixo.")]
        [InlineData("(11) 2525-1240")]
        [InlineData("011 3625-1240")]
        [InlineData("+55 11 4025-6620")]
        [InlineData("+5 11 55256620")]
        public void GetPhoneType_WhenLandline(string number)
        {
            PhoneHelper.GetPhoneType(number).Should().Be(PhoneType.Landline);
        }
    }
}