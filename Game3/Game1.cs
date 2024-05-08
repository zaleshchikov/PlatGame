using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Game3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        Texture2D charaset;
        Texture2D background;
        Vector2 position;
        float timer;
        byte direction;
        int threshold;
        float speed = 2f;
        Rectangle[] sourceRectangles;
        double verticalSpeed = 0;
        double g = 9.8;
        int _width;
        int _height;
        bool _isJumped;
        byte previousAnimationIndex;
        byte currentAnimationIndex;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        private void SetFullscreen()
        {
             _width = Window.ClientBounds.Width;
             _height = Window.ClientBounds.Height;

            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
            charaset = Content.Load < Texture2D> ("charaset");
            background = Content.Load<Texture2D>("back_1");

            SetFullscreen();
            timer = 0;
            direction = 0;
            threshold = 250;
            _isJumped = false;
            sourceRectangles = new Rectangle[8];
            int k = 1;
            sourceRectangles[0] = new Rectangle(0*k, 64 * k, 48 * k, 64 * k);
            sourceRectangles[1] = new Rectangle(48 * k, 64 * k, 48 * k, 64 * k);
            sourceRectangles[2] = new Rectangle(96 * k, 64 * k, 48 * k, 64 * k);
            sourceRectangles[3] = new Rectangle(0 * k, 192 * k, 48 * k, 64 * k);
            sourceRectangles[4] = new Rectangle(48 * k, 192 * k, 48 * k, 64 * k);
            sourceRectangles[5] = new Rectangle(96 * k, 192 * k, 48 * k, 64 * k);
            previousAnimationIndex = 5;
            currentAnimationIndex = 4;
            position = new Vector2(100, 100);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var prevPosition = position;
            if (keyboardState.IsKeyDown(Keys.Left)) {
                direction = 3;
                position.X -= speed; }
            if (keyboardState.IsKeyDown(Keys.Right)) {
                direction = 0;
                 position.X += speed; }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (verticalSpeed == 0.0)
                {
                    _isJumped = true;
                    verticalSpeed = -5f;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Down) && position.Y <= _height * 2)
                position.Y += speed*3;


            if (position.Y <= _height * 2 || _isJumped)
            {
                verticalSpeed += g * 0.01;
                position.Y += (float)(verticalSpeed);
                if (position.Y >= _height * 2)
                {
                    _isJumped = false;
                    verticalSpeed = 0.0;
                }
            }
            if (timer > threshold && position.X == prevPosition.X)
            {
                currentAnimationIndex = (byte)(1 + direction);
                
            }

            if (timer > threshold && position.X != prevPosition.X)
            {
                // If Alex is in the middle sprite of the animation.
                if (currentAnimationIndex == 1 + direction)
                {
                    // If the previous animation was the left-side sprite, then the next animation should be the right-side sprite.
                    if (previousAnimationIndex == direction)
                    {
                        currentAnimationIndex = (byte)(2 + direction);
                    }
                    else

                    // If not, then the next animation should be the left-side sprite.
                    {
                        currentAnimationIndex = direction;
                    }

                    // Track the animation.
                    previousAnimationIndex = currentAnimationIndex;
                }
                // If Alex was not in the middle sprite of the animation, he should return to the middle sprite.
                else
                {
                    currentAnimationIndex = (byte)(1 + direction);
                }

                // Reset the timer.
                timer = 0;
            }
            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            Rectangle sourceRectangle = new Rectangle(0, 0, 48, 64);
            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(charaset, position, sourceRectangles[currentAnimationIndex], Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
