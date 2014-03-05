using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using AnimalWars.Screens.Maps;

namespace AnimalWars.Entities
{
    class Character
    {

        public enum CharacterState
        {
            DUNGYEN,
            DICHUYEN,
            TANCONG
        }


        public Texture2D image;
        public Vector2 position;
        public float velocity;
        
        public int attack;
        public int defend;
        public int copyAttack;
        public int copyDefend;

        public int vision;
        public int type;
        public bool isMine;
        public float blood;
        public float rateImage;
        public bool live;
        public int level;
        public Texture2D imagiBlood;
        public Point currentFrame;
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame = 75;
        public Point sheetSize = new Point(4, 1);
        public Point frameSize = new Point(75, 75);

        public float scale =1;
        //SpriteManager spriteManager;

        //SpriteFont test;
        
        public List<Rectangle> compatibility = new List<Rectangle>();
        public List<Rectangle> unCompatibility = new List<Rectangle>();
        public CharacterState currentState = CharacterState.DUNGYEN;

        public Vector2 currentDirection;
        public Map spriteManager;

        //public double angle; // for test
        public Character() { }

        public Character(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                            int attack, int defend, int vision, int type, bool isMine, 
                            float blood, float rateImage, bool live, int level, Map spriteManager)
        {
            this.currentFrame = currentFrame;
            this.image = image;
            this.position = position;
            this.velocity = velocity;
            this.attack = attack;
            this.defend = defend;
            this.copyAttack = attack;
            this.copyDefend = defend;
            this.vision = vision;
            this.type = type;
            this.isMine = isMine;
            this.blood = blood;         
            this.rateImage = rateImage;
            this.live = live;
            this.level = level;
            this.scale = (float)(1 + (position.Y / Statics.GAME_HEIGHT) * 0.4);
            this.spriteManager = spriteManager;
        }

        protected bool moveStraightTo(Vector2 destination)
        {
            Vector2 lastPosition = this.position;
            Vector2 direction = destination - this.position;

            if (destination == position)
            {
<<<<<<< HEAD
                this.currentState = CharacterState.DUNGYEN;
=======
>>>>>>> f9564b87c678d788bc2dee98be15d874f804ee16
                return false;
            } 
            else if (direction.Length() < velocity)
            {
                this.currentState = CharacterState.DUNGYEN;
                this.position = destination;
            }
            else
            {
                direction.Normalize();
                this.currentState = CharacterState.DICHUYEN;
                this.position += direction * this.velocity;
            }

            // lấy giá trị của hướng di chuyển của sprite sau 1 frame bằng cách sử dụng lastPosition đã lưu trước đó.
            this.currentDirection = position - lastPosition;
            this.currentDirection.Normalize();

            return true;
        }

        public void Skill()
        {
            //Code here
        }

       
        public bool IsCollision(Rectangle rect)
        {
            Rectangle animal = new Rectangle((int)position.X,(int) position.Y, frameSize.X, frameSize.Y);
            if (animal.Intersects(rect))
                return true;
            else
                return false;
        }

        public bool CheckLive(bool live)
        {
            return true;
        }

        public void CheckLevel()
        {
            //Code here
        }

        public bool CheckCompatibility(Vector2 position, List<Rectangle> compa)
        {
            foreach (Rectangle rect in compa)
            {
                if (rect.Contains(new Point((int)position.X, (int)position.Y)))
                    return true;
            }
            return false;
        }

        public bool isCompatibility(Character sprite)
        {
            if (CheckCompatibility(sprite.position, sprite.compatibility))
                return true;
            else
                return false;
        }

        public bool isUnCompatibility(Character sprite)
        {
            if (CheckCompatibility(sprite.position, sprite.unCompatibility))
                return true;
            else
                return false;
        }

        public  void LoadContent(GameTime gameTime)
        {
            //Code here
        }

        public  virtual void Update(GameTime gameTime)
        {
            //Code here
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame >= millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.Y++;
                    //reset X
                    currentFrame.X = 0;
                    //check Y
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
                scale = (float)(1 + (position.Y / Statics.GAME_HEIGHT) * 0.4);

            }

        }

        public void Draw()
        {
            // Code here
            //Rectangle r = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
            //spriteBatch.Draw(textureImage, position, r, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);

           // Rectangle r = new Rectangle(0, 0, image.Bounds.Width, image.Bounds.Height);
            
            Rectangle r = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
            Vector2 imagePosition = new Vector2(position.X - frameSize.X / 2, position.Y - frameSize.Y / 2);
            Statics.SPRITEBATCH.Draw(this.image, imagePosition, r, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
            Rectangle b;
            float copyBlood = 3500;
            // make position sprite is center of image
            
            if ( blood > copyBlood*9/10 )
            {
                b = new Rectangle((int)(position.X - 20), (int)(position.Y - 20),
                    this.imagiBlood.Width, this.imagiBlood.Height);
            }
            else if (blood <= copyBlood * 9 / 10 && blood > copyBlood * 8 / 10)
            {
                b = new Rectangle((int)(position.X - 20), (int)(position.Y - 20),
                   this.imagiBlood.Width * 9 / 10, this.imagiBlood.Height);
            }
            else if (blood <= copyBlood * 8 / 10 && blood > copyBlood * 7 / 10)
            {
                b = new Rectangle((int)(position.X - 20), (int)(position.Y - 20),
                   this.imagiBlood.Width * 8 / 10, this.imagiBlood.Height);
            }
            else if (blood <= copyBlood * 7 / 10 && blood > copyBlood * 6 / 10)
            {
                b = new Rectangle((int)(position.X - 20), (int)(position.Y - 20),
                   this.imagiBlood.Width * 7 / 10, this.imagiBlood.Height);
            }
            else if (blood <= copyBlood * 6 / 10 && blood > copyBlood * 5 / 10)
            {
                b = new Rectangle((int)(position.X - 20), (int)(position.Y - 20),
                   this.imagiBlood.Width * 6 / 10, this.imagiBlood.Height);
            }
            else if (blood <= copyBlood * 5 / 10 && blood > copyBlood * 4 / 10)
            {
                b = new Rectangle((int)(position.X - 20), (int)(position.Y - 20),
                   this.imagiBlood.Width * 5 / 10, this.imagiBlood.Height);
            }
            else if (blood <= copyBlood * 4 / 10 && blood > copyBlood * 3 / 10)
            {
                b = new Rectangle((int)(position.X - 20), (int)(position.Y - 20),
                   this.imagiBlood.Width * 4 / 10, this.imagiBlood.Height);
            }
            else if (blood <= copyBlood * 3 / 10 && blood > copyBlood * 2 / 10)
            {
                b = new Rectangle((int)(position.X - 20), (int)(position.Y - 20),
                   this.imagiBlood.Width * 3 / 10, this.imagiBlood.Height);
            }
            else if (blood <= copyBlood * 2 / 10 && blood > copyBlood * 1 / 10)
            {
                b = new Rectangle((int)(position.X - 20), (int)(position.Y - 20),
                   this.imagiBlood.Width * 2 / 10, this.imagiBlood.Height);
            }
            else
            {
                b = new Rectangle((int)(position.X - 20), (int)(position.Y - 20),
                   this.imagiBlood.Width * 1 / 10, this.imagiBlood.Height);
            }


            Statics.SPRITEBATCH.Draw(this.imagiBlood, new Vector2(position.X - 20, position.Y - 50), b, Color.White);
            Statics.SPRITEBATCH.DrawString(Statics.BLOOD_INDEX_FONT, "blood: " + blood, new Vector2(position.X - 20, position.Y - 100), Color.Black);
            

            //spriteBatch.DrawString(sf, angle.ToString(), new Vector2(position.X - 20, position.Y + 50), Color.White);
        }

        public Rectangle boundsRectangle
        {
             get
            {
                int offset = 13;
                Vector2 imagePosition = new Vector2(position.X - frameSize.X / 2, position.Y - frameSize.X / 2);
                return new Rectangle((int)imagePosition.X + offset, (int)imagePosition.Y + offset,
                    (int)(frameSize.X - offset * 2), (int)(frameSize.X - offset * 2));

            }
        }

        public Vector2 GetCenter()
        {
            Vector2 center;
            center.X = (position.X + frameSize.X) / 2;
            center.Y = (position.Y + frameSize.Y) / 2;
            
            return center;
        }
        public virtual void ChangeImageByMoving()
        { 
            
        }

        public void Hit(Entities.Character enemy)
        {
            
            if (enemy.blood > 0)
            {
                enemy.blood--;
                this.blood--;
                if (this.blood <= 0)
                {
                    this.live = false;
                }
            }
            else {
                enemy.live = false;
            }

        }

        public virtual void CheckState()
        { 
            
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
