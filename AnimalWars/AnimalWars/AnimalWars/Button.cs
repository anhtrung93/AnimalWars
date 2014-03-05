using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimalWars
{
    class Button
    {
        public String name;
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 position;
        public float rate;
        public bool isEnable = false;

        public Button(String s, Texture2D t, Vector2 v, bool b)
        {
            name = s;
            texture = t;
            position = v;
            rectangle = new Rectangle(0,0, texture.Width, texture.Height);
            isEnable = b;
            rate = 1;
        }
        public Button(String s, Texture2D t, Vector2 v,Rectangle r, bool b)
        {
            name = s;
            texture = t;
            position = v;
            rectangle = r;
            isEnable = b;
            rate = 1;
        }
        public Button(String s, Texture2D t, Vector2 v, Rectangle r, bool b,float ra)
        {
            name = s;
            texture = t;
            position = v;
            rectangle = r;
            isEnable = b;
            rate = ra;
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)(rate*rectangle.Width),(int) (rate*rectangle.Height));
        }
        public void DrawButton(SpriteBatch sp,Color c)
        {
            sp.Draw(texture, position, rectangle,c,0,Vector2.Zero,rate,SpriteEffects.None,0);
                    
        }

    }
}
