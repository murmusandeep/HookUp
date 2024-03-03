namespace HookUpDAL.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly dob)
        {
            DateTime currentDate = DateTime.Today;
            DateTime birthDate = DateTime.Parse(dob.ToString());

            int age = currentDate.Year - birthDate.Year;

            // Check if the birthday has not occurred yet this year or is today
            if (birthDate > currentDate.AddYears(-age) || birthDate == currentDate)
            {
                age--;
            }

            // Check if the birthdate is on a leap day (February 29)
            if (birthDate.Month == 2 && birthDate.Day == 29 && !DateTime.IsLeapYear(birthDate.Year))
            {
                age--;
            }

            return age;
        }
    }
}
