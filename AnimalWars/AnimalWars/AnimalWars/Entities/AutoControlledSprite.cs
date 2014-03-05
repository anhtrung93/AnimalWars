using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace AnimalWars.Entities
{
    class AutoControlledSprite : Character
    {
        public Screens.PlayingScreen spriteManager;
        public int evade = -1; // default: not evade
        public float collideRange = 75;
        public Vector2 amzPos = Vector2.Zero;
        
        public AutoControlledSprite(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    float blood, float rateImage, bool live, int level, Screens.PlayingScreen spriteManager, Texture2D imagiBlood)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend, vision, type, isMine, blood, rateImage, live, level)
        {
            this.spriteManager = spriteManager;
            this.imagiBlood = imagiBlood;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void AutoMove()
        {
            MoveTo(destination);
            
        }

        public void CheckOuOfScreen()
        {
            Vector2 imageSize = new Vector2(frameSize.X, frameSize.Y);
            float minWidth = imageSize.X / 2;
            float minHeight = imageSize.Y / 2;
            float maxWidth = Statics.GAME_WIDTH - imageSize.X / 2;
            float maxHeight = Statics.GAME_HEIGHT - imageSize.Y / 2;
            if (destination.X < minWidth)
                destination.X = minWidth;
            if (destination.X > maxWidth)
                destination.X = maxWidth;
            if (destination.Y < minHeight)
                destination.Y = minHeight;
            if (destination.Y > maxHeight)
                destination.Y = maxHeight;

        }
        public double movingAngle
        {
            get
            {
                return (Math.Atan2(currentDirection.X, currentDirection.Y) * 360 / (2 * Math.PI));
            }
        }


    }
}
