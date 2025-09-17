using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.GameObjects
{
    public class ConsoleView
    {

        public void Draw(Maze maze, int playerX, int playerY)
        {
            Console.Clear();
            Console.WriteLine("Лабиринт! Стрелки - движение, H - подсказка, ESC - выход\n");

            // Верхняя граница всего лабиринта
            Console.Write("╔");
            for (int x = 0; x < maze.Width; x++)
            {
                Console.Write(maze.Grid[0, x].HasTopWall ? "══" : "  ");
                if (x < maze.Width - 1)
                {
                    Console.Write("╦");
                }
            }
            Console.WriteLine("╗");

            for (int y = 0; y < maze.Height; y++)
            {
                // Содержимое строки с игроком
                Console.Write(y == 0 ? "║" : "║"); // Левая граница

                for (int x = 0; x < maze.Width; x++)
                {
                    // Игрок или пустота
                    if (x == playerX && y == playerY)
                    {
                        Console.Write("☺");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                    // Правая стенка или проход
                    Console.Write(maze.Grid[y, x].HasRightWall ? "║" : " ");
                }
                Console.WriteLine();

                // Нижние границы (кроме последней строки)
                if (y < maze.Height - 1)
                {
                    Console.Write("╠");
                    for (int x = 0; x < maze.Width; x++)
                    {
                        Console.Write(maze.Grid[y, x].HasBottomWall ? "══" : "  ");
                        if (x < maze.Width - 1)
                        {
                            // Уголок или перекресток
                            if (maze.Grid[y, x].HasRightWall && maze.Grid[y, x + 1].HasLeftWall)
                            {
                                Console.Write("╬");
                            }
                            else if (maze.Grid[y, x].HasRightWall)
                            {
                                Console.Write("╩");
                            }
                            else if (maze.Grid[y, x + 1].HasLeftWall)
                            {
                                Console.Write("╣");
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                        }
                    }
                    Console.WriteLine("╣");
                }
            }

            // Нижняя граница всего лабиринта
            Console.Write("╚");
            for (int x = 0; x < maze.Width; x++)
            {
                Console.Write(maze.Grid[maze.Height - 1, x].HasBottomWall ? "══" : "  ");
                if (x < maze.Width - 1)
                {
                    Console.Write("╩");
                }
            }
            Console.WriteLine("╝");

        }

    }
}
