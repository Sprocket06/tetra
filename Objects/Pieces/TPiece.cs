using System;
using System.Collections.Generic;
using System.Text;
using Chroma.Graphics;

namespace tetra.Objects.Pieces
{
    class TPiece : Piece
    {
        public TPiece() : base(3)
        {
            Grid[1,0] = 3;
            Grid[0,1] = 3;
            Grid[1,1] = 3;
            Grid[2,1] = 3;
            Color = Color.Purple;
        }
    }
}
