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
        internal static string ContentPath;

        private GameBoard GameField;

        internal GameCore()
        {
            ContentPath = Content.ContentRoot;
            GameField = new GameBoard(new Vector2(20, 20), new Vector2(10, 22));
            GameField.RegisterPiece<TPiece>();
            GameField.RegisterPiece<IPiece>();
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
            GameField.KeyPressed(e);
        }

        protected override void KeyReleased(KeyEventArgs e)
        {
            GameField.KeyReleased(e);
        }

        protected override void Draw(RenderContext context)
        {
            GameField.Draw(context);
            
        }
    }
}
