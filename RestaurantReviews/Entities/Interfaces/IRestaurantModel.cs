using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{ 
    public interface IRestaurantModel
    {
        string Address
        { get; set; }

        IChainModel Chain
        { get; set; }

        ICityModel City
        { get; set; }

        int Id
        { get; set; }

        string Name
        { get; set; }

        // SO MANY FIELDS:  type of food (Italian, American, etc), menu or menu link, phone number, website, hours open, all the address fields, location for GPS mapping, type of food, number of tables, cost, user photos, etc.
    }
}
