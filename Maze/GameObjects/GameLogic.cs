namespace Maze.GameObjects
{
    /// <summary>
    /// Класс для основной логики работы игры
    /// </summary>
    internal class GameLogic
    {
        private readonly MazeGenerator mazeGenerator;
        private readonly Player player;
        private int exitX, exitY;
        private bool gameRunning;

        public GameLogic(int width, int height)
        {
            mazeGenerator = new MazeGenerator(width, height);
            player = new Player();
            gameRunning = true;
        }

        public void Start()
        {
            mazeGenerator.GenerateMaze();
            FindExitPosition();

            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (gameRunning)
            {
                DrawMaze();

                if (CheckWinCondition())
                {
                    ShowWinMessage();
                    break;
                }

                HandleInput();
            }
        }

        /// <summary>
        /// Находит позицию выхода в лабиринте
        /// </summary>
        private void FindExitPosition()
        {
            var maze = mazeGenerator.GetMaze();

            for (int x = 0; x < maze.GetLength(1); x++)
            {
                if (maze[maze.GetLength(0) - 1, x] == ' ')
                {
                    exitX = x;
                    exitY = maze.GetLength(0) - 1;
                    break;
                }
            }
        }

        /// <summary>
        /// Проверка на достижение игроком выхода
        /// </summary>
        private bool CheckWinCondition()
        {
            var playerPos = player.GetPosition();
            return playerPos.x == exitX && playerPos.y == exitY;
        }

        /// <summary>
        /// Показывает сообщение о победе
        /// </summary>
        private void ShowWinMessage()
        {
            Console.Clear();
            Console.WriteLine("Поздравляю!");
            Console.WriteLine("Вы успешно прошли лабиринт!^_^");
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
        }

        private void HandleInput()
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
            {
                gameRunning = false;
            }
            else
            {
                MovePlayer(key);
            }
        }

        /// <summary>
        /// Метод обработки кнопок движения игрока
        /// </summary>
        /// <param name="key"></param>
        private void MovePlayer(ConsoleKey key)
        {
            var maze = mazeGenerator.GetMaze();
            int newX = player.X;
            int newY = player.Y;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    newY--;
                    break;
                case ConsoleKey.DownArrow:
                    newY++;
                    break;
                case ConsoleKey.LeftArrow:
                    newX--;
                    break;
                case ConsoleKey.RightArrow:
                    newX++;
                    break;
                default:
                    return;
            }

            // проверка можно ли двигаться
            if (newX >= 0 && newX < maze.GetLength(1) &&
                newY >= 0 && newY < maze.GetLength(0) &&
                maze[newY, newX] != '#')
            {
                player.X = newX;
                player.Y = newY;
            }
        }

        /// <summary>
        /// Метод для отрисовки игровых элементов
        /// </summary>
        private void DrawMaze()
        {
            Console.Clear();
            var maze = mazeGenerator.GetMaze();
            var playerPos = player.GetPosition();

            for (int y = 0; y < maze.GetLength(0); y++)
            {
                for (int x = 0; x < maze.GetLength(1); x++)
                {
                    if (x == playerPos.x && y == playerPos.y)
                    {
                        Console.Write('♥');
                    }
                    else if (x == exitX && y == exitY)
                    {
                        Console.Write('✘');
                    }
                    else if (maze[y, x] == '#')
                    {
                        Console.Write('█');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nСтрелки - двигаться, ESC - выйти");
        }
    }
}
