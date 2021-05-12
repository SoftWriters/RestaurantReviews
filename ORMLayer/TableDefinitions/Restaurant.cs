using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ORMLayer.TableDefinitions
{
  public class Restaurant
  {
    public long Id { get; set; }
    [StringLength(30)]
    public string Name { get; set; }
    [StringLength(30)]
    public string Cuisine { get; set; }

    [StringLength(30)]
    public string Address { get; set; }
    [ForeignKey("City")]
    public long CityId { get; set; }
    public City City { get; set; }
  }
}
