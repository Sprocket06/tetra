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
    }
}
