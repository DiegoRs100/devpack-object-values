using Devpack.Extensions.Types;
using Devpack.ObjectValues.Interfaces;
using Devpack.ObjectValues.PhoneExtensions;
using System.Diagnostics.CodeAnalysis;

namespace Devpack.ObjectValues
{
    public readonly struct Phone : IContact
    {
        public string Ddd { get; }
        public string Number { get; }
        public PhoneType PhoneType { get; }

        public bool IsValid => Validate();

        public Phone(string ddd, string number, PhoneType phoneType)
        {
            Ddd = ddd;
            Number = number;
            PhoneType = phoneType;
        }

        public static bool operator ==(Phone a, string b)
        {
            var result = TryParse(b, out var phone);
            return result && phone.ToString() == a.ToString();
        }

        public static bool operator ==(string a, Phone b)
        {
            return b == a;
        }

        public static bool operator !=(Phone a, string b)
        {
            return !(a == b);
        }

        public static bool operator !=(string a, Phone b)
        {
            return b != a;
        }

        public static bool TryParse(string number, out Phone? phone)
        {
            phone = null;

            var phoneType = PhoneHelper.GetPhoneType(number);

            if (phoneType == PhoneType.Invalid)
                return false;

            var phoneAux = phoneType == PhoneType.CellPhone
                ? PhoneFactory.CreateCellPhoneNumber(number)
                : PhoneFactory.CreateLandlineNumber(number);

            if (phoneAux.IsValid)
                phone = phoneAux;

            return phoneAux.IsValid;
        }

        public override string ToString()
        {
            return IsValid ? $"{Ddd}{Number}" : string.Empty;
        }

        public string ToFormattedString()
        {
            return IsValid ? $"({Ddd}) {Number[..^4]}-{Number[^4..]}" : string.Empty;
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private bool Validate()
        {
            var isValid = Ddd != null && PhoneHelper.ValidDddRegex.IsMatch(Ddd);

            if (PhoneType == PhoneType.CellPhone)
                return isValid && Number?.Length == 9 && Number[0] == '9' && Number[1] != '0';

            if (PhoneType == PhoneType.Landline)
                return isValid && Number?.Length == 8 && Number[0].In(PhoneHelper.LandlineFirstValidDigit);

            return false;
        }
    }
}