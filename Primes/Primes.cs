using System.Collections.Generic;
using System.Linq;

namespace Primes
{
    public static class Primes
    {
        public static List<int> GetPrimeNumbers(int lowerBound, int upperBound)
        {
            List<int> primeNumbers = new List<int>();

            if (upperBound - lowerBound <= 0)
            {
                return primeNumbers;
            }

            primeNumbers = Enumerable.Range(lowerBound, ((upperBound - lowerBound) + 1)).Where(x => IsPrime(x)).ToList();
            return primeNumbers;
        }

        public static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }
            return !Enumerable.Range(2, number).Any(x => x != number && number % x == 0);
        }
    }
}
