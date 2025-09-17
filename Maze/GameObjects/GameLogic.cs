using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.GameObjects
{
    public class GameLogic
    {
        private Maze maze;
        private int playerX;
        private int playerY;
        private ConsoleView renderer;

        public GameLogic(int width, int height)
        {
            maze = new Maze(width, height);
            maze.Generate();
            renderer = new ConsoleView();
            playerX = 0;
            playerY = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            while (true)
            {
                renderer.Draw(maze, playerX, playerY);

                var key = Console.ReadKey(true).Key;

                // Обработка движения
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow ||
                    key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow)
                {
                    MovePlayer(key);
                }
                else if (key == ConsoleKey.H)
                {
                    ShowHint();
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Метод для движения игрока
        /// </summary>
        /// <param name="key"></param>
        private void MovePlayer(ConsoleKey key)
        {
            int newX = playerX;
            int newY = playerY;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (playerY > 0 && !maze.Grid[playerY, playerX].HasTopWall)
                    { newY--; }
                    break;
                case ConsoleKey.RightArrow:
                    if (playerX < maze.Width - 1 && !maze.Grid[playerY, playerX].HasRightWall)
                    { newX++; }
                    break;
                case ConsoleKey.DownArrow:
                    if (playerY < maze.Height - 1 && !maze.Grid[playerY, playerX].HasBottomWall)
                    { newY++; }
                    break;
                case ConsoleKey.LeftArrow:
                    if (playerX > 0 && !maze.Grid[playerY, playerX].HasLeftWall)
                    { newX--; }
                    break;
            }

            // Проверка выхода за границы лабиринта
            if (newX >= 0 && newX < maze.Width && newY >= 0 && newY < maze.Height)
            {
                playerX = newX;
                playerY = newY;
            }

            CheckWinCondition();
        }

        /// <summary>
        /// Функция для проверки победы
        /// </summary>
        private void CheckWinCondition()
        {
            if (playerX == maze.Width - 1 && playerY == maze.Height - 1)
            {
                Console.Clear();
                Console.WriteLine("Поздравляем! Вы прошли лабиринт! ^_^");
                Console.WriteLine("Нажмите любую клавишу чтобы выйти...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// Функция для подсказки пути
        /// </summary>
        private void ShowHint()
        {
            Console.WriteLine("Подсказка будет реализована позже!");
            Console.WriteLine("Нажмите любую клавишу чтобы продолжить...");
            Console.ReadKey();
        }
    }
}
