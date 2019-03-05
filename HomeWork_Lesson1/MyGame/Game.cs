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
        
        public static Bullet _bullet;
        public static Asteroid[] _asteroids;
        public static Planet _planet;

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

            _planet.Draw();

            foreach (BaseObject obj in _asteroids)
                obj.Draw();            

            _bullet.Draw();

            Buffer.Render();

        }

        public static void Update()
        {

            foreach (BaseObject obj in _objs)
                obj.Update();

            _planet.Update();

            foreach (Asteroid obj in _asteroids)
                obj.Update();            

            _bullet.Update();
        }

        public static void Load()
        {
            _objs = new BaseObject[30];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];
            _planet = new Planet(new Point(-199, 250), new Point(5, 0), new Size(50, 50));
            var rnd = new Random();
            for (var i = 0; i <_objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(100, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
                
            }
            
            
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {

            Update();
            Draw();

        }

    }
}
