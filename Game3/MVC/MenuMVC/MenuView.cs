using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Game3.MVC.MenuMVC
{
    public class MenuView : Microsoft.Xna.Framework.Game
    {
        MenuModel menuModel;

        public void StartGame(GraphicsDeviceManager _graphics)
        {
            this._graphics = _graphics;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public void SetFullscreen()
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        Texture2D menuBack;
        Texture2D menuText;
        Texture2D button;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;


        public void LoadData(ContentManager content, GraphicsDevice graphicsDevice, MenuModel menuModel)
        {
            this.menuModel = menuModel;
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(graphicsDevice);
            menuBack = content.Load<Texture2D>("b3");
            menuText = content.Load<Texture2D>("menu_text");
            button = content.Load<Texture2D>("play");
        }

        public void DrawMenu(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(menuBack, menuModel.BackgroundPosition, Color.White);
            _spriteBatch.Draw(menuText, menuModel.TextPosition, Color.White);
            _spriteBatch.Draw(button, menuModel.StartButtonPosition, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
