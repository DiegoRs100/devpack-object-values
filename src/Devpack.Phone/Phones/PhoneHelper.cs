using Devpack.Extensions.Types;
using System.Text.RegularExpressions;

namespace Devpack.ObjectValues.Phones
{
    public static class PhoneHelper
    {
        internal static readonly Regex ValidDddRegex = new(@"^([14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])$");
        internal static readonly char[] CellPhoneFirstValidDigit = { '6', '7', '8', '9' };
        internal static readonly char[] LandlineFirstValidDigit = { '2', '3', '4', '5' };

        public static PhoneType GetPhoneType(string number)
        {
            number = number.GetOnlyDigits().TrimStart('0');

            if (number.Length < 8)
                return PhoneType.Invalid;

            number = number.Substring(number.Length - 8, 1);
            var firstDigit = int.Parse(number);

            if (firstDigit <= 1)
                return PhoneType.Invalid;

            if (firstDigit <= 5)
                return PhoneType.Landline;

            return PhoneType.CellPhone;
        }
    }
}