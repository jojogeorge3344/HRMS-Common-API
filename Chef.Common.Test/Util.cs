﻿using System;
using System.Linq;

namespace Chef.Common.Test
{
    public class Util
    {
        private static readonly Random random = new();
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
            Random random = new();
            return (T)values.GetValue(random.Next(values.Length));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="noOfDigits">Max value is 9</param>
        /// <returns></returns>
        public static string GetRandomCode(int noOfDigits)
        {
            if (noOfDigits < 1 || noOfDigits > 9)
            {
                throw new Exception($"{noOfDigits} has to be greater than 0 and less than 9");
            }

            int maxNumber = Convert.ToInt32("999999999".Substring(0, noOfDigits));
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
            return random.Next(100) % 2 == 0;
        }
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static int[] GetChunks(int totalQuantity, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new Exception($"chunkSize {chunkSize} cannot be less than or equal zero");
            }

            int quotient = totalQuantity / chunkSize;
            int remainder = totalQuantity - (quotient * chunkSize);
            int[] chunks = new int[chunkSize];
            for (int i = 0; i < chunks.Length; i++)
            {
                chunks[i] = (i == 0) ? quotient + remainder : quotient;
            }

            return chunks;
        }
    }
}
