namespace MinesweeperCode
{
    internal class Game
    {
        internal static int EnterInteger(string message, string integerName, int minValue, int maxValue)
        {
            bool isValidInteger = false;
            int integer = 0;
            while (!isValidInteger)
            {
                Console.WriteLine(message);
                try
                {
                    integer = Convert.ToInt32(Console.ReadLine());
                    if (integer < minValue || integer > maxValue)
                    {
                        Console.WriteLine($"The {integerName} must be between {minValue} and {maxValue} :/");
                    }
                    else
                    {
                        isValidInteger = true;
                    }
                }
                catch
                {
                    Console.WriteLine("An integer is needed...");
                }
            }
            return integer;
        }
        internal static int EnterCoordinate(Board board, string coordinateName, int dimension)
        {
            bool isValidCoordinate = false;
            int coordinate = 0;
            while (!isValidCoordinate)
            {
                Console.WriteLine($"Enter cell {coordinateName} coordinate: ");
                string? coordinateInput = Console.ReadLine();
                try
                {
                    coordinate = Convert.ToInt32(coordinateInput);
                    if (coordinate < 1 || coordinate > board.colsCount)
                    {
                        Console.WriteLine($"Cell's {coordinateName} coordinate must be between 1 and {board.colsCount}!");
                    }
                    else
                    {
                        isValidCoordinate = true;
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter an integer");
                }
            }

            return coordinate - 1;
        }

        internal static void RevealAllMines(Board board)
        {
            for (int i = 0; i < board.minesArray.Length; i++)
            {
                int row = board.minesArray[i] / board.colsCount;
                int col = board.minesArray[i] % board.colsCount;
                board.cells[col, row].isRevealed = true;
            }
        }
        private static bool IsValid(Board board, int row, int col)
        {
            return (row >= 0) && (row < board.rowsCount)
                && (col >= 0) && (col < board.colsCount)
                && (board.cells[col, row].isRevealed != true); 
                //&& (board.cells[col, row].neighbouringMinesCount == 0);
        }
        internal static void RevealAllNeighbouringEmptyCells(Board board, int row, int col)
        {
            int[] rowOffsets = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] colOffsets = { -1, 0, 1, -1, 1, -1, 0, 1 };

            Queue<Node> queue = new Queue<Node>();
            board.cells[col, row].isRevealed = true;
            board.revealedCellsCount++;
            queue.Enqueue(new Node(row, col));

            while (queue.Any())
            {
                Node node = queue.Dequeue();
                col = node.col;
                row = node.row;

                for (int i = 0; i < 8; i++)
                {
                    int rowOffset = row + colOffsets[i];
                    int colOffset = col + rowOffsets[i];
                    if (IsValid(board, rowOffset, colOffset))
                    {
                        board.cells[colOffset, rowOffset].isRevealed = true;
                        board.revealedCellsCount++;
                        if (board.cells[colOffset, rowOffset].neighbouringMinesCount == 0)
                        {
                            queue.Enqueue(new Node(rowOffset, colOffset));
                        }
                    }
                }
            }
        }

        internal static (bool, bool) RevealCell(Board board, int row, int col)
        {
            (bool gameEnd, bool win) isEndOfGame = (false, false);
            if (board.cells[col, row].neighbouringMinesCount == 0)
            {
                Game.RevealAllNeighbouringEmptyCells(board, row, col);
            }
            else
            {
                board.cells[col, row].isRevealed = true;
                board.revealedCellsCount++;
            }

            if (board.revealedCellsCount == board.colsCount * board.rowsCount - board.minesCount)
            {
                Game.RevealAllMines(board);
                isEndOfGame.gameEnd = true;
                isEndOfGame.win = true;
            }
            return isEndOfGame;
        }
    }

    internal class Node
    {
        internal int row;
        internal int col;
        internal Node(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
}
