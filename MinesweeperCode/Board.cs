namespace MinesweeperCode
{
    internal class Board
    {
        internal Cell[,] cells { get; set; }
        internal int rowsCount { get; set; }
        internal int colsCount { get; set; }
        internal int minesCount { get; set; }
        internal int[] minesArray { get; set; }
        internal int revealedCellsCount { get; set; }

        internal Board(int sizeRow, int sizeCol, int minesCount)
        {
            this.rowsCount = sizeRow;
            this.colsCount = sizeCol;
            this.minesCount = minesCount;
            cells = new Cell[colsCount, rowsCount];
            revealedCellsCount = 0;
            minesArray = GenerateMinesArray();
            CreateCells();
            CalculateNeighbouringMines();
        }

        internal int[] GenerateMinesArray(int? cellToExclude = null)
        {
            Random random = new Random();
            HashSet<int> randomNumbers = new HashSet<int>();

            while (randomNumbers.Count < minesCount)
            {
                int randomNumber = random.Next(rowsCount * colsCount);

                if (randomNumber != cellToExclude)
                {
                    randomNumbers.Add(randomNumber);
                }
            }

            return randomNumbers.ToArray();
        }

        internal void CreateCells()
        {
            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < colsCount; col++) {
                    if (minesArray.Contains(row * colsCount + col))
                    {
                        cells[col, row] = new Cell(true);
                    }
                    else
                    {
                        cells[col, row] = new Cell(false);
                    }
                }
            }
        }

        internal void CalculateNeighbouringMines()
        {
            int[] rowOffsets = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] colOffsets = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < colsCount ; col++)
                {
                    int neighbouringMines = 0;

                    for (int i = 0; i < 8; i++)
                    {
                        int colOffset = col + colOffsets[i];
                        int rowOffset = row + rowOffsets[i];
                        if (colOffset >= 0 && colOffset < colsCount && rowOffset >= 0 && rowOffset < rowsCount)
                        {
                            if (cells[colOffset, rowOffset].isMine)
                            {
                                neighbouringMines++;
                            }
                        }
                    }

                    cells[col, row].neighbouringMinesCount = neighbouringMines;
                }
            }
        }

        private void PrintBoardRowIndexes()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("     ");
            for (int i = 0; i < colsCount; i++)
            {
                Console.Write($"{(i + 1) / 10} ", Console.ForegroundColor);
            }
            Console.WriteLine();
            Console.Write("     ");
            for (int i = 0; i < colsCount / 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (j == 9)
                    {
                        Console.Write($"0 ", Console.ForegroundColor);
                    }
                    else
                    {
                        Console.Write($"{j + 1} ", Console.ForegroundColor);
                    }
                }
            }
            for (int i = 0; i < colsCount % 10; i++)
            {
                Console.Write($"{i + 1} ", Console.ForegroundColor);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private void PrintBoardColumnIndexes(int row)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (row < 9)
            {
                Console.Write($" 0{row + 1}  ", Console.ForegroundColor);
            }
            else
            {
                Console.Write($" {row + 1}  ", Console.ForegroundColor);
            }
        }

        internal void PrintBoard(bool revealAll = false)
        {
            PrintBoardRowIndexes();

            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = -1; col <= colsCount; col++)
                {
                    if (col == -1)
                    {
                        PrintBoardColumnIndexes(row);
                    }
                    else if (col == colsCount)
                    {
                        PrintBoardColumnIndexes(row);
                    }
                    else if (cells[col, row].isRevealed || revealAll)
                    {
                        if (cells[col, row].isMine)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write($"X ", Console.ForegroundColor);
                        }
                        else
                        {
                            int mineCount = cells[col, row].neighbouringMinesCount;
                            Cell.SetCellColor(mineCount);
                            Console.Write($"{cells[col, row].neighbouringMinesCount} ", Console.ForegroundColor);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"🭶", Console.ForegroundColor);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            PrintBoardRowIndexes();
        }
    }
}