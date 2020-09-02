using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Drawing;
using Chroma;
using Chroma.Graphics;
using Chroma.Input;
using Chroma.Input.EventArgs;
using Chroma.Windowing;
using tetra.Objects;
using tetra.Objects.Pieces;
using Chroma.Diagnostics;
using Chroma.Diagnostics.Logging;
using System.Diagnostics;

namespace tetra
{
    class GameCore : Game
    {
        private GameBoard GameField;

        internal GameCore()
        {
            GameField = new GameBoard(new Vector2(20, 20), new Size(10, 22));
            GameField.RegisterPiece<TPiece>();
            GameField.SpawnPiece();
            FixedUpdateFrequency = 60;
            Window.Title = "Tetra";
        }

        protected override void Update(float delta)
        {
            
        }

        protected override void FixedUpdate(float fixedDelta)
        {
            GameField.FixedUpdate(fixedDelta);
        }

        protected override void KeyPressed(KeyEventArgs e)
        {
            if(e.KeyCode == KeyCode.Escape)
            {
                Quit();
            }
        }

        protected override void Draw(RenderContext context)
        {
            GameField.Draw(context);
            
        }
    }
}
