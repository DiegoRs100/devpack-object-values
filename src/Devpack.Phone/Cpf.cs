using Devpack.Extensions.Types;
using System.Diagnostics.CodeAnalysis;

namespace Devpack.ObjectValues
{
    public readonly struct Cpf
    {
        public string Number { get; }

        public bool IsValid => Validate();

        public Cpf(string cpf)
        {
            Number = cpf.GetOnlyDigits();
        }

        public static bool operator ==(Cpf a, string b)
        {
            var result = TryParse(b, out var cpf);
            return result && cpf!.Value.Number == a.Number;
        }

        public static bool operator ==(string a, Cpf b)
        {
            return b == a;
        }

        public static bool operator !=(Cpf a, string b)
        {
            return !(a == b);
        }

        public static bool operator !=(string a, Cpf b)
        {
            return b != a;
        }

        public static bool TryParse(string number, out Cpf? cpf)
        {
            cpf = null;

            var cpfAux = new Cpf(number);

            if (cpfAux.IsValid)
                cpf = cpfAux;

            return cpfAux.IsValid;
        }

        public override string ToString()
        {
            return Number;
        }

        public string ToFormattedString()
        {
            return IsValid 
                ? Convert.ToUInt64(Number).ToString(@"000\.000\.000\-00") 
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
            if (!Number.HasOnlyDigits() || Number?.Length != 11)
                return false;

            var number = Number[..9];

            var firstDigit = CalculateVerifierDigit(number);
            var secondDigit = CalculateVerifierDigit($"{number}{firstDigit}");

            return Number == $"{number}{firstDigit}{secondDigit}";
        }

        private static int CalculateVerifierDigit(string number)
        {
            var multiplier = number.Length + 1;
            var factor = new List<int>();

            foreach (var digit in number.Select(d => (int)char.GetNumericValue(d)))
                factor.Add(digit * (multiplier - factor.Count));

            var factorModule = factor.Sum() % 11;
            var verifierDigit = factorModule < 2 ? 0 : 11 - factorModule;

            return verifierDigit;
        }
    }
}