using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AnimalWars.Screens
{
    class LoadingScreen: Screen
    {
        public Texture2D[] textures;
        int textureIndex = 0;
        int animTimer = 300;
        double animElapsed = 0;
        public LoadingScreen(Game game) :base(game)
        {
            textures = new Texture2D[11];
            textures[0] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/0");
            textures[1] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/1");
            textures[2] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/2");
            textures[3] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/3");
            textures[4] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/4");
            textures[5] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/5");
            textures[6] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/6");
            textures[7] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/7");
            textures[8] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/8");
            textures[9] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/9");
            textures[10] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/10");
            //textures[11] = Statics.CONTENT.Load<Texture2D>("Images/Loadings/11");
        }

        public override void Update(GameTime gameTime)
        {
            animElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (animElapsed >= animTimer)
            {
                animElapsed = 0;
                textureIndex++;
                if (textureIndex > 10)
                {
                    this.Visible = false;
                    this.isActived = false;
                    this.Enabled = false;
                }  

            }

            base.Update();
        }
        public override void Draw(GameTime gameTime)
        {
            Statics.SPRITEBATCH.Begin();
            Statics.SPRITEBATCH.Draw(textures[textureIndex], Vector2.Zero, Color.White);
            Statics.SPRITEBATCH.End();
            base.Draw();
        }

    }
}
