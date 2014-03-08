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
    class Map1: Map
    {
        public Map1(Game game): base(game)
        {
           
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            checkCompatibility = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            checkUnCompatibility = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            // khởi tạo charactersList và userList
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
                                new Vector2(700, 0), (float)1, 10, 8, 1, 1, true, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1"), 300));
            comradeList.Add(new Entities.Rua1(Game.Content.Load<Texture2D>(@"Images/Entities/Rua/right_dichuyen"), new Point(0, 0), 0,
                                new Vector2(700, 100), (float)01, 10, 8, 1, 1, true, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1"), 300));
            
            Entities.Enemy enemy1 = new Entities.Rua(Game.Content.Load<Texture2D>(@"Images/Entities/Rua/right_dichuyen"), new Point(0, 0), 0,
                                new Vector2(700, 500), (float)01, 10, 8, 1, 1, false, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1"), 300);
            Entities.Enemy enemy2 = new Entities.Buom(Game.Content.Load<Texture2D>(@"Images/Entities/Rua/right_dichuyen"), new Point(0, 0), 0,
                                new Vector2(700, 250), (float)01, 10, 8, 1, 1, false, 3500, 2 / 3, true, 1, this,
                                Game.Content.Load<Texture2D>(@"Blood\blood1"), 300);

            enemy1.setPatrolPath(new Vector2[2] { new Vector2(500, 500), new Vector2(600, 500) });

            enemyList.Add(enemy1);
            enemyList.Add(enemy2);
            myCharacterList.Add(mainCharacter);
            foreach (Entities.Character comrade in comradeList)
            {
                myCharacterList.Add(comrade);
            }
            foreach (Entities.Character myCharacter in myCharacterList)
            {
                charactersList.Add(myCharacter);
            }
            foreach (Entities.Character enemy in enemyList)
            {
                charactersList.Add(enemy);
            }
            base.LoadContent();
        }
    }


}
