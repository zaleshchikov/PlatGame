using Game3.MVC.ScreenManager;
using Game3.MVC.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Game3.MVC.MenuMVC
{
    public class MenuController
    {
        MenuModel menuModel;
        ScreenManager.ScreenManager screenManager;


        public void LoadData(MenuModel menuModel, ScreenManager.ScreenManager screenManager)
        {
            this.menuModel = menuModel;
            this.screenManager = screenManager;

        }

        public void detectKey()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                screenManager.ChangeScreen(new FirstLevelScreen());
            }
        }
    }
}
