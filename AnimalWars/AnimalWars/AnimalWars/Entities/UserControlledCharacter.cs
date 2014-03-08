using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AnimalWars.Screens;
using AnimalWars.Screens.Maps;

namespace AnimalWars.Entities
{
    abstract class UserControlledCharacter: Character
    {
        public int evade = -1; // default: not evade
        public float collideRange = 75;
        public Vector2 amzPos = Vector2.Zero;
        

        public UserControlledCharacter(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    int blood, float rateImage, bool live, int level, Map spriteManager, Texture2D imagiBlood)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend,vision, type, isMine, blood, rateImage, live, level, spriteManager)
        {
            this.imagiBlood = imagiBlood;
            this.destination = position;
        }
        
        // My variables
        public Vector2 destination;
        //public Vector2 direction{get; set;}
        public override void Update(GameTime gameTime)
        {
            if (destination != null)
            {
                moveStraightTo(destination);
                //currentState = CharacterState.DICHUYEN;
            }
            //ChangeImageByMoving();
            base.Update(gameTime);
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

    }
}
