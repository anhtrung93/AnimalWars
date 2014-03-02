using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AnimalWars.Screens
{
    public class IntroScreen: Screen
    {
        Texture2D Texture;
        public IntroScreen(Game game): base(game)
        {
            this.game = game;
            Texture = Statics.CONTENT.Load<Texture2D>("Images/Intro/intro");
            
        }
        public override void Update(GameTime gameTime)
        {
            if (Statics.INPUT.isMouseClicked() && skipPosition.Contains(Statics.INPUT.mousePosition))
            { 
                isActived = false;
                this.Visible = false;
                this.Enabled = false;
            }
            base.Update();
        }
        public override void Draw(GameTime gameTime)
        {
            Statics.SPRITEBATCH.Begin();
            Statics.SPRITEBATCH.Draw(Texture, Vector2.Zero, Color.White);
            Statics.SPRITEBATCH.Draw(Statics.PIXEL, skipPosition, new Color(0.1f ,0.2f, 0, 0.3f));
            Statics.SPRITEBATCH.End();
            base.Draw();
        }
        public Rectangle skipPosition {
            get {
                return new Rectangle(670, 516, 63, 50);
            }
        }
    }
}
