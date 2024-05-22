using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.MVC
{
    internal class EnviromentModel
    {
        public int[,] env;
        public List<Rectangle> Pillarpositions;
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

        const int pillarWidth = 300;
        const int PillarHeight = 50;
        public void LoadData()
        {
            _width = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.93);
            _height = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.93);
            Pillarpositions = new List<Rectangle>();
            env = new int[(int)(_width / 50) + 2, (int)(_height * 2 / 50) + 2];
            BackgroundPosition = new Vector2(0, 0);
        }

        public void LoadPillarPositionFirstLevel()
        {
            for (int i = 0; i < 6; i++)
            {
                Pillarpositions.Add(new Rectangle(_width / 10 * (i + 1) + 400, _height - _height / 10 * i - 100, pillarWidth, PillarHeight));
                for (int l = 0; l < 6; l++)
                {
                    env[(int)((_width / 10 * (i + 1) + 400) / 50 + l), (int)((_height - _height / 10 * i - 100) / 50)] = 1;
                }
            }
        }

        public void LoadPillarPositionSecondLevel()
        {

            for (int s = 0; s < 6; s++)
            {
                for (int i = 0; i < 6; i++)
                {
                    Pillarpositions.Add(new Rectangle(_width / 10 * (s+1) - (i % 2 == 0 ? 0 : pillarWidth) + 400, _height - _height / 10 * i - 100 - 50 * i, pillarWidth, PillarHeight));
                    for (int l = 0; l < 4; l++)
                    {
                        env[(int)((_width / 10 * (s+1) + 400 - (i % 2 == 0 ? 0 : pillarWidth)) / 50 + l), (int)((_height - _height / 10 * i - 10 - 50 * i - 50) / 50)] = 1;
                    }
                }
            }
          }

        public void LoadPillarPositionThirdLevel()
        {

            for (int s = 0; s < 3; s++)
            {
                for (int i = 0; i < 6; i++)
                {
                    Pillarpositions.Add(new Rectangle(_width / 10 * (i + 1) + 400, _height - _height / 10 * i*s - 100, pillarWidth, PillarHeight));
                    for (int l = 0; l < 6; l++)
                    {
                        env[(int)((_width / 10 * (i + 1) + 400) / 50 + l), (int)((_height - _height / 10 * i*s - 100) / 50)] = 1;
                    }
                }
            }
        }

        public void checkEnviroment()
        {
            for (int i = 0; i < (int)(_width / 50) + 2; i++)
            {
                for (int j = 0; j < (int)(_height / 50) + 2; j++)
                {
                    System.Diagnostics.Debug.Write(env[i, j]);
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }

    }
}
