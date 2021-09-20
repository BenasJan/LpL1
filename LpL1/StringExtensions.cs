namespace LpL1
{
    public static class StringExtensions
    {
        public static bool EndsWithOddNumber(this string stringToCheck)
        {
            return stringToCheck.EndsWith('1') ||
                   stringToCheck.EndsWith('3') ||
                   stringToCheck.EndsWith('5') ||
                   stringToCheck.EndsWith('7') ||
                   stringToCheck.EndsWith('9');
        }
    }
}