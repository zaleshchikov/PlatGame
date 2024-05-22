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

namespace Game3.MVC.ScreenManager
{
    public class ScreenManager
    {
        public GameScreen currentScreen;

        public void checkingChangeScreen(GameScreen newScreen, GraphicsDeviceManager _graphicsDeviceManager, ContentManager content, GraphicsDevice graphicsDevice)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                ChangeScreen(newScreen, _graphicsDeviceManager, content, graphicsDevice);
            }

        }
        public void ChangeScreen(GameScreen newScreen, GraphicsDeviceManager _graphicsDeviceManager, ContentManager content, GraphicsDevice graphicsDevice)
        {
            if (currentScreen != null)
            {
                currentScreen.UnloadContent();
            }
            currentScreen = newScreen;
            currentScreen.Initialize();
            currentScreen.Game(_graphicsDeviceManager);
            currentScreen.LoadContent(content, graphicsDevice);
        }
    }
}
