using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.MVC
{
    internal class EnvironmentViewData
    {
        public int[,] env;
        public List<Rectangle> PillarPositions;
        int _height;
        int _width;
        public Vector2 BackgroundPosition;
        public int Height
        {
            get => _height;
        }

        public int Width
        {
            get => _width;
        }

        const int tileSize = 50;
        const int pillarWidth = 300;
        const int PillarHeight = 50;
        const double kScreen = 0.93;
        const int errorRate = 2;
        const int tileInPillar = 6;
        const int firstLevelPillarCount = 6;
        const int secondLevelLoopCount = 6;
        const int secondLevelPillarCount = 6;
        const int levelStartX = 400;
        const int levelStartY = 100; 
        const int distanceK = 10;
        const int doorHeight = 150;

        public void LoadData()
        {
            _width = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * kScreen);
            _height = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * kScreen);
            PillarPositions = new List<Rectangle>();
            env = new int[(int)(_width / tileSize) + errorRate, (int)(_height / tileSize) + errorRate];
            BackgroundPosition = new Vector2(0, 0);
        }

        public void LoadPillarPositionFirstLevel()
        {
            for (int i = 0; i < firstLevelPillarCount; i++)
            {
                PillarPositions.Add(new Rectangle(_width / distanceK * (i + 1) + levelStartX, _height - _height / distanceK * i - levelStartY, pillarWidth, PillarHeight));
                for (int l = 0; l < tileInPillar; l++)
                {
                    env[(int)((_width / distanceK * (i + 1) + levelStartX) / tileSize + l), (int)((_height - _height / distanceK * i - levelStartY) / tileSize)] = 1;
                    if(i == 5)
                    {
                        for (int k = 1; k <= (int)(doorHeight/tileSize); k++)
                        {
                            env[(int)((_width / distanceK * (i + 1) + levelStartX) / tileSize + l), (int)((_height - _height / distanceK * i - levelStartY) / tileSize-k)] = 2;
                        }
                    }
                }
            }
        }

        public void LoadPillarPositionSecondLevel()
        {

            for (int s = 0; s < secondLevelLoopCount; s++)
            {
                for (int i = 0; i < secondLevelPillarCount; i++)
                {
                    PillarPositions.Add(new Rectangle(_width / distanceK * (s + 1) - (i % 2 == 0 ? 0 : pillarWidth) + levelStartX, _height - _height / distanceK * i - levelStartY - tileSize * i, pillarWidth, PillarHeight));
                    for (int l = 0; l < tileInPillar; l++)
                    {
                        env[(int)((_width / distanceK * (s + 1) + levelStartX - (i % 2 == 0 ? 0 : pillarWidth)) / tileSize + l), (int)((_height - _height / distanceK * i - distanceK - tileSize * i - tileSize) / tileSize)] = 1;
                    }
                }
            }
        }

        public void checkEnviroment()
        {
            for (int i = 0; i < (int)(_width / tileSize) + errorRate; i++)
            {
                for (int j = 0; j < (int)(_height / tileSize) + errorRate; j++)
                {
                    System.Diagnostics.Debug.Write(env[i, j]);
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }

    }
}
