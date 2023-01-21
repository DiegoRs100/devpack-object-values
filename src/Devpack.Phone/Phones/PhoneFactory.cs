using Devpack.Extensions.Types;

namespace Devpack.ObjectValues.Phones
{
    internal static class PhoneFactory
    {
        public static Phone CreateCellPhoneNumber(string value)
        {
            var onlyDigits = value.GetOnlyDigits().TrimStart('0');

            if (!onlyDigits?.Length.IsBetween(10, 14) != true)
                return new Phone();

            return FormatAsCellPhone(value);
        }

        public static Phone CreateLandLineNumber(string value)
        {
            var onlyDigits = value.GetOnlyDigits().TrimStart('0');

            if (!onlyDigits?.Length.IsBetween(10, 13) != true)
                return new Phone();

            return FormatAsLandline(value);
        }

        private static Phone FormatAsCellPhone(string value)
        {
            var validDigits = value;

            if (value.Length == 10)
                validDigits = $"{value[..^8]}9{value[^8..]}";

            else if (value.Length > 11)
                validDigits = value[^11..];

            var ddd = validDigits[..^8];
            var number = validDigits[^8..];

            return new Phone(ddd, number, PhoneType.CellPhone);
        }

        private static Phone FormatAsLandline(string value)
        {
            var validDigits = value;

            if (value.Length > 10)
                validDigits = value[^10..];

            var ddd = validDigits[..^8];
            var number = validDigits[^8..];

            return new Phone(ddd, number, PhoneType.Landline);
        }
    }
}