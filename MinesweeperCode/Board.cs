namespace MinesweeperCode
{
    public class Board
    {
        public Cell[,] cells { get; set; }
        public int rowsCount { get; set; }
        public int colsCount { get; set; }
        public int minesCount { get; set; }
        public int[] minesArray { get; set; }

        public int revealedCellsCount { get; set; }

        public Board(int sizeRow, int sizeCol, int minesCount)
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

        private int[] GenerateMinesArray()
        {
            Random random = new Random();
            HashSet<int> randomNumbers = new HashSet<int>();

            while (randomNumbers.Count < minesCount)
            {
                randomNumbers.Add(random.Next(rowsCount * colsCount));
            }

            return randomNumbers.ToArray();
        }

        private void CreateCells()
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

        private void CalculateNeighbouringMines()
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

        public void PrintMinesArray()
        {
            for (int i = 0; i < minesArray.Length; i++)
            {
                Console.Write($"{minesArray[i]}, ");
            }
            Console.WriteLine();
        }

        public void PrintBoard(bool revealAll = false)
        {
            Console.Write("     ");
            for (int i = 0; i < colsCount; i++)
            {
                Console.Write($"{(i + 1) / 10} ");
            }
            Console.WriteLine();
            Console.Write("     ");
            for (int i = 0; i < colsCount / 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (j == 9)
                    {
                        Console.Write($"0 ");
                    }
                    else
                    {
                        Console.Write($"{j + 1} ");
                    }
                }
            }
            for (int i = 0; i < colsCount % 10; i++)
            {
                Console.Write($"{i + 1} ");
            }
            Console.WriteLine();
            Console.WriteLine();

            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = -1; col <= colsCount; col++)
                {
                    if (col == -1)
                    {
                        if (row < 9)
                        {
                            Console.Write($" 0{row + 1}  ");
                        }
                        else
                        {
                            Console.Write($" {row + 1}  ");
                        }
                    }
                    else if (col == colsCount)
                    {
                        if (row < 9)
                        {
                            Console.Write($" 0{row + 1}");
                        }
                        else
                        {
                            Console.Write($" {row + 1}");
                        }
                    }
                    else if (cells[col, row].isRevealed || revealAll)
                    {
                        if (cells[col, row].isMine)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write($"X ", Console.ForegroundColor);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            int mineCount = cells[col, row].neighbouringMinesCount;
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

                            }
                            Console.Write($"{cells[col, row].neighbouringMinesCount} ", Console.ForegroundColor);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        Console.Write($"_ ");
                    }
                    
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.Write("     ");
            for (int i = 0; i < colsCount; i++)
            {
                Console.Write($"{(i + 1) / 10} ");
            }
            Console.WriteLine();
            Console.Write("     ");
            for (int i = 0; i < colsCount / 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (j == 9)
                    {
                        Console.Write($"0 ");
                    }
                    else
                    {
                        Console.Write($"{j + 1} ");
                    }
                }
            }

            for (int i = 0; i < colsCount % 10; i++)
            {
                Console.Write($"{i + 1} ");
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}