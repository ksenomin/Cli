using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.GameObjects
{
    public class Maze
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Cell[,] Grid { get; private set; }

        public Maze(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new Cell[height, width];
            InitializeGrid();
        }

        /// <summary>
        /// Метод для подготовки сетки лабиринта перед отрисовкой
        /// </summary>
        private void InitializeGrid()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Grid[y, x] = new Cell(x, y);
                }
            }
        }

        /// <summary>
        /// Метод для вызова генерации лабиринта
        /// </summary>
        public void Generate()
        {
            GenerateSidewinder();
        }

        /// <summary>
        /// Алгоритм генерации лабиринта Sidewinder
        /// </summary>
        public void GenerateSidewinder()
        {
            Random random = new Random();

            // Первая строка
            for (int x = 0; x < Width - 1; x++)
            {
                Grid[0, x].HasRightWall = false;
                Grid[0, x + 1].HasLeftWall = false;
            }

            // Остальные строки
            for (int y = 1; y < Height; y++)
            {
                int runStart = 0;

                for (int x = 0; x < Width; x++)
                {
                    // 50 на 50 что дальше будет провод вправо
                    if (x < Width - 1 && random.Next(2) == 0)
                    {
                        // Проход вправо
                        Grid[y, x].HasRightWall = false;
                        Grid[y, x + 1].HasLeftWall = false;
                    }
                    else
                    {
                        // Проход вниз
                        int randomX = random.Next(runStart, x + 1);
                        Grid[y - 1, randomX].HasBottomWall = false;
                        Grid[y, randomX].HasTopWall = false;

                        runStart = x + 1; // Переход на новый коридор
                    }
                }
            }

            CreateEntranceAndExit();
        }

        /// <summary>
        /// Метод для обозначения входа и выхода из лабиринта
        /// </summary>
        private void CreateEntranceAndExit()
        {
            // Вход
            Grid[0, 0].HasLeftWall = false;

            // Выход
            Grid[Height - 1, Width - 1].HasRightWall = false;
        }
    }
}
