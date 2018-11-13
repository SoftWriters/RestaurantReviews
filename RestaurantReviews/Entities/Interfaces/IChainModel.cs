using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface IChainModel
    {
        int Id
        { get; set; }

        string Name
        { get; set; }

        // FIELDS FIELD GET YOUR FIELDS HERE: type of food (Italian, American, etc), website, etc.
    }
}
