using MinesweeperCode;

Console.WriteLine("Welcome to the pasta minesweeper!!!");
int boardWidth = Game.EnterInteger("Please enter board width:", "board width", 1, 50);
int boardHeight = Game.EnterInteger("Please enter board height:", "board height", 1, 50);

int minesCount = Game.EnterInteger("Please enter mines count:", "mines count", 0, boardWidth * boardHeight);

Board board = new Board(boardHeight, boardWidth, minesCount);
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
            gameEnd = true;
            win = true;
        }
    }
    else
    {
        board.cells[col, row].isRevealed = true;
        Game.RevealAllMines(board);
        gameEnd = true;
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
            Console.WriteLine("You fucked up :(, gg...");
        }
    }
}

return 0;
