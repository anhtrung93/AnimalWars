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
using AnimalWars.Screens;

namespace AnimalWars.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlayingScreen : Screen
    {
        //SpriteFont display;
        Texture2D backGround;
        // List giữ giá trị của cả player và enemy
        List<Entities.Character> spriteList = new List<Entities.Character>();
        List<Entities.UserControlledSprite> playerList = new List<Entities.UserControlledSprite>();
        List<Entities.Enemy> enemyList = new List<Entities.Enemy>();
        
        //List<UserControlledSprite> playerList2 = new List<UserControlledSprite>();

        int selectedSprite = -1;
        //MouseState lastMouseState;

        public const int coefficientCompatibility = 10;
        //public int checkCompatibility = 0;
       // public int checkUnCompatibility = 0;
        int[] checkCompatibility = new int[10]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        int[] checkUnCompatibility = new int[10]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        public PlayingScreen(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public int GetLengthPlayerList()
        {
            return playerList.Count;
        }

       
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
            selectedSprite = -1;
            
        }

       
        
        protected override void LoadContent()
        {
            //spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            //display = Statics.BLOOD_INDEX_FONT;
            backGround = Statics.CONTENT.Load<Texture2D>("Images/Backgrounds/diahinh");
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
            enemyList.Add(new Entities.ExtendEnemy(Game.Content.Load<Texture2D>(@"Images\e-down"), new Point(0, 0), 0,
                                new Vector2(700, 500), (float)0.2, 10, 8, 1, 1, false, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1"), 300));
            enemyList.Add(new Entities.ExtendEnemy(Game.Content.Load<Texture2D>(@"Images\e-down"), new Point(0, 0), 0,
                                new Vector2(700, 300), (float)0.2, 10, 8, 1, 1, false, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1"), 300));

            foreach (Entities.Character s in playerList)
                spriteList.Add(s);
            foreach (Entities.Character s in enemyList)
                spriteList.Add(s);
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {

            //foreach (Entities.Character e in enemyList)
            //{
            //        e.Update(gameTime);
                   
            //}
            //for (int i = 0; i < playerList.Count; i++)
            //{
            //    Entities.UserControlledSprite p = playerList[i];
            //    //if (p.isRunning)
            //    {
            //        p.Update(gameTime);
            //    }
            //}
            //MouseState currentMouseState = Mouse.GetState();
            MovingTowardMouse(gameTime);
            
            //lastMouseState = currentMouseState;
            Compatibility(gameTime);
            Attack();
            foreach (Entities.Character e in spriteList)
            {
                e.Update(gameTime);
                e.CheckState();
            }
            //ForTest();
            base.Update(gameTime);
        }

        public void ChekSelected()
        {
           // double angle;
            for (int i = 0; i < playerList.Count; i++)
            {
                Entities.UserControlledSprite p = playerList[i];
                //angle = Math.Atan2(p.currentDirection.X, p.currentDirection.Y) * 360 / (2 * Math.PI);
     
            }
        }

        
        public void MovingTowardMouse(GameTime gameTime)
        {
            if (IsMouseInScreen() 
                && Statics.INPUT.isMouseClicked())
            {

                // nếu chuột click trên vùng trống hoac chi vao quan dich
                if (!ClickOnMySprite())
                {
                    for (int i = 0; i < playerList.Count; i++)
                    {
                        Entities.UserControlledSprite p = playerList[i];

                        // if p is selected before
                        if (selectedSprite == i)
                        {
                            //spriteList[i].image = Statics.CONTENT.Load<Texture2D>("Images/test");
                            selectedSprite = -1;
                            p.destination = new Vector2(Statics.INPUT.mousePosition.X, Statics.INPUT.mousePosition.Y);
                            p.CheckOuOfScreen();
                            p.isRunning = true;

                        }
                    }
                }
            }
           
        }
        public override void Draw(GameTime gameTime)
        {
            Statics.SPRITEBATCH.Begin();
            Statics.SPRITEBATCH.Draw(backGround, Vector2.Zero, Color.White);
            spriteList = spriteList.OrderByDescending(x => x.position.Y*-1).ToList();
            foreach (Entities.Character u in spriteList)
            {
                if (u.live)
                {
                    u.Draw();
                }
            }
            
            Statics.SPRITEBATCH.End();
            base.Draw(gameTime);
        }

        public bool ClickOnMySprite()
        {
            
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].live) // nếu sprite còn sống
                {
                    Entities.UserControlledSprite p = playerList[i];
                    // nếu click trúng sprite
                    if (p.boundsRectangle.Contains(Statics.INPUT.mousePosition)) 
                    {
                        if (!p.isMine)
                        {
                            return false;
                        }
                        // sprite này đã đợc chọn từ trước (double click) => thì reset: coi như chưa chọn
                        if (selectedSprite == i)
                        {
                            selectedSprite = -1;
                            return true;
                        }
                        else
                        {
                            
                            selectedSprite = i;
                            //spriteList[i].image = Statics.CONTENT.Load<Texture2D>("Images/test");
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsMouseInScreen()
        {
            if (Statics.INPUT.mousePosition.X > Statics.GAME_WIDTH
                || Statics.INPUT.mousePosition.X < 0
                || Statics.INPUT.mousePosition.Y < 0
                || Statics.INPUT.mousePosition.Y > Statics.GAME_HEIGHT)
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
                    Entities.UserControlledSprite userControll = playerList[i];
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

        public void Attack()
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
                                spriteList[i].Hit(spriteList[j]);
                                spriteList[i].currentState = Entities.Character.CharacterState.TANCONG;
                                spriteList[j].currentState = Entities.Character.CharacterState.TANCONG;

                            }
                           
                        }
                    }
                }

            }
            //CheckBlood();
        }
        public void ForTest()
        {
            if (Statics.INPUT.isMouseClicked())
            {
                for(int i = 0; i < spriteList.Count; i++)
                {
                    if (selectedSprite == i)
                    {
                        spriteList[i].image = Statics.CONTENT.Load<Texture2D>("Images/test");
                    }
                }
            }
        }

    }
}
