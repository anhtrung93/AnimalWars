using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AnimalWars
{
    public class Statics
    {

        public static int GAME_WIDTH = 600;
        public static int GAME_HEIGHT = 800;
        public static string TITLE = "Adventure And Rescue";

        public static GameTime GAMETIME;
        public static SpriteBatch SPRITEBATCH;
        public static ContentManager CONTENT;
        public static GraphicsDevice GRAPHICSDEVICE;
        public static Managers.InputManager INPUT;

        // fonts
        public static SpriteFont BLOOD_INDEX_FONT;
        public static SpriteFont FONT;

        // for test
        public static Texture2D PIXEL;

        // for mapListScreen
        
    }
}
