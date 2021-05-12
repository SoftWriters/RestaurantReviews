using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
  public class RestaurantDTO
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Cuisine { get; set; }
    public string Address { get; set; }
    public long CityId { get; set; }
  }
}
