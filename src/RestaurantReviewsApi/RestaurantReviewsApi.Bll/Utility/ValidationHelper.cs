using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RestaurantReviewsApi.Bll.Utility
{
    public static class ValidationHelper
    {
        public static IList<string> FormatValidations(ValidationResult validations)
        {
            var ret = new List<string>();

            foreach (var validation in validations.Errors)
            {
                ret.Add($"{validation.PropertyName}:{validation.ErrorMessage}");
            }

            return ret;
        }

        public static bool ValidZipCode(string zipCode) => zipCode == null ? false : Regex.IsMatch(zipCode, ValidationConstants.ZipCodeRegex);

        public static bool ValidPhoneNumber(string phoneNumber) => phoneNumber == null ? false : Regex.IsMatch(phoneNumber, ValidationConstants.PhoneRegex);

        public static bool ValidState(string state) => state == null ? false : ValidationConstants.StateAbbreviations.Contains(state);

        public static class ValidationConstants
        {
            public const string PhoneRegex = @"^\d{10,12}$";
            public const string ZipCodeRegex = @"^((\d{5})|(\d{5}-\d{4}))$";

            public static ICollection<string> StateAbbreviations = new HashSet<string>
            {
                "AL",
                "AS",
                "AK",
                "AZ",
                "AR",
                "CA",
                "CO",
                "CT",
                "DC",
                "DE",
                "FL",
                "FM",
                "GA",
                "GU",
                "HI",
                "ID",
                "IL",
                "IN",
                "IA",
                "KS",
                "KY",
                "LA",
                "ME",
                "MH",
                "MD",
                "MA",
                "MI",
                "MN",
                "MS",
                "MO",
                "MP",
                "MT",
                "NE",
                "NV",
                "NH",
                "NJ",
                "NM",
                "NY",
                "NC",
                "ND",
                "OH",
                "OK",
                "OR",
                "PA",
                "PR",
                "RI",
                "SC",
                "SD",
                "TN",
                "TX",
                "UT",
                "VT",
                "VA",
                "VI",
                "WA",
                "WV",
                "WI",
                "WY"
            };
        }
    }
}
