using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AnimalWars
{
    class Lion:UserControlledSprite
    {
        // SpriteManager spriteManaer;
        public Lion(Texture2D image, int timeSinceLastFrame, Point currentFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    float blood, float rateImage, bool live, int level, SpriteManager spriteManager, Texture2D imagiBlood)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend,vision, type, isMine, blood, rateImage, live, level, spriteManager, imagiBlood)
        {
        compatibility = new List<Rectangle> {
        new Rectangle(292,39,211,139),
        new Rectangle(656,35,144,209),
        new Rectangle(467,320,90,100),
        new Rectangle(0,0,100,100)};

        unCompatibility = new List<Rectangle> {
        new Rectangle(371,482,113,118),
        new Rectangle(688,412,112,188),};
         }

         
       
    }
}
