﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.MVC
{
    internal class PlayerModel
    {
        Vector2 position;
        byte direction;
        Rectangle[] sourceRectangles;
        Rectangle playerRect;
        const int spriteHeight = 64;
        const int spriteWidth = 48;
        const int k = 1;

        public void loadPlayerData()
        {
            sourceRectangles = new Rectangle[8];
            sourceRectangles[0] = new Rectangle(0 * k * spriteWidth, k * spriteHeight,  k * spriteWidth, k * spriteHeight);
            sourceRectangles[1] = new Rectangle(1 * k * spriteWidth, k * spriteHeight,  k * spriteWidth, k * spriteHeight);
            sourceRectangles[2] = new Rectangle(2 * k * spriteWidth, k * spriteHeight,  k * spriteWidth, k * spriteHeight);
            sourceRectangles[3] = new Rectangle(0 * k * spriteWidth,  3 * k * spriteHeight,  k * spriteWidth, k * spriteHeight);
            sourceRectangles[4] = new Rectangle(1 * k * spriteWidth, 3 * k * spriteHeight, k * spriteWidth, k * spriteHeight);
            sourceRectangles[5] = new Rectangle(2 * k * spriteWidth, 3 * k * spriteHeight, k * spriteWidth, k * spriteHeight);
            position = new Vector2(100, 100);
            direction = 0;
            playerRect = new Rectangle((int)position.X, (int)position.Y, 48, 64);
        }
    }
}
