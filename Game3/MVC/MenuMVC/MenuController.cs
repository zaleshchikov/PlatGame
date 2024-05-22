using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.MVC.MenuMVC
{
    public class MenuController
    {
        MenuModel menuModel;

        public void LoadData(MenuModel menuModel)
        {
            this.menuModel = menuModel;
        }

        public void detectKey()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            //System.Diagnostics.Debug.WriteLine(mouseState.X);
            if (mouseState.X >= menuModel.StartButtonPosition.X || 
               //mouseState.X <= menuModel.StartButtonPosition.X + menuModel.StartButtonWidth && 
               //mouseState.Y >= menuModel.StartButtonPosition.Y && 
               //mouseState.Y <= menuModel.StartButtonPosition.Y || 
               mouseState.LeftButton == ButtonState.Pressed)
            {
                System.Diagnostics.Debug.WriteLine("Mouse clicked");
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                System.Diagnostics.Debug.WriteLine("Enter clicked");
            }
        }
    }
}
