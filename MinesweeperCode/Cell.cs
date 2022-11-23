namespace MinesweeperCode
{
    public class Cell
    {
        public bool isMine { get; set; }
        public int neighbouringMinesCount { get; set; }
        public bool isRevealed { get; set; }

        public Cell(bool isMine)
        {
            this.isMine = isMine;
            neighbouringMinesCount = 0;
            isRevealed = false;
        }
    }
}
