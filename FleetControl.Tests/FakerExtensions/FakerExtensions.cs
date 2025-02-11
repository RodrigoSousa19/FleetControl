using Bogus;
using Bogus.DataSets;

namespace FleetControl.Tests.FakerExtensions
{
    public static class FakerExtensions
    {
        public static string Cnh(this Person person)
        {
            Random random = new Random();
            int[] numbers = new int[9];

            for (int i = 0; i < 9; i++)
            {
                numbers[i] = random.Next(0, 10);
            }

            int soma1 = 0;
            for (int i = 0, peso = 9; i < 9; i++, peso--)
            {
                soma1 += numbers[i] * peso;
            }
            int dv1 = soma1 % 11;
            dv1 = dv1 >= 10 ? 0 : dv1;

            int soma2 = 0;
            for (int i = 0, peso = 1; i < 9; i++, peso++)
            {
                soma2 += numbers[i] * peso;
            }
            soma2 += dv1 * 9;

            int dv2 = soma2 % 11;
            dv2 = dv2 >= 10 ? 0 : dv2;

            string cnh = string.Concat(numbers.Select(n => n.ToString())) + dv1 + dv2;
            return cnh;
        }

        public static string BrazilLicensePlate(this Vehicle vehicle, bool mercosulFormat = true)
        {
            Random random = new Random();
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string letters = new string(Enumerable.Range(0, 3).Select(_ => alphabet[random.Next(alphabet.Length)]).ToArray());
            string numbers = new string(Enumerable.Range(0, 4).Select(_ => random.Next(0, 10).ToString()[0]).ToArray());

            if (mercosulFormat)
                return $"{letters[0]}{letters[1]}{letters[2]}{numbers[0]}{letters[random.Next(letters.Length)]}{numbers[1]}{numbers[2]}";

            return $"{letters[0]}{letters[1]}{letters[2]}-{numbers[0]}{numbers[1]}{numbers[2]}{numbers[3]}"; ;
        }
    }
}
