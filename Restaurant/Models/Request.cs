using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models
{
    public class Request
    {
        public int CityId { get; set; }
    }
    public class SaveRest
    {
        public int CityId { get; set; }
        public string ResturantName { get; set; }
        public int Address { get; set; }
    }
}