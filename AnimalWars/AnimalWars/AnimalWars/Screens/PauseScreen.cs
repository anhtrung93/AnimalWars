using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimalWars.Screens
{
    
    class PauseScreen : Screen
    {
        public Texture2D Texture;
        
        public ScreenState currentState;
        public ScreenState nextState;
        public Screen map;
        List<Maps.Map> mapsList;
        int currentMap;
        public PauseScreen(Game game)
            : base(game)
        {
            this.game = game;
            
            Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Menu");
            // khởi tạo ở trạng thái NORMAL
            currentState = ScreenState.NORMAL;

        }

        public PauseScreen(Game game, Screen map)
            : this(game)
        {
            this.map = map;
        }
        public PauseScreen(Game game, Screen map, List<Maps.Map> mapsList, int currentMap)
            : this(game, map)
        {
            this.mapsList = new List<Maps.Map>();
            mapsList.AddRange(this.mapsList);
            this.currentMap = currentMap;
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
                isActived = false;
                this.Visible = false;
                this.Enabled = false;
                
                //map = new MapListScreen(game, mapsList, currentMap);
                map.isActived = true;
                this.nextState = ScreenState.NEWGAME;
                //game.Components.Add(map);
               
                
            }
        }
        void checkResume()
        {
            if (Statics.INPUT.isMouseClicked() && continueButton.Contains(Statics.INPUT.mousePosition))
            {
                isActived = false;
                this.Visible = false;
                this.Enabled = false;
                map.isActived = true;
                map.Visible = true;
                map.Enabled = true;
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
                this.currentState = ScreenState.EXIT;
            }
        }
        void checkHoverButton()
        {
            if (currentState == ScreenState.NORMAL)
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
                else
                {
                    this.Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Menu");
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (currentState == ScreenState.NORMAL)
            {
                checkNewGame();
                checkResume();
                checkOption();
                checkQuit();
                checkHoverButton();
            }
            else if (currentState == ScreenState.EXIT)
            {
                if (Statics.INPUT.isMouseClicked() && yesButton.Contains(Statics.INPUT.mousePosition))
                    this.game.Exit();
                else if (Statics.INPUT.isMouseClicked() && noButton.Contains(Statics.INPUT.mousePosition))
                {
                    this.Texture = Statics.CONTENT.Load<Texture2D>("Images/Menu/Menu");
                    this.currentState = ScreenState.NORMAL;
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
