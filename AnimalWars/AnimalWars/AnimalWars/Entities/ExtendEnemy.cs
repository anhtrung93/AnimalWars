using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


namespace AnimalWars.Entities
{
    class ExtendEnemy: Enemy
    {
        public ExtendEnemy(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    float blood, float rateImage, bool live, int level, Screens.PlayingScreen playingScreen, Texture2D bloddImage, int tamNhin)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend, vision, type, isMine, blood, rateImage, live, level, playingScreen, bloddImage, tamNhin)
        {
           
        }



        public override void ChangeImageByMoving()
        {
            double angle = this.movingAngle;
            if (Math.Abs(angle) < 45) // move down
                image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/ExtendEnemys/e-down");
            else if (Math.Abs(angle) > 135) // move up
                image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/ExtendEnemys/e-up");
            else if (angle > 45 && angle < 135) // move right
                image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/ExtendEnemys/e-right");
            else
                image = Statics.CONTENT.Load<Texture2D>(@"Images/Entities/ExtendEnemys/e-left");
            
            base.ChangeImageByMoving();
        }
        
    }
}
