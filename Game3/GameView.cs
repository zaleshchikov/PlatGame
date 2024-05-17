using Game3.MVC;
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
        private GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        Texture2D charaset;
        Texture2D pillar;
        Texture2D background;
        PlayerModel player;
        EnviromentModel enviroment;
        Controller controller;
        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        private void SetFullscreen()
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
            charaset = Content.Load < Texture2D> ("charaset");
            pillar = Content.Load<Texture2D>("new_pillar_2");
            background = Content.Load<Texture2D>("back_1");
            SetFullscreen();
            enviroment = new EnviromentModel();
            player = new PlayerModel();
            controller = new Controller();
            player.LoadData();
            enviroment.LoadData();
            enviroment.LoadPillarPositionFirstLevel();
            controller.LoadData(player, enviroment);
        }




        protected override void Update(GameTime gameTime)
        {
            var prevPosition = player.position;
            controller.detectKey();
            controller.animatePlayer(gameTime, prevPosition);
            controller.addPhysics();
            player.playerRect = new Rectangle((int)player.position.X, (int)player.position.Y, PlayerModel.spriteWidth, PlayerModel.spriteHeight);
            base.Update(gameTime);
        }





        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            DrawBack();
            DrawPlayer();
            DrawPillars();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawPlayer()
        {
            _spriteBatch.Draw(charaset, player.position, player.sourceRectangles[controller.currentAnimationIndex], Color.White);
        }

        public void DrawBack()
        {
            _spriteBatch.Draw(background, enviroment.BackgroundPosition, Color.White);
        }

        public void DrawPillars()
        {
            foreach (var pos in enviroment.Pillarpositions)
            {
                _spriteBatch.Draw(pillar, pos, Color.White);
            }
        }
    }
}


