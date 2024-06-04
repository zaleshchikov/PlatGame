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
        int currentScreenIndex = 0;
        GraphicsDeviceManager _graphicsDeviceManager;
        ContentManager _content;
        GraphicsDevice _graphicsDevice;

        public void LoadData(GraphicsDeviceManager graphicsDeviceManager, ContentManager content, GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _content = content;
            _graphicsDeviceManager = graphicsDeviceManager;
        }

        List<GameScreen> screens = new List<GameScreen>() { new MainMenuScreen(), new FirstLevelScreen(), new SecondLevelScreen()};

        public void ChangeScreen(GameScreen newScreen)
        {
            if (currentScreen != null)
            {
                currentScreen.UnloadContent();
            }

            currentScreen = newScreen;
            currentScreen.Initialize();
            currentScreen.Game(_graphicsDeviceManager);
            currentScreen.LoadContent(_content, _graphicsDevice, this);
        }
    }
}
