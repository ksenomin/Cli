namespace Maze.GameObjects
{
    /// <summary>
    /// Класс для генерации лабиринта при помощи алгоритма Прима
    /// </summary>
    internal class MazeGenerator
    {
        private readonly int width;
        private readonly int height;
        private readonly char[,] maze;
        private readonly Random random;

        /// <summary>
        /// Инициализирует новый экземпляр генератора лабиринта
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public MazeGenerator(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.maze = new char[height, width];
            this.random = new Random();
        }

        /// <summary>
        /// Генерация лабиринта
        /// </summary>
        public void GenerateMaze()
        {
            // инициализация лабиринта стенами-заглушками
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    maze[y, x] = '#';
                }
            }

            int startX = random.Next(1, width - 2);
            int startY = random.Next(1, height - 2);
            startX = startX % 2 == 0 ? startX + 1 : startX;
            startY = startY % 2 == 0 ? startY + 1 : startY;

            maze[startY, startX] = ' ';

            // список граничных стен
            var walls = new List<(int x, int y, int fromX, int fromY)>();
            AddWalls(startX, startY, walls);

            while (walls.Count > 0)
            {
                int randomIndex = random.Next(walls.Count);
                var (wallX, wallY, fromX, fromY) = walls[randomIndex];
                walls.RemoveAt(randomIndex);

                if (IsValidWall(wallX, wallY))
                {
                    // определение клетки за стеной
                    int newX = 2 * wallX - fromX;
                    int newY = 2 * wallY - fromY;

                    if (newX >= 0 && newX < width && newY >= 0 && newY < height && maze[newY, newX] == '#')
                    {
                        // пробивает стену и новую клетку
                        maze[wallY, wallX] = ' ';
                        maze[newY, newX] = ' ';

                        AddWalls(newX, newY, walls);
                    }
                }
            }

            CreateEntranceAndExit();
        }

        /// <summary>
        /// Проверка - является ли стена допустимой для пробивания
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true если стена может быть пробита, иначе false</returns>
        private bool IsValidWall(int x, int y)
        {
            if (x <= 0 || x >= width - 1 || y <= 0 || y >= height - 1)
            {
                return false;
            }

            int count = 0;
            if (maze[y - 1, x] == ' ')
            {
                count++;
            }

            if (maze[y + 1, x] == ' ')
            {
                count++;
            }

            if (maze[y, x - 1] == ' ')
            {
                count++;
            }

            if (maze[y, x + 1] == ' ')
            {
                count++;
            }

            return count == 1; // стена может граничить только с одной проходной клеткой
        }

        /// <summary>
        /// Добавляет границы стены вокруг клетки в список
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="walls"></param>
        private void AddWalls(int x, int y, List<(int, int, int, int)> walls)
        {
            if (x > 1)
            {
                walls.Add((x - 1, y, x, y));
            }

            if (x < width - 2)
            {
                walls.Add((x + 1, y, x, y));
            }

            if (y > 1)
            {
                walls.Add((x, y - 1, x, y));
            }

            if (y < height - 2)
            {
                walls.Add((x, y + 1, x, y));
            }
        }

        /// <summary>
        /// Создает вход и выход в лабиринте
        /// </summary>
        private void CreateEntranceAndExit()
        {
            // вход сверху
            for (int x = 1; x < width - 1; x++)
            {
                if (maze[1, x] == ' ')
                {
                    maze[0, x] = ' ';
                    break;
                }
            }

            // выход снизу
            for (int x = width - 2; x > 0; x--)
            {
                if (maze[height - 2, x] == ' ')
                {
                    maze[height - 1, x] = ' ';
                    break;
                }
            }
        }

        /// <summary>
        /// Возвращает сгенерированный лабиринт в виде двумерного массива
        /// </summary>
        /// <returns>Двумерный массив символов</returns>
        public char[,] GetMaze() => maze;
    }
}
