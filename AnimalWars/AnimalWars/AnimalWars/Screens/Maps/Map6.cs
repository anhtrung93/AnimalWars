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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    class Map6 : Map
    {

        public Map6(Game game)
            : base(game)
        {

        }


        public override void Initialize()
        {
            // TODO: Add your initialization code here

            checkCompatibility = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            checkUnCompatibility = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            charactersList = new List<Entities.Character>();
            mainCharacter = null;
            enemyList = new List<Entities.Enemy>();
            comradeList = new List<Entities.SemiAuto>();
            myCharacterList = new List<Entities.Character>();
            base.Initialize();


        }

        protected override void LoadContent()
        {
            background = Statics.CONTENT.Load<Texture2D>("Images/Backgrounds/diahinh");
            mainCharacter = new Demen(Game.Content.Load<Texture2D>(@"Images\right"), new Point(0, 0), 0,
                                 new Vector2(100, 40), 2, 10, 8, 1, 1, true, 3000, 2 / 3, true, 1, this,
                                 Game.Content.Load<Texture2D>(@"Blood\blood1"));
            comradeList.Add(new Entities.Rua1(Game.Content.Load<Texture2D>(@"Images/Entities/Rua/right_dichuyen"), new Point(0, 0), 0,
                                new Vector2(700, 500), (float)0.2, 10, 8, 1, 1, false, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1"), 300));
            comradeList.Add(new Entities.Rua1(Game.Content.Load<Texture2D>(@"Images/Entities/Rua/right_dichuyen"), new Point(0, 0), 0,
                                 new Vector2(700, 500), (float)0.2, 10, 8, 1, 1, false, 3500, 2 / 3, true, 1, this,
                                 Game.Content.Load<Texture2D>(@"Blood\blood1"), 300));
            enemyList.Add(new Entities.Rua(Game.Content.Load<Texture2D>(@"Images/Entities/Rua/right_dichuyen"), new Point(0, 0), 0,
                                new Vector2(700, 500), (float)0.2, 10, 8, 1, 1, false, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1"), 300));
            enemyList.Add(new Entities.Rua(Game.Content.Load<Texture2D>(@"Images/Entities/Rua/right_dichuyen"), new Point(0, 0), 0,
                                new Vector2(700, 300), (float)0.2, 10, 8, 1, 1, false, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1"), 300));

            foreach (Entities.Character myCharacter in myCharacterList)
                charactersList.Add(myCharacter);
            foreach (Entities.Character enemy in enemyList)
                charactersList.Add(enemy);
            base.LoadContent();
        }
    }
}
