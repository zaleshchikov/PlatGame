using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.MVC.MenuMVC
{
    public class MenuModel
    {
        public Vector2 BackgroundPosition;
        public Vector2 StartButtonPosition;
        public Vector2 TextPosition;
        int _height;
        int _width;




        public void LoadData()
        {
            _width = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.93);
            _height = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.93);
            BackgroundPosition = new Vector2(0, 0);
            StartButtonPosition = new Vector2(_width/2, _height/2);
            TextPosition = new Vector2(_width / 5, _height / 3 * 2);
            System.Diagnostics.Debug.WriteLine(StartButtonPosition.X);
        }

    }
}
