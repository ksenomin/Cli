
namespace Maze.GameObjects
{
    /// <summary>
    /// Класс для генерации лабиринта при помощи алгоритма Prima
    /// </summary>
    internal class MazeGenerator
    {
        private readonly int width;
        private readonly int height;
        private readonly char[,] maze;
        private readonly Random random;

        public MazeGenerator(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.maze = new char[height, width];
            this.random = new Random();
        }

        public void GenerateMaze()
        {
            // Инициализация лабиринта стенами
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    maze[y, x] = '#';
                }
            }

            // Выбираем случайную стартовую точку (нечетные координаты)
            int startX = random.Next(1, width - 2);
            int startY = random.Next(1, height - 2);
            startX = startX % 2 == 0 ? startX + 1 : startX;
            startY = startY % 2 == 0 ? startY + 1 : startY;

            maze[startY, startX] = ' ';

            // Список граничных стен
            var walls = new List<(int x, int y, int fromX, int fromY)>();
            AddWalls(startX, startY, walls);

            while (walls.Count > 0)
            {
                // Выбираем случайную стену
                int randomIndex = random.Next(walls.Count);
                var (wallX, wallY, fromX, fromY) = walls[randomIndex];
                walls.RemoveAt(randomIndex);

                // Проверяем, можно ли пройти через эту стену
                if (IsValidWall(wallX, wallY))
                {
                    // Определяем клетку за стеной
                    int newX = 2 * wallX - fromX;
                    int newY = 2 * wallY - fromY;

                    if (newX >= 0 && newX < width && newY >= 0 && newY < height && maze[newY, newX] == '#')
                    {
                        // Пробиваем стену и новую клетку
                        maze[wallY, wallX] = ' ';
                        maze[newY, newX] = ' ';

                        // Добавляем стены новой клетки
                        AddWalls(newX, newY, walls);
                    }
                }
            }

            // Создаем вход и выход
            CreateEntranceAndExit();
        }

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

            return count == 1; // Стена должна граничить только с одной проходной клеткой
        }

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

        private void CreateEntranceAndExit()
        {
            // Вход сверху
            for (int x = 1; x < width - 1; x++)
            {
                if (maze[1, x] == ' ')
                {
                    maze[0, x] = ' ';
                    break;
                }
            }

            // Выход снизу
            for (int x = width - 2; x > 0; x--)
            {
                if (maze[height - 2, x] == ' ')
                {
                    maze[height - 1, x] = ' ';
                    break;
                }
            }
        }

        public char[,] GetMaze() => maze;
    }
}
