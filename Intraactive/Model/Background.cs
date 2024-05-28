using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intraactive.Model
{
    public class Background : Sprite
    {
        private int _backWidth;
        public Background(Texture2D img, int startingWidth) : base(img, new Vector2(0, 0))
        {
            _backWidth = startingWidth;
        }

        internal bool attemptMove(KeyboardState currState, GameTime gameTime)
        {
            if (currState.IsKeyDown(Keys.Left) || currState.IsKeyDown(Keys.A))
            {
                if (_position.X == 0)
                {
                    return false;
                }

                _position = moveInDirection(1, gameTime);
                return true;
            }

            if (currState.IsKeyDown(Keys.Right) || currState.IsKeyDown(Keys.D))
            {
                if (_position.X <= _backWidth - texture.Width)
                {
                    return false;
                }

                _position = moveInDirection(-1, gameTime);
                return true;
            }

            return false;
        }

        private Vector2 moveInDirection(int dir, GameTime gameTime)
        {
            Vector2 resultOfMove = _position;
            if (dir > 0)
            {
                resultOfMove.X += MOVE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            else
            {
                resultOfMove.X -= MOVE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            resultOfMove.X = Math.Min(resultOfMove.X, 0);
            return resultOfMove;
        }
    }
}
