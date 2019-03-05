using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Калмыков

// 1 Переделать виртуальный метод Update в BaseObject в абстрактный и реализовать его в наследниках.
// 2 Сделать так, чтобы при столкновении пули с астероидом они регенерировались в разных концах экрана.
// 3 Сделать проверку на задание размера экрана в классе Game.Если высота или ширина(Width, Height) больше 1000 или принимает отрицательное значение, 
//выбросить исключение ArgumentOutOfRangeException().


namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {

            Form form = new Form()
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height
            };
            init:
            try
            {
                
                Game.Init(form);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка " + e + ". Устанавливаем значения по умолчанию.");

                form.Height = 999;
                form.Width = 999;
                Game.Init(form);
                goto init;
            }
                
            form.Show();
            Game.Load();
            Game.Draw();
            Application.Run(form);

        }
    }
}
