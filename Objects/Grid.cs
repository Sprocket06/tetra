using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Chroma;
using Chroma.Graphics;

namespace tetra.Objects
{
    class Grid
    {
        private int[] Cells;
        public Vector2 Size { get; private set; }

        public Grid(Vector2 size)
        {
            Size = size;
            Cells = new int[ (int)size.X * (int)size.Y ];

            //initialize grid
            for(var i = 0; i < size.X*size.Y; i++)
            {
                Cells[i] = 0;
            }
        }

        public int this[int x, int y]
        {
            get
            {
                return Cells[(y * (int)Size.X) + x];
            }
            set
            {
                Cells[(y * (int)Size.X) + x] = value;
            }
        }

        public bool InBounds(int x, int y)
            => x >= 0 && x < (int)Size.X && y >= 0 && y < (int)Size.Y;

    }
}
