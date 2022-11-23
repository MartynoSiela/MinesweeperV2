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
            
            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < colsCount; col++)
                {
                    if (cells[col, row].isRevealed || revealAll)
                    {
                        if (cells[col, row].isMine)
                            Console.Write($"X ");
                        else
                        {
                            Console.Write($"{cells[col, row].neighbouringMinesCount} ");
                        }
                    }
                    else
                    {
                        Console.Write($"_ ");
                    }
                    
                }
                Console.WriteLine();
            }
        }
    }
}