using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Game3.MVC
{
    internal class View : Microsoft.Xna.Framework.Game
    {
        Texture2D charaset;
        Texture2D pillar;
        Texture2D background;
        private GraphicsDeviceManager _graphics;
        PlayerModel player;
        EnvironmentViewData enviroment;
        Controller controller;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;

        public void SetFullscreen()
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }
        public void LoadData(ContentManager content, PlayerModel player,
        EnvironmentViewData enviroment,
        Controller controller, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(graphicsDevice);
            this._spriteBatch = _spriteBatch;
            charaset = content.Load<Texture2D>("charaset");
            pillar = content.Load<Texture2D>("new_pillar_2");
            background = content.Load<Texture2D>("back_1");
            this.player = player;
            this.enviroment = enviroment;
            this.controller = controller;
        }

        public void StartGame(GraphicsDeviceManager _graphics)
        {
            this._graphics = _graphics;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void DrawLevelOne(GameTime gameTime)
        {
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
            foreach (var pos in enviroment.PillarPositions)
            {
                _spriteBatch.Draw(pillar, pos, Color.White);
            }
        }
    }
}
