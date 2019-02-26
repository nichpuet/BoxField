using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoxField
{
    class box
    {
        public int x, y, size, r, g, blue;
                 
        public  box(int _x, int _y, int _size, int _r, int _g, int _blue)
        {
            x = _x;
            y = _y;
            size = _size;
            r = _r;
            g = _g;
            blue = _blue;
        }

        public void boxMove(int u)
        {
            y = y + u;
        }

        public void boxMove(string direction, int e)
        {
            if (direction == "left")
            {
                x = x - e;
            }
            else if (direction == "right")
            {
                x = x + e;
            }
        }

        public bool Collision(box b)
        {
            Rectangle rec1 = new Rectangle(b.x, b.y, b.size, b.size);
            Rectangle rec2 = new Rectangle(x, y, size, size);

            if (rec1.IntersectsWith(rec2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
