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
using Chroma.Input.EventArgs;
using Chroma.Input;

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
        public int Gravity { get; private set; }
        public int LockDelay = 30;
        private int GravTimer;
        private int LockTimer;
        private bool canDrop = true;

        public GameBoard(Vector2 position, Size size, int cellSize = 24)
        {
            Grid = new Grid(size);
            Position = position;
            CellSize = cellSize;
            RNG = new Random();
            Pieces = new List<Type>();
            Gravity = 60;
            GravTimer = Gravity;
            LockTimer = LockDelay;
        }

        public void RegisterPiece<T>() where T : Piece
        {
            Pieces.Add(typeof(T));
        }

        public void SpawnPiece()
        {
            CurrentPiece = (Piece)Activator.CreateInstance(Pieces[RNG.Next(Pieces.Count)]);
            CurrentPiece.Position = new Vector2(3, 0);
            LockTimer = LockDelay;
            GravTimer = Gravity;
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
            //Collision is down in FixedUpdate, you're welcome past self I did it.

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

        public void KeyPressed(KeyEventArgs e)
        {
            if(e.KeyCode == KeyCode.X)
            {
                CurrentPiece.RotateCW();
            }
            else if(e.KeyCode == KeyCode.Z)
            {
                CurrentPiece.RotateCCW();
            }
            else if(e.KeyCode == KeyCode.Down)
            {
                Gravity = 3;
            }
        }

        public void KeyReleased(KeyEventArgs e)
        {
            if(e.KeyCode == KeyCode.Down)
            {
                Gravity = 60;
            }
        }

        public void FixedUpdate(float dt)
        {
            GravTimer -= 1;
            if (GravTimer == 0) { 
                //Aight, time for gravity
                var newPos = new Vector2(CurrentPiece.Position.X, CurrentPiece.Position.Y);
                newPos.Y += 1;
                Debug.Print("----------");
                Debug.Print(Gravity.ToString());
                Debug.Print(newPos.Y.ToString());

                //Actual collision time
                canDrop = true;
                for (var y = 0; y < CurrentPiece.Grid.Size.Height; y++)
                {
                    for (var x = 0; x < CurrentPiece.Grid.Size.Width; x++)
                    {
                        //if we're not looking at an actually occupied block in the grid, stop, there's nothing to collision-check
                        if (!CurrentPiece.Grid[x, y]) continue;
                        //check below first
                        if (!Grid.InBounds(x + (int)CurrentPiece.Position.X, y + (int)CurrentPiece.Position.Y + 1) || Grid[x + (int)CurrentPiece.Position.X, y + (int)CurrentPiece.Position.Y + 1])
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
                    GravTimer = Gravity; // can't believe i forgot to reset this too kek
                    LockTimer = LockDelay; // reset lock timer on successful drop
                }
            }
            if (!canDrop)
            {
                LockTimer -= 1;
                if(LockTimer == 0)
                {
                    Lock();
                }
            }
            // now we do left/right movement
            bool canMove = true;
            int deltaX = 0;
            if (Keyboard.IsKeyDown(KeyCode.Left))
            {
                deltaX -= 1;
            }
            if (Keyboard.IsKeyDown(KeyCode.Right))
            {
                deltaX += 1;
            }
            if(deltaX != 0)
            {
                for (var y = 0; y < CurrentPiece.Grid.Size.Height; y++)
                {
                    for (var x = 0; x < CurrentPiece.Grid.Size.Width; x++)
                    {
                        //if we're not looking at an actually occupied block in the grid, stop, there's nothing to collision-check
                        if (!CurrentPiece.Grid[x, y]) continue;
                        //check below first
                        if (!Grid.InBounds(x + (int)CurrentPiece.Position.X, y + (int)CurrentPiece.Position.Y + deltaX) || Grid[x + (int)CurrentPiece.Position.X + deltaX, y + (int)CurrentPiece.Position.Y])
                        {
                            canMove = false;
                        }
                    }
                }
            }
            if (canMove)
            {
                CurrentPiece.Position.X += deltaX;
            }
        }
        
        private void Lock()
        {
            LockTimer = LockDelay;
            //ok, we need to yeet piece into array now for new piece
            for(var y = 0; y < CurrentPiece.Grid.Size.Height; y++)
            {
                for(var x = 0; x < CurrentPiece.Grid.Size.Width; x++)
                {
                    if (!CurrentPiece.Grid[x, y]) continue;
                    Grid[x + (int)CurrentPiece.Position.X, y + (int)CurrentPiece.Position.Y] = CurrentPiece.Grid[x, y];
                }
            }
            // oh boy there's collision to be done here at some point for line clears oh fuck oh boy.

            //ok it is time to spawn new piece now.
            SpawnPiece();
        }
    }
}
