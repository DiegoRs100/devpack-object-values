using Devpack.Extensions.Types;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Devpack.ObjectValues.Tests")]
namespace Devpack.ObjectValues.PhoneExtensions
{
    internal static class PhoneFactory
    {
        public static Phone CreateCellPhoneNumber(string value)
        {
            var onlyDigits = value.GetOnlyDigits().TrimStart('0');

            if (onlyDigits?.Length.IsBetween(11, 14) != true)
                return new Phone();

            return FormatAsCellPhone(onlyDigits);
        }

        public static Phone CreateLandlineNumber(string value)
        {
            var onlyDigits = value.GetOnlyDigits().TrimStart('0');

            if (onlyDigits?.Length.IsBetween(10, 13) != true)
                return new Phone();

            return FormatAsLandline(onlyDigits!);
        }

        private static Phone FormatAsCellPhone(string value)
        {
            var validDigits = value;

            if (value.Length > 11)
                validDigits = value[^11..];

            var ddd = validDigits[..^9];
            var number = validDigits[^9..];

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