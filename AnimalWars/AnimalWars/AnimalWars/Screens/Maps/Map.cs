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
        public enum ControlState
        {
            FOLLOW,
            MOVE
        }
        public static ControlState controlState = ControlState.FOLLOW;
        

        public Texture2D background;
        Texture2D pauseButton, followButton, moveButton;

        public List<Entities.Character> charactersList;
        // charactersList = myCharacterList + enemyList

        public Entities.UserControlledCharacter mainCharacter;
        public List<Entities.SemiAuto> comradeList;
        // myCharacterList = comradeList + mainCharacter
        public List<Entities.Character> myCharacterList;
        public List<Entities.Enemy> enemyList;

        public int[] checkCompatibility;
        public int[] checkUnCompatibility;
        public int coefficientCompatibility = 10;
        public Map(Game game): base(game)
        {
            pauseButton = Statics.CONTENT.Load<Texture2D>("Images/Backgrounds/pause");
            isActived = true;
            followButton = Statics.CONTENT.Load<Texture2D>("Images/Intro/skip");
            moveButton = Statics.CONTENT.Load<Texture2D>("Images/Backgrounds/pause");
            //selectedSprite = -1;
        }


        void MovingTowardMouse()
        {
            if (IsMouseInScreen()){
                Entities.UserControlledCharacter p = mainCharacter;
                // if p is selected before
                p.destination = new Vector2(Statics.INPUT.mousePosition.X, Statics.INPUT.mousePosition.Y);
                p.CheckOuOfScreen();
            }
        }

        public override void Update(GameTime gameTime)
        {
            // neu nut pause duoc nhan:
            // map bi disable, unvisible
            // isActived = false
            Control();
            
            //lastMouseState = currentMouseState;
            Compatibility();
            //Attack();
            foreach (Entities.Character e in charactersList)
            {
                e.Update(gameTime);
                e.CheckState();
            }
            //ForTest();
            base.Update(gameTime);

            List<Entities.Character> charactersList2 = new List<Entities.Character>();
            foreach (Entities.Character e in charactersList)
            {
                charactersList2.Add(e);
            }
            foreach (Entities.Character e in charactersList2)
            {
                if (e.live == false)
                {
                    removeCharacter(e);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Statics.SPRITEBATCH.Begin();
            Statics.SPRITEBATCH.Draw(background, Vector2.Zero, Color.White);
            charactersList = charactersList.OrderByDescending(x => x.position.Y*-1).ToList();
            foreach (Entities.Character u in charactersList)
            {
                u.Draw();
            }
            Statics.SPRITEBATCH.Draw(pauseButton, pauseButtonRect, Color.White);
            Statics.SPRITEBATCH.Draw(followButton, followButtonRect, Color.White);
            Statics.SPRITEBATCH.Draw(moveButton, moveButtonRect, Color.White);
            
            Statics.SPRITEBATCH.End();
            base.Draw(gameTime);
        }

        public Entities.Character getMainCharacter()
        {
            return this.mainCharacter;
        }

        public List<Entities.Character> getEnemyList()
        {
            List<Entities.Character> tempList = new List<Entities.Character>();
            for (int idEnemy = 0; idEnemy < this.enemyList.Count; ++idEnemy)
            {
                tempList.Add(this.enemyList[idEnemy]);
            }
            return tempList;
        }

        public List<Entities.Character> getComradeList()
        {
            List<Entities.Character> tempList = new List<Entities.Character>();
            for (int idComrade = 0; idComrade < this.comradeList.Count; ++idComrade)
            {
                tempList.Add(this.comradeList[idComrade]);
            }
            return tempList;
        }

        public List<Entities.Character> getMyCharacterList()
        {
            List<Entities.Character> tempList = getComradeList();
            tempList.Add(this.mainCharacter);
            return tempList;
        }

        public void removeCharacter(Entities.Character deadCharacter)
        {
            if (deadCharacter is Entities.UserControlledCharacter)
            {
                myCharacterList.Remove(deadCharacter);
                //TODO het game
            }
            else if (deadCharacter is Entities.Enemy)
            {
                enemyList.Remove((Entities.Enemy)deadCharacter);
            }
            else if (deadCharacter is Entities.SemiAuto)
            {
                comradeList.Remove((Entities.SemiAuto)deadCharacter);
                myCharacterList.Remove(deadCharacter);
            }
            charactersList.Remove(deadCharacter);
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
            for (int i = 0; i < myCharacterList.Count; i++)
            {
                Entities.Character userControll = myCharacterList[i];
                if (userControll.isCompatibility(userControll))
                {
                    if (checkCompatibility[i] < 1)
                    {
                        userControll.attack += coefficientCompatibility;
                        userControll.defend += coefficientCompatibility;
                        checkCompatibility[i]++;
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

        //public void Attack()
        //{

        //    for (int i = 0; i < charactersList.Count; i++)
        //    {
        //        if (charactersList[i].live)
        //        {
        //            for (int j = 0; j < charactersList.Count; j++)
        //            {
        //                if (charactersList[j].live)
        //                {
        //                    if (charactersList[i].isCollision(charactersList[j].boundsRectangle)
        //                        && charactersList[i].isMine != charactersList[j].isMine)
        //                    {
        //                        charactersList[i].Hit(charactersList[j]);
        //                        charactersList[i].currentState = Entities.Character.CharacterState.TANCONG;
        //                        charactersList[j].currentState = Entities.Character.CharacterState.TANCONG;

        //                    }

        //                }
        //            }
        //        }

        //    }
        //    //CheckBlood();
        //}


        public Rectangle[] rectangleList
        {
            get
            {
                Rectangle[] rl = new Rectangle[this.charactersList.Count];
                for (int i = 0; i < charactersList.Count; i++)
                {
                    rl[i] = charactersList[i].getBoundsRectangle();
                }
                return rl;
            }
        }

        Rectangle pauseButtonRect {
            get {
                return new Rectangle(705, 15, 45, 45);
            }
        }
        Rectangle followButtonRect
        {
            get
            {
                return new Rectangle(655, 15, 45, 45);
            }
        }
        Rectangle moveButtonRect
        {
            get
            {
                return new Rectangle(605, 15, 45, 45);
            }
        }
        void Control()
        {
            if (Statics.INPUT.isMouseClicked)
            {
                if (followButtonRect.Contains(Statics.INPUT.mousePosition))
                {
                    controlState = ControlState.FOLLOW;
                }
                else
                {
                    if (moveButtonRect.Contains(Statics.INPUT.mousePosition))
                    {
                        controlState = ControlState.MOVE;
                    }
                    else
                    {
                        if (pauseButtonRect.Contains(Statics.INPUT.mousePosition))
                        {
                            this.Enabled = false;
                            this.Visible = false;
                            isActived = false;
                        }
                        else MovingTowardMouse();
                    }
                }
            }
        }

    }
}
