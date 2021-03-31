using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Snake
{
    public class Game1 : Game
    {
        // Monogame declarations
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        private enum BlockType
        {
            Empty,
            Snake,
            Apple
        }

        // Board
        private const int GridSize = 16;
        private const int BoardWidth = 16;
        private const int BoardHeight = 16;

        // SnakeHead
        private struct SnakeHead
        {
            private int _x;
            public int X
            {
                get { return _x; }
                set
                {
                    if (value < 0) _x = BoardWidth - 1;
                    else if (value > BoardWidth - 1) _x = 0;
                    else _x = value;
                }
            }
            private int _y;
            public int Y
            {
                get { return _y; }
                set
                {
                    if (value < 0) _y = BoardHeight - 1;
                    else if (value > BoardHeight - 1) _y = 0;
                    else _y = value;
                }
            }

            public SnakeHead(int x, int y)
            {
                _x = 0;
                _y = 0;
                X = x;
                Y = y;
            }
        }

        // Vector2Int struct
        private struct Vector2Int
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Vector2Int(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private bool takeInput = true;
        private SnakeHead snakeHead = new SnakeHead(0, 0);
        private Queue<Vector2Int> snake = new Queue<Vector2Int>();
        private Direction snakeDirection = Direction.Down;

        private int frames = 0;
        private int framesPerUpdate = 10;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initialize graphics
            graphics.PreferredBackBufferWidth = GridSize * BoardWidth;
            graphics.PreferredBackBufferHeight = GridSize * BoardHeight;
            graphics.ApplyChanges();

            // Initialize snake
            snake.Enqueue(new Vector2Int(snakeHead.X, snakeHead.Y));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            frames += 1;
            ProcessKeyboardState();
            base.Update(gameTime);

            // Frame update
            if (frames % framesPerUpdate == 0)
            {
                MoveSnake();
                takeInput = true;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);

            // Draw snake
            texture.SetData(new Color[] { Color.Green });
            foreach (Vector2Int position in snake)
            {
                Rectangle snakeRect = new Rectangle(position.X * GridSize, position.Y * GridSize, GridSize, GridSize);
                spriteBatch.Draw(texture, snakeRect, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Processes the current keyboard state
        private void ProcessKeyboardState()
        {
            // Get keyboard state
            KeyboardState keyboardState = Keyboard.GetState();
            // If escape get, exit
            if (keyboardState.IsKeyDown(Keys.Escape)) Exit();
            // Process direction keys
            else if (keyboardState.IsKeyDown(Keys.Up)) InputDirection(Direction.Up);
            else if (keyboardState.IsKeyDown(Keys.Down)) InputDirection(Direction.Down);
            else if (keyboardState.IsKeyDown(Keys.Left)) InputDirection(Direction.Left);
            else if (keyboardState.IsKeyDown(Keys.Right)) InputDirection(Direction.Right);
        }

        // Processes given input direction
        private void InputDirection(Direction direction)
        {
            // Return if not taking input
            if (!takeInput) return;
            takeInput = false;
            // Change snake direction based on input
            if (direction == Direction.Up && snakeDirection != Direction.Down) snakeDirection = Direction.Up;
            else if (direction == Direction.Down && snakeDirection != Direction.Up) snakeDirection = Direction.Down;
            else if (direction == Direction.Left && snakeDirection != Direction.Right) snakeDirection = Direction.Left;
            else if (direction == Direction.Right && snakeDirection != Direction.Left) snakeDirection = Direction.Right;
        }

        // Moves snake in current direction
        private void MoveSnake()
        {
            // Dequeue tail
            snake.Dequeue();
            // Move snake head
            if (snakeDirection == Direction.Up) snakeHead.Y -= 1;
            else if (snakeDirection == Direction.Down) snakeHead.Y += 1;
            else if (snakeDirection == Direction.Left) snakeHead.X -= 1;
            else if (snakeDirection == Direction.Right) snakeHead.X += 1;
            // Enqueue head
            snake.Enqueue(new Vector2Int(snakeHead.X, snakeHead.Y));
        }
    }
}
