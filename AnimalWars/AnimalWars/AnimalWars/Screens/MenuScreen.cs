using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimalWars.Screens
{
    
    class MenuScreen: Screen
    {
        public Texture2D Texture;
        
        public ScreenState currentScreen;
        
        public MenuScreen(Game game): base(game)
        {
            this.game = game;

            Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Menu");
            // khởi tạo ở trạng thái NORMAL
            currentScreen = ScreenState.NORMAL;
            nextState = ScreenState.NORMAL;

        }
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
           

        }
        void checkNewGame()
        {
            if (Statics.INPUT.isMouseClicked() && newGameButton.Contains(Statics.INPUT.mousePosition))
            {
                nextState = ScreenState.NEWGAME;  
                isActived = false;
                this.Visible = false;
                this.Enabled = false;
            }
        }
        void checkContinue()
        {
            if (Statics.INPUT.isMouseClicked() && continueButton.Contains(Statics.INPUT.mousePosition))
            {
                nextState = ScreenState.CONTINUE; 
                isActived = false;
                this.Visible = false;
                this.Enabled = false;
            }
        }
        void checkOption()
        { 
        
        }
        void checkQuit()
        {
            if (Statics.INPUT.isMouseClicked() && quitButton.Contains(Statics.INPUT.mousePosition))
            {
                this.Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/exitMenu");
                this.currentScreen = ScreenState.EXIT;
            }
        }
        void checkHoverButton()
        {
            if (currentScreen == ScreenState.NORMAL)
            {
                if (newGameButton.Contains(Statics.INPUT.mousePosition))
                {
                    this.Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Hover/newgame");
                }
                else if (continueButton.Contains(Statics.INPUT.mousePosition))
                {
                    this.Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Hover/continue");
                }
                else if (minigameButton.Contains(Statics.INPUT.mousePosition))
                {
                    this.Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Hover/minigame");
                }
                else if (helpButton.Contains(Statics.INPUT.mousePosition))
                {
                    this.Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Hover/help");
                }
                else if (settingButton.Contains(Statics.INPUT.mousePosition))
                {
                    this.Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Hover/setting");
                }
                else if (quitButton.Contains(Statics.INPUT.mousePosition))
                {
                    this.Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Hover/quit");
                }
                else {
                    this.Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Menu");
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (currentScreen == ScreenState.NORMAL)
            {
                checkNewGame();
                checkContinue();
                checkOption();
                checkQuit();
                checkHoverButton();
            }
            else if (currentScreen == ScreenState.EXIT)
            {
                if (Statics.INPUT.isMouseClicked() && yesButton.Contains(Statics.INPUT.mousePosition))
                    this.game.Exit();
                else if (Statics.INPUT.isMouseClicked() && noButton.Contains(Statics.INPUT.mousePosition))
                {
                    this.Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Menu");
                    this.currentScreen = ScreenState.NORMAL;
                }
            }

           
        }
        public override void Draw(GameTime gameTime)
        {
            Statics.SPRITEBATCH.Begin();
            Statics.SPRITEBATCH.Draw(Texture, Vector2.Zero, Color.White);
            
            // for test
            //Statics.SPRITEBATCH.Draw(Statics.PIXEL, newGameButton, new Color(0.2f, 0.2f, 0, 0.3f));
            //Statics.SPRITEBATCH.Draw(Statics.PIXEL, continueButton, new Color(0.2f, 0.2f, 0, 0.3f));
            //Statics.SPRITEBATCH.Draw(Statics.PIXEL, minigameButton, new Color(0.2f, 0.2f, 0, 0.3f));
            //Statics.SPRITEBATCH.Draw(Statics.PIXEL, helpButton, new Color(0.2f, 0.2f, 0, 0.3f));
            //Statics.SPRITEBATCH.Draw(Statics.PIXEL, settingButton, new Color(0.2f, 0.2f, 0, 0.3f));
            //Statics.SPRITEBATCH.Draw(Statics.PIXEL, quitButton, new Color(0.2f, 0.2f, 0, 0.3f));


            Statics.SPRITEBATCH.End();
            base.Draw(gameTime);
        }
        public Rectangle newGameButton { get { return new Rectangle(233, 113, 337, 58); } }
        public Rectangle quitButton { get { return new Rectangle(233, 473, 334, 58); } }
        public Rectangle continueButton { get { return new Rectangle(233, 184, 337, 58); } }
        public Rectangle minigameButton { get { return new Rectangle(233, 256, 337, 58); } }
        public Rectangle helpButton { get { return new Rectangle(233, 330, 334, 58); } }
        public Rectangle settingButton { get { return new Rectangle(233, 399, 337, 58); } }
        public Rectangle yesButton { get { return new Rectangle(276, 251, 55, 27); } }
        public Rectangle noButton { get { return new Rectangle(470, 251, 55, 27); } }
    }
}
