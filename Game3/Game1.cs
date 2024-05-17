using Game3.MVC;
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
        float speed = 4f;
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
        int[,] env;
        PlayerModel player;
        public Game1()
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
             _width = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width*0.93);
             _height = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height*0.93);

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
            env = new int[(int)(_width / 50)+2, (int)(_height * 2 / 50)+2];
            for (int i = 0; i < (int)(_width / 50)+2; i++)
            {
                for (int j = 0; j < (int)(_height / 50)+2; j++)
                {
                    env[i, j] = 0;
                }
            }
            Pillarpositions = new List<Rectangle>();
            for (int i = 0; i < 6; i++)
            {
                Pillarpositions.Add(new Rectangle(_width/10*(i+1)+400, _height -_height/10*i-100,  pillar.Width, pillar.Height));
                for(int l = 0; l< 6; l++)
                {
                    env[(int)((_width / 10 * (i + 1)+400) / 50 + l), (int)((_height - _height / 10 * i-100) / 50)] = 1;
                }
            }


            for (int i = 0; i < (int)(_width / 50)+2; i++)
            {
                for (int j = 0; j < (int)(_height / 50)+2; j++)
                {
                    System.Diagnostics.Debug.Write(env[i, j]);
                }
                System.Diagnostics.Debug.WriteLine("");
            }
            Pillarposition = new Vector2(200, _height);
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
            player = new PlayerModel();
            player.loadPlayerData();
        }

        protected void detectKey()
        {
            KeyboardState keyboardState = Keyboard.GetState(); if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (keyboardState.IsKeyDown(Keys.Left) && !TouchedRight())
            {
                direction = 3; position.X -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.Right) && !TouchedLeft())
            {
                direction = 0;
                position.X += speed;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (verticalSpeed == 0.0)
                {
                    _isJumped = true;
                    verticalSpeed = -10f;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Down) && position.Y <= _height && !TouchedTop())
            {
            }
            if (position.X < 0) position.X = 0;
            if (position.Y < 0) position.Y = 0;
            if (position.X > _width) position.X = _width;
            if (position.Y > _height) position.Y = _height;
        }

        protected void animatePlayer(GameTime gameTime, Vector2 prevPosition)
        {
            if (timer > threshold && position.X == prevPosition.X)
            {
                currentAnimationIndex = (byte)(1 + direction);
            }
            if (timer > threshold && position.X != prevPosition.X)
            {
                if (currentAnimationIndex == 1 + direction)
                {
                    if (previousAnimationIndex == direction)
                    {
                        currentAnimationIndex = (byte)(2 + direction);
                    }
                    else
                    {
                        currentAnimationIndex = direction;
                    }
                    previousAnimationIndex = currentAnimationIndex;
                }
                else
                {
                    currentAnimationIndex = (byte)(1 + direction);
                }
                
                timer = 0;
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        protected void addPhysics()
        {

            if ((position.Y <= _height - playerRect.Height/1.3  || _isJumped))
            {
                if (!TouchedTop())
                {
                    verticalSpeed += g * 0.03;
                }
                if ((!TouchedTop() || verticalSpeed <= 0))
                {
                    if (TouchedBottom())
                    {
                        if(verticalSpeed < 0) { verticalSpeed = -verticalSpeed; }
                        position.Y += (float)(verticalSpeed);
                        _isJumped = false;
                    }
                    else
                    {
                        position.Y += (float)(verticalSpeed);
                    }
                }
                if (position.Y >= _height - playerRect.Height/1.3 || TouchedTop())
                {
                    _isJumped = false;
                    verticalSpeed = 0.0;
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var prevPosition = position;
            detectKey();
            animatePlayer(gameTime, prevPosition);
            addPhysics();
            playerRect = new Rectangle((int)position.X, (int)position.Y, 48, 64);
            base.Update(gameTime);
        }



        protected bool TouchedLeft()
        {
            return env[(int)(position.X / 50) + 1, (int)(position.Y / 50)] == 1;
        }

        protected bool TouchedRight()
        {
            return env[(int)(position.X / 50) - 1, (int)(position.Y / 50)] == 1;
        }

        protected bool TouchedTop()
        {
            return env[(int)(position.X / 50), (int)(position.Y / 50)+1] == 1;
        }

        protected bool TouchedBottom()
        {
            return env[(int)(position.X / 50), (int)(position.Y / 50)] == 1;

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

public enum GameStates
{ // Just for readability
    Menu,
    Playing,
    Paused,
    GameOver
}

public class GameSession
{

    GameStates _state = GameStates.Menu;

    public void SetGameState(GameStates state)
    {

        if (state != _state)
        {
            _state = state;
            switch (state)
            {
                case GameStates.Menu:
                    // Show menu window or overlay here if not already open
                    // Pause game world time flow
                    // Start potential transitions here
                    break;
                case GameStates.Playing:
                    // Resume game world time flow
                    break;
                case GameStates.Paused:
                    // Pause game world time flow
                    // Maybe show game paused UI
                    break;
                case GameStates.GameOver:
                    // Show game over screen or overlay
                    // Pause game world time flow (not strictly)
                    // Any interaction from player leads to state e.g. GameStates.Menu
                    break;
                default:
                    break;
            }
        }
    }
}
