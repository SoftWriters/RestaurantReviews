using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviewsApi.UnitTests
{
    public class HelperFunctions
    {
        private static readonly Random random = new Random();
        private const string CharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+☀☁☂☃☄";
        private const string BasicCharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const string NumericCharSet = "0123456789";

        public static string RandomString(int length = 15, string charSet = CharSet)
        {
            var stringChars = new char[length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = charSet[random.Next(charSet.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

        public static string RandomEmail()
        {
            return $"{RandomString(10, BasicCharSet)}@{RandomString(10, BasicCharSet)}.com";
        }

        public static string RandomPhone()
        {
            return RandomString(10, NumericCharSet);
        }

        public static int RandomNumber(int? max = null)
        {
            return max == null ? random.Next() : random.Next(1,max.Value);
        }

        public static bool RandomBoolean()
        {
            return random.Next(5) / 2 == 0;
        }

        public static T RandomEnum<T>(params T[] validEnums) => validEnums[random.Next(validEnums.Length)];

        public static T RandomEnum<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            T randomEnum = (T)values.GetValue(random.Next(values.Length));
            return randomEnum;
        }

        public static T RandomElement<T>(ICollection<T> collection)
        {
            return collection.ToList()[random.Next(collection.Count)];
        }

        public static DateTime RandomDateTime()
        {
            var start = new DateTime(1995, 1, 1);
            var range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}