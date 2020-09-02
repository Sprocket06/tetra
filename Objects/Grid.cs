using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using Chroma;
using Chroma.Graphics;

namespace tetra.Objects
{
    class Grid
    {
        private bool[] Cells;
        public Size Size { get; private set; }

        public Grid(Size size)
        {
            Size = size;
            Cells = new bool[size.Width * size.Height];

            //initialize grid
            for(var i = 0; i < size.Width*size.Height; i++)
            {
                Cells[i] = false;
            }
        }

        public bool this[int x, int y]
        {
            get
            {
                return Cells[(y * Size.Width) + x];
            }
            set
            {
                Cells[(y * Size.Width) + x] = value;
            }
        }

        public bool InBounds(int x, int y)
            => x >= 0 && x < Size.Width && y >= 0 && y < Size.Height;

    }
}
