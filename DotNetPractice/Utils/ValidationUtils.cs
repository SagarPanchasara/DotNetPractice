using System.Text.RegularExpressions;

namespace DotNetPractice.Utils
{
    public partial class ValidationUtils
    {
        [GeneratedRegex(@"^\d{13}$")]
        private static partial Regex MyRegex();

        public static bool IsValidIdNumber(string idNumber)
        {
            // Check if ID number matches the format (13 digits)
            if (!MyRegex().IsMatch(idNumber))
                return false;

            // Extract date of birth from ID number
            int year = int.Parse(idNumber.Substring(0, 2));
            int month = int.Parse(idNumber.Substring(2, 2));
            int day = int.Parse(idNumber.Substring(4, 2));
            int gender = int.Parse(idNumber.Substring(6, 1));

            // Validate date of birth
            if (!IsValidDateOfBirth(year, month, day))
                return false;

            // Validate gender
            if (gender < 0 || gender > 4)
                return false;

            // Validate citizenship (7th digit must be 0 or 1)
            int citizenship = int.Parse(idNumber.Substring(10, 1));
            if (citizenship != 0 && citizenship != 1)
                return false;

            // Perform additional checks (optional)

            // If all checks pass, ID number is valid
            return true;
        }

        static bool IsValidDateOfBirth(int year, int month, int day)
        {
            try
            {
                DateTime dob = new(year, month, day);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }
    }
}
