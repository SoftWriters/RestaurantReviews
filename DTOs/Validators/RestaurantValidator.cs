using System;

namespace DTOs.Validators
{
  public class RestaurantValidator : IValidator<RestaurantDTO>
  {
    public void ValidateData(RestaurantDTO poco)
    {
      if(string.IsNullOrWhiteSpace(poco.Name))
        throw new ValidationException(String.Format("Name field is required for a Restaurant"));
      if(string.IsNullOrWhiteSpace(poco.Address))
        throw new ValidationException("Address field is required for a Restaurant");
    }
  }
}
