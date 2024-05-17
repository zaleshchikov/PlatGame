using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.MVC
{
    internal class Controller
    {
        PlayerModel player;
        EnviromentModel enviroment;
        public float timer;
        public int threshold;
        public float speed;
        public double verticalSpeed;
        public double g;
        public bool _isJumped;
        public byte previousAnimationIndex;
        public byte currentAnimationIndex;

        public void LoadData(PlayerModel player, EnviromentModel enviroment)
        {
            speed = 4f;
            verticalSpeed = 0;
            g = 9.8;
            this.player = player;
            timer = 0;
            threshold = 250;
            previousAnimationIndex = 5;
            currentAnimationIndex = 4;
            _isJumped = false;
            this.enviroment = enviroment;
        }

        public void detectKey()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left) && !TouchedRight())
            {
                player.direction = 3; player.position.X -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.Right) && !TouchedLeft())
            {
                player.direction = 0;
                player.position.X += speed;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (verticalSpeed == 0.0)
                {
                    _isJumped = true;
                    verticalSpeed = -10f;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Down) && player.position.Y <= enviroment.Height && !TouchedTop())
            {
            }
            if (player.position.X < 0) player.position.X = 0;
            if (player.position.Y < 0) player.position.Y = 0;
            if (player.position.X > enviroment.Width) player.position.X = enviroment.Width;
            if (player.position.Y > enviroment.Height) player.position.Y = enviroment.Height;
        }


        public void animatePlayer(GameTime gameTime, Vector2 prevPosition)
        {
            if (timer > threshold && player.position.X == prevPosition.X)
            {
                currentAnimationIndex = (byte)(1 + player.direction);
            }
            if (timer > threshold && player.position.X != prevPosition.X)
            {
                if (currentAnimationIndex == 1 + player.direction)
                {
                    if (previousAnimationIndex == player.direction)
                    {
                        currentAnimationIndex = (byte)(2 + player.direction);
                    }
                    else
                    {
                        currentAnimationIndex = player.direction;
                    }
                    previousAnimationIndex = currentAnimationIndex;
                }
                else
                {
                    currentAnimationIndex = (byte)(1 + player.direction);
                }

                timer = 0;
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public void addPhysics()
        {

            if ((player.position.Y <= enviroment.Height - player.playerRect.Height / 1.3 || _isJumped))
            {
                if (!TouchedTop())
                {
                    verticalSpeed += g * 0.03;
                }
                if ((!TouchedTop() || verticalSpeed <= 0))
                {
                    if (TouchedBottom())
                    {
                        if (verticalSpeed < 0) { verticalSpeed = -verticalSpeed; }
                        player.position.Y += (float)(verticalSpeed);
                        _isJumped = false;
                    }
                    else
                    {
                        player.position.Y += (float)(verticalSpeed);
                    }
                }
                if (player.position.Y >= enviroment.Height - player.playerRect.Height / 1.3 || TouchedTop())
                {
                    _isJumped = false;
                    verticalSpeed = 0.0;
                }
            }
        }



        protected bool TouchedLeft()
        {
            return enviroment.env[(int)(player.position.X / 50) + 1, (int)(player.position.Y / 50)] == 1;
        }

        protected bool TouchedRight()
        {
            return enviroment.env[(int)(player.position.X / 50) - 1, (int)(player.position.Y / 50)] == 1;
        }

        protected bool TouchedTop()
        {
            return enviroment.env[(int)(player.position.X / 50), (int)(player.position.Y / 50) + 1] == 1;
        }

        protected bool TouchedBottom()
        {
            return enviroment.env[(int)(player.position.X / 50), (int)(player.position.Y / 50)] == 1;

        }
    }
}
