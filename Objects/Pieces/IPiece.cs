using System;
using System.Collections.Generic;
using System.Text;
using Chroma.Graphics;

namespace tetra.Objects.Pieces
{
    class IPiece : Piece
    {
        public IPiece(): base(4)
        {
            Color = Color.Cyan;
            Grid[0,1] = 1;
            Grid[1,1] = 1;
            Grid[2,1] = 1;
            Grid[3,1] = 1;
        }
    }
}
