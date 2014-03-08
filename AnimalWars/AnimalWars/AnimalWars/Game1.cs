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

namespace AnimalWars
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class AdventureAndRescueGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont testCompatibility;
        Texture2D diahinh;
        public int currentMap;

        List<Screens.Maps.Map> mapsList;

        //int currentMap;
        


        // My variables
       // Screens.Screen introScreen;
        Screens.Screen currentScreen;
        Screens.PauseScreen pauseScreen;
       // Screens.Screen menuScreen;


        public AdventureAndRescueGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = Statics.GAME_HEIGHT;
            graphics.PreferredBackBufferWidth = Statics.GAME_WIDTH;
            Statics.CONTENT = Content;
            Statics.GRAPHICSDEVICE = GraphicsDevice;
            Statics.INPUT = new Managers.InputManager();

            Window.Title = Statics.TITLE;
           
            this.graphics.ApplyChanges();

            currentMap = 2;
            
            this.IsMouseVisible = true;
        }

        void InitMap()
        {
            mapsList = new List<Screens.Maps.Map>();
            Screens.Maps.Map1 map1 = new Screens.Maps.Map1(this);
            Screens.Maps.Map2 map2 = new Screens.Maps.Map2(this);
            Screens.Maps.Map3 map3 = new Screens.Maps.Map3(this);
            Screens.Maps.Map4 map4 = new Screens.Maps.Map4(this);
            Screens.Maps.Map5 map5 = new Screens.Maps.Map5(this);
            Screens.Maps.Map6 map6 = new Screens.Maps.Map6(this);
            mapsList.Add(map1);
            mapsList.Add(map2);
            mapsList.Add(map3);
            mapsList.Add(map4);
            mapsList.Add(map5);
            mapsList.Add(map6);
        }
        protected override void Initialize()
        {

            InitMap();
            //currentScreen = new Screens.Maps.PlayingScreen(this);
            //currentScreen = new Screens.MapListScreen(this, mapsList);
            //pauseScreen = new PauseScreen(this);

           // Components.Add(pauseScreen);
           // pauseScreen.Enabled = false;
            //pauseScreen.Visible = false;
            
            currentScreen = new Screens.LoadingScreen(this);
            Components.Add(currentScreen);
           
            base.Initialize();
        }
        

       
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Statics.SPRITEBATCH = spriteBatch;
            Statics.BLOOD_INDEX_FONT = Statics.CONTENT.Load<SpriteFont>("Fonts/bloodFont");
            Statics.FONT = Statics.CONTENT.Load<SpriteFont>("Fonts/font");
            Statics.PIXEL = Statics.CONTENT.Load<Texture2D>("Images/Backgrounds/pixel");

            testCompatibility = Statics.BLOOD_INDEX_FONT;
            diahinh = Content.Load<Texture2D>(@"Images\m_5");
            
            // maps
            
           
        }

       
        protected override void UnloadContent()
        {
           
        }
        public void ChangeState()
        {
            
            if (!currentScreen.isActived)
            {
                if (currentScreen is Screens.LoadingScreen)
                {
                    Components.Clear();
                    currentScreen = new Screens.IntroScreen(this);
                    Components.Add(currentScreen);

                }
                else if (currentScreen is Screens.IntroScreen)
                {
                    Components.Clear();
                    currentScreen = new Screens.MenuScreen(this);
                    Components.Add(currentScreen);

                }
                else if (currentScreen is Screens.MenuScreen)
                {
                    if (currentScreen.nextState == ScreenState.NEWGAME)
                    {
                        Components.Clear();

                        currentScreen = new Screens.MapListScreen(this, mapsList, 0);
                        currentMap = 0;
                        Components.Add(currentScreen);
                    }
                    else if (currentScreen.nextState == ScreenState.CONTINUE)
                    {
                        Components.Clear();
                        currentScreen = new Screens.MapListScreen(this, mapsList, currentMap);
                        Components.Add(currentScreen);
                    }
                    
                }
                else if (currentScreen is Screens.MapListScreen)
                {
                    if (currentScreen.nextState == ScreenState.VEMENU)
                    {
                        Components.Clear();
                        currentScreen = new MenuScreen(this);
                        Components.Add(currentScreen);
                    }
                    else if (currentScreen.nextState == ScreenState.SANGCHOI)
                    {
                        Components.Clear();
                        //currentScreen = mapsList[currentScreen.selectedMap];
                        switch (currentScreen.selectedMap)
                        { 
                            case 0:
                                currentScreen = new Screens.Maps.Map1(this);
                                break;
                            case 1:
                                currentScreen = new Screens.Maps.Map2(this);
                                break;
                            case 2:
                                currentScreen = new Screens.Maps.Map3(this);
                                break;
                            case 3:
                                currentScreen = new Screens.Maps.Map4(this);
                                break;
                            case 4:
                                currentScreen = new Screens.Maps.Map5(this);
                                break;
                            case 5:
                                currentScreen = new Screens.Maps.Map6(this);
                                break;
                        }
                        Components.Add(currentScreen);
                    }
                }
                else if (currentScreen is Screens.Maps.Map)
                {
                    //Components.Clear();
                    if (pauseScreen == null || pauseScreen.currentState != ScreenState.EXIT)
                    {
                        pauseScreen = new PauseScreen(this, currentScreen, mapsList, currentMap);
                        Components.Add(pauseScreen);
                    }
                    
                }
                

            }
            if (pauseScreen != null && pauseScreen.isActived == false && pauseScreen.nextState == ScreenState.NEWGAME)
            {
                Components.Clear();
                currentScreen = new Screens.MapListScreen(this, mapsList, currentMap);
                Components.Add(currentScreen);
                
                pauseScreen.nextState = ScreenState.KHONGDOI;
            }
           
        }
     
        protected override void Update(GameTime gameTime)
        {
            Statics.INPUT.Update();
            ChangeState();
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Statics.SPRITEBATCH.Begin();
            //spriteBatch.Draw(diahinh, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
            //    Color.White);
            //currentScreen.Draw();
            
            //Statics.SPRITEBATCH.End();
            
            base.Draw(gameTime);
        }
    }
}
