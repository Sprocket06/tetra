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
            Grid[1,0] = true;
            Grid[0,1] = true;
            Grid[1,1] = true;
            Grid[2,1] = true;
            Color = Color.Purple;
        }
    }
}
