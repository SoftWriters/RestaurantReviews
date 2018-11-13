using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface ICityModel
    {
        int Id
        { get; set; }

        string Name
        { get; set; }

        // *slaps roof* This baby can hold so many fields.  State.  OK, maybe not so many.  I took more effort to type this comment than to actually add the State field, but I'm commited now!  Or maybe I should be committed.
    }
}
