using RestaurantReviewsApi.Bll.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RestaurantReviewsApi.UnitTests.UtilityTests
{
    public class ValidationHelperTests
    {
        public static IEnumerable<object[]> ValidStates =>
            new List<object[]>
            {
                new object[] { "OR" },
                new object[] { "PA" },
                new object[] { "CA" },
                new object[] { "MD" },
                new object[] { "MN" },
                new object[] { "FL" },
                new object[] { "CO" },
                new object[] { "TX" },
                new object[] { "NY" },
                new object[] { "CT" },
                new object[] { "OH" }
            };

        public static IEnumerable<object[]> InvalidStates =>
            new List<object[]>
            {
                new object[] { " " },
                new object[] { "XX" },
                new object[] { "HG" },
                new object[] { "mn" },
                new object[] { "looooong" },
                new object[] { "RE" },
                new object[] { "re" },
                new object[] { "23" },
                new object[] { "P" },
                new object[] { "H6" }
            };

        public static IEnumerable<object[]> NullAndEmpty =>
            new List<object[]>
            {
                new object[] { "" },
                new object[] { null }
            };

        public static IEnumerable<object[]> ValidZipCodes =>
         new List<object[]>
         {
            new object[] { "15317" },
            new object[] { "15051" },
            new object[] { "55555" },
            new object[] { "11111" },
            new object[] { "15108" },
            new object[] { "15057" },
            new object[] { "15057-1565" },
            new object[] { "15108-9855" }
         };

        public static IEnumerable<object[]> InvalidZipCodes =>
            new List<object[]>
            {
                new object[] { "151089855" },
                new object[] { "aaaaa" },
                new object[] { "34sdf" },
                new object[] { "123453" },
                new object[] { "150578" },
                new object[] { "15-108" }
            };

        public static IEnumerable<object[]> ValidPhoneNumbers =>
            new List<object[]>
            {
                new object[] { "4129992242" },
                new object[] { "14129225223" },
                new object[] { "18009992524" },
                new object[] { "12312312345" }
            };

        public static IEnumerable<object[]> InvalidPhoneNumbers =>
            new List<object[]>
            {
                new object[] { "1241" },
                new object[] { "aaaaa" },
                new object[] { "34sdf" },
                new object[] { "1124224124124214" },
                new object[] { "asdasdasdadssadadsasdasd" },
                new object[] { "15-108" }
            };

        [Theory]
        [MemberData(nameof(ValidStates))]
        public void ValidStatesValid(string state)
        {
            var valid = ValidationHelper.ValidState(state);
            Assert.True(valid, "Valid state failed on: " + state);
        }

        [Theory]
        [MemberData(nameof(InvalidStates))]
        [MemberData(nameof(NullAndEmpty))]
        public void InvalidStatesInvalid(string state)
        {
            var valid = ValidationHelper.ValidState(state);
            Assert.False(valid, "Invalid state failed on: " + state);
        }

        [Theory]
        [MemberData(nameof(ValidZipCodes))]
        public void ValidZipCodesValid(string zipCode)
        {
            var valid = ValidationHelper.ValidZipCode(zipCode);
            Assert.True(valid, "Valid zip code failed on: " + zipCode);
        }

        [Theory]
        [MemberData(nameof(InvalidZipCodes))]
        [MemberData(nameof(NullAndEmpty))]
        public void InValidZipCodesValid(string zipCode)
        {
            var valid = ValidationHelper.ValidZipCode(zipCode);
            Assert.False(valid, "Invalid zip code failed on: " + zipCode);
        }

        [Theory]
        [MemberData(nameof(ValidPhoneNumbers))]
        public void ValidPhoneNumbersValid(string phoneNumber)
        {
            var valid = ValidationHelper.ValidPhoneNumber(phoneNumber);
            Assert.True(valid, "Valid phone number failed on: " + phoneNumber);
        }

        [Theory]
        [MemberData(nameof(InvalidPhoneNumbers))]
        [MemberData(nameof(NullAndEmpty))]
        public void InvalidPhoneNumbersInvalid(string phoneNumber)
        {
            var valid = ValidationHelper.ValidPhoneNumber(phoneNumber);
            Assert.False(valid, "Valid phone number failed on: " + phoneNumber);
        }
    }
}
