using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace suduko
{
    public enum Level
    {
        Easy,
        Medium,
        Hard,
    }

    public enum CheckParams
    {
        Row,
        Column,
        Square
    }
}
