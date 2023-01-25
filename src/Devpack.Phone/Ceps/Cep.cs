using Devpack.Extensions.Types;
using Devpack.ObjectValues.Emails;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.RegularExpressions;

namespace Devpack.ObjectValues.Ceps
{
    public readonly struct Cep
    {
        public string Number { get; }

        public bool IsValid => Validate();

        public Cep(string cep)
        {
            Number= cep;
        }

        public static bool operator ==(Cep a, string b)
        {
            var result = TryParse(b, out var cep);
            return result && cep!.Value.Number == a.Number;
        }

        public static bool operator ==(string a, Cep b)
        {
            return b == a;
        }

        public static bool operator !=(Cep a, string b)
        {
            return !(a == b);
        }

        public static bool operator !=(string a, Cep b)
        {
            return b != a;
        }

        public static bool TryParse(string number, out Cep? cep)
        {
            cep = null;

            var cepAux = new Cep(number);

            if (cepAux.IsValid)
                cep = cepAux;

            return cepAux.IsValid;
        }

        public override string ToString()
        {
            return Number;
        }

        public string ToFormattedString()
        {
            return IsValid ? $"{Number[..^3]}-{Number[^5..]}" : string.Empty;
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
            if (Number.IsNullOrEmpty())
                return false;

            var regex = new Regex("^[0-9]{8}$");
            return regex.IsMatch(Number);
        }
    }
}