using MinesweeperCode;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Welcome to the pasta minesweeper!!!");

int boardWidth = Game.EnterInteger("Please enter board width:", "board width", 1, 50);
int boardHeight = Game.EnterInteger("Please enter board height:", "board height", 1, 50);
int minesCount = Game.EnterInteger("Please enter mines count:", "mines count", 0, boardWidth * boardHeight - 1);

Board board = new Board(boardHeight, boardWidth, minesCount);
bool isFirstClick = true;
bool gameEnd = false;
bool win = false;
board.PrintBoard();

while (!gameEnd)
{
    int col = 0;
    int row = 0;

    col = Game.EnterCoordinate(board, "x", board.colsCount);
    row = Game.EnterCoordinate(board, "y", board.rowsCount);

    if (board.cells[col, row].isRevealed)
    {
        Console.WriteLine($"Cell {{{row + 1}, {col + 1}}} is already revealed, please pick another cell");
        col = Game.EnterCoordinate(board, "x", board.colsCount);
        row = Game.EnterCoordinate(board, "y", board.rowsCount);
    }

    if (board.cells[col, row].isMine == false)
    {
        (gameEnd, win) = Game.RevealCell(board, row, col);
        isFirstClick = false;
    }
    else
    {
        if (isFirstClick)
        {
            board.minesArray = board.GenerateMinesArray(row * board.colsCount + col);
            board.CreateCells();
            board.CalculateNeighbouringMines();
            (gameEnd, win) = Game.RevealCell(board, row, col);
            isFirstClick = false;
        } 
        else
        {
            board.cells[col, row].isRevealed = true;
            Game.RevealAllMines(board);
            gameEnd = true;
        }
    }

    board.PrintBoard(false);
    if (gameEnd)
    {
        if (win)
        {
            Console.WriteLine("You win!!!");
        }
        else
        {
            Console.WriteLine("You messed up :(, gg...");
        }
    }
}