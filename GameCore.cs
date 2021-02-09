using System.Numerics;
using Chroma;
using Chroma.Graphics;
using Chroma.Input;
using tetra.Objects;
using tetra.Objects.Pieces;

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
            FixedTimeStepTarget = 60;
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
