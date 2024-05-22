using Game3.MVC;
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
        PlayerModel player;
        EnviromentModel enviroment;
        Controller controller;
        View view;
        private GameScreen _currentScreen;

        public Game()
        {

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //view = new View();
            //view.StartGame(new GraphicsDeviceManager(this));
            _currentScreen = new LevelScreen();
            _currentScreen.Game(new GraphicsDeviceManager(this));
        }

        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _currentScreen.LoadContent(Content, GraphicsDevice);
            //_currentScreen.LoadContent(Content, GraphicsDevice);
            //enviroment = new EnviromentModel();
            //player = new PlayerModel();
            //controller = new Controller();
            //player.LoadData();
            //enviroment.LoadData();
            //enviroment.LoadPillarPositionFirstLevel();
            //controller.LoadData(player, enviroment);
            //view.LoadData(Content, player, enviroment, controller, GraphicsDevice);
            //view.SetFullscreen();
        }




        protected override void Update(GameTime gameTime)
        {
            _currentScreen.Update(gameTime);
            //var prevPosition = player.position;
            //controller.detectKey();
            //controller.animatePlayer(gameTime, prevPosition);
            //controller.addPhysics();
            //player.playerRect = new Rectangle((int)player.position.X, (int)player.position.Y, PlayerModel.spriteWidth, PlayerModel.spriteHeight);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            _currentScreen.Draw(gameTime);
            //view.DrawLevelOne(gameTime);
        }

        
    }
}


