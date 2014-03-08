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
    abstract class Character
    {
        const float HIT_DISTANCE = 100.0f;

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
        public int blood;
        public float rateImage;
        public bool live;
        public int level;
        public Texture2D imagiBlood;
        public Point currentFrame;
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame = 75;
        public Point sheetSize;
        public Point frameSize ;

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
                            int blood, float rateImage, bool live, int level, Map spriteManager)
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

        protected void setMoving()
        {
            this.currentState = CharacterState.DICHUYEN;
        }

        protected void setStandStill()
        {
            this.currentState = CharacterState.DUNGYEN;
        }

        protected void setAttack()
        {
            this.currentState = CharacterState.TANCONG;
        }

        protected bool moveStraightTo(Vector2 destination)
        {
            Vector2 lastPosition = this.position;
            Vector2 direction = destination - this.position;

            if (destination == position)
            {
                setStandStill();                
                return false;
            } 
            else if (direction.Length() < velocity)
            {
                setMoving();
                this.position = destination;
            }
            else
            {
                direction.Normalize();
                setMoving();
                this.position += direction * this.velocity;
            }

            // lấy giá trị của hướng di chuyển của sprite sau 1 frame bằng cách sử dụng lastPosition đã lưu trước đó.
            this.currentDirection = position - lastPosition;
            this.currentDirection.Normalize();
            if (!isSafe)
            {
                position = lastPosition;
            }           

            return true;
        }

        public void Skill()
        {
            //Code here
        }

       
        public bool isCollision(Rectangle rect)
        {
            Rectangle animal = new Rectangle((int)position.X,(int) position.Y, frameSize.X, frameSize.Y);
            if (animal.Intersects(rect))
                return true;
            else
                return false;
        }

        // kiểm tra xem sprite có giao nhau hay đè hình lên sprite khác
        public bool isSafe
        {
            get
            {
                Rectangle[] rl = this.spriteManager.rectangleList;
                for (int i = 0; i < rl.Length; i++)
                {
                    // nếu sprite còn sống
                    if (rl[i] != Rectangle.Empty)
                    {
                        // check if it is not itself
                        if (this.getBoundsRectangle() != rl[i])
                        {
                            if (this.getBoundsRectangle().Intersects(rl[i]))
                                return false;
                        }
                    }
                }
                return true;
            }
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
            sheetSize = new Point(image.Bounds.Width / frameSize.X, image.Bounds.Height / frameSize.Y);
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
                scale = (float)(1 + (position.Y / Statics.GAME_HEIGHT) * 0.25);

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
            int copyBlood = 3500;
            // make position sprite is center of image
            int blood1 = (int)(blood*10 / copyBlood);
            if(blood1*copyBlood < blood*10) blood1++;
            b = new Rectangle((int)(position.X - 20), (int)(position.Y - 20),
                   (this.imagiBlood.Width * blood1) / 10, this.imagiBlood.Height);
            
            Statics.SPRITEBATCH.Draw(this.imagiBlood, new Vector2(position.X - 20, position.Y - 50), b, Color.White);
            Statics.SPRITEBATCH.DrawString(Statics.BLOOD_INDEX_FONT, "blood: " + blood, new Vector2(position.X - 20, position.Y - 100), Color.Black);
            if (Statics.DEBUG_FLAG == true)
            {
                Statics.SPRITEBATCH.Draw(Statics.PIXEL, getBoundsRectangle(), new Color(0.6f, 0.7f, 0.6f, 0.3f));
                Statics.SPRITEBATCH.Draw(Statics.PIXEL, new Rectangle((int)position.X - 3 , (int)position.Y - 3, 6, 6),
                    new Color(0.9f, 0.2f, 0.3f, 0.3f));
            }

            //spriteBatch.DrawString(sf, angle.ToString(), new Vector2(position.X - 20, position.Y + 50), Color.White);
        }

        abstract public Rectangle getBoundsRectangle();

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

        public bool isAbleToHit(Entities.Character opponent)
        {
            float distance = Vector2.Distance(opponent.position, this.position);
            if (distance < HIT_DISTANCE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void hit(Entities.Character enemy)
        {
            if (enemy.blood > 0)
            {
                enemy.blood--;
                if (this.blood <= 0)
                {
                    this.live = false;
                }
            }
            else 
            {
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
