using Intraactive.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;

namespace Intraactive
{
    public class Game1 : Game
    {
        protected GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player _mainPlayer;
        private Background _bg;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true; 
        }

        protected override void Initialize()
        {
            _mainPlayer = new Player(Content.Load<Texture2D>("img/walking"), 
                                    new Vector2(_graphics.PreferredBackBufferWidth / 3, 
                                                _graphics.PreferredBackBufferHeight / 2));

            _bg = new Background(Content.Load<Texture2D>("img/Background"), _graphics.PreferredBackBufferWidth);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            bool didMove = handleMovement(gameTime);

            //Debug.WriteLine(didMove);
            base.Update(gameTime);
        }

        /// <summary>
        /// This method handles character movement
        /// </summary>
        /// <param name="gameTime">Reference to the GameTime object this instance is using</param>
        protected bool handleMovement(GameTime gameTime)
        {
            KeyboardState currState = Keyboard.GetState();

            bool didMove = false;
            if (_mainPlayer.isCentered())
            {
                didMove = _bg.attemptMove(currState, gameTime);
            }

            if (!didMove)
            {
                didMove = _mainPlayer.attemptMove(currState, gameTime);
            }

            _mainPlayer.updateAnimation();

            return didMove;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //_bg.Draw(_spriteBatch);
            _mainPlayer.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
