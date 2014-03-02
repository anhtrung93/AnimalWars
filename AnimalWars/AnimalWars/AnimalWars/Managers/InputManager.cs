using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AnimalWars.Managers
{
    public class InputManager
    {
        public MouseState _oldMS;
        public MouseState _MS;
        

        public InputManager()
        {
            Statics.INPUT = this;
        }
        public void Update()
        {
            if (_MS != null)
                _oldMS = _MS;

            _MS = Mouse.GetState();
        }
        public bool isMouseClicked()
        {
            return (_oldMS.LeftButton == ButtonState.Released && _MS.LeftButton == ButtonState.Pressed);
        }
        public Point mousePosition { get { return new Point(_MS.X, _MS.Y); } }
        

    }
}
