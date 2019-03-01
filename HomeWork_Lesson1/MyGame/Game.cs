using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    static class Game
    {
        public static BaseObject[] _objs;

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {

        }

        public static void Init(Form form)
        {

            Graphics g;

            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            Timer timer = new Timer();
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        public static void Draw()
        {

            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.FillEllipse(Brushes.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.Clear(Color.Black);

            foreach (BaseObject obj in _objs)
                obj.Draw();

            Buffer.Render();

        }

        public static void Update()
        {

            foreach (BaseObject obj in _objs)
                obj.Update();

        }

        public static void Load()
        {
            int t = 0;
            _objs = new BaseObject[30];
            for (int i = 0; i < _objs.Length / 2; i++)
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i, -i), new Size(10, 10));
            for (int i = _objs.Length / 2; i < _objs.Length - 1; i++)
            {
                t++;
                _objs[i] = new Star(new Point(600, t * (Height / 15)), new Point(-i, 0), new Size(5, 5));
            }
            _objs[29] = new Planet(new Point(600, 250), new Point(5,0), new Size(50, 50));
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {

            Update();
            Draw();

        }

    }
}
