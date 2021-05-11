using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ORMLayer.TableDefinitions
{
  public class User
  {
    public long Id { get; set; }
    [StringLength(30)]
    public string Username { get; set; }
  }
}
