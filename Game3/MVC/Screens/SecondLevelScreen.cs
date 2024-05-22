using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.MVC.Screens
{
    public class SecondLevelScreen : GameScreen
    {
        PlayerModel player;
        EnviromentModel enviroment;
        Controller controller;
        View view;
        public override void Initialize()
        {

        }

        public override void Game(GraphicsDeviceManager _graphics)
        {
            view = new View();
            view.StartGame(_graphics);
        }

        public override void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            enviroment = new EnviromentModel();
            player = new PlayerModel();
            controller = new Controller();
            player.LoadData();
            enviroment.LoadData();
            enviroment.LoadPillarPositionSecondLevel();
            controller.LoadData(player, enviroment);
            view.LoadData(content, player, enviroment, controller, graphicsDevice);
            view.SetFullscreen();
        }

        public override void UnloadContent()
        {
            // Разгрузите контент для игрового экрана
        }

        public override void Update(GameTime gameTime)
        {
            var prevPosition = player.position;
            controller.detectKey();
            controller.animatePlayer(gameTime, prevPosition);
            controller.addPhysics();
            player.playerRect = new Rectangle((int)player.position.X, (int)player.position.Y, PlayerModel.spriteWidth, PlayerModel.spriteHeight);
        }

        public override void Draw(GameTime gameTime)
        {
            view.DrawLevelOne(gameTime);
        }
    }
}
