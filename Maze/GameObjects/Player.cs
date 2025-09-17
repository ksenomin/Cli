namespace Maze.GameObjects
{
    /// <summary>
    /// Класс Игрок
    /// </summary>
    internal class Player
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Player()
        {
            X = 1;
            Y = 0;
        }

        /// <summary>
        /// Метод, возвращающий текущую позицию игрока
        /// </summary>
        /// <returns></returns>
        public (int x, int y) GetPosition()
        {
            return (X, Y);
        }
    }
}
