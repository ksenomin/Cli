using System.Text;
using Maze.GameObjects;

namespace Maze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Добро пожаловать в игру Лабиринт!");
            Console.WriteLine("【Генератор лабиринта с использованием алгоритма Прима】");
            Console.WriteLine();
            Console.WriteLine("Введите размеры лабиринта (лучше нечетные числа):");
            Console.WriteLine();
            Console.Write("➤ Ширина: ");
            int width = int.Parse(Console.ReadLine());

            Console.Write("➤ Высота: ");
            int height = int.Parse(Console.ReadLine());

            // нечетные размеры для нормальной работы алгоритма
            if (width % 2 == 0)
            {
                width++;
            }

            if (height % 2 == 0)
            {
                height++;
            }

            var game = new GameLogic(width, height);
            game.Start();

            Console.ReadKey();
        }
    }
}
