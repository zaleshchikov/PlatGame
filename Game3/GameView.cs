using Game3.MVC;
using Game3.MVC.ScreenManager;
using Game3.MVC.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Game3
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphicsDeviceManager;
        ScreenManager screenManager;
        public Game()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            screenManager = new ScreenManager();
            screenManager.currentScreen = new MainMenuScreen();
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            screenManager.currentScreen.Game(_graphicsDeviceManager);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            screenManager.LoadData(_graphicsDeviceManager, Content, GraphicsDevice);
            screenManager.currentScreen.LoadContent(Content, GraphicsDevice, screenManager);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            screenManager.currentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            screenManager.currentScreen.Draw(gameTime);
        }
    }
}


