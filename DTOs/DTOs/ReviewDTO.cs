using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
  public class ReviewDTO
  {
    public long Id { get; set; }
    public int Stars { get; set; }
    public string ReviewText { get; set; }
    public long RestaurantId { get; set; }
    public long UserId { get; set; }
  }
}
