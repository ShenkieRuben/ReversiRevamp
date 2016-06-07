using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiRevamp
{
    struct Tile
    {
        public Rectangle Rectangle { get; set; }
        public int FieldNumber { get; set; }

        public Tile(int fieldNumber, Rectangle rect)
        {
            FieldNumber = fieldNumber;
            Rectangle = rect;
        }

    }
}
