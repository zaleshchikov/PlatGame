using Game3.MVC.MenuMVC;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game3.MVC.Screens
{
    public class MainMenuScreen : GameScreen
    {
        MenuModel menuModel;
        MenuView menuView;
        MenuController menuController;
        public override void Initialize()
        {
        }

        public override void Game(GraphicsDeviceManager _graphics)
        {
            menuView = new MenuView();
            menuView.StartGame(_graphics);
        }

        public override void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            menuModel = new MenuModel();
            menuController = new MenuController();
            menuModel.LoadData();
            menuController.LoadData(menuModel);
            menuView.LoadData(content, graphicsDevice, menuModel);
            menuView.SetFullscreen();
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            menuController.detectKey();
        }

        public override void Draw(GameTime gameTime)
        {
            menuView.DrawMenu(gameTime);
        }
    }
}
