using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intraactive.Model
{
    public class Sprite
    {
        protected Texture2D texture;
        protected Vector2 _position;
        protected const float MOVE_SPEED = 200f;

        public Sprite(Texture2D img, Vector2 position)
        {
            texture = img;
            _position = position;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, _position, Color.White);
        }
    }
}
