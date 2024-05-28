using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Image at https://www.kindpng.com/imgv/iTixoii_walking-sprite-sheet-png-walking-man-sprite-sheet/
namespace Intraactive.Model
{
    enum PlayerState : int
    {
        STANDING = 0,
        WALKING = 1
    }

    class Player : Sprite
    {
        public Vector2 Position
        {
            get => _position;

            set
            {
                _position = value;
            }
        }

        private Vector2 _startPosition;
        private int _upperXBound;
        private int _animationState;
        private int _animationCounter;

        public PlayerState State { get; set; }

        public Player(Texture2D texture, Vector2 startingPos) : base(texture, startingPos)
        {
            _startPosition = startingPos;
            _upperXBound = (int)((startingPos.X * 3) - texture.Width);
            _animationCounter = 0;
        }

        internal bool attemptMove(KeyboardState currState, GameTime gameTime)
        {
            State = PlayerState.STANDING;
            Vector2 resultOfMove = Position;
            bool didMove = false;

            if (currState.IsKeyDown(Keys.Right) || currState.IsKeyDown(Keys.D))
            {
                resultOfMove.X += MOVE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
                didMove = true;
            }

            else if (currState.IsKeyDown(Keys.Left) || currState.IsKeyDown(Keys.A))
            {
                resultOfMove.X -= MOVE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
                didMove = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                resultOfMove.Y -= MOVE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
                didMove = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                resultOfMove.Y += MOVE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
                didMove = true;
            }

            if (!didMove)
            {
                return false;
            }

            if (resultOfMove.X < 0)
            {
                resultOfMove.X = 0;
                return false;
            }

            else if (resultOfMove.X > _upperXBound)
            {
                resultOfMove.X = _upperXBound;
                return false;
            }

            State = PlayerState.WALKING;
            _position = resultOfMove;

            return true;
        }

        public new void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, _position, new Rectangle(_animationState*100, 0, 100, 100), Color.White, 0f, new Vector2(0, 0), new Vector2(0.5f, 0.5f), SpriteEffects.None, 0f);
            //(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        }

        internal bool isCentered()
        {
            return _startPosition.X == (_position.X);
        }

        internal void updateAnimation()
        {
            if (State == PlayerState.STANDING)
            {
                return;
            }

            _animationCounter--;
            if (_animationCounter <= 0)
            {
                _animationState++;
                if (_animationState == 5)
                {
                    _animationState = 0;
                }

                _animationCounter = 10;
            }
           
        }
    }
}
