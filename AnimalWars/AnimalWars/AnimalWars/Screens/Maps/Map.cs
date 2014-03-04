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


namespace AnimalWars.Screens.Maps
{
    class Map : Screen
    {

        public Texture2D background;
        Texture2D pauseButton;
        public List<Entities.Character> charactersList;
        public List<Entities.UserControlledSprite> usersList;
        public List<Entities.Enemy> enemyList;
        public int selectedSprite;
        public int[] checkCompatibility;
        public int[] checkUnCompatibility;
        public int coefficientCompatibility = 10;
        public Map(Game game): base(game)
        {
            pauseButton = Statics.CONTENT.Load<Texture2D>("Images/Backgrounds/pause");
            isActived = true;
            //selectedSprite = -1;
        }


        void MovingTowardMouse()
        {
            if (IsMouseInScreen()
                && Statics.INPUT.isMouseClicked)
            {

                // nếu chuột click trên vùng trống hoac chi vao quan dich
                if (!ClickOnMySprite())
                {
                    for (int i = 0; i < usersList.Count; i++)
                    {
                        Entities.UserControlledSprite p = usersList[i];

                        // if p is selected before
                        if (selectedSprite == i)
                        {
                            //spriteList[i].image = Statics.CONTENT.Load<Texture2D>("Images/test");
                            selectedSprite = -1;
                            p.destination = new Vector2(Statics.INPUT.mousePosition.X, Statics.INPUT.mousePosition.Y);
                            p.CheckOuOfScreen();

                        }
                    }
                }
            }
        }
        public override void Initialize()
        {
            selectedSprite = -1;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // neu nut pause duoc nhan:
            // map bi disable, unvisible
            // isActived = false
            CheckPause();
            MovingTowardMouse();

            //lastMouseState = currentMouseState;
            Compatibility();
            Attack();
            foreach (Entities.Character e in charactersList)
            {
                e.Update(gameTime);
                e.CheckState();
            }
            //ForTest();
            base.Update(gameTime);
            //base.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            Statics.SPRITEBATCH.Begin();
            Statics.SPRITEBATCH.Draw(background, Vector2.Zero, Color.White);
            charactersList = charactersList.OrderByDescending(x => x.position.Y*-1).ToList();
            foreach (Entities.Character u in charactersList)
            {
                if (u.live)
                {
                    u.Draw();
                }
            }
            Statics.SPRITEBATCH.Draw(pauseButton, pauseButtonRect, Color.White);
            Statics.SPRITEBATCH.End();
            base.Draw(gameTime);
        }

        public int GetPlayerList()
        {
            return usersList.Count;
        }
        

        bool ClickOnMySprite()
        {
            for (int i = 0; i < usersList.Count; i++)
            {
                if (usersList[i].live) // nếu sprite còn sống
                {
                    Entities.UserControlledSprite p = usersList[i];
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

        bool IsMouseInScreen()
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

        public void Compatibility()
        {
            for (int i = 0; i < usersList.Count; i++)
            {
                if (usersList[i].live)
                {
                    Entities.UserControlledSprite userControll = usersList[i];
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

        public void Attack()
        {

            for (int i = 0; i < charactersList.Count; i++)
            {
                if (charactersList[i].live)
                {
                    for (int j = 0; j < charactersList.Count; j++)
                    {
                        if (charactersList[j].live)
                        {
                            if (charactersList[i].IsCollision(charactersList[j].boundsRectangle)
                                && charactersList[i].isMine != charactersList[j].isMine)
                            {
                                charactersList[i].Hit(charactersList[j]);
                                charactersList[i].currentState = Entities.Character.CharacterState.TANCONG;
                                charactersList[j].currentState = Entities.Character.CharacterState.TANCONG;

                            }

                        }
                    }
                }

            }
            //CheckBlood();
        }

        public Vector2[] GetPositionList
        {
            get
            {
                Vector2[] positionList = new Vector2[usersList.Count];
                for (int i = 0; i < usersList.Count; i++)
                {
                    if (usersList[i].live)
                        positionList[i] = usersList[i].position;
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
                Vector2[] positionList = new Vector2[charactersList.Count];
                for (int i = 0; i < charactersList.Count; i++)
                {
                    if (charactersList[i].live)
                        positionList[i] = charactersList[i].position;
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
                Rectangle[] rl = new Rectangle[this.charactersList.Count];
                for (int i = 0; i < charactersList.Count; i++)
                {
                    if (charactersList[i].live)
                    {
                        rl[i] = charactersList[i].boundsRectangle;
                    }
                    else
                        rl[i] = Rectangle.Empty;
                }
                return rl;
            }
        }

        void CheckPause()
        { 
            if(Statics.INPUT.isMouseClicked && pauseButtonRect.Contains(Statics.INPUT.mousePosition))
            {
                this.Enabled = false;
                this.Visible = false;
                isActived = false;
            }
        }
        
        Rectangle pauseButtonRect {
            get {
                return new Rectangle(705, 15, 45, 45);
            }
        }

    }
}
