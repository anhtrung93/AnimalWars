using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AnimalWars
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SpriteFont display;
        //Texture2D blood;
        // List giữ giá trị của cả player và enemy
        List<Sprite> spriteList = new List<Sprite>();
        List<UserControlledSprite> playerList = new List<UserControlledSprite>();
        List<Enemy> enemyList = new List<Enemy>();
        //List<UserControlledSprite> playerList2 = new List<UserControlledSprite>();

        int selectedSprite;
        MouseState lastMouseState;

        public const int coefficientCompatibility = 10;
        //public int checkCompatibility = 0;
       // public int checkUnCompatibility = 0;
        int[] checkCompatibility = new int[10]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        int[] checkUnCompatibility = new int[10]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public int GetLengthPlayerList()
        {
            return playerList.Count;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
            selectedSprite = -1;
            
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            display = Game.Content.Load<SpriteFont>(@"fond");
           // blood = Game.Content.Load<Texture2D>(@"Blood\blood1");
            
            playerList.Add(new BugBuster(Game.Content.Load<Texture2D>(@"Images\right"), new Point(0, 0), 0,
                                new Vector2(100, 40), 2, 10, 8, 1, 1, true, 3000 , 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1")));
            playerList.Add(new BugBuster(Game.Content.Load<Texture2D>(@"Images\right"), new Point(0, 0), 0,
                                new Vector2(100, 100), 2, 10, 7, 1, 1, true, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1")));
            playerList.Add(new BugBuster(Game.Content.Load<Texture2D>(@"Images\right"), new Point(0, 0), 0,
                               new Vector2(100, 400), 2, 10, 7, 1, 1, true, 3500, 2 / 3, true, 1, this,
                               Game.Content.Load<Texture2D>(@"Blood\blood1")));
            ////playerList.Add(new BugBuster(Game.Content.Load<Texture2D>(@"Images\1384608133_bug"),
            ////                    new Vector2(700, 40), 2, 10, 8, 1, 1, false, 2500, 2 / 3, true, 1, this,
            ////                    Game.Content.Load<Texture2D>(@"Blood\blood1")));
            ////playerList.Add(new BugBuster(Game.Content.Load<Texture2D>(@"Images\1384608133_bug"),
            ////                    new Vector2(700, 100), 2, 10, 8, 1, 1, false, 3500, 2 / 3, true, 1, this,
            ////                    Game.Content.Load<Texture2D>(@"Blood\blood1")));
            //////playerList.Add(new Lion(Game.Content.Load<Texture2D>(@"Images\lion_pts"), 
            //    new Vector2(40,200),2,10,1,1,2,false,1,1,true,1, this));
            enemyList.Add(new Enemy(Game.Content.Load<Texture2D>(@"Images\down"), new Point(0, 0), 0,
                                new Vector2(700, 500), (float)0.2, 10, 8, 1, 1, false, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1"), 300));
            enemyList.Add(new Enemy(Game.Content.Load<Texture2D>(@"Images\e-down"), new Point(0, 0), 0,
                                new Vector2(700, 300), (float)0.2, 10, 8, 1, 1, false, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1"), 300));
            
            foreach (Sprite s in playerList)
                spriteList.Add(s);
            foreach (Sprite s in enemyList)
                spriteList.Add(s);
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //ChekSelected();
            foreach (Enemy e in enemyList)
            {
                    e.Update(gameTime);
            
            }
            MouseState currentMouseState = Mouse.GetState();
            MovingTowardMouse(gameTime, currentMouseState);
            
            lastMouseState = currentMouseState;
            Compatibility(gameTime);
            Hit();
            ChangeImageByMovingDirection();
            //CheckBlood();
            base.Update(gameTime);
        }

        public void ChekSelected()
        {
            double angle;
            for (int i = 0; i < playerList.Count; i++)
            {
                UserControlledSprite p = playerList[i];
                angle = Math.Atan2(p.currentDirection.X, p.currentDirection.Y) * 360 / (2 * Math.PI);
     
                //if (p.isRunning)
                //{
                //    if (p.evade > -1)
                //        p.image = Game.Content.Load<Texture2D>(@"Images\tranh");
                //    else
                //        p.image = Game.Content.Load<Texture2D>(@"Images\1384608133_bug");
                //}
                //else
                //{
                //    p.image = Game.Content.Load<Texture2D>(@"Images\1384505709_kbugbuster");

                //}
                //if (i == selectedSprite)
                //{
                //    p.image = Game.Content.Load<Texture2D>(@"Images\1384614265_Bug");
                //}
            }
        }

        public void ChangeImageByMovingDirection()
        { 
            double angle;
            // đối với player
            for (int i = 0; i < playerList.Count; i++)
            {
                UserControlledSprite p = playerList[i];
                angle = Math.Atan2(p.currentDirection.X, p.currentDirection.Y) * 360 / (2 * Math.PI);
               
                if (Math.Abs(angle) < 45) // move down
                    p.image = Game.Content.Load<Texture2D>(@"Images/down");
                else if(Math.Abs(angle) > 135) // move up
                    p.image = Game.Content.Load<Texture2D>(@"Images/up");
                else if(angle > 45 && angle < 135) // move right
                    p.image = Game.Content.Load<Texture2D>(@"Images/right");
                else
                    p.image = Game.Content.Load<Texture2D>(@"Images/left");
              
                
                    
            }
            // đối với enemy
            for (int i = 0; i < enemyList.Count; i++)
            {
                Enemy p = enemyList[i];
                angle = Math.Atan2(p.currentDirection.X, p.currentDirection.Y) * 360 / (2 * Math.PI);

                if (Math.Abs(angle) < 45) // move down
                    p.image = Game.Content.Load<Texture2D>(@"Images/e-down");
                else if (Math.Abs(angle) > 135) // move up
                    p.image = Game.Content.Load<Texture2D>(@"Images/e-up");
                else if (angle > 45 && angle < 135) // move right
                    p.image = Game.Content.Load<Texture2D>(@"Images/e-right");
                else
                    p.image = Game.Content.Load<Texture2D>(@"Images/e-left");



            }
        }
        public void MovingTowardMouse(GameTime gameTime, MouseState currentMouseState)
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && IsMouseInScreen(currentMouseState) 
                && lastMouseState.LeftButton == ButtonState.Released)
            {

                // nếu chuột click trên vùng trống
                if (!ClickOnMySprite(currentMouseState))
                {
                    for (int i = 0; i < playerList.Count; i++)
                    {
                        UserControlledSprite p = playerList[i];

                        // if p is selected before
                        if (selectedSprite == i)
                        {
                            selectedSprite = -1;
                            p.destination = new Vector2(currentMouseState.X, currentMouseState.Y);
                            p.CheckOuOfScreen(Game.Window.ClientBounds);
                            p.isRunning = true;

                        }
                    }
                }
            }
            for (int i = 0; i < playerList.Count; i++)
            {
                UserControlledSprite p = playerList[i];
                if (p.isRunning)
                {
                    p.Update(gameTime);
                }
            }
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Sprite u in spriteList)
            {
                if (u.live)
                {
                    u.Draw(spriteBatch, display);
                }
                //spriteBatch.DrawString(this.display, "attack" + u.attack, new Vector2(u.position.X, u.position.Y - 20), Color.Green);
            }
            //foreach (Enemy e in enemyList)
            //{
            //    if (e.live)
            //    {
            //        e.Draw(spriteBatch, display);
            //    }
            //    //spriteBatch.DrawString(this.display, "attack" + u.attack, new Vector2(u.position.X, u.position.Y - 20), Color.Green);
            //}
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public bool ClickOnMySprite(MouseState mouseState)
        {
            
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].live) // nếu sprite còn sống
                {
                    UserControlledSprite p = playerList[i];
                    // nếu click trúng sprite
                    if (p.boundsRectangle.Contains(new Point(mouseState.X, mouseState.Y))) 
                    {
                        if (!p.isMine)
                        {
                            return false;
                        }
                        // sprite này đã được chọn từ trước (double click) => thì reset: coi như chưa chọn
                        if (selectedSprite == i)
                        {
                            selectedSprite = -1;
                            return true;
                        }
                        else
                        {
                            selectedSprite = i;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsMouseInScreen(MouseState mouseState)
        {
            if (mouseState.X > ((Game1)Game).Window.ClientBounds.Width
                || mouseState.X < 0
                || mouseState.Y < 0
                || mouseState.Y > ((Game1)Game).Window.ClientBounds.Height)
            {
                return false;
            }
            return true;
        }

        public void Compatibility(GameTime gameTime)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].live)
                {
                    UserControlledSprite userControll = playerList[i];
                    if (userControll.isCompatibility(userControll))
                    {
                        if (checkCompatibility[i] < 1)
                        {
                            userControll.attack += coefficientCompatibility;
                            userControll.defend += coefficientCompatibility;
                            checkCompatibility[i]++;
                        }
                    }

                    if (userControll.isUnCompatibility(userControll))
                    {
                        if (checkUnCompatibility[i] < 1)
                        {
                            userControll.attack -= coefficientCompatibility;
                            userControll.defend -= coefficientCompatibility;
                            checkUnCompatibility[i]++;
                        }
                    }

                    if (!userControll.isCompatibility(userControll) &&
                        !userControll.isUnCompatibility(userControll))
                    {
                        userControll.attack = userControll.copyAttack;
                        userControll.defend = userControll.copyDefend;
                        checkCompatibility[i] = 0;
                        checkUnCompatibility[i] = 0;
                    }

                }
            }
        }
       
        public Vector2[] GetPositionList
        {
            get
            {
                Vector2[] positionList = new Vector2[playerList.Count];
                for (int i = 0; i < playerList.Count; i++)
                {
                    if (playerList[i].live)
                        positionList[i] = playerList[i].position;
                    else
                        positionList[i] = new Vector2(-1, -1);
                }
                return positionList;
            }

        }
        public Vector2[] GetSpritePositionList
        {
            get
            {
                Vector2[] positionList = new Vector2[spriteList.Count];
                for (int i = 0; i < spriteList.Count; i++)
                {
                    if (spriteList[i].live)
                        positionList[i] = spriteList[i].position;
                    else
                        positionList[i] = new Vector2(-1, -1);
                }
                return positionList;
            } 
        }
        public Rectangle[] rectangleList
        {
            get
            {
                Rectangle[] rl = new Rectangle[this.spriteList.Count];
                for (int i = 0; i < spriteList.Count; i++)
                {
                    if (spriteList[i].live)
                    {
                        rl[i] = spriteList[i].boundsRectangle;
                    }
                    else
                        rl[i] = Rectangle.Empty;
                }
                return rl;
            }
        }

        public void Hit()
        {
           
            for (int i = 0; i < spriteList.Count; i++)
            {
                if (spriteList[i].live)
                {
                    for (int j = 0; j < spriteList.Count; j++)
                    {
                        if(spriteList[j].live)
                        {
                            if (spriteList[i].IsCollision(spriteList[j].boundsRectangle)
                                && spriteList[i].isMine != spriteList[j].isMine)
                            {
                                int attackI = spriteList[i].attack;
                                int defendI = spriteList[i].defend;

                                int attackJ = spriteList[j].attack;
                                int defendJ = spriteList[j].defend;
                               /* if (spriteList[i].isMine == true)
                                {
                                    spriteList[i].image = Game.Content.Load<Texture2D>(@"Images\lion_pts");
                                }
                                else
                                {
                                    spriteList[i].image = Game.Content.Load<Texture2D>(@"Images\casau_pts");
                                }

                                if (spriteList[j].isMine == true)
                                {
                                    spriteList[j].image = Game.Content.Load<Texture2D>(@"Images\lion_pts");
                                }
                                else
                                {
                                    spriteList[j].image = Game.Content.Load<Texture2D>(@"Images\casau_pts");
                                }
                                */
                                if (spriteList[i].blood > 0)
                                {
                                    //temp = spriteList[i].blood;
                                    spriteList[i].blood -= (attackJ - defendI) / 2;
                                }
                                else
                                {
                                    spriteList[i].live = false;
                                }
                                if (spriteList[j].blood > 0)
                                {
                                    //temp = spriteList[j].blood;
                                    spriteList[j].blood -= (attackI - defendJ) / 2;
                                }
                                else
                                {
                                    spriteList[j].live = false;
                                }

                            }
                            //else
                            //{
                            //    if (spriteList[i].isMine == true)
                            //    {
                            //        spriteList[i].image = Game.Content.Load<Texture2D>(@"Images\1384505709_kbugbuster");
                            //    }
                            //    else
                            //    {
                            //        spriteList[i].image = Game.Content.Load<Texture2D>(@"Images\1384608133_bug");
                            //    }

                            //    if (spriteList[j].isMine == true)
                            //    {
                            //        spriteList[j].image = Game.Content.Load<Texture2D>(@"Images\1384505709_kbugbuster");
                            //    }
                            //    else
                            //    {
                            //        spriteList[j].image = Game.Content.Load<Texture2D>(@"Images\1384608133_bug");
                            //    }
                            //}
                        }
                    }
                }

            }
            //CheckBlood();
        }


    }
}
