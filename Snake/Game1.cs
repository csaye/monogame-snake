using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        private BlockType[,] board = new BlockType[BoardWidth, BoardHeight];

        // Snake
        private struct Snake
        {
            public Snake(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
        }

        Snake snake = new Snake(0, 0);

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            Texture2D snakeTexture = new Texture2D(GraphicsDevice, 1, 1);
            snakeTexture.SetData(new Color[] { Color.Green });
            Rectangle snakeRect = new Rectangle(snake.X * GridSize, snake.Y * GridSize, GridSize, GridSize);
            spriteBatch.Draw(snakeTexture, snakeRect, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
