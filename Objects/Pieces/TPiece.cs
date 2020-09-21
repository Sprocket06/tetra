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
            Grid[1,0] = (int)PieceColors.Purple;
            Grid[0,1] = (int)PieceColors.Purple;
            Grid[1,1] = (int)PieceColors.Purple;
            Grid[2,1] = (int)PieceColors.Purple;
            Color = Color.Purple;
        }
    }
}
