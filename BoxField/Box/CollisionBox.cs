using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxField
{
    class CollisionBox
    {
        public int x, y, size;

        public CollisionBox(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
        }
    }
}
