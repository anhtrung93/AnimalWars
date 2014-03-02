using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AnimalWars.Screens
{
    
    public enum ScreenState
    {
        // for menuScreen
        NORMAL,
        NEWGAME,
        CONTINUE,
        EXIT,
        OPTION,
        HELP,
        // for maplistScreen
        GIUNGUYEN,
        VEMENU,
        SANGCHOI,
        // for pause screen
        KHONGDOI

    }
    public abstract class Screen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        
        public ScreenState nextState;
        public bool isActived = true;
        public Game game;
        public int selectedMap = -1;

        public Screen(Game game): base(game)
        {
            isActived = true;
            nextState = ScreenState.GIUNGUYEN;
            selectedMap = -1;
        }

        public virtual void Update()
        {
        }
        public virtual void Draw()
        { 
        
        }
        public Rectangle MyProperty { get; set; }
    }
}
