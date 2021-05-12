using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ORMLayer.TableDefinitions
{
  public class Review
  {
    public long Id { get; set; }
    public int Stars { get; set; }
    public string ReviewText { get; set; }

    [ForeignKey("Restaurant")]
    public long RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }

    [ForeignKey("User")]
    public long UserId { get; set; }
    public User User { get; set; }
  }
}
