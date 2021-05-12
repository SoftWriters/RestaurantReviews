using System;
using System.ComponentModel.DataAnnotations;

namespace ORMLayer.TableDefinitions
{
  public class City
  {
    public long Id { get; set; }
    [StringLength(30)]
    public string Name { get; set; }
  }
}
