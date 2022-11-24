namespace MinesweeperCode
{
    internal class Cell
    {
        internal bool isMine { get; set; }
        internal int neighbouringMinesCount { get; set; }
        internal bool isRevealed { get; set; }

        internal Cell(bool isMine)
        {
            this.isMine = isMine;
            neighbouringMinesCount = 0;
            isRevealed = false;
        }

        internal static void SetCellColor(int mineCount)
        {
            switch (mineCount)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }
}
