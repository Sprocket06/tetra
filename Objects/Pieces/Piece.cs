using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Numerics;
using Chroma.Graphics;

namespace tetra.Objects.Pieces
{
    class Piece
    {
        public Grid Grid { get; private set; }
        public Vector2 Position;
        public Chroma.Graphics.Color Color;
        protected Piece(int gridSize)
        {
            Grid = new Grid(new Size(gridSize, gridSize));
        }

        public void RotateCW()
        {
            var newGrid = new Grid(Grid.Size);
            for(var y = 0; y < Grid.Size.Height; y++)
            {
                for(var x = 0; x < Grid.Size.Width; x++)
                {
                    newGrid[(Grid.Size.Width-1) - y ,x] = Grid[x, y];
                }
            }
            Grid = newGrid;
        }

        public void RotateCCW()
        {
            var newGrid = new Grid(Grid.Size);
            for (var y = 0; y < Grid.Size.Height; y++)
            {
                for (var x = 0; x < Grid.Size.Width; x++)
                {
                    newGrid[y, (Grid.Size.Width-1) - x] = Grid[x, y];
                }
            }
            Grid = newGrid;
        }
    }
}
