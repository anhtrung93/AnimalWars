using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AnimalWars.Screens;
using AnimalWars.Screens.Maps;

namespace AnimalWars.Screens
{
    class MapListScreen: Screen
    {
        
        Texture2D[] textures;
        int textureIndex = 0;
        List<Map> mapsList;
        int currentMap;
        List<Rectangle> rectList;
        Texture2D lockImage = Statics.CONTENT.Load<Texture2D>("Images/Maps/lock");
        Texture2D bolderImage = Statics.CONTENT.Load<Texture2D>("Images/Maps/bolder");
        //public NextState nextState;
        //int selectedMap = -1;
        public MapListScreen(Game game, List<Map> mapsList):base(game)
        {
            this.textures = new Texture2D[2];
            textures[0] = Statics.CONTENT.Load<Texture2D>("Images/Maps/map");
            this.mapsList = new List<Map>();
            mapsList.AddRange(this.mapsList);
            // rectangles
            rectList = new List<Rectangle>();
            rectList.Add(new Rectangle(50, 50, 200, 180));
            rectList.Add(new Rectangle(300, 50, 200, 180));
            rectList.Add(new Rectangle(550, 50, 200, 180));
            rectList.Add(new Rectangle(50, 280, 200, 180));
            rectList.Add(new Rectangle(300, 280, 200, 180));
            rectList.Add(new Rectangle(550, 280, 200, 180));
            

        }

        public MapListScreen(Game game, List<Map> mapsList, int currentMap)
            : this(game, mapsList)
        {
            this.currentMap = currentMap;
        }

        public override void Update(GameTime gameTime)
        {
            CheckSelectMap();  
            if (nextState != ScreenState.GIUNGUYEN)
            { 
                this.Visible = false;
                this.Enabled = false;
                //if (nextState == NextState.VEMENU)
                {
                    isActived = false;
                }
            }
            base.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            Statics.SPRITEBATCH.Begin();
            Statics.SPRITEBATCH.Draw(this.textures[textureIndex], Vector2.Zero, Color.White);
            if(selectedMap != -1)
                Statics.SPRITEBATCH.Draw(bolderImage, rectList[selectedMap], Color.White);
            for (int i = currentMap + 1; i < 6; i++)
            {
                Statics.SPRITEBATCH.Draw(lockImage, rectList[i], Color.White);
            }
                // for test
               // Statics.SPRITEBATCH.Draw(Statics.PIXEL, backPosition, new Color(0.6f, 0.7f, 0.8f, 0.3f));
            //Statics.SPRITEBATCH.Draw(Statics.PIXEL, playPosition, new Color(0.6f, 0.7f, 0.8f, 0.3f));
            Statics.SPRITEBATCH.End();
            
            base.Draw();
        }
        // phải có 1 đối tượng là 1 list các map
        // phải có 1 hàm để xét xem có map nào được chon không, và map được chọn có chỉ số là gì.
        // có hai nút điều khiển ở dưới.
        // back to Menu
        // Play

        void CheckSelectMap()
        {
            int mapIndex = clickTrungMap;
            if(Statics.INPUT.isMouseClicked){
                    if (mapIndex != -1)
                    {
                        if(mapIndex <= currentMap)
                        {
                            if (selectedMap == mapIndex)
                            {
                                selectedMap = -1;
                            }
                            else
                            {
                                selectedMap = mapIndex;
                            }
                        }
                    }
                else {
                    if (backPosition.Contains(Statics.INPUT.mousePosition))
                    { 
                        // quay ve menu chinh
                        nextState = ScreenState.VEMENU;
                        
                    }
                    else if (playPosition.Contains(Statics.INPUT.mousePosition))
                    {
                        if (selectedMap == -1)
                        {
                            // khong lam gi ca
                            nextState = ScreenState.GIUNGUYEN;
                        }
                        else
                        {
                            // vao choi
                            nextState = ScreenState.SANGCHOI;
                        }
                    }
                    else {
                        selectedMap = -1;
                        nextState = ScreenState.GIUNGUYEN;
                    }

                    
                }
            // khi ma click trung map:
                // neu map ay duoc cho tu truoc: thi reset ve chua chon
                // neu map ay chua duoc cho thi set map ay duoc chon
            // neu khong trung map
                // neu trung nut quay ve: quay ve menu
                // neu trung nut choi:
                    // neu chua chon map: khong lam gi ca
                    // neu da chon map: thi khoi tao vao cho man do
                // khong trung bat ki nut nao: reset
                }
        }
        //public int selectedMap
        //{
        //    get {

        //        List<Rectangle> rectList = new List<Rectangle>();
        //        rectList.Add(new Rectangle(50, 50, 200, 180));
        //        rectList.Add(new Rectangle(300, 50, 200, 180));
        //        rectList.Add(new Rectangle(550, 50, 200, 180));
        //        rectList.Add(new Rectangle(50, 280, 200, 180));
        //        rectList.Add(new Rectangle(300, 280, 200, 180));
        //        rectList.Add(new Rectangle(550, 280, 200, 180));
        //        for (int i = 0; i < rectList.Count; i++)
        //        {
        //            if (rectList[i].Contains(Statics.INPUT.mousePosition))
        //            {
        //                return i;
        //            }
        //        }
        //        return -1;
        //    }


        //}
        int clickTrungMap
        {
            get {
                
                for (int i = 0; i < rectList.Count; i++)
                {
                    if (rectList[i].Contains(Statics.INPUT.mousePosition))
                    {
                        
                        return i;
                    }
                }
                return -1;
            
            }
        }

        Rectangle backPosition
        {
            get {
                return new Rectangle(60, 490, 95, 90);
            }
        }
        Rectangle playPosition
        {
            get
            {
                return new Rectangle(680, 495, 95, 90);
            }
        }
        

    }
}
