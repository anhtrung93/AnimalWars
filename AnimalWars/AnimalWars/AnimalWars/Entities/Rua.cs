using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using AnimalWars.Screens;
using AnimalWars.Screens.Maps;

namespace AnimalWars.Entities
{
    class Rua: Enemy
    {
        public Rua(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    float blood, float rateImage, bool live, int level, Map playingScreen, Texture2D bloddImage, int tamNhin)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend, vision, type, isMine, blood, rateImage, live, level, playingScreen, bloddImage, tamNhin)
        {
            frameSize = new Point(100, 100);
            sheetSize = new Point(4, 1);
            millisecondsPerFrame = 170;
        }



        public override void ChangeImageByMoving()
        {
            double angle = this.movingAngle;
            if (currentState == CharacterState.DICHUYEN)
            {

                if (angle >= 0 && angle <= 180)
                {
                    // right
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/Rua/right_dichuyen");
                }
                else
                {
                    // left
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/Rua/left_dichuyen");
                }
            }

            base.ChangeImageByMoving();
        }

        public override void CheckState()
        {
            double angle = movingAngle;
            if (currentState == CharacterState.DUNGYEN)
            {
                if (angle >= 0 && angle <= 180)
                {
                    // right
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/Rua/right_dungyen");
                }
                else
                {
                    // left
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/Rua/left_dungyen");
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
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/Rua/right_dichuyen");
                }
                else
                {
                    // left
                    image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/Rua/left_dichuyen");

                }
            }

            base.CheckState();
        }
        
    }
}
