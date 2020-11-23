using System;
using System.Linq;

namespace Chef.Common.Test
{
    public class Util
    {
        private static Random random = new Random();
        public static string GetUniqueRandomString()
        {

            string guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            return guidString;
        }

        public static T GetRandomEnum<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            Random random = new Random();
            return (T)values.GetValue(random.Next(values.Length));
        }
        public static string GetRandomCode(int noOfDigits)
        {
            if (noOfDigits < 1 || noOfDigits > 9)
                throw new Exception($"{noOfDigits} has to be greater than 0 and less than 9");
            var maxNumber = Convert.ToInt32("999999999".Substring(0, noOfDigits));
            return string.Format($"{{0:D{noOfDigits}}}", new Random().Next(1, maxNumber));
        }

        public static string GenerateValidPhoneNumber()
        {
            return "111-111-1111";
        }
        public static string GenerateValidZipCode()
        {
            return "0000001";
        }
        public static string GenerateValidEmail()
        {
            return "test@test.com";
        }
        public static string GetRandomName()
        {
            return "John";
        }
        public static bool RandomBoolean()
        {
            return new Random().Next(100) % 2 == 0;
            return random.Next(100) % 2 == 0;
        }
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
