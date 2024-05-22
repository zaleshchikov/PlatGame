using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.MVC.Screens
{
    public abstract class GameScreen
    {
        public abstract void Initialize();

        public abstract void Game(GraphicsDeviceManager _graphics);

        public abstract void LoadContent(ContentManager content, GraphicsDevice graphicsDevice);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }

}
