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
using AnimalWars.Screens.Maps;

namespace AnimalWars.Screens
{
    class Demen:Entities.UserControlledSprite
    {
        Map spriteManager;
        public Demen(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    float blood, float rateImage, bool live, int level, Map spriteManager, Texture2D imagiBlood)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend,vision, type, isMine, blood, rateImage, live, level, spriteManager, imagiBlood)
        {
        compatibility = new List<Rectangle> {
            new Rectangle(292,39,211,139),
            new Rectangle(656,35,144,209),
            new Rectangle(467,320,90,100)};

            unCompatibility = new List<Rectangle> {
            new Rectangle(371,482,113,118),
            new Rectangle(688,412,112,188),
            new Rectangle(0,0,100,100)};
            frameSize = new Point(100, 100);
            millisecondsPerFrame = 150;
            
         }
        public override void ChangeImageByMoving()
        {
            double angle = this.movingAngle;
            if (currentState == CharacterState.DICHUYEN)
            {
                
                if (angle >= 0 && angle <= 180)
                {
                    // right
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/demen/right_dichuyen");
                }
                else {
                    // left
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/demen/left_dichuyen");
                }
            }

            base.ChangeImageByMoving();
        }

        public override void CheckState()
        {
            double angle  = movingAngle;
            if (currentState == CharacterState.DUNGYEN)
            {
                if (angle >= 0 && angle <= 180)
                {
                    // right
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/demen/right_dungyen");
                }
                else
                {
                    // left
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/demen/left_dungyen");
                }
            }
            else if (currentState == CharacterState.DICHUYEN)
            {
                ChangeImageByMoving();
            }
            else
            {
                if (angle >= 0 && angle <= 180)
                {
                    // right
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/demen/right_dichuyen");
                }
                else
                {
                    // left
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/demen/left_dichuyen");

                }
            }

            base.CheckState();
        }
    }
}
