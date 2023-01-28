using Devpack.Extensions.Types;
using System.Diagnostics.CodeAnalysis;

namespace Devpack.ObjectValues
{
    public readonly struct Cnpj
    {
        public string Number { get; }

        public bool IsValid => Validate();

        public Cnpj(string cenpj)
        {
            Number = cenpj.GetOnlyDigits();
        }

        public static bool operator ==(Cnpj a, string b)
        {
            var result = TryParse(b, out var cnpj);
            return result && cnpj!.Value.Number == a.Number;
        }

        public static bool operator ==(string a, Cnpj b)
        {
            return b == a;
        }

        public static bool operator !=(Cnpj a, string b)
        {
            return !(a == b);
        }

        public static bool operator !=(string a, Cnpj b)
        {
            return b != a;
        }

        public static bool TryParse(string number, out Cnpj? cnpj)
        {
            cnpj = null;

            var cnpjAux = new Cnpj(number);

            if (cnpjAux.IsValid)
                cnpj = cnpjAux;

            return cnpjAux.IsValid;
        }

        public override string ToString()
        {
            return Number;
        }

        public string ToFormattedString()
        {
            return IsValid
                ? Convert.ToUInt64(Number).ToString(@"00\.000\.000\/0000\-00")
                : string.Empty;
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
            if (!Number.HasOnlyDigits() || Number?.Length != 14)
                return false;

            var number = Number[..12];

            var firstDigit = CalculateVerifierDigit(number);
            var secondDigit = CalculateVerifierDigit($"{number}{firstDigit}");

            return Number == $"{number}{firstDigit}{secondDigit}";
        }

        private static int CalculateVerifierDigit(string number)
        {
            var multiplier = 2;
            var factor = new List<int>();

            foreach (var digit in number.Reverse().Select(d => (int)char.GetNumericValue(d)))
            {
                factor.Add(digit * multiplier);
                multiplier = multiplier == 9 ? 2 : multiplier + 1;
            }

            var factorModule = factor.Sum() % 11;
            var verifierDigit = factorModule < 2 ? 0 : 11 - factorModule;

            return verifierDigit;
        }
    }
}