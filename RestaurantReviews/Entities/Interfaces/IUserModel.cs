using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface IUserModel
    {
        int Id
        { get; set; }
        
        string Name
        { get; set; }

        bool IsAdmin
        { get; }

        // He who controls the fields controls the universe:  Address (including a CityModel), phone, status (i.e. 'gold reviewer' for trusted reviewers'), date joined, photos, etc.
    }
}
