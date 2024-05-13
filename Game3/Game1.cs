using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Game3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        Texture2D charaset;
        Texture2D pillar;
        Texture2D background;
        Vector2 position;
        Vector2 Pillarposition;
        float timer;
        byte direction;
        int threshold;
        float speed = 2f;
        List<Rectangle> Pillarpositions;
        Rectangle[] sourceRectangles;
        Rectangle playerRect;
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
            pillar = Content.Load<Texture2D>("Pillar_01");
            background = Content.Load<Texture2D>("back_1");
            SetFullscreen();
            Pillarpositions = new List<Rectangle>();
            for (int i = 0; i < 6; i++)
            {
                Pillarpositions.Add(new Rectangle(_width/3*(i+1), _height * 2-_height/6*i,  pillar.Width, pillar.Height));
            }
            Pillarposition = new Vector2(200, _height * 2);
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
            if (keyboardState.IsKeyDown(Keys.Left) &&!TouchedRight()) {
                direction = 3;
                position.X -= speed; }
            if (keyboardState.IsKeyDown(Keys.Right) && !TouchedLeft()) {
                direction = 0;
                 position.X += speed; }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (verticalSpeed == 0.0)
                {
                    _isJumped = true;
                    verticalSpeed = -2f;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Down) && position.Y <= _height * 2 &&!TouchedTop())
            {
                //position.Y += speed * 3;
            }

            if ((position.Y <= _height * 2 || _isJumped))
            {
                if(TouchedTop())System.Diagnostics.Debug.WriteLine(TouchedTop());
                if (!TouchedTop())
                {
                    verticalSpeed += g * 0.001;
                }
                if ((!TouchedTop() || verticalSpeed <= 0)) { position.Y += (float)(verticalSpeed); }
                if (position.Y >= _height * 2 || TouchedTop())
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
                if (position.X < 0)
                    position.X = 0;
                if (position.Y < 0)
                    position.Y = 0;
                if (position.X > _width * 2.5)
                    position.X = _width * 2.5f;
                if (position.Y > _height * 2)
                    position.Y = _height * 2;
                timer = 0;
            }
            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }


            playerRect = new Rectangle((int)position.X, (int)position.Y, 48, 64);
            base.Update(gameTime);
        }



        protected bool TouchedLeft()
        {
            foreach (var pillarRect in Pillarpositions)
            {
                if(playerRect.Right == pillarRect.Left && playerRect.Bottom >= pillarRect.Top && playerRect.Bottom <= pillarRect.Bottom)
                {
                    return true;
                }
            }
            return false;
        }

        protected bool TouchedRight()
        {
            foreach (var pillarRect in Pillarpositions)
            {
                if (playerRect.Left == pillarRect.Right && playerRect.Bottom >= pillarRect.Top && playerRect.Bottom <= pillarRect.Bottom)
                {
                    return true;
                }
            }
            return false;

        }

        protected bool TouchedTop()
        {
            foreach (var pillarRect in Pillarpositions)
            {
                if (playerRect.Bottom == pillarRect.Top && playerRect.Right >= pillarRect.Left && playerRect.Left <= pillarRect.Right)
                {
                    return true;
                }
            }
            return false;
        }

        protected bool TouchedBottom()
        {
            foreach (var pillarRect in Pillarpositions)
            {
                if (playerRect.Top == pillarRect.Bottom && playerRect.Right >= pillarRect.Left && playerRect.Left <= pillarRect.Right)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            Rectangle sourceRectangle = new Rectangle(0, 0, 48, 64);
            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(charaset, position, sourceRectangles[currentAnimationIndex], Color.White);
            foreach (var pos in Pillarpositions)
            {
                _spriteBatch.Draw(pillar, pos, Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
