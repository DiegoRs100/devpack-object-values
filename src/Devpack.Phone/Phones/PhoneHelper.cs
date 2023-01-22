using Devpack.Extensions.Types;
using System.Text.RegularExpressions;

namespace Devpack.ObjectValues.Phones
{
    public static class PhoneHelper
    {
        internal static readonly Regex ValidDddRegex = new(@"^([14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])$");
        internal static readonly char[] LandlineFirstValidDigit = { '2', '3', '4', '5' };

        public static PhoneType GetPhoneType(string number)
        {
            number = number.GetOnlyDigits().TrimStart('0');

            if (number.Length < 10)
                return PhoneType.Invalid;

            var nineDigit = number.Substring(number.Length - 9, 1).First();

            if (number.Length == 10 || nineDigit != '9')
            {
                var eigthDigit = number.Substring(number.Length - 8, 1).First();

                return eigthDigit.In(LandlineFirstValidDigit) 
                    ? PhoneType.Landline 
                    : PhoneType.Invalid;
            }

            return PhoneType.CellPhone;
        }
    }
}