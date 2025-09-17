using System.Text;
using Maze.GameObjects;

namespace Maze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Генератор лабиринта с использованием алгоритма Прима");
            Console.WriteLine("Введите размеры лабиринта (рекомендуется нечетные числа):");

            Console.Write("Ширина: ");
            int width = int.Parse(Console.ReadLine());

            Console.Write("Высота: ");
            int height = int.Parse(Console.ReadLine());

            // Делаем размеры нечетными для правильной работы алгоритма
            if (width % 2 == 0)
            {
                width++;
            }

            if (height % 2 == 0)
            {
                height++;
            }

            // Создаем и запускаем игру
            var game = new GameLogic(width, height);
            game.Start();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
