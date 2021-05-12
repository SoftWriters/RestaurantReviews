using DTOs;
using System;

namespace DTOs.Validators
{
  public class ReviewValidator : IValidator<ReviewDTO>
  {
    public void ValidateData(ReviewDTO poco)
    {
      if(poco.Stars < 0 || poco.Stars > 5)
        throw new ValidationException(String.Format("{0} is an invalid star rating. Range is between 0 and 5", poco.Stars));
      if(poco.ReviewText.Length < 20)
        throw new ValidationException("ReviewText is required to be at least 20 characters in length");
    }
  }
}
