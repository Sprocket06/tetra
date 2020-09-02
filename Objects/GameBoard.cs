using System;
using System.Numerics;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Chroma;
using Chroma.Graphics;
using tetra.Objects.Pieces;
using System.Security.Cryptography;
using System.Diagnostics;

namespace tetra.Objects
{
    class GameBoard
    {
        public Random RNG;
        public Grid Grid {get; private set;}
        public Vector2 Position;
        public int CellSize;
        public Piece CurrentPiece;
        public List<Type> Pieces;
        public float Gravity { get; private set; }

        public GameBoard(Vector2 position, Size size, int cellSize = 24)
        {
            Grid = new Grid(size);
            Position = position;
            CellSize = cellSize;
            RNG = new Random();
            Pieces = new List<Type>();
            Gravity = (float)(1.0/60.0);
        }

        public void RegisterPiece<T>() where T : Piece
        {
            Pieces.Add(typeof(T));
        }

        public void SpawnPiece()
        {
            CurrentPiece = (Piece)Activator.CreateInstance(Pieces[RNG.Next(Pieces.Count)]);
            CurrentPiece.Position = new Vector2(3, 0);
        }

        public void Draw(RenderContext ctx)
        {
            //Time to draw the grid 
            for (var y = 0; y < Grid.Size.Height; y++)
            {
                for (var x = 0; x < Grid.Size.Width; x++)
                {
                    if (Grid[x, y])
                    {
                        ctx.Rectangle(ShapeMode.Fill, new Rectangle(new Point((int)Position.X + (x * CellSize), (int)Position.Y + (y * CellSize)), new Size(CellSize, CellSize)), Chroma.Graphics.Color.Purple);
                    }
                }
            }

            //TODO: Collision in here somewhere lmao

            //Draw current piece
            if(CurrentPiece != null)
            {
                for(var y = 0; y < CurrentPiece.Grid.Size.Height; y++)
                {
                    for(var x = 0; x < CurrentPiece.Grid.Size.Width; x++)
                    {
                        if (CurrentPiece.Grid[x, y])
                        { 
                            ctx.Rectangle(
                                ShapeMode.Fill,
                                new Vector2(
                                    Position.X + (((int)CurrentPiece.Position.X + x) * CellSize),
                                    Position.Y + (((int)CurrentPiece.Position.Y + y) * CellSize)
                                ),
                                new Size(CellSize, CellSize),
                                CurrentPiece.Color
                            );
                        }
                    }
                }
            }

            // Time to draw the grid lines
            for (var x = 0; x <= Grid.Size.Width; x++)
            {
                ctx.Line(new Vector2(Position.X + x * CellSize, Position.Y), new Vector2(Position.X + x * CellSize, Position.Y + Grid.Size.Height * CellSize), Chroma.Graphics.Color.White);
            }
            for (var y = 0; y <= Grid.Size.Height; y++)
            {
                ctx.Line(new Vector2(Position.X, Position.Y + y * CellSize), new Vector2(Position.X + Grid.Size.Width * CellSize, Position.Y + y * CellSize), Chroma.Graphics.Color.White);
            }
        }

        public void FixedUpdate(float dt)
        {
            //Aight, time for gravity
            var newPos = new Vector2(CurrentPiece.Position.X, CurrentPiece.Position.Y);
            newPos.Y += Gravity;
            Debug.Print("----------");
            Debug.Print(Gravity.ToString());
            Debug.Print(newPos.Y.ToString());

            //Actual collision time
            bool canDrop = true;
            for(var y = 0; y < CurrentPiece.Grid.Size.Height; y++)
            {
                for(var x = 0; x < CurrentPiece.Grid.Size.Width; x++)
                {
                    //if we're not looking at an actually occupied block in the grid, stop, there's nothing to collision-check
                    if (!CurrentPiece.Grid[x, y]) continue;
                    //check below first
                    if(!Grid.InBounds(x + (int)CurrentPiece.Position.X, y + (int)CurrentPiece.Position.Y + 1) || Grid[x + (int)CurrentPiece.Position.X, y + (int)CurrentPiece.Position.Y + 1])
                    {
                        canDrop = false;
                    }
                }
            }

            if (canDrop)
            {
                // we gud, update that shit
                CurrentPiece.Position = newPos;
                Debug.Print(CurrentPiece.Position.Y.ToString());
            }
        }
    }
}
