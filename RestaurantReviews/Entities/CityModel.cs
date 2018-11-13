using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class CityModel : ICityModel
    {
        public CityModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}